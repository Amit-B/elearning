using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace EL.Classes
{
    /// <summary>
    /// מחלקה העוסקת ברשימת המקצועות בבית הספר.
    /// </summary>
    class Subject
    {
        /// <summary>
        /// הוספת מקצוע.
        /// </summary>
        /// <param name="name">שם המקצוע</param>
        /// <returns>מספר סידורי של המקצוע</returns>
        public static int Add(string name)
        {
            int id = Classes.App.GetNextID("Subjects", "sid");
            Classes.SQL.Insert("Subjects", "sid,stitle,active", string.Format("'{0}','{1}',{2}", id.ToString(), name, true));
            return id;
        }
        /// <summary>
        /// מחיקת מקצוע.
        /// </summary>
        /// <param name="sid">מספר המקצוע</param>
        public static void Remove(int sid)
        {
            Classes.SQL.Update("Subjects", "`active` = " + false, "sid", sid.ToString());
        }
        /// <summary>
        /// שינוי שם מקצוע.
        /// </summary>
        /// <param name="sid">מספר המקצוע</param>
        /// <param name="newname">השם החדש</param>
        public static void Rename(int sid, string newname)
        {
            Classes.SQL.Update("Subjects", "`stitle` = '" + newname + "'", "sid", sid.ToString());
        }
        /// <summary>
        /// בדיקת שם המקצוע.
        /// </summary>
        /// <param name="sid">מספר המקצוע</param>
        /// <returns>שם המקצוע</returns>
        public static string GetName(int sid)
        {
            return Classes.SQL.Select("Subjects", "stitle", "sid", sid.ToString());
        }
        /// <summary>
        /// קבלת רשימת המקצועות הקיימים.
        /// </summary>
        /// <returns>הרשימה</returns>
        public static DataTable List()
        {
            return Classes.SQL.Select("Subjects", "active", true);
        }
    }
}