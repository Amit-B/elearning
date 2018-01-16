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
    public partial class Notes : Form
    {
        public Notes()
        {
            InitializeComponent();
        }
        private List<string> students = new List<string>(), groups = new List<string>(), startums = null, notetypes = new List<string>(),
            teachers = new List<string>(), allstudents = new List<string>();
        private string clco = string.Empty;
        private void Notes_Load(object sender, EventArgs e)
        {
            DataTable studentlist = Classes.Student.List();
            for (int i = 0; i < studentlist.Rows.Count; i++)
            {
                comboBox1.Items.Add(studentlist.Rows[i]["firstname"].ToString() + " " + studentlist.Rows[i]["lastname"].ToString());
                comboBox6.Items.Add(studentlist.Rows[i]["firstname"].ToString() + " " + studentlist.Rows[i]["lastname"].ToString());
                students.Add(studentlist.Rows[i]["id"].ToString());
                allstudents.Add(studentlist.Rows[i]["id"].ToString());
            }
            startums = Classes.Class.GetStartums();
            for (int i = 0; i < startums.Count; i++)
                comboBox7.Items.Add(startums[i]);
            ReloadNoteTypes();
            DataTable teacherlist = Classes.Teacher.List();
            for (int i = 0; i < teacherlist.Rows.Count; i++)
            {
                comboBox3.Items.Add(teacherlist.Rows[i]["firstname"].ToString() + " " + teacherlist.Rows[i]["lastname"].ToString());
                teachers.Add(teacherlist.Rows[i]["tid"].ToString());
            }
            ReloadNotes();
        }
        private void ReloadNotes()
        {
            dataGridView1.DataSource = Classes.Note.GetNotesBy(
            id: comboBox1.SelectedIndex == -1 ? "" : students[comboBox1.SelectedIndex],
            ntid: comboBox5.SelectedIndex == -1 ? "" : notetypes[comboBox5.SelectedIndex],
            tid: comboBox3.SelectedIndex == -1 ? "" : teachers[comboBox3.SelectedIndex]);
            if (dataGridView1.Rows.Count == 0)
            {
                label6.Text = "לא נמצאו הערות";
                System.Media.SystemSounds.Beep.Play();
            }
            else
            {
                label6.Text = dataGridView1.Rows.Count == 1 ? "נמצאה הערה אחת" : "סך הכל " + dataGridView1.Rows.Count + " הערות";
                System.Media.SystemSounds.Asterisk.Play();
            }
            Classes.App.GenerateDataGridViewStyle(dataGridView1, Classes.App.DataGridStyles.Notes);
        }
        private void ReloadNoteTypes()
        {
            DataTable nt = Classes.Note.ListNoteTypes();
            comboBox5.Items.Clear();
            listBox1.Items.Clear();
            notetypes.Clear();
            for (int i = 0; i < nt.Rows.Count; i++)
            {
                comboBox5.Items.Add(nt.Rows[i]["ntitle"].ToString());
                listBox1.Items.Add(nt.Rows[i]["ntitle"].ToString());
                notetypes.Add(nt.Rows[i]["ntid"].ToString());
                comboBox4.Items.Add(nt.Rows[i]["ntitle"].ToString());
            }
            label15.Text = nt.Rows.Count == 1 ? "סך הכל סוג הערה אחד." : ("סך הכל " + nt.Rows.Count + " סוגי הערות.");
        }
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            comboBox2.Enabled = true;
            comboBox7.Enabled = true;
            textBox5.Enabled = false;
            button13.Enabled = false;
            button14.Enabled = false;
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            comboBox2.Enabled = false;
            comboBox7.Enabled = false;
            textBox5.Enabled = true;
            button13.Enabled = true;
            button14.Enabled = true;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!Classes.Group.Exists(Convert.ToInt32(groups[comboBox1.SelectedIndex]))) return;
            comboBox1.SelectedIndex = -1;
            comboBox1.Items.Clear();
            students.Clear();
            DataTable studentlist = Classes.Group.GetStudents(Convert.ToInt32(groups[comboBox1.SelectedIndex]));
            for (int i = 0; i < studentlist.Rows.Count; i++)
            {
                comboBox1.Items.Add(studentlist.Rows[i]["firstname"].ToString() + " " + studentlist.Rows[i]["lastname"].ToString());
                students.Add(studentlist.Rows[i]["id"].ToString());
            }
        }
        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
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
        }
        private void button14_Click(object sender, EventArgs e)
        {
            SelectClass sc = new SelectClass(false);
            sc.ShowDialog();
            if (sc.clco.Length > 0)
            {
                this.clco = sc.clco;
                textBox3.Text = Classes.Class.ClassNameByCode(sc.clco);
            }
            if (!Classes.Class.Exists(clco)) return;
            comboBox2.SelectedIndex = -1;
            comboBox2.Items.Clear();
            students.Clear();
            Classes.SQL.Query("SELECT * FROM `Students` WHERE `classid` = '" + clco + "'");
            DataTable studentlist = Classes.SQL.ds.Tables[0];
            for (int i = 0; i < studentlist.Rows.Count; i++)
            {
                comboBox2.Items.Add(studentlist.Rows[i]["firstname"].ToString() + " " + studentlist.Rows[i]["lastname"].ToString());
                students.Add(studentlist.Rows[i]["id"].ToString());
            }
        }
        private void button13_Click(object sender, EventArgs e)
        {
            clco = string.Empty;
            textBox3.Text = string.Empty;
            comboBox2.SelectedIndex = -1;
            comboBox2.Items.Clear();
            students.Clear();
            DataTable studentlist = Classes.Student.List();
            for (int i = 0; i < studentlist.Rows.Count; i++)
            {
                comboBox2.Items.Add(studentlist.Rows[i]["firstname"].ToString() + " " + studentlist.Rows[i]["lastname"].ToString());
                students.Add(studentlist.Rows[i]["id"].ToString());
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ReloadNotes();
        }
        private void button11_Click(object sender, EventArgs e)
        {
            AddNote an = new AddNote(1);
            an.ShowDialog();
            DateTime ndatetime = Convert.ToDateTime(an.returned["Date"].ToString());
            if (an.returned != null)
            {
                int nid = Classes.Note.Add(Convert.ToInt32(an.returned["LessonID"]), an.returned["StudentID"].ToString(), Convert.ToInt32(an.returned["NoteType"]), CurrentUser.ID.ToString(), Convert.ToBoolean(an.returned["Justified"]), an.returned["Text"].ToString(), new DateTime(ndatetime.Year, ndatetime.Month, ndatetime.Day, Convert.ToInt32(an.returned["Time"]), 0, 0));
                ReloadNotes();
                MessageBox.Show("!הערת משמעת מספר #" + nid + " נוספה בהצלחה", "הוספת הערה", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // עריכה
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
                Classes.Note.Delete(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
        }
        private void button9_Click(object sender, EventArgs e)
        {
            // תקציר
        }
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox6.SelectedIndex == -1) return;
            label8.Text = "אזהרות עבור " + Classes.Student.GetName(allstudents[comboBox6.SelectedIndex]) + ":";
            DataTable notes = Classes.Note.GetNotesBy(id: allstudents[comboBox6.SelectedIndex]),
                notetypes = Classes.Note.ListNoteTypes();
            List<int> absenceNoteTypes = new List<int>();
            Hashtable other = new Hashtable(), amounts = new Hashtable();
            textBox1.ResetText();
            for (int i = 0; i < notetypes.Rows.Count; i++)
                if (Convert.ToBoolean(notetypes.Rows[i]["absence"]))
                    absenceNoteTypes.Add(Convert.ToInt32(notetypes.Rows[i]["ntid"]));
                else
                {
                    other[notetypes.Rows[i]["ntid"].ToString()] = notetypes.Rows[i]["ntitle"].ToString();
                    amounts[notetypes.Rows[i]["ntid"].ToString()] = 0;
                }
            int absences = 0, lessons = Classes.Lessons.Count(Classes.Lessons.LessonSelectType.Exist);
            if (lessons > 0)
            {
                for (int i = 0; i < notes.Rows.Count; i++)
                {
                    if (absenceNoteTypes.Contains(Convert.ToInt32(notes.Rows[i]["ntid"])))
                        absences++;
                    else
                        amounts.Add(notetypes.Rows[i]["ntid"].ToString(), Convert.ToInt32(amounts[notetypes.Rows[i]["ntid"].ToString()]) + 1);
                }
                if (absences >= (double)lessons * 0.3)
                    textBox1.Text += "לתלמיד זה יש מעל ל-30 אחוז היעדרויות מהשיעורים שהתקיימו עד עכשיו (" + Convert.ToInt32((double)(absences * 100) / (double)lessons) + "%)";
                for (int i = 0; i < amounts.Count; i++)
                    if (Convert.ToInt32(amounts[i]) >= (double)lessons * 0.3)
                        textBox1.Text += "לתלמיד זה יש מעל ל-30 אחוז הערות משמעת מסוג \"" + other[i].ToString() + "\" (" + Convert.ToInt32((double)(Convert.ToInt32(amounts[i]) * 100) / (double)lessons) + "%)";
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            // תקציר אזהרות
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRow details = Classes.Note.GetNoteTypeInfo(Convert.ToInt32(notetypes[listBox1.SelectedIndex].ToString()));
            groupBox3.Text = "מאפייני סוג: " + details["ntitle"].ToString();
            textBox2.Text = notetypes[listBox1.SelectedIndex].ToString();
            textBox3.Text = details["ntitle"].ToString();
            textBox4.Text = Classes.Teacher.GetName(details["addedby"].ToString());
            textBox6.Text = Convert.ToBoolean(details["absence"]) ? "כן" : "לא";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Input i = new Input("הוספת סוג הערה", "נא להקליד כאן את שם סוג ההערה החדש שיווצר:");
            i.ShowDialog();
            if (i.returned.Length > 0)
            {
                Classes.Note.AddNoteType(CurrentUser.ID, i.returned, false);
                MessageBox.Show("סוג ההערה החדש נוסף בהצלחה.", "סוגי הערות", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReloadNoteTypes();
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                Classes.App.Error("עליך לבחור סוג הערה כדי לבצע את הפעולה הזו.");
            else
            {
                Input i = new Input("עריכת סוג הערה", "נא להקליד כאן את השם החדש שיינתן לסוג ההערה שבחרת:");
                i.ShowDialog();
                if (i.returned.Length > 0)
                {
                    if (i.returned == Classes.Note.GetNoteTypeInfo(Convert.ToInt32(notetypes[listBox1.SelectedIndex]))["ntitle"].ToString())
                        Classes.App.Error("שם סוג ההערה לא השתנה.");
                    else
                    {
                        Classes.Note.RenameNoteType(Convert.ToInt32(notetypes[listBox1.SelectedIndex]), i.returned);
                        MessageBox.Show("סוג ההערה נערך בהצלחה.", "סוגי הערות", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ReloadNoteTypes();
                    }
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                Classes.App.Error("עליך לבחור סוג הערה כדי לבצע את הפעולה הזו.");
            else
            {
                DataTable dt = Classes.Note.GetNotesBy(ntid: notetypes[listBox1.SelectedIndex]);
                if (dt.Rows.Count > 0)
                {
                    if (radioButton1.Checked)
                    {
                        if (MessageBox.Show("האם אתה בטוח שברצונך למחוק את סוג ההערה ביחד עם " + dt.Rows.Count + " הערות שרשומות עליו?", "סוגי הערות", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Classes.SQL.Delete("Notes", "ntid", notetypes[listBox1.SelectedIndex]);
                            Classes.Note.DeleteNoteType(Convert.ToInt32(notetypes[listBox1.SelectedIndex]));
                            MessageBox.Show("סוג ההערה נמחק בהצלחה ביחד עם ההערות שהיו רשומות עליו.", "סוגי הערות", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        if (comboBox4.SelectedIndex == -1)
                            Classes.App.Error("עליך לבחור את סוג ההערה אליו יעברו כל ההערות שרשומות על הסוג שברצונך למחוק.");
                        else if (notetypes[listBox1.SelectedIndex] == notetypes[comboBox4.SelectedIndex])
                            Classes.App.Error("סוג ההערה שבחרת להעברת ההערות זהה לסוג שאתה רוצה למחוק - עליך לשנות אחד מהם.");
                        else if (MessageBox.Show("האם אתה בטוח שברצונך למחוק את סוג ההערה ולהעביר את " + dt.Rows.Count + " הערות שרשומות עליו?", "סוגי הערות", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Classes.SQL.Update("Notes", "`ntid` = '" + notetypes[comboBox4.SelectedIndex] + "'", "ntid", notetypes[listBox1.SelectedIndex]);
                            Classes.Note.DeleteNoteType(Convert.ToInt32(notetypes[listBox1.SelectedIndex]));
                            MessageBox.Show("סוג ההערה נמחק בהצלחה ביחד עם ההערות שהיו רשומות עליו.", "סוגי הערות", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    Classes.Note.DeleteNoteType(Convert.ToInt32(notetypes[listBox1.SelectedIndex]));
                    MessageBox.Show("סוג ההערה נמחק ללא הערות שהשתנו או נמחקו יחד איתו.", "סוגי הערות", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("אפשרות זו תמחק את כל סוגי ההערות וגם את כל ההערות.\r\nהאם ברצונך להמשיך?", "סוגי הערות", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Classes.SQL.Delete("Notes");
                Classes.SQL.Delete("NoteTypes");
                MessageBox.Show("ההערות והסוגים נמחקו בהצלחה.", "סוגי הערות", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button8_Click_1(object sender, EventArgs e)
        {
            Input i = new Input("הוספת העדרות", "נא להקליד כאן את שם סוג ההערה החדש שיווצר (בתור העדרות):");
            i.ShowDialog();
            if (i.returned.Length > 0)
            {
                Classes.Note.AddNoteType(CurrentUser.ID, i.returned, true);
                MessageBox.Show("סוג ההערה החדש נוסף כהעדרות בהצלחה.", "סוגי הערות", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReloadNoteTypes();
            }
        }
        private void button12_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            ReloadNotes();
        }
    }
}