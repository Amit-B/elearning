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
    public partial class StudentCard : Form
    {
        private string id = "-1";
        private string clco = string.Empty;
        public StudentCard(string id)
        {
            InitializeComponent();
            this.id = id;
        }
        private void StudentCard_Load(object sender, EventArgs e)
        {
            this.Size = new Size(365, 413);
            if (id == "-1")
            {
                label4.Text = "הוספת תלמיד חדש";
                this.Text = "הוספת תלמיד חדש";
                button1.Text = "שמירה";
                textBox1.ResetText();
                textBox2.ResetText();
                textBox3.ResetText();
                textBox5.ResetText();
                dateTimePicker1.Visible = false;
                label5.Visible = false;
                dateTimePicker2.Visible = false;
                label6.Visible = false;
                checkBox2.Checked = true;
                label7.Visible = false;
                textBox4.Visible = false;
                SetAll(false);
            }
            else
            {
                label4.Text = "כרטיס תלמיד #" + id;
                DataRow sRow = Classes.Student.GetFullInfo(id.ToString());
                textBox1.Text = sRow["firstname"].ToString();
                textBox2.Text = sRow["lastname"].ToString();
                clco = sRow["classid"].ToString();
                textBox3.Text = Classes.Class.ClassNameByCode(clco);
                if(sRow["fastnote"].ToString() != "Empty") textBox4.Text = sRow["fastnote"].ToString();
                textBox5.ReadOnly = true;
                textBox5.Text = id.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(sRow["regdate"]);
                dateTimePicker2.Value = Convert.ToDateTime(sRow["enddate"]);
                if (sRow["gender"].ToString() == "1") radioButton2.Checked = true;
                else radioButton1.Checked = true;
                checkBox2.Checked = Convert.ToBoolean(sRow["active"]);
                SetAll(true);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (id == "-1")
            {
                if (CheckValues())
                {
                    Classes.Student.Add(textBox5.Text, textBox1.Text, textBox2.Text, clco, radioButton1.Checked ? Classes.App.Gender.Female : Classes.App.Gender.Male, textBox6.Text);
                    MessageBox.Show("!התלמיד נוסף בהצלחה", "EL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            else
            {
                this.Size = new Size(365, groupBox1.Visible ? 413 : 478);
                SetAll(groupBox1.Visible);
                groupBox1.Visible = !groupBox1.Visible;
            }
        }
        private void SetAll(bool rdonly)
        {
            Control[] cs = { textBox1, textBox2, textBox3, textBox4, dateTimePicker1, dateTimePicker2, checkBox2, radioButton1, radioButton2 };
            for (int i = 0; i < cs.Length; i++) cs[i].Enabled = !rdonly;
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
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            CancelStudent cans = new CancelStudent(id.ToString());
            cans.ShowDialog();
            if (cans.accept == 1)
            {
                Classes.Student.Delete(id, true);
                MessageBox.Show("!התלמיד בוטל בהצלחה", "EL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (cans.accept == 2)
            {
                Classes.Student.Delete(id, false);
                MessageBox.Show("!התלמיד בוטל לצמיתות בהצלחה", "EL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (CheckValues())
            {
                Classes.Student.Edit(id.ToString(), textBox1.Text, textBox2.Text, clco, dateTimePicker1.Value, dateTimePicker2.Value, radioButton1.Checked ? Classes.App.Gender.Female : Classes.App.Gender.Male, textBox6.Text, checkBox2.Checked, textBox4.Text.Length == 0 ? "Empty" : textBox4.Text);
                MessageBox.Show("!התלמיד עודכן בהצלחה", "EL", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (CheckValues())
            {
                Classes.Student.Edit(id.ToString(), textBox1.Text, textBox2.Text, clco, dateTimePicker1.Value, dateTimePicker2.Value, radioButton1.Checked ? Classes.App.Gender.Female : Classes.App.Gender.Male, textBox6.Text, checkBox2.Checked, textBox4.Text.Length == 0 ? "Empty" : textBox4.Text);
                MessageBox.Show("!התלמיד עודכן בהצלחה", "EL", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
        private bool CheckValues()
        {
            int v = 0;
            if (!Classes.Text.IsIdentityNumber(textBox5.Text))
            {
                if (textBox5.Text.Length == 10) textBox5.Text = textBox5.Text.Remove(0, 1);
                Classes.App.Error("חובה להקליד מספר תעודת זהות תקין.");
                return false;
            }
            if ((v = Classes.Text.IsValidField(textBox1.Text)) != 0)
            {
                if (v == 1) Classes.App.Error("חובה להקליד שם פרטי.");
                else if (v == 2) Classes.App.Error("השם הפרטי שהוקלד אינו תקין.");
                return false;
            }
            if ((v = Classes.Text.IsValidField(textBox2.Text)) != 0)
            {
                if (v == 1) Classes.App.Error("חובה להקליד שם משפחה.");
                else if (v == 2) Classes.App.Error("השם המשפחה שהוקלד אינו תקין.");
                return false;
            }
            if (Classes.Text.LanguageIs(textBox3.Text,Classes.Text.Language.Numbers) || clco == string.Empty)
            {
                Classes.App.Error("חובה לבחור כיתה תקין.");
                return false;
            }
            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                Classes.App.Error("חובה לבחור זכר או נקבה.");
                return false;
            }
            return true;
        }
    }
}