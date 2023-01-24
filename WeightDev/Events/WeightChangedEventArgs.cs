using System;

namespace WeightDev.Events
{
    public class WeightChangedEventArgs : EventArgs
    {

        public WeightChangedEventArgs(string label = "", decimal weight = 0)
        {
            Label = label;
            Weight = weight;
        }

        public string Label { get; set; }
        public decimal Weight { get; set; }
    }

    public class AxleWtAddedEventArgs : EventArgs
    {

        public AxleWtAddedEventArgs(int axleNum, decimal weight, DateTime DateCaptured)
        {
            AxleNum = axleNum;
            Weight = weight;
            this.DateCaptured = DateCaptured;
        }

        public int AxleNum { get; set; }
        public decimal Weight { get; set; }

        public DateTime DateCaptured { get; set; }
    }
}
