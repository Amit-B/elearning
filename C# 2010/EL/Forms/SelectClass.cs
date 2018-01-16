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
    public partial class SelectClass : Form
    {
        private DataTable classes;
        public string clco = string.Empty;
        private bool emptyallowed = false;
        public SelectClass(bool emptyallowed)
        {
            InitializeComponent();
            this.emptyallowed = emptyallowed;
        }
        private void SelectClass_Load(object sender, EventArgs e)
        {
            classes = Classes.Class.List();
            string toAdd = string.Empty;
            for (int i = 0; i < classes.Rows.Count; i++)
            {
                toAdd = string.Empty;
                if (Classes.Class.GetStudents(classes.Rows[i]["classid"].ToString()).Rows.Count == 0)
                {
                    toAdd = "* ";
                    if (!label2.Visible)
                        label2.Visible = true;
                }
                toAdd += Classes.Class.ClassNameByCode(classes.Rows[i]["classid"].ToString());
                listBox1.Items.Add(toAdd);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                Classes.App.Error("לא בחרת כיתה.");
            else if(Classes.Class.GetStudents(classes.Rows[listBox1.SelectedIndex]["classid"].ToString()).Rows.Count == 0 && !emptyallowed)
                Classes.App.Error("עליך לבחור כיתה לא ריקה.");
            else
            {
                clco = classes.Rows[listBox1.SelectedIndex]["classid"].ToString();
                this.Close();
            }
        }
    }
}