using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace EL.Forms.Administration_Forms
{
    public partial class TeacherAdd : UserControl
    {
        public TeacherAdd()
        {
            InitializeComponent();
        }
        List<string> dutyids = new List<string>();
        private void TeacherAdd_Load(object sender, EventArgs e)
        {
            DataTable ds = Classes.Duty.List();
            for (int i = 0; i < ds.Rows.Count; i++)
            {
                comboBox1.Items.Add(ds.Rows[i]["dutyname"].ToString());
                dutyids.Add(ds.Rows[i]["dutyid"].ToString());
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.ResetText();
            textBox2.ResetText();
            textBox3.ResetText();
            textBox4.ResetText();
            comboBox1.SelectedIndex = -1;
            checkBox1.Checked = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (Classes.Text.IsValidField(textBox3.Text) != 0)
                Classes.App.Error("עליך להקליד שם פרטי תקין.");
            else if (Classes.Text.IsValidField(textBox2.Text) != 0)
                Classes.App.Error("עליך להקליד שם משפחה תקין.");
            else if (!Classes.Text.IsIdentityNumber(textBox1.Text))
                Classes.App.Error("עליך להקליד מספר ת.ז תקין.");
            else if (!radioButton1.Checked && !radioButton2.Checked)
                Classes.App.Error("עליך לבחור מין.");
            else if (textBox4.Text.Length == 0)
                Classes.App.Error("עליך לבחור סיסמת התחברות.");
            else if (Classes.Teacher.Exists(textBox1.Text))
                Classes.App.Error("מורה בת.ז הזה כבר קיים.");
            else
            {
                Classes.Teacher.Add(textBox1.Text, textBox3.Text, textBox2.Text, radioButton1.Checked ? Classes.App.Gender.Male : Classes.App.Gender.Female, "?", textBox4.Text, checkBox1.Checked, comboBox1.SelectedIndex == -1 ? 0 : Convert.ToInt32(dutyids[comboBox1.SelectedIndex]));
                MessageBox.Show("המורה נוסף בהצלחה:\r\n\r\nת.ז: " + textBox1.Text + "\r\nשם: " + textBox3.Text + " " + textBox2.Text + "\r\nסיסמא: " + textBox4.Text, "הוספת מורה", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}