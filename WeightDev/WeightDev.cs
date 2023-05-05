using System;
using System.Drawing;
using System.Windows.Forms;
using WeightDev.Enums;
using WeightDev.Events;

namespace WeightDev
{
    public partial class WeightIndicator : TextBox
    {
        public static System.Threading.ManualResetEventSlim evt = new System.Threading.ManualResetEventSlim(false);

        private WeightDevRepository weightDevRepo = new WeightDevRepository();

        public WeightIndicator()
        {
            // Add any initialization after the InitializeComponent() call.
            //InitializeComponent();
            this.ReadOnly = true;
            this.BackColor = Color.Black;
            this.ForeColor = Color.YellowGreen;
            this.Font = new System.Drawing.Font("Segoe UI", 16.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextAlign = HorizontalAlignment.Center;
            this.Width = 150;
            this.CommPortName = "COM1";
            this.CommBaudRate = 9600;
            this.CommReceivedBytesThreshold = 1;
            this.CommReadBufferSize = 4;
            this.CommNewLine = "\n";

            this.CommParity = "None";
            this.CommDataBits = 8;
            this.CommStopBits = "1";
            this.CommDTREnable = true;
            this.CommRTSEnable = false;

            this.CommReadTimeout = 0;
            this.CommWriteTimeout = 1000;
            this.SimulationSpeed = 100;
            this.SimulationIncrement = 10;

            this.ReadingInterval = 100;

            this.IPAddress = "192.168.1.41";
            this.IPReadTimeOut = 1000;
            this.IPPort = "1";

            this.WeighingMode = WeighingModeEnum.STANDARD;

            weightDevRepo.AxleWt_Added += WeightDevRepo_AxleWtAddedEvent;

        }


        public TextBox LengthViewer { get; set; }
        public TextBox SignalViewer { get; set; }

        public string CommPortName { get; set; }
        public int CommBaudRate { get; set; }
        public string CommParity { get; set; }
        public int CommDataBits { get; set; }
        public string CommStopBits { get; set; }
        public bool CommDTREnable { get; set; }
        public bool CommRTSEnable { get; set; }
        public int CommReadTimeout { get; set; }
        public int CommWriteTimeout { get; set; }
        public int CommReceivedBytesThreshold { get; set; }
        public int CommReadBufferSize { get; set; }
        public string CommNewLine { get; set; }
        public string CommParityReplace { get; set; }
        public string WeighingDevice { get; set; }
        public int ReadingInterval { get; set; }
        public string AccessPwd { get; set; }
        public decimal SimulationIncrement { get; set; }
        public int SimulationSpeed { get; set; }
        public string StartCharacter { get; set; }
        public string EndCharacter { get; set; }

        private bool Started { get; set; }

        public bool DiscardInBuffer { get; set; }
        public bool DiscardOutBuffer { get; set; }
        public int ExtStartIndex { get; set; }
        private int ExtLength { get; set; }
        public string ConnectionType { get; set; }

        public string COMMEncoding { get; set; }

        public string COMMReadType { get; set; }

        public System.Net.Sockets.TcpClient IP { get; set; }
        public string IPAddress { get; set; }
        public string IPPort { get; set; }
        public int IPReadTimeOut { get; set; }

        public int DataLength { get; set; }
        public int DataSent { get; set; }

        public bool InMotion { get { return weightDevRepo.InMotion; } }

        public decimal Sensitivity { get; set; }

        public decimal AxleTriggerWt { get; set; }

        public WeighingModeEnum WeighingMode { get; set; }


        private WeighingInputEnum _weighingInput;
        public WeighingInputEnum WeighingInput
        {
            get
            {
                return _weighingInput;
            }
            set
            {
                _weighingInput = value;
                this.ReadOnly = value == WeighingInputEnum.AUTO;
                weightDevRepo.WeighingInput = value;
            }
        }

        public void Start()
        {
            Started = false;

            weightDevRepo.AccessPwd = AccessPwd;
            weightDevRepo.WeighingDevice = WeighingDevice;
            weightDevRepo.WeighingMode = WeighingMode;
            weightDevRepo.ConnectionType = ConnectionType;
            weightDevRepo.CommPortName = CommPortName;
            weightDevRepo.CommBaudRate = CommBaudRate;
            weightDevRepo.CommReceivedBytesThreshold = CommReceivedBytesThreshold;
            weightDevRepo.CommReadTimeout = CommReadTimeout;
            weightDevRepo.CommPortName = CommPortName;
            weightDevRepo.CommWriteTimeout = CommWriteTimeout;
            weightDevRepo.CommReadBufferSize = CommReadBufferSize;
            weightDevRepo.WeighingMode = WeighingMode;
            weightDevRepo.WeighingInput = WeighingInput;

            if (!string.IsNullOrEmpty(CommNewLine))
                weightDevRepo.CommNewLine = CommNewLine;

            weightDevRepo.CommParity = CommParity;
            weightDevRepo.CommDataBits = CommDataBits;
            weightDevRepo.CommStopBits = CommStopBits;
            weightDevRepo.CommDTREnable = CommDTREnable;
            weightDevRepo.CommRTSEnable = CommRTSEnable;
            weightDevRepo.CommParityReplace = CommParityReplace;

            Enum.TryParse<Enums.COMMEncoding>(COMMEncoding?.Replace(" ", "_"), out var commE);
            Enum.TryParse<Enums.COMMReadType>(COMMReadType?.Replace(" ", "_"), out var commR);

            weightDevRepo.COMMEncoding = commE;
            weightDevRepo.COMMReadType = commR;
            weightDevRepo.HandlerTextBox = this;
            weightDevRepo.DataLength = DataLength;
            weightDevRepo.SignalTextBox = SignalViewer;
            weightDevRepo.LengthViewerTextBox = LengthViewer;
            weightDevRepo.DiscardInBuffer = DiscardInBuffer;
            weightDevRepo.DiscardOutBuffer = DiscardOutBuffer;
            weightDevRepo.Sensitivity = Sensitivity;
            weightDevRepo.CommNewLine = CommNewLine;

            weightDevRepo.IPAddress = IPAddress;
            weightDevRepo.IPPort = IPPort;
            weightDevRepo.IPReadTimeOut = IPReadTimeOut;
            weightDevRepo.StartCharacter = StartCharacter;
            weightDevRepo.EndCharacter = EndCharacter;
            weightDevRepo.ExtStartIndex = ExtStartIndex;
            weightDevRepo.ExtLength = ExtLength;

            weightDevRepo.ReadingInterval = ReadingInterval;

            weightDevRepo.Start();

            Started = true;
        }

        public void Stop()
        {
            Started = false;
            evt.Wait(500);

            weightDevRepo.Stop();
        }

        public static string WeightData
        {
            get; set;
        }

        /// <summary>
        ///   This will be called when new axle is registered. It will work only if WeighingMode is AXLE
        /// </summary>
        public event System.EventHandler<AxleWtAddedEventArgs> AxleWt_Added;


        private void WeightDevRepo_AxleWtAddedEvent(object sender, AxleWtAddedEventArgs e)
        {
            AxleWt_Added?.Invoke(this, e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            //if (WeighingInput == WeighingInputEnum.AUTO) 
            base.OnTextChanged(e);
   

            if (WeighingMode == WeighingModeEnum.AXLE)
            {
                decimal.TryParse(this.Text, out var wt);
                weightDevRepo.ProcessAxleWt(wt);

            }
        }

        public void ResetAxle() => weightDevRepo.ResetAxleWt();

    }
}
