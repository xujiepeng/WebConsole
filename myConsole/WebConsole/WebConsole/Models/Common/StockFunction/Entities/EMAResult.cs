﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebConsole.Models.Common.Entities
{
    /// <summary>
    /// Contains calculation results for EMA indicator
    /// </summary>
    public class EMAResult
    {
        public List<double> Values { get; set; }

        /// <summary>
        /// Represents the index of input signal at which the indicator starts
        /// </summary>
        public int StartIndexOffset { get; set; }
    }
}
