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
            switch (label3.Text)
            {
                case "(00)": label2.Text = "reserved By Document"; break;
                case "(10)": label2.Text = "general Reject"; break;
                case "(11)": label2.Text = "service Not Supported"; break;
                case "(12)": label2.Text = "sub Function Not Supported-invalidFormat "; break;
                case "(13)": label2.Text = "incorrect Message Length Or InvalidFormat"; break;
                case "(21)": label2.Text = "busy-repeat Request "; break;
                case "(22)": label2.Text = "conditions Not Correct Or Request Sequence Error"; break;
                case "(23)": label2.Text = "routine Not Complete Or Service In Progress"; break;
                case "(24)": label2.Text = "request Sequence Error (RSE)"; break;
                case "(31)": label2.Text = "request Out Of Range"; break;
                case "(33)": label2.Text = "security Access Denied-security Access Requested"; break;
                case "(35)": label2.Text = "invalid Key"; break;
                case "(36)": label2.Text = "exceed Number Of Attempts"; break;
                case "(37)": label2.Text = "required Time Delay Not Expired"; break;
                case "(40)": label2.Text = "download Not Accepted"; break;
                case "(41)": label2.Text = "improper Download Type"; break;
                case "(42)": label2.Text = "can Not Download To Specified Address"; break;
                case "(43)": label2.Text = "can Not Download Number Of Bytes Requested"; break;
                case "(50)": label2.Text = "upload Not Accepted"; break;
                case "(51)": label2.Text = "improper Upload Type"; break;
                case "(52)": label2.Text = "can Not Upload From Specified Address"; break;
                case "(53)": label2.Text = "can Not Upload Number Of Bytes Requested"; break;
                case "(71)": label2.Text = "Transfer Suspended - This response code indicates that a data transfer operation was halted due to some fault."; break;
                case "(72)": label2.Text = "Transfer Aborted - This response code indicates that the server detected an error when erasing or programming a memory location in the permanent memory device (e.g. Flash Memory)."; break;
                case "(73)": label2.Text = "wrong Block Sequence Counter (WBSC)"; break;
                case "(74)": label2.Text = "illegal Address In Block Transfer"; break;
                case "(75)": label2.Text = "illegal Byte Count In Block Transfer"; break;
                case "(76)": label2.Text = "illegal Block Transfer Type"; break;
                case "(77)": label2.Text = "block Transfer Data Checksum Error"; break;
                case "(78)": label2.Text = "Request Correctly Received-Response Pending)"; break;
                case "(79)": label2.Text = "incorrect Byte Count During Block Transfer"; break;
                case "(7E)": label2.Text = "subFunction Not Supported In Active Session (SFNSIAS)"; break;
                case "(7F)": label2.Text = "service Not Supported In Active Session (SNSIAS)"; break;
                case "(80)": label2.Text = "service Not Supported In Active Diagnostic Session"; break;
                case "(8F)": label2.Text = "reserved By Document"; break;
                case "(81)": label2.Text = "rpmTooHigh (RPMTH)"; break;
                case "(82)": label2.Text = "rpmTooLow (RPMTL)"; break;
                case "(83)": label2.Text = "engine Is Running (EIR)"; break;
                case "(84)": label2.Text = "engine Is Not Running (EINR)"; break;
                case "(85)": label2.Text = "engine Run Time Too Low (ERTTL)"; break;
                case "(86)": label2.Text = "temperature Too High (TEMPTH)"; break;
                case "(87)": label2.Text = "temperature Too Low (TEMPTL)"; break;
                case "(88)": label2.Text = "vehicle Speed Too High (VSTH)"; break;
                case "(89)": label2.Text = "vehicle Speed Too Low (VSTL)"; break;
                case "(8A)": label2.Text = "throttle/Pedal Too High (TPTH)"; break;
                case "(8B)": label2.Text = "throttle/Pedal Too Low (TPTL)"; break;
                case "(8C)": label2.Text = "transmission Range Not In Neutral (TRNIN)"; break;
                case "(90)": label2.Text = "vehicle Manufacturer Specific"; break;
                case "(91)": label2.Text = "torque Converter Clutch Locked (TCCL)"; break;
                case "(92)": label2.Text = "voltage Too High (VTH)"; break;
                case "(93)": label2.Text = "voltage Too Low (VTL)"; break;
                case "(A0)": label2.Text = "ECU is not responding"; break;
                case "(A1)": label2.Text = "ECU address unknown"; break;
                case "(A2)": label2.Text = "Gateway communication error"; break;
                case "(A3)": label2.Text = "not defined"; break;
                case "(FA)": label2.Text = "system Supplier Specific"; break;
                case "(FF)": label2.Text = "reserved By Document "; break;
            }
        }

        public static int Get7FResponseDesc()
        {

            return 1;
        }
    }
}
