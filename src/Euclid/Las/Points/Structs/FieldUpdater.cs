﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euclid.Las.Points.Structs
{
    public class FieldUpdater
    {
        /* NOTE:
         * - 1byte scan angle: -128..127 byte values corresponds to -90...90 degrees.
         * - 2byte: -30,000...+30,000 values correspond to +-180 degrees (PRF 6+)
         * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

        public const double ByteToShortScanAngle = 30000.0 / 127.0;
        public const double ShortScanAngleToDeg = 180.0 / 30000.0;

        public static short ScanAngle(byte scanAngle)
        {
            return (short)(ByteToShortScanAngle * scanAngle);
        }
    }
}
