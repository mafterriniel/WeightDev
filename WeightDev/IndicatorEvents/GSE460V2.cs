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
        #region GSE460V2
        private void COMM_GSE460V2_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
        private void COMM_GSE460V2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                switch (e.EventType)
                {
                    case SerialData.Chars:
                        try
                        {

                            //ReceivedData.Add((byte)COMM.ReadByte());

                            //if (ReceivedData.Count >= (DataLength-1))
                            //{
                            //    var d = System.Text.Encoding.UTF8.GetString(ReceivedData.ToArray());
                            //    WeightData = d.Substring(ExtStartIndex, (ExtLength-1));
                            //    SetValue(WeightData);
                            //}

                            if (Started == false) return;
                            var signal = COMM.ReadLine();
                            byte[] bytes = Encoding.ASCII.GetBytes(signal);

                            if (signal.LastIndexOf(EndCharacterCHAR) != (DataLength)) return;
                            var filter = signal.Substring(ExtStartIndex, 8);
                            var intFilter = 0;
                            int.TryParse(filter, out intFilter);
                            SetValue(intFilter.ToString());
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.Message);
                            SetValue(CONST_STR.NO_WEIGHT);
                        }
                        break;



                        //string data = COMM.ReadChar();
                        //string filteredData = data;
                        //filteredData = data.Replace("\n", "");
                        //filteredData = data.Replace(" ", "");
                        //var intData = 0;
                        //Int32.TryParse(filteredData, out intData);
                        //SetValue(filteredData);
                        //this.SetLength(data.Length.ToString());
                        //break;
                    case SerialData.Eof:
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                SetValue(CONST_STR.NO_WEIGHT);
            }
        }

        #endregion
    }
}
