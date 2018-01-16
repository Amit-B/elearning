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
    public partial class Input : Form
    {
        public Input(string header, string label)
        {
            InitializeComponent();
            this.Text = header;
            textBox2.Text = label;
        }
        public string returned = string.Empty;
        private void button1_Click(object sender, EventArgs e)
        {
            returned = textBox1.Text;
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}