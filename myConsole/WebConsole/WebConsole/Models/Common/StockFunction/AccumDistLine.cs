using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebConsole.Models.Common.Entities;

namespace WebConsole.Models.Common {
    public sealed partial class AnalysisEngine {
        /// <summary>
        /// Calculates Accumulation/Distribution Line
        /// </summary>
        /// <param name="highs">Signal representing price highs</param>
        /// <param name="lows">Signal representing price lows</param>
        /// <param name="closes">Signal representing closing prices</param>
        /// <param name="volumes">Signal representing volumes</param>
        /// <returns>Object containing operation results</returns>
        public static AccumDistLineResult AccumDistLine(IEnumerable<double> highs, IEnumerable<double> lows, IEnumerable<double> closes, IEnumerable<long> volumes)
        {
            List<double> returnList = new List<double>();
            long previousAccumDistLine = long.MinValue;
            for (int i = 0; i < highs.Count(); i++)
            {
                double multiplier; 
                double high = highs.ElementAt(i);
                double low = lows.ElementAt(i);
                double close = closes.ElementAt(i);

                if (Math.Abs((high - low) - 0.0) < 0.001) {
                    multiplier = 0;
                }
                else {
                    multiplier = ((close - low) - (high - close)) / (high - low);
                }
                long accumDist = Convert.ToInt64(multiplier * (volumes.ElementAt(i) / 1000));
                if (previousAccumDistLine != long.MinValue)
                {
                    accumDist = accumDist + previousAccumDistLine;
                }
                previousAccumDistLine = accumDist;
                returnList.Add(accumDist);
            }

            var result = new AccumDistLineResult {
                StartIndexOffset = 0,
                Values = returnList
            };

            return result;
        }

    }
}
