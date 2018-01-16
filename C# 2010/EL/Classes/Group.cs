using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace EL.Classes
{
    /// <summary>
    /// מחלקה המכילה פעולות שקשורות בקבוצות לימוד.
    /// </summary>
    class Group
    {
        /// <summary>
        /// הוספת קבוצה חדשה.
        /// </summary>
        /// <param name="sid">מספר מקצוע</param>
        /// <param name="gclass">שכבה</param>
        /// <param name="tid">ת.ז של מורה הקבוצה</param>
        /// <param name="gname">שם הקבוצה</param>
        /// <returns>מספר הקבוצה החדשה</returns>
        public static int Add(string sid, string gclass, string tid, string gname)
        {
            int id = App.GetNextID("Groups", "gid");
            Classes.SQL.Insert("Groups", "`gid`,`sid`,`gclass`,`tid`,`gname`,`active`", string.Format("'{0}','{1}','{2}','{3}','{4}',{5}", id.ToString(), sid, gclass, tid, gname, true));
            return id;
        }
        /// <summary>
        /// מחיקת קבוצה.
        /// </summary>
        /// <param name="gid">מספר הקבוצה</param>
        public static void Delete(string gid)
        {
            Classes.SQL.Update("Groups", "`active` = " + false, "gid", gid);
        }
        /// <summary>
        /// החזרת קבוצה מחוקה.
        /// </summary>
        /// <param name="gid">מספר הקבוצה</param>
        public static void Revive(string gid)
        {
            Classes.SQL.Update("Groups", "`active` = " + true, "gid", gid);
        }
        /// <summary>
        /// בדיקת מספר קבוצות הלימוד הקיימות.
        /// </summary>
        /// <returns>מספר הקבוצות</returns>
        public static int Count()
        {
            return Classes.SQL.Select("Groups").Rows.Count;
        }
        /// <summary>
        /// קבלת רשימת הקבוצות.
        /// </summary>
        /// <returns>הרשימה</returns>
        public static DataTable List()
        {
            return Classes.SQL.Select("Groups", "active", true);
        }
        /// <summary>
        /// בדיקת האם קבוצת לימוד קיימת.
        /// </summary>
        /// <param name="gid">מספר הקבוצה</param>
        /// <returns>אמת או שקר אם הקבוצה קיימת</returns>
        public static bool Exists(int gid)
        {
            return Classes.SQL.Select("Groups", "gid", gid.ToString()).Rows.Count > 0;
        }
        /// <summary>
        /// מציאת כל התלמידים מקבוצה מסויימת.
        /// </summary>
        /// <param name="gid">מספר הקבוצה</param>
        /// <returns>רשימת התלמידים</returns>
        public static DataTable GetStudents(int gid)
        {
            return Classes.SQL.Select("GroupMembers", "gid", gid.ToString());
        }
        /// <summary>
        /// בדיקת המקצוע של קבוצה.
        /// </summary>
        /// <param name="gid">מספר הקבוצה</param>
        /// <returns>מספר המקצוע</returns>
        public static int GetSubject(int gid)
        {
            return Convert.ToInt32(Classes.SQL.Select("Groups", "gid", gid.ToString()).Rows[0]["sid"]);
        }
        /// <summary>
        /// מציאת שם הקבוצה.
        /// </summary>
        /// <param name="gid">מספר הקבוצה</param>
        /// <returns>השם של הקבוצה</returns>
        public static string GetName(int gid)
        {
            return Classes.SQL.Select("Groups", "gid", gid.ToString()).Rows[0]["gname"].ToString();
        }
        /// <summary>
        /// מציאת מידע מלא על קבוצת לימוד.
        /// </summary>
        /// <param name="gid">מספר קבוצה</param>
        /// <returns>המידע</returns>
        public static DataTable GetInfo(int gid)
        {
            return Classes.SQL.Select("Groups", "gid", gid.ToString());
        }
        /// <summary>
        /// הוספת תלמיד לקבוצת לימוד.
        /// </summary>
        /// <param name="gid">מספר קבוצה</param>
        /// <param name="id">ת.ז של התלמיד</param>
        public static void AddStudent(string gid, string id)
        {
            Classes.SQL.Insert("GroupMembers", "id,gid", string.Format("'{0}','{1}'", id.ToString(), gid.ToString()));
        }
        /// <summary>
        /// הסרת תלמיד מקבוצת לימוד.
        /// </summary>
        /// <param name="gid">מספר קבוצה</param>
        /// <param name="id">ת.ז של התלמיד</param>
        public static void DeleteStudent(string gid, string id)
        {
            Classes.SQL.Query("DELETE * FROM `GroupMembers` WHERE `gid` = '" + gid + "' AND `id` = '" + id + "'");
        }
        /// <summary>
        /// הסרת כל התלמידים מקבוצת לימוד.
        /// </summary>
        /// <param name="gid">מספר הקבוצה</param>
        public static void DeleteAllStudents(string gid)
        {
            Classes.SQL.Delete("GroupMembers", "gid", gid);
        }
    }
}