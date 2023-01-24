using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WeightDev.Constants;
using WeightDev.Enums;
using WeightDev.Events;

namespace WeightDev
{
    public partial class WeightDevRepository
    {
        public WeightDevRepository()
        {
            //AxleWtAddedEvent = new EventHandler<AxleWtAddedEventArgs>();
            ResetAxleWt();
        }

        public static System.Threading.ManualResetEventSlim evt = new System.Threading.ManualResetEventSlim(false);
        public SerialPort COMM { get; set; }
        private static System.Net.Sockets.TcpClient IP { get; set; }
        private static System.Net.Sockets.NetworkStream IPStream { get; set; }
        public BackgroundWorker IPBgWorker { get; set; }
        public TextBox HandlerTextBox { get; set; }

        public static ObservableCollection<byte> ReceivedData;

        public decimal Sensitivity { get; set; }

        private WeighingDeviceEnum WD = WeighingDeviceEnum.NONE;
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

        public string CommParityReplace { get; set; }


        public COMMEncoding COMMEncoding { get; set; }

        public COMMReadType COMMReadType { get; set; }
        public string WeighingDevice { get; set; }

        public WeighingModeEnum WeighingMode { get; set; }


        private WeighingInputEnum _wInput;

        public WeighingInputEnum WeighingInput
        {
            get => _wInput;
            set
            {
                _wInput = value;
            }
        }


        public int ReadingInterval { get; set; }
        public string AccessPwd { get; set; }
        public decimal SimulationIncrement { get; set; }
        public int SimulationSpeed { get; set; }

        public string StartCharacter { get; set; }

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

        public string Label { get; set; }

        public TextBox SignalTextBox { get; set; }
        public TextBox LengthViewerTextBox { get; set; }

        public string WeightData
        {
            get; set;
        }
        #endregion

        #region METHODS

        private void setWeightDevSettings()
        {
            Enum.TryParse<ConnType>(ConnectionType, out CType);
            Enum.TryParse<WeighingDeviceEnum>(WeighingDevice, out WD);



            switch (WD)
            {
                case WeighingDeviceEnum.NONE:
                    break;
                case WeighingDeviceEnum.GSE460:
                    DataLength = 9;
                    EndCharacter = "\r";
                    ExtStartIndex = 0;
                    ExtLength = 9;
                    break;
                case WeighingDeviceEnum.GSE460V2:
                    DataLength = 8;
                    EndCharacter = "\r";
                    ExtStartIndex = 0;
                    ExtLength = 8;
                    break;
                case WeighingDeviceEnum.TOLEDO:
                    break;
                case WeighingDeviceEnum.RINSTRUMR323:
                    DataLength = 10;
                    EndCharacter = "\r";
                    CommNewLine = "";
                    break;
                case WeighingDeviceEnum.RINSTRUMR323V2:
                    DataLength = 17;
                    EndCharacter = "\u0002";
                    ExtStartIndex = 7;
                    ExtLength = 7;
                    CommNewLine = "\r";
                    break;
                case WeighingDeviceEnum.RINSTRUMR323V3:
                    DataLength = 13;
                    EndCharacter = "\u0002";
                    ExtStartIndex = 6;
                    ExtLength = 10;
                    CommReceivedBytesThreshold = 13;
                    DiscardInBuffer = false;
                    CommReadBufferSize = 20;
                    CommNewLine = "\u0003";
                    break;
                case WeighingDeviceEnum.RINSTRUMR323V4:
                    DataLength = 11;
                    EndCharacter = "\n";
                    ExtStartIndex = 2;
                    ExtLength = 9;
                    CommReceivedBytesThreshold = 13;
                    DiscardInBuffer = false;
                    CommReadBufferSize = 4;
                    CommNewLine = "\n";
                    break;
                case WeighingDeviceEnum.ZM201:
                    DataLength = 8;
                    EndCharacter = "\n";
                    ExtStartIndex = 0;
                    ExtLength = 8;
                    break;
                case WeighingDeviceEnum.ZM405:
                    DataLength = 7;
                    EndCharacter = "\n";
                    ExtStartIndex = 0;
                    ExtLength = 7;
                    break;
                case WeighingDeviceEnum.KELI_XK:
                    //DataLength = 17;
                    //EndCharacter = "\r";
                    //ExtStartIndex = 0;
                    //ExtLength = 17;
                    //CommReadBufferSize = 8;
                    //CommReceivedBytesThreshold = 1;
                    //CommNewLine = EndCharacter;
                    break;

            }

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
                    case WeighingDeviceEnum.NONE:
                        break;
                    case WeighingDeviceEnum.GSE460:
                        COMM.DataReceived -= COMM_GSE460_DataReceived;
                        COMM.DataReceived += COMM_GSE460_DataReceived;
                        COMM.ErrorReceived -= COMM_GSE460_ErrorReceived;
                        COMM.ErrorReceived += COMM_GSE460_ErrorReceived;
                        break;
                    case WeighingDeviceEnum.GSE460V2:
                        COMM.DataReceived -= COMM_GSE460V2_DataReceived;
                        COMM.DataReceived += COMM_GSE460V2_DataReceived;
                        COMM.ErrorReceived -= COMM_GSE460V2_ErrorReceived;
                        COMM.ErrorReceived += COMM_GSE460V2_ErrorReceived;
                        break;
                    case WeighingDeviceEnum.TOLEDO:
                        break;
                    case WeighingDeviceEnum.RINSTRUMR323:
                        COMM.DataReceived -= COMM_RINSTRUMR323_DataReceived;
                        COMM.DataReceived += COMM_RINSTRUMR323_DataReceived;
                        COMM.ErrorReceived -= COMM_RINSTRUMR323_ErrorReceived;
                        COMM.ErrorReceived += COMM_RINSTRUMR323_ErrorReceived;
                        break;
                    case WeighingDeviceEnum.RINSTRUMR323V2:
                        COMM.DataReceived -= COMM_RINSTRUMR323V2_DataReceived;
                        COMM.DataReceived += COMM_RINSTRUMR323V2_DataReceived;
                        COMM.ErrorReceived -= COMM_RINSTRUMR323V2_ErrorReceived;
                        COMM.ErrorReceived += COMM_RINSTRUMR323V2_ErrorReceived;
                        break;
                    case WeighingDeviceEnum.RINSTRUMR323V3:
                        COMM.DataReceived -= COMM_RINSTRUMR323V3_DataReceived;
                        COMM.DataReceived += COMM_RINSTRUMR323V3_DataReceived;
                        COMM.ErrorReceived -= COMM_RINSTRUMR323V3_ErrorReceived;
                        COMM.ErrorReceived += COMM_RINSTRUMR323V3_ErrorReceived;
                        break;
                    case WeighingDeviceEnum.RINSTRUMR323V4:
                        COMM.DataReceived -= COMM_RINSTRUMR323V4_DataReceived;
                        COMM.DataReceived += COMM_RINSTRUMR323V4_DataReceived;
                        COMM.ErrorReceived -= COMM_RINSTRUMR323V4_ErrorReceived;
                        COMM.ErrorReceived += COMM_RINSTRUMR323V4_ErrorReceived;
                        break;
                    case WeighingDeviceEnum.ZM201:
                        COMM.DataReceived -= COMM_ZM201_DataReceived;
                        COMM.DataReceived += COMM_ZM201_DataReceived;
                        COMM.ErrorReceived -= COMM_ZM201_ErrorReceived;
                        COMM.ErrorReceived += COMM_ZM201_ErrorReceived;
                        break;
                    case WeighingDeviceEnum.ZM405:
                        COMM.DataReceived -= COMM_ZM405_DataReceived;
                        COMM.DataReceived += COMM_ZM405_DataReceived;
                        COMM.ErrorReceived -= COMM_ZM405_ErrorReceived;
                        COMM.ErrorReceived += COMM_ZM405_ErrorReceived;
                        break;
                    case WeighingDeviceEnum.KELI_XK:
                        COMM.DataReceived -= COMM_KELI_XK3196E2_WIM_DataReceived;
                        COMM.DataReceived += COMM_KELI_XK3196E2_WIM_DataReceived;
                        COMM.ErrorReceived -= COMM_KELI_XK3196E2_WIM_ErrorReceived;
                        COMM.ErrorReceived += COMM_KELI_XK3196E2_WIM_ErrorReceived;
                        break;

                }
                #endregion
                COMM.Disposed -= COMM_Disposed;
                COMM.Disposed += COMM_Disposed;
            }
            else if (CType == ConnType.IP)
            {

            }

            ReceivedData = new ObservableCollection<byte>();
            ReceivedData.CollectionChanged -= ReceivedData_CollectionChanged;
            ReceivedData.CollectionChanged += ReceivedData_CollectionChanged;
        }

        public void Start()
        {
            ResetAxleWt();

            Started = false;

            if (AccessPwd != "mijochanel09041990") return;

            if (COMM == null) COMM = new SerialPort();
            if (COMM.IsOpen) Stop();

            setWeightDevSettings();


            try
            {

                if (CType == ConnType.COMM)
                {
                    COMM.PortName = CommPortName;
                    COMM.BaudRate = CommBaudRate;
                    COMM.Handshake = Handshake.None;
                    COMM.ReadTimeout = CommReadTimeout == 0 ? 1000 : CommReadTimeout;
                    COMM.WriteTimeout = CommWriteTimeout == 0 ? 1000 : CommWriteTimeout;
                    COMM.ReceivedBytesThreshold = CommReceivedBytesThreshold;
                    COMM.ReadBufferSize = CommReadBufferSize;

                    if (!String.IsNullOrEmpty(CommNewLine)) COMM.NewLine = CommNewLine;

                    if (String.IsNullOrEmpty(CommParityReplace) == false)
                    {
                        byte[] pr = System.Text.Encoding.ASCII.GetBytes(CommParityReplace);
                        COMM.ParityReplace = pr[0];
                    }
                    else
                    {

                    }


                    switch (COMMEncoding)
                    {
                        case COMMEncoding.TYPE_1:
                            //COMM.Encoding = Encoding.GetEncoding(28591);
                            break;
                        case COMMEncoding.TYPE_2:
                            COMM.Encoding = Encoding.GetEncoding(932);
                            break;
                        case COMMEncoding.TYPE_3:
                            COMM.Encoding = Encoding.GetEncoding(1252);
                            break;
                        case COMMEncoding.TYPE_4:
                            COMM.Encoding = Encoding.GetEncoding("Windows-1252");
                            break;
                    }

                    //COMM.Encoding = Encoding.GetEncoding(28591);
                    //COMM.Encoding = Encoding.GetEncoding("Windows-1252");

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
                    Started = true;
                }
                else if (CType == ConnType.IP)
                {
                    if (IP != null)
                    {
                        if (IP.Connected) IP.GetStream().Close();
                        IP.Close();
                    }
                    IP = new TcpClient();

                    int ipPort = 1;
                    int.TryParse(IPPort, out ipPort);
                    //IP.ReceiveTimeout = IPReadTimeOut;
                    IP.Connect(IPAddress, ipPort);

                    IPStream = IP.GetStream();
                    //System.Threading.Thread.Sleep(2000);
                    IP.NoDelay = true;
                    IP.ReceiveBufferSize = 4096;

                    IPBgWorker = new System.ComponentModel.BackgroundWorker();
                    IPBgWorker.WorkerSupportsCancellation = true;
                    IPBgWorker.DoWork -= IPBgWorker_DoWork;
                    IPBgWorker.DoWork += IPBgWorker_DoWork;
                    Started = true;
                    IPBgWorker.RunWorkerAsync();
                    evt.Reset();

                }
                ReadingInterval = ReadingInterval == 0 ? 100 : ReadingInterval;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void COMM_Disposed(object sender, EventArgs e)
        {
            SetValue(CONST_STR.NO_WEIGHT);
        }

        private void IPBgWorker_DoWork(object sender, DoWorkEventArgs e)
        {

            while (Started)
            {
                DataLength = 8;
                if (e.Cancel) return;
                var bytesReceived = new byte[DataLength];
                evt.Wait(200);
                evt.Set();
                try
                {
                    IPStream.Read(bytesReceived, 0, DataLength);

                    if (bytesReceived.Count() > 0)
                    {
                        var streamData = Encoding.ASCII.GetString(bytesReceived);
                        byte[] bytes = Encoding.ASCII.GetBytes(streamData);

                        //if (streamData.IndexOf('\n') == (DataLength - 1) || streamData.IndexOf('\r') == (DataLength - 1))
                        //{
                        //    SetValue(streamData);
                        //}
                        System.Diagnostics.Debug.WriteLine(streamData);
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
            Enum.TryParse<WeighingDeviceEnum>(WeighingDevice, out WD);


            switch (WD)
            {
                case WeighingDeviceEnum.ZM201:
                    if (str.IndexOf(EndCharacterCHAR) != DataLength - 1) valid = false;
                    break;
                case WeighingDeviceEnum.ZM405:
                    if (str.IndexOf(EndCharacterCHAR) != DataLength - 1) valid = false;
                    break;
                case WeighingDeviceEnum.KELI_XK:
                    if (str.IndexOf("\u0010") != DataLength - 1) valid = false;
                    break;
            }


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

                var b = Decimal.TryParse(str.Substring(ExtStartIndex, ExtLength), out var r);

                if (b == true)
                {
                    str = r.ToString();
                    SetValue(str);
                    WeightData = str;
                }
                //IP.GetStream().Flush();
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
            evt.Set();

            this.AxleWt_Added -= AxleWt_Added;
            this.WeightChanged -= WeightChanged;

            if (CType == ConnType.COMM)
            {
                if (!COMM.IsOpen) return;
                COMM.DiscardOutBuffer();
                COMM.DiscardInBuffer();

                switch (WD)
                {
                    case WeighingDeviceEnum.NONE:
                    case WeighingDeviceEnum.GSE460:
                        COMM.DataReceived -= COMM_GSE460_DataReceived;
                        COMM.ErrorReceived -= COMM_GSE460_ErrorReceived;
                        break;
                    case WeighingDeviceEnum.GSE460V2:
                        COMM.DataReceived -= COMM_GSE460V2_DataReceived;
                        COMM.ErrorReceived -= COMM_GSE460V2_ErrorReceived;
                        break;
                    case WeighingDeviceEnum.RINSTRUMR323V2:
                        COMM.DataReceived -= COMM_RINSTRUMR323V2_DataReceived;
                        COMM.ErrorReceived -= COMM_RINSTRUMR323V2_ErrorReceived;
                        break;
                    case WeighingDeviceEnum.ZM201:
                        COMM.DataReceived -= COMM_ZM201_DataReceived;
                        COMM.ErrorReceived -= COMM_ZM201_ErrorReceived;
                        break;
                    case WeighingDeviceEnum.ZM405:
                        COMM.DataReceived -= COMM_ZM405_DataReceived;
                        COMM.ErrorReceived -= COMM_ZM405_ErrorReceived;
                        break;
                }

                Thread CloseDown = new Thread(new ThreadStart(stopCOMM)); //close port in new thread to avoid hang

                CloseDown.Start();

            }
            else if (CType == ConnType.IP)
            {
                Thread CloseDown = new Thread(new ThreadStart(stopIP)); //close ip in new thread to avoid hang
                CloseDown.Start();
                //System.Threading.Thread.Sleep(6000)

                ;
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
                if (IPBgWorker != null)
                {
                    IPBgWorker.DoWork -= IPBgWorker_DoWork;
                    IPBgWorker.DoWork -= IPBgWorker_DoWork;
                    IPBgWorker.CancelAsync();
                    IPBgWorker.Dispose();
                }
                if (IP.Connected)
                {
                    //IPStream.Dispose();
                    //System.Threading.Thread.Sleep(4000);
                    //IPStream.Close();
                    //IP.Close();

                    //clearMemory(new object[] { IP, IPBgWorker });
                }
                //evt.Wait(10000);
                //evt.Set();
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
            }
            else
            {
                if (motionDetectionCount <= 0) InMotion = false;
                motionDetectionCount -= 1;
            }

            lastValue = cValue;

            //Console.WriteLine(InMotion);

            if (HandlerTextBox.InvokeRequired)
            {
                try
                {
                    if (!HandlerTextBox.IsDisposed)
                        HandlerTextBox.Invoke(new MethodInvoker(delegate { HandlerTextBox.Text = val; }));
                }
                catch
                {

                }
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

        public event System.EventHandler<WeightChangedEventArgs> WeightChanged;

        protected virtual void OnWeightChanged(string value)
        {
            decimal.TryParse(value, out var wt);
            if (WeightChanged != null) WeightChanged(this, new WeightChangedEventArgs()
            {
                Label = this.Label,
                Weight = wt
            }); ;
        }

        public void SendWeightChangedEvent()
        {
            OnWeightChanged(WeightData);
        }

        public void clearMemory(object[] objects)
        {
            GC.Collect();
            GC.Collect();

            foreach (var obj in objects)
            {
                GC.SuppressFinalize(obj);
            }
        }


        private decimal maxWeight { get; set; }

        public Dictionary<int, decimal> AxleWts { get; set; }
            = new Dictionary<int, decimal>();

        private decimal AxleTriggerWt { get; set; } = 100;

        private decimal AxleCountLimt { get; set; } = 8;

        public void ResetAxleWt()
        {
            maxWeight = 0;
            AxleWts.Clear();
        }

        public event System.EventHandler<AxleWtAddedEventArgs> AxleWt_Added;

        private bool isUpTrend;
        private bool isDownTrend;
        private bool zeroReached;
        private List<decimal> lastWeights;
        private int lastWeightMaxCount = 20;

        public void ProcessAxleWt(decimal wt)
        {
            if (WeighingMode != WeighingModeEnum.AXLE) return;

            if (AxleWts.Count >= AxleCountLimt) return;

            // Set Axle Number
            var axleNum = AxleWts.Count + 1;


            // Store Last 20 Weight Movement
            //if (lastWeights?.Count >= 20)
            //{
            //    // Check if DownTrend
            //    decimal summed_nums = lastWeights.Sum();
            //    decimal multiplied_data = 0;
            //    var summed_idx = 0;
            //    double squared_idx = 0;
            //    for (var i = 0; i<=19;i++)
            //    {
            //        i += 1;
            //        multiplied_data += i * lastWeights[i - 1];
            //        summed_idx += i;
            //        squared_idx += Math.Pow((double)i, 2d);
            //    }

            //    var numerator = (lastWeights.Count * multiplied_data) - (summed_nums * summed_idx);
            //    var denominator = (lastWeights.Count * squared_idx) - (Math.Pow(summed_idx, 2));

            //    var res = denominator != 0 ? ((double)numerator / denominator) : 0;
            //    isDownTrend = res <= 0;

            //    lastWeights.Clear();
            //} else
            //{
            //    if (lastWeights == null) lastWeights = new List<decimal>();
            //    lastWeights?.Add(wt);
            //}

            // Get The Max Weight
            if (wt > maxWeight) maxWeight = wt;


            if (isDownTrend)
            {

                var triggerWt = AxleWts.Count == 0 ? 0 : AxleTriggerWt;

                if (wt <= AxleTriggerWt & maxWeight != 0)
                {
                    // check decreasing weight.
                    // if currentwt is less than {AxleTriggerWtDiff} of last Wt store 
                    AxleWts.Add(axleNum, wt);
                    AxleWt_Added?.Invoke(this, new AxleWtAddedEventArgs(axleNum, maxWeight, DateTime.Now));
                    maxWeight = 0;
                }
                isDownTrend = false;
            }

        }

    }
}
