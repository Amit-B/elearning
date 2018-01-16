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
    public partial class TextEditor : Form
    {
        public TextEditor(string documentName, string text)
        {
            InitializeComponent();
            label2.Text = documentName;
            this.Text = documentName + " - עריכת טקסט";
            richTextBox1.Text = text;
        }
        public string returned = string.Empty;
        private void button1_Click(object sender, EventArgs e)
        {
            returned = richTextBox1.Text;
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (label3.Visible)
                label3.Text = richTextBox1.Text.Length + " תוים";
            if (label4.Visible)
                label4.Text = richTextBox1.Lines.Length + " שורות";
        }
        private void שמירהToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "שמירה...";
            if (sfd.ShowDialog() == DialogResult.OK)
                File.WriteAllText(sfd.FileName, richTextBox1.Text);
        }
        private void פתיחהToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "פתיחה...";
            if (ofd.ShowDialog() == DialogResult.OK)
                richTextBox1.Text = File.ReadAllText(ofd.FileName);
        }
        private void סיוםעריכהToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }
        private void ביטולעריכהToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }
        private void איפוסToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ResetText();
        }
        private void בחרהכלToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }
        private void בטלToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }
        private void בצעשובToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }
        private void גזורToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }
        private void העתקToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }
        private void הדבקToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }
        private void גלישתשורותToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.WordWrap = גלישתשורותToolStripMenuItem.Checked;
        }
        private void מספרתויםToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label3.Visible = מספרתויםToolStripMenuItem.Checked;
        }
        private void מספרשורותToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label4.Visible = מספרשורותToolStripMenuItem.Checked;
        }
    }
}