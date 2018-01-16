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
    public partial class GroupEditor : Form
    {
        public GroupEditor()
        {
            InitializeComponent();
        }
        private List<string> classes = new List<string>(), teachers = new List<string>(), embedClasses = new List<string>(),
            embedStudents = new List<string>(), groups = new List<string>(), subjects = new List<string>(), startums = null;
        private void GroupEditor_Load(object sender, EventArgs e)
        {
            ReloadClasses();
            DataTable list = Classes.Teacher.List();
            for (int i = 0; i < list.Rows.Count; i++)
            {
                comboBox1.Items.Add(list.Rows[i]["tid"] + " - " + list.Rows[i]["firstname"] + " " + list.Rows[i]["lastname"]);
                comboBox5.Items.Add(list.Rows[i]["tid"] + " - " + list.Rows[i]["firstname"] + " " + list.Rows[i]["lastname"]);
                teachers.Add(list.Rows[i]["tid"].ToString());
            }
            Classes.SQL.Query("SELECT * FROM `Students` ORDER BY `firstname` DESC");
            for (int i = 0; i < Classes.SQL.ds.Tables[0].Rows.Count; i++)
                listBox3.Items.Add(Classes.SQL.ds.Tables[0].Rows[i]["id"].ToString() + ": " + Classes.SQL.ds.Tables[0].Rows[i]["firstname"].ToString() + " " + Classes.SQL.ds.Tables[0].Rows[i]["lastname"].ToString());
            list = Classes.Subject.List();
            for (int i = 0; i < list.Rows.Count; i++)
            {
                subjects.Add(list.Rows[i]["sid"].ToString());
                comboBox3.Items.Add(list.Rows[i]["stitle"]);
            }
            ReloadGroups();
        }
        private void ReloadClasses()
        {
            DataTable list = Classes.Class.List();
            classes.Clear();
            listBox1.Items.Clear();
            listBox1.SelectedIndex = -1;
            for (int i = 0; i < list.Rows.Count; i++)
            {
                classes.Add(list.Rows[i]["classid"].ToString());
                listBox1.Items.Add(Classes.Class.ClassNameByCode(list.Rows[i]["classid"].ToString()));
            }
            startums = Classes.Class.GetStartums();
            listBox6.Items.Clear();
            comboBox4.Items.Clear();
            for (int i = 0; i < startums.Count; i++)
            {
                listBox6.Items.Add(startums[i]);
                comboBox4.Items.Add(startums[i]);
            }
        }
        private void ReloadGroups()
        {
            DataTable list = Classes.Group.List();
            groups.Clear();
            listBox4.Items.Clear();
            listBox4.SelectedIndex = -1;
            for (int i = 0; i < list.Rows.Count; i++)
            {
                groups.Add(list.Rows[i]["gid"].ToString());
                listBox4.Items.Add(list.Rows[i]["gname"].ToString() + " (" + Classes.Class.GetStartumChar(Convert.ToInt32(list.Rows[i]["gclass"])) + ")");
            }
        }
        private void UpdateClassProperties()
        {
            if (listBox1.SelectedIndex != -1)
            {
                label3.Text = classes[listBox1.SelectedIndex].ToString();
                label4.Text = Classes.Class.GetStartumChar(Classes.Class.GetClCoStartumNum(classes[listBox1.SelectedIndex]));
                label5.Text = Classes.Class.GetClCoClassNum(classes[listBox1.SelectedIndex]).ToString();
                label9.Text = Classes.Class.GetStudents(classes[listBox1.SelectedIndex]).Rows.Count.ToString();
                label14.Text = Classes.Teacher.GetName(Classes.Class.GetInfo(classes[listBox1.SelectedIndex], "tid"));
            }
        }
        private void UpdateGroupProperties()
        {
            if (listBox4.SelectedIndex != -1)
            {
                DataRow info = Classes.Group.GetInfo(Convert.ToInt32(groups[listBox4.SelectedIndex])).Rows[0];
                label27.Text = groups[listBox4.SelectedIndex];
                label33.Text = Classes.Subject.GetName(Convert.ToInt32(info["sid"]));
                label19.Text = Classes.Class.GetStartumChar(Convert.ToInt32(info["gclass"]));
                label30.Text = Classes.Group.GetStudents(Convert.ToInt32(groups[listBox4.SelectedIndex])).Rows.Count.ToString();
                label29.Text = Classes.Teacher.GetName(info["tid"].ToString());
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateClassProperties();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                Classes.App.Error("עליך לבחור כיתה מרשימת הכיתות.");
            else if (Classes.Class.GetStudents(classes[listBox1.SelectedIndex]).Rows.Count == 0)
                Classes.App.Error("בכיתה זו אין תלמידים.");
            else
            {
                MessageBox.Show("מהתפריט שיופיע לפניך, בחר כיתה שאליה יועברו כל התלמידים מכיתה " + Classes.Class.ClassNameByCode(classes[listBox1.SelectedIndex]), "ניהול כיתות", MessageBoxButtons.OK, MessageBoxIcon.Information);
                string clco = string.Empty;
                SelectClass sc = new SelectClass(true);
                sc.ShowDialog();
                if (clco.Length > 0)
                {
                    clco = sc.clco;
                    if (clco == classes[listBox1.SelectedIndex])
                        Classes.App.Error("בחרת שתי כיתות זהות.");
                    else
                    {
                        if (MessageBox.Show("האם אתה בטוח שברצונך להעביר " +
                            Classes.Class.GetStudents(classes[listBox1.SelectedIndex]).Rows.Count + " תלמידים מכיתה " + Classes.Class.ClassNameByCode(classes[listBox1.SelectedIndex]) +
                            " לכיתה " + Classes.Class.ClassNameByCode(clco) + " שבה כבר יש " + Classes.Class.GetStudents(clco).Rows.Count + " תלמידים?"
                            , "ניהול כיתות", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Classes.SQL.Update("Students", "`classid` = '" + clco + "'", "classid", classes[listBox1.SelectedIndex]);
                            MessageBox.Show("ההעברה נעשתה, כעת בכיתה " + Classes.Class.ClassNameByCode(clco) + " יש " + Classes.Class.GetStudents(clco).Rows.Count + " תלמידים.", "ניהול כיתות", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            UpdateClassProperties();
                        }
                    }
                }
                sc.Dispose();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                Classes.App.Error("עליך לבחור כיתה מרשימת הכיתות.");
            else
            {
                new EditClassStudents(classes[listBox1.SelectedIndex]).ShowDialog();
                UpdateClassProperties();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                Classes.App.Error("עליך לבחור כיתה מרשימת הכיתות.");
            else if (Classes.Class.Count() == 1)
                Classes.App.Error("המערכת חייבת להכיל לפחות כיתה אחת. בגלל שזו הכיתה האחרונה, לא ניתן למחוק אותה.");
            else
            {
                bool flag = false;
                if (Classes.Class.List(startum: Classes.Class.GetClCoStartumNum(classes[listBox1.SelectedIndex]).ToString()).Rows.Count == 1)
                {
                    DataTable list = Classes.Group.List();
                    for (int i = 0; i < list.Rows.Count && !flag; i++)
                        if (list.Rows[i]["gclass"].ToString() == Classes.Class.GetClCoStartumNum(classes[listBox1.SelectedIndex]).ToString())
                            flag = true;
                }
                if (flag)
                    Classes.App.Error("הכיתה שבחרת למחוק היא האחרונה מהשכבה שלה. קיימות קבוצות לימוד שמתנהלות בשכבה זו. עליך למחוק או לשנות את הקבוצות האלו ולאחר מכן לחזור לכאן.");
                else
                {
                    Classes.SQL.Delete("Classes", "classid", classes[listBox1.SelectedIndex]);
                    ReloadClasses();
                    MessageBox.Show("הכיתה נמחקה בהצלחה.", "ניהול כיתות", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void button14_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                Classes.App.Error("עליך לבחור כיתה מרשימת הכיתות.");
            else
            {
                SelectTeacher st = new SelectTeacher();
                st.ShowDialog();
                if (st.returned.Length > 0)
                {
                    if (st.returned == Classes.Class.GetInfo(classes[listBox1.SelectedIndex], "tid"))
                        Classes.App.Error("המורה שבחרת כבר מחנך את הכיתה הזו.");
                    else
                    {
                        Classes.SQL.Update("Classes", "`tid` = '" + st.returned + "'", "classid", classes[listBox1.SelectedIndex]);
                        UpdateClassProperties();
                        MessageBox.Show("מחנך הכיתה הוחלף בהצלחה.", "ניהול כיתות", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                st.Dispose();
            }
        }
        private void UpdateNewClassCode()
        {
            label12.Text = "(קוד: " + (textBox1.Text.Length > 0 ? Classes.Class.GetClassCode(Classes.Class.GetStartumNum(textBox1.Text), (int)numericUpDown1.Value) : "????") + ")";
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                bool flag = false;
                if (textBox1.Text.Length >= 1)
                {
                    if (textBox1.Text[0] < 'א' && textBox1.Text[0] > 'י')
                        flag = true;
                    else if (textBox1.Text.Length >= 2)
                        if ((textBox1.Text[1] < 'א' && textBox1.Text[1] > 'ב') || textBox1.Text[0] != 'י')
                            flag = true;
                }
                if (flag)
                {
                    textBox1.ResetText();
                    Classes.App.Error("בתיבה זו יש להקליד את אותיות השכבה, מהאות א' עד יב' בלבד.");
                }
                else
                    UpdateNewClassCode();
            }
            else
                UpdateNewClassCode();
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            UpdateNewClassCode();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
                Classes.App.Error("עליך לבחור מחנך לכיתה החדשה.");
            else if (Classes.Class.Exists(Classes.Class.GetClassCode(Classes.Class.GetStartumNum(textBox1.Text), (int)numericUpDown1.Value - 1)))
                Classes.App.Error("כיתה כזו כבר קיימת במערכת, נא לשנות את הפרטים.");
            else
            {
                Classes.Class.Add(Classes.Class.GetStartumNum(textBox1.Text), (int)numericUpDown1.Value, teachers[comboBox1.SelectedIndex]);
                ReloadClasses();
                MessageBox.Show("הכיתה נוספה בהצלחה.", "ניהול כיתות", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.ResetText();
            numericUpDown1.Value = 1;
            comboBox1.SelectedIndex = -1;
        }
        private void button18_Click(object sender, EventArgs e)
        {
            if (embedClasses.Count >= Classes.Class.MAX_EMBED_CLASSES)
                Classes.App.Error("לא ניתן להוסיף עוד כיתות. ניתן לשבץ רק ב-" + Classes.Class.MAX_EMBED_CLASSES + " כיתות בכל פעם.");
            else
            {
                SelectClass sc = new SelectClass(true);
                sc.ShowDialog();
                if (sc.clco.Length > 0)
                    if (!embedClasses.Contains(sc.clco))
                    {
                        embedClasses.Add(sc.clco);
                        listBox7.Items.Add(Classes.Class.ClassNameByCode(sc.clco));
                    }
                sc.Dispose();
            }
        }
        private void button20_Click(object sender, EventArgs e)
        {
            if (listBox7.SelectedItems.Count > 0)
            {
                List<int> toRemove = new List<int>();
                for (int i = listBox7.SelectedItems.Count - 1; i >= 0; i--)
                    toRemove.Add(listBox7.Items.IndexOf(listBox7.SelectedItems[i]));
                for (int i = 0; i < toRemove.Count; i++)
                {
                    embedClasses.RemoveAt(toRemove[i]);
                    listBox7.Items.RemoveAt(toRemove[i]);
                }
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            StudentsSelect ss = new StudentsSelect("שיבוץ בכיתות", 1);
            ss.ShowDialog();
            if (ss.returnedStudents != null)
                for (int i = 0; i < ss.returnedStudents.Count; i++)
                    if (!embedStudents.Contains(ss.returnedStudents[i]))
                    {
                        embedStudents.Add(ss.returnedStudents[i]);
                        listBox2.Items.Add(ss.returnedStudents[i]);
                    }
            ss.Dispose();
        }
        private void button19_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItems.Count > 0)
            {
                List<int> toRemove = new List<int>();
                for (int i = listBox2.SelectedItems.Count - 1; i >= 0; i--)
                    toRemove.Add(listBox2.Items.IndexOf(listBox2.SelectedItems[i]));
                for (int i = 0; i < toRemove.Count; i++)
                {
                    embedStudents.RemoveAt(toRemove[i]);
                    listBox2.Items.RemoveAt(toRemove[i]);
                }
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            listBox7.Items.Clear();
            embedClasses.Clear();
            embedStudents.Clear();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            new EmbeddingClasses(embedStudents, embedClasses).ShowDialog();
        }
        private void button17_Click(object sender, EventArgs e)
        {
            if (listBox4.SelectedIndex == -1)
                Classes.App.Error("עליך לבחור קבוצת לימוד מרשימת הקבוצות.");
            else
            {
                SelectTeacher st = new SelectTeacher();
                st.ShowDialog();
                if (st.returned.Length > 0)
                {
                    if (st.returned == Classes.Group.GetInfo(Convert.ToInt32(groups[listBox4.SelectedIndex])).Rows[0]["tid"].ToString())
                        Classes.App.Error("המורה שבחרת הוא כבר המורה של הקבוצה הזו.");
                    else
                    {
                        Classes.SQL.Update("Groups", "`tid` = '" + st.returned + "'", "gid", groups[listBox4.SelectedIndex]);
                        UpdateGroupProperties();
                        MessageBox.Show("מורה הקבוצה הוחלף בהצלחה.", "ניהול קבוצות לימוד", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                st.Dispose();
            }
        }
        private void button16_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                Classes.App.Error("עליך לבחור קבוצת לימוד מרשימת הקבוצות.");
            else
            {
                Classes.Group.Delete(groups[listBox4.SelectedIndex]);
                ReloadGroups();
                MessageBox.Show("הקבוצה נמחקה בהצלחה.", "ניהול כיתות", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateGroupProperties();
            listBox5.Items.Clear();
            if(listBox4.SelectedIndex != -1)
            {
                DataTable gstudents = Classes.Group.GetStudents(Convert.ToInt32(groups[listBox4.SelectedIndex]));
                for (int i = 0; i < gstudents.Rows.Count; i++)
                    listBox5.Items.Add(gstudents.Rows[i]["id"] + ": " + Classes.Student.GetName(gstudents.Rows[i]["id"].ToString()));
            }
        }
        private void button12_Click(object sender, EventArgs e)
        {
            if (listBox4.SelectedIndex != -1 && listBox3.SelectedIndex != -1 && listBox3.Items.Count > 0)
                if (!listBox5.Items.Contains(listBox3.SelectedItem.ToString()))
                {
                    listBox5.Items.Add(listBox3.SelectedItem.ToString());
                    Classes.Group.AddStudent(groups[listBox4.SelectedIndex], listBox3.SelectedItem.ToString().Split(':')[0]);
                    UpdateGroupProperties();
                }
        }
        private void button11_Click(object sender, EventArgs e)
        {
            if (listBox4.SelectedIndex != -1 && listBox5.SelectedIndex != -1 && listBox5.Items.Count > 0)
            {
                Classes.Group.DeleteStudent(groups[listBox4.SelectedIndex], listBox5.SelectedItem.ToString().Split(':')[0]);
                listBox5.Items.RemoveAt(listBox5.SelectedIndex);
                UpdateGroupProperties();
            }
        }
        private void button13_Click(object sender, EventArgs e)
        {
            if (listBox4.SelectedIndex != -1)
            {
                if (listBox5.Items.Count == 0)
                    Classes.App.Error("אפשרות זו משמשת להוצאת כל התלמידים מקבוצה מסויימת. בקבוצה שבחרת אין תלמידים.");
                else if (MessageBox.Show("האם אתה בטוח שברצונך להוציא את כל התלמידים מהקבוצה שבחרת?", "ניהול קבוצות", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Classes.Group.DeleteAllStudents(groups[listBox4.SelectedIndex]);
                    listBox5.Items.Clear();
                    UpdateGroupProperties();
                }
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == -1)
                Classes.App.Error("עליך לבחור מקצוע לקבוצת הלימוד החדשה.");
            else if (comboBox4.SelectedIndex == -1)
                Classes.App.Error("עליך לבחור שכבה לקבוצת הלימוד החדשה.");
            else if (comboBox5.SelectedIndex == -1)
                Classes.App.Error("עליך לבחור מורה לקבוצה החדשה.");
            else
            {
                int gid = Classes.Group.Add(subjects[comboBox3.SelectedIndex], Classes.Class.GetStartumNum(startums[comboBox4.SelectedIndex]).ToString(), teachers[comboBox5.SelectedIndex], textBox2.Text);
                ReloadGroups();
                MessageBox.Show("קבוצת לימוד חדשה נוספה בהצלחה, מספר הקבוצה: " + gid, "ניהול כיתות", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            comboBox5.SelectedIndex = -1;
            textBox2.ResetText();
        }
    }
}