using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
namespace EL.Classes
{
    /// <summary>
    /// ספריית פעולות סטרינג והצפנה.
    /// </summary>
    class Text
    {
        public enum Language { English, EnglishC, Hebrew, Numbers }
        /// <summary>
        /// העברת טקסט לטקסט קריא ע"י הפונקציה Classes.Audio.TTS.
        /// </summary>
        /// <param name="text">הטקסט</param>
        /// <returns>הטקסט באנגלית, קריא</returns>
        public static string ToReadableText(string text)
        {
            string returned = string.Empty;
            for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] == 'א') returned += "a";
                    else if (text[i] == 'ב') returned += "b";
                    else if (text[i] == 'ג') returned += "g";
                    else if (text[i] == 'ד') returned += "d";
                    else if (text[i] == 'ה') returned += i == text.Length - 1 || (i != text.Length - 1 && text[i + 1] == ' ') ? "ah" : "h";
                    else if (text[i] == 'ו')
                    {
                        if (i < text.Length - 1 && text[i + 1] == 'ו')
                        {
                            returned += "v";
                            continue;
                        }
                        returned += "u";
                    }
                    else if (text[i] == 'ז') returned += "z";
                    else if (text[i] == 'ח') returned += "ch";
                    else if (text[i] == 'ט') returned += "t";
                    else if (text[i] == 'י') returned += i == text.Length - 1 || (i != text.Length - 1 && text[i + 1] == ' ') ? "y" : "i";
                    else if (text[i] == 'כ') returned += "c";
                    else if (text[i] == 'ל') returned += "l";
                    else if (text[i] == 'מ') returned += "me";
                    else if (text[i] == 'נ') returned += "n";
                    else if (text[i] == 'ס') returned += "s";
                    else if (text[i] == 'ע') returned += "aa";
                    else if (text[i] == 'פ') returned += "p";
                    else if (text[i] == 'צ') returned += "ts";
                    else if (text[i] == 'ק') returned += "k";
                    else if (text[i] == 'ר') returned += "r";
                    else if (text[i] == 'ש') returned += "sh";
                    else if (text[i] == 'ת') returned += "t";
                    else if (text[i] == 'ם') returned += "m";
                    else if (text[i] == 'ן') returned += "n";
                    else if (text[i] == 'ף') returned += "ph";
                    else if (text[i] == 'ך') returned += "ch";
                    else if (text[i] == 'ץ') returned += "ts";
                    else returned += text[i];
                }
            return returned;
        }
        /// <summary>
        /// בדיקת שפת טקסט כלשהו.
        /// </summary>
        /// <param name="str">הטקסט לבדיקה</param>
        /// <param name="type">השפה להשוואה</param>
        /// <returns>אמת או שקר אם השפה של הטקסט היא השפה שצויינה</returns>
        public static bool LanguageIs(string str, Language type)
        {
            string charsToCheck = string.Empty;
            if (type == Language.English) charsToCheck += "abcdefghijklmnopqrstuvwxyz";
            if (type == Language.EnglishC) charsToCheck += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (type == Language.Hebrew) charsToCheck += "אבגדהוזחטיכלמנסעפצקרשתןםךףץ";
            if (type == Language.Numbers) charsToCheck += "0123456789";
            for (int i = 0; i < str.Length; i++) if (!charsToCheck.Contains(str[i])) return false;
            return true;
        }
        /// <summary>
        /// בדיקת האם טקסט כלשהו הוא מספר ת.ז.
        /// </summary>
        /// <param name="str">הטקסט לבדיקה</param>
        /// <returns>אמת או שקר אם הטקסט הוא ת.ז</returns>
        public static bool IsIdentityNumber(string str)
        {
            return (str.Length == 9 && LanguageIs(str, Language.Numbers));
        }
        /// <summary>
        /// הפיכת פאנל לתיבת קוד אבטחה.
        /// </summary>
        /// <param name="labels">הלייבלים</param>
        /// <param name="xs">איקסים ראשוניים</param>
        /// <param name="panel">הפאנל</param>
        /// <returns>קוד האבטחה</returns>
        public static string GenerateCodePanel(Label[] labels, int[] xs, Panel panel)
        {
            string code = string.Empty, chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            string[] fonts = { "Arial", "Tahoma", "Times New Roman", "Microsoft Sans Serif", "Courier New" };
            FontStyle[] fs = { FontStyle.Regular, FontStyle.Bold, FontStyle.Italic, FontStyle.Underline, FontStyle.Bold | FontStyle.Italic };
            Random rnd = new Random();
            int num = rnd.Next(4, 9), grey = rnd.Next(50, 201);
            Color back = Color.FromArgb(rnd.Next(150, 201), grey, grey, grey);
            panel.BackColor = back;
            for (int i = 0; i < labels.Length; i++)
            {
                labels[i].Visible = i < num;
                if (labels[i].Visible)
                {
                    labels[i].Font = new Font(fonts[rnd.Next(0, fonts.Length)], (float)rnd.Next(9, 17), fs[rnd.Next(0, fs.Length)]);
                    labels[i].Text = chars[rnd.Next(0, chars.Length)].ToString();
                    code += labels[i].Text;
                    labels[i].ForeColor = Color.FromArgb(255, rnd.Next(20, 241), rnd.Next(20, 241), rnd.Next(20, 241));
                    labels[i].BackColor = back;
                    labels[i].Location = new Point(xs[i] + rnd.Next(0, 4), rnd.Next(1, 9));
                }
            }
            return code;
        }
        /// <summary>
        /// בדיקת תוכן שדה כתיבה.
        /// </summary>
        /// <param name="val">טקסט לבדיקה</param>
        /// <returns>בעיית תקינות</returns>
        public static int IsValidField(string val)
        {
            if (val.Length == 0) return 1;
            for (int i = 0; i < val.Length; i++) if (val[i] < 'a' && val[i] > 'z' && val[i] < 'A' && val[i] > 'Z' && val[i] < 'א' && val[i] > 'ת') return 2;
            return 0;
        }
        /// <summary>
        /// בדיקה האם שנה מסויימת תקינה.
        /// </summary>
        /// <param name="year">שנה</param>
        /// <returns>אמת או שקר אם השנה תקינה</returns>
        public static bool IsValidYear(int year)
        {
            return year >= 1970 && year <= DateTime.Now.Year;
        }
        /// <summary>
        /// עריכת טקסט לפי משתנים מוגדרים מראש.
        /// </summary>
        /// <param name="text">הטקסט</param>
        /// <param name="id">ת.ז של הנמען</param>
        /// <param name="status">האם הנמען הוא מורה?</param>
        /// <returns>הטקסט הערוך</returns>
        public static string TextFormatting(string text, string id, bool status)
        {
            string txt = text;
            Classes.SQL.Query("SELECT * FROM " + (status ? "`Teachers`" : "`Students`") + " WHERE " + (status ? "`tid`" : "`id`") + " = '" + id + "'");
            System.Data.DataRow row = Classes.SQL.ds.Tables[0].Rows[0];
            txt = txt.Replace("{FIRSTNAME}", row["firstname"].ToString());
            txt = txt.Replace("{LASTNAME}", row["lastname"].ToString());
            if (!status)
            {
                txt = txt.Replace("{CLASS}", Classes.Class.ClassNameByCode(row["classid"].ToString()));
                txt = txt.Replace("{STARTUM}", Classes.Class.GetStartumChar(Classes.Class.GetClCoStartumNum(row["classid"].ToString())));
                txt = txt.Replace("{CLASSNUM}", Classes.Class.GetClCoClassNum(row["classid"].ToString()).ToString());
            }
            txt = txt.Replace("{DATETIME}", DateTime.Now.ToString());
            txt = txt.Replace("{ID}", row[status ? "tid" : "id"].ToString());
            return txt;
        }
        /// <summary>
        /// הוספת שורה לסטרינג.
        /// </summary>
        /// <param name="text">המשתנה</param>
        /// <param name="line">השורה להוספה</param>
        public static void WriteLine(ref string text, string line)
        {
            text += line + "\r\n";
        }
    }
}