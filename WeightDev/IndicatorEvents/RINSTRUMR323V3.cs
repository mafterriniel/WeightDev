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
        #region RINSTRUMR323V3
        private void COMM_RINSTRUMR323V3_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
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

        private void COMM_RINSTRUMR323V3_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            switch (e.EventType)
            {
                case SerialData.Chars:
                    try
                    {
                        if (Started == false) return;

                        var signal = COMM.ReadLine();

                        SetSignalValue(signal);
                        SetSignaLength(signal);
                        SetValue(signal.ToString());
                        if (signal.Length > 13) { COMM.DiscardInBuffer(); }

                        var filtered = signal.Replace("\u0003", "");
                        filtered = filtered.Replace("\u0002","");
                        filtered = filtered.Replace("GSI", "");
                        filtered = filtered.Replace("GMI", "");
                        int.TryParse(filtered,   out var intFilter);
                        if (intFilter == 0)
                        {
                            System.Diagnostics.Debug.WriteLine("");
                        }
                       
                        //SetValue(intFilter.ToString());
                       
                        //COMM.DiscardInBuffer();

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
