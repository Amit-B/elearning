using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace EL.Forms.Administration_Forms
{
    public partial class EditMOTD : UserControl
    {
        public EditMOTD()
        {
            InitializeComponent();
        }
        private void EditMOTD_Load(object sender, EventArgs e)
        {
            textBox1.Text = File.ReadAllText(AppFile.GetFile(AppFile.MARQUEE));
        }
        private void button8_Click(object sender, EventArgs e)
        {
            File.WriteAllText(AppFile.GetFile(AppFile.MARQUEE), textBox1.Text);
            MessageBox.Show("ההודעה עודכנה.", "הודעה מהירה", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.ResetText();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            TextEditor te = new TextEditor("הערה מהירה", textBox1.Text);
            te.ShowDialog();
            if (te.returned.Length > 0)
                textBox1.Text = te.returned;
            te.Dispose();
        }
    }
}