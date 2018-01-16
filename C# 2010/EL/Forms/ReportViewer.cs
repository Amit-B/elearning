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
    public partial class ReportViewer : Form
    {
        private object cr = null;
        public ReportViewer(object cr)
        {
            InitializeComponent();
            this.cr = cr;
        }
        private void ReportViewer_Load(object sender, EventArgs e)
        {
            crystalReportViewer1.ReportSource = this.cr;
        }
    }
}