using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeightDev.Constants;
using WeightDev.Enums;

namespace WeightDev
{
    public partial class WeightDevRepository
    {
        public static System.Threading.ManualResetEventSlim evt = new System.Threading.ManualResetEventSlim(false);
        public SerialPort COMM { get; set; }
        private static System.Net.Sockets.TcpClient IP { get; set; }
        public BackgroundWorker IPBgWorker { get; set; }

        public TextBox HandlerTextBox { get; set; }

        public static ObservableCollection<byte> ReceivedData;

        public decimal Sensitivity { get; set; }

        private WDevice WD = WDevice.NONE;
        private ConnType CType = ConnType.COMM;

        #region PROPERTIES
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
        public string WeighingDevice { get; set; }
        public int ReadingInterval { get; set; }
        public string AccessPwd { get; set; }
        public decimal SimulationIncrement { get; set; }
        public int SimulationSpeed { get; set; }
        public string EndCharacter { get; set; }
        private char EndCharacterCHAR { get; set; }
        private bool Started { get; set; }
        public bool DiscardInBuffer { get; set; }
        public bool DiscardOutBuffer { get; set; }
        public int ExtStartIndex { get; set; }
        public int ExtLength { get; set; }
        public string ConnectionType { get; set; }

        public string IPAddress { get; set; }
        public string IPPort { get; set; }
        public int IPReadTimeOut { get; set; }

        public int DataLength { get; set; }

        public TextBox SignalTextBox { get; set; }
        public TextBox LengthViewerTextBox { get; set; }

        public static string WeightData
        {
            get; set;
        }
        #endregion

        #region METHODS

        private void setConnection()
        {
            try
            {
                Enum.TryParse<ConnType>(ConnectionType, out CType);
                Enum.TryParse<WDevice>(WeighingDevice, out WD);

                if (CType == ConnType.COMM)
                {

                    COMM = new SerialPort();
                    COMM.PortName = CommPortName;
                    COMM.BaudRate = CommBaudRate;
                    COMM.Handshake = Handshake.None;
                    COMM.ReadTimeout = CommReadTimeout == 0 ? 1000 : CommReadTimeout;
                    COMM.WriteTimeout = CommWriteTimeout == 0 ? 1000 : CommWriteTimeout;
                    COMM.ReceivedBytesThreshold = CommReceivedBytesThreshold;
                    COMM.ReadBufferSize = CommReadBufferSize;
                    COMM.NewLine = CommNewLine;
                    COMM.ParityReplace = 0 ;
                    COMM.Encoding = Encoding.GetEncoding(28591);

                    #region PARITY
                    switch (CommParity)
                    {
                        case "None":
                            COMM.Parity = Parity.None;
                            break;
                        case "Even":
                            COMM.Parity = Parity.Even;
                            break;
                        case "Mark":
                            COMM.Parity = Parity.Mark;
                            break;
                        case "Odd":
                            COMM.Parity = Parity.Odd;
                            break;
                        case "Space":
                            COMM.Parity = Parity.Space;
                            break;
                    }
                    #endregion

                    COMM.DataBits = CommDataBits;

                    #region STOPBITS
                    switch (CommStopBits)
                    {
                        case "0":
                            COMM.StopBits = StopBits.None;
                            break;
                        case "1":
                            COMM.StopBits = StopBits.One;
                            break;
                        case "1.5":
                            COMM.StopBits = StopBits.OnePointFive;
                            break;
                        case "2":
                            COMM.StopBits = StopBits.Two;
                            break;
                    }
                    #endregion

                    COMM.DtrEnable = CommDTREnable;
                    COMM.RtsEnable = CommRTSEnable;

                    if (COMM.IsOpen)
                    {
                        COMM.Close();
                    }

                    COMM.Open();
                }
                else if (CType == ConnType.IP)
                {
                    if (IP != null) { if (IP.Connected) IP.Close(); }
                    IP = new TcpClient();

                    int ipPort = 1;
                    int.TryParse(IPPort, out ipPort);
                    IP.ReceiveTimeout = IPReadTimeOut;
                    IP.Connect(IPAddress, ipPort);
                    IP.NoDelay = true;

                }
                ReadingInterval = ReadingInterval == 0 ? 100 : ReadingInterval;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Start()
        {
            Started = false;

            if (AccessPwd != "mijochanel09041990") return;


            switch (WD)
            {
                case WDevice.NONE:
                    break;
                case WDevice.GSE460:
                    DataLength = 9;
                    EndCharacter = "\r";
                    ExtStartIndex = 0;
                    ExtLength = 9;
                    break;
                case WDevice.GSE460V2:
                    DataLength = 8;
                    EndCharacter = "\r";
                    ExtStartIndex = 0;
                    ExtLength = 8;
                    break;
                case WDevice.TOLEDO:
                    break;
                case WDevice.RINSTRUMR323:
                    DataLength = 12;
                    EndCharacter = "\r";
                    break;
                case WDevice.RINSTRUMR323V2:
                    DataLength = 17;
                    EndCharacter = "\r";
                    ExtStartIndex = 6;
                    ExtLength = 8;
                    break;
                case WDevice.RINSTRUMR323V3:
                    DataLength = 13;
                    EndCharacter = "\u0002";
                    ExtStartIndex = 6;
                    ExtLength = 10;
                    CommReceivedBytesThreshold = 13;
                    DiscardInBuffer = false;
                    CommReadBufferSize = 20;
                    COMM.NewLine = "\u0003";

                    break;
                case WDevice.ZM405V2:
                    DataLength = 9;
                    EndCharacter = "\r";
                    ExtStartIndex = 0;
                    ExtLength = 9;
                    break;

            }

            setConnection();

         

            switch (EndCharacter)
            {
                case "\\n":
                case "\n":
                    EndCharacterCHAR = '\n';
                    break;
                case "\\r":
                case "\r":
                    EndCharacterCHAR = '\r';
                    break;
                case "\\0":
                case "\0":
                case "":
                    EndCharacterCHAR = '\0';
                    break;
                case null:
                    EndCharacterCHAR = '\0';
                    break;
                default:
                    EndCharacterCHAR = Convert.ToChar(EndCharacter);
                    break;
            }
            if (CType == ConnType.COMM)
            {
                #region WD
                switch (WD)
                {
                    case WDevice.NONE:
                        break;
                    case WDevice.GSE460:
                        COMM.DataReceived -= COMM_GSE460_DataReceived;
                        COMM.DataReceived += COMM_GSE460_DataReceived;
                        COMM.ErrorReceived -= COMM_GSE460_ErrorReceived;
                        COMM.ErrorReceived += COMM_GSE460_ErrorReceived;
                        break;
                    case WDevice.GSE460V2:
                        COMM.DataReceived -= COMM_GSE460V2_DataReceived;
                        COMM.DataReceived += COMM_GSE460V2_DataReceived;
                        COMM.ErrorReceived -= COMM_GSE460V2_ErrorReceived;
                        COMM.ErrorReceived += COMM_GSE460V2_ErrorReceived;
                        break;
                    case WDevice.TOLEDO:
                        break;
                    case WDevice.RINSTRUMR323:
                        COMM.DataReceived -= COMM_RINSTRUMR323_DataReceived;
                        COMM.DataReceived += COMM_RINSTRUMR323_DataReceived;
                        COMM.ErrorReceived -= COMM_RINSTRUMR323_ErrorReceived;
                        COMM.ErrorReceived += COMM_RINSTRUMR323_ErrorReceived;
                        break;
                    case WDevice.RINSTRUMR323V2:
                        COMM.DataReceived -= COMM_RINSTRUMR323V2_DataReceived;
                        COMM.DataReceived += COMM_RINSTRUMR323V2_DataReceived;
                        COMM.ErrorReceived -= COMM_RINSTRUMR323V2_ErrorReceived;
                        COMM.ErrorReceived += COMM_RINSTRUMR323V2_ErrorReceived;
                        break;
                    case WDevice.RINSTRUMR323V3:
                        COMM.DataReceived -= COMM_RINSTRUMR323V3_DataReceived;
                        COMM.DataReceived += COMM_RINSTRUMR323V3_DataReceived;
                        COMM.ErrorReceived -= COMM_RINSTRUMR323V3_ErrorReceived;
                        COMM.ErrorReceived += COMM_RINSTRUMR323V3_ErrorReceived;
                        break;
                    case WDevice.ZM405V2:
                        COMM.DataReceived -= COMM_ZM405V2_DataReceived;
                        COMM.DataReceived += COMM_ZM405V2_DataReceived;
                        COMM.ErrorReceived -= COMM_ZM405V2_ErrorReceived;
                        COMM.ErrorReceived += COMM_ZM405V2_ErrorReceived;
                        break;

                }
                #endregion
                COMM.Disposed -= COMM_Disposed;
                COMM.Disposed += COMM_Disposed;


            }
            else if (CType == ConnType.IP)
            {
                Started = true;

                IPBgWorker = new System.ComponentModel.BackgroundWorker();
                IPBgWorker.WorkerSupportsCancellation = true;
                IPBgWorker.DoWork -= IPBgWorker_DoWork;
                IPBgWorker.DoWork += IPBgWorker_DoWork;
                IPBgWorker.RunWorkerAsync();
                evt.Reset();
            }

            ReceivedData = new ObservableCollection<byte>();
            ReceivedData.CollectionChanged -= ReceivedData_CollectionChanged;
            ReceivedData.CollectionChanged += ReceivedData_CollectionChanged;

            Started = true;
        }

        private void COMM_Disposed(object sender, EventArgs e)
        {
            SetValue(CONST_STR.NO_WEIGHT);
        }

        private void IPBgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var netStream = IP.GetStream();

            while (Started)
            {
                if (e.Cancel) return;
                var bytesReceived = new byte[DataLength];
                evt.Wait(50);
                evt.Set();
                try
                {
                    netStream.Read(bytesReceived, 0, DataLength);

                    if (bytesReceived.Count() > 0)
                    {
                        var streamData = Encoding.ASCII.GetString(bytesReceived);
                        byte[] bytes = Encoding.ASCII.GetBytes(streamData);

                        for (int i = 0; i <= bytes.Count() - 1; i++)
                        {
                            ReceivedData.Add(bytesReceived[i]);
                        }
                    }
                    else
                    {
                        if (e.Cancel) return;
                        SetValue(CONST_STR.NO_WEIGHT);
                    }
                    evt.Reset();
                }
                catch (Exception ex)
                {
                    if (e.Cancel) return;
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                    SetValue(CONST_STR.NO_WEIGHT);
                }
            }

        }

        private void ReceivedData_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                if (ReceivedData.Count() >= (DataLength))
                {
                    processReceivedData();
                }
            }
        }

        private void processReceivedData()
        {
            evt.Wait(50);
            evt.Set();

            var str = System.Text.Encoding.UTF8.GetString(ReceivedData.ToArray());

            if ((str ?? string.Empty).Trim() == HandlerTextBox.Text.Trim())
            {
                ReceivedData.Clear();
                if (CType == ConnType.COMM)
                {
                    COMM.DiscardInBuffer();
                }
            }

            var valid = true;
            if (str.LastIndexOf(EndCharacterCHAR) != (DataLength - 1)) valid = false;

            if (str.Length < (ExtStartIndex + ExtLength))
            {
                valid = false;
            }

            if (SignalTextBox != null)
            {
                SetSignalValue(str.ToString() + "    -> " + (valid ? "VALID" : "INVALID"));
            }

            //if (EndCharacterCHAR != '\0')
            //{
            //    if (str.LastIndexOf(EndCharacterCHAR) != (DataLength - 1)) valid = false;
            //}

            if (valid)
            {
                str = Convert.ToDecimal(str.Substring(ExtStartIndex, ExtLength)).ToString();
                SetValue(str);
                WeightData = str;
            }

            evt.Reset();

            ReceivedData.Clear();
            if (CType == ConnType.COMM)
            {
                COMM.DiscardInBuffer();
            }
        }

        public void Stop()
        {
            Started = false;
            if (COMM == null) return;
            evt.Wait(500);
            if (CType == ConnType.COMM)
            {
                if (!COMM.IsOpen) return;
                COMM.DiscardOutBuffer();
                COMM.DiscardInBuffer();

                switch (WD)
                {
                    case WDevice.NONE:
                    case WDevice.GSE460:
                        COMM.DataReceived -= COMM_GSE460_DataReceived;
                        COMM.ErrorReceived -= COMM_GSE460_ErrorReceived;
                        break;
                    case WDevice.GSE460V2:
                        COMM.DataReceived -= COMM_GSE460V2_DataReceived;
                        COMM.ErrorReceived -= COMM_GSE460V2_ErrorReceived;
                        break;
                    case WDevice.RINSTRUMR323V2:
                        COMM.DataReceived -= COMM_RINSTRUMR323V2_DataReceived;
                        COMM.ErrorReceived -= COMM_RINSTRUMR323V2_ErrorReceived;
                        break;
                    case WDevice.ZM405V2:
                        COMM.DataReceived -= COMM_ZM405V2_DataReceived;
                        COMM.ErrorReceived -= COMM_ZM405V2_ErrorReceived;
                        break;
                }

                Thread CloseDown = new Thread(new ThreadStart(stopCOMM)); //close port in new thread to avoid hang

                CloseDown.Start();

            }
            else if (CType == ConnType.IP)
            {
                Thread CloseDown = new Thread(new ThreadStart(stopIP)); //close ip in new thread to avoid hang

                CloseDown.Start();
            }



        }

        private void stopCOMM()
        {
            if (COMM != null)
            {
                if (COMM.IsOpen)
                {
                    COMM.DiscardInBuffer();
                    COMM.DiscardOutBuffer();
                    COMM.Dispose();
                }
            }
        }

        private void stopIP()
        {
            if (IP != null)
            {
                IPBgWorker.DoWork -= IPBgWorker_DoWork;
                if (IP.Connected)
                {
                    IP.Close();
                    IPBgWorker.CancelAsync();
                }
            }
        }

        #endregion

        public bool InMotion { get; private set; } = false;

        private int motionDetectionCount = 5;

        private decimal lastValue { get; set; }

        private decimal InMotionValue { get; set; }

        private void SetValue(string val)
        {
            if (Started == false) return;

            if (HandlerTextBox == null) return;

            var cValue = 0M;
            decimal.TryParse(HandlerTextBox.Text, out cValue);

            var compareValue = InMotion ? InMotionValue : lastValue;

            if (cValue > (compareValue + Sensitivity) || cValue < (compareValue - Sensitivity))
            {
                if (InMotion == false) InMotionValue = cValue; else InMotionValue = lastValue;
                InMotion = true;
                motionDetectionCount = 4;
            } else
            {
                if (motionDetectionCount <= 0) InMotion = false;
                motionDetectionCount -= 1;
            }

            lastValue = cValue;

            //Console.WriteLine(InMotion);

            if (HandlerTextBox.InvokeRequired)
            {
                HandlerTextBox.Invoke(new MethodInvoker(delegate { HandlerTextBox.Text = val; }));
            }
            else
            {
                HandlerTextBox.Text = val;
            }
        }

        private void SetSignalValue(string val)
{
    if (Started == false) return;

    if (SignalTextBox == null) return;

    if (SignalTextBox.InvokeRequired)
    {
        SignalTextBox.Invoke(new MethodInvoker(delegate { SignalTextBox.Text += val; }));
    }
    else
    {
        SignalTextBox.Text = val;
    }
}

        private void SetSignaLength(string val)
        {
            if (Started == false) return;

            if (LengthViewerTextBox == null) return;

            if (LengthViewerTextBox.InvokeRequired)
            {
                LengthViewerTextBox.Invoke(new MethodInvoker(delegate { LengthViewerTextBox.Text = val.Count().ToString(); }));
            }
            else
            {
                LengthViewerTextBox.Text = val;
            }
        }

    }
}
