using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mGeek
{
    public partial class Form1 : Form
    {
        string LastLogLoad;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mTxtBox.ReadOnly = true;
            DateTime DateNow = DateTime.Now;
            MonthSelectLogs.SelectedItem = DateNow.Month.ToString("00");

            if (Properties.Settings.Default.AutpLoadLogs == true)
            {
                ListLogs.Items.Clear();
                DirectoryInfo di = new DirectoryInfo(Properties.Settings.Default.LogsPath + DateNow.Month.ToString("00"));
                foreach (FileInfo file in FindFiles(di, "*.*"))
                {
                    // process file
                    ListLogs.Items.Add(file.ToString());
                }
            }
        }
        public IEnumerable<FileInfo> FindFiles(DirectoryInfo startDirectory, string pattern)
        {
            return startDirectory.EnumerateFiles(pattern, SearchOption.AllDirectories);
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void OpenFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            OpenLogToRead(openFileDialog1.FileName);
        }

        public void OpenLogToRead(string Path)
        {
            if (Path == null) return;
            mTxtBox.Clear();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();
            long size = new FileInfo(Path).Length;
            ReadFile(Path, size, mTxtBox, mGeekToolStripMenuItem.Checked);
            stopwatch.Stop();
            if (mGeekToolStripMenuItem.Checked == true) TimeDebug.Text = "Time To Load With mGeek: " + stopwatch.ElapsedMilliseconds + " ms";
            else TimeDebug.Text = "Time To Load: " + stopwatch.ElapsedMilliseconds + " ms";
            //mTxtBox.Focus();
        }
        public void ReadFile(string FilePath, long length, RichTextBox RchText, bool Syntax = false)
        {
            StringBuilder resultAsString = new StringBuilder();
            using (MemoryMappedFile memoryMappedFile = MemoryMappedFile.CreateFromFile(FilePath))
            using (MemoryMappedViewStream memoryMappedViewStream = memoryMappedFile.CreateViewStream(0, length))
            {
                resultAsString.Append(LineCountConvert(0));//add 0 as first line
                int LineCount = 0;
                for (int i = 0; i < length; i++)//do byte by byte
                {
                    int result = memoryMappedViewStream.ReadByte();
                    if (result == -1) break;
                    char letter = (char)result;
                    //MessageBox.Show(((byte)letter).ToString() + "  |  " + letter);
                    if ((byte)letter == 11) continue;//cantch invalid/unwanted chars(only in ASCI format)
                    resultAsString.Append(letter);
                    if (letter == '\n')
                    {
                        LineCount++;
                        resultAsString.Append(LineCountConvert(LineCount));
                    }
                }
                RchText.AppendText(resultAsString.ToString());
                if (Syntax == true) SetSyntaxForCtrl(RchText);
            }
            RchText.SelectionStart = 0;
            RchText.SelectionLength = 0;
        }
        public void SetSyntaxForCtrl(RichTextBox RchText)
        {
            StringBuilder bar = new StringBuilder();

            int Wordindex = 0, Startindex = 0;
            string TempWord = "";
            var MatchStrings = new List<string> { "7F", "start", "UDS", "Preparing", "Coding", "CAFD", "static", "PROGRAMMING", "Error", "Finalising", "Error", "ACK" , "Failed" , "BlockLength",
                                                  "opening","Starting","Download","addressRange" ,"MidCheck" , "cfg"};
            foreach (char letter in mTxtBox.Text)
            {
                byte[] bytes = Encoding.Default.GetBytes(letter.ToString());
                string UTF8Str = Encoding.UTF8.GetString(bytes);

                if (UTF8Str == " " || UTF8Str == "." || UTF8Str == ":" || UTF8Str == "|" || UTF8Str == "+" || UTF8Str == "(" || UTF8Str.ToCharArray()[0] == '"' || UTF8Str == ")"
                            || UTF8Str == "," || UTF8Str == "_" || UTF8Str == "=" || UTF8Str == ";" || UTF8Str == "{" || UTF8Str == "}" || UTF8Str == "\n" || UTF8Str == "\n")//if space
                {
                    bool contains = MatchStrings.Contains(TempWord, StringComparer.OrdinalIgnoreCase);
                    if (contains)//if match found from Match string
                    {
                        RchText.Select(Startindex, Wordindex);
                        RchText.SelectionColor = Color.IndianRed;
                        RchText.SelectionFont = new Font(RchText.Font, FontStyle.Bold);
                    }
                    Startindex = Startindex + Wordindex + 1;
                    Wordindex = 0;
                    TempWord = "";
                }
                else if (letter == '\r' || UTF8Str == "")
                {
                    //do nothing, multiple line
                }
                else
                {
                    Wordindex++;
                    TempWord = TempWord + UTF8Str;
                }
            }

        }

        public static string LineCountConvert(int InputLine)
        {
            string ConverSTR = "|" + InputLine.ToString() + "|";
            if (InputLine >= 0 & InputLine < 10) ConverSTR = "    " + InputLine + "|";
            if (InputLine >= 10 & InputLine < 100) ConverSTR = "   " + InputLine + "|";
            if (InputLine >= 100 & InputLine < 1000) ConverSTR = "  " + InputLine + "|";
            if (InputLine >= 1000 & InputLine < 10000) ConverSTR = " " + InputLine + "|";
            if (InputLine >= 10000 & InputLine < 100000) ConverSTR = "" + InputLine + "|";
            return ConverSTR;
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(mTxtBox.Lines.Length.ToString());
        }

        private void MGeekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mGeekToolStripMenuItem.Checked == false) mGeekToolStripMenuItem.Checked = true;
            else mGeekToolStripMenuItem.Checked = false;
        }

        private void Button6_MouseLeave(object sender, EventArgs e)
        {
            if (BMWPanel.Visible == false) BMWButton.BackgroundImage = Properties.Resources.CBMW;
        }

        private void Button6_MouseEnter(object sender, EventArgs e)
        {
            BMWButton.BackgroundImage = Properties.Resources.CBMW_SEL_;
        }

        private void Button1_MouseEnter(object sender, EventArgs e)
        {
            MercButton.BackgroundImage = Properties.Resources.MERC_SEL_;
        }
        private void Button1_MouseLeave(object sender, EventArgs e)
        {
            if (MERCPanel.Visible == false) MercButton.BackgroundImage = Properties.Resources.MERC;
        }
        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings SettForm = new Settings();
            SettForm.ShowDialog();
        }
        private void Button2_MouseEnter(object sender, EventArgs e)
        {
            LRButton.BackgroundImage = Properties.Resources.LR_SEL_1;
        }
        private void Button2_MouseLeave(object sender, EventArgs e)
        {
            if (LRPanel.Visible == false) LRButton.BackgroundImage = Properties.Resources.LR;
        }

        private void ListLogs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ListLogs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenLogToRead(Properties.Settings.Default.LogsPath + MonthSelectLogs.Text + @"\" + ListLogs.SelectedItem);
        }

        private void ListLogs_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (ListLogs.SelectedIndex == ListLogs.IndexFromPoint(e.X, e.Y)) contextLogsMenuStrip1.Show(MousePosition);
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (ListLogs.SelectedIndex == ListLogs.IndexFromPoint(e.X, e.Y))
                {
                    if (LastLogLoad == Properties.Settings.Default.LogsPath + MonthSelectLogs.Text + @"\" + ListLogs.SelectedItem) return;
                    LastLogLoad = Properties.Settings.Default.LogsPath + MonthSelectLogs.Text + @"\" + ListLogs.SelectedItem;
                    OpenLogToRead(LastLogLoad);
                }
            }

        }

        private void ReloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LastLogLoad = Properties.Settings.Default.LogsPath + MonthSelectLogs.Text + @"\" + ListLogs.SelectedItem;
            OpenLogToRead(LastLogLoad);
        }

        private void MTxtBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F4)//refresh Log
            {
                OpenLogToRead(LastLogLoad);
            }
            if (e.KeyData == (Keys.Control | Keys.F))
            {
                SearchForm SrcHForm = new SearchForm();
                SrcHForm.Show();
            }
        }

        public void SetLogInfo()
        {
            VersionTxt.Text = "";
        }
        private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help OpenForm = new Help();
            OpenForm.ShowDialog();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ListLogs.Items.Clear();
            DateTime DateNow = DateTime.Now;
            DirectoryInfo di = new DirectoryInfo(@Properties.Settings.Default.LogsPath + MonthSelectLogs.Text + @"\");
            //string[] array2 = Directory.GetFiles(@"\\log01.lan.autologic.com\Logs");
            //FileInfo myFile = new FileInfo(@Properties.Settings.Default.LogsPath);
            foreach (FileInfo file in FindFiles(di, "*.log"))
            {
                // process file
                if (file.ToString().ToLower().Contains(LogSearchTextBox.Text.ToLower()) == true)
                {
                    ListLogs.Items.Add(file.ToString());
                }
            }
        }

        private void LogSearchList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SearchLogsButton.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }


        public static string GetWordUnderCursor(RichTextBox control, MouseEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(control.Text))
                return null;
            var index = control.GetCharIndexFromPosition(e.Location);
            if (char.IsWhiteSpace(control.Text[index]))
                return null;
            var start = index;//find the start index of the word
            while (start > 0 && !char.IsWhiteSpace(control.Text[start - 1]))
                start--;
            var end = index;//find the end index of the word
            while (end < control.Text.Length - 1 && !char.IsWhiteSpace(control.Text[end + 1]))
                end++;
            control.SelectionStart = start;
            control.SelectionLength = end - start + 1;
            return control.Text.Substring(start, end - start + 1);
        }
        public static int GetLineUnderCursor(RichTextBox control)
        {
            int line = control.GetLineFromCharIndex(control.GetFirstCharIndexOfCurrentLine());
            return line;
        }
        private void MTxtBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                string word = GetWordUnderCursor(mTxtBox, e);
                if (word == "7F")//if we are dealing with 7F respons(get the meaning)
                {
                    string LineSv = mTxtBox.Lines[GetLineUnderCursor(mTxtBox)];//line propery false
                    string[] WordsList = LineSv.Split(' ');
                    int Index = 0;
                    foreach (string CWord in WordsList)
                    {
                        Index++;
                        if (CWord == "7F")
                        {
                            _7FMsgbox frm2 = new _7FMsgbox(Cursor.Position.X - 300, Cursor.Position.Y - 20);
                            frm2.label3.Text = "(" + WordsList[Index + 1] + ")";
                            frm2.ShowDialog();
                            break;
                        }
                    }
                }
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            SetWorkingVehicle(0);//BMW
        }
        public void ClearVHImages()
        {
            if (BMWPanel.Visible == false) BMWButton.BackgroundImage = Properties.Resources.CBMW;
            if (MERCPanel.Visible == false) MercButton.BackgroundImage = Properties.Resources.MERC;
            if (LRPanel.Visible == false) LRButton.BackgroundImage = Properties.Resources.LR;
        }
        public void SetWorkingVehicle(int index)
        {
            BMWPanel.Visible = false;
            MERCPanel.Visible = false;
            LRPanel.Visible = false;
            if (index == 0) BMWPanel.Visible = true;
            if (index == 1) MERCPanel.Visible = true;
            if (index == 2) LRPanel.Visible = true;
            ClearVHImages();
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            SetWorkingVehicle(1);//MERC
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SetWorkingVehicle(2);//LR
        }

        private void ListLogs_MouseHover(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ExtendSearcHover == true) ListLogs.Height = this.Height - 200;
        }

        private void ListLogs_MouseLeave(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ExtendSearcHover == true) ListLogs.Height = 150;
        }
    }
}
