using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace EL
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        Classes.INI cfg = new Classes.INI(AppFile.GetFile(AppFile.CONFIG));
        private void Main_Load(object sender, EventArgs e)
        {
            Classes.SQL.Connect();
            string dispVer = string.Empty;
            for (int i = 0, c = 0; i < EL.Properties.Resources.Version.Length && dispVer.Length == 0; i++)
                if (EL.Properties.Resources.Version[i] == '.')
                    if (++c >= 2)
                        dispVer = EL.Properties.Resources.Version.Remove(i);
            label3.Text = label3.Text.Replace("X", dispVer);
            label1.Text = EL.Properties.Resources.Version;
            if (Directory.Exists(Classes.App.GetProgramDirectory()))
            {
                string lastid = cfg.GetKey("LastID");
                if (lastid != "?" && Classes.Text.IsIdentityNumber(lastid))
                {
                    textBox2.Text = lastid;
                    checkBox1.Checked = true;
                }
            }
            else
            {
                this.Hide();
                new Forms.TMP_Install(this).ShowDialog();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string tid = textBox2.Text.Length == 10 ? textBox2.Text.Remove(0, 1) : textBox2.Text;
            if (!Classes.Text.IsIdentityNumber(tid))
                Classes.App.Error("מספר תעודת זהות לא תקין.");
            else if (!Classes.Teacher.Exists(tid))
                Classes.App.Error("מספר תעודת זהות לא קיים במערכת.");
            else
            {
                if (!Convert.ToBoolean(Classes.Teacher.GetInfo(tid, "admin")) && cfg.GetKey("Off") != "?")
                    Classes.App.Error("המערכת כבויה כעת: " + cfg.GetKey("Off"));
                else
                {
                    if (checkBox1.Checked)
                        cfg.SetKey("LastID", textBox2.Text);
                    new Forms.Login(tid, this).ShowDialog();
                }
            }
        }
        private void יציאהToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) button2_Click(sender, e);
        }
        private void label3_Click(object sender, EventArgs e)
        {
            label1.Visible = !label1.Visible;
        }
    }
}