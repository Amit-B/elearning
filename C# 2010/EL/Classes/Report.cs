using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace EL.Classes
{
    class Report
    {
        public readonly string Title = string.Empty, Text = string.Empty;
        public readonly object CrystalReport = null;
        public Report(object cr, string ti, string te)
        {
            this.Title = ti;
            this.Text = te;
            this.CrystalReport = cr;
        }
    }
}