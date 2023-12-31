﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
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
        private int ExtLength { get; set; }
        public string ConnectionType { get; set; }

        public  System.Net.Sockets.TcpClient IP { get; set; }
        public string IPAddress { get; set; }
        public string IPPort { get; set; }
        public int IPReadTimeOut { get; set; }

        public int DataLength { get; set; }
        public int DataSent { get; set; }

        public bool InMotion { get { return weightDevRepo.InMotion; } }

        public decimal Sensitivity { get; set; }
        public void Start()
        {
            Started = false;

            weightDevRepo.AccessPwd = AccessPwd;
            weightDevRepo.WeighingDevice = WeighingDevice;
            weightDevRepo.ConnectionType = ConnectionType;
            weightDevRepo.CommPortName = CommPortName;
            weightDevRepo.CommBaudRate = CommBaudRate;
            weightDevRepo.CommReceivedBytesThreshold = CommReceivedBytesThreshold;
            weightDevRepo.CommReadTimeout = CommReadTimeout;
            weightDevRepo.CommPortName = CommPortName;
            weightDevRepo.CommWriteTimeout = CommWriteTimeout;
            weightDevRepo.CommReadBufferSize = CommReadBufferSize;
            weightDevRepo.CommNewLine = CommNewLine;
            weightDevRepo.CommParity = CommParity;
            weightDevRepo.CommDataBits = CommDataBits;
            weightDevRepo.CommStopBits = CommStopBits;
            weightDevRepo.CommDTREnable = CommDTREnable;
            weightDevRepo.CommRTSEnable = CommRTSEnable;
            weightDevRepo.HandlerTextBox = this;
            weightDevRepo.DataLength = DataLength;
            weightDevRepo.SignalTextBox = SignalViewer;
            weightDevRepo.LengthViewerTextBox = LengthViewer;
            weightDevRepo.DiscardInBuffer = DiscardInBuffer;
            weightDevRepo.DiscardOutBuffer = DiscardOutBuffer;
            weightDevRepo.Sensitivity = Sensitivity;

            weightDevRepo.IPAddress = IPAddress;
            weightDevRepo.IPPort = IPPort;
            weightDevRepo.IPReadTimeOut = IPReadTimeOut;
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

    }
}
