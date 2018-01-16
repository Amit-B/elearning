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
    public partial class InfoFile : Form
    {
        public InfoFile(string header, string text)
        {
            InitializeComponent();
            label1.Text = header;
            this.Text = "קובץ מידע: " + header;
            textBox1.Text = text;
        }
        private void button12_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}