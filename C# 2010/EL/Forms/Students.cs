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
    public partial class Students : Form
    {
        public Students()
        {
            InitializeComponent();
        }
        private string clco = string.Empty;
        private string clco2 = string.Empty;
        private void Students_Load(object sender, EventArgs e)
        {
            ReloadList(Classes.Student.List());
        }
        private void ReloadList(object ds)
        {
            dataGridView1.DataSource = ds;
            label3.Visible = dataGridView1.Rows.Count == 0;
            dataGridView1.Visible = dataGridView1.Rows.Count > 0;
            Classes.App.GenerateDataGridViewStyle(dataGridView1, Classes.App.DataGridStyles.Students);
            if (dataGridView2.Rows.Count > 0)
                RefreshSearchResults();
        }
        private void button11_Click(object sender, EventArgs e)
        {
            if (Classes.Class.Count() == 0)
                Classes.App.Error("חובה ליצור לפחות כיתה אחת לפני שיוצרים תלמידים");
            else
            {
                new StudentCard("-1").ShowDialog();
                ReloadList(Classes.Student.List());
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            new AddSomeStudents().ShowDialog();
            ReloadList(Classes.Student.List());
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            new StudentCard(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()).ShowDialog();
            ReloadList(Classes.Student.List());
        }
        private void button9_Click(object sender, EventArgs e)
        {
            // תקציר
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!Classes.Text.IsIdentityNumber(textBox4.Text))
                Classes.App.Error("מספר תעודת הזהות שהוקלד אינו תקין.");
            else
            {
                FindStudents("SELECT * FROM `Students` WHERE `id` = '" + textBox4.Text + "'");
                if (dataGridView2.Rows.Count == 0)
                {
                    label11.Text = "לא נמצא תלמיד בתעודת זהות זו.";
                    System.Media.SystemSounds.Beep.Play();
                }
                else
                {
                    label11.Text = "נמצא התלמיד הבא:";
                    System.Media.SystemSounds.Asterisk.Play();
                }
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0 && textBox2.Text.Length == 0 && clco2.Length == 0)
                Classes.App.Error("עליך להקליד לפחות אלמנט אחד לחיפוש.");
            else if (listBox2.SelectedIndex == -1)
                Classes.App.Error("עליך לבחור אופציה מהרשימה לחיפוש - מכיל או שווה.");
            else
            {
                RefreshSearchResults();
                if (dataGridView2.Rows.Count == 0)
                {
                    label11.Text = "לא נמצאו תלמידים.";
                    System.Media.SystemSounds.Beep.Play();
                }
                else
                {
                    label11.Text = "נמצאו תלמידים.";
                    System.Media.SystemSounds.Asterisk.Play();
                }
            }
        }
        private void RefreshSearchResults()
        {
            string query = "SELECT * FROM `Students` WHERE `active` = " + (!checkBox1.Checked);
            if (textBox1.Text.Length > 0)
                query += listBox2.SelectedIndex == 0 ? (" AND `firstname` = '" + textBox1.Text + "'") : (" AND `firstname` LIKE '%" + textBox1.Text + "%'");
            if (textBox2.Text.Length > 0)
                query += listBox2.SelectedIndex == 0 ? (" AND `lastname` = '" + textBox2.Text + "'") : (" AND `lastname` LIKE '%" + textBox2.Text + "%'");
            if (clco2.Length > 0)
                query += " AND `classid` = '" + clco2 + "'";
            FindStudents(query);
        }
        private void FindStudents(string query)
        {
            Classes.SQL.Query(query);
            dataGridView2.DataSource = Classes.SQL.ds.Tables[0];
            Classes.App.GenerateDataGridViewStyle(dataGridView2, Classes.App.DataGridStyles.Students);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            SelectClass sc = new SelectClass(false);
            sc.ShowDialog();
            if (sc.clco.Length > 0)
            {
                this.clco2 = sc.clco;
                textBox5.Text = Classes.Class.ClassNameByCode(sc.clco);
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            clco2 = string.Empty;
            textBox5.Text = string.Empty;
        }
        private void button30_Click(object sender, EventArgs e)
        {
            DataGridViewer dgv = new DataGridViewer("תלמידים", dataGridView1);
            dgv.ShowDialog();
            if (dgv.reaction)
                dataGridView1_CellDoubleClick(dgv._sender, dgv._e);
        }
        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            new StudentCard(dataGridView2.SelectedRows[0].Cells[0].Value.ToString()).ShowDialog();
            ReloadList(Classes.Student.List());
        }
    }
}