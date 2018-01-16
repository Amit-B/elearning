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
    public partial class Duties : UserControl
    {
        public Duties()
        {
            InitializeComponent();
        }
        private List<string> dutyids = new List<string>();
        private void Duties_Load(object sender, EventArgs e)
        {
            ReloadDuties();
        }
        private void ReloadDuties()
        {
            listBox1.Items.Clear();
            dutyids.Clear();
            DataTable ds = Classes.Duty.List();
            for (int i = 0; i < ds.Rows.Count; i++)
            {
                listBox1.Items.Add(ds.Rows[i]["dutyname"].ToString());
                dutyids.Add(ds.Rows[i]["dutyid"].ToString());
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                Classes.SQL.Query("SELECT * FROM `Teachers` WHERE `dutyid` = '" + dutyids[listBox1.SelectedIndex] + "'");
                label3.Text = "מספר בעלי תפקיד זה: " + Classes.SQL.ds.Tables[0].Rows.Count;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                Classes.App.Error("נא לבחור תפקיד.");
            else
            {
                Classes.SQL.Query("SELECT * FROM `Teachers` WHERE `dutyid` = '" + dutyids[listBox1.SelectedIndex] + "'");
                string teachers = string.Empty;
                for (int i = 0; i < Classes.SQL.ds.Tables[0].Rows.Count; i++)
                    Classes.Text.WriteLine(ref teachers, Classes.SQL.ds.Tables[0].Rows[i]["tid"] + " - " + Classes.SQL.ds.Tables[0].Rows[i]["firstname"] + " " + Classes.SQL.ds.Tables[0].Rows[i]["lastname"]);
                MessageBox.Show("רשימת בעלי התפקיד שבחרת:\r\n\r\n" + teachers, "ניהול תפקידים", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                Classes.App.Error("נא לבחור תפקיד.");
            else
            {
                Classes.SQL.Query("SELECT * FROM `Teachers` WHERE `active` = " + true + " AND `dutyid` = '" + dutyids[listBox1.SelectedIndex] + "'");
                string warning = "האם אתה בטוח שברצונך למחוק את התפקיד הזה?";
                if (Classes.SQL.ds.Tables[0].Rows.Count > 0)
                    warning += "\r\nכל המורים שמשוייכים אליו (" + Classes.SQL.ds.Tables[0].Rows.Count + ") יצויינו כחסרי תפקיד.";
                if (MessageBox.Show(warning, "ניהול תפקידים", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (Classes.SQL.ds.Tables[0].Rows.Count > 0)
                        Classes.SQL.Update("Teachers", "`dutyid` = '0'", "dutyid", dutyids[listBox1.SelectedIndex]);
                    Classes.Duty.Delete(dutyids[listBox1.SelectedIndex]);
                    MessageBox.Show("התפקיד נמחק בהצלחה.", "ניהול תפקידים", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReloadDuties();
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                Classes.App.Error("נא לבחור תפקיד.");
            else
            {
                Input i = new Input("שינוי שם לתפקיד: " + Classes.Duty.GetName(dutyids[listBox1.SelectedIndex]), "נא להקליד כאן את שם התפקיד החדש:");
                i.ShowDialog();
                if (i.returned.Length > 0)
                {
                    Classes.Duty.SetName(dutyids[listBox1.SelectedIndex], i.returned);
                    MessageBox.Show("שם התפקיד נערך בהצלחה.", "ניהול תפקידים", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReloadDuties();
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            Input i = new Input("הוספת תפקיד חדש", "נא להקליד כאן את שם התפקיד החדש:");
            i.ShowDialog();
            if (i.returned.Length > 0)
            {
                Classes.Duty.Add(i.returned);
                MessageBox.Show("התפקיד החדש נוסף בהצלחה.", "ניהול תפקידים", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReloadDuties();
            }
        }
    }
}