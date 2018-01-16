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
    public partial class AddSomeStudents : Form
    {
        public AddSomeStudents()
        {
            InitializeComponent();
        }
        private string clco = string.Empty;
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
                MessageBox.Show("לא רשמת טקסט, שום תלמיד לא נוסף.", "הוספת כמה תלמידים", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (clco == string.Empty)
                Classes.App.Error("עליך לבחור כיתה שאליה יתווספו כל התלמידים שתוסיף.");
            else
            {
                progressBar1.Minimum = 0;
                progressBar1.Maximum = textBox1.Lines.Length;
                progressBar1.Visible = true;
                int ln = 0;
                string[] l = new string[5];
                //try
                //{
                    for (int i = 0; i < textBox1.Lines.Length; i++)
                    {
                        ln = i + 1;
                        progressBar1.Value++;
                        l = textBox1.Lines[i].Split(',');
                        Classes.Student.Add(l[0], l[1], l[2], clco, l[3] == "נקבה" ? Classes.App.Gender.Female : Classes.App.Gender.Male, l[4]);
                    }
                    progressBar1.Visible = false;
                    MessageBox.Show("!נוספו בהצלחה " + ln + " תלמידים", "הוספת כמה תלמידים", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //catch
                //{
                    //Classes.App.Error("רישום התלמידים אינו תקין ולכן התוכנה הפסיקה לפעול מהשורה שלא תקינה (שורה " + ln + ").", true);
                //}
            }
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0 && MessageBox.Show("האם אתה בטוח שאתה רוצה לאפס את רשימת כל התלמידים שרשמת עד כה?", "איפוס", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                textBox1.ResetText();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            SelectClass sc = new SelectClass(true);
            sc.ShowDialog();
            if (sc.clco.Length > 0)
            {
                this.clco = sc.clco;
                textBox3.Text = Classes.Class.ClassNameByCode(sc.clco);
            }
        }
    }
}
