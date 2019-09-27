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
    public partial class _7FMsgbox : Form
    {
        private int desiredStartLocationX;
        private int desiredStartLocationY;
        public _7FMsgbox(int x, int y)
        {
            InitializeComponent();
            this.desiredStartLocationX = x;
            this.desiredStartLocationY = y;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _7FMsgbox_Load(object sender, EventArgs e)
        {
            this.SetDesktopLocation(desiredStartLocationX, desiredStartLocationY);
            if (label3.Text == "31") label2.Text = "Request Out Of Range";
            if (label3.Text == "7F") label2.Text = "";
            if (label3.Text == "12") label2.Text = "Access Denied";
        }

        public static int Get7FResponseDesc()
        {

            return 1;
        }
    }
}
