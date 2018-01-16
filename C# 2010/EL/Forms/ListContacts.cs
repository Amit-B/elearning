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
    public partial class ListContacts : Form
    {
        public ListContacts(List<string> listemall)
        {
            InitializeComponent();
            for (int i = 0; i < listemall.Count; i++)
                listBox1.Items.Add((i + 1) + ") " + (listemall[i].Split('-')[1] == "1" ? "מורה" : "תלמיד") + ": " + listemall[i].Split('-')[0] + " - " + (listemall[i].Split('-')[1] == "1" ? Classes.Teacher.GetName(listemall[i].Split('-')[0]) : Classes.Student.GetName(listemall[i].Split('-')[0])));
        }
        private void button12_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}