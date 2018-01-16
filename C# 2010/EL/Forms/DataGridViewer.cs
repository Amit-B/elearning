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
    public partial class DataGridViewer : Form
    {
        public bool reaction = false;
        public object _sender = null;
        public DataGridViewCellEventArgs _e = null;
        public DataGridViewer(string tableName, DataGridView table)
        {
            InitializeComponent();
            label2.Text = tableName;
            this.Text = tableName + " - צפייה בטבלה";
            dataGridView1.AllowUserToAddRows = table.AllowUserToAddRows;
            dataGridView1.AllowUserToDeleteRows = table.AllowUserToDeleteRows;
            dataGridView1.ColumnHeadersHeightSizeMode = table.ColumnHeadersHeightSizeMode;
            dataGridView1.MultiSelect = table.MultiSelect;
            dataGridView1.ReadOnly = table.ReadOnly;
            dataGridView1.RowHeadersVisible = table.RowHeadersVisible;
            dataGridView1.SelectionMode = table.SelectionMode;
            dataGridView1.ShowCellErrors = table.ShowCellErrors;
            dataGridView1.ShowCellToolTips = table.ShowCellToolTips;
            dataGridView1.ShowEditingIcon = table.ShowEditingIcon;
            dataGridView1.ShowRowErrors = table.ShowRowErrors;
            dataGridView1.Visible = table.Visible;
            dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellDoubleClick);
            //dataGridView1.Size = new Size(1035, 495);
            for (int i = 0; i < table.Columns.Count; i++)
            {
                dataGridView1.Columns.Add(table.Columns[i]);
                dataGridView1.Columns[i].Width = table.Columns[i].Width * 2;
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                dataGridView1.Rows.Add(table.Rows[i]);
                for (int j = 0; j < table.Rows[i].Cells.Count; j++)
                    dataGridView1.Rows[i].Cells.Add(table.Rows[i].Cells[j]);
            }
            label3.Text = dataGridView1.Columns.Count + " עמודות";
            label3.Text = dataGridView1.Rows.Count + " שורות";
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            reaction = true;
            _sender = sender;
            _e = e;
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            // Save as...
        }
    }
}