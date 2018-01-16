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
    public partial class Lessons : Form
    {
        public Lessons()
        {
            InitializeComponent();
        }
        private List<string> startums = null, groups = new List<string>(), teachers = new List<string>();
        private bool editmode = false;
        private int current = -1;
        private void Lessons_Load(object sender, EventArgs e)
        {
            startums = Classes.Class.GetStartums();
            for (int i = 0; i < startums.Count; i++)
                comboBox3.Items.Add(startums[i]);
        }
        private void ReloadLessons()
        {
            if (groups.Count > 0 && comboBox2.SelectedIndex != -1 && Classes.Group.Exists(comboBox2.SelectedIndex))
            {
                string qu = "SELECT * FROM `Lessons` WHERE `active` = " + true;
                if (comboBox2.SelectedIndex != groups.Count)
                    qu += " AND `gid` = '" + groups[comboBox2.SelectedIndex] + "'";
                Classes.SQL.Query(qu);
                dataGridView1.DataSource = Classes.SQL.ds.Tables[0];
                label21.Visible = dataGridView1.Rows.Count == 0;
                dataGridView1.Visible = dataGridView1.Rows.Count > 0;
                Classes.App.GenerateDataGridViewStyle(dataGridView1, Classes.App.DataGridStyles.Lessons);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ReloadLessons();
            if (!label2.Visible) label2.Visible = true;
            if (dataGridView1.Rows.Count == 0)
            {
                label2.Text = "לא נמצאו שיעורים";
                System.Media.SystemSounds.Beep.Play();
            }
            else
            {
                label2.Text = dataGridView1.Rows.Count == 1 ? "נמצא שיעור אחד" : "סך הכל " + dataGridView1.Rows.Count + " שיעורים";
                System.Media.SystemSounds.Asterisk.Play();
            }
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = -1;
            comboBox2.Items.Clear();
            groups.Clear();
            DataTable grouplist = Classes.Group.List();
            for (int i = 0; i < grouplist.Rows.Count; i++)
                if (Classes.Class.GetStartumChar(Convert.ToInt32(grouplist.Rows[i]["gclass"])) == startums[comboBox3.SelectedIndex])
                {
                    comboBox2.Items.Add(grouplist.Rows[i]["gname"].ToString());
                    groups.Add(grouplist.Rows[i]["gid"].ToString());
                }
            //comboBox2.Items.Add("כל קבוצה");
        }
        private void button11_Click(object sender, EventArgs e)
        {
            new AddLesson().ShowDialog();
            ReloadLessons();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
                if (dataGridView1.SelectedRows[0].Index != -1)
                {
                    SingleLessonMode("Focus");
                    SingleLessonMode("Select", int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                    SingleLessonMode("Edit", 1);
                }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("?האם אתה בטוח שברצונך למחוק את השיעור", "מחיקת שיעור", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (current == int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                    SingleLessonMode("Reset");
                Classes.Lessons.Delete(int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                ReloadLessons();
                MessageBox.Show("!השיעור נמחק בהצלחה", "מחיקת שיעור", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            // תקציר
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            button2_Click(sender, e);
        }
        private void SingleLessonMode(string act, int param = 0)
        {
            switch (act)
            {
                case "Focus":
                    {
                        tabControl1.SelectedIndex = 1;
                        break;
                    }
                case "Select":
                    {
                        numericUpDown4.Value = param;
                        current = param;
                        if (label15.Visible)
                        {
                            label15.Visible = false;
                            groupBox5.Visible = true;
                        }
                        DataRow info = Classes.Lessons.GetInfo(param);
                        label6.Text = Classes.Group.GetName(int.Parse(info["gid"].ToString())) + "(" + info["gid"] + ")";
                        DataTable teacherlist = Classes.Teacher.List(), notes = null;
                        int sel = -1;
                        for (int i = 0; i < teacherlist.Rows.Count; i++)
                        {
                            comboBox5.Items.Add(teacherlist.Rows[i]["firstname"].ToString() + " " + teacherlist.Rows[i]["lastname"].ToString());
                            teachers.Add(teacherlist.Rows[i]["tid"].ToString());
                            if (teacherlist.Rows[i]["tid"].ToString() == info["tid"].ToString()) sel = i;
                        }
                        comboBox5.SelectedIndex = sel;
                        if (checkBox1.Checked = (Convert.ToInt32(info["lexist"]) == 1 ? true : false))
                        {
                            textBox2.Text = info["ltitle"].ToString();
                            notes = Classes.Note.GetNotesBy(lid: param.ToString());
                            int[] counts = { 0, 0 };
                            DataRow student = null, nt = null;
                            listBox1.Items.Clear();
                            listBox2.Items.Clear();
                            for (int i = 0; i < notes.Rows.Count; i++)
                            {
                                student = Classes.Student.GetFullInfo(notes.Rows[i]["id"].ToString());
                                nt = Classes.Note.GetNoteTypeInfo(int.Parse(notes.Rows[i]["ntid"].ToString()));
                                if (Convert.ToBoolean(nt["absence"]))
                                {
                                    counts[0]++;
                                    listBox1.Items.Add("(" + student["id"] + ") " + student["firstname"] + " " + student["lastname"]);
                                }
                                else
                                {
                                    counts[1]++;
                                    listBox2.Items.Add("(" + student["id"] + ") " + student["firstname"] + " " + student["lastname"] + ": " + Classes.Note.GetNoteTypeInfo(Convert.ToInt32(nt["ntid"]))["ntitle"].ToString());
                                }
                            }
                            label11.Text = counts[0].ToString();
                            label20.Text = counts[1].ToString();
                        }
                        else textBox4.Text = info["ltitle"].ToString();
                        DateTime dt = Convert.ToDateTime(info["ldate"].ToString()), dt2 = Convert.ToDateTime(info["lasteditdt"]);
                        dateTimePicker1.Value = new DateTime(dt.Year, dt.Month, dt.Day);
                        numericUpDown1.Value = dt.Hour;
                        label17.Text = Classes.Teacher.GetName(info["lastedit"].ToString());
                        label19.Text = label19.Text.Replace("X", Classes.App.IntBase(dt2.Day, 2) + "/" + Classes.App.IntBase(dt2.Month, 2) + "/" + dt2.Year);
                        label19.Text = label19.Text.Replace("Y", Classes.App.IntBase(dt2.Hour, 2) + ":" + Classes.App.IntBase(dt2.Minute, 2));
                        break;
                    }
                case "Edit":
                    {
                        editmode = (param == 1);
                        Control[] ctrls = { textBox2, textBox4, comboBox5, checkBox1, dateTimePicker1, numericUpDown1 };
                        for(int i = 0; i < ctrls.Length; i++)
                            ctrls[i].Enabled = editmode;
                        break;
                    }
                case "Reset":
                    {
                        SingleLessonMode("Edit", 0);
                        groupBox5.Visible = false;
                        current = -1;
                        break;
                    }
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            if (Classes.Lessons.Exists((int)numericUpDown4.Value))
                SingleLessonMode("Select", (int)numericUpDown4.Value);
            else
                Classes.App.Error("שיעור זה לא קיים.");
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (editmode)
            {
                SingleLessonMode("Select", (int)numericUpDown4.Value);
                SingleLessonMode("Edit", 0);
                button8.Text = "עריכה";
                numericUpDown4.ReadOnly = false;
            }
            else
            {
                SingleLessonMode("Edit", 1);
                button8.Text = "ביטול";
                numericUpDown4.ReadOnly = true;
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.ReadOnly = !checkBox1.Checked;
            textBox4.ReadOnly = checkBox1.Checked;
        }
        private void button13_Click(object sender, EventArgs e)
        {
            // תקציר
        }
        private void button14_Click(object sender, EventArgs e)
        {
            Classes.SQL.Update("Lessons",
@"`ldate` = '" + dateTimePicker1.Value.ToShortDateString() + @"',
`lexist` = '" + Convert.ToInt32(checkBox1.Checked) + @"',
`ltitle` = '" + (checkBox1.Checked ? textBox2.Text : textBox4.Text) + @"',
`lastedit` = '" + CurrentUser.ID + @"',
`lasteditdt` = '" + DateTime.Now.ToString() + "'",
                  "lid", numericUpDown4.Value.ToString());
            MessageBox.Show("!השיעור נערך בהצלחה", "עריכת שיעור", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ReloadLessons();
            SingleLessonMode("Reset");
        }
        private void button12_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("?האם אתה בטוח שברצונך למחוק את השיעור", "מחיקת שיעור", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Classes.Lessons.Delete((int)numericUpDown4.Value);
                SingleLessonMode("Reset");
                ReloadLessons();
                MessageBox.Show("!השיעור נמחק בהצלחה", "מחיקת שיעור", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}