using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Euclid.Las.Points;
using Euclid.Las.Headers;
using Euclid.Las.Headers.Interfaces;
using Euclid.Las.Reader.Stream.Interfaces;

namespace Euclid.Las.Reader.Stream.Interfaces
{
    internal interface IStreamHandler
    {
        ulong ReadTicks { get; }

        ulong PointsRead { get; }
        ulong PointsYielded { get; }
        ulong PointsReturned { get; }

        long BytesRemaining { get; }

        bool EOF { get; }

        Type PointType { get; }

        ILasHeader Header { get; }
        IEnumerable<LasVariableLengthRecord> VLRs { get; }

        T ReadOfType<T>();
        void Seek(long pos);
        void Dispose();

        LasPoint GetNextPoint();
        void GetNextPoint(ref LasPoint lpt);
    }
}
