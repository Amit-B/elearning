using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace EL.Classes
{
    /// <summary>
    /// ספרייה המשמשת לפעולות בנושא ציונים, רשימות ציונים וסוגי ציונים.
    /// </summary>
    class Grades
    {
        /// <summary>
        /// מציאת כל רשימות הציונים.
        /// </summary>
        /// <returns>רשימות הציונים</returns>
        public static DataTable GetGradeLists()
        {
            return SQL.Select("GradeLists");
        }
        /// <summary>
        /// מציאת כל רשימות הציונים לפי תנאי.
        /// </summary>
        /// <param name="getby">התנאי</param>
        /// <param name="value">ערך התנאי</param>
        /// <returns>רשימות הציונים</returns>
        public static DataTable GetGradeListsBy(string getby, string value)
        {
            return SQL.Select("GradeLists", getby, value);
        }
        /// <summary>
        /// הוספת רשימת ציונים.
        /// </summary>
        /// <param name="tid">יוצר הרשימה</param>
        /// <param name="dt">תאריך ושעה של קיום מדד הציון</param>
        /// <param name="title">מידע כללי</param>
        /// <param name="gid">קבוצת לימוד</param>
        /// <returns>מספר הרשימה החדשה</returns>
        public static int AddGradeList(string gtid, string tid, string dt, string title, string gid)
        {
            int id = Classes.App.GetNextID("GradeLists", "glid");
            Classes.SQL.Insert("GradeLists", "glid,gtid,gldt,gltitle,gid,tid", string.Format("'{0}','{1}','{2}','{3}','{4}','{5}'", id.ToString(), gtid, dt, title, gid, tid));
            return id;
        }
        /// <summary>
        /// מחיקת רשימת ציונים.
        /// </summary>
        /// <param name="glid">מספר הרשימה</param>
        public static void DeleteGradeList(string glid)
        {
            Classes.SQL.Delete("GradeLists", "glid", glid);
        }
        /// <summary>
        /// ספירת רשימות הציונים.
        /// </summary>
        /// <param name="glid">מספר הרשימה</param>
        /// <returns>כמות</returns>
        public static int CountGradeList(string glid)
        {
            return GetGradeListsBy("glid", glid).Rows.Count;
        }
        /// <summary>
        /// עריכת רשימת ציונים.
        /// </summary>
        /// <param name="glid">מספר הרשימה לעריכה</param>
        /// <param name="dt">תאריך ושעה חדשים</param>
        /// <param name="title">מידע חדש</param>
        /// <param name="gid">קבוצת הלימוד</param>
        public static void EditGradeList(string glid, string dt, string title, string gid, string gtid)
        {
            Classes.SQL.Update("GradeLists", "`gldt`='" + dt + "',`gltitle`='" + title + "',`gid`='" + gid + "',`gtid`='" + gtid + "'", "glid", glid);
        }
        /// <summary>
        /// מציאת ציונים.
        /// </summary>
        /// <param name="glid">רשימת ציונים למציאת כל הציונים</param>
        /// <returns>הציונים</returns>
        public static DataTable GetGrades(string glid)
        {
            return Classes.SQL.Select("Grades", "glid", glid);
        }
        /// <summary>
        /// מציאת ציון יחיד.
        /// </summary>
        /// <param name="id">ת.ז של תלמיד</param>
        /// <param name="glid">מספר רשימת הציונים</param>
        /// <returns>הציון</returns>
        public static int GetGrade(string id, string glid)
        {
            Classes.SQL.Query("SELECT * FROM `Grades` WHERE `glid` = '" + glid + "' AND `id` = '" + id + "'");
            return Classes.Text.LanguageIs(Classes.SQL.ds.Tables[0].Rows[0]["grade"].ToString(),Text.Language.Numbers) ? Convert.ToInt32(Classes.SQL.ds.Tables[0].Rows[0]["grade"]) : 0;
        }
        /// <summary>
        /// שינוי ציון יחיד.
        /// </summary>
        /// <param name="id">ת.ז של תלמיד</param>
        /// <param name="glid">מספר רשימת הציונים</param>
        /// <param name="newgrade">הציון החדש</param>
        public static void SetGrade(string id, string glid, int newgrade)
        {
            Classes.SQL.Query("UPDATE `Grades` SET `grade` = '" + newgrade + "' WHERE `glid` = '" + glid + "' AND `id` = '" + id + "'");
        }
        /// <summary>
        /// הוספת ציון לרשימת ציונים.
        /// </summary>
        /// <param name="id">ת.ז של התלמיד</param>
        /// <param name="glid">מספר רשימת הציונים</param>
        /// <param name="grade">הציון להוספה</param>
        public static void AddGrade(string id, string glid, int grade)
        {
            Classes.SQL.Insert("Grades", "glid,id,grade", string.Format("'{0}','{1}','{2}'", glid, id, grade.ToString()));
        }
        /// <summary>
        /// מציאת מקצוע של רשימת ציונים.
        /// </summary>
        /// <param name="glid">מספר רשימת הציונים</param>
        /// <returns>מספר המקצוע</returns>
        public static int GetGradeListSubject(string glid)
        {
            return Classes.Group.GetSubject(Convert.ToInt32(SQL.Select("GradeLists", "glid", glid).Rows[0]["gid"]));
        }
        /// <summary>
        /// מציאת רשימת סוגי הציונים.
        /// </summary>
        /// <returns>הרשימה</returns>
        public static DataTable ListGradeTypes()
        {
            return Classes.SQL.Select("GradeTypes");
        }
        /// <summary>
        /// מציאת סוג ציון יחיד.
        /// </summary>
        /// <param name="gtid">מספר הסוג</param>
        /// <returns>שם הסוג</returns>
        public static string GetGradeType(string gtid)
        {
            return Classes.SQL.Select("GradeTypes", "gtid", gtid).Rows[0]["gtname"].ToString();
        }
        /// <summary>
        /// הוספת סוג ציון חדש.
        /// </summary>
        /// <param name="gtname">שם הסוג</param>
        /// <returns>מספר הסוג החדש</returns>
        public static int AddGradeType(string gtname)
        {
            int id = Classes.App.GetNextID("GradeTypes", "gtid");
            Classes.SQL.Insert("GradeTypes", "gtid,gtname", string.Format("'{0}','{1}'", id.ToString(), gtname));
            return id;
        }
        /// <summary>
        /// מחיקת סוג ציון קיים.
        /// </summary>
        /// <param name="gtid">מספר הסוג</param>
        public static void DeleteGradeType(string gtid)
        {
            Classes.SQL.Update("GradeLists", "`gtid` = '0'", "gtid", gtid);
            Classes.SQL.Delete("GradeLists", "gtid", gtid);
        }
    }
}