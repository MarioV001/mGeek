using System;
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
            richTextBox1.Clear();
            richTextBox1.AppendText(SearchInLog(textBox1.Text, Form1.mTxtBox.Text, CaseSCheck.Checked));//COMPILE ERROR
        }
        public static string SearchInLog(string SearchStr, string InputString, bool CaseSen = false)
        {
            string[] WordsList = InputString.Split('\n');
            string Allperse = "";
            foreach (string CWord in WordsList)
            {
                if (CWord.Contains(SearchStr) == true && CaseSen == true || CWord.ToLower().Contains(SearchStr.ToLower()) == true && CaseSen == false)//found match(case sens check)
                {
                    Allperse += CWord + Environment.NewLine;
                }
            }
            return Allperse;
        }
        private void RichTextBox1_SelectionChanged(object sender, EventArgs e)
        {


        }
        void ScrollToLine(int lineNumber, RichTextBox RChTextCTRL)
        {
            if (lineNumber > RChTextCTRL.Lines.Count()) return;

            RChTextCTRL.SelectionStart = RChTextCTRL.Find(RChTextCTRL.Lines[lineNumber]);
            RChTextCTRL.ScrollToCaret();
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

        private void RichTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            int linenumber = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart);
            if (linenumber < 0 || linenumber > richTextBox1.Lines.Length) return;//Dont show if outofrange
            string NewNumber = richTextBox1.Lines[linenumber].Substring(0, 5);
            ScrollToLine(Convert.ToInt32(NewNumber), Form1.mTxtBox);
        }
    }
}
