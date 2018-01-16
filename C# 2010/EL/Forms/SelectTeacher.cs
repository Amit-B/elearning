using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace EL.Forms
{
    public partial class SelectTeacher : Form
    {
        public SelectTeacher()
        {
            InitializeComponent();
        }
        private List<string> teachers = new List<string>();
        public string returned = string.Empty;
        private void SelectTeacher_Load(object sender, EventArgs e)
        {
            DataTable list = Classes.Teacher.List();
            for (int i = 0; i < list.Rows.Count; i++)
            {
                comboBox1.Items.Add(list.Rows[i]["tid"].ToString() + " - " + list.Rows[i]["firstname"].ToString() + " " + list.Rows[i]["lastname"].ToString());
                teachers.Add(list.Rows[i]["tid"].ToString());
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
                returned = teachers[comboBox1.SelectedIndex];
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}