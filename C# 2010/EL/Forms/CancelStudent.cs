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
    public partial class CancelStudent : Form
    {
        public CancelStudent(string id)
        {
            InitializeComponent();
        }
        public int accept = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            accept = checkBox1.Checked ? 2 : 1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            accept = 0;
        }
    }
}