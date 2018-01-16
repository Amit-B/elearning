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
    public partial class Administration : Form
    {
        public Administration()
        {
            InitializeComponent();
        }
        UserControl current = null;
        private void Administration_Load(object sender, EventArgs e)
        {
            SelectPanel(new Forms.Administration_Forms.Lobby());
        }
        private void treeView1_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            UserControl uc = null;
            switch (e.Node.Text)
            {
                // 1
                case "תפריט ראשי": uc = new Forms.Administration_Forms.Lobby(); break;
                // 2
                case "עריכת הערה מהירה": uc = new Forms.Administration_Forms.EditMOTD(); break;
                case "שליחת דואר כוללת": uc = new Forms.Administration_Forms.Mailer(); break;
                case "קבצי מידע": uc = new Forms.Administration_Forms.InfoFiles(); break;
                case "ניהול מקצועות": uc = new Forms.Administration_Forms.Subjects(); break;
                // 3
                case "רשימת המורים": uc = new Forms.Administration_Forms.TeacherList(); break;
                case "הוספת מורה": uc = new Forms.Administration_Forms.TeacherAdd(); break;
                case "עריכת מורה": uc = new Forms.Administration_Forms.TeacherEdit(); break;
                case "מחנכי כיתות": uc = new Forms.Administration_Forms.Educators(); break;
                // 4
                case "נתוני העדרויות": uc = new Forms.Administration_Forms.Statistics1(); break;
                case "נתוני הערות משמעת": uc = new Forms.Administration_Forms.Statistics2(); break;
                case "נתוני ציונים": uc = new Forms.Administration_Forms.Statistics3(); break;
                case "נתוני שיעורים": uc = new Forms.Administration_Forms.Statistics4(); break;
                case "מונה": uc = new Forms.Administration_Forms.Counter(); break;
                // 5
                case "גיבוי ושחזור המערכת": uc = new Forms.Administration_Forms.Lobby(); break;
                case "סל המחזור": uc = new Forms.Administration_Forms.Lobby(); break;
                // 6
                case "הגדרות כלליות": uc = new Forms.Administration_Forms.SystemSetting(); break;
                case "ניהול תפקידים": uc = new Forms.Administration_Forms.Duties(); break;
                case "הרשאות": uc = new Forms.Administration_Forms.Permissions(); break;
                // 7
                case "יצירת קשר": uc = new Forms.Administration_Forms.Contact(); break;
            }
            if (uc != null)
                SelectPanel(uc);
        }
        private void SelectPanel(UserControl uc)
        {
            if (uc == null)
                return;
            if (current != null)
                current.Dispose();
            current = uc;
            panel1.Controls.Clear();
            panel1.Controls.Add(uc);
        }
        private void button8_Click(object sender, EventArgs e)
        {
            treeView1.ExpandAll();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.CollapseAll();
        }
    }
}