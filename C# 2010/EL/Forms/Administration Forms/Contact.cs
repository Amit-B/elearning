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
    public partial class Contact : UserControl
    {
        public Contact()
        {
            InitializeComponent();
        }
        private void Contact_Load(object sender, EventArgs e)
        {
            textBox1.Text = Classes.Teacher.GetName(CurrentUser.ID) + " (" + CurrentUser.ID + ")";
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox4.Visible = comboBox1.SelectedIndex == comboBox1.Items.Count - 1;
        }
        private void Reset()
        {
            textBox1.Text = Classes.Teacher.GetName(CurrentUser.ID) + " (" + CurrentUser.ID + ")";
            textBox2.ResetText();
            textBox3.ResetText();
            textBox4.ResetText();
            textBox5.ResetText();
            textBox4.Visible = false;
            comboBox1.SelectedIndex = -1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0 || (textBox2.Text.Length == 0 && textBox3.Text.Length == 0)
                || comboBox1.SelectedIndex == -1 || textBox5.Text.Length == 0 ||
                (comboBox1.SelectedIndex == comboBox1.Items.Count - 1 && textBox4.Text.Length == 0))
                Classes.App.Error("חובה למלא את כל התאים.");
            else
            {
                string body = string.Empty, header = comboBox1.SelectedIndex == comboBox1.Items.Count - 1 ? ("אחר: " + textBox4.Text) : comboBox1.SelectedItem.ToString();
                Classes.Text.WriteLine(ref body, "יצירת קשר דרך לוח הבקרה לניהול:");
                Classes.Text.WriteLine(ref body, "שולח: " + textBox1.Text);
                if (textBox2.Text.Length > 0)
                    Classes.Text.WriteLine(ref body, "מייל לחזרה: " + textBox2.Text);
                if (textBox3.Text.Length > 0)
                    Classes.Text.WriteLine(ref body, "טלפון לחזרה: " + textBox3.Text);
                Classes.Text.WriteLine(ref body, "נושא: " + header);
                Classes.Text.WriteLine(ref body, "תוכן ההודעה: \r\n\r\n" + textBox5.Text);
                Classes.Net.SendMail("amitbarami@hotmail.com", comboBox1.SelectedIndex == comboBox1.Items.Count - 1 ? ("אחר: " + textBox4.Text) : comboBox1.SelectedItem.ToString(), body);
                Reset();
                MessageBox.Show("ההודעה נשלחה בהצלחה.", "יצירת קשר", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}