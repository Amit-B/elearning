using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Shop
{
    public partial class Form3 : Form
    {
        DataTable prod;
        DataTable comp;
        DataRow row;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Class1.GetDataSet("Select * from Products");
            prod = Class1.ds.Tables[0];
            dataGridView1.DataSource = prod;

            Class1.GetDataSet("Select * from Company");
            comp = Class1.ds.Tables[0];

            comboBox1.DataSource = prod;
            comboBox1.ValueMember = "nameprod";
            comboBox2.DataSource = comp;
            comboBox2.ValueMember = "namec";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";

            if (prod.Rows.Count == 0)
                textBox1.Text = "10001";
            else
            {
                int x = Convert.ToInt16(prod.Rows[prod.Rows.Count - 1][0]) + 1;
                textBox1.Text = Convert.ToString(x);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string strSql = "SELECT IdC from Company WHERE NameC='" + comboBox2.Text + "'";
            Class1.GetDataSet(strSql);
            row = Class1.ds.Tables[0].Rows[0];
            string code = row["IdC"].ToString();

            strSql = "INSERT INTO Products(idprod,nameprod,idsap,price,kamut,kamutmin,act) VALUES ('" + textBox1.Text + "','" + comboBox1.Text + "','" + code + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + Convert.ToInt16(checkBox1.Checked) + "')";
            Class1.GetDataSet(strSql);
            MessageBox.Show(strSql);
            MessageBox.Show("נתונים נשמרו");
            Form3_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string strSql = "SELECT IdC from Company WHERE NameC='" + comboBox2.Text + "'";
            Class1.GetDataSet(strSql);
            row = Class1.ds.Tables[0].Rows[0];
            string code = row["IdC"].ToString();

            strSql = "UPDATE Products SET idprod='" + textBox1.Text + "',idsap='" + code + "',nameprod='" + comboBox1.Text + "',price='" + textBox2.Text + "',kamut='" + textBox3.Text + "',kamutmin='" + textBox4.Text + "',act='" + Convert.ToInt16(checkBox1.Checked) + "' WHERE idprod='" + textBox1.Text + "' ";
            Class1.GetDataSet(strSql);
            MessageBox.Show("נתונים עודכנו");
            Form3_Load(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3_Load(sender, e);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                int i = comboBox1.SelectedIndex;
                textBox1.Text = prod.Rows[i]["idprod"].ToString();
                comboBox1.Text = prod.Rows[i]["nameprod"].ToString();
                textBox2.Text = prod.Rows[i]["price"].ToString();
                textBox3.Text = prod.Rows[i]["kamut"].ToString();
                textBox4.Text = prod.Rows[i]["kamutmin"].ToString();

                string strSql = "SELECT NameC from Company WHERE IdC='" + prod.Rows[i]["idsap"] + "'";
                Class1.GetDataSet(strSql);
                row = Class1.ds.Tables[0].Rows[0];
                string nameC = row["NameC"].ToString();
                comboBox2.Text = nameC;

                checkBox1.Checked = Convert.ToBoolean(prod.Rows[i]["act"]);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            double a;
            if (textBox2.Text != "")
            {

                if (!double.TryParse(textBox2.Text, out a))
                {
                    MessageBox.Show("ERROR");
                    textBox2.Text = "";
                }
            }
        }
    }
}