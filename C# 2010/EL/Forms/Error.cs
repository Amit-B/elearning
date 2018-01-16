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
    public partial class Error : Form
    {
        public Error(string error, bool abort = true)
        {
            InitializeComponent();
            if (!abort)
            {
                button1.Visible = false;
                button2.Visible = true;
                label1.Visible = false;
                label2.Visible = false;
                button3.Visible = false;
            }
            textBox1.Text = error;
        }
        bool dc = false;
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            dc = true;
            this.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            bool success = Classes.Net.SendMail("amit_barami@walla.com", "EL Error", textBox1.Text);
            MessageBox.Show(success ? "הדיווח נשלח בהצלחה!" : "שליחת הדיווח נכשלה.", "EL", MessageBoxButtons.OK, success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }
        private void Error_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(!dc) Application.Exit();
        }
    }
}