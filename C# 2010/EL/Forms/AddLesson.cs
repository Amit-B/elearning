using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace EL.Forms
{
    public partial class AddLesson : Form
    {
        public AddLesson()
        {
            InitializeComponent();
        }
        private List<string> startums = null, groups = new List<string>(), teachers = new List<string>(), gstudents = new List<string>();
        private List<Hashtable> notes = new List<Hashtable>();
        private void AddLesson_Load(object sender, EventArgs e)
        {
            numericUpDown3.Value = Classes.App.GetNextID("Lessons","lid");
            startums = Classes.Class.GetStartums();
            for (int i = 0; i < startums.Count; i++)
                comboBox3.Items.Add(startums[i]);
            DataTable teacherlist = Classes.Teacher.List();
            int sel = -1;
            for (int i = 0; i < teacherlist.Rows.Count; i++)
            {
                comboBox2.Items.Add(teacherlist.Rows[i]["firstname"].ToString() + " " + teacherlist.Rows[i]["lastname"].ToString());
                teachers.Add(teacherlist.Rows[i]["tid"].ToString());
                if (teacherlist.Rows[i]["tid"].ToString() == CurrentUser.ID) sel = i;
            }
            comboBox2.SelectedIndex = sel;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            label9.Enabled = !checkBox1.Checked;
            textBox2.Enabled = !checkBox1.Checked;
            label12.Enabled = checkBox1.Checked;
            label13.Enabled = checkBox1.Checked;
            label5.Enabled = checkBox1.Checked;
            checkedListBox1.Enabled = checkBox1.Checked;
            listBox1.Enabled = checkBox1.Checked;
            textBox4.Enabled = checkBox1.Checked;
            button6.Enabled = checkBox1.Checked;
            button7.Enabled = checkBox1.Checked;
            button8.Enabled = checkBox1.Checked;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == -1)
                Classes.App.Error("עליך לבחור שכבת לימוד לפני הוספת הערות משמעת.");
            else if (comboBox1.SelectedIndex == -1)
                Classes.App.Error("עליך לבחור קבוצת לימוד לפני הוספת הערות משמעת.");
            else
            {
                AddNote an = new AddNote(0, startums[comboBox3.SelectedIndex], groups[comboBox1.SelectedIndex], dateTimePicker1.Value.ToShortDateString(), (int)numericUpDown3.Value);
                an.ShowDialog();
                if (an.returned != null)
                {
                    notes.Add(an.returned);
                    listBox1.Items.Add(an.returned["StudentName"].ToString() + ": " + an.returned["NoteTypeName"].ToString() + (Convert.ToBoolean(an.returned["Justified"]) ? " (מוצדק)" : ""));
                }
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1 && listBox1.Items.Count > 0)
            {
                notes.RemoveAt(listBox1.SelectedIndex);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            textBox4.ResetText();
            checkBox1.Checked = true;
            textBox2.ResetText();
            listBox1.Items.Clear();
            notes.Clear();
            groups.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            numericUpDown3.Value = 1;
            dateTimePicker1.Value = DateTime.Today;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.Items.Count == 0) return;
            int c = 0;
            bool val = true;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                if(checkedListBox1.GetItemChecked(i))
                    c++;
            if (c == checkedListBox1.Items.Count) val = false;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                checkedListBox1.SetItemChecked(i, val);
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            comboBox1.Items.Clear();
            groups.Clear();
            DataTable grouplist = Classes.Group.List();
            for (int i = 0; i < grouplist.Rows.Count; i++)
                if (Classes.Class.GetStartumChar(Convert.ToInt32(grouplist.Rows[i]["gclass"])) == startums[comboBox3.SelectedIndex])
                {
                    comboBox1.Items.Add(grouplist.Rows[i]["gname"].ToString());
                    groups.Add(grouplist.Rows[i]["gid"].ToString());
                }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1) return;
            int id = -1;
            for (int i = 0; i < teachers.Count && id == -1; i++)
                if (teachers[i] == Classes.SQL.Select("Groups", "gid", groups[comboBox1.SelectedIndex].ToString()).Rows[0]["tid"].ToString())
                    id = i;
            comboBox2.SelectedIndex = id;
            DataTable students = Classes.Group.GetStudents(Convert.ToInt32(groups[comboBox1.SelectedIndex]));
            checkedListBox1.Items.Clear();
            gstudents.Clear();
            for (int i = 0; i < students.Rows.Count; i++)
            {
                checkedListBox1.Items.Add(students.Rows[i]["id"].ToString() + " - " + Classes.Student.GetName(students.Rows[i]["id"].ToString()), true);
                gstudents.Add(students.Rows[i]["id"].ToString());
            }
            ReloadCounts();
        }
        private void ReloadCounts()
        {
            label2.Text = checkedListBox1.CheckedItems.Count + " / " + checkedListBox1.Items.Count;
        }
        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            ReloadCounts();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == -1)
                Classes.App.Error("עליך לבחור שכבת לימוד.");
            else if (comboBox1.SelectedIndex == -1)
                Classes.App.Error("עליך לבחור קבוצת לימוד.");
            else if (comboBox2.SelectedIndex == -1)
                Classes.App.Error("עליך לבחור מורה שלימד בשיעור זה.");
            else
            {
                string warnings = string.Empty;
                if (checkBox1.Checked)
                {
                    if (checkedListBox1.Items.Count > 0)
                    {
                        if (checkedListBox1.CheckedItems.Count == checkedListBox1.Items.Count)
                            Classes.Text.WriteLine(ref warnings, "כל התלמידים הגיעו לשיעור");
                        else if (checkedListBox1.CheckedItems.Count == 0)
                            Classes.Text.WriteLine(ref warnings, "כולם נעדרו מהשיעור");
                    }
                    else
                        Classes.Text.WriteLine(ref warnings, "בשיעור זה לא היו תלמידים");
                }
                else
                    Classes.Text.WriteLine(ref warnings, "שיעור זה לא התקיים או בוטל");
                if (MessageBox.Show(":שים לב\r\n" + warnings + "\r\n\r\nהאם אתה מאשר את הוספת השיעור?", "הוספת שיעור", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Lesson
                    DateTime lessonDateTime = new DateTime(dateTimePicker1.Value.Year, dateTimePicker1.Value.Month, dateTimePicker1.Value.Day, (int)numericUpDown1.Value, 0, 0);
                    int lid = Classes.Lessons.AddLesson(int.Parse(groups[comboBox1.SelectedIndex]), int.Parse(teachers[comboBox2.SelectedIndex]), lessonDateTime, checkBox1.Checked, checkBox1.Checked ? textBox4.Text : textBox2.Text);
                    // Notes
                    for (int i = 0; i < notes.Count; i++)
                        Classes.Note.Add(lid, notes[i]["StudentID"].ToString(), Convert.ToInt32(notes[i]["NoteType"]), CurrentUser.ID, Convert.ToBoolean(notes[i]["Justified"]), notes[i]["Text"].ToString(), lessonDateTime);
                    // Absences
                    int ntid = -1;
                    DataTable dt = Classes.Note.ListNoteTypes();
                    for (int i = 0; i < dt.Rows.Count && ntid == -1; i++)
                        if (Convert.ToBoolean(dt.Rows[i]["absence"]))
                            ntid = i;
                    if(ntid != -1)
                        for (int i = 0; i < gstudents.Count; i++)
                            if(checkedListBox1.GetItemCheckState(i) == CheckState.Checked)
                                Classes.Note.Add(lid, gstudents[i], ntid, teachers[comboBox2.SelectedIndex], false, string.Empty, lessonDateTime);
                    MessageBox.Show(lid != (int)numericUpDown3.Value ? ".השיעור נוצר, אך לא בטוח שבצורה תקינה" : "!השיעור נוצר בהצלחה", "הוספת שיעור", MessageBoxButtons.OK, lid != (int)numericUpDown3.Value ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
                    if (ntid == -1 && gstudents.Count > 0)
                        MessageBox.Show("העדרויות לא נרשמו לשיעור זה כי לא נמצא סוג הערת משמעת של העדרות", "הוספת שיעור", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
            }
        }
    }
}