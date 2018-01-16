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
    public partial class SelectSchool : Form
    {
        public SelectSchool()
        {
            InitializeComponent();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < comboBox1.Items.Count; i++) comboBox1.Items[i] = "A";
        }
    }
}