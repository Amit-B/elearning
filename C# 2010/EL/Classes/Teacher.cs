using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace EL.Classes
{
    /// <summary>
    /// מחלקה המכילה פעולות הקשורות במורים.
    /// </summary>
    class Teacher
    {
        /// <summary>
        /// מציאת כל המורים הקיימים.
        /// </summary>
        /// <param name="inactives">חיפוש גם בלא פעילים</param>
        /// <returns>רשימת מורים</returns>
        public static DataTable List(bool inactives = false)
        {
            return inactives ? Classes.SQL.Select("Teachers") : Classes.SQL.Select("Teachers", "active", true);
        }
        /// <summary>
        /// בדיקה אם מורה קיים.
        /// </summary>
        /// <param name="id">ת.ז של המורה לבדיקה</param>
        /// <returns>אמת או שקר אם הת.ז קיימת ברשימה</returns>
        public static bool Exists(string id)
        {
            DataTable dt = List();
            for (int i = 0; i < dt.Rows.Count; i++) if (dt.Rows[i]["tid"].ToString() == id) return true;
            return false;
        }
        /// <summary>
        /// מציאת מידע על מורה מסויים.
        /// </summary>
        /// <param name="id">ת.ז של המורה</param>
        /// <param name="key">המידע להשגה</param>
        /// <returns>המידע</returns>
        public static string GetInfo(string id, string key)
        {
            return Classes.SQL.Select("Teachers", key, "tid", id).ToString();
        }
        /// <summary>
        /// מציאת השם המלא (פרטי + משפחה) של מורה מסויים.
        /// </summary>
        /// <param name="id">ת.ז של המורה</param>
        /// <returns>השם המלא</returns>
        public static string GetName(string id)
        {
            DataTable dt = Classes.SQL.Select("Teachers", "tid", id);
            return dt.Rows[0]["firstname"].ToString() + " " + dt.Rows[0]["lastname"].ToString();
        }
        /// <summary>
        /// הוספת מורה חדש.
        /// </summary>
        /// <param name="id">ת.ז</param>
        /// <param name="fname">שם פרטי</param>
        /// <param name="lname">שם משפחה</param>
        /// <param name="gender">מין</param>
        /// <param name="phone">טלפון</param>
        /// <param name="pass">סיסמת כניסה</param>
        /// <param name="admin">ניהול ראשי</param>
        /// <param name="dutyid">מספר תפקיד</param>
        public static void Add(string id, string fname, string lname, App.Gender gender, string phone, string pass, bool admin, int dutyid)
        {
            Classes.SQL.Insert("Teachers", "`tid`,`firstname`,`lastname`,`gender`,`phone`,`password`,`admin`,`dutyid`,`active`", string.Format("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}',{8}", id, fname, lname, Classes.App.GenderNum(gender), phone, pass, Convert.ToInt32(admin), dutyid, true));
        }
        /// <summary>
        /// מחיקת מורה.
        /// </summary>
        /// <param name="id">ת.ז של המורה</param>
        public static void Kill(string id)
        {
            Classes.SQL.Update("Teachers", "`active` = " + false, "tid", id);
        }
        /// <summary>
        /// החזרת מורה מחוק.
        /// </summary>
        /// <param name="id">ת.ז של המורה</param>
        public static void Revive(string id)
        {
            Classes.SQL.Update("Teachers", "`active` = " + false, "tid", id);
        }
    }
}