using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using MathNet.Numerics.LinearAlgebra;

namespace Euclid.Headers.Interfaces
{
    public interface ILasHeader14 : ILasHeader
    {
        ulong StartOfWaveformDataPacketRecord { get; }
        ulong StartOfFirstExtendedVLR { get; }
        uint NumExtendedVLRs { get; }
        ulong NumPointRecords { get; }
        ulong[] NumPointRecordsByReturn { get; }
    }
}