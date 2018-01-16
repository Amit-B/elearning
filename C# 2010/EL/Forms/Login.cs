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
    public partial class Login : Form
    {
        private string code = string.Empty;
        private string tid = "N";
        private Main m;
        public Login(string id, Main me)
        {
            InitializeComponent();
            tid = id;
            m = me;
        }
        private void Login_Load(object sender, EventArgs e)
        {
            code = Classes.Text.GenerateCodePanel(
                new Label[] { label2, label3, label4, label5, label6, label7, label8, label9 },
                new int[] { 3, 44, 85, 126, 167, 208, 249, 290 },
                panel1);
            string name = Classes.Teacher.GetName(tid);
            textBox1.Text = textBox1.Text.Replace("{X}", name);
            textBox1.Text = textBox1.Text.Replace("{N}", tid);
            Classes.Net.TTS("Hello, " + Classes.Text.ToReadableText(name) + ". Please enter your password.");
        }
        private void button3_Click(object sender, EventArgs e)
        {
            code = Classes.Text.GenerateCodePanel(
                new Label[] { label2, label3, label4, label5, label6, label7, label8, label9 },
                new int[] { 3, 44, 85, 126, 167, 208, 249, 290 },
                panel1);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            HideBoth();
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            HideBoth();
            string written = textBox3.Text;
            for (int i = 0; i < written.Length; i++)
                if (written[i] >= 'a' && written[i] <= 'z')
                    written = written.Replace(written[i].ToString(), ((char)((int)written[i] - 32)).ToString());
            if (code != written)
            {
                timer1.Start();
                label11.Visible = true;
                Classes.Net.TTS("Wrong security code!");
            }
            else if (Classes.Teacher.GetInfo(tid, "password") != textBox2.Text)
            {
                timer2.Start();
                label12.Visible = true;
                Classes.Net.TTS("Wrong password!");
            }
            else if (!label11.Visible && !label12.Visible)
            {
                Classes.Net.TTS("Welcome!");
                this.Visible = false;
                m.Visible = false;
                this.Hide();
                m.Hide();
                CurrentUser.ID = this.tid;
                CurrentUser.Administrator = Convert.ToBoolean(Classes.Teacher.GetInfo(tid, "admin"));
                new TeacherPanel(m).Show();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label11.Visible = false;
            timer1.Stop();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            label12.Visible = false;
            timer2.Stop();
        }
        private void HideBoth()
        {
            if (label11.Visible)
            {
                timer1.Stop();
                label11.Visible = false;
            }
            if (label12.Visible)
            {
                timer2.Stop();
                label12.Visible = false;
            }
        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) button2_Click(sender, e);
        }
        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter) button2_Click(sender, e);
        }
    }
}