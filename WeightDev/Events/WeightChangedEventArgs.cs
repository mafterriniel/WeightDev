using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
