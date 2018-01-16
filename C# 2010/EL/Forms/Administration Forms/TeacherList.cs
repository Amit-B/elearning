using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace EL.Forms.Administration_Forms
{
    public partial class TeacherList : UserControl
    {
        public TeacherList()
        {
            InitializeComponent();
        }
        List<string> teachers = new List<string>();
        private void TeacherList_Load(object sender, EventArgs e)
        {
            DataTable ts = Classes.Teacher.List();
            for (int i = 0; i < ts.Rows.Count; i++)
            {
                listBox1.Items.Add(ts.Rows[i]["tid"] + " - " + ts.Rows[i]["firstname"] + " " + ts.Rows[i]["lastname"]);
                teachers.Add(ts.Rows[i]["tid"].ToString());
            }
            label1.Text = "סך הכל " + ts.Rows.Count + " מורים.";
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRow row = Classes.SQL.Select("Teachers", "tid", teachers[listBox1.SelectedIndex]).Rows[0];
            string pic = AppDirectory.GetDirectory(AppDirectory.PROFILEPICS) + "/" + row["tid"].ToString() + ".png";
            pictureBox2.Image = File.Exists(pic) ? Image.FromFile(pic) : Images.NoImage;
            label2.Text = row["firstname"].ToString() + " " + row["lastname"].ToString();
            textBox1.Text = row["tid"].ToString();
            textBox2.Text = Classes.App.GenderName(Convert.ToInt32(row["gender"]));
            textBox3.Text = Classes.Duty.GetName(row["dutyid"].ToString());
            textBox4.Text = Convert.ToBoolean(row["admin"]) ? "כולל גישת ניהול ראשי" : "ללא";
            textBox6.Text = row["phone"].ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                Classes.App.Error("נא לבחור מורה.");
            else
            {
                Classes.Teacher.Kill(teachers[listBox1.SelectedIndex]);
                MessageBox.Show("המורה נמחק בהצלחה.", "מחיקת מורה", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}