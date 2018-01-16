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
    public partial class InfoFiles : UserControl
    {
        public InfoFiles()
        {
            InitializeComponent();
        }
        private List<string> files = new List<string>();
        private void InfoFiles_Load(object sender, EventArgs e)
        {
            ReloadFiles();
        }
        private void ReloadFiles()
        {
            string[] list = Directory.GetFiles(AppDirectory.GetDirectory(AppDirectory.INFO));
            FileInfo tmp;
            for (int i = 0; i < list.Length; i++)
            {
                tmp = new FileInfo(list[i]);
                listBox1.Items.Add(tmp.Name);
                files.Add(list[i]);
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                string[] lines = File.ReadAllLines(files[listBox1.SelectedIndex]);
                for (int i = 0; i < 5 && i < lines.Length; i++)
                {
                    textBox1.Text += lines[i] + "\r\n";
                    if (i == 4)
                        textBox1.Text += "...";
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                Classes.App.Error("נא לבחור קובץ.");
            else
            {
                TextEditor te = new TextEditor("קובץ מידע קיים", textBox1.Text);
                te.ShowDialog();
                if (te.returned.Length > 0)
                    textBox1.Text = te.returned;
                te.Dispose();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                Classes.App.Error("נא לבחור קובץ.");
            else
                new InfoFile(listBox1.SelectedItem.ToString(), File.ReadAllText(files[listBox1.SelectedIndex])).ShowDialog();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                Classes.App.Error("נא לבחור קובץ.");
            else if(MessageBox.Show("האם אתה בטוח שברצונך למחוק את קובץ המידע?","קבצי מידע",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                File.Delete(files[listBox1.SelectedIndex]);
                /*files.RemoveAt(listBox1.SelectedIndex);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);*/
                ReloadFiles();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Input i = new Input("קובץ מידע חדש", "נא להקליד כאן את שם קובץ המידע החדש:");
            i.ShowDialog();
            if (i.returned.Length > 0)
            {
                TextEditor te = new TextEditor("קובץ מידע חדש", "");
                te.ShowDialog();
                if (te.returned.Length > 0)
                    File.WriteAllText(AppDirectory.GetDirectory(AppDirectory.INFO) + "/" + i.returned + ".txt", te.returned);
                ReloadFiles();
            }
        }
    }
}