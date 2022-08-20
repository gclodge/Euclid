using System;

using Euclid.Las.Points;
using Euclid.Las.Points.Interfaces;
using Euclid.Las.Headers.Interfaces;
using Euclid.Las.Stream.Interfaces;

namespace Euclid.Las.Stream
{
    internal class AsyncStreamBuffer : IStreamBuffer
    {
        public Array Data { get; private set; }

        public int Loaded { get; private set; } = 0;
        public int Consumed { get; private set; } = 0;

        public int Available => Loaded - Consumed;

        public AsyncStreamBuffer(uint count, Type type)
        {
            Data = Array.CreateInstance(type, count);
        }

        public void SetLoaded(int num)
        {
            Loaded = num;
        }

        public void SetConsumed(int num)
        {
            Consumed = num;
        }

        public LasPoint GetNext(ILasHeader header)
        {
            LasPoint lpt = new LasPoint();
            GetNext(header, ref lpt);
            return lpt;
        }

        public void GetNext(ILasHeader header, ref LasPoint lpt)
        {
            var p = Data.GetValue(Consumed);
            Consumed++;

            //< Update with all the required/usual fields
            lpt.Update((ILasPointStruct)p, header);

            //< Check and update as necessary for timestamped / RGB / 4band point data
            if (p is ILasTime)
            {
                lpt.Update(p as ILasTime);
            }
            if (p is ILasRgb)
            {
                lpt.Update(p as ILasRgb);
            }
            if (p is ILasWaveform)
            {
                lpt.Update(p as ILasWaveform);
            }
        }
    }
}
