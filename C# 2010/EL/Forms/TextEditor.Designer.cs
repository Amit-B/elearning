namespace EL.Forms
{
    partial class TextEditor
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.קובץToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.שמירהToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.פתיחהToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.סיוםעריכהToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ביטולעריכהToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.טקסטToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.איפוסToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.בחרהכלToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.בטלToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.בצעשובToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.גזורToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.העתקToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.הדבקToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.אפשרויותToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.גלישתשורותToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.מספרתויםToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.מספרשורותToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::EL.Images.Pencil;
            this.pictureBox1.Location = new System.Drawing.Point(12, 27);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(82, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 29);
            this.label1.TabIndex = 15;
            this.label1.Text = "עריכת טקסט:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(82, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 23);
            this.label2.TabIndex = 16;
            this.label2.Text = "X";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 97);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBox1.Size = new System.Drawing.Size(468, 426);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.button1.Image = global::EL.Images.V;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(325, 529);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 31);
            this.button1.TabIndex = 2;
            this.button1.Text = "סיום";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.button2.Image = global::EL.Images.X;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(164, 529);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(155, 31);
            this.button2.TabIndex = 3;
            this.button2.Text = "ביטול";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 529);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "0 תוים";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.קובץToolStripMenuItem,
            this.טקסטToolStripMenuItem,
            this.אפשרויותToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(492, 21);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // קובץToolStripMenuItem
            // 
            this.קובץToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.שמירהToolStripMenuItem,
            this.פתיחהToolStripMenuItem,
            this.toolStripSeparator3,
            this.סיוםעריכהToolStripMenuItem,
            this.ביטולעריכהToolStripMenuItem});
            this.קובץToolStripMenuItem.Name = "קובץToolStripMenuItem";
            this.קובץToolStripMenuItem.Size = new System.Drawing.Size(43, 17);
            this.קובץToolStripMenuItem.Text = "קובץ";
            // 
            // שמירהToolStripMenuItem
            // 
            this.שמירהToolStripMenuItem.Name = "שמירהToolStripMenuItem";
            this.שמירהToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.שמירהToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.שמירהToolStripMenuItem.Text = "שמירה";
            this.שמירהToolStripMenuItem.Click += new System.EventHandler(this.שמירהToolStripMenuItem_Click);
            // 
            // פתיחהToolStripMenuItem
            // 
            this.פתיחהToolStripMenuItem.Name = "פתיחהToolStripMenuItem";
            this.פתיחהToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.פתיחהToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.פתיחהToolStripMenuItem.Text = "פתיחה";
            this.פתיחהToolStripMenuItem.Click += new System.EventHandler(this.פתיחהToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(146, 6);
            // 
            // סיוםעריכהToolStripMenuItem
            // 
            this.סיוםעריכהToolStripMenuItem.Name = "סיוםעריכהToolStripMenuItem";
            this.סיוםעריכהToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.סיוםעריכהToolStripMenuItem.Text = "סיום עריכה";
            this.סיוםעריכהToolStripMenuItem.Click += new System.EventHandler(this.סיוםעריכהToolStripMenuItem_Click);
            // 
            // ביטולעריכהToolStripMenuItem
            // 
            this.ביטולעריכהToolStripMenuItem.Name = "ביטולעריכהToolStripMenuItem";
            this.ביטולעריכהToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.ביטולעריכהToolStripMenuItem.Text = "ביטול עריכה";
            this.ביטולעריכהToolStripMenuItem.Click += new System.EventHandler(this.ביטולעריכהToolStripMenuItem_Click);
            // 
            // טקסטToolStripMenuItem
            // 
            this.טקסטToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.איפוסToolStripMenuItem,
            this.בחרהכלToolStripMenuItem,
            this.toolStripSeparator1,
            this.בטלToolStripMenuItem,
            this.בצעשובToolStripMenuItem,
            this.toolStripSeparator2,
            this.גזורToolStripMenuItem,
            this.העתקToolStripMenuItem,
            this.הדבקToolStripMenuItem});
            this.טקסטToolStripMenuItem.Name = "טקסטToolStripMenuItem";
            this.טקסטToolStripMenuItem.Size = new System.Drawing.Size(51, 17);
            this.טקסטToolStripMenuItem.Text = "עריכה";
            // 
            // איפוסToolStripMenuItem
            // 
            this.איפוסToolStripMenuItem.Name = "איפוסToolStripMenuItem";
            this.איפוסToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.איפוסToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.איפוסToolStripMenuItem.Text = "מחיקת הכל";
            this.איפוסToolStripMenuItem.Click += new System.EventHandler(this.איפוסToolStripMenuItem_Click);
            // 
            // בחרהכלToolStripMenuItem
            // 
            this.בחרהכלToolStripMenuItem.Name = "בחרהכלToolStripMenuItem";
            this.בחרהכלToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.בחרהכלToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.בחרהכלToolStripMenuItem.Text = "בחירת הכל";
            this.בחרהכלToolStripMenuItem.Click += new System.EventHandler(this.בחרהכלToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(173, 6);
            // 
            // בטלToolStripMenuItem
            // 
            this.בטלToolStripMenuItem.Name = "בטלToolStripMenuItem";
            this.בטלToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.בטלToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.בטלToolStripMenuItem.Text = "בטל";
            this.בטלToolStripMenuItem.Click += new System.EventHandler(this.בטלToolStripMenuItem_Click);
            // 
            // בצעשובToolStripMenuItem
            // 
            this.בצעשובToolStripMenuItem.Name = "בצעשובToolStripMenuItem";
            this.בצעשובToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.בצעשובToolStripMenuItem.Text = "בצע שוב";
            this.בצעשובToolStripMenuItem.Click += new System.EventHandler(this.בצעשובToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(173, 6);
            // 
            // גזורToolStripMenuItem
            // 
            this.גזורToolStripMenuItem.Name = "גזורToolStripMenuItem";
            this.גזורToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.גזורToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.גזורToolStripMenuItem.Text = "גזור";
            this.גזורToolStripMenuItem.Click += new System.EventHandler(this.גזורToolStripMenuItem_Click);
            // 
            // העתקToolStripMenuItem
            // 
            this.העתקToolStripMenuItem.Name = "העתקToolStripMenuItem";
            this.העתקToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.העתקToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.העתקToolStripMenuItem.Text = "העתק";
            this.העתקToolStripMenuItem.Click += new System.EventHandler(this.העתקToolStripMenuItem_Click);
            // 
            // הדבקToolStripMenuItem
            // 
            this.הדבקToolStripMenuItem.Name = "הדבקToolStripMenuItem";
            this.הדבקToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.הדבקToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.הדבקToolStripMenuItem.Text = "הדבק";
            this.הדבקToolStripMenuItem.Click += new System.EventHandler(this.הדבקToolStripMenuItem_Click);
            // 
            // אפשרויותToolStripMenuItem
            // 
            this.אפשרויותToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.גלישתשורותToolStripMenuItem,
            this.מספרתויםToolStripMenuItem,
            this.מספרשורותToolStripMenuItem});
            this.אפשרויותToolStripMenuItem.Name = "אפשרויותToolStripMenuItem";
            this.אפשרויותToolStripMenuItem.Size = new System.Drawing.Size(68, 17);
            this.אפשרויותToolStripMenuItem.Text = "אפשרויות";
            // 
            // גלישתשורותToolStripMenuItem
            // 
            this.גלישתשורותToolStripMenuItem.Checked = true;
            this.גלישתשורותToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.גלישתשורותToolStripMenuItem.Name = "גלישתשורותToolStripMenuItem";
            this.גלישתשורותToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.גלישתשורותToolStripMenuItem.Text = "גלישת שורות";
            this.גלישתשורותToolStripMenuItem.Click += new System.EventHandler(this.גלישתשורותToolStripMenuItem_Click);
            // 
            // מספרתויםToolStripMenuItem
            // 
            this.מספרתויםToolStripMenuItem.Checked = true;
            this.מספרתויםToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.מספרתויםToolStripMenuItem.Name = "מספרתויםToolStripMenuItem";
            this.מספרתויםToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.מספרתויםToolStripMenuItem.Text = "מספר תוים";
            this.מספרתויםToolStripMenuItem.Click += new System.EventHandler(this.מספרתויםToolStripMenuItem_Click);
            // 
            // מספרשורותToolStripMenuItem
            // 
            this.מספרשורותToolStripMenuItem.Checked = true;
            this.מספרשורותToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.מספרשורותToolStripMenuItem.Name = "מספרשורותToolStripMenuItem";
            this.מספרשורותToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.מספרשורותToolStripMenuItem.Text = "מספר שורות";
            this.מספרשורותToolStripMenuItem.Click += new System.EventHandler(this.מספרשורותToolStripMenuItem_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 545);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 22;
            this.label4.Text = "0 שורות";
            // 
            // TextEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(492, 568);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.ForeColor = System.Drawing.Color.DarkBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "TextEditor";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "עריכת טקסט";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem טקסטToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem איפוסToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem בחרהכלToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripMenuItem קובץToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem שמירהToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem פתיחהToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem סיוםעריכהToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ביטולעריכהToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem בטלToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem בצעשובToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem גזורToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem העתקToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem הדבקToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem אפשרויותToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem גלישתשורותToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem מספרתויםToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem מספרשורותToolStripMenuItem;
    }
}