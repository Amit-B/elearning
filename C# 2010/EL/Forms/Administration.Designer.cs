namespace EL.Forms
{
    partial class Administration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("תפריט ראשי");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("עריכת הערה מהירה");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("שליחת דואר כוללת");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("קבצי מידע");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("ניהול מקצועות");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("נהלים והוראות", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("רשימת המורים");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("הוספת מורה");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("עריכת מורה");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("מחנכי כיתות");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("ניהול מורים", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10});
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("נתוני העדרויות");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("נתוני הערות משמעת");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("נתוני ציונים");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("נתוני שיעורים");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("מונה");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("סטטיסטיקות", new System.Windows.Forms.TreeNode[] {
            treeNode12,
            treeNode13,
            treeNode14,
            treeNode15,
            treeNode16});
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("גיבוי ושחזור המערכת");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("סל המחזור");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("תחזוקה", new System.Windows.Forms.TreeNode[] {
            treeNode18,
            treeNode19});
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("הגדרות כלליות");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("ניהול תפקידים");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("הרשאות");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("הגדרות", new System.Windows.Forms.TreeNode[] {
            treeNode21,
            treeNode22,
            treeNode23});
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("יצירת קשר");
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 29);
            this.label1.TabIndex = 16;
            this.label1.Text = "ניהול ראשי";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(12, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(475, 487);
            this.panel1.TabIndex = 127;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.button1.Image = global::EL.Images.TreeView;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(493, 501);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 27);
            this.button1.TabIndex = 2;
            this.button1.Text = "הסתרת הכל";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button8
            // 
            this.button8.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.button8.Image = global::EL.Images.TreeView;
            this.button8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button8.Location = new System.Drawing.Point(609, 501);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(110, 27);
            this.button8.TabIndex = 1;
            this.button8.Text = "הצגת הכל";
            this.button8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(493, 41);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "Node0";
            treeNode1.Text = "תפריט ראשי";
            treeNode2.Name = "Node4";
            treeNode2.Text = "עריכת הערה מהירה";
            treeNode3.Name = "Node0";
            treeNode3.Text = "שליחת דואר כוללת";
            treeNode4.Name = "Node18";
            treeNode4.Text = "קבצי מידע";
            treeNode5.Name = "Node0";
            treeNode5.Text = "ניהול מקצועות";
            treeNode6.Name = "Node1";
            treeNode6.Text = "נהלים והוראות";
            treeNode7.Name = "Node6";
            treeNode7.Text = "רשימת המורים";
            treeNode8.Name = "Node7";
            treeNode8.Text = "הוספת מורה";
            treeNode9.Name = "Node8";
            treeNode9.Text = "עריכת מורה";
            treeNode10.Name = "Node0";
            treeNode10.Text = "מחנכי כיתות";
            treeNode11.Name = "Node2";
            treeNode11.Text = "ניהול מורים";
            treeNode12.Name = "Node2";
            treeNode12.Text = "נתוני העדרויות";
            treeNode13.Name = "Node3";
            treeNode13.Text = "נתוני הערות משמעת";
            treeNode14.Name = "Node4";
            treeNode14.Text = "נתוני ציונים";
            treeNode15.Name = "Node5";
            treeNode15.Text = "נתוני שיעורים";
            treeNode16.Name = "Node6";
            treeNode16.Text = "מונה";
            treeNode17.Name = "Node1";
            treeNode17.Text = "סטטיסטיקות";
            treeNode18.Name = "Node8";
            treeNode18.Text = "גיבוי ושחזור המערכת";
            treeNode19.Name = "Node9";
            treeNode19.Text = "סל המחזור";
            treeNode20.Name = "Node7";
            treeNode20.Text = "תחזוקה";
            treeNode21.Name = "Node20";
            treeNode21.Text = "הגדרות כלליות";
            treeNode22.Name = "Node2";
            treeNode22.Text = "ניהול תפקידים";
            treeNode23.Name = "Node1";
            treeNode23.Text = "הרשאות";
            treeNode24.Name = "Node19";
            treeNode24.Text = "הגדרות";
            treeNode25.Name = "Node0";
            treeNode25.Text = "יצירת קשר";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode6,
            treeNode11,
            treeNode17,
            treeNode20,
            treeNode24,
            treeNode25});
            this.treeView1.RightToLeftLayout = true;
            this.treeView1.Size = new System.Drawing.Size(226, 454);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect_1);
            // 
            // Administration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(731, 540);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.ForeColor = System.Drawing.Color.DarkBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Administration";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ניהול ראשי";
            this.Load += new System.EventHandler(this.Administration_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView treeView1;
    }
}