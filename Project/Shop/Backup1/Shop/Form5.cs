using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Shop
{
    public partial class Form5 : Form
    {
        DataTable company;
        DataTable orders;
        DataTable products;
        DataTable prodorder;
        DataRow row;
        string strSql;

        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            listBox3.Items.Clear(); listBox4.Items.Clear();
            listBox5.Items.Clear(); listBox6.Items.Clear(); listBox7.Items.Clear();
            Class1.GetDataSet("Select * from Orders");
            orders = Class1.ds.Tables[0];
            if (orders.Rows.Count == 0) textBox1.Text = "1001";
            else
            {
                int x = Convert.ToInt16(orders.Rows[orders.Rows.Count - 1][0]) + 1;
                textBox1.Text = Convert.ToString(x);
            }

            Class1.GetDataSet("Select * from Company");
            company = Class1.ds.Tables[0];
            comboBox1.DataSource = company;
            comboBox1.ValueMember = "namec";

            comboBox1.Text = ""; textBox4.Text = ""; textBox5.Text = "";
            textBox2.Text = (System.DateTime.Today.ToString("d"));// Now.Date.ToString());
            dateTimePicker1.Value = System.DateTime.Today.AddDays(7);
            


            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Class1.GetDataSet("Select idprod,nameprod from Company,Products Where namec='"+comboBox1.Text+"' AND IdC=idsap");
            listBox1.DataSource = Class1.ds.Tables[0];
            listBox1.ValueMember = "nameprod";
            listBox2.DataSource = Class1.ds.Tables[0];
            listBox2.ValueMember = "idprod";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox4.Items.Contains(listBox1.Text) == false)
            {
                listBox3.Items.Add(listBox2.SelectedValue.ToString());
                listBox4.Items.Add(listBox1.SelectedValue.ToString());
                listBox6.Items.Add(textBox4.Text);
                Class1.GetDataSet("Select price from Products Where idprod='" + listBox2.SelectedValue.ToString() + "'");
                row = Class1.ds.Tables[0].Rows[0];
                listBox5.Items.Add(row["price"].ToString());
                listBox7.Items.Add(Convert.ToString(Convert.ToDouble(row["price"]) * Convert.ToInt16(textBox4.Text)));


                double sum = 0;
                for (int i = 0; i < listBox7.Items.Count; i++)
                    sum = sum + Convert.ToDouble(listBox7.Items[i]);
                textBox5.Text = sum.ToString();
            }
            else
            {
                MessageBox.Show("פריט נמצא");
              
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string strSql = "Select IdC from Company Where NameC='"+comboBox1.Text+"'";
            Class1.GetDataSet(strSql);
            row = Class1.ds.Tables[0].Rows[0];
            string code = row["IdC"].ToString();
            
            strSql = "INSERT INTO Orders(IdO,IdC,DateO,DateO1) VALUES ('" + textBox1.Text + "','" + code + "','" + textBox2.Text + "','" + dateTimePicker1.Value + "')";
            Class1.GetDataSet(strSql);
           
            
            for (int i = 0; i < listBox3.Items.Count; i++)
            {
                strSql = "INSERT INTO ProdOrder(IdO,IdP,amount) VALUES ('" + textBox1.Text + "','" + listBox3.Items[i].ToString() + "','" + listBox6.Items[i].ToString() + "')";
                Class1.GetDataSet(strSql);
            } 
            MessageBox.Show("נתונים נשמרו");
            Form5_Load(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear(); listBox4.Items.Clear(); listBox5.Items.Clear(); listBox6.Items.Clear(); listBox7.Items.Clear();
            string strSql = "Select * From Orders Where ido='"+textBox6.Text+"'";
            Class1.GetDataSet(strSql);
            orders = Class1.ds.Tables[0];
            if (orders.Rows.Count == 0)
            {
                MessageBox.Show("הזמנה לא נמצאת");
                textBox6.Text = "";
            }
            else
            {
                textBox1.Text = orders.Rows[0]["ido"].ToString();
                textBox2.Text = Convert.ToDateTime(orders.Rows[0]["dateo"]).ToString("d");
              //  if(orders.Rows[0]["dateo2"]!=null)
                textBox3.Text = orders.Rows[0]["dateo2"].ToString();
                dateTimePicker1.Value=Convert.ToDateTime(orders.Rows[0]["dateo1"]);

                strSql = "Select * from Company Where idc='" + orders.Rows[0]["idc"] + "'";
                Class1.GetDataSet(strSql);
                company = Class1.ds.Tables[0];
                comboBox1.Text = company.Rows[0]["NameC"].ToString();

                strSql = "Select * from ProdOrder Where ido='"+textBox6.Text+"'";
                Class1.GetDataSet(strSql);
                prodorder = Class1.ds.Tables[0];
                for (int i = 0; i < prodorder.Rows.Count; i++)
                {
                    listBox3.Items.Add(prodorder.Rows[i]["idp"]);
                    listBox6.Items.Add(prodorder.Rows[i]["amount"]);
                   
                    strSql = "Select * from Products Where idprod='"+prodorder.Rows[i]["idp"]+"'";
                    Class1.GetDataSet(strSql);
                    products = Class1.ds.Tables[0];
                    listBox4.Items.Add(products.Rows[0]["nameprod"]);
                    listBox5.Items.Add(products.Rows[0]["price"]);
                    listBox7.Items.Add(Convert.ToDouble(products.Rows[0]["price"]) *Convert.ToInt16(prodorder.Rows[i]["amount"]));                   
                }
                double sum = 0;
                for (int i = 0; i < listBox7.Items.Count; i++)
                    sum = sum + Convert.ToDouble(listBox7.Items[i]);
                textBox5.Text = sum.ToString();


                strSql = "Select * from ProdOrder Where ido='" + textBox6.Text + "'";
                Class1.GetDataSet(strSql);
                CrystalReport1 mr = new CrystalReport1();
              DataSet ds = Class1.ds;
                oleDbDataAdapter1.SelectCommand.Parameters["@par"].Value = textBox6.Text;
                oleDbDataAdapter1.Fill(ds,"prodorder");
                mr.SetDataSource(ds);
                crystalReportViewer1.ReportSource = new CrystalReport1();
//                Form6 f = new Form6();
//                f.ShowDialog();
            }
        }

        private void oleDbConnection1_InfoMessage(object sender, System.Data.OleDb.OleDbInfoMessageEventArgs e)
        {

        }
    }
}