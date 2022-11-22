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
                cboDevice.Text = "KELI_XK";
                txtCommPort.Text = "COM3";
                cboCType.Text = "COMM";
                cboEncoding.Text = "TYPE 1";
                cboReadType.Text = "TYPE 1";

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
                weightIndicator1.CommBaudRate = Convert.ToInt32(cboBaudRate.Text);
                weightIndicator1.ReadingInterval = 100;
                weightIndicator1.CommReadTimeout = 3000;
                weightIndicator1.CommReadBufferSize = Convert.ToInt32(txtReadBufferSize.Text);
                weightIndicator1.CommReceivedBytesThreshold = Convert.ToInt32(txtReceivedBytesThreshold.Text);
                weightIndicator1.CommNewLine = txtNewLine.Text.Trim();
                weightIndicator1.CommParityReplace = txtParityReplace.Text;
                weightIndicator1.DiscardOutBuffer = chkDIscardOutBuffer.Checked;
                weightIndicator1.DiscardInBuffer = chkDiscardInBuffer.Checked;
                weightIndicator1.COMMEncoding = cboEncoding.Text;
                weightIndicator1.COMMReadType = cboReadType.Text;

                weightIndicator1.ConnectionType = cboCType.Text;
                weightIndicator1.IPAddress = txtIPAddr.Text;
                weightIndicator1.IPPort = txtIPPort.Text;
                weightIndicator1.IPReadTimeOut = Convert.ToInt32(txtTimeOut.Text);

                weightIndicator1.DataLength = Convert.ToInt32(txtDataLength.Text);
                weightIndicator1.ExtStartIndex = Convert.ToInt32(nupdStartIdx.Value);
                weightIndicator1.StartCharacter = txtStartChar.Text;
                weightIndicator1.EndCharacter = txtEndChar.Text;

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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txtSignalLength_TextChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtEndCharacter_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void chkDIscardOutBuffer_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cboCType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
        {

        }
    }
}
