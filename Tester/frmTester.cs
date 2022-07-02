using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tester
{
    public partial class frmTester : Form
    {
        public frmTester()
        {
            InitializeComponent();
        }

        private void frmTester_Load(object sender, EventArgs e)
        {
            try
            {
                cboDevice.Text = "RINSTRUMR323V2";
                txtCommPort.Text = "COM1";
                cboCType.Text = "COMM";



                //button1_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            weightIndicator1.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {try
            {
          
                weightIndicator1.WeighingDevice = cboDevice.Text;
                weightIndicator1.CommPortName = txtCommPort.Text; ;
                weightIndicator1.CommBaudRate = 9600;
                weightIndicator1.ReadingInterval = 100;
                weightIndicator1.CommReadTimeout = 3000;
                weightIndicator1.CommReadBufferSize = Convert.ToInt32(txtReadBufferSize.Text);
                weightIndicator1.CommReceivedBytesThreshold = Convert.ToInt32(txtReceivedBytesThreshold.Text);
                weightIndicator1.DiscardOutBuffer = chkDIscardOutBuffer.Checked;
                weightIndicator1.DataLength = Convert.ToInt32(txtDataLength.Text);
                weightIndicator1.EndCharacter = txtEndCharacter.Text;
                weightIndicator1.ConnectionType = cboCType.Text;
                weightIndicator1.IPAddress = txtIPAddr.Text;
                weightIndicator1.IPPort = txtIPPort.Text;
                weightIndicator1.IPReadTimeOut = Convert.ToInt32(txtTimeOut.Text);
                weightIndicator1.LengthViewer = txtSignalLength;
                weightIndicator1.SignalViewer = txtSignal;
                weightIndicator1.InitializeLifetimeService();
                weightIndicator1.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        private void txtReceivedBytesThreshold_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboDevice_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
