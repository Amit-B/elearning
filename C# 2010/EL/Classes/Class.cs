using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace EL.Classes
{
    /// <summary>
    /// מחלקה המכילה פעולות הקשורות בכיתות.
    /// </summary>
    class Class
    {
        public const int MAX_EMBED_CLASSES = 6;
        /// <summary>
        /// מציאת קוד כיתה.
        /// </summary>
        /// <param name="stratum">שכבה</param>
        /// <param name="classnum">מספר כיתה</param>
        /// <returns>הקוד</returns>
        public static string GetClassCode(int stratum, int classnum)
        {
            string ret = string.Empty;
            if (stratum <= 9) ret += "0";
            ret += stratum;
            if (classnum <= 9) ret += "0";
            ret += classnum;
            return ret;
        }
        /// <summary>
        /// בדיקת מספר כיתה לפי קוד כיתה.
        /// </summary>
        /// <param name="clco">קוד</param>
        /// <returns>מספר הכיתה</returns>
        public static int GetClCoClassNum(string clco)
        {
            return (Convert.ToInt32(clco[2] == '0' ? clco[3].ToString() : (clco[2].ToString() + clco[3].ToString()))) + 1;
        }
        /// <summary>
        /// בדיקת מספר שכבה לפי קוד כיתה.
        /// </summary>
        /// <param name="clco">קוד</param>
        /// <returns>מספר השכבה</returns>
        public static int GetClCoStartumNum(string clco)
        {
            return Convert.ToInt32(clco[0] == '0' ? clco[1].ToString() : (clco[0].ToString() + clco[1].ToString()));
        }
        /// <summary>
        /// בדיקת אות מציינת של שכבה לפי מספר שכבה.
        /// </summary>
        /// <param name="startum">מספר שכבה</param>
        /// <returns>אות מציינת</returns>
        public static string GetStartumChar(int startum)
        {
            return startum >= 0 && startum <= 9 ? ((char)(((int)'א') + startum)).ToString() : ("י" + ((char)((int)'א' + (startum - 10)))).ToString();
        }
        /// <summary>
        /// בדיקת מספר שכבה לפי אות מציינת.
        /// </summary>
        /// <param name="startum">אות מציינת</param>
        /// <returns>מספר שכבה</returns>
        public static int GetStartumNum(string startum)
        {
            return (int)(startum.Length == 1 ? (startum[0] - 'א') : (startum[0] + (startum[1] + 1 - 'א')));
        }
        /// <summary>
        /// קבלת שם כיתה לפי קוד כיתה.
        /// </summary>
        /// <param name="classcode">הקוד</param>
        /// <returns>שם הכיתה</returns>
        public static string ClassNameByCode(string classcode)
        {
            return Classes.Class.GetStartumChar(Classes.Class.GetClCoStartumNum(classcode)) + " " + Classes.Class.GetClCoClassNum(classcode);
        }
        /// <summary>
        /// בדיקת מספר הכיתות הקיימות.
        /// </summary>
        /// <returns>מספר הכיתות</returns>
        public static int Count()
        {
            return Classes.SQL.Select("Classes").Rows.Count;
        }
        /// <summary>
        /// הוספת כיתה חדשה.
        /// </summary>
        /// <param name="stratum">שכבה</param>
        /// <param name="classnum">מספר כיתה</param>
        /// <param name="teacher">מורה בלעדי לכיתה</param>
        public static void Add(int stratum, int classnum, string teacher)
        {
            Classes.SQL.Insert("Classes", "`classid`,`class`,`number`,`tid`,`active`", string.Format("'{0}','{1}','{2}','{3}',{4}", GetClassCode(stratum, classnum), stratum, classnum, teacher, true));
        }
        /// <summary>
        /// קבלת רשימת הכיתות.
        /// </summary>
        /// <param name="inactives">חיפוש גם בלא פעילים</param>
        /// <param name="startum">שכבה</param>
        /// <returns>הרשימה</returns>
        public static DataTable List(bool inactives = false, string startum = "")
        {
            string str = "SELECT * FROM `Classes`";
            if (!inactives) str += " WHERE `active` = " + true;
            if (startum.Length > 0) str += (str[0] == '`' ? " WHERE " : " AND ") + "`class` = '" + startum + "'";
            return startum.Length > 0 ? Classes.SQL.Select("Classes", "class", startum) : Classes.SQL.Select("Classes");
        }
        /// <summary>
        /// בדיקת האם קוד כיתה קיים.
        /// </summary>
        /// <param name="classid">הקוד</param>
        /// <returns>אמת או שקר אם הקוד קיים</returns>
        public static bool Exists(string classid)
        {
            return Classes.SQL.Select("Classes", "classid", classid).Rows.Count > 0;
        }
        /// <summary>
        /// מציאת כל התלמידים מכיתה מסויימת.
        /// </summary>
        /// <param name="classid">קוד כיתה</param>
        /// <returns>רשימת התלמידים</returns>
        public static DataTable GetStudents(string classid)
        {
            return Classes.SQL.Select("Students", "classid", classid);
        }
        /// <summary>
        /// מציאת רשימה של כל השכבות הקיימות.
        /// </summary>
        /// <returns>רשימת השכבות</returns>
        public static List<string> GetStartums()
        {
            List<string> list = new List<string>();
            DataTable classes = List();
            for (int i = 0; i < classes.Rows.Count; i++)
                if (!list.Contains(GetStartumChar(Convert.ToInt32(classes.Rows[i]["class"]))))
                    list.Add(GetStartumChar(Convert.ToInt32(classes.Rows[i]["class"])));
            return list;
        }
        /// <summary>
        /// מציאת מידע על כיתה.
        /// </summary>
        /// <param name="classc">קוד כיתה</param>
        /// <param name="key">המידע להשגה</param>
        /// <returns>המידע</returns>
        public static string GetInfo(string classc, string key)
        {
            return Classes.SQL.Select("Classes", key, "classid", classc).ToString();
        }
    }
}