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
    public partial class SettingGrades : Form
    {
        public List<string> grades = null;
        private string gid = string.Empty;
        public SettingGrades(List<string> grades, string gid)
        {
            InitializeComponent();
            this.grades = grades;
            this.gid = gid;
        }
        private void SettingGrades_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < grades.Count; i++)
                dataGridView1.Rows.Add(new object[]
                {
                    grades[i].Split('=')[0],
                    Classes.Student.GetName(grades[i].Split('=')[0]),
                    grades[i].Split('=')[1]
                });
            DataTable ginfo = Classes.Group.GetInfo(Convert.ToInt32(gid));
            textBox1.Text = textBox1.Text.Replace("A", ginfo.Rows[0]["gname"].ToString());
            textBox1.Text = textBox1.Text.Replace("B", Classes.Class.GetStartumChar(Convert.ToInt32(ginfo.Rows[0]["gclass"])));
            textBox1.Text = textBox1.Text.Replace("C", Classes.Subject.GetName(Convert.ToInt32(ginfo.Rows[0]["sid"])));
            textBox1.Text = textBox1.Text.Replace("D", Classes.Teacher.GetName(ginfo.Rows[0]["tid"].ToString()));
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("האם אתה בטוח שברצונך למחוק את כל הציונים?\r\nהדרך היחידה לשחזר אותם תהיה לחיצה על כפתור ביטול.", "הזנת ציונים", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    dataGridView1.Rows[i].Cells[2].Value = "?";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int amount = 0;
            for (int i = 0, j = 0; i < dataGridView1.Rows.Count; i++)
                if(Classes.Text.LanguageIs(dataGridView1.Rows[i].Cells[2].Value.ToString(), Classes.Text.Language.Numbers))
                {
                    j = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString());
                    if (j >= 0 && j <= 100)
                        amount += j;
                }
            MessageBox.Show("ממוצע: " + (double)(amount / dataGridView1.Rows.Count), "הזנת ציונים", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("האם אתה בטוח שברצונך לבטל את העריכה?\r\nפעולה זה תגרום לכל הנתונים שנערכו עד כה להמחק ולחזור למה שהיו לפניכן.", "הזנת ציונים", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.grades = null;
                this.Close();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            grades.Clear();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                grades.Add(dataGridView1.Rows[i].Cells[0].Value.ToString() + "=" + dataGridView1.Rows[i].Cells[2].Value.ToString());
            MessageBox.Show("הנתונים נשמרו בהצלחה.", "הזנת ציונים", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}