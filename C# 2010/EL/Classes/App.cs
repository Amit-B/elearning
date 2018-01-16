using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
namespace EL.Classes
{
    /// <summary>
    /// ספריית שימוש בתוכנה זו.
    /// </summary>
    class App
    {
        public enum Gender { Male, Female }
        public enum DataGridStyles { Students, Lessons, Notes, GradeLists }
        /// <summary>
        /// מציאת מיקום הייעד של תיקיית התוכנה.
        /// </summary>
        /// <returns>המיקום</returns>
        public static string GetProgramDirectory()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "/" + EL.Properties.Resources.Folder;
        }
        /// <summary>
        /// בדיקת מספר מין.
        /// </summary>
        /// <param name="g">מין</param>
        /// <returns>מספר</returns>
        public static int GenderNum(Gender g)
        {
            return g == Gender.Male ? 1 : 2;
        }
        /// <summary>
        /// בדיקת שם מין.
        /// </summary>
        /// <param name="g">מספר</param>
        /// <returns>שם</returns>
        public static string GenderName(int g)
        {
            return g == 1 || g == 2 ? (g == 1 ? "זכר" : "נקבה") : string.Empty;
        }
        /// <summary>
        /// בדיקת שם מין.
        /// </summary>
        /// <param name="g">סוג</param>
        /// <returns>שם</returns>
        public static string GenderName(Gender g)
        {
            return g == Gender.Male ? "זכר" : "נקבה";
        }
        /// <summary>
        /// שליחת הודעת תקלה.
        /// </summary>
        /// <param name="text">טקסט התקלה</param>
        /// <param name="abort">האם לא ניתן להשתמש בתוכנה</param>
        public static void Error(string text, bool abort = false)
        {
            new Forms.Error(text, abort).ShowDialog();
        }
        /// <summary>
        /// קבלת המספר הסידורי הבא של רשומה בטבלה כלשהית.
        /// </summary>
        /// <param name="table">שם הטבלה</param>
        /// <param name="key">שם המפתח של המספר הסידורי</param>
        /// <returns>המספר הסידורי הבא</returns>
        public static int GetNextID(string table, string key)
        {
            int id = 0;
            while (Classes.SQL.Select(table, key, id.ToString()).Rows.Count > 0)
                id++;
            return id;
        }
        /// <summary>
        /// החזרת מספר עם כמות אפסים מאחוריו.
        /// </summary>
        /// <param name="num">המספר</param>
        /// <param name="zeros">כמות אפסים</param>
        /// <returns>המספר החדש</returns>
        public static string IntBase(int num, int zeros)
        {
            string ret = num.ToString();
            if (num < (int)Math.Pow((double)zeros, (double)10))
                for (int i = 0; i < zeros - 1; i++)
                    ret = "0" + ret;
            return ret;
        }
        /// <summary>
        /// הפיכת שדה מסוג דטא גריד לטבלה מסודרת לפי סטייל קבוע.
        /// </summary>
        /// <param name="dataGridView">השדה</param>
        /// <param name="style">סוג הסטייל</param>
        public static void GenerateDataGridViewStyle(DataGridView dataGridView, DataGridStyles style)
        {
            int gridStatus = -1;
            switch (style)
            {
                case DataGridStyles.Students:
                    {
                        object[,] columns = {
                                                /* 0 */ { "ת.ז", 90 },
                                                /* 1 */ { "שם פרטי", 108 },
                                                /* 2 */ { "שם משפחה", 108 },
                                                /* 3 */ { "כיתה", 45 },
                                                /* 4 */ { "תאריך הרשמה", 120 },
                                                /* 5 */ { "תאריך סיום", 120 },
                                                /* 6 */ { "מין", 45 },
                                                /* 7 */ { "פעיל", 45 }
                                            };
                        dataGridView.Columns.RemoveAt(9);
                        dataGridView.Columns.RemoveAt(6);
                        for (int i = 0, j = columns.GetLength(0); i < j; i++)
                        {
                            dataGridView.Columns[i].HeaderText = columns[i, 0].ToString();
                            dataGridView.Columns[i].Width = (int)columns[i, 1];
                        }
                        for (int i = 0; i < dataGridView.Rows.Count; i++)
                        {
                            dataGridView.Rows[i].Cells[3].Value = Classes.Class.ClassNameByCode(dataGridView.Rows[i].Cells[3].Value.ToString());
                            dataGridView.Rows[i].Cells[6].Value = Classes.App.GenderName(Convert.ToInt32(dataGridView.Rows[i].Cells[6].Value.ToString()));
                        }
                        gridStatus = 0;
                        break;
                    }
                case DataGridStyles.Lessons:
                    {
                        object[,] columns = {
                                  /* 0 */ { "קוד", 65 },
                                  /* 1 */ { "מקצוע", 150 },
                                  /* 2 */ { "מורה", 150 },
                                  /* 3 */ { "תאריך", 100 },
                                  /* 4 */ { "התקיים", 75 },
                                  /* 5 */ { "נושא", 200 }
                                    };
                        dataGridView.Columns.RemoveAt(8);
                        dataGridView.Columns.RemoveAt(7);
                        dataGridView.Columns.RemoveAt(6);
                        for (int i = 0, j = columns.GetLength(0); i < j; i++)
                        {
                            dataGridView.Columns[i].HeaderText = columns[i, 0].ToString();
                            dataGridView.Columns[i].Width = (int)columns[i, 1];
                        }
                        MessageBox.Show("1");
                        for (int i = 0; i < dataGridView.Rows.Count; i++)
                        {
                            dataGridView.Rows[i].Cells[1].Value = Classes.Subject.GetName(Classes.Group.GetSubject(Convert.ToInt32(dataGridView.Rows[i].Cells[1].Value.ToString()))) + " (" + Classes.Group.GetName(Convert.ToInt32(dataGridView.Rows[i].Cells[1].Value.ToString())) + ")";
                            dataGridView.Rows[i].Cells[2].Value = Classes.Teacher.GetName(dataGridView.Rows[i].Cells[2].Value.ToString());
                        }
                        break;
                    }
                case DataGridStyles.Notes:
                    {
                        object[,] columns = {
                                  /* 0 */ { "מ.ס", 65 },
                                  /* 1 */ { "שיעור", 75 },
                                  /* 2 */ { "תלמיד", 150 },
                                  /* 3 */ { "סוג הערה", 120 },
                                  /* 4 */ { "מורה", 150 },
                                  /* 5 */ { "מוצדק", 60 },
                                  /* 6 */ { "הסבר", 200 }
                                    };
                        dataGridView.Columns.RemoveAt(7);
                        for (int i = 0, j = columns.GetLength(0); i < j; i++)
                        {
                            dataGridView.Columns[i].HeaderText = columns[i, 0].ToString();
                            dataGridView.Columns[i].Width = (int)columns[i, 1];
                        }
                        for (int i = 0; i < dataGridView.Rows.Count; i++)
                        {
                            dataGridView.Rows[i].Cells[2].Value = Classes.Student.GetName(dataGridView.Rows[i].Cells[2].Value.ToString());
                            dataGridView.Rows[i].Cells[3].Value = Classes.Note.GetNoteTypeInfo(Convert.ToInt32(dataGridView.Rows[i].Cells[3].Value.ToString()))["ntitle"].ToString();
                            dataGridView.Rows[i].Cells[4].Value = Classes.Teacher.GetName(dataGridView.Rows[i].Cells[4].Value.ToString());
                        }
                        break;
                    }
                case DataGridStyles.GradeLists:
                    {
                        object[,] columns = {
                                  /* 0 */ { "מ.ס", 65 },
                                  /* 1 */ { "סוג ציון", 80 },
                                  /* 2 */ { "תאריך", 120 },
                                  /* 3 */ { "מידע", 250 },
                                  /* 4 */ { "קבוצת לימוד", 150 },
                                  /* 5 */ { "מורה", 200 }
                                    };
                        for (int i = 0, j = columns.GetLength(0); i < j; i++)
                        {
                            dataGridView.Columns[i].HeaderText = columns[i, 0].ToString();
                            dataGridView.Columns[i].Width = (int)columns[i, 1];
                        }
                        dataGridView.Columns.RemoveAt(3);
                        for (int i = 0; i < dataGridView.Rows.Count; i++)
                        {
                            dataGridView.Rows[i].Cells[1].Value = Classes.Grades.GetGradeType(dataGridView.Rows[i].Cells[1].Value.ToString());
                            dataGridView.Rows[i].Cells[3].Value = Classes.Group.GetName(Convert.ToInt32(dataGridView.Rows[i].Cells[3].Value));
                            dataGridView.Rows[i].Cells[4].Value = Classes.Teacher.GetName(dataGridView.Rows[i].Cells[4].Value.ToString());
                        }
                        break;
                    }
            }
            switch (gridStatus)
            {
                case 0:
                    {
                        dataGridView.AllowUserToAddRows = false;
                        dataGridView.AllowUserToDeleteRows = false;
                        dataGridView.MultiSelect = false;
                        dataGridView.ReadOnly = true;
                        dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        break;
                    }
            }
        }
    }
}