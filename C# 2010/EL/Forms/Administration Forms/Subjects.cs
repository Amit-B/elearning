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
    public partial class Subjects : UserControl
    {
        public Subjects()
        {
            InitializeComponent();
        }
        private List<string> subs = new List<string>();
        private void ReloadSubjects()
        {
            listBox1.Items.Clear();
            subs.Clear();
            DataTable ds = Classes.Subject.List();
            for (int i = 0; i < ds.Rows.Count; i++)
            {
                listBox1.Items.Add(ds.Rows[i]["stitle"].ToString());
                subs.Add(ds.Rows[i]["sid"].ToString());
            }
        }
        private void Subjects_Load(object sender, EventArgs e)
        {
            ReloadSubjects();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Input i = new Input("הוספת מקצוע", "נא להקליד כאן את שם המקצוע:");
            i.ShowDialog();
            if (i.returned.Length > 0)
            {
                Classes.Subject.Add(i.returned);
                ReloadSubjects();
                MessageBox.Show("המקצוע נוסף בהצלחה.", "ניהול מקצועות", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                Classes.Subject.Remove(Convert.ToInt32(subs[listBox1.SelectedIndex]));
                ReloadSubjects();
                MessageBox.Show("המקצוע נמחק בהצלחה.", "ניהול מקצועות", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Input i = new Input("עריכת מקצוע", "נא להקליד כאן את שם המקצוע:");
            i.ShowDialog();
            if (i.returned.Length > 0)
            {
                Classes.Subject.Rename(Convert.ToInt32(subs[listBox1.SelectedIndex]), i.returned);
                ReloadSubjects();
                MessageBox.Show("המקצוע נערך בהצלחה.", "ניהול מקצועות", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
