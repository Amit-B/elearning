using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace EL.Classes
{
    /// <summary>
    /// מחלקה הקשורה בפעולות על הערות משמעת.
    /// </summary>
    class Note
    {
        /// <summary>
        /// הוספת סוג הערה.
        /// </summary>
        /// <param name="by">מורה שהוסיף</param>
        /// <param name="type">שם הסוג</param>
        /// <returns>מספר סידורי</returns>
        public static int AddNoteType(string by, string type, bool absence)
        {
            int id = App.GetNextID("NoteTypes","ntid");
            Classes.SQL.Insert("NoteTypes", "ntid,addedby,ntitle,absence", string.Format("'{0}','{1}','{2}','{3}'", id.ToString(), by.ToString(), type, Convert.ToInt32(absence)));
            return id;
        }
        /// <summary>
        /// מחיקת סוג הערה.
        /// </summary>
        /// <param name="ntid">מספר סידורי של הסוג</param>
        public static void DeleteNoteType(int ntid)
        {
            Classes.SQL.Delete("NoteTypes", "ntid", ntid.ToString());
        }
        /// <summary>
        /// עריכת שם סוג הערה.
        /// </summary>
        /// <param name="ntid">מספר הסוג</param>
        /// <param name="newname">השם החדש</param>
        public static void RenameNoteType(int ntid, string newname)
        {
            Classes.SQL.Update("NoteTypes", "`ntitle` = '" + newname + "'", "ntid", ntid.ToString());
        }
        /// <summary>
        /// קבלת רשימת סוגי ההערות.
        /// </summary>
        /// <returns>הרשימה</returns>
        public static DataTable ListNoteTypes()
        {
            return Classes.SQL.Select("NoteTypes");
        }
        /// <summary>
        /// מציאת מידע עבור סוג הערה.
        /// </summary>
        /// <param name="ntid">מספר הסוג</param>
        /// <returns>המידע</returns>
        public static DataRow GetNoteTypeInfo(int ntid)
        {
            return Classes.SQL.Select("NoteTypes", "ntid", ntid.ToString()).Rows[0];
        }
        /// <summary>
        /// בדיקה האם סוג הערה קיים.
        /// </summary>
        /// <param name="ntid">מספר הסור</param>
        /// <returns>אמת או שקר אם הסוג קיים</returns>
        public static bool NoteTypeExists(int ntid)
        {
            return Classes.SQL.Select("NoteTypes", "ntid", ntid.ToString()).Rows.Count > 0;
        }
        /// <summary>
        /// מציאת כל הערות המשמעת בסינון לפי תנאים.
        /// </summary>
        /// <param name="id">סינון לפי תלמיד</param>
        /// <param name="ntid">סינון לפי סוג הערה</param>
        /// <param name="lid">סינון לפי מספר שיעור</param>
        /// <param name="tid">סינון לפי מורה</param>
        /// <returns>הרשימה המסוננת</returns>
        public static DataTable GetNotesBy(string id = "", string ntid = "", string lid = "", string tid = "")
        {
            string query = "SELECT * FROM `Notes`";
            //if(WHERE `id` = '" + id + "'";
            if (id.Length > 0) query += " " + (query.EndsWith("`Notes`") ? "WHERE" : "AND") + " `id` = '" + id + "'";
            if (ntid.Length > 0) query += " " + (query.EndsWith("`Notes`") ? "WHERE" : "AND") + " `ntid` = '" + ntid + "'";
            if (tid.Length > 0) query += " " + (query.EndsWith("`Notes`") ? "WHERE" : "AND") + " `tid` = '" + tid + "'";
            if (lid.Length > 0) query += " " + (query.EndsWith("`Notes`") ? "WHERE" : "AND") + " `lid` = '" + lid + "'";
            Classes.SQL.Query(query);
            return Classes.SQL.ds.Tables[0];
        }
        /// <summary>
        /// מציאת כל הערות המשמעת בבית הספר.
        /// </summary>
        /// <returns>הרשימה</returns>
        public static DataTable GetNotes()
        {
            return Classes.SQL.Select("Notes");
        }
        /// <summary>
        /// הוספת הערת משמעת חדשה.
        /// </summary>
        /// <param name="lid">מספר השיעור</param>
        /// <param name="id">ת.ז של תלמיד</param>
        /// <param name="ntid">סוג הערה</param>
        /// <param name="tid">ת.ז של מורה</param>
        /// <param name="justified">הצדקה</param>
        /// <param name="explain">פרטים</param>
        /// <returns>מספר ההערה</returns>
        public static int Add(int lid, string id, int ntid, string tid, bool justified, string explain, DateTime dt)
        {
            int nid = App.GetNextID("Notes", "nid");
            Classes.SQL.Insert("Notes", "nid,lid,id,ntid,tid,justified,explain,ndatetime", string.Format("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}'",
                nid, lid, id, ntid, tid, Convert.ToInt32(justified), explain, dt.ToString()));
            return nid;
        }
        /// <summary>
        /// מחיקת הערת משמעת.
        /// </summary>
        /// <param name="nid">מספר ההערה</param>
        public static void Delete(int nid)
        {
            Classes.SQL.Delete("Notes", "nid", nid.ToString());
        }
    }
}