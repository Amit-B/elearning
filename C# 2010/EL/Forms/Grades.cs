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
    public partial class Grades : Form
    {
        public Grades()
        {
            InitializeComponent();
        }
        private List<string> groups = new List<string>(), groups2 = new List<string>(), startums = null,
            gradetypes = new List<string>(), teachers = new List<string>(), newgrades = new List<string>();
        private int viewingGradeList = -1;
        private void Grades_Load(object sender, EventArgs e)
        {
            startums = Classes.Class.GetStartums();
            for (int i = 0; i < startums.Count; i++)
            {
                comboBox7.Items.Add(startums[i]);
                comboBox2.Items.Add(startums[i]);
            }
            DataTable teacherlist = Classes.Teacher.List();
            for (int i = 0; i < teacherlist.Rows.Count; i++)
            {
                comboBox4.Items.Add(teacherlist.Rows[i]["firstname"].ToString() + " " + teacherlist.Rows[i]["lastname"].ToString());
                teachers.Add(teacherlist.Rows[i]["tid"].ToString());
            }
            DataTable gt = Classes.Grades.ListGradeTypes();
            for (int i = 0; i < gt.Rows.Count; i++)
            {
                comboBox3.Items.Add(gt.Rows[i]["gtname"].ToString());
                comboBox6.Items.Add(gt.Rows[i]["gtname"].ToString());
                gradetypes.Add(gt.Rows[i]["gtid"].ToString());
            }
            ReloadGradeLists();
            SettingGrades("Reset");
        }
        private void ReloadGradeLists()
        {
            string query = "SELECT * FROM `GradeLists`";
            if (comboBox1.SelectedIndex != -1)
                query += (query.EndsWith("`") ? " WHERE " : " AND ") + "`gid` = '" + groups[comboBox1.SelectedIndex] + "'";
            if (comboBox3.SelectedIndex != -1)
                query += (query.EndsWith("`") ? " WHERE " : " AND ") + "`gtid` = '" + gradetypes[comboBox3.SelectedIndex] + "'";
            if (comboBox4.SelectedIndex != -1)
                query += (query.EndsWith("`") ? " WHERE " : " AND ") + "`tid` = '" + teachers[comboBox4.SelectedIndex] + "'";
            Classes.SQL.Query(query);
            dataGridView1.DataSource = Classes.SQL.ds.Tables[0]; //Classes.Grades.GetGradeLists();
            label4.Visible = dataGridView1.Rows.Count == 0;
            dataGridView1.Visible = dataGridView1.Rows.Count > 0;
            Classes.App.GenerateDataGridViewStyle(dataGridView1, Classes.App.DataGridStyles.GradeLists);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("האם אתה בטוח שברצונך למחוק את רשימת הציונים?\r\nכל הציונים והפרטים שנמצאים בה ימחקו לצמיתות.", "רשימות ציונים", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (viewingGradeList == Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value))
                    SettingGrades("Reset");
                Classes.Grades.DeleteGradeList(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ReloadGradeLists();
                MessageBox.Show("רשימת הציונים נמחקה בהצלחה.", "רשימות ציונים", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
                if (dataGridView1.SelectedRows[0].Index != -1)
                {
                    SettingGrades("Select", Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value));
                    SettingGrades("Focus");
                }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            // תקציר
        }
        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            comboBox1.Items.Clear();
            groups.Clear();
            DataTable grouplist = Classes.Group.List();
            for (int i = 0; i < grouplist.Rows.Count; i++)
                if (Classes.Class.GetStartumChar(Convert.ToInt32(grouplist.Rows[i]["gclass"])) == startums[comboBox7.SelectedIndex])
                {
                    comboBox1.Items.Add(grouplist.Rows[i]["gname"].ToString());
                    groups.Add(grouplist.Rows[i]["gid"].ToString());
                }
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex != -1)
            {
                comboBox5.SelectedIndex = -1;
                comboBox5.Items.Clear();
                groups2.Clear();
                DataTable grouplist = Classes.Group.List();
                for (int i = 0; i < grouplist.Rows.Count; i++)
                    if (Classes.Class.GetStartumChar(Convert.ToInt32(grouplist.Rows[i]["gclass"])) == startums[comboBox2.SelectedIndex])
                    {
                        comboBox5.Items.Add(grouplist.Rows[i]["gname"].ToString());
                        groups2.Add(grouplist.Rows[i]["gid"].ToString());
                    }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadGradeLists();
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadGradeLists();
        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadGradeLists();
        }
        private void SettingGrades(string act, int param = 0)
        {
            switch (act)
            {
                case "Focus":
                    tabControl1.SelectedTab = tabPage2; break;
                case "Select":
                    {
                        viewingGradeList = param;
                        label6.Text = "רשימת ציונים מספר #" + param;
                        if (!tableLayoutPanel1.Visible)
                            tableLayoutPanel1.Visible = true;
                        DataTable list = Classes.Grades.GetGradeListsBy("glid", param.ToString());
                        label8.Text = Classes.Teacher.GetName(list.Rows[0]["tid"].ToString()) + " (" + list.Rows[0]["tid"].ToString() + ")";
                        int flag = -1;
                        for (int i = 0; i < comboBox2.Items.Count && flag == -1; i++)
                            if (startums[i] == Classes.Class.GetStartumChar(Convert.ToInt32(Classes.Group.GetInfo(Convert.ToInt32(list.Rows[0]["gid"])).Rows[0]["gclass"])))
                                flag = i;
                        comboBox2.SelectedIndex = flag;
                        flag = -1;
                        for (int i = 0; i < comboBox5.Items.Count && flag == -1; i++)
                            if (groups2[i] == list.Rows[0]["gid"].ToString())
                                flag = i;
                        comboBox5.SelectedIndex = flag;
                        label12.Text = Classes.Subject.GetName(Classes.Group.GetSubject(Convert.ToInt32(groups2[flag])));
                        DateTime dt = Convert.ToDateTime(list.Rows[0]["gldt"]);
                        dateTimePicker4.Value = dt;
                        numericUpDown1.Value = dt.Hour;
                        comboBox6.SelectedIndex = Convert.ToInt32(list.Rows[0]["gtid"]);
                        textBox1.Text = list.Rows[0]["gltitle"].ToString();
                        int count = Classes.Grades.CountGradeList(param.ToString());
                        label16.Text = count == 0 ? "- רשימת הציונים ריקה, לחץ כדי להזין ציונים" : ("- רשימת הציונים מכילה " + count + " ציונים כרגע");
                        label16.Visible = true;
                        label19.Visible = false;
                        label20.Text = "- חשוב! לא לשכוח לשמור את רשימת הציונים";
                        label20.Visible = true;
                        button7.Enabled = true;
                        button8.Enabled = true;
                        button6.Enabled = false;
                        newgrades.Clear();
                        DataTable curgrades = Classes.Grades.GetGrades(param.ToString());
                        for (int i = 0; i < curgrades.Rows.Count; i++)
                            newgrades.Add(curgrades.Rows[i]["id"].ToString() + "=" + curgrades.Rows[i]["grade"].ToString());
                        break;
                    }
                case "New":
                    {
                        viewingGradeList = -2;
                        label6.Text = "רשימת ציונים חדשה";
                        if (!tableLayoutPanel1.Visible)
                            tableLayoutPanel1.Visible = true;
                        label8.Text = Classes.Teacher.GetName(CurrentUser.ID) + " (" + CurrentUser.ID + ")";
                        comboBox2.SelectedIndex = -1;
                        comboBox5.SelectedIndex = -1;
                        label2.Text = "נא לבחור קבוצת לימוד...";
                        dateTimePicker4.Value = DateTime.Today;
                        numericUpDown1.Value = 1;
                        comboBox6.SelectedIndex = -1;
                        textBox1.ResetText();
                        label16.Text = "- רשימת הציונים ריקה, לחץ כדי להזין ציונים";
                        label16.Visible = true;
                        label19.Text = "- לחץ כדי לבטל את יצירת הרשימה החדשה";
                        label19.Visible = true;
                        label20.Text = "- חשוב! לא לשכוח לשמור את רשימת הציונים החדשה";
                        label20.Visible = true;
                        button7.Enabled = true;
                        button8.Enabled = true;
                        button6.Enabled = false;
                        newgrades.Clear();
                        break;
                    }
                case "Reset":
                    {
                        viewingGradeList = -1;
                        label6.Text = "לא מוצגת שום רשימת ציונים. בחר רשימה מהטאב \"רשימות ציונים\" או התחל רשימה חדשה.";
                        tableLayoutPanel1.Visible = false;
                        label16.Visible = false;
                        label19.Visible = false;
                        label20.Visible = false;
                        button7.Enabled = false;
                        button8.Enabled = false;
                        button6.Enabled = true;
                        newgrades.Clear();
                        break;
                    }
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            SettingGrades("New");
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox5.SelectedIndex == -1)
                Classes.App.Error("עליך לבחור קבוצת לימוד.");
            else
            {
                if (viewingGradeList == -2)
                {
                    DataTable students = Classes.Group.GetStudents(Convert.ToInt32(groups2[comboBox5.SelectedIndex]));
                    newgrades.Clear();
                    for (int i = 0; i < students.Rows.Count; i++)
                        newgrades.Add(students.Rows[i]["id"].ToString() + "=?");
                }
                SettingGrades sg = new SettingGrades(newgrades, groups2[comboBox5.SelectedIndex]);
                sg.ShowDialog();
                if (sg.grades != null)
                {
                    newgrades = sg.grades;
                    label16.Text = newgrades.Count == 0 ? "- רשימת הציונים ריקה, לחץ כדי להזין ציונים" : ("- רשימת הציונים מכילה " + newgrades.Count + " ציונים כרגע");
                }
                sg.Dispose();
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(viewingGradeList == -2 ? ("האם אתה בטוח שברצונך לשמור את הרשימה כעת?") : ("האם אתה בטוח שאתה רוצה לשמור את העריכה ברשימת ציונים זו?"), "הזנת ציונים", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (comboBox2.SelectedIndex == -1 || comboBox5.SelectedIndex == -1 || comboBox6.SelectedIndex == -1)
                    Classes.App.Error("נא למלאות את כל הפרטים.");
                else
                {
                    if (viewingGradeList == -2)
                    {
                        int id = Classes.Grades.AddGradeList(gradetypes[comboBox6.SelectedIndex], CurrentUser.ID, new DateTime(dateTimePicker4.Value.Year, dateTimePicker4.Value.Month, dateTimePicker4.Value.Day, (int)numericUpDown1.Value, 0, 0).ToString(), textBox1.Text, groups2[comboBox5.SelectedIndex]);
                        for (int i = 0; i < newgrades.Count; i++)
                            Classes.Grades.AddGrade(newgrades[i].Split('=')[0], id.ToString(), Convert.ToInt32(newgrades[i].Split('=')[1]));
                        MessageBox.Show("רשימת הציונים נוספה בהצלחה למאגר (רשימה מספר " + id + ").", "הזנת ציונים", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Classes.Grades.EditGradeList(viewingGradeList.ToString(), new DateTime(dateTimePicker4.Value.Year, dateTimePicker4.Value.Month, dateTimePicker4.Value.Day, (int)numericUpDown1.Value, 0, 0).ToString(), textBox1.Text, groups2[comboBox5.SelectedIndex], gradetypes[comboBox6.SelectedIndex]);
                        for (int i = 0; i < newgrades.Count; i++)
                            Classes.Grades.SetGrade(newgrades[i].Split('=')[0], viewingGradeList.ToString(), Convert.ToInt32(newgrades[i].Split('=')[1]));
                        MessageBox.Show("רשימת הציונים נערכה ונשמרה בהצלחה (רשימה מספר " + viewingGradeList + ")", "הזנת ציונים", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    ReloadGradeLists();
                    SettingGrades("Reset");
                }
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            SettingGrades("Reset");
        }
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox5.SelectedIndex != -1)
                label12.Text = Classes.Subject.GetName(Classes.Group.GetSubject(Convert.ToInt32(groups2[comboBox5.SelectedIndex])));
        }
    }
}