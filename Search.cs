﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mGeek
{
    public partial class SearchForm : Form
    {
        public SearchForm()
        {
            InitializeComponent();
        }
        bool IsClosing = false;
        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "") return;
            ListSearchBox.Items.Clear();
            SearchInLog(ListSearchBox, textBox1.Text, Form1.mTxtBox.Text, CaseSCheck.Checked);//COMPILE ERROR
        }
        public static string SearchInLog(ListBox ListCTRL, string SearchStr, string InputString, bool CaseSen = false)
        {
            string[] WordsList = InputString.Split('\n');
            foreach (string CWord in WordsList)
            {
                if (CWord.Contains(SearchStr) == true && CaseSen == true || CWord.ToLower().Contains(SearchStr.ToLower()) == true && CaseSen == false)//found match(case sens check)
                {
                    ListCTRL.Items.Add(CWord);
                }
            }
            return "";
        }
        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SearchBTN.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }//erorore

        private void SearchForm_Deactivate(object sender, EventArgs e)
        {
            if (IsClosing == false)
            {
                this.Opacity = Double.Parse("0." + Properties.Settings.Default.SearchOpacity);
                //this.TopMost = true;
            }
        }

        private void SearchForm_Activated(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void SearchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsClosing = true;
            Properties.Settings.Default.CaseSensSearch = CaseSCheck.Checked;
        }

        private void ListSearchBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListSearchBox.SelectedIndex == -1) return;//Dont show if out of range
            int linenumber = Convert.ToInt32(ListSearchBox.GetItemText(ListSearchBox.SelectedItem).Substring(0, 5));
            ScrollToLine(linenumber, Form1.mTxtBox);
        }
        void ScrollToLine(int lineNumber, RichTextBox RChTextCTRL)
        {
            if (lineNumber > RChTextCTRL.Lines.Count()) return;//makes sure we dont exceed lines
            RChTextCTRL.SelectionStart = RChTextCTRL.Find(RChTextCTRL.Lines[lineNumber]);
            RChTextCTRL.ScrollToCaret();
            RChTextCTRL.Focus();
        }

        private void SearchForm_Load(object sender, EventArgs e)
        {
            CaseSCheck.Checked = Properties.Settings.Default.CaseSensSearch;
            //theme
            if (Properties.Settings.Default.Theme == 1)
            {
                this.BackColor = Color.FromArgb(43, 53, 73);//Dark Blue
                checkBox1.ForeColor = Color.White;
                checkBox2.ForeColor = Color.White;
                CaseSCheck.ForeColor = Color.White;
            }
            if (Properties.Settings.Default.Theme == 2)
            {
                this.BackColor = Color.FromArgb(55, 65, 75);//Dark Silver
                checkBox1.ForeColor = Color.White;
                checkBox2.ForeColor = Color.White;
                CaseSCheck.ForeColor = Color.White;
            }
        }

        private void convertToHEXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = ToHex(textBox1.Text);
        }
        private void convertToASCIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = ToAscii(textBox1.Text);
        }

        public static string ToHex(string input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in input)
                sb.AppendFormat("0x{0:X2} ", (int)c);
            return sb.ToString().Replace("0x", "").Trim();
        }

        public static string ToAscii(string input)
        {
            input = input.Replace(" ", "");

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < input.Length; i += 2)
            {
                try
                {
                    string hs = input.Substring(i, 2);
                    sb.Append(Convert.ToChar(Convert.ToUInt32(hs, 16)));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return sb.ToString();
        }
    }
}
