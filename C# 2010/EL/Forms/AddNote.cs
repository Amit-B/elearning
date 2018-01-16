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
    public partial class AddNote : Form
    {
        private string st = string.Empty, gname = string.Empty;
        private int mode, hour = 0, lid = 0;
        private DateTime dt;
        private List<string> startums = null, groups = new List<string>(), students = new List<string>(), notetypes = new List<string>(), notenames = new List<string>();
        private const int LESSON_MODE = 0, NOTE_MODE = 1, EDIT_MODE = 2;
        private DataRow curNote = null;
        public Hashtable returned = null;
        public AddNote(int mode, string st = "", string gname = "", string dt = "", int hour = 0, int lid = 0)
        {
            InitializeComponent();
            this.mode = mode;
            switch (mode)
            {
                case LESSON_MODE:
                    {
                        this.st = st;
                        this.gname = gname;
                        this.dt = DateTime.Parse(dt);
                        this.hour = hour;
                        this.lid = lid;
                        break;
                    }
                case EDIT_MODE:
                    {
                        curNote = Classes.SQL.Select("Notes", "nid", st).Rows[0];
                        break;
                    }
            }
        }
        private void AddNote_Load(object sender, EventArgs e)
        {
            switch (mode)
            {
                case LESSON_MODE:
                    {
                        comboBox4.Text = st;
                        comboBox1.Text = gname;
                        numericUpDown2.Value = lid;
                        dateTimePicker1.Value = dt;
                        numericUpDown1.Value = hour;
                        Control[] controls = { label6, label1, label5, label8, comboBox1, comboBox4, dateTimePicker1, numericUpDown1, numericUpDown2 };
                        for (int i = 0; i < controls.Length; i++)
                            controls[i].Enabled = false;
                        break;
                    }
                case NOTE_MODE:
                    {
                        startums = Classes.Class.GetStartums();
                        for (int i = 0; i < startums.Count; i++)
                            comboBox4.Items.Add(startums[i]);
                        break;
                    }
                case EDIT_MODE:
                    {
                        startums = Classes.Class.GetStartums();
                        for (int i = 0; i < startums.Count; i++)
                            comboBox4.Items.Add(startums[i]);
                        numericUpDown2.Value = Convert.ToDecimal(curNote["lid"]);
                        DateTime dt = Convert.ToDateTime(curNote["ndatetime"].ToString());
                        dateTimePicker1.Value = new DateTime(dt.Year, dt.Month, dt.Day);
                        numericUpDown1.Value = dt.Hour;
                        checkBox1.Checked = Convert.ToBoolean(curNote["justified"]);
                        textBox2.Text = curNote["explain"].ToString();
                        break;
                    }
            }
            DataTable nt = Classes.Note.ListNoteTypes();
            for (int i = 0; i < nt.Rows.Count; i++)
            {
                comboBox3.Items.Add(nt.Rows[i]["ntitle"].ToString());
                notetypes.Add(nt.Rows[i]["ntid"].ToString());
                notenames.Add(nt.Rows[i]["ntitle"].ToString());
            }
            comboBox4.SelectedIndex = -1;
            DataTable studentlist = Classes.Student.List();
            for (int i = 0; i < studentlist.Rows.Count; i++)
            {
                comboBox2.Items.Add(studentlist.Rows[i]["firstname"].ToString() + " " + studentlist.Rows[i]["lastname"].ToString());
                students.Add(studentlist.Rows[i]["id"].ToString());
            }
            if (mode == EDIT_MODE)
            {
                comboBox2.SelectedIndex = students.IndexOf(curNote["id"].ToString());
                comboBox3.SelectedIndex = notetypes.IndexOf(curNote["ntid"].ToString());
            }
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mode != NOTE_MODE) return;
            comboBox1.SelectedIndex = -1;
            comboBox1.Items.Clear();
            groups.Clear();
            DataTable grouplist = Classes.Group.List();
            for (int i = 0; i < grouplist.Rows.Count; i++)
                if (Classes.Class.GetStartumChar(Convert.ToInt32(grouplist.Rows[i]["gclass"])) == startums[comboBox4.SelectedIndex])
                {
                    comboBox1.Items.Add(grouplist.Rows[i]["gname"].ToString());
                    groups.Add(grouplist.Rows[i]["gid"].ToString());
                }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mode != NOTE_MODE) return;
            if (comboBox1.SelectedIndex != -1)
            {
                comboBox2.SelectedIndex = -1;
                comboBox2.Items.Clear();
                students.Clear();
                DataTable studentlist = Classes.Group.GetStudents(Convert.ToInt32(groups[comboBox1.SelectedIndex]));
                for (int i = 0; i < studentlist.Rows.Count; i++)
                {
                    comboBox2.Items.Add(studentlist.Rows[i]["firstname"].ToString() + " " + studentlist.Rows[i]["lastname"].ToString());
                    students.Add(studentlist.Rows[i]["id"].ToString());
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1)
                Classes.App.Error("עליך לבחור תלמיד להוספת ההערה.");
            else if (comboBox3.SelectedIndex == -1)
                Classes.App.Error("עליך לבחור סוג הערה.");
            else
            {
                returned = new System.Collections.Hashtable();
                returned.Add("StudentID", students[comboBox2.SelectedIndex]);
                returned.Add("StudentName", Classes.Student.GetName(students[comboBox2.SelectedIndex]));
                returned.Add("NoteType", notetypes[comboBox3.SelectedIndex]);
                returned.Add("NoteTypeName", notenames[comboBox3.SelectedIndex]);
                returned.Add("LessonID", (int)numericUpDown2.Value);
                returned.Add("Date", dateTimePicker1.Value.ToShortDateString());
                returned.Add("Time", numericUpDown1.Value);
                returned.Add("Justified", checkBox1.Checked);
                returned.Add("Text", textBox2.Text);
                this.Close();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            /*ComboBox[] cb = { comboBox2, comboBox3, null, null };
            if (mode != LESSON_MODE)
            {
                cb[2] = comboBox1;
                cb[3] = comboBox4;
            }
            for (int i = 0; i < cb.Length; i++)
                cb[i].SelectedIndex = -1;
            dateTimePicker1.Value = DateTime.Today;
            numericUpDown1.Value = 1;
            textBox2.ResetText();*/
            returned = null;
            this.Close();
        }
    }
}