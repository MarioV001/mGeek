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

        private void Button1_Click(object sender, EventArgs e)
        {
            Form1 MainF = new Form1();
            string LogText = Form1.Get_LogText(MainF.mTxtBox);
            MessageBox.Show(LogText);
            richTextBox1.AppendText(SearchInLog(textBox1.Text, LogText, CaseSCheck.Checked));
            
        }
        public static string SearchInLog(string SearchStr, string InputString, bool CaseSen = false)
        {
            string[] WordsList = InputString.Split('\n');
            string Allperse = "";
            foreach (string CWord in WordsList)
            {
                if (CWord.Contains(SearchStr) == true && CaseSen == false || CWord.ToLower().Contains(SearchStr.ToLower()) == true && CaseSen == true)//found match(case sens check)
                {
                    Allperse += CWord + Environment.NewLine;
                }
            }
            return Allperse;
        }
    }
}
