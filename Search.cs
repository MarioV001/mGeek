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
            richTextBox1.AppendText(SearchInLog(textBox1.Text, Form1.mTxtBox.Text, CaseSCheck.Checked));
            richTextBox1.Focus();
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

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                button1.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void SearchForm_Deactivate(object sender, EventArgs e)
        {
            if (IsClosing == false) {
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
    }
}
