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

                        // ST,GS,+001260.kg\r\n
                        // FORMAR_4
                        //ReceivedData.Add((byte)COMM.ReadByte());

                        //var d = System.Text.Encoding.UTF8.GetString(ReceivedData.ToArray());
                        //System.Diagnostics.Debug.WriteLine(d);
                        if (Started == false) return;
                        var signal = COMM.ReadLine();
                        byte[] bytes = Encoding.ASCII.GetBytes(signal);

                        if (signal.LastIndexOf(EndCharacterCHAR) != (DataLength - 1)) return;
                        var filter = signal.Substring(ExtStartIndex, 7);
                        var intFilter = 0;
                        int.TryParse(filter,  out intFilter);
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
