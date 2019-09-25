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
        string CurrentBrandBrowse;
        public Form1()
        {
            InitializeComponent();
        }
        public void RefreshLogFiles()
        {
            List<string> logListContent = new List<string>();
            //I:\bmw\2019\
            DateTime DateNow = DateTime.Now;
            try
            {
                logListContent = Directory.GetFiles(@Properties.Settings.Default.LogsPath + @"\" + CurrentBrandBrowse + @"\" + DateNow.Year.ToString("0000") + @"\" + MonthSelectLogs.Text + @"\").ToList();
                logListContent = logListContent.OrderByDescending(x => x).ToList();
            }
            catch (IOException ioex)
            {
                MessageBox.Show(ioex.Message, "Did not find path!");
                return ;
            }
            
            for(int count = 0; count < logListContent.Count(); count++)//clear out the names
                {
                string[] words = logListContent[count].Split('\\');
                logListContent[count] = words[words.Count()-1];
            }
            ListLogs.DataSource = null;
            ListLogs.DataSource = logListContent.ToArray();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SetAppTheme(Properties.Settings.Default.Theme);
            if (Properties.Settings.Default.LoadLastBrand == true) SetWorkingVehicle(Properties.Settings.Default.LastBrandBrowsing,Properties.Settings.Default.AutpLoadLogs);
             else SetWorkingVehicle(0, Properties.Settings.Default.AutpLoadLogs);
            //load log into RichTxt
            mTxtBox.ReadOnly = true;
            DateTime DateNow = DateTime.Now;
            MonthSelectLogs.SelectedItem = DateNow.Month.ToString("00");

        }
        private void refreshLogsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshLogFiles();
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
            FileStream file = File.OpenRead(FilePath);
            using (MemoryMappedFile mappedFile = MemoryMappedFile.CreateFromFile(file, "PEIMAGE", file.Length, MemoryMappedFileAccess.Read, null, 0, false))
            using (var viewStream = mappedFile.CreateViewStream(0, file.Length, MemoryMappedFileAccess.Read))
            {
                resultAsString.Append(LineCountConvert(0));//add 0 as first line
                int LineCount = 0;
                for (int i = 0; i < length; i++)//do byte by byte
                {
                    int result = viewStream.ReadByte();
                    if (result == -1) break;
                    char letter = (char)result;
                    //MessageBox.Show(((byte)letter).ToString() + "  |  " + letter);//for debbuging unwanted Chars
                    if ((byte)letter == 11 || (byte)letter == 17 || (byte)letter == 0) continue;//cantch invalid/unwanted chars(only in ASCI format)|----Ù = 217
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
                            || UTF8Str == "," || UTF8Str == "_" || UTF8Str == "=" || UTF8Str == ";" || UTF8Str == "{" || UTF8Str == "}" || UTF8Str == "\n" || UTF8Str == "\n")//using this to separete and cout word positions
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
                else if (letter == '\r')
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
            if (LastLogLoad == "") MessageBox.Show("Open a log first!");
            //K:\Tools\Log2SDF\
            if (CurrentBrandBrowse == "bmw") System.Diagnostics.Process.Start(@"K:\Tools\Log2SDF\BMW\CreateSDF.exe", LastLogLoad);
            if (CurrentBrandBrowse == "merc") System.Diagnostics.Process.Start(@"K:\Tools\Log2SDF\Mercedes\log-to-sdf.exe", LastLogLoad);
            if (CurrentBrandBrowse == "lr") System.Diagnostics.Process.Start(@"K:\Tools\Log2SDF\JLR\JLRLog2Sdf.exe", LastLogLoad);
            if (CurrentBrandBrowse == "vag") System.Diagnostics.Process.Start(@"K:\Tools\Log2SDF\JLR\log2sdf.exe", LastLogLoad);
            if (CurrentBrandBrowse == "vovlo") System.Diagnostics.Process.Start(@"K:\Tools\Log2SDF\JLR\log2sdf.exe", LastLogLoad);

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
            DateTime DateNow = DateTime.Now;
            OpenLogToRead(@Properties.Settings.Default.LogsPath + @"\" + CurrentBrandBrowse + @"\" + DateNow.Year.ToString("0000") + @"\" + MonthSelectLogs.Text + @"\" + ListLogs.SelectedItem);
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
                    DateTime DateNow = DateTime.Now;
                    if (LastLogLoad == @Properties.Settings.Default.LogsPath + @"\" + CurrentBrandBrowse + @"\" + DateNow.Year.ToString("0000") + @"\" + MonthSelectLogs.Text + @"\" + ListLogs.SelectedItem) return;
                    LastLogLoad = @Properties.Settings.Default.LogsPath + @"\" + CurrentBrandBrowse + @"\" + DateNow.Year.ToString("0000") + @"\" + MonthSelectLogs.Text + @"\" + ListLogs.SelectedItem;
                    OpenLogToRead(LastLogLoad);
                }
            }

        }

        private void ReloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LastLogLoad = Properties.Settings.Default.LogsPath + MonthSelectLogs.Text + @"\" + ListLogs.SelectedItem;
            OpenLogToRead(LastLogLoad);
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
            ListLogs.DataSource = null;
            ListLogs.Items.Clear();
            DateTime DateNow = DateTime.Now;
            //load all files in arrey first
            List<string> logListContent = new List<string>();
            List<string> SearchList = new List<string>();
            logListContent = Directory.GetFiles(@Properties.Settings.Default.LogsPath + @"\" + CurrentBrandBrowse + @"\" + DateNow.Year.ToString("0000") + @"\" + MonthSelectLogs.Text + @"\").ToList();
            logListContent = logListContent.OrderByDescending(x => x).ToList();
            for (int count = 0; count < logListContent.Count(); count++)//clear out the names
            {
                string[] words = logListContent[count].Split('\\');
                if (logListContent[count].ToLower().Contains(LogSearchTextBox.Text.ToLower()) == true) SearchList.Add(words[words.Count() - 1]);
            }
            ListLogs.DataSource = SearchList.ToArray();


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
        public void SetAppTheme(int Theme)
        {
            if (Theme == 0)//Default
            {
                this.BackColor = Color.White;

                //Controls
                ListLogs.BackColor = Color.Transparent;
                mTxtBox.BackColor = Color.Transparent;
            }
            else if (Theme == 1){//Dark Blue
                this.BackColor = Color.FromArgb(17, 36, 56);
                //Controls
                ListLogs.BackColor = Color.FromArgb(34, 60, 69);
                mTxtBox.BackColor = Color.FromArgb(34, 60, 69);
            }else if (Theme == 2){//Dark Silver
                this.BackColor = Color.FromArgb(73,82,97);
                //Controls
                ListLogs.BackColor = Color.FromArgb(48,54,64);
                mTxtBox.BackColor = Color.FromArgb(48, 54, 64);
            }

                return;
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
        public void SetWorkingVehicle(int index,bool RefreshLog =true)
        {
            BMWPanel.Visible = false;
            MERCPanel.Visible = false;
            LRPanel.Visible = false;
            tabControl1.SelectedTab = tabPage1;
            tabPage2.Hide();//CarLogs
            tabPage3.Hide();//CIP Logs
            if (index == 0){//if BMW
                BMWPanel.Visible = true;
                CurrentBrandBrowse = "bmw";
                tabPage2.Show();//CarLogs
                tabPage3.Show();
            }
            else if (index == 1){//if Mercedes
                MERCPanel.Visible = true;
                CurrentBrandBrowse = "merc";
            }else if (index == 2){//if LandRover
                LRPanel.Visible = true;
                CurrentBrandBrowse = "lr";
            }else if (index == 3){//IF JAG
                //LRPanel.Visible = true;
                CurrentBrandBrowse = "jag";
            }else if (index == 4) {//if VAG
                //LRPanel.Visible = true;
                CurrentBrandBrowse = "vag";
            }
            ClearVHImages();
            if(RefreshLog==true) RefreshLogFiles();//re load the log list
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
            if (Properties.Settings.Default.ExtendSearcHover == true && ListLogs.Items.Count >= 9) ListLogs.Height = this.Height - 200;
        }

        private void ListLogs_MouseLeave(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ExtendSearcHover == true) ListLogs.Height = 150;
        }

        private void mTxtBox_KeyDown(object sender, KeyEventArgs e)
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

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchForm SrcHForm = new SearchForm();
            SrcHForm.Show();
        }
    }
}
