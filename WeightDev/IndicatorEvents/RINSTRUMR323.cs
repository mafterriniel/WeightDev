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
                        if (Started == false) return;

                        var signal = COMM.ReadLine();

                        var isNegative = false;
                        var filtered = signal.Replace("", "");
                        filtered = filtered.Replace("KG", "");
                        filtered = filtered.Replace("M", "");
                        isNegative = filtered.Contains("-");
                        filtered = filtered.Replace("-", "");

                        int.TryParse(filtered,   out var intFilter);
                        if (intFilter == 0)
                        {
                            System.Diagnostics.Debug.WriteLine("");
                        }
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
