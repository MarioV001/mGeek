using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mGeek
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void TabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _textBrush;

            // Get the item from the collection.
            TabPage _tabPage = tabControl1.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = tabControl1.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {

                // Draw a different background color, and don't paint a focus rectangle.
                _textBrush = new SolidBrush(Color.Black);
                g.FillRectangle(Brushes.LightGray, e.Bounds);
            }
            else
            {
                _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
                e.DrawBackground();
            }

            // Use our own font.
            Font _tabFont = new Font("Arial", 10.0f, FontStyle.Bold, GraphicsUnit.Pixel);

            // Draw string. Center the text.
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Settings_Load(object sender, EventArgs e)//LOAD Settings
        {
            textBox1.Text = Properties.Settings.Default.LogsPath;//load Path
            AutoLoadLog.Checked = Properties.Settings.Default.AutpLoadLogs;
            EnableMGeek.Checked = Properties.Settings.Default.StartMGekk;
            //search
            trackBar1.Value = Properties.Settings.Default.SearchOpacity;
            label2.Text = "Search Box Transparency: " + trackBar1.Value * 10 + "%";
        }
        private void Button1_Click(object sender, EventArgs e)//Save Settings
        {
            //save
            Properties.Settings.Default.AutpLoadLogs = AutoLoadLog.Checked;
            Properties.Settings.Default.StartMGekk = EnableMGeek.Checked;
            Properties.Settings.Default.Save();
            //search
            Properties.Settings.Default.SearchOpacity = trackBar1.Value;
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            string dummyFileName = "Folder Select";

            SaveFileDialog sf = new SaveFileDialog();
            // Feed the dummy name to the save dialog
            sf.FileName = dummyFileName;
            sf.CheckFileExists = false;

            if (sf.ShowDialog() == DialogResult.OK)
            {
                // Now here's our save folder
                string savePath = Path.GetDirectoryName(sf.FileName);
                Properties.Settings.Default.LogsPath = savePath + @"\";
                textBox1.Text = Properties.Settings.Default.LogsPath;
            }
        }

        

        private void TrackBar1_ValueChanged(object sender, EventArgs e)
        {
            label2.Text = "Search Box Transparency: " + trackBar1.Value*10 + "%";
        }
    }
}
