using System;
using System.Collections.Generic;
using System.Text;
// �� ������
using System.Data;
using System.Data.OleDb;


// �� �����
namespace Shop
{
    class Class1
    {
        public static DataSet ds; //������ ������� ������ 
        public static OleDbConnection objConn; // Connection ������
        public static OleDbDataAdapter da;

        public static void GetDataSet(string sqlStr)
        {
            // ���� �� ������ ������
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Shop.mdb";
            // ����� ����� ����� �������
            objConn = new OleDbConnection(strConn);
            // DataSet ����� ������ ���� 
            ds = new DataSet();
            // ����� �� �������
            da = new OleDbDataAdapter(sqlStr, strConn);
            // DataSet ���� �� ����� ������� ���� 
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
