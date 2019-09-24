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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.CaseSCheck = new System.Windows.Forms.CheckBox();
            this.ListSearchBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(0, 27);
            this.textBox1.MaxLength = 100;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(649, 26);
            this.textBox1.TabIndex = 0;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox1_KeyDown);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(651, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 27);
            this.button1.TabIndex = 2;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
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
            this.ListSearchBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(7)))), ((int)(((byte)(7)))));
            this.ListSearchBox.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.ListSearchBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.ListSearchBox.FormattingEnabled = true;
            this.ListSearchBox.ItemHeight = 16;
            this.ListSearchBox.Location = new System.Drawing.Point(0, 50);
            this.ListSearchBox.Name = "ListSearchBox";
            this.ListSearchBox.Size = new System.Drawing.Size(764, 260);
            this.ListSearchBox.TabIndex = 6;
            this.ListSearchBox.SelectedIndexChanged += new System.EventHandler(this.ListSearchBox_SelectedIndexChanged);
            // 
            // SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 308);
            this.Controls.Add(this.ListSearchBox);
            this.Controls.Add(this.CaseSCheck);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SearchForm";
            this.Opacity = 0.5D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search";
            this.Activated += new System.EventHandler(this.SearchForm_Activated);
            this.Deactivate += new System.EventHandler(this.SearchForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SearchForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox CaseSCheck;
        private System.Windows.Forms.ListBox ListSearchBox;
    }
}