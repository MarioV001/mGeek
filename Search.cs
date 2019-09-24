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
                button1.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }//erorore

        private void SearchForm_Deactivate(object sender, EventArgs e)
        {
            if (IsClosing == false)
            {
                this.Opacity = Double.Parse("0." + Properties.Settings.Default.SearchOpacity);
                this.TopMost = true;
            }
        }

        private void SearchForm_Activated(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void SearchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsClosing = true;
        }

        private void ListSearchBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int linenumber = Convert.ToInt32(ListSearchBox.GetItemText(ListSearchBox.SelectedItem).Substring(0, 5));
            if (linenumber < 0) return;//Dont show if outofrange
            ScrollToLine(linenumber, Form1.mTxtBox);
        }
        void ScrollToLine(int lineNumber, RichTextBox RChTextCTRL)
        {
            if (lineNumber > RChTextCTRL.Lines.Count()) return;//makes sure we dont exceed lines
            RChTextCTRL.SelectionStart = RChTextCTRL.Find(RChTextCTRL.Lines[lineNumber]);
            RChTextCTRL.ScrollToCaret();
            RChTextCTRL.Focus();
        }
    }
}
