using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace EL.Classes
{
    /// <summary>
    /// מחלקה המכילה פעולות הקשורות בתלמידים.
    /// </summary>
    class Student
    {
        /// <summary>
        /// מציאת כל התלמידים הקיימים.
        /// </summary>
        /// <param name="inactives">חיפוש גם בלא פעילים</param>
        /// <returns>רשימת תלמידים</returns>
        public static DataTable List(bool inactives = false)
        {
            return inactives ? Classes.SQL.Select("Students") : Classes.SQL.Select("Students", "active", true);
        }
        /// <summary>
        /// בדיקה אם תלמיד קיים.
        /// </summary>
        /// <param name="id">ת.ז של תלמיד לבדיקה</param>
        /// <returns>אמת או שקר אם הת.ז קיימת ברשימה</returns>
        public static bool Exists(string id)
        {
            return Classes.SQL.Select("Students", "id", id).Rows.Count > 0;
        }
        /// <summary>
        /// מציאת מידע על תלמיד מסויים.
        /// </summary>
        /// <param name="id">ת.ז של התלמיד</param>
        /// <param name="key">המידע להשגה</param>
        /// <returns>המידע</returns>
        public static string GetInfo(string id, string key)
        {
            return Classes.SQL.Select("Students", key, "id", id).ToString();
        }
        /// <summary>
        /// מציאת השם המלא (פרטי ומשפחה) של תלמיד.
        /// </summary>
        /// <param name="id">ת.ז של התלמיד</param>
        /// <returns>השם המלא</returns>
        public static string GetName(string id)
        {
            DataTable dt = Classes.SQL.Select("Students","id",id);
            return dt.Rows[0]["firstname"].ToString() + " " + dt.Rows[0]["lastname"].ToString();
        }
        /// <summary>
        /// מציאת כל המידע האפשרי על תלמיד מסויים.
        /// </summary>
        /// <param name="id">ת.ז של התלמיד</param>
        /// <returns>כל המידע</returns>
        public static DataRow GetFullInfo(string id)
        {
            return Classes.SQL.Select("Students", "id", id).Rows[0];
        }
        /// <summary>
        /// הוספת תלמיד חדש.
        /// </summary>
        /// <param name="id">ת.ז</param>
        /// <param name="fname">שם פרטי</param>
        /// <param name="lname">שם משפחה</param>
        /// <param name="classid">קוד כיתה</param>
        /// <param name="gender">מין</param>
        /// <param name="phone">טלפון</param>
        public static void Add(string id, string fname, string lname, string classid, App.Gender gender, string phone)
        {
            Classes.SQL.Insert("Students", "`id`,`firstname`,`lastname`,`classid`,`regdate`,`enddate`,`gender`,`phone`,`active`,`fastnote`", string.Format("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}'", id, fname, lname, classid, DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("dd/MM/yyyy"), App.GenderNum(gender), phone, true, string.Empty));
        }
        /// <summary>
        /// עריכת תלמיד.
        /// </summary>
        /// <param name="id">ת.ז</param>
        /// <param name="fname">שם פרטי</param>
        /// <param name="lname">שם משפחה</param>
        /// <param name="classid">קוד כיתה</param>
        /// <param name="regdate">תאריך הרשמה</param>
        /// <param name="enddate">תאריך יציאה</param>
        /// <param name="gender">מין</param>
        /// <param name="phone">טלפון</param>
        /// <param name="active">פעיל</param>
        /// <param name="fastnote">הערה מהירה</param>
        public static void Edit(string id, string fname, string lname, string classid, DateTime regdate, DateTime enddate, App.Gender gender, string phone, bool active, string fastnote)
        {
            Classes.SQL.Update("Students", "`id` = '" + id + "', `firstname` = '" + fname + "', `lastname` = '" + lname + "', `classid` = '" + classid + "', `regdate` = '" + regdate.ToShortDateString() + "', `enddate` = '" + enddate.ToShortDateString() + "', `gender` = '" + App.GenderNum(gender) + "', `phone` = '" + phone + "', `active` = " + active + ", `fastnote` = '" + fastnote + "'", "id", id);
        }
        /// <summary>
        /// מחיקת תלמיד.
        /// </summary>
        /// <param name="id">ת.ז</param>
        /// <param name="soft">מחיקה רכה</param>
        public static void Delete(string id, bool soft)
        {
            if (soft) Classes.SQL.Update("Students", "`enddate` = '" + DateTime.Now.ToString("dd/MM/yyyy") + "', `active` = '" + false + "'", "id", id);
            else Classes.SQL.Delete("Students", "id", id);
        }
    }
}