using System;
using System.IO.Ports;
using WeightDev.Constants;

namespace WeightDev
{
    public partial class WeightDevRepository
    {
        #region KELI_XK3196E2_WIM
        private void COMM_KELI_XK3196E2_WIM_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            switch (e.EventType)
            {
                case SerialError.Frame:
                    break;
                case SerialError.Overrun:
                    break;
                case SerialError.RXOver:
                    break;
                case SerialError.RXParity:
                    break;
                case SerialError.TXFull:
                    break;
            }
        }







        private void COMM_KELI_XK3196E2_WIM_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            switch (e.EventType)
            {
                case SerialData.Chars:
                    try
                    {
                        /// ï     0
                        /// ASCII: 195 175 2 16 32 32 32 32 32 48
                        /// 
                        if (Started == false) return;

                        //// COMM.Read
                        //char[] cs = new char[DataLength];
                        //byte[] bs = new byte[COMM.ReadBufferSize];
                        //COMM.Read(bs,0, COMM.ReadBufferSize);
                        //cs = System.Text.Encoding.UTF8.GetChars(bs);
                        //string signal = new string(cs);
                        //COMM.DiscardInBuffer();
                        //System.Threading.Thread.Sleep(500);

                        byte[] bs = new byte[1];

                        COMM.Read(bs, 0, 1);

                        ReceivedData.Add(bs[0]);

                        //string signal = String.Empty;
                        //if (COMMReadType == Enums.COMMReadType.TYPE_1)
                        //{
                        //    signal = COMM.ReadLine();
                        //} 
                        //else if (COMMReadType == Enums.COMMReadType.TYPE_2)
                        //{
                        //    signal = COMM.ReadExisting();
                        //}

                        //SetSignalValue(signal);
                        //SetSignaLength(signal);
                        //COMM.DiscardInBuffer();


                        //if (DataLength != signal.Length) return;

                        //SetValue(signal.ToString());
                        //if (signal.Length > DataLength)
                        //{
                        //    COMM.DiscardInBuffer();
                        //    return;
                        //}


                        //SetValue(signal.ToString());
                        //var isNegative = signal.Contains("-");
                        //var filtered = signal;

                        //filtered = filtered.Replace("G", "");
                        //filtered = filtered.Replace("K", "");
                        //filtered = filtered.Replace("M", "");
                        //filtered = filtered.Replace("N", "");
                        //filtered = filtered.Replace(" ", "");

                        //SetValue(filtered.ToString());

                        //var isNumeric = int.TryParse(filtered,   out int intFilter);
                        //if (!isNumeric) return;

                        //SetValue(intFilter.ToString());

                        //COMM.DiscardOutBuffer();

                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        SetValue(CONST_STR.NO_WEIGHT);
                    }

                    break;
                case SerialData.Eof:
                    SetValue(CONST_STR.NO_WEIGHT);
                    break;
            }
        }

        #endregion
    }
}
