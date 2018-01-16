using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace EL.Classes
{
    /// <summary>
    /// מחלקה המכילה פעולות הקשורות ברשימת השיעורים הקיימים.
    /// </summary>
    class Lessons
    {
        public enum LessonSelectType { All, Exist, NExist }
        /// <summary>
        /// בדיקת מספר השיעורים הקיימים.
        /// </summary>
        /// <param name="exist">כולל שיעורים שהתבטלו</param>
        /// <returns>כמות השיעורים</returns>
        public static int Count(LessonSelectType exist = LessonSelectType.All)
        {
            int c = 0;
            switch (exist)
            {
                case LessonSelectType.All: c = Classes.SQL.Select("Lessons").Rows.Count; break;
                case LessonSelectType.Exist: c = Classes.SQL.Select("Lessons", "lexist", 1.ToString()).Rows.Count; break;
                case LessonSelectType.NExist: c = Classes.SQL.Select("Lessons", "lexist", 0.ToString()).Rows.Count; break;
            }
            return c;
        }
        /// <summary>
        /// קבלת רשימת השיעורים הקיימים.
        /// </summary>
        /// <param name="exist">כולל שיעורים שהתבטלו</param>
        /// <returns>רשימת השיעורים</returns>
        public static DataTable List(LessonSelectType exist = LessonSelectType.All)
        {
            DataTable list = null;
            switch (exist)
            {
                case LessonSelectType.All: list = Classes.SQL.Select("Lessons"); break;
                case LessonSelectType.Exist: list = Classes.SQL.Select("Lessons", "lexist", 1.ToString()); break;
                case LessonSelectType.NExist: list = Classes.SQL.Select("Lessons", "lexist", 0.ToString()); break;
            }
            return list;
        }
        /// <summary>
        /// בדיקה האם שיעור קיים לפי מספר שיעור.
        /// </summary>
        /// <param name="lid">מספר השיעור</param>
        /// <returns>אמת או שקר אם השיעור קיים</returns>
        public static bool Exists(int lid)
        {
            Classes.SQL.Query("SELECT * FROM `Lessons` WHERE `lid` = '" + lid + "' AND `active` = " + true);
            return Classes.SQL.ds.Tables[0].Rows.Count > 0;
        }
        /// <summary>
        /// הוספת שיעור חדש למאגר.
        /// </summary>
        /// <param name="gid">קבוצת לימוד</param>
        /// <param name="tid">מורה</param>
        /// <param name="dt">תאריך ושעה</param>
        /// <param name="exists">התקיים</param>
        /// <param name="title">מידע</param>
        /// <returns>מספר סידורי של השיעור החדש</returns>
        public static int AddLesson(int gid, int tid, DateTime dt, bool exists, string title)
        {
            int id = App.GetNextID("Lessons","lid");
            Classes.SQL.Insert("Lessons", "lid,gid,tid,ldate,lexist,ltitle,lastedit,lasteditdt,active", string.Format("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}'",
                id.ToString(), gid.ToString(), tid.ToString(), dt.ToString(), Convert.ToInt32(exists), title, tid.ToString(), dt.ToString(), true));
            return id;
        }
        /// <summary>
        /// מציאת מידע עבור שיעור מסויים.
        /// </summary>
        /// <param name="lid">מספר השיעור</param>
        /// <returns>המידע</returns>
        public static DataRow GetInfo(int lid)
        {
            return Classes.SQL.Select("Lessons", "lid", lid.ToString()).Rows[0];
        }
        /// <summary>
        /// מחיקת שיעור.
        /// </summary>
        /// <param name="lid">מספר השיעור</param>
        public static void Delete(int lid)
        {
            Classes.SQL.Update("Lessons", "`active` = " + false, "lid", lid.ToString());
        }
    }
}