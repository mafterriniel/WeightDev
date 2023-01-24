using System;
using System.IO.Ports;
using WeightDev.Constants;

namespace WeightDev
{
    public partial class WeightDevRepository
    {
        //
        /// <summary>
        ///  ALL RINSTRUM R323 FORMATS
        ///  FORMAT_6 =        0G Z- kg inline
        ///  FORMAT_5 =        0KG inline
        ///  FORMAT_4 = ST,GS,+000000.kg inline
        ///  FORMAT_3 = 00000000GSIZ newline
        ///  FORMAT_2 =        0G Z- kg inline
        ///  FORMAT_1 =        0G inline
        ///  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region RINSTRUMR323
        private void COMM_RINSTRUMR323_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
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

        private void COMM_RINSTRUMR323_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            switch (e.EventType)
            {
                case SerialData.Chars:
                    try
                    {
                        //       0G
                        //       0G

                        if (Started == false) return;

                        var signal = COMM.ReadLine();

                        if (signal.Length != DataLength) return;

                        SetSignalValue(signal);
                        SetSignaLength(signal);
                        //SetValue(signal);

                        var isNegative = false;
                        var filtered = signal.Replace("", "");
                        filtered = filtered.Replace("", "");
                        filtered = filtered.Replace("K", "");
                        filtered = filtered.Replace("G", "");
                        filtered = filtered.Replace("M", "");
                        filtered = filtered.Replace("N", "");
                        filtered = filtered.Replace(" ", "");
                        isNegative = filtered.Contains("-");
                        filtered = filtered.Replace("-", "");

                        int.TryParse(filtered, out var intFilter);
                        if (isNegative) { intFilter = intFilter * (-1); }
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
