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
    public partial class TMP_Install : Form
    {
        private bool allowClose = false;
        private Main m;
        public TMP_Install(Main mainForm)
        {
            InitializeComponent();
            m = mainForm;
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = checkBox1.Checked;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Forms.TMP_Install_2(m, this).ShowDialog();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            allowClose = true;
            //this.Close();
            Application.Exit();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            allowClose = true;
            this.Close();
            m.Show();
        }
        private void Install_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(!allowClose)
                Application.Exit();
        }
        private void Install_Load(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Replace("X", EL.Properties.Resources.Version);
            textBox2.Text =
@"EL היא תוכנה המאפשרת ניהול בית ספר בדרך ממוחשבת.
באמצעות התוכנה ניתן לטפל בנושאים כמו ציונים, כיתות, קבוצות לימוד, מאגר תלמידים, הערות משמעת ועוד,
וכמו כן אפשרויות מתקדמות כמו דוא""ל למורים, שיבוץ בכיתות ועוד.

• דרישות מינימליות לתוכנה:

- Microsoft Windows XP
- .NET Framework 4
- כמות שטח פנוי בדיסק הקשיח עבור התוכנה עצמה ורישומים נוספים
- גישה לאינטרנט

• מידע על הפיתוח:

- התוכנה פותחה ע""י עמית בראמי EL © 2011-2012
- אייקונים בתוכנה הובאו מהאתר IconArchive.com ©
- דיבור ע""י Google Translate ©

- לפיתוח התוכנה או העבודה בה אין קשר ישיר למשרד החינוך;
היא פועלת במחשב בו היא הותקנה והמידע נשאר באותו המחשב ולא מועבר לשום צד שלישי.

• תנאי השימוש:

1) אין להשתמש בתוכנה למטרה מסחרית ללא אישור מפורש מהמפתח.
2) מפתח התוכנה לא אחראי על שום נזק שיגרם למחשב או על מידע שימחק (פנימי או חיצוני לתוכנה) כתוצאה משימוש בתוכנה.
3) הלקוח אינו יכול לתבוע את המפתח מכל סיבה שקשורה בתוכנה.
4) השימוש בתוכנה באחראיות המשתמש בלבד.
5) כל הזכויות על תוכנה זו שמורות למפתח בלבד, אין להעתיק את קוד המקור או להשתמש בו ללא אישור מפורש מהמפתח.
6) אין להסתמך על הכנסת מידע רגיש לתוכנה ושמירה על בטיחותו. מפתח התוכנה אינו אחראי על מידע שנתגלה בלי שהיה צריך להתגלות.
";
        }
    }
}