using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using System.Collections.Generic;

namespace Euclid.Las
{
    public static class LasHelper
    {
        public static ushort GetGlobalEncoding(bool useGpsStandardTime = true, bool useProjWkt = false)
        {
            var newBits = new BitArray(16);

            //< Set the time flag (true being GPS Standard Time, false being Seconds of Week)
            newBits[0] = useGpsStandardTime;
            newBits[1] = false;
            newBits[2] = false;
            newBits[3] = false;
            //< Set the Projection to WKT
            newBits[4] = useProjWkt;
            //< All 'Reserved' bits set to zero
            newBits[5] = false;
            newBits[6] = false;
            newBits[7] = false;
            newBits[8] = false;
            newBits[9] = false;
            newBits[10] = false;
            newBits[11] = false;
            newBits[12] = false;
            newBits[13] = false;
            newBits[14] = false;
            newBits[15] = false;

            var bytes = new byte[2];
            newBits.CopyTo(bytes, 0);

            var value = BitConverter.ToUInt16(bytes, 0);
            return value;
        }
    }
}
