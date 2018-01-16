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
    public partial class TMP_Install_3 : Form
    {
        private Main m;
        public TMP_Install_3(Main mainForm, TMP_Install_2 prev)
        {
            InitializeComponent();
            prev.allowClose = true;
            prev.Close();
            m = mainForm;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            label9.Visible = true;
            label9.Refresh();
            Classes.Duty.Add("None");
            Classes.Teacher.Add(textBox3.Text, textBox1.Text, textBox2.Text, radioButton1.Checked ? Classes.App.Gender.Male : Classes.App.Gender.Female, "?", textBox4.Text, true, 0);
            for(int i = 0; i < 6; i++)
                for (int j = 0; j < 3; j++)
                    Classes.Class.Add(i + 6, j, textBox3.Text);
            List<int> subj = new List<int>(), subj2 = new List<int>();
            subj.Add(Classes.Subject.Add("מתמטיקה"));
            subj.Add(Classes.Subject.Add("אנגלית"));
            subj2.Add(Classes.Subject.Add("לשון"));
            subj2.Add(Classes.Subject.Add("הסטוריה"));
            subj2.Add(Classes.Subject.Add("ספורט"));
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < subj2.Count; j++)
                    Classes.Group.Add(subj2[j].ToString(), (i + 6).ToString(), textBox3.Text, Classes.Subject.GetName(subj2[j]) + " " + Classes.Class.GetStartumChar(i + 6));
                for (int j = 0; j < subj.Count; j++)
                {
                    if (i >= 3)
                        for (int a = 3; a <= 5; a++)
                            Classes.Group.Add(subj[j].ToString(), (i + 6).ToString(), textBox3.Text, Classes.Subject.GetName(subj[j]) + " " + Classes.Class.GetStartumChar(i + 6) + (i >= 3 ? (" " + a + " יחל") : ""));
                    else
                        Classes.Group.Add(subj[j].ToString(), (i + 6).ToString(), textBox3.Text, Classes.Subject.GetName(subj[j]) + " " + Classes.Class.GetStartumChar(i + 6));
                }
            }
            Classes.Grades.AddGradeType("ללא");
            Classes.Grades.AddGradeType("מבחן");
            Classes.Grades.AddGradeType("בוחן");
            Classes.Grades.AddGradeType("הערכה");
            Classes.Duty.Add("מנהל/ת");
            Classes.Duty.Add("סגנ/ית מנהל/ת");
            Classes.Duty.Add("מורה מקצועי");
            Classes.Duty.Add("מחנכ/ת");
            Classes.Duty.Add("מורה מחליפ/ה");
            Classes.Note.AddNoteType(textBox3.Text, "חיסור", true);
            Classes.Note.AddNoteType(textBox3.Text, "איחור", false);
            Classes.Note.AddNoteType(textBox3.Text, "חוסר ציוד", false);
            Classes.Note.AddNoteType(textBox3.Text, "הפרעה", false);
            MessageBox.Show("ההתקנה הסתיימה בהצלחה!", "התקנה", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
            m.Show();
            m.Visible = true;
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            label7.Visible = false;
        }
    }
}