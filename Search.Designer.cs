namespace mGeek
{
    partial class SearchForm
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
            this.components = new System.ComponentModel.Container();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SearchBTN = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.CaseSCheck = new System.Windows.Forms.CheckBox();
            this.ListSearchBox = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.convertToHEXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToASCIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.textBox1.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(0, 26);
            this.textBox1.MaxLength = 100;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(649, 26);
            this.textBox1.TabIndex = 0;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox1_KeyDown);
            // 
            // SearchBTN
            // 
            this.SearchBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchBTN.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SearchBTN.Location = new System.Drawing.Point(655, 26);
            this.SearchBTN.Name = "SearchBTN";
            this.SearchBTN.Size = new System.Drawing.Size(115, 27);
            this.SearchBTN.TabIndex = 2;
            this.SearchBTN.Text = "Search";
            this.SearchBTN.UseVisualStyleBackColor = true;
            this.SearchBTN.Click += new System.EventHandler(this.Button1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(204, 17);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Performe Search in all Displayed Logs\r\n";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(232, 6);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(137, 17);
            this.checkBox2.TabIndex = 4;
            this.checkBox2.Text = "Highlight Search Match";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // CaseSCheck
            // 
            this.CaseSCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CaseSCheck.AutoSize = true;
            this.CaseSCheck.Checked = true;
            this.CaseSCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CaseSCheck.Location = new System.Drawing.Point(663, 6);
            this.CaseSCheck.Name = "CaseSCheck";
            this.CaseSCheck.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.CaseSCheck.Size = new System.Drawing.Size(96, 17);
            this.CaseSCheck.TabIndex = 5;
            this.CaseSCheck.Text = "Case Sensitive";
            this.CaseSCheck.UseVisualStyleBackColor = true;
            // 
            // ListSearchBox
            // 
            this.ListSearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListSearchBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(7)))), ((int)(((byte)(7)))));
            this.ListSearchBox.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.ListSearchBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.ListSearchBox.FormattingEnabled = true;
            this.ListSearchBox.ItemHeight = 16;
            this.ListSearchBox.Location = new System.Drawing.Point(-2, 54);
            this.ListSearchBox.Name = "ListSearchBox";
            this.ListSearchBox.Size = new System.Drawing.Size(773, 260);
            this.ListSearchBox.TabIndex = 6;
            this.ListSearchBox.SelectedIndexChanged += new System.EventHandler(this.ListSearchBox_SelectedIndexChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertToHEXToolStripMenuItem,
            this.convertToASCIToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 70);
            // 
            // convertToHEXToolStripMenuItem
            // 
            this.convertToHEXToolStripMenuItem.Name = "convertToHEXToolStripMenuItem";
            this.convertToHEXToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.convertToHEXToolStripMenuItem.Text = "Convert To HEX";
            this.convertToHEXToolStripMenuItem.Click += new System.EventHandler(this.convertToHEXToolStripMenuItem_Click);
            // 
            // convertToASCIToolStripMenuItem
            // 
            this.convertToASCIToolStripMenuItem.Name = "convertToASCIToolStripMenuItem";
            this.convertToASCIToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.convertToASCIToolStripMenuItem.Text = "Convert To ASCI";
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 311);
            this.Controls.Add(this.ListSearchBox);
            this.Controls.Add(this.CaseSCheck);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.SearchBTN);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(785, 350);
            this.Name = "SearchForm";
            this.Opacity = 0.5D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search";
            this.Activated += new System.EventHandler(this.SearchForm_Activated);
            this.Deactivate += new System.EventHandler(this.SearchForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SearchForm_FormClosing);
            this.Load += new System.EventHandler(this.SearchForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox CaseSCheck;
        private System.Windows.Forms.ListBox ListSearchBox;
        public System.Windows.Forms.Button SearchBTN;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem convertToHEXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToASCIToolStripMenuItem;
    }
}