using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeightDev.Constants;

namespace WeightDev
{
   public  partial class WeightDevRepository
    {
        #region ZM405V2
        private  void COMM_ZM405_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
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
        private void COMM_ZM405_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
    
            switch (e.EventType)
            {
                case SerialData.Chars:
                    try
                    {
                        var signal = COMM.ReadLine();
                        //byte[] bytes = Encoding.ASCII.GetBytes(signal);

                        //for (int i = 0; i<= bytes.Length-1;i++)
                        //{
                        //    ReceivedData.Add(bytes[i]);
                        //}

                        var filtered = signal.Replace("\r", "");
                        filtered = filtered.Replace(" ", "");
                        SetValue(signal);
                        //;
                        COMM.DiscardInBuffer();
                        COMM.DiscardOutBuffer();

                        //if (ReceivedData.Count >= DataLength)
                        //{
                        //    var d = System.Text.Encoding.UTF8.GetString(ReceivedData.ToArray());
                        //    WeightData = d.Substring(ExtStartIndex, ExtLength);
                        //}
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

            //switch (e.EventType)
            //{
            //    case SerialData.Chars:
            //        string data = COMM.ReadLine();
            //        string filteredData = data;
            //        this.SetValue(filteredData);
            //        break;
            //    case SerialData.Eof:
            //        break;
            //}
        }
        #endregion
    }
}
