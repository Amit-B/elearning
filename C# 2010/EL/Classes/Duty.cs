using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace EL.Classes
{
    /// <summary>
    /// מחלקה המכילה פעולות הקשורות בדרגות מורים.
    /// </summary>
    class Duty
    {
        public const int UNKNOWN_DUTY = 0;
        /// <summary>
        /// הוספת דרגת מורה.
        /// </summary>
        /// <param name="name">שם הדרגה</param>
        public static void Add(string name)
        {
            int id = Classes.App.GetNextID("Duties", "dutyid");
            Classes.SQL.Insert("Duties", "dutyid,dutyname", string.Format("'{0}','{1}'", id, name));
        }
        /// <summary>
        /// מחיקת דרגת מורה.
        /// </summary>
        /// <param name="dutyid">מספר הדרגה</param>
        public static void Delete(string dutyid)
        {
            Classes.SQL.Delete("Duties", "dutyid", dutyid);
        }
        /// <summary>
        /// מציאת רשימת כל דרגות המורים.
        /// </summary>
        /// <returns>הרשימה</returns>
        public static DataTable List()
        {
            return Classes.SQL.Select("Duties");
        }
        /// <summary>
        /// מציאת שם של דרגה.
        /// </summary>
        /// <param name="dutyid">מספר הדרגה</param>
        /// <returns>השם</returns>
        public static string GetName(string dutyid)
        {
            return dutyid == "0" ? "אין תפקיד מוגדר" : Classes.SQL.Select("Duties", "dutyid", dutyid.ToString()).Rows[0]["dutyname"].ToString();
        }
        /// <summary>
        /// שינוי שם של דרגה.
        /// </summary>
        /// <param name="dutyid">מספר הדרגה</param>
        /// <param name="newname">השם החדש</param>
        public static void SetName(string dutyid, string newname)
        {
            Classes.SQL.Update("Duties", "`dutyname` = '" + newname + "'", "dutyid", dutyid.ToString());
        }
    }
}