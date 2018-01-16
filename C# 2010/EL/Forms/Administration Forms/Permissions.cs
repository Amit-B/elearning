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
    public partial class Permissions : UserControl
    {
        public Permissions()
        {
            InitializeComponent();
        }
        Classes.INI cfg = new Classes.INI(AppFile.GetFile(AppFile.CONFIG));
        private bool loadingyafool = false;
        private void Permissions_Load(object sender, EventArgs e)
        {
            loadingyafool = true;
            ComboBox[] cb = { comboBox1, comboBox2, comboBox3, comboBox4, comboBox5, comboBox6, comboBox7, comboBox8 };
            for (int i = 0; i < cb.Length; i++)
                cb[i].SelectedIndex = Convert.ToInt32(cfg.GetKey("Permission" + (i + 1)));
            loadingyafool = false;
        }
        private void SelectPermission(object sender, EventArgs e)
        {
            if (!loadingyafool && (sender as ComboBox).SelectedIndex != -1)
            {
                cfg.SetKey("Permission" + (sender as ComboBox).Name.Replace("comboBox", ""), (sender as ComboBox).SelectedIndex.ToString());
                MessageBox.Show("ההרשאה שונתה בהצלחה.", "הרשאות", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}