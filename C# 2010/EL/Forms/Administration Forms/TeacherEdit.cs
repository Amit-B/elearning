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
    public partial class TeacherEdit : UserControl
    {
        public TeacherEdit()
        {
            InitializeComponent();
        }
        private List<string> dutyids = new List<string>();
        private string viewing = string.Empty;
        private void TeacherEdit_Load(object sender, EventArgs e)
        {
            DataTable ds = Classes.Duty.List();
            for (int i = 0; i < ds.Rows.Count; i++)
            {
                comboBox1.Items.Add(ds.Rows[i]["dutyname"].ToString());
                dutyids.Add(ds.Rows[i]["dutyid"].ToString());
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (!Classes.Text.IsIdentityNumber(textBox5.Text))
                Classes.App.Error("עליך להקליד מספר ת.ז תקין.");
            else if (!Classes.Teacher.Exists(textBox5.Text))
                Classes.App.Error("מספר תעודת הזהות הזה לא קיים ברשימת המורים.");
            else
            {
                viewing = textBox5.Text;
                DataRow row = Classes.SQL.Select("Teachers", "tid", textBox5.Text).Rows[0];
                textBox3.Text = row["firstname"].ToString();
                textBox2.Text = row["lastname"].ToString();
                if (Convert.ToInt32(row["gender"]) == 1)
                    radioButton1.Checked = true;
                else
                    radioButton2.Checked = true;
                checkBox1.Checked = Convert.ToBoolean(row["admin"]);
                textBox4.Text = row["password"].ToString();
                comboBox1.SelectedIndex = -1;
                for (int i = 0; i < dutyids.Count && comboBox1.SelectedIndex == -1; i++)
                    if (dutyids[i] == row["dutyid"].ToString())
                        comboBox1.SelectedIndex = i;
                label9.Visible = true;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (viewing.Length == 0)
                Classes.App.Error("נא לבחור מורה.");
            else
            {
                if ((!radioButton1.Checked && !radioButton2.Checked) || Classes.Text.IsValidField(textBox2.Text) != 0 || Classes.Text.IsValidField(textBox3.Text) != 0 || textBox4.Text.Length == 0 || comboBox1.SelectedIndex == -1)
                    Classes.App.Error("הפרטים שנרשמו אינם תקינים.");
                else
                {
                    Classes.SQL.Update("Teachers", "`firstname` = '" + textBox3.Text + "', `lastname` = '" + textBox2.Text + "', `gender` = '" + (radioButton1.Checked ? 1 : 2) + "', `admin` = '" + Convert.ToInt32(checkBox1.Checked) + "', `password` = '" + textBox4.Text + "', `dutyid` = '" + (comboBox1.SelectedIndex == -1 ? 0.ToString() : dutyids[comboBox1.SelectedIndex]) + "'", "tid", viewing);
                    MessageBox.Show("המורה נערך בהצלחה.", "מחיקת מורה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Reset();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (viewing.Length == 0)
                Classes.App.Error("נא לבחור מורה.");
            else
            {
                Classes.Teacher.Kill(viewing);
                MessageBox.Show("המורה נמחק בהצלחה.", "מחיקת מורה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
            }
        }
        private void Reset()
        {
            viewing = string.Empty;
            textBox3.ResetText();
            textBox2.ResetText();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            checkBox1.Checked = false;
            textBox4.ResetText();
            comboBox1.SelectedIndex = -1;
            label9.Visible = false;
            textBox5.ResetText();
        }
    }
}