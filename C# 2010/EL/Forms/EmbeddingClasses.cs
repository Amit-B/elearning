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
    public partial class EmbeddingClasses : Form
    {
        private enum ClassSlotAction { Open, Close, Update }
        private List<string> classes, students;
        private bool[] open = new bool[Classes.Class.MAX_EMBED_CLASSES];
        private string[] classcodes = new string[Classes.Class.MAX_EMBED_CLASSES];
        private const string div = "-  שיבוץ אוטומטי של כל התלמידים באקראיות (0 תלמידים בכל כיתה)";
        public EmbeddingClasses(List<string> students, List<string> classes)
        {
            InitializeComponent();
            this.classes = classes;
            this.students = students;
            for (int i = 0; i < classes.Count; i++)
            {
                ClassSlot(i, ClassSlotAction.Open, classes[i]);
                label1.Text += Classes.Class.ClassNameByCode(classes[i]) + (i == classes.Count - 1 ? "" : ", ");
            }
            for (int i = 0; i < students.Count; i++)
                listBox1.Items.Add(students[i]);
        }
        private void ClassSlot(int slot, ClassSlotAction action, string clco)
        {
            Label slotLabel = null;
            ListBox slotListBox = GetSlotListBox(slot);
            Button[] slotButtons = { null, null, null };
            switch (slot)
            {
                case 0: slotLabel = label3; slotButtons[0] = button5; slotButtons[1] = button6; slotButtons[2] = button7; break;
                case 1: slotLabel = label5; slotButtons[0] = button8; slotButtons[1] = button10; slotButtons[2] = button9; break;
                case 2: slotLabel = label6; slotButtons[0] = button11; slotButtons[1] = button13; slotButtons[2] = button12; break;
                case 3: slotLabel = label7; slotButtons[0] = button14; slotButtons[1] = button16; slotButtons[2] = button15; break;
                case 4: slotLabel = label8; slotButtons[0] = button18; slotButtons[1] = button20; slotButtons[2] = button19; break;
                case 5: slotLabel = label9; slotButtons[0] = button21; slotButtons[1] = button23; slotButtons[2] = button22; break;
            }
            if (action == ClassSlotAction.Open)
            {
                slotLabel.Text = "(0) כיתה " + Classes.Class.ClassNameByCode(clco);
                slotButtons[2].Image = Images.X;
                classcodes[slot] = clco;
                UpdateDivText();
            }
            else if (action == ClassSlotAction.Close)
            {
                if (slotListBox.Items.Count > 0)
                    SendStudentsBack(slot);
                slotButtons[2].Image = Images.Plus;
                classcodes[slot] = string.Empty;
                UpdateDivText();
            }
            else if (action == ClassSlotAction.Update)
                slotLabel.Text = "(" + slotListBox.Items.Count +") כיתה " + Classes.Class.ClassNameByCode(clco);
            slotLabel.Visible = action == ClassSlotAction.Open;
            GetSlotListBox(slot).Visible = action == ClassSlotAction.Open;
            slotButtons[0].Visible = action == ClassSlotAction.Open;
            slotButtons[1].Visible = action == ClassSlotAction.Open;
            slotButtons[2].Visible = true;
            open[slot] = action == ClassSlotAction.Open;
        }
        private void UpdateDivText()
        {
            int openSlots = 0;
            for (int i = 0; i < open.Length; i++)
                if (open[i])
                    openSlots++;
            label10.Text = div.Replace("0", (listBox1.Items.Count > 0 && openSlots > listBox1.Items.Count ? (openSlots / 2) : 0).ToString());
        }
        private ListBox GetSlotListBox(int slot)
        {
            ListBox slotListBox = null;
            switch (slot)
            {
                case 0: slotListBox = listBox2; break;
                case 1: slotListBox = listBox3; break;
                case 2: slotListBox = listBox4; break;
                case 3: slotListBox = listBox5; break;
                case 4: slotListBox = listBox6; break;
                case 5: slotListBox = listBox7; break;
            }
            return slotListBox;
        }
        private void SendStudentsBack(int slot)
        {
            ListBox slotListBox = GetSlotListBox(slot);
            for (int i = 0; i < slotListBox.Items.Count; i++)
                listBox1.Items.Add(slotListBox.Items[i]);
            slotListBox.Items.Clear();
        }
        private void Left_Click(object sender, EventArgs e)
        {
            int slot = Convert.ToInt32((sender as Button).Text.Replace("#", "")) - 1;
            if (open[slot] && listBox1.SelectedIndex != -1)
            {
                GetSlotListBox(slot).Items.Add(listBox1.SelectedItem);
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }
        private void Right_Click(object sender, EventArgs e)
        {
            int slot = Convert.ToInt32((sender as Button).Text.Replace("#", "")) - 1;
            ListBox tmp = GetSlotListBox(slot);
            if (open[slot])
            {
                if (tmp.SelectedIndex == -1)
                    Classes.App.Error("כפתור זה משמש להחזרת תלמיד מכיתה לרשימת התלמידים. עליך לבחור תלמיד מהרשימה של כיתה זו.");
                else
                {
                    listBox1.Items.Add(tmp.SelectedItem);
                    tmp.Items.Remove(tmp.SelectedItem);
                }
            }
        }
        private void SlotClick(object sender, EventArgs e)
        {
            int slot = Convert.ToInt32((sender as Button).Text.Replace("#", "")) - 1;
            if (open[slot])
                ClassSlot(slot, ClassSlotAction.Close, string.Empty);
            else
            {
                SelectClass sc = new SelectClass(true);
                sc.ShowDialog();
                if (sc.clco.Length > 0)
                {
                    bool flag = false;
                    for (int i = 0; i < Classes.Class.MAX_EMBED_CLASSES && !flag; i++)
                        if (open[i] && classcodes[i] == sc.clco)
                            flag = true;
                    if (flag)
                        Classes.App.Error("כיתה זו כבר קיימת ברשימה.");
                    else
                        ClassSlot(slot, ClassSlotAction.Open, sc.clco);
                }
                sc.Dispose();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            StudentsSelect ss = new StudentsSelect("שיבוץ בכיתות", 1);
            ss.ShowDialog();
            if (ss.returnedStudents != null)
                for (int i = 0; i < ss.returnedStudents.Count; i++)
                    if (!listBox1.Items.Contains(ss.returnedStudents[i]))
                        listBox1.Items.Add(ss.returnedStudents[i]);
            UpdateDivText();
            ss.Dispose();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                Classes.App.Error("אפשרות זו משמשת למחיקת תלמיד מהרשימה השמאלית. נא לבחור תלמיד כדי למחוק.");
            else
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
                UpdateDivText();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count == 0)
                Classes.App.Error("אין תלמידים ברשימה.");
            else
            {
                listBox1.Items.Clear();
                UpdateDivText();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            int openSlots = 0;
            for (int i = 0; i < open.Length; i++)
                if (open[i])
                    openSlots++;
            if (openSlots <= 1)
                Classes.App.Error("כמות הכיתות נמוכה מדי לביצוע שיבוץ אוטומטי.");
            else if (listBox1.Items.Count <= openSlots)
                Classes.App.Error("כמות התלמידים נמוכה מדי לביצוע שיבוץ אוטומטי.");
            else
            {
                Random rnd = new Random();
                int slot = rnd.Next(0, open.Length);
                while (!open[slot])
                    slot = rnd.Next(0, open.Length);
                while (listBox1.Items.Count > 0)
                {
                    GetSlotListBox(slot).Items.Add(listBox1.SelectedItem);
                    listBox1.Items.Remove(listBox1.SelectedItem);
                }
            }
        }
        private void button17_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < open.Length; i++)
                if (open[i])
                    SendStudentsBack(i);
        }
        private void button24_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("האם אתה בטוח שברצונך לשמור את תצורת השיבוץ הנוכחית?\r\nשים לב, אישור הפעולה יוציא את כל התלמידים מהכיתות הנוכחיות שלהם,\r\nויוסיף אותם לכיתות שהוגדרו כאן בטופס השיבוץ.", "שיבוץ בכיתות", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ListBox lb = null;
                for (int i = 0; i < open.Length; i++)
                    if (open[i])
                    {
                        lb = GetSlotListBox(i);
                        for (int j = 0; j < lb.Items.Count; j++ )
                            Classes.SQL.Update("Students", "`classid` = '" + classcodes[i] + "'", "id", lb.Items[i].ToString().Split(':')[0]);
                    }
            }
        }
    }
}