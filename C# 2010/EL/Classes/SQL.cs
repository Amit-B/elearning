//#define DEBUGGING
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
namespace EL.Classes
{
    /// <summary>
    /// ספריית שימוש במסד נתונים.
    /// </summary>
    class SQL
    {
        public static DataSet ds = null;
        public static OleDbConnection objConn = null;
        public static OleDbDataAdapter da = null;
        private const int MICROSOFT_ACCESS_VERSION = 0; // 0 = mdb, 1 = accdb
        private const string strConn = MICROSOFT_ACCESS_VERSION == 0 ? "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DB.mdb" : "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DB.accdb";
        private const bool DEBUG = false;
        /// <summary>
        /// התחברות למסד הנתונים.
        /// </summary>
        public static void Connect()
        {
            try
            {
                objConn = new OleDbConnection(strConn);
            }
            catch (Exception ex)
            {
                Classes.App.Error(ex.Message);
            }
        }
        /// <summary>
        /// יציאה ממסד הנתונים.
        /// </summary>
        public static void Disconnect()
        {
            try
            {
                objConn.Close();
            }
            catch (Exception ex)
            {
                Classes.App.Error(ex.Message);
            }
        }
        /// <summary>
        /// שליחת שאילתה למסד הנתונים.
        /// </summary>
        /// <param name="sqlStr">סטרינג הבקשה לשליחה</param>
        public static void Query(string sqlStr)
        {
            if (objConn == null)
                Connect();
            ds = new DataSet();
            da = new OleDbDataAdapter(sqlStr, strConn);
#if DEBUGGING
            da.Fill(ds);
#else
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Classes.App.Error(ex.Message);
            }
#endif
        }
        /// <summary>
        /// Insert Command
        /// </summary>
        public static void Insert(string table, string rows, string valuecode)
        {
           Query("INSERT INTO " + table + "(" + rows + ") VALUES (" + valuecode + ")");
        }
        /// <summary>
        /// Update Command
        /// </summary>
        public static void Update(string table, string updatecode, string wherekey, string whereval)
        {
            Query("UPDATE `" + table + "` SET " + updatecode + " WHERE `" + wherekey + "` = '" + whereval + "'");
        }
        /// <summary>
        /// Update Command
        /// </summary>
        public static void Update(string table, string updatecode, string wherekey, bool whereval)
        {
            Query("UPDATE `" + table + "` SET " + updatecode + " WHERE `" + wherekey + "` = " + whereval);
        }
        /// <summary>
        /// Select Command
        /// </summary>
        public static DataTable Select(string table)
        {
            Query("SELECT * FROM `" + table + "`");
            return ds.Tables[0];
        }
        /// <summary>
        /// Select Command
        /// </summary>
        public static DataTable Select(string table, string wherekey, string whereval)
        {
            Query("SELECT * FROM `" + table + "` WHERE `" + wherekey + "` = '" + whereval + "'");
            return ds.Tables[0];
        }
        /// <summary>
        /// Select Command
        /// </summary>
        public static DataTable Select(string table, string wherekey, bool whereval)
        {
            Query("SELECT * FROM `" + table + "` WHERE `" + wherekey + "` = " + whereval);
            return ds.Tables[0];
        }
        /// <summary>
        /// Select Command
        /// </summary>
        public static string Select(string table, string col, string wherekey, string whereval)
        {
            Query("SELECT `" + col + "` FROM `" + table + "` WHERE `" + wherekey + "` = '" + whereval + "'");
            return ds.Tables[0].Rows[0][col].ToString();
        }
        /// <summary>
        /// Select Command
        /// </summary>
        public static DataTable Select(string table, string col)
        {
            Query("SELECT " + col + " FROM " + table);
            return ds.Tables[0];
        }
        /// <summary>
        /// Delete Command
        /// </summary>
        public static void Delete(string table, string wherekey, string whereval)
        {
            Query("DELETE * FROM `" + table + "` WHERE `" + wherekey + "` = '" + whereval + "'");
        }
        /// <summary>
        /// Delete Command
        /// </summary>
        public static void Delete(string table, string wherekey, bool whereval)
        {
            Query("DELETE * FROM `" + table + "` WHERE `" + wherekey + "` = " + whereval);
        }
        /// <summary>
        /// Delete Command
        /// </summary>
        public static void Delete(string table)
        {
            Query("DELETE * FROM `" + table + "`");
        }
    }
}