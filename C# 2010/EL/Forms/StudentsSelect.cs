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
    public partial class StudentsSelect : Form
    {
        public List<string> returnedStudents = null;
        private List<string> startums = null;
        private int returnType = 0;
        public StudentsSelect(string header, int returnType = 0)
        {
            InitializeComponent();
            this.Text += header;
            this.returnType = returnType;
        }
        private void StudentsSelect_Load(object sender, EventArgs e)
        {
            startums = Classes.Class.GetStartums();
            for (int i = 0; i < startums.Count; i++)
                comboBox1.Items.Add(startums[i]);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            returnedStudents = new List<string>();
            for (int i = 0; i < listBox1.Items.Count; i++)
                returnedStudents.Add(this.returnType == 0 ? listBox1.Items[i].ToString().Split(':')[0] : listBox1.Items[i].ToString());
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                if (!Classes.Text.IsIdentityNumber(textBox1.Text))
                    Classes.App.Error("מספר תעודת הזהות שהוקלד אינו תקין.");
                else if (!Classes.Student.Exists(textBox1.Text))
                    Classes.App.Error("מספר תעודת הזהות שהוקלד לא נמצא ברשימת התלמידים.");
                else if (ItemExists(textBox1.Text))
                    Classes.App.Error("תלמיד זה כבר קיים.");
                else
                {
                    listBox1.Items.Add(textBox1.Text + ": " + Classes.Student.GetName(textBox1.Text));
                    MessageBox.Show("נוסף בהצלחה תלמיד אחד:\r\n" + Classes.Student.GetName(textBox1.Text), "הוספת תלמידים", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (radioButton2.Checked)
            {
                if (!Classes.Text.LanguageIs(textBox2.Text, Classes.Text.Language.Numbers))
                    Classes.App.Error("יש להקליד שנת לידה במספרים בלבד.");
                else if (!Classes.Text.IsValidYear(Convert.ToInt32(textBox2.Text)))
                    Classes.App.Error("השנה שהוקלדה אינה תקינה.");
                else
                {
                    DataTable students = Classes.Student.List();
                    DateTime dt;
                    int[] c = { 0, 0 };
                    for (int i = 0; i < students.Rows.Count; i++)
                    {
                        dt = Convert.ToDateTime(students.Rows[i]["regdate"]);
                        if (dt.Year == Convert.ToInt32(textBox2.Text))
                        {
                            if (ItemExists(students.Rows[i]["id"].ToString()))
                                c[1]++;
                            else
                            {
                                listBox1.Items.Add(students.Rows[i]["id"] + ": " + students.Rows[i]["firstname"] + " " + students.Rows[i]["lastname"]);
                                c[0]++;
                            }
                        }
                    }
                    MessageBox.Show(c[1] == 0 ? ("נוספו בהצלחה " + c[0] + " תלמידים.") : ("נוספו בהצלחה " + c[0] + " תלמידים, כאשר " + c[1] + " נוספים היו כבר ברשימה."), "הוספת תלמידים", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (radioButton3.Checked)
            {
                if (comboBox1.SelectedIndex == -1)
                    Classes.App.Error("עליך לבחור שכבת לימוד.");
                else
                {
                    DataTable classes = Classes.Class.List(startum: startums[comboBox1.SelectedIndex]), students = null;
                    int[] c = { 0, 0 };
                    for (int i = 0; i < classes.Rows.Count; i++)
                    {
                        students = Classes.Class.GetStudents(classes.Rows[i]["classid"].ToString());
                        for (int j = 0; j < students.Rows.Count; j++)
                            if (ItemExists(students.Rows[j]["id"].ToString()))
                                c[1]++;
                            else
                            {
                                listBox1.Items.Add(students.Rows[j]["id"] + ": " + students.Rows[j]["firstname"] + " " + students.Rows[j]["lastname"]);
                                c[0]++;
                            }
                    }
                    MessageBox.Show(c[1] == 0 ? ("נוספו בהצלחה " + c[0] + " תלמידים.") : ("נוספו בהצלחה " + c[0] + " תלמידים, כאשר " + c[1] + " נוספים היו כבר ברשימה."), "הוספת תלמידים", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private bool ItemExists(string id)
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
                if (listBox1.Items[i].ToString().StartsWith(id))
                    return true;
            return false;
        }
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex != -1)
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }
    }
}