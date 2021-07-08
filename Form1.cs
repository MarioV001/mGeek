using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace mGeek
{
    public partial class Form1 : Form
    {
        string LastLogLoad;
        public string CurrentBrandBrowse;
        public string WorkingInSubfolder = "";
        public static int CurrentCaseTimerUpdate = 0;
        private int MAxScroll=0;
        public Form1()
        {
            InitializeComponent();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        public void RefreshLogFiles()
        {
            List<string> logListContent = new List<string>();
            DataSet SearchList = new DataSet();
            SearchList.Tables.Add("Logs");
            SearchList.Tables[0].Columns.Add("Logs", typeof(string)).MaxLength = 200;
            //I:\bmw\2019\
            
            try
            {
                logListContent = Directory.GetFiles(@Properties.Settings.Default.LogsPath + @"\" + CurrentBrandBrowse + @"\" + YearComboBox.SelectedItem + @"\" + MonthSelectLogs.Text + @"\" + WorkingInSubfolder, "*.log").ToList();
                logListContent = logListContent.OrderByDescending(x => x).ToList();
            }
            catch (IOException ioex)
            {
                MessageBox.Show(ioex.Message, "Did not find path!");
                return;
            }

            for (int count = 0; count < logListContent.Count(); count++)//clear out the names
            {
                string[] words = logListContent[count].Split('\\');
                SearchList.Tables[0].Rows.Add(words[words.Count() - 1]);
            }
            LogViewDataGrid.DataSource = SearchList.Tables[0];
            LogViewDataGrid.Columns[0].Width = 277;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SetAppTheme(Properties.Settings.Default.Theme);
            if (Properties.Settings.Default.LoadLastBrand == true) SetWorkingVehicle(Properties.Settings.Default.LastBrandBrowsing, Properties.Settings.Default.AutpLoadLogs);
            else SetWorkingVehicle(0, Properties.Settings.Default.AutpLoadLogs);
            //load log into RichTxt
            mTxtBox.ReadOnly = true;
            DateTime DateNow = DateTime.Now;
            MonthSelectLogs.SelectedItem = DateNow.Month.ToString("00");
            //load all years
            foreach (string s in Directory.GetDirectories(Properties.Settings.Default.LogsPath + @"\dir"))
            {
                YearComboBox.Items.Add(s.Remove(0,7));
            }
                //
            YearComboBox.SelectedItem = DateNow.Year.ToString("0000");
            if (Properties.Settings.Default.UsingCOMsType == 2) radioButton2.Checked = true;
            else if (Properties.Settings.Default.UsingCOMsType == 1) radioButton1.Checked = true;
            ////ADD extra features\\\\
            pictureBox4.Image = RotateImage(Properties.Resources.Down);
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
            LogSearchTextBox.Text = openFileDialog1.FileName;
        }

        private void WorkerDoWork(string Path)
        {
            long size = new FileInfo(Path).Length;
            ReadFile(Path, size, mTxtBox, mGeekToolStripMenuItem.Checked);
        }

        /// <summary>
        /// ///////////////////////////////////////////////--------[LOG READER]--------/////////////////////////////--------[END]--------///////////////////////
        /// </summary>
        void LogReadComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            CaseTimerCount.Start();//runs every Minute!
            mTxtBox.HideSelection = false;
            //mTxtBox.Focus();
            //
            SearchForm form = Application.OpenForms.OfType<SearchForm>().FirstOrDefault();
            if (form != null && form.checkBox1.Checked == false) form.SearchBTN.PerformClick();
            //set the max scroll
            MAxScroll = mTxtBox.GetPositionFromCharIndex(mTxtBox.Text.Length).Y ;
            if(Properties.Settings.Default.ExtendedLogDetails==true)//After Load Complete, run extended info for log
            {
                string line;
                try
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(@"I:\data\" + YearComboBox.Text + @"\" + MonthSelectLogs.Text + @"\" + LogViewDataGrid.SelectedCells[0].Value.ToString().Replace(".log", ".txt"));
                    while ((line = file.ReadLine()) != null)
                    {
                        if (line.Contains("VIN") == true) VINLabel.Text = line.Replace("VIN:", "");
                        if (line.Contains("VehicleDescription") == true) label6.Text = line.Replace("VehicleDescription:", "");
                    }
                }
                catch
                {
                    VINLabel.Text = "VIN: -!";
                    label6.Text = "Model: -!";
                }
                
            }
            
        }


            public void OpenLogToRead(string Path)
        {
            if (Path == null) return;
            mTxtBox.HideSelection = true;
            mTxtBox.Clear();
            CaseTimerCount.Stop();
            CurrentCaseTimerUpdate = 0;//reset Case Time(in Minutes)
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();
            if (File.Exists(Path) == false) { ShowNoteWindows("Log Not Found!"); return; }
            //create Backround worker
            BeginUpdate();//disable form controls

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (obj, e) => WorkerDoWork(Path);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(LogReadComplete);
            worker.RunWorkerAsync();
            //Time
            stopwatch.Stop();
            if (mGeekToolStripMenuItem.Checked == true) TimeDebug.Text = "Time To Load With SyntaxH: " + stopwatch.ElapsedMilliseconds + " ms";
            else TimeDebug.Text = "Time To Load: " + stopwatch.ElapsedMilliseconds + " ms";
            if (Properties.Settings.Default.LogDetailsLoad == true)//if Auto loading details from LOG
            {

            }
            
        }

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

        private const int WM_SETREDRAW = 11;
        public void BeginUpdate()
        {
            SendMessage(this.Handle, WM_SETREDRAW, false, 0);
        }
        public void EndUpdate()
        {
            SendMessage(this.Handle, WM_SETREDRAW, true, 0);
            this.Refresh();
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
                    if ((byte)letter == 11 || (byte)letter == 17 || (byte)letter == 0 || (byte)letter == 13 || (byte)letter == 12) continue;//cantch invalid/unwanted chars(only in ASCI format)|----Ù = 217
                    resultAsString.Append(letter);
                    if (letter == '\n')
                    {
                        LineCount++;
                        resultAsString.Append(LineCountConvert(LineCount));
                    }
                }
                
                RchText.Invoke(new Action(() => RchText.AppendText(resultAsString.ToString())));
                mappedFile.Dispose();
               
                
            }
            if (Syntax == true) SetSyntaxForCtrlEX(RchText);
            RchText.Invoke(new Action(() => RchText.SelectionStart = 0));
            RchText.Invoke(new Action(() => RchText.SelectionLength = 0));
        }
        public void SetSyntaxForCtrl(RichTextBox RchText)
        {
            StringBuilder bar = new StringBuilder();

            int Wordindex = 0, Startindex = 0;
            string TempWord = "";
            var MatchStrings = new List<string> { "7F", "start", "UDS", "Preparing", "Coding", "CAFD", "static", "PROGRAMMING", "Error", "Finalising", "Error", "ACK" , "Failed" , "BlockLength",
                                                  "opening","Starting","Download","addressRange" ,"MidCheck" , "cfg"};
            string Output = ""; ;
            RchText.Invoke(new Action(() => mTxtBox.SuspendLayout()));
            mTxtBox.Invoke(new MethodInvoker(delegate { Output = mTxtBox.Text; }));//consider changing to not user memmory
            foreach (char letter in Output)
            {
                byte[] bytes = Encoding.Default.GetBytes(letter.ToString());
                string UTF8Str = Encoding.UTF8.GetString(bytes);

                if (UTF8Str == " " || UTF8Str == "." || UTF8Str == ":" || UTF8Str == "|" || UTF8Str == "+" || UTF8Str == "(" || UTF8Str.ToCharArray()[0] == '"' || UTF8Str == ")"
                            || UTF8Str == "," || UTF8Str == "_" || UTF8Str == "=" || UTF8Str == ";" || UTF8Str == "{" || UTF8Str == "}" || UTF8Str == "\n" || UTF8Str == "\n")//using this to separete and cout word positions
                {
                    bool contains = MatchStrings.Contains(TempWord, StringComparer.OrdinalIgnoreCase);
                    if (contains)//if match found from Match string
                    {
                        RchText.Invoke(new Action(() => RchText.Select(Startindex, Wordindex)));
                        RchText.Invoke(new Action(() => RchText.SelectionColor = Color.IndianRed));
                        RchText.Invoke(new Action(() => RchText.SelectionFont = new Font(RchText.Font, FontStyle.Bold)));
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
            this.Invoke(new Action(() => EndUpdate()));
        }

        public void SetSyntaxForCtrlEX(RichTextBox RchText)//faster
        {
            StringBuilder bar = new StringBuilder();
            List<string> MatchStrings = new List<string> { "7F", "start", "UDS", "Preparing", "Coding", "CAFD", "static", "PROGRAMMING", "Error", "Finalising", "Error", "ACK" , "Failed" , "BlockLength",
                                                  "opening","Starting","Download","addressRange" ,"MidCheck" , "cfg"};
            RchText.Invoke(new Action(() => mTxtBox.SuspendLayout()));
            foreach (var WordSer in MatchStrings)
            {
                MatchCollection mColl = Regex.Matches((string)mTxtBox.Invoke(new Func<String>(() => mTxtBox.Text)), WordSer);
                foreach (System.Text.RegularExpressions.Match g in mColl)
                {
                    RchText.Invoke(new Action(() => RchText.Select(g.Index, g.Length)));
                    RchText.Invoke(new Action(() => RchText.SelectionColor = Color.IndianRed));
                    RchText.Invoke(new Action(() => RchText.SelectionFont = new Font(RchText.Font, FontStyle.Bold)));
                }
            }
            this.Invoke(new Action(() => EndUpdate()));
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

        /// <summary>
        /// ///////////////////////////////////////////////--------[LOG READER]--------///////////////////--------[END]--------/////////////////////////
        /// </summary>
        /// 
        /// <summary>
        /// ///////////////////////////////////////////////--------[PROCESS SDF SIMULATION]--------///////-------[START]-------/////////////////////////
        /// </summary>
        private void Button4_Click(object sender, EventArgs e)
        {
            if (LastLogLoad == "") MessageBox.Show("Open a log first!");
            //K:\Tools\Log2SDF\
            ProcessStartInfo start_info = null;
            bool ExtendedWorkDone = false;
            DateTime DateNow = DateTime.Now;
            if (CurrentBrandBrowse == "bmw") start_info = new ProcessStartInfo(@"K:\Tools\Log2SDF\BMW\CreateSDF.exe", LastLogLoad);
            if (CurrentBrandBrowse == "merc") start_info = new ProcessStartInfo(@"K:\Tools\Log2SDF\Mercedes\log-to-sdf.exe", GetPathForSDFToBeCreated(CurrentBrandBrowse) + @"\" + LogViewDataGrid.SelectedCells[0].Value.ToString());
            if (CurrentBrandBrowse == "lr" || CurrentBrandBrowse == "jag")
            {
                if(CheckJLRLogProcess(LastLogLoad)>=2) ExtendedWorkDone = true;
                else start_info = new ProcessStartInfo(@"K:\Tools\Log2SDF\JLR\JLRLog2Sdf.exe",LastLogLoad);
            }
            if (CurrentBrandBrowse == "vag") start_info = new ProcessStartInfo(@"K:\Tools\Log2SDF\VAG\log2sdf.exe", LastLogLoad);
            if (CurrentBrandBrowse == "volvo") start_info = new ProcessStartInfo(@"K:\Tools\Log2SDF\Volvo\"+ ManualVolvoLog2SdfExe(), ManualVolvoProtocol() + " " + GetPathForSDFToBeCreated(CurrentBrandBrowse) + @"\" + LogViewDataGrid.SelectedCells[0].Value.ToString());
            if (CurrentBrandBrowse == "porsche") start_info = new ProcessStartInfo(@"K:\Tools\Log2SDF\Porsche\LogToSDF.exe", GetPathForSDFToBeCreated(CurrentBrandBrowse) + @"\" + LogViewDataGrid.SelectedCells[0].Value.ToString());
            //start the proc
            Process proc = new Process();
            if (ExtendedWorkDone==false)
            { 
                proc.StartInfo = start_info;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.ErrorDialog = false;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
            }
            //show reader first
            ConsolRead OpenForm = new ConsolRead();
            OpenForm.Left = this.Bounds.Right - 360;
            OpenForm.Top = this.Bounds.Top + 35;
            if (ExtendedWorkDone == false) OpenForm.Show();
            //check/Set Up Things Before we start Lot2SDF
            if (CurrentBrandBrowse == "merc" | CurrentBrandBrowse == "porsche" |
                CurrentBrandBrowse == "volvo") SetUpSDFPath();
            //Start
            if (ExtendedWorkDone == false)
            {
                proc.Start();
                OpenForm.TopMost = true;
                //
                ConsolRead f1 = (ConsolRead)Application.OpenForms["ConsolRead"];
                TextBox tb = (TextBox)f1.Controls["textBox1"];
                var reader = proc.StandardOutput;
                while (!reader.EndOfStream)
                {
                    tb.Text += reader.ReadLine();
                }
                // Wait for LogProce2Finish to finish.
                proc.WaitForExit();
                proc.Close();
            }
            //Check for file Re-Ordering
            CheckLogReOrdering();
            //Check if we need to open Megasim

            if (CurrentBrandBrowse == "lr" | CurrentBrandBrowse == "jag") Process.Start(@"X:\diagnos\megasim\MegaSIM.exe", @"K:\SDF\" + GetFullBrandName().ToUpper() + @"\" + LogViewDataGrid.SelectedCells[0].Value.ToString().Replace(".log", GetTrueComsTypeFromat(CurrentBrandBrowse, GetCurrentComStatus()) + ".sdf") + " " + Properties.Settings.Default.UsingCOMsType);
            if (CurrentBrandBrowse == "merc") Process.Start(@"X:\diagnos\megasim_merc\MegaSIM.exe", GetPathForSDFToBeCreated(CurrentBrandBrowse) + @"\" + LogViewDataGrid.SelectedCells[0].Value.ToString().Replace(".log", ".log" + GetTrueComsTypeFromat(CurrentBrandBrowse, GetCurrentComStatus())) + " " + Properties.Settings.Default.UsingCOMsType);
            if (CurrentBrandBrowse == "porsche") Process.Start(@"X:\diagnos\megasim\MegaSIM.exe", GetPathForSDFToBeCreated(CurrentBrandBrowse) + @"\" + LogViewDataGrid.SelectedCells[0].Value.ToString() + GetTrueComsTypeFromat(CurrentBrandBrowse, GetCurrentComStatus()) + " " + Properties.Settings.Default.UsingCOMsType);
            if (CurrentBrandBrowse == "volvo") Process.Start(@"X:\diagnos\megasim\MegaSIM.exe", GetPathForSDFToBeCreated(CurrentBrandBrowse) + @"\" + LogViewDataGrid.SelectedCells[0].Value.ToString().Replace(".log", GetTrueComsTypeFromat(CurrentBrandBrowse, GetCurrentComStatus())) + " " + Properties.Settings.Default.UsingCOMsType);



        }

        private void SetUpSDFPath(string brand="")
        {
                DateTime DateNow = DateTime.Now;
                string SetFolderUP = GetPathForSDFToBeCreated(CurrentBrandBrowse);
                if (!Directory.Exists(SetFolderUP)) Directory.CreateDirectory(SetFolderUP); //if Dir Doesnt exist then creat it...
                File.Copy(LastLogLoad, SetFolderUP + @"\" + LogViewDataGrid.SelectedCells[0].Value.ToString(), true);//Copy File to the Directory
        }
        private string GetPathForSDFToBeCreated(string BRAND)
        {
            DateTime DateNow = DateTime.Now;
            if (BRAND == "merc") return @"K:\SDF\MERCEDES\" + YearComboBox.SelectedItem + @"\" + DateNow.Month.ToString("0") + @"\" + LogViewDataGrid.SelectedCells[0].Value.ToString().Replace(".log", "");
            if (BRAND == "porsche") return @"K:\SDF\PORSCHE\" + YearComboBox.SelectedItem + @"\" + DateNow.Month.ToString("00") + @"\" + LogViewDataGrid.SelectedCells[0].Value.ToString().Replace(".log", "");
            if (BRAND == "volvo") return @"K:\SDF\VOLVO\" + YearComboBox.SelectedItem + @"\" + DateNow.Month.ToString("00") + @"\" + LogViewDataGrid.SelectedCells[0].Value.ToString().Replace(".log", "");
            return "";
        }
        private void MGeekToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mGeekToolStripMenuItem.Checked == false) mGeekToolStripMenuItem.Checked = true;
            else mGeekToolStripMenuItem.Checked = false;
        }
        private int CheckJLRLogProcess(string logprocess)
        {
            int Is7E8logFile = 0;
            foreach (string line in File.ReadAllLines(logprocess).Take(100))
            {
                if (line.Contains("can_filter_config"))//7E8 coms
                {
                    Is7E8logFile = 1;
                    break;

                }
                else if (line.Contains("can_setup_filter_from_script"))//F1 0F coms(need python log 2sdf)
                {
                    Is7E8logFile = 2;
                    ShowNoteWindows("Using Python2SDF - CAN Setup", 3, 2800);
                    break;
                }
                else if (line.Contains("Calling C#: KWP2000")){
                    Is7E8logFile = 4;
                    ShowNoteWindows("Using Python2SDF (KWP2000)", 0, 4500);
                    break;
                }
            }
            //process if needed
            if (Is7E8logFile >= 2)//run python script
            {
                string[] FileName = logprocess.Split('\\');
                File.Copy(logprocess, @"K:\Tools\Log2SDF\Experimental\V\" + FileName[FileName.Length - 1], true);
                //Run python
                Process process = new Process();
                //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                process.StartInfo.FileName = @"C:\Program Files (x86)\Microsoft Visual Studio\Shared\Python36_64\python.exe";
                process.StartInfo.WorkingDirectory = @"K:\Tools\Log2SDF\Experimental\V";
                process.StartInfo.Arguments = @"LRCAN29Sdf.py " + FileName[FileName.Length - 1];
                process.Start();
                process.WaitForExit();
                //move files
                if (File.Exists(@"K:\SDF\LANDROVER\" + FileName[FileName.Length - 1].Replace(".log", "_125.sdf")) == true) File.Delete(@"K:\SDF\LANDROVER\" + FileName[FileName.Length - 1].Replace(".log", "_125.sdf"));
                if (File.Exists(@"K:\SDF\LANDROVER\" + FileName[FileName.Length - 1].Replace(".log", "_500.sdf")) == true) File.Delete(@"K:\SDF\LANDROVER\" + FileName[FileName.Length - 1].Replace(".log", "_500.sdf"));
                File.Move(@"K:\Tools\Log2SDF\Experimental\V\" + FileName[FileName.Length - 1].Replace(".log", "_125.sdf"), @"K:\SDF\LANDROVER\" + FileName[FileName.Length - 1].Replace(".log", "_125.sdf"));
                File.Move(@"K:\Tools\Log2SDF\Experimental\V\" + FileName[FileName.Length - 1].Replace(".log", "_500.sdf"), @"K:\SDF\LANDROVER\" + FileName[FileName.Length - 1].Replace(".log", "_500.sdf"));
                File.Delete(@"K:\Tools\Log2SDF\Experimental\V\" + FileName[FileName.Length - 1]);
            }
            return Is7E8logFile;
        }
        private void CheckLogReOrdering()
        {

            if (CurrentBrandBrowse == "jag")
            {
                if (File.Exists(@"K:\SDF\JAGUAR\" + LogViewDataGrid.SelectedCells[0].Value.ToString().Replace(".log", ".sdf")) == false)
                {
                    File.Copy(@"K:\SDF\LANDROVER\" + LogViewDataGrid.SelectedCells[0].Value.ToString().Replace(".log", ".sdf"), @"K:\SDF\JAGUAR\" + LogViewDataGrid.SelectedCells[0].Value.ToString().Replace(".log", ".sdf"), true);
                    if (File.Exists(@"K:\SDF\LANDROVER\" + LogViewDataGrid.SelectedCells[0].Value.ToString().Replace(".log", "_500.sdf")) == true)//if file is create(log is processed)
                    {
                        File.Copy(@"K:\SDF\LANDROVER\" + LogViewDataGrid.SelectedCells[0].Value.ToString().Replace(".log", "_500.sdf"), @"K:\SDF\JAGUAR\" + LogViewDataGrid.SelectedCells[0].Value.ToString().Replace(".log", "_500.sdf"), true);
                        File.Copy(@"K:\SDF\LANDROVER\" + LogViewDataGrid.SelectedCells[0].Value.ToString().Replace(".log", "_125.sdf"), @"K:\SDF\JAGUAR\" + LogViewDataGrid.SelectedCells[0].Value.ToString().Replace(".log", "_125.sdf"), true);
                        File.Delete(@"K:\SDF\LANDROVER\" + LogViewDataGrid.SelectedCells[0].Value.ToString().Replace(".log", "_500.sdf"));
                        File.Delete(@"K:\SDF\LANDROVER\" + LogViewDataGrid.SelectedCells[0].Value.ToString().Replace(".log", "_125.sdf"));
                    }
                    else//if not copy the original to both speeds
                    {
                        File.Copy(@"K:\SDF\LANDROVER\" + LogViewDataGrid.SelectedCells[0].Value.ToString().Replace(".log", ".sdf"), @"K:\SDF\JAGUAR\" + LogViewDataGrid.SelectedCells[0].Value.ToString().Replace(".log", "_500.sdf"), true);
                        File.Copy(@"K:\SDF\LANDROVER\" + LogViewDataGrid.SelectedCells[0].Value.ToString().Replace(".log", ".sdf"), @"K:\SDF\JAGUAR\" + LogViewDataGrid.SelectedCells[0].Value.ToString().Replace(".log", "_125.sdf"), true);
                    }
                    File.Delete(@"K:\SDF\LANDROVER\" + LogViewDataGrid.SelectedCells[0].Value.ToString().Replace(".log", ".sdf"));
                }
            }
        }

        /// <summary>
        /// ///////////////////////////////////////////////--------[PROCESS SDF SIMULATION]--------///////-------[END]-------/////////////////////////
        /// </summary>
        /// 
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
            LRButton.BackgroundImage = Properties.Resources.LR_SEL_;
        }
        private void Button2_MouseLeave(object sender, EventArgs e)
        {
            if (LRPanel.Visible == false) LRButton.BackgroundImage = Properties.Resources.LR;
        }

        private void ListLogs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenLogToRead(@Properties.Settings.Default.LogsPath + @"\" + CurrentBrandBrowse + @"\" + YearComboBox.SelectedItem + @"\" + MonthSelectLogs.Text + @"\" + WorkingInSubfolder + LogViewDataGrid.SelectedCells[0].Value);
        }

        private void ReloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LastLogLoad = Properties.Settings.Default.LogsPath + @"\" + CurrentBrandBrowse + @"\" + YearComboBox.SelectedItem + @"\" + MonthSelectLogs.Text + @"\" + WorkingInSubfolder + LogViewDataGrid.SelectedCells[0].Value;
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
        /// <summary>
        /// ///////////////////////////////////////////////--------[Backround Workers]--------/////////////////////////////----------------///////////////////////
        /// </summary>
        private void BackroundLogsWorker(object sender, DoWorkEventArgs e)
        {
            List<string> logListContent = new List<string>();
            logListContent = Directory.GetFiles(@Properties.Settings.Default.LogsPath + @"\" + CurrentBrandBrowse + @"\" + YearComboBox.Invoke(new Func<String>(() => YearComboBox.SelectedItem.ToString())) + @"\" + MonthSelectLogs.Invoke(new Func<String>(() => MonthSelectLogs.Text)) + @"\" + WorkingInSubfolder, "*.log").ToList();
            e.Result = logListContent;
        }
        void work_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<string> logListContent = (e.Result as List<string>);
            DataSet SearchList = new DataSet();
            SearchList.Tables.Add("Logs");
            SearchList.Tables[0].Columns.Add("Logs", typeof(string)).MaxLength = 200;
            logListContent = logListContent.OrderByDescending(x => x).ToList();
            for (int count = 0; count < logListContent.Count(); count++)//clear out the names
            {
                string[] words = logListContent[count].Split('\\');
                if (logListContent[count].ToLower().Contains(LogSearchTextBox.Text.ToLower()) == true) SearchList.Tables[0].Rows.Add(words[words.Count() - 1]);
            }

            //ListLogs.DataSource = SearchList;
            LogViewDataGrid.DataSource = SearchList.Tables[0];
            LogViewDataGrid.Columns[0].Width = 277;
            LoadingGIF.Visible = false;
            logListContent.Clear();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            //load all files in arrey first
            LoadingGIF.Visible=true;
            //
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(BackroundLogsWorker);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(work_RunWorkerCompleted);
            worker.RunWorkerAsync();
        }
        /// ///////////////////////////////////////////////--------[Backround Workers]--------/////////////////////////////--------[END]--------///////////////////////
        private void LogSearchList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                SearchLogsButton.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
        public Color GetCurrentThemeControlColor()
        {
            if (Properties.Settings.Default.Theme == 1) return Color.FromArgb(48, 54, 64);
            if (Properties.Settings.Default.Theme == 2) return Color.FromArgb(48, 54, 64);
            return Color.DarkSlateGray;//if none of above return 0 default
        }
        public void SetAppTheme(int Theme)
        {
            if (Theme == 0)//Default
            {
                this.BackColor = Color.White;
                //Controls
                LogViewDataGrid.BackgroundColor = Color.DarkSlateGray;
                LogViewDataGrid.GridColor = Color.DarkSlateGray;
                mTxtBox.BackColor = Color.DarkSlateGray;
                UpDownImage.BackColor= Color.DarkSlateGray;
                pictureBox4.BackColor = Color.DarkSlateGray;
                label4.ForeColor = Color.Black;
            }
            else if (Theme == 1)
            {//Dark Blue
                this.BackColor = Color.FromArgb(43, 53, 73);
                //Controls
                LogViewDataGrid.BackgroundColor = Color.FromArgb(48, 54, 64);
                LogViewDataGrid.GridColor = Color.FromArgb(48, 54, 64);
                LogViewDataGrid.DefaultCellStyle.BackColor = Color.FromArgb(48, 54, 64);
                mTxtBox.BackColor = Color.FromArgb(48, 54, 64);
                UpDownImage.BackColor = Color.FromArgb(48, 54, 64);
                pictureBox4.BackColor = Color.FromArgb(48, 54, 64);
                label4.ForeColor = Color.White;
            }
            else if (Theme == 2)
            {//Dark Silver
                this.BackColor = Color.FromArgb(55, 65, 75);
                //Controls
                LogViewDataGrid.BackgroundColor = Color.FromArgb(48, 54, 64);
                LogViewDataGrid.GridColor = Color.FromArgb(48, 54, 64);
                LogViewDataGrid.DefaultCellStyle.BackColor = Color.FromArgb(48, 54, 64);
                mTxtBox.BackColor = Color.FromArgb(48, 54, 64);
                UpDownImage.BackColor = Color.FromArgb(48, 54, 64);
                pictureBox4.BackColor = Color.FromArgb(48, 54, 64);
                label4.ForeColor = Color.White;
            }
           
            return;
        }

        /// ///////////////////////////////////////////////--------[Log Read Functions]--------/////////////////////////////--------[START]--------///////////////////////
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
            openCFGToolStripMenuItem.Visible = false;
            openCFGWithVSC.Visible = false;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                string word = GetWordUnderCursor(mTxtBox, e);
                if (word == "" || word == null) return;//if nothing is under cursor, then dont continue code                
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
                           
                            int position = Cursor.Position.X; if (position < 335 & position > 0) position = 0;
                            if (position < 0) //Secondary Display
                            {
                                if (position < (Screen.PrimaryScreen.WorkingArea.Width -335) * -1) position = Screen.PrimaryScreen.WorkingArea.Width *-1;//if to end of screen
                                if (Screen.PrimaryScreen.WorkingArea.Width - (position * -1) > 335) position = position - 335;
                            }
                            if (position >= 335) position = position - 335;//if there is enough space on the left to fit
                            _7FMsgbox frm2 = new _7FMsgbox(position, Cursor.Position.Y - 20);
                            frm2.label3.Text = "(" + Regex.Replace(WordsList[Index + 1], @"[^\d]", "") + ")";
                            frm2.ShowDialog();
                            break;
                        }
                    }
                }
                else if (word.Contains("|Error"))//if we are dealing with error message try transalting it...
                {
                    string LineSv = mTxtBox.Lines[GetLineUnderCursor(mTxtBox)];//line propery false
                    string[] WordsList = LineSv.Split('|');
                    //open  Error Browser
                    ErrorMSGBox frm2 = new ErrorMSGBox(Cursor.Position.X-10, Cursor.Position.Y - 10);
                    frm2.label3.Text = "(" + Regex.Replace(WordsList[1].Substring(6), @"[^\d]", "") + ")";
                    frm2.ShowDialog();
                }
                else if (word.ToLower().Contains(".cfg,"))//if we are dealing with CFG files(try opening it)
                {
                    string LineSv = mTxtBox.Lines[GetLineUnderCursor(mTxtBox)];//line propery false
                    string[] WordsList = LineSv.Split(new char[] { ',', ' ', '.', '/' }, StringSplitOptions.None);
                    int Index = 0;//could simplify by using WordsLsit.Length-5 (need 2 test)
                    foreach (string CWord in WordsList)
                    {
                        Index++;
                        if (CWord.ToLower() == "cfg")
                        {
                            //MessageBox.Show(WordsList[WordsList.Length -5]);//For Debugging
                            openCFGToolStripMenuItem.Visible = true;
                            openCFGToolStripMenuItem.Text = "Open: " + WordsList[Index - 2] + ".cfg";
                            openCFGWithVSC.Visible = true;
                            openCFGWithVSC.Text = "Open: " + WordsList[Index - 2] + ".cfg With VSCode";
                            LogContextMenu.Show();
                            break;
                        }
                    }
                }
            }
        }

        private void OpenCFGFileInEditor(bool UseVSCodeEditor = false)
        {
            if (File.Exists("filemap.ini") == false) { MessageBox.Show("Data Files Have Not Been Mapped Yet!." + Environment.NewLine + "Go To: Tools > Settings > Log Reader Tools > Map File Paths", "'filemap.ini' Not Found!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }
            string[] WordsList = mTxtBox.SelectedText.Split(new char[] { ',', ' ', '.', '/' }, StringSplitOptions.None);
            var lines = File.ReadAllLines("filemap.ini");
            bool Found = false;
            for (var i = 0; i < lines.Length; i += 1)
            {
                if (lines[i].ToLower().Contains("[" + GetFullBrandName() + "]" + WordsList[0].ToLower() + "." + WordsList[1].ToLower() + "|") == true)
                {
                    string[] GetPath = lines[i].Split('|');
                    //MessageBox.Show(GetPath[1] + " -f" + "[" + WordsList[2] + "]");//Debug
                    Process ffmpeg = new Process();
                    if (UseVSCodeEditor == true) ffmpeg.StartInfo.FileName = "Code";//Open it with VSCode
                    else ffmpeg.StartInfo.FileName = "Uedit32.exe";//Open it with ultra edit
                    if (WordsList.Length >= 4) ffmpeg.StartInfo.Arguments = GetPath[1] + " -f" + "[" + WordsList[2].ToLower() + "]";//VS Code doesnt support search
                    else ffmpeg.StartInfo.Arguments = GetPath[1];
                    ffmpeg.Start();
                    Found = true;
                    break;
                }
                // Process line
            }
            if (Found == false) MessageBox.Show("Could Not Find File Path From FileMap List!." + Environment.NewLine + "Go To: Tools > Settings > Log Reader Tools > Map File Paths", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


        }
        /// ///////////////////////////////////////////////--------[Log Read Functions]--------/////////////////////////////--------[END]--------///////////////////////
        /// 
        private void Button6_Click(object sender, EventArgs e)
        {
            SetWorkingVehicle(0);//BMW
        }
        public void ClearVHImages()
        {
            if (BMWPanel.Visible == false) BMWButton.BackgroundImage = Properties.Resources.CBMW;
            if (MERCPanel.Visible == false) MercButton.BackgroundImage = Properties.Resources.MERC;
            if (LRPanel.Visible == false) LRButton.BackgroundImage = Properties.Resources.LR;
            if (JAGpanel.Visible == false) JAGButton.BackgroundImage = Properties.Resources.JAG;
            if (VOLPanel.Visible == false) VolvoButton.BackgroundImage = Properties.Resources.Volvo_icon;
            if (PORPanel.Visible == false) PORButton.BackgroundImage = Properties.Resources.Porche;
        }
        
        private void Button1_Click_1(object sender, EventArgs e)
        {
            SetWorkingVehicle(1);//MERC
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            SetWorkingVehicle(2);//LR
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
                SrcHForm.Show(this);
            }
            if(e.KeyData == (Keys.Control | Keys.D))//Bookmark
            {
                LogViewerMenuStrip.Show();

                saveLogToFavouritesToolStripMenuItem.Select();
                toolStripTextBox1.Select();
            }
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchForm SrcHForm = new SearchForm();
            SrcHForm.Show(this);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0) WorkingInSubfolder = "";//No subfolder
            if (tabControl1.SelectedIndex == 1) WorkingInSubfolder = @"carlogs\";//CarLogs
            SearchLogsButton.PerformClick();
        }

        private void driveDataFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"I:\dir\2019\");
        }

        private void datasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"Q:\CIP\SP_4_17_13\datas");
        }

        private void dataDumpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"Q:\CIP\SP_4_17_13\psdzdata\swe\cafd\");
        }
        private void bMWCIPDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@"Q:\CIP\SP_4_19_30\psdzdata\kiswb\F056\TABLES\TECHNISCHEEINHEIT");
        }

        private void LogSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            LogSearchTextBox.Text = LogSearchTextBox.Text.Replace(" ", "_");
            LogSearchTextBox.SelectionStart = LogSearchTextBox.Text.Length;
        }

        private void openCFGToolStripMenuItem_Click(object sender, EventArgs e)//open selected CFG
        {
            OpenCFGFileInEditor(false);
        }
        public string GetFullBrandName(bool Extended =false)
        {
            if (CurrentBrandBrowse == "merc") return "mercedes";
            if (CurrentBrandBrowse == "lr" & Extended == true) return "land rover";
            if (CurrentBrandBrowse == "lr") return "landrover";
            if (CurrentBrandBrowse == "jag") return "jaguar";
            if (CurrentBrandBrowse == "vag") return "vag";
            if (CurrentBrandBrowse == "porsche") return "porsche";
            if (CurrentBrandBrowse == "volvo") return "volvo";
            if (CurrentBrandBrowse == "psar") return "renault";
            if (CurrentBrandBrowse == "ford") return "ford";
            return CurrentBrandBrowse;
        }
        
        private void bMWToolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EndianConverter OpenForm = new EndianConverter();
            OpenForm.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FavLogsViewer OpenForm = new FavLogsViewer(Cursor.Position.X + 10, Cursor.Position.Y - 40);
            OpenForm.Show();
        }
        private void saveLogToFavouritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LastLogLoad == null) return;
            string[] PathSplit = LastLogLoad.Split('\\');
            if (toolStripTextBox1.Text == "Description/Notes") toolStripTextBox1.Text = "";
            Properties.Settings.Default.FavouriteLogsSaved += CurrentBrandBrowse + "|" + PathSplit[PathSplit.Length - 1] + "|" + YearComboBox.Text + "|" + MonthSelectLogs.Text + "|" + toolStripTextBox1.Text + "@";//add to list log ID full
            toolStripTextBox1.Text = "Description/Notes";
            LogViewerMenuStrip.Close();
            ShowNoteWindows("Log Saved To Favourites",1);
        }
        public void ShowNoteWindows(string Message,int ConditionColor=0, int timeToShow=1500)
        {
            textBox1.Text = Message;
            if (ConditionColor == 0) textBox1.BackColor = Color.Firebrick;//RED
            if (ConditionColor == 1) textBox1.BackColor = Color.LightGreen;//Green
            if (ConditionColor == 2) textBox1.BackColor = Color.CadetBlue;//UpdateState
            textBox1.Visible = true;
            if (timeToShow == -1) return;
            NoteResetTimer.Interval = timeToShow;
            NoteResetTimer.Start();
        }

        private void NoteResetTimer_Tick(object sender, EventArgs e)
        {
            NoteResetTimer.Stop();
            textBox1.Visible = false;
        }

        private void searchToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(mTxtBox.SelectedText);
        }
        public void SetLogSearchString(string input)
        {
            LogSearchTextBox.Text = input;
        }

        private void toolStripDropDownButton5_Click(object sender, EventArgs e)//open remote Servers Window
        {
            RemoteServerConnections OpenForm = new RemoteServerConnections(Cursor.Position.X -20, Cursor.Position.Y - 10);
            OpenForm.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if(groupBox1.Visible ==false)
            {
                groupBox1.Visible = true;
                pictureBox2.Image = Properties.Resources.back;
            }
            else{//hide it
                groupBox1.Visible = false;
                pictureBox2.Image = Properties.Resources.play_next;
            }
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true) Properties.Settings.Default.UsingCOMsType = 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton2.Checked==true) Properties.Settings.Default.UsingCOMsType = 2;
        }

        private void VolvoButton_Click(object sender, EventArgs e)
        {
            SetWorkingVehicle(6);//Volvo
        }

        private void PORButton_Click(object sender, EventArgs e)
        {
            SetWorkingVehicle(5);//Porch
        }

        private void JAGButton_Click(object sender, EventArgs e)
        {
            SetWorkingVehicle(3);//Jag
        }

        private void VolvoButton_MouseEnter(object sender, EventArgs e)
        {
            VolvoButton.BackgroundImage = Properties.Resources.Volvo_Highlighted;
        }

        private void VolvoButton_MouseLeave(object sender, EventArgs e)
        {
            if(VOLPanel.Visible==false) VolvoButton.BackgroundImage = Properties.Resources.Volvo_icon;
        }

        private void PORButton_MouseEnter(object sender, EventArgs e)
        {
            PORButton.BackgroundImage = Properties.Resources.Porche_HIGH;
        }

        private void PORButton_MouseLeave(object sender, EventArgs e)
        {
            if(PORPanel.Visible==false) PORButton.BackgroundImage = Properties.Resources.Porche;
        }

        private void JAGButton_MouseEnter(object sender, EventArgs e)
        {
            JAGButton.BackgroundImage = Properties.Resources.JAG_High;
        }

        private void JAGButton_MouseLeave(object sender, EventArgs e)
        {
            if(JAGpanel.Visible==false) JAGButton.BackgroundImage = Properties.Resources.JAG;
        }

        private void CaseTimerCount_Tick(object sender, EventArgs e)
        {
            CurrentCaseTimerUpdate+=1;
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            CaseTimer OpenForm = new CaseTimer();
            OpenForm.Show();
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            var totalMinutes = Form1.CurrentCaseTimerUpdate;
            var time = TimeSpan.FromMinutes(totalMinutes);
            label3.Text = "Current Case Time: " + time.Hours.ToString() + ":" + time.Minutes.ToString();
            label3.Visible = true;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            label3.Visible = false;
        }

        private void codingFileExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CodingFileExplorer OpenForm = new CodingFileExplorer();
            OpenForm.Show();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (LastLogLoad == @Properties.Settings.Default.LogsPath + @"\" + CurrentBrandBrowse + @"\" + YearComboBox.SelectedItem + @"\" + MonthSelectLogs.Text + @"\" + WorkingInSubfolder + LogViewDataGrid.SelectedCells[0].Value.ToString()) return;
            LastLogLoad = @Properties.Settings.Default.LogsPath + @"\" + CurrentBrandBrowse + @"\" + YearComboBox.SelectedItem + @"\" + MonthSelectLogs.Text + @"\" + WorkingInSubfolder + LogViewDataGrid.SelectedCells[0].Value.ToString();
            OpenLogToRead(LastLogLoad);
        }

        private void dataGridView1_MouseHover(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ExtendSearcHover == true && LogViewDataGrid.Rows.Count >= 9) LogViewDataGrid.Height = this.Height - 200;

        }

        private void dataGridView1_MouseLeave(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ExtendSearcHover == true) LogViewDataGrid.Height = 148;
        }

        private void openCFGWithVSCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenCFGFileInEditor(true);
        }
        private string GetCurrentComStatus()
        {
            if (radioButton1.Checked == true) return comboBox1.Text;
            else if (radioButton2.Checked == true) return comboBox2.Text;
            return "";
        }
        
        public void SetWorkingVehicle(int index, bool RefreshLog = true)
        {
            BMWPanel.Visible = false;
            MERCPanel.Visible = false;
            LRPanel.Visible = false;
            JAGpanel.Visible = false;
            PORPanel.Visible = false;
            VOLPanel.Visible = false;
            tabControl1.SelectedTab = tabPage1;
            tabPage2.Hide();//CarLogs
            tabPage3.Hide();//CIP Logs
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            if (index == 0)
            {//if BMW
                BMWPanel.Visible = true;
                CurrentBrandBrowse = "bmw";
                tabPage2.Show();//CarLogs
                tabPage3.Show();
                comboBox1.Items.Add("HS"); comboBox1.Items.Add("MS"); comboBox1.SelectedIndex = Properties.Settings.Default.ComsTypeSelectedBMW;
                comboBox2.Items.Add("HS"); comboBox2.Items.Add("MS"); comboBox2.SelectedIndex = Properties.Settings.Default.ComsTypeSelectedBMW2;
            }
            else if (index == 1)//if Mercedes
            {
                MERCPanel.Visible = true;
                CurrentBrandBrowse = "merc";
                comboBox1.Items.Add("CAN"); comboBox1.Items.Add("SERIAL"); comboBox1.Items.Add("SERIAL 9600"); comboBox1.Items.Add("STAR CAN"); comboBox1.SelectedIndex = Properties.Settings.Default.ComsTypeSelectedMERC;
                comboBox2.Items.Add("CAN"); comboBox2.Items.Add("SERIAL"); comboBox2.Items.Add("SERIAL 9600"); comboBox2.Items.Add("STAR CAN"); comboBox2.SelectedIndex = Properties.Settings.Default.ComsTypeSelectedMERC2;
            }
            else if (index == 2)//if LandRover
            {
                LRPanel.Visible = true;
                CurrentBrandBrowse = "lr";
                comboBox1.Items.Add("HS"); comboBox1.Items.Add("MS"); comboBox1.SelectedIndex = Properties.Settings.Default.ComsTypeSelectedLR;
                comboBox2.Items.Add("HS"); comboBox2.Items.Add("MS"); comboBox2.SelectedIndex = Properties.Settings.Default.ComsTypeSelectedLR2;
            }
            else if (index == 3)//IF JAG
            {
                JAGpanel.Visible = true;
                CurrentBrandBrowse = "jag";
                comboBox1.Items.Add("HS"); comboBox1.Items.Add("MS"); comboBox1.SelectedIndex = Properties.Settings.Default.ComsTypeSelectedJAG;
                comboBox2.Items.Add("HS"); comboBox2.Items.Add("MS"); comboBox2.SelectedIndex = Properties.Settings.Default.ComsTypeSelectedJAG2;
            }
            else if (index == 4)//if VAG
            {
                //LRPanel.Visible = true;
                CurrentBrandBrowse = "vag";
                comboBox1.Items.Add("KW"); comboBox1.Items.Add("SERIAL"); comboBox1.SelectedIndex = 0;
                comboBox2.Items.Add("KW"); comboBox2.Items.Add("SERIAL"); comboBox2.SelectedIndex = 0;
            }
            else if (index == 5)//if porsche
            {
                PORPanel.Visible = true;
                CurrentBrandBrowse = "porsche";
                comboBox1.Items.Add("CAN"); comboBox1.Items.Add("KW2000"); comboBox1.SelectedIndex = Properties.Settings.Default.ComsTypeSelectedPORCHE;
                comboBox2.Items.Add("CAN"); comboBox2.Items.Add("KW2000"); comboBox2.SelectedIndex = Properties.Settings.Default.ComsTypeSelectedPORCHE2;
            }
            else if (index == 6)//if volvo
            {
                VOLPanel.Visible = true;
                CurrentBrandBrowse = "volvo";
                comboBox1.Items.AddRange(new string[] { "CF11 MS", "CF11 HS", "CF29 HS", "CF29 MS", "D2 HS", "D2 MS" }); comboBox1.SelectedIndex = Properties.Settings.Default.ComsTypeSelectedVOLVO;
                comboBox2.Items.AddRange(new string[] { "CF11 MS", "CF11 HS", "CF29 HS", "CF29 MS", "D2 HS", "D2 MS" }); comboBox2.SelectedIndex = Properties.Settings.Default.ComsTypeSelectedVOLVO2;
            }
            else if (index == 7)//if psar
            {
                //LRPanel.Visible = true;
                CurrentBrandBrowse = "psar";
                comboBox1.Items.Add("CAN"); comboBox1.Items.Add("KW2000"); comboBox1.Items.Add("PSA2"); comboBox1.SelectedIndex = 0;
                comboBox2.Items.Add("CAN"); comboBox2.Items.Add("KW2000"); comboBox2.Items.Add("PSA2"); comboBox2.SelectedIndex = 0;
            }
            else if (index == 8)//if FORD
            {
                //LRPanel.Visible = true;
                CurrentBrandBrowse = "ford";
                comboBox1.Items.Add("HS"); comboBox1.Items.Add("MS"); comboBox1.SelectedIndex = 0;
                comboBox2.Items.Add("HS"); comboBox2.Items.Add("MS"); comboBox2.SelectedIndex = 0;
            }
            ClearVHImages();
            if (RefreshLog == true) RefreshLogFiles();//re load the log list
        }
        private string GetTrueComsTypeFromat(string Brand, string COMS)
        {
            if (Brand == "merc")
            {
                if (COMS == "CAN") return ".can.sdf";
                if (COMS == "SERIAL") return ".serial.sdf";
                if (COMS == "SERIAL 9600") return ".serial9600.sdf";
                if (COMS == "STAR CAN") return ".STAR_can.sdf";
            }
            else if (Brand == "lr" | Brand == "jag")
            {
                if (COMS == "HS") return "_500";
                if (COMS == "MS") return "_125";
            }
            else if (Brand == "porsche")
            {
                if (COMS == "CAN") return "_CAN.SDF";
                if (COMS == "KW2000") return "_SER.SDF";
            }
            else if (Brand == "volvo")
            {
                if (COMS == "CF11 HS") return "_CF11_500.sdf";
                if (COMS == "CF11 MS") return "_CF11_125.sdf";
                if (COMS == "D2 HS") return "_CAN500.sdf";
                if (COMS == "D2 MS") return "_CAN125.sdf";
                if (COMS == "CF29 HS") return "_CF29_500.sdf";
                if (COMS == "CF29 MS") return "_CF29_125.sdf";
            }
            return "";
        }

        private string ManualVolvoLog2SdfExe()
        {
            if (GetCurrentComStatus() == "CF11 HS") return "format_Logs_cf.exe";
            if (GetCurrentComStatus() == "CF11 MS") return "format_Logs_cf.exe";
            if (GetCurrentComStatus() == "D2 HS") return "format_Logs_can.exe";
            if (GetCurrentComStatus() == "D2 MS") return "format_Logs_can.exe";
            if (GetCurrentComStatus() == "CF29 HS") return "format_Logs_cf.exe";
            if (GetCurrentComStatus() == "CF29 MS") return "format_Logs_cf.exe";
            return "log2sdf.exe";
        }
        private string ManualVolvoProtocol()
        {
            if (GetCurrentComStatus() == "CF11 HS") return "/TCF11";
            if (GetCurrentComStatus() == "CF11 MS") return "/TCF11";
            if (GetCurrentComStatus() == "D2 HS") return "";
            if (GetCurrentComStatus() == "D2 MS") return "";
            if (GetCurrentComStatus() == "CF29 HS") return "/TCF29";
            if (GetCurrentComStatus() == "CF29 MS") return "/TCF29";
            return "";
        }
        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter) saveLogToFavouritesToolStripMenuItem.PerformClick();
        }

        private void copyFullLogIDToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void copyFullLogIDToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Clipboard.SetText(LogViewDataGrid.SelectedCells[0].Value.ToString());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrentBrandBrowse == "bmw") Properties.Settings.Default.ComsTypeSelectedBMW = comboBox1.SelectedIndex;
            if (CurrentBrandBrowse == "merc") Properties.Settings.Default.ComsTypeSelectedMERC = comboBox1.SelectedIndex;
            if (CurrentBrandBrowse == "lr") Properties.Settings.Default.ComsTypeSelectedLR = comboBox1.SelectedIndex;
            if (CurrentBrandBrowse == "jag") Properties.Settings.Default.ComsTypeSelectedJAG = comboBox1.SelectedIndex;
            if (CurrentBrandBrowse == "porsche") Properties.Settings.Default.ComsTypeSelectedPORCHE = comboBox1.SelectedIndex;
            if (CurrentBrandBrowse == "volvo") Properties.Settings.Default.ComsTypeSelectedVOLVO = comboBox1.SelectedIndex;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CurrentBrandBrowse == "bmw") Properties.Settings.Default.ComsTypeSelectedBMW2 = comboBox2.SelectedIndex;
            if (CurrentBrandBrowse == "merc") Properties.Settings.Default.ComsTypeSelectedMERC2 = comboBox2.SelectedIndex;
            if (CurrentBrandBrowse == "lr") Properties.Settings.Default.ComsTypeSelectedLR2 = comboBox2.SelectedIndex;
            if (CurrentBrandBrowse == "jag") Properties.Settings.Default.ComsTypeSelectedJAG2 = comboBox2.SelectedIndex;
            if (CurrentBrandBrowse == "porsche") Properties.Settings.Default.ComsTypeSelectedPORCHE2 = comboBox2.SelectedIndex;
            if (CurrentBrandBrowse == "volvo") Properties.Settings.Default.ComsTypeSelectedVOLVO2 = comboBox2.SelectedIndex;
        }

        private void BackroundLogViewer_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void UpDownImage_Click(object sender, EventArgs e)
        {
            mTxtBox.SelectionStart = mTxtBox.Text.Length;
            mTxtBox.ScrollToCaret();
        }
        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            pictureBox4.Image = RotateImage(Properties.Resources.Down_SEL_);
        }
        private void UpDownImage_MouseEnter(object sender, EventArgs e)
        {
            UpDownImage.Image = Properties.Resources.Down_SEL_;
        }

        private void UpDownImage_MouseLeave(object sender, EventArgs e)
        {
            UpDownImage.Image = Properties.Resources.Down;
        }

        public Image RotateImage(Image img)
        {
            img.RotateFlip(RotateFlipType.Rotate180FlipNone);
            return img;
        }
            private void mTxtBox_VScroll(object sender, EventArgs e)
        {
            VersionTxt.Text = GetCurrentScrollValue() + "|" + GetMAxScrollValue();
            if (GetCurrentScrollValue() > GetMAxScrollValue() -800)//if scrolled more the 500 show go to top
            {
                UpDownImage.Visible = false;
            }else UpDownImage.Visible = true;
            if (GetCurrentScrollValue() > 600) pictureBox4.Visible = true;
            else pictureBox4.Visible = false;
        }
        private int GetMAxScrollValue()
        {
            return (MAxScroll - mTxtBox.Height) + 38;
        }
        private int GetCurrentScrollValue()
        {
            return mTxtBox.GetPositionFromCharIndex(0).Y * -1;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Image = RotateImage(Properties.Resources.Down);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            mTxtBox.SelectionStart = 0;
            mTxtBox.ScrollToCaret();
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)//Get_Unit_Information
        {
            if (LogViewDataGrid.SelectedCells.Count ==0) return;
            label4.Visible = true;
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            string[] UnitN = LogViewDataGrid.SelectedCells[0].Value.ToString().Split('_');
            if (!UnitN[1].Contains("A03"))
            {
                string path;
                char splitCondition = ',';
                if (UnitN[1].Contains("A02")) path = @"I:\AssistPlus\" + UnitN[1] + @" \Y_" + UnitN[1] + "_SOFTWARESTATUS.CSV";//A+
                else//BB
                {
                    path = @"I:\\Autolink\swInfo\" + UnitN[1] + @" \";
                    var infoF = new DirectoryInfo(path).GetFiles().OrderByDescending(o => o.LastWriteTime).FirstOrDefault();
                    path += infoF.Name;
                    splitCondition = ':';
                }
                System.IO.StreamReader file = new System.IO.StreamReader(path);
                string line, Versions = "UNIT: [" + UnitN[1].Remove(0, 2) + "]" + Environment.NewLine;
                while ((line = file.ReadLine()) != null)
                {
                    string[] Splitter = line.Split(splitCondition);
                    if (line.Contains("MAIN") == true) Versions += "MAIN: " + Splitter[1] + Environment.NewLine;
                    if (line.Contains(GetFullBrandName(true).ToUpper()) == true) Versions += GetFullBrandName(true).ToUpper() + ": " + Splitter[1] + Environment.NewLine;
                    if (line.Contains("installer") == true) Versions += "INS: " + Splitter[1];
                }
                label4.Text = Versions;
            }
            else label4.Text = "UNIT: [" + UnitN[1].Remove(0, 2) + "]" + Environment.NewLine + "Unsupported";
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            label4.Text = "";
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            ExtendedLogInfo OpenForm = new ExtendedLogInfo();
            OpenForm.Show();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if(VINLabel.Text.Length > 10)
            {
                SearchForm SrcHForm = new SearchForm();
                SrcHForm.Show(this);
                TextBox tb = (TextBox)SrcHForm.Controls["TextToSearchBox"];
                tb.Text = SearchForm.ToHex(VINLabel.Text);
                SrcHForm.checkBox1.Checked = true;
                SrcHForm.SearchBTN.PerformClick();
                SrcHForm.checkBox1.Checked = false;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(VINLabel.Text);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(SearchForm.ToHex(VINLabel.Text));
        }
    }

 }


//methodinvoker action = delegate
//{ textbox1.text += "connected to server... \n"; };
//textbox1.begininvoke(action);
//Use Invoke method