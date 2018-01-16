using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Shop
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Class1.GetDataSet("Select * from Company");
            dataGridView1.DataSource = Class1.ds.Tables[0];

            comboBox1.DataSource = Class1.ds.Tables[0];
            comboBox1.ValueMember = "namec";
            comboBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

            if (Class1.ds.Tables[0].Rows.Count == 0)
                textBox1.Text = "1001";
            else
            {
                int x = Convert.ToInt16(Class1.ds.Tables[0].Rows[Class1.ds.Tables[0].Rows.Count - 1][0]) + 1;
                textBox1.Text = Convert.ToString(x);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = comboBox1.SelectedIndex;
            textBox1.Text = Class1.ds.Tables[0].Rows[i]["idc"].ToString();
            textBox2.Text = Class1.ds.Tables[0].Rows[i]["address"].ToString();
            textBox3.Text = Class1.ds.Tables[0].Rows[i]["tel"].ToString();
            textBox4.Text = Class1.ds.Tables[0].Rows[i]["pel"].ToString();
            checkBox1.Checked = Convert.ToBoolean(Class1.ds.Tables[0].Rows[i]["act"]);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > '9' || e.KeyChar < '0') & (e.KeyChar != (char)8)) e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar > '9' || e.KeyChar < '0') & (e.KeyChar != (char)8)) e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool t = true;

            if (comboBox1.Text == "")
            {
                comboBox1.BackColor = Color.Yellow;
                t = false;
            }
            else comboBox1.BackColor = Color.White;
            if (textBox2.Text == "")
            {
                textBox2.BackColor = Color.Yellow;
                t = false;
            }
            else textBox2.BackColor = Color.White;
            if (textBox3.Text == "")
            {
                textBox3.BackColor = Color.Yellow;
                t = false;
            }
            else textBox3.BackColor = Color.White;
            if (textBox4.Text == "")
            {
                textBox4.BackColor = Color.Yellow;
                t = false;
            }
            else textBox4.BackColor = Color.White;

            if (t == true)
            {

                string strSql = "INSERT INTO Company(idc,namec,pel,tel,address,act) VALUES ('" + textBox1.Text + "','" + comboBox1.Text + "','" + textBox4.Text + "','" + textBox3.Text + "','" + textBox2.Text + "','" + Convert.ToInt16(checkBox1.Checked) + "')";
                Class1.GetDataSet(strSql);
                MessageBox.Show("נתונים נשמרו");
                Form2_Load(sender, e);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string strSql = "UPDATE Company SET idc='" + textBox1.Text + "',namec='" + comboBox1.Text + "',pel='" + textBox4.Text + "',tel='" + textBox3.Text + "',address='" + textBox2.Text + "',act='" + Convert.ToInt16(checkBox1.Checked) + "' WHERE namec='" + comboBox1.Text + "' ";
            Class1.GetDataSet(strSql);
            MessageBox.Show("נתונים עודכנו");
            Form2_Load(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2_Load(sender, e);
        }
    }
}