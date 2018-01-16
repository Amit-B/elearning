using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Shop
{
    public partial class Form4 : Form
    {
        DataTable prod;
        DataTable comp;
        DataRow row;

        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Class1.GetDataSet("Select * from Products");
            prod = Class1.ds.Tables[0];
           
            Class1.GetDataSet("Select * from Company");
            comp = Class1.ds.Tables[0];

            comboBox1.DataSource = comp;
            comboBox1.ValueMember = "namec";
            
          
            comboBox1.Text = "";
            radioButton1.Select();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                string strSql = "SELECT idprod,nameprod,price,kamut,kamutmin,products.act from Company,Products WHERE (Products.IdSap=Company.idC)and(namec='" + comboBox1.Text + "')";
                Class1.GetDataSet(strSql);
                dataGridView1.DataSource = Class1.ds.Tables[0];
                listBox1.DataSource = Class1.ds.Tables[0];
                listBox1.ValueMember = "nameprod";
            }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                string strSql = "SELECT idprod,nameprod,price,kamut,kamutmin,products.act from Company,Products WHERE (Products.IdSap=Company.idC)and(namec='" + comboBox1.Text + "')";
                Class1.GetDataSet(strSql);
                dataGridView1.DataSource = Class1.ds.Tables[0];
                listBox1.DataSource = Class1.ds.Tables[0];
                listBox1.ValueMember = "nameprod";
            }
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                string strSql = "SELECT idprod,nameprod,price,kamut,kamutmin,products.act from Company,Products WHERE (Products.IdSap=Company.idC)and(namec='" + comboBox1.Text + "')and(Products.act=true)";
                Class1.GetDataSet(strSql);
                dataGridView1.DataSource = Class1.ds.Tables[0];
                listBox1.DataSource = Class1.ds.Tables[0];
                listBox1.ValueMember = "nameprod";
            }
        }

        private void radioButton3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                string strSql = "SELECT idprod,nameprod,price,kamut,kamutmin,products.act from Company,Products WHERE (Products.IdSap=Company.idC)and(namec='" + comboBox1.Text + "')and(Products.act=true)and(kamut<kamutmin)";
                Class1.GetDataSet(strSql);
                dataGridView1.DataSource = Class1.ds.Tables[0];
                listBox1.DataSource = Class1.ds.Tables[0];
                listBox1.ValueMember = "nameprod";
            }

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
          
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
             string strSql = "SELECT * from Products WHERE nameprod='" + listBox1.Text + "'";
             Class1.GetDataSet(strSql);
           //  dataGridView1.DataSource = Class1.ds.Tables[0];
             label8.Text = Class1.ds.Tables[0].Rows[0]["idprod"].ToString();
             label9.Text = Class1.ds.Tables[0].Rows[0]["nameprod"].ToString();
             label10.Text = Class1.ds.Tables[0].Rows[0]["price"].ToString();
             label11.Text = Class1.ds.Tables[0].Rows[0]["kamut"].ToString();
             label12.Text = Class1.ds.Tables[0].Rows[0]["kamutmin"].ToString();
          
        }

        
    }
}