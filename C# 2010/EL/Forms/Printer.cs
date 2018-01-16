using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
namespace EL.Forms
{
    public partial class Printer : Form
    {
        public Printer()
        {
            InitializeComponent();
        }
        List<string> templates = new List<string>(), letterAddressee = new List<string>(), gradeTypes = new List<string>(),
            noteTypes = new List<string>(), calcode = new List<string>(), students = new List<string>();
        List<Classes.Report> reports = new List<Classes.Report>();
        string modifiedTemplate = string.Empty;
        private void Printer_Load(object sender, EventArgs e)
        {
            LoadReports();
            UpdateTemplates();
            DataTable gradetypes = Classes.Grades.ListGradeTypes();
            for (int i = 0; i < gradetypes.Rows.Count; i++)
                gradeTypes.Add(gradetypes.Rows[i]["gtid"].ToString());
            DataTable notetypes = Classes.Note.ListNoteTypes();
            for (int i = 0; i < notetypes.Rows.Count; i++)
                noteTypes.Add(notetypes.Rows[i]["ntid"].ToString());
            MaskedTextBox[] texts = { maskedTextBox1, maskedTextBox2, maskedTextBox3, maskedTextBox4, maskedTextBox5 };
            CheckBox[] checks = { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5 };
            ComboBox[] cb = { comboBox4, comboBox5, comboBox6, comboBox7, comboBox8 };
            for (int i = 0; i < cb.Length; i++)
            {
                for (int j = 0; j < notetypes.Rows.Count; j++)
                {
                    cb[i].Items.Add("אחוז " + (Convert.ToBoolean(notetypes.Rows[j]["absence"]) ? "העדרויות" : "הערות") + " מסוג " + notetypes.Rows[j]["ntitle"]);
                    if (i == 0)
                        calcode.Add((Convert.ToBoolean(notetypes.Rows[j]["absence"]) ? "absence" : "note") + ";" + notetypes.Rows[j]["ntid"]);
                }
                for (int j = 0; j < gradetypes.Rows.Count; j++)
                {
                    cb[i].Items.Add("ממוצע ציונים מסוג " + gradetypes.Rows[j]["gtname"]);
                    if (i == 0)
                        calcode.Add("grade;" + gradetypes.Rows[j]["gtid"]);
                }
            }
            try
            {
                Classes.INI cal = new Classes.INI(AppFile.GetFile(AppFile.CALCULATION));
                string key = string.Empty;
                for (int i = 0; i < 5; i++)
                {
                    key = cal.GetKey("Slot" + (i + 1));
                    if (key == "None")
                        checks[i].Checked = false;
                    else
                    {
                        checks[i].Checked = true;
                        texts[i].Text = key.Split(';')[0];
                        string lookFor = string.Empty;
                        switch (key.Split(';')[1])
                        {
                            case "grade":
                                {
                                    int row = -1;
                                    for (int j = 0; j < gradetypes.Rows.Count && row == -1; j++)
                                        if (gradetypes.Rows[j]["gtid"].ToString() == key.Split(';')[2])
                                            row = j;
                                    lookFor = "ממוצע ציונים מסוג " + gradetypes.Rows[row]["gtname"];
                                    break;
                                }
                            case "note":
                                {
                                    int row = -1;
                                    for (int j = 0; j < notetypes.Rows.Count && row == -1; j++)
                                        if (notetypes.Rows[j]["ntid"].ToString() == key.Split(';')[2])
                                            row = j;
                                    lookFor = "אחוז הערות מסוג " + notetypes.Rows[row]["ntitle"];
                                    break;
                                }
                            case "absence":
                                {
                                    int row = -1;
                                    for (int j = 0; j < notetypes.Rows.Count && row == -1; j++)
                                        if (notetypes.Rows[j]["ntid"].ToString() == key.Split(';')[2])
                                            row = j;
                                    lookFor = "אחוז העדרויות מסוג " + notetypes.Rows[row]["ntitle"];
                                    break;
                                }
                        }
                        cb[i].SelectedIndex = -1;
                        for(int j = 0; j < cb[i].Items.Count && cb[i].SelectedIndex == -1; j++)
                            if(cb[i].Items[j].ToString() == lookFor)
                                cb[i].SelectedIndex = j;
                    }
                }
            }
            catch (Exception ex)
            {
                Classes.App.Error("לא היה ניתן לטעון את הגדרות התעודה (" + ex.Message + ").");
            }
            DataTable st = Classes.Student.List();
            for (int i = 0; i < st.Rows.Count; i++)
            {
                comboBox3.Items.Add(st.Rows[i]["id"] + " - " + st.Rows[i]["firstname"] + " " + st.Rows[i]["lastname"]);
                students.Add(st.Rows[i]["id"].ToString());
            }
        }
        private void UpdateTemplates()
        {
            templates.Clear();
            string[] list = Directory.GetFiles(AppDirectory.GetDirectory(AppDirectory.TEMPLATES));
            FileInfo fi;
            int c = 0;
            comboBox1.Items.Clear();
            listBox1.Items.Clear();
            for (int i = 0; i < list.Length; i++)
            {
                fi = new FileInfo(list[i]);
                comboBox1.Items.Add(fi.Name.Remove(fi.Name.LastIndexOf('.')));
                listBox1.Items.Add(fi.Name.Remove(fi.Name.LastIndexOf('.')));
                templates.Add(fi.FullName);
                c++;
            }
            label8.Text = "סך הכל " + c + " תבניות.";
        }
        private void LoadReports()
        {
            reports.Add(new Classes.Report(new CrystalReports.TeacherList(), "דף קשר - מורים",
@"רשימת כל המורים הפעילים בבית הספר.
הרשימה מכילה שדות עיקריים כמו שם מלא, טלפון, תפקיד בבית הספר ועוד."));
            reports.Add(new Classes.Report(new CrystalReports.ClassList(), "רשימת הכיתות",
@"דוח המכיל רשימה שלמה של כל הכיתות בבית הספר,
כאשר כל שורה כוללת את השכבה והמספר של הכיתה, המחנך שלה וכמות התלמידים."));
            reports.Add(new Classes.Report(new CrystalReports.StudentList(), "מאגר התלמידים",
@"רשימת כל התלמידים בבית הספר.
הרשימה כוללת שם מלא, טלפון, כיתה, ועוד מספר פרטים נחוצים לרשימה כמו זו.
שים לב כי דוח זה יכול להכיל מספר רב של עמודים בהתאם לכמות התלמידים בבית הספר."));
            for (int i = 0; i < reports.Count; i++)
                listBox2.Items.Add(reports[i].Title);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                if (richTextBox1.Text.Length > 0)
                    if (MessageBox.Show("שימוש בתבנית יגרום להחלפת הטקסט שנרשם עד עכשיו, האם אתה בטוח?", "דוח", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        return;
                richTextBox1.Text = File.ReadAllText(templates[comboBox1.SelectedIndex]);
            }
        }
        private void button13_Click(object sender, EventArgs e)
        {
            richTextBox1.Size = new Size(tableLayoutPanel1.Visible ? 685 : 459, 343);
            richTextBox1.Location = new Point(tableLayoutPanel1.Visible ? 6 : 229, 39);
            tableLayoutPanel1.Visible = !tableLayoutPanel1.Visible;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            letterAddressee.Clear();
            switch (comboBox2.SelectedIndex)
            {
                case 0: // ללא נמען
                        break;
                case 1: // כל התלמידים
                        DataTable students = Classes.Student.List();
                        for (int i = 0; i < students.Rows.Count; i++)
                            AddAddressee(students.Rows[i]["id"].ToString() + "-0");
                        break;
                case 2: // כל המורים
                    {
                        DataTable teachers = Classes.Teacher.List();
                        for (int i = 0; i < teachers.Rows.Count; i++)
                            AddAddressee(teachers.Rows[i]["tid"].ToString() + "-1");
                        break;
                    }
                case 3: // כל התלמידים והמורים
                    {
                        DataTable curList = Classes.Student.List();
                        for (int i = 0; i < curList.Rows.Count; i++)
                            AddAddressee(curList.Rows[i]["id"].ToString() + "-0");
                        curList.Dispose();
                        curList = Classes.Teacher.List();
                        for (int i = 0; i < curList.Rows.Count; i++)
                            AddAddressee(curList.Rows[i]["tid"].ToString() + "-1");
                        curList.Dispose();
                        break;
                    }
                case 4: // תלמיד ספציפי
                    {
                        Input i = new Input("בחירת תלמיד ספציפי...", "נא להקליד כאן את מספר תעודת הזהות של התלמיד אליו ברצונך לשלוח את המכתב:");
                        i.ShowDialog();
                        if (i.returned.Length > 0)
                        {
                            if (Classes.Text.IsIdentityNumber(i.returned))
                            {
                                if (Classes.Student.Exists(i.returned))
                                    AddAddressee(i.returned + "-0");
                                else
                                    Classes.App.Error("מספר תעודת הזהות שהוקלד לא קיים ברשימת התלמידים.");
                            }
                            else
                                Classes.App.Error("מספר תעודת הזהות שהוקלד אינו תקין.");
                        }
                        i.Dispose();
                        break;
                    }
                case 5: // מורה ספציפי
                    {
                        Input i = new Input("בחירת מורה ספציפי...", "נא להקליד כאן את מספר תעודת הזהות של המורה אליו ברצונך לשלוח את המכתב:");
                        i.ShowDialog();
                        if (i.returned.Length > 0)
                        {
                            if (Classes.Text.IsIdentityNumber(i.returned))
                            {
                                if (Classes.Teacher.Exists(i.returned))
                                    AddAddressee(i.returned + "-1");
                                else
                                    Classes.App.Error("מספר תעודת הזהות שהוקלד לא קיים ברשימת המורים.");
                            }
                            else
                                Classes.App.Error("מספר תעודת הזהות שהוקלד אינו תקין.");
                        }
                        i.Dispose();
                        break;
                    }
                case 6: // בחירת תלמידים
                    {
                        StudentsSelect ss = new StudentsSelect("שליחת מכתב");
                        ss.ShowDialog();
                        if (ss.returnedStudents != null)
                            for (int i = 0; i < ss.returnedStudents.Count; i++)
                                AddAddressee(ss.returnedStudents[i] + "-0");
                        ss.Dispose();
                        break;
                    }
            }
        }
        private void AddAddressee(string val)
        {
            if (!letterAddressee.Contains(val))
            {
                letterAddressee.Add(val);
                UpdateAddressees();
            }
        }
        private void UpdateAddressees()
        {
            button12.Text = "צפייה בנמענים (" + letterAddressee.Count + ")";
        }
        private void button30_Click(object sender, EventArgs e)
        {
            TextEditor te = new TextEditor("מכתב", richTextBox1.Text);
            te.ShowDialog();
            if (te.returned.Length > 0)
                richTextBox1.Text = te.returned;
            te.Dispose();
        }
        private void button31_Click(object sender, EventArgs e)
        {
            letterAddressee.Clear();
            UpdateAddressees();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 0)
                if (MessageBox.Show("שימוש בכפתור זה ימחק את כל מה שנרשם עד עכשיו, האם אתה בטוח?", "מכתב", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            richTextBox1.ResetText();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBox1.Text);
            MessageBox.Show("הטקסט הועתק בהצלחה.", "מכתב", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length == 0)
                Classes.App.Error("מכתב לא רשום.");
            else
            {
                if (letterAddressee.Count == 0)
                {
                    if (MessageBox.Show("האם אתה בטוח שברצונך להדפיס?", "הנפקת מכתב", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Classes.PCPrint printing = new Classes.PCPrint();
                        printing.PrinterFont = richTextBox1.Font;
                        printing.TextToPrint = "." + richTextBox1.Text;
                        printing.Print();
                        printing.Dispose();
                    }
                }
                else
                {
                    if (MessageBox.Show("אופציה זו תדפיס את המכתב עבור " + letterAddressee.Count + " נמענים שבחרת, מכתב לכל אחד.\r\nהאם אתה בטוח שברצונך להדפיס?", "הנפקת מכתב", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        for (int i = 0; i < letterAddressee.Count; i++)
                        {
                            Classes.PCPrint printing = new Classes.PCPrint();
                            printing.PrinterFont = richTextBox1.Font;
                            printing.TextToPrint = "." + Classes.Text.TextFormatting(richTextBox1.Text, letterAddressee[i].Split('-')[0], letterAddressee[i].Split('-')[1] == "1");
                            printing.Print();
                            printing.Dispose();
                        }
                }
            }
        }
        private void button34_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.FontMustExist = true;
            if (fd.ShowDialog() == DialogResult.OK)
                richTextBox1.Font = fd.Font;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                Classes.App.Error("עליך לבחור תבנית.");
            else
            {
                groupBox1.Text = "תבנית נבחרת: " + listBox1.Items[listBox1.SelectedIndex].ToString();
                textBox1.Text = listBox1.Items[listBox1.SelectedIndex].ToString();
                textBox2.Text = File.ReadAllText(templates[listBox1.SelectedIndex]);
                modifiedTemplate = templates[listBox1.SelectedIndex];
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            TextEditor te = new TextEditor("תבנית קיימת", textBox2.Text);
            te.ShowDialog();
            if (te.returned.Length > 0)
                textBox2.Text = te.returned;
            te.Dispose();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0 || textBox2.Text.Length == 0)
                Classes.App.Error("לא ניתן להשאיר תבנית ללא שם או ללא תוכן.");
            else
            {
                File.WriteAllText(modifiedTemplate, textBox2.Text);
                File.Move(modifiedTemplate, modifiedTemplate = (AppDirectory.TEMPLATES + "/" + textBox1.Text + ".txt"));
                groupBox1.Text = "תבנית נבחרת: " + textBox1.Text;
                UpdateTemplates();
                MessageBox.Show("העריכה בוצעה בהצלחה!", "תבנית", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            File.Delete(modifiedTemplate);
            modifiedTemplate = string.Empty;
            groupBox1.Text = "תבנית נבחרת";
            UpdateTemplates();
            MessageBox.Show("התבנית נמחקה בהצלחה!", "תבנית", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void button10_Click(object sender, EventArgs e)
        {
            if (textBox4.Text.Length == 0 || textBox3.Text.Length == 0)
                Classes.App.Error("לא ניתן ליצור תבנית ללא שם או ללא תוכן.");
            else
            {
                File.WriteAllText(AppDirectory.TEMPLATES + "/" + textBox4.Text + ".txt", textBox3.Text);
                UpdateTemplates();
                MessageBox.Show("התבנית נוספה בהצלחה!", "תבנית", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            textBox3.ResetText();
            textBox4.ResetText();
        }
        private void button11_Click(object sender, EventArgs e)
        {
            TextEditor te = new TextEditor("תבנית חדשה", textBox2.Text);
            te.ShowDialog();
            if (te.returned.Length > 0)
                textBox3.Text = te.returned;
            te.Dispose();
        }
        private void button12_Click(object sender, EventArgs e)
        {
            new ListContacts(letterAddressee).ShowDialog();
        }
        private void button32_Click(object sender, EventArgs e)
        {
            new ReportViewer(reports[listBox2.SelectedIndex].CrystalReport).ShowDialog();
        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupBox3.Text = "מידע: " + reports[listBox2.SelectedIndex].Title;
            textBox13.Text = reports[listBox2.SelectedIndex].Text;
        }
        private void button33_Click_1(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "בחר מיקום לשמירת העתק...";
            sfd.Filter = "Text File (*.txt)|*.txt|All Files|*.*";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(sfd.FileName, richTextBox1.Text);
                MessageBox.Show("הקובץ נשמר בהצלחה.", "הנפקת מכתבים", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button14_Click(object sender, EventArgs e)
        {
            TextEditor te = new TextEditor("תעודה", textBox2.Text);
            te.ShowDialog();
            if (te.returned.Length > 0)
                textBox2.Text = te.returned;
            te.Dispose();
        }
        private void button15_Click(object sender, EventArgs e)
        {
            MaskedTextBox[] texts = { maskedTextBox1, maskedTextBox2, maskedTextBox3, maskedTextBox4, maskedTextBox5 };
            CheckBox[] checks = { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5 };
            ComboBox[] cb = { comboBox4, comboBox5, comboBox6, comboBox7, comboBox8 };
            int total = 0;
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].Text = Classes.App.IntBase(Convert.ToInt32(texts[i].Text), 3);
                total += Convert.ToInt32(texts[i].Text);
            }
            bool flag = false;
            for (int i = 0; i < 5 && !flag; i++)
                if (cb[i].SelectedIndex == -1 && checks[i].Checked)
                    flag = true;
            if (flag)
                Classes.App.Error("לא בחרת אפשרות לכל שורות האחוזים המאופשרות.");
            else
            {
                if (total == 100)
                {
                    Classes.INI cal = new Classes.INI(AppFile.GetFile(AppFile.CALCULATION));
                    for (int i = 0; i < 5; i++)
                        cal.SetKey("Slot" + (i + 1), checks[i].Checked ? (texts[i].Text + ";" + calcode[cb[i].SelectedIndex]) : "None");
                    MessageBox.Show("החישוב לציונים יורכב מהאחוזים שציינת.\r\nבנוסף החישוב נשמר בהצלחה.", "הנפקת תעודות", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    Classes.App.Error("עליך להרכיב 100 אחוזים.");
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            maskedTextBox1.Enabled = checkBox1.Checked;
            comboBox4.Enabled = checkBox1.Checked;
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            maskedTextBox2.Enabled = checkBox2.Checked;
            comboBox5.Enabled = checkBox2.Checked;
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            maskedTextBox3.Enabled = checkBox3.Checked;
            comboBox6.Enabled = checkBox3.Checked;
        }
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            maskedTextBox4.Enabled = checkBox4.Checked;
            comboBox7.Enabled = checkBox4.Checked;
        }
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            maskedTextBox5.Enabled = checkBox5.Checked;
            comboBox8.Enabled = checkBox5.Checked;
        }
        private void button16_Click(object sender, EventArgs e)
        {
            if (comboBox1.Items.Contains("ממוצע ציונים מסוג מבחן") &&
                comboBox2.Items.Contains("ממוצע ציונים מסוג בוחן") &&
                comboBox3.Items.Contains("אחוז העדרויות מסוג חיסור"))
            {
                checkBox1.Checked = true;
                checkBox2.Checked = true;
                checkBox3.Checked = true;
                maskedTextBox1.Text = "060";
                maskedTextBox2.Text = "030";
                maskedTextBox3.Text = "010";
                comboBox1.SelectedIndex = -1;
                for (int i = 0; i < comboBox1.Items.Count && comboBox1.SelectedIndex == -1; i++)
                    if (comboBox1.Items[i].ToString() == "ממוצע ציונים מסוג מבחן")
                        comboBox1.SelectedIndex = i;
                for (int i = 0; i < comboBox2.Items.Count && comboBox2.SelectedIndex == -1; i++)
                    if (comboBox2.Items[i].ToString() == "ממוצע ציונים מסוג בוחן")
                        comboBox2.SelectedIndex = i;
                for (int i = 0; i < comboBox3.Items.Count && comboBox3.SelectedIndex == -1; i++)
                    if (comboBox3.Items[i].ToString() == "ממוצע העדרויות מסוג חיסור")
                        comboBox3.SelectedIndex = i;
            }
            else
                Classes.App.Error("כדי להשלים באופן אוטומטי, בית הספר חייב להכיל סוגי ציונים של \"מבחן\", \"בוחן\", וסוג העדרות בשם \"חיסור\".");
        }
        private void button25_Click(object sender, EventArgs e)
        {
            if (richTextBox3.Text.Length > 0 && MessageBox.Show("האם אתה בטוח שברצונך למחוק את כל הטקסט המופיע בתעודה?", "הנפקת תעודות", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.Yes)
                richTextBox3.ResetText();
        }
        private void button23_Click(object sender, EventArgs e)
        {
            string variables = string.Empty;
            Classes.Text.WriteLine(ref variables, "{FIRSTNAME} = שם פרטי");
            Classes.Text.WriteLine(ref variables, "{LASTNAME} = שם משפחה");
            Classes.Text.WriteLine(ref variables, "{CLASS} = שכבה ומספר כיתה");
            Classes.Text.WriteLine(ref variables, "{STARTUM} = שכבה");
            Classes.Text.WriteLine(ref variables, "{CLASSNUM} = מספר כיתה");
            Classes.Text.WriteLine(ref variables, "{DATETIME} = תאריך ושעת הדפסה");
            Classes.Text.WriteLine(ref variables, "{ID} = מספר תעודת זהות");
            DataTable groups = Classes.Group.List();
            int pclass = Classes.Class.GetClCoStartumNum(Classes.Student.GetInfo(students[comboBox3.SelectedIndex], "classid"));
            for (int i = 0; i < groups.Rows.Count; i++)
                if (groups.Rows[i]["gclass"].ToString() == pclass.ToString())
                    Classes.Text.WriteLine(ref variables, "{GRADE" + groups.Rows[i]["gid"] + "} = ציון במקצוע " + Classes.Subject.GetName(Convert.ToInt32(groups.Rows[i]["sid"])) + " (קבוצת לימוד: " + groups.Rows[i]["gname"] + ")");
        }
        private void button24_Click(object sender, EventArgs e)
        {
            if (richTextBox3.Text.Length == 0)
                Classes.App.Error("אין טקסט תעודה.");
            else
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "בחר מיקום לשמירת תעודה...";
                sfd.Filter = "Text Files (*.txt)|*.txt|All Files|*.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, richTextBox3.Text);
                    MessageBox.Show("התעודה נשמרה בהצלחה.", "הנפקת תעודות", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void button28_Click(object sender, EventArgs e)
        {
            if (!AbleToGetFinalGrade())
                Classes.App.Error("בעיה במערכת חישוב הציון לא מאפשרת את ביצוע הפעולה הזו.");
            else
            {
                string toPrint = Classes.Text.TextFormatting(richTextBox3.Text, students[comboBox3.SelectedIndex].ToString(), false);
                DataTable groups = Classes.Group.List();
                int pclass = Classes.Class.GetClCoStartumNum(Classes.Student.GetInfo(students[comboBox3.SelectedIndex], "classid"));
                for (int i = 0; i < groups.Rows.Count; i++)
                    if (groups.Rows[i]["gclass"].ToString() == pclass.ToString())
                        if (toPrint.Contains("{G." + groups.Rows[i]["gid"] + "}"))
                            toPrint = toPrint.Replace("{GRADE" + groups.Rows[i]["gid"] + "}", GetFinalGrade(students[comboBox3.SelectedIndex], groups.Rows[i]["gid"].ToString()).ToString());
                MessageBox.Show(toPrint, "תצוגה מקדימה", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void button22_Click(object sender, EventArgs e)
        {
            if (!AbleToGetFinalGrade())
                Classes.App.Error("בעיה במערכת חישוב הציון לא מאפשרת את ביצוע הפעולה הזו.");
            else
            {
                if (MessageBox.Show("האם אתה בטוח שברצונך להדפיס את התעודה?", "הנפקת תעודות", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string toPrint = Classes.Text.TextFormatting(richTextBox3.Text, students[comboBox3.SelectedIndex].ToString(), false);
                    DataTable groups = Classes.Group.List();
                    int pclass = Classes.Class.GetClCoStartumNum(Classes.Student.GetInfo(students[comboBox3.SelectedIndex], "classid"));
                    for (int i = 0; i < groups.Rows.Count; i++)
                        if (groups.Rows[i]["gclass"].ToString() == pclass.ToString())
                            if (toPrint.Contains("{G." + groups.Rows[i]["gid"] + "}"))
                                toPrint = toPrint.Replace("{GRADE" + groups.Rows[i]["gid"] + "}", GetFinalGrade(students[comboBox3.SelectedIndex], groups.Rows[i]["gid"].ToString()).ToString());
                    Classes.PCPrint printing = new Classes.PCPrint();
                    printing.PrinterFont = richTextBox3.Font;
                    printing.TextToPrint = toPrint;
                    printing.Print();
                    printing.Dispose();
                }
            }
        }
        private void button29_Click(object sender, EventArgs e)
        {
            if (richTextBox3.Text.Length > 0)
                Classes.App.Error("כבר קיים טקסט בתעודה, מחק את כולו כדי להוסיף באופן אוטומטי.");
            else
            {
                string header =
@"תעודת סיום לימודי כיתה {STARTUM}


שם התלמיד: {FIRSTNAME} {LASTNAME} (ת.ז: {ID})
כיתה: {CLASS}


-----
ציונים:

";
                string grades = string.Empty;
                DataTable groups = Classes.Group.List();
                int pclass = Classes.Class.GetClCoStartumNum(Classes.Student.GetInfo(students[comboBox3.SelectedIndex], "classid"));
                for (int i = 0; i < groups.Rows.Count; i++)
                    if (groups.Rows[i]["gclass"].ToString() == pclass.ToString())
                        Classes.Text.WriteLine(ref grades, " * " + Classes.Subject.GetName(Convert.ToInt32(groups.Rows[i]["sid"])) + ": {GRADE" + groups.Rows[i]["gid"] + "}");
                string footer =
@"
-----


הודפס על ידי מערכת ELearning - למידה מתוקשבת ({DATETIME})";
                richTextBox3.Text = header + grades + footer;
            }
        }
        private void button17_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.FontMustExist = true;
            if (fd.ShowDialog() == DialogResult.OK)
                richTextBox3.Font = fd.Font;
        }
        private double GetFinalGrade(string id, string gid)
        {
            MaskedTextBox[] texts = { maskedTextBox1, maskedTextBox2, maskedTextBox3, maskedTextBox4, maskedTextBox5 };
            CheckBox[] checks = { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5 };
            ComboBox[] cb = { comboBox4, comboBox5, comboBox6, comboBox7, comboBox8 };
            int amount = 0;
            double final = 0.0;
            for (int i = 0; i < 5; i++)
                if (checks[i].Checked)
                {
                    if (calcode[cb[i].SelectedIndex].Split(';')[0] == "grade")
                    {
                        // [(amount of all grades) / (count of grades)] * (percentage / 100)
                        Classes.SQL.Query("SELECT * FROM `GradeLists` WHERE `gid` = '" + gid + "' AND `gtid` = '" + calcode[cb[i].SelectedIndex].Split(';')[1] + "'");
                        for (int j = 0; j < Classes.SQL.ds.Tables[0].Rows.Count; j++)
                            amount += Classes.Grades.GetGrade(id, Classes.SQL.ds.Tables[0].Rows[j]["glid"].ToString());
                        final += (amount / Classes.SQL.ds.Tables[0].Rows.Count) * (Convert.ToDouble(texts[i].Text) / 100);
                    }
                    if (calcode[cb[i].SelectedIndex].Split(';')[0] == "note" || calcode[cb[i].SelectedIndex].Split(';')[0] == "absence")
                    {
                        // [(count of note) / (count of lessons)] * percentage
                        final += Convert.ToDouble(texts[i].Text) - ((Classes.Note.GetNotesBy(id: id, ntid: calcode[cb[i].SelectedIndex].Split(';')[1]).Rows.Count * Classes.Lessons.Count(Classes.Lessons.LessonSelectType.Exist)) * Convert.ToDouble(texts[i].Text));
                    }
                }
            return final;
        }
        private bool AbleToGetFinalGrade()
        {
            MaskedTextBox[] texts = { maskedTextBox1, maskedTextBox2, maskedTextBox3, maskedTextBox4, maskedTextBox5 };
            CheckBox[] checks = { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5 };
            ComboBox[] cb = { comboBox4, comboBox5, comboBox6, comboBox7, comboBox8 };
            int total = 0;
            for (int i = 0; i < texts.Length; i++)
                total += Convert.ToInt32(texts[i].Text);
            bool flag = false;
            for (int i = 0; i < 5 && !flag; i++)
                if (cb[i].SelectedIndex == -1 && checks[i].Checked)
                    flag = true;
            if (flag)
                return false;
            else if (total != 100)
                    return false;
            return true;
        }
    }
}