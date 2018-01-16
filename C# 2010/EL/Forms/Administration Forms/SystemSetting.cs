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
    public partial class SystemSetting : UserControl
    {
        public SystemSetting()
        {
            InitializeComponent();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = checkBox1.Checked;
            label1.Enabled = checkBox1.Checked;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Classes.INI cfg = new Classes.INI(AppFile.GetFile(AppFile.CONFIG));
            cfg.SetKey("Off", checkBox1.Checked ? textBox1.Text : "?");
        }
    }
}