using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebConsole.Models.Model
{
    public class LineStru
    {
        public string name
        {
            get;
            set;
        }
        public string date
        {
            get;
            set;
        }
        public string[] list_time
        {
            get;
            set;
        }
        public int[] list_vol
        {
            get;
            set;
        }

        public double[] list_price
        {
            get;
            set;
        }
    }
}
