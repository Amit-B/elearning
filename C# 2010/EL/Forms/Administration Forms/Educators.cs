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
    public partial class Educators : UserControl
    {
        public Educators()
        {
            InitializeComponent();
        }
        private List<string> classes = new List<string>(), teachers = new List<string>();
        private void Educators_Load(object sender, EventArgs e)
        {
            DataTable list = Classes.Class.List();
            for (int i = 0; i < list.Rows.Count; i++)
            {
                classes.Add(list.Rows[i]["classid"].ToString());
                listBox1.Items.Add(Classes.Class.ClassNameByCode(list.Rows[i]["classid"].ToString()));
            }
            list = Classes.Teacher.List();
            for (int i = 0; i < list.Rows.Count; i++)
            {
                comboBox1.Items.Add(list.Rows[i]["tid"] + " - " + list.Rows[i]["firstname"] + " " + list.Rows[i]["lastname"]);
                teachers.Add(list.Rows[i]["tid"].ToString());
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                Classes.App.Error("נא לבחור כיתה.");
            else
            {
                string edu = Classes.Class.GetInfo(classes[listBox1.SelectedIndex], "tid");
                comboBox1.SelectedIndex = -1;
                for (int i = 0; i < teachers.Count && comboBox1.SelectedIndex == -1; i++)
                    if (teachers[i] == edu)
                        comboBox1.SelectedIndex = i;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                Classes.App.Error("נא לבחור כיתה.");
            else if (comboBox1.SelectedIndex == -1)
                Classes.App.Error("נא לבחור מחנך חדש.");
            else
            {
                string edu = Classes.Class.GetInfo(classes[listBox1.SelectedIndex], "tid");
                if (edu == teachers[comboBox1.SelectedIndex])
                    Classes.App.Error("המחנך החדש שבחרת הוא אותו מחנך קיים.");
                else
                {
                    Classes.SQL.Update("Classes", "`tid` = '" + teachers[comboBox1.SelectedIndex] + "'", "classid", classes[listBox1.SelectedIndex]);
                    MessageBox.Show("מחנך הכיתה נערך בהצלחה.", "מחנכי הכיתות", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}