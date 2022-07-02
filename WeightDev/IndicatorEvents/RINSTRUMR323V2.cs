using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeightDev.Constants;

namespace WeightDev
{
    public partial class WeightDevRepository
    {
       
        #region RINSTRUMR323V2
        private void COMM_RINSTRUMR323V2_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
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

        private void COMM_RINSTRUMR323V2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            switch (e.EventType)
            {
                case SerialData.Chars:
                    try
                    {
                        //evt.Wait(10);
                        //evt.Set();

                        //ST,GS,+000000.kg
                        //ST,GS,+000000.kg
                        //ST,GS,+000000.kg
                        //ST,GS,+000000.kg
                        //ST,GS,+000000.kg
                        //ST,GS,+001260.kg\r\n
                        // FORMAR_4
                        //ReceivedData.Add((byte)COMM.ReadByte());

                        //
                        //var d = System.Text.Encoding.UTF8.GetString(ReceivedData.ToArray());
                        //System.Diagnostics.Debug.WriteLine(d);
                        //if (Started == false) return;
                        var signal = COMM.ReadLine();

                        SetSignalValue(signal);
                        SetSignaLength(signal);

                        if (signal.Length > 17) return;

                        var filtered = signal.Replace(",", "");
                        filtered = filtered.Replace("US", "");
                        filtered = filtered.Replace("GS", "");
                        filtered = filtered.Replace("ST", "");
                        filtered = filtered.Replace("NT", "");
                        filtered = filtered.Replace("\u0003", "");
                        filtered = filtered.Replace("\u0002", "");
                        filtered = filtered.Replace(".kg", "");
                        filtered = filtered.Replace(" ", "");
                        int.TryParse(filtered, out var intFilter);
                      
                        SetValue(intFilter.ToString());
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
