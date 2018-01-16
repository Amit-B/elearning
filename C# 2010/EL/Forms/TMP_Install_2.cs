using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace EL.Forms
{
    public partial class TMP_Install_2 : Form
    {
        private Main m;
        private TMP_Install previous;
        public bool allowClose = false;
        public TMP_Install_2(Main mainForm, TMP_Install prev)
        {
            InitializeComponent();
            m = mainForm;
            previous = prev;
            prev.Visible = false;
        }
        private void Install_2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!allowClose && (e.CloseReason == CloseReason.UserClosing || e.CloseReason == CloseReason.None || e.CloseReason == CloseReason.TaskManagerClosing))
            {
                e.Cancel = true;
                if (MessageBox.Show("האם אתה בטוח שאתה רוצה לצאת?\r\nיציאה תבטל את ההתקנה ויכולה לגרום לבעיות שימוש בתוכנה בעתיד.", "EL", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Application.Exit();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            previous.Close();
            this.allowClose = true;
            this.Visible = false;
            new Forms.TMP_Install_3(m, this).ShowDialog();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            previous.Visible = true;
            this.Visible = false;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Progress_Init(AppDirectory.LIST.Length + AppFile.LIST.Length);
                for (int i = 0; i < AppDirectory.LIST.Length; i++)
                {
                    textBox2.Text += "יוצר תיקיה: " + AppDirectory.GetDirectory(AppDirectory.LIST[i]);
                    Directory.CreateDirectory(AppDirectory.GetDirectory(AppDirectory.LIST[i]));
                    Progress_Add();
                    textBox2.Text += "נוצרה בהצלחה.";
                    textBox2.ScrollToCaret();
                }
                for (int i = 0; i < AppFile.LIST.Length; i++)
                {
                    textBox2.Text += "יוצר קובץ: " + AppFile.GetFile(AppFile.LIST[i]);
                    File.WriteAllText(AppFile.GetFile(AppFile.LIST[i]), AppFile.GetFileDefaultText(AppFile.LIST[i]));
                    Progress_Add();
                    textBox2.Text += "נוצר בהצלחה.";
                }
            }
            catch (Exception ex)
            {
                Classes.App.Error("בעיה בהפעלת חלק " + progressBar1.Value + " של ההתקנה:\r\n" + ex.Message, true);
            }
            finally
            {
                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = false;
                System.Media.SystemSounds.Exclamation.Play();
            }
        }
        private void Progress_Init(int max)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = max;
            progressBar1.Value = 0;
            label1.Text = "0% / 100%";
        }
        private void Progress_Add()
        {
            progressBar1.Value++;
            label1.Text = Convert.ToInt32(((progressBar1.Value * 100) / progressBar1.Maximum)) + "% / 100%";
            label1.Refresh();
        }
    }
}