using System;
using System.Collections.Generic;
using System.Text;
// יש להוסיף
using System.Data;
using System.Data.OleDb;


// יש לשנות
namespace Shop
{
    class Class1
    {
        public static DataSet ds; //אוביקט לאיחזור נתונים 
        public static OleDbConnection objConn; // Connection אוביקט
        public static OleDbDataAdapter da;

        public static void GetDataSet(string sqlStr)
        {
            // בונה את מחרוזת הקישור
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Shop.mdb";
            // מאתחל חיבור לבסיס הנתונים
            objConn = new OleDbConnection(strConn);
            // DataSet מאתחל אוביקט מסוג 
            ds = new DataSet();
            // מבצעה את השאילתה
            da = new OleDbDataAdapter(sqlStr, strConn);
            // DataSet טוען את תוצאת השאילתה לתוך 
            try
            {
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);

            }




        }
    }
}
