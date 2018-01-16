using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
namespace sqlclass
{
    public class Class1
    {
        public DataSet ds;
        public OleDbConnection objConn;
        public OleDbDataAdapter da;
        private void GetDataSet(string sqlStr)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=project.mdb";
            objConn = new OleDbConnection(strConn);
            ds = new DataSet();
            da = new OleDbDataAdapter(sqlStr, strConn);
            try
            {
                da.Fill(ds);
            }
            catch// (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        public void Insert(string table, string rows, string valuecode)
        {
            GetDataSet("INSERT INTO " + table + "(" + rows + ") VALUES (" + valuecode + ")");
        }
        public void Update(string table, string updatecode, string updateby)
        {
            GetDataSet("UPDATE " + table + " SET " + updatecode + " WHERE " + updateby);
        }
        public DataTable Select(string table)
        {
            GetDataSet("SELECT * from " + table);
            return ds.Tables[0];
        }
        public string Select(string table, int row, string col, string selectby)
        {
            GetDataSet("SELECT " + col + " from " + table + " WHERE " + selectby);
            return ds.Tables[0].Rows[row][col].ToString();
        }
        public void Query(string q)
        {
            GetDataSet(q);
        }
    }
}