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
    public partial class TeacherPanel : Form
    {
        private Main toShow;
        public TeacherPanel(Main m)
        {
            InitializeComponent();
            toShow = m;
        }
        private void TeacherPanel_Load(object sender, EventArgs e)
        {
            timer1.Start();
            Classes.INI cfg = new Classes.INI(AppFile.GetFile(AppFile.CONFIG));
            Button[] bs = { button1, button2, button3, button4, button5, button6, button7, button9 };
            if (!CurrentUser.Administrator)
            {
                for (int i = 0; i < bs.Length; i++)
                    bs[i].Enabled = cfg.GetKey("Permission" + (i + 1)) == "0";
                if (!button9.Enabled)
                    button9.Visible = false;
            }
            else
                button9.Visible = true;
            label1.Text = Classes.Teacher.GetName(CurrentUser.ID) + " (ת.ז: " + CurrentUser.ID + ")" + (CurrentUser.Administrator ? " + כולל הרשאת ניהול" : "");
            textBox1.Text = File.ReadAllText(AppFile.GetFile(AppFile.MARQUEE), Encoding.UTF8);
        }
        private void button10_Click(object sender, EventArgs e)
        {
            this.Close();
            toShow.Show();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Location = textBox1.Location.Y >= 346 ? new Point(16, 46) : new Point(textBox1.Location.X, textBox1.Location.Y + 1);
            textBox1.Size = textBox1.Location.Y >= 346 ? new Size(278, 296) : new Size(textBox1.Size.Width, textBox1.Size.Height - 1);
        }
        private void TeacherPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            toShow.Show();
        }
        private void button_Click(object sender, EventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case "button1":
                    {
                        new Students().ShowDialog();
                        break;
                    }
                case "button2":
                    {
                        new Lessons().ShowDialog();
                        break;
                    }
                case "button3":
                    {
                        new Notes().ShowDialog();
                        break;
                    }
                case "button4":
                    {
                        new Grades().ShowDialog();
                        break;
                    }
                case "button5":
                    {
                        new Printer().ShowDialog();
                        break;
                    }
                case "button6":
                    {
                        new GroupEditor().ShowDialog();
                        break;
                    }
                case "button7":
                    {
                        new PrivatePanel().ShowDialog();
                        break;
                    }
                case "button9":
                    {
                        new Administration().ShowDialog();
                        break;
                    }
                case "button10":
                    {
                        this.Close();
                        break;
                    }
            }
        }
    }
}