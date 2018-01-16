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
    public partial class EditClassStudents : Form
    {
        private string clco = string.Empty;
        public EditClassStudents(string clco)
        {
            InitializeComponent();
            this.clco = clco;
            this.Text += Classes.Class.ClassNameByCode(clco);
        }
        private void EditClassStudents_Load(object sender, EventArgs e)
        {
            DataTable students = Classes.Class.GetStudents(clco);
            for (int i = 0; i < students.Rows.Count; i++)
                listBox1.Items.Add(students.Rows[i]["id"] + ": " + students.Rows[i]["firstname"] + " " + students.Rows[i]["lastname"]);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            StudentsSelect ss = new StudentsSelect("עריכת תלמידים בכיתה", 1);
            ss.ShowDialog();
            if (ss.returnedStudents != null)
                if (MessageBox.Show("האם אתה בטוח שברצונך להוסיף את " + ss.returnedStudents.Count + " התלמידים שנבחרו לכיתה זו?", "עריכת תלמידי כיתה", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    for (int i = 0; i < ss.returnedStudents.Count; i++)
                        if (!listBox1.Items.Contains(ss.returnedStudents[i]))
                        {
                            listBox1.Items.Add(ss.returnedStudents[i]);
                            Classes.SQL.Update("Students", "`classid` = '" + clco + "'", "id", ss.returnedStudents[i].Split(':')[0]);
                        }
            ss.Dispose();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                SelectClass sc = new SelectClass(true);
                sc.ShowDialog();
                if (sc.clco.Length > 0)
                {
                    if (sc.clco == clco)
                        Classes.App.Error("עליך לבחור תיקיה שונה מזו שבעריכה כרגע.");
                    else
                    {
                        Classes.SQL.Update("Students", "`classid` = '" + sc.clco + "'", "id", listBox1.Items[listBox1.SelectedIndex].ToString().Split(':')[0]);
                        listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                    }
                }
                sc.Dispose();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (this.Text[this.Text.Length - 1] != '*') this.Text += "*";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}