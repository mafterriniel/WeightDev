using System;
using System.IO.Ports;
using WeightDev.Constants;

namespace WeightDev
{
    public partial class WeightDevRepository
    {
        #region RINSTRUMR323V4
        private void COMM_RINSTRUMR323V4_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
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

        private void COMM_RINSTRUMR323V4_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            switch (e.EventType)
            {
                case SerialData.Chars:
                    try
                    {

                        //       0KG
                        if (Started == false) return;

                        var signal = COMM.ReadLine();

                        SetSignalValue(signal);
                        SetSignaLength(signal);
                        //SetValue(signal.ToString());
                        var isNegative = signal.Contains("-");
                        var filtered = signal.Replace("", "");
                        filtered = filtered.Replace("G", "");
                        filtered = filtered.Replace("K", "");
                        filtered = filtered.Replace("M", "");
                        filtered = filtered.Replace("N", "");
                        filtered = filtered.Replace(" ", "");

                        var isNumeric = int.TryParse(filtered, out int intFilter);
                        if (!isNumeric) return;

                        SetValue(intFilter.ToString());

                        COMM.DiscardInBuffer();

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
