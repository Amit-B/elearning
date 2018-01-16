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
    public partial class PrivatePanel : Form
    {
        public PrivatePanel()
        {
            InitializeComponent();
        }
        private List<string> files = new List<string>(), teachers = new List<string>();
        private string pic = AppDirectory.GetDirectory(AppDirectory.PROFILEPICS) + "/" + CurrentUser.ID + ".png";
        private void PrivatePanel_Load(object sender, EventArgs e)
        {
            textBox1.Text = File.ReadAllText(AppFile.GetFile(AppFile.MARQUEE), Encoding.UTF8);
            string[] list = Directory.GetFiles(AppDirectory.GetDirectory(AppDirectory.INFO));
            FileInfo tmp;
            for (int i = 0; i < list.Length; i++)
            {
                tmp = new FileInfo(list[i]);
                listBox1.Items.Add(tmp.Name);
                files.Add(list[i]);
            }
            label13.Text = CurrentUser.ID;
            DataRow row = Classes.SQL.Select("Teachers", "tid", CurrentUser.ID).Rows[0];
            label5.Text = row["firstname"].ToString();
            label7.Text = row["lastname"].ToString();
            label9.Text = Classes.App.GenderName(Convert.ToInt32(row["gender"]));
            label11.Text = Classes.Duty.GetName(row["dutyid"].ToString());
            textBox4.Text = row["phone"].ToString();
            textBox3.Text = row["password"].ToString();
            pictureBox1.Image = File.Exists(pic) ? Image.FromFile(pic) : Images.NoImage;
            DataTable ts = Classes.Teacher.List();
            for (int i = 0; i < ts.Rows.Count; i++)
            {
                teachers.Add(ts.Rows[i]["tid"].ToString());
                dataGridView1.Rows.Add(new object[]
                {
                    ts.Rows[i]["firstname"].ToString() + " " + ts.Rows[i]["lastname"].ToString(),
                    Classes.Duty.GetName(ts.Rows[i]["dutyid"].ToString()),
                    ts.Rows[i]["phone"].ToString()
                });
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                string[] lines = File.ReadAllLines(files[listBox1.SelectedIndex]);
                for (int i = 0; i < 3 && i < lines.Length; i++)
                {
                    textBox1.Text += lines[i] + "\r\n";
                    if (i == 2)
                        textBox1.Text += "...";
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                Classes.App.Error("נא לבחור קובץ.");
            else
                new InfoFile(listBox1.SelectedItem.ToString(), File.ReadAllText(files[listBox1.SelectedIndex])).ShowDialog();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Classes.SQL.Update("Teachers", "`password` = '" + textBox3.Text + "', `phone` = '" + textBox2.Text + "'", "tid", CurrentUser.ID);
            textBox3.ReadOnly = true;
            MessageBox.Show("הפרטים שלך נשמרו בהצלחה.", "כרטיס אישי", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void button14_Click(object sender, EventArgs e)
        {
            Input oldpass = new Input("שינוי סיסמא", "נא להקליד כאן את הסיסמא הנוכחית שלך:");
            oldpass.ShowDialog();
            if (oldpass.returned.Length > 0)
            {
                if (oldpass.returned == Classes.Teacher.GetInfo(CurrentUser.ID, "password"))
                {
                    textBox3.ReadOnly = false;
                    MessageBox.Show("ניתן כעת לשנות את הסיסמא דרך תיבת הטקסט העליונה.\r\nלא לשכוח ללחוץ על שמירה!", "כרטיס אישי", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    Classes.App.Error("הסיסמא הנוכחית שגויה.");
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == Images.NoImage)
                Classes.App.Error("אין לך תמונה.");
            else if(MessageBox.Show("האם אתה בטוח שברצונך להסיר את התמונה?","כרטיס אישי",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                pictureBox1.Image = Images.NoImage;
                pictureBox2.Image = Images.NoImage;
                if (File.Exists(pic)) try { File.Delete(pic); } catch { }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "בחר תמונה...";
            ofd.Filter = "PNG|*.png|JPG|*.jpg|BMP|*.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Images.NoImage;
                pictureBox2.Image = Images.NoImage;
                try { File.Copy(ofd.FileName, pic, true); }
                catch { }
                pictureBox1.Image = Image.FromFile(pic);
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
                if (dataGridView1.SelectedRows[0].Index != -1)
                {
                    label29.Text = Classes.Teacher.GetName(teachers[dataGridView1.SelectedRows[0].Index]);
                    textBox8.Text = Classes.Teacher.GetInfo(teachers[dataGridView1.SelectedRows[0].Index], "phone");
                    string tpic = AppDirectory.GetDirectory(AppDirectory.PROFILEPICS) + "/" + teachers[dataGridView1.SelectedRows[0].Index] + ".png";
                    pictureBox2.Image = File.Exists(tpic) ? Image.FromFile(tpic) : Images.NoImage;
                }
        }
    }
}