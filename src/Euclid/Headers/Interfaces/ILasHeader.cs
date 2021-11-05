using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using MathNet.Numerics.LinearAlgebra;

namespace Euclid.Headers.Interfaces
{
    public interface ILasHeader
    {
        ulong PointCount { get; }
        double ScaleX { get; }
        double ScaleY { get; }
        double ScaleZ { get; }
        double OriginX { get; }
        double OriginY { get; }
        double OriginZ { get; }
        double MaxX { get; }
        double MinX { get; }
        double MaxY { get; }
        double MinY { get; }
        double MaxZ { get; }
        double MinZ { get; }
        ushort PointDataRecordLength { get; }
        ushort HeaderSize { get; }
        uint OffsetToPointData { get; }
        uint NumberOfVLRs { get; }
        byte PointDataFormat { get; }
        byte VersionMajor { get; }
        byte VersionMinor { get; }
        ushort CreationYear { get; }
        ushort CreationDOY { get; }

        void SetPointCount(ulong count);
        void CheckExtrema(Vector<double> pos);
        void SetNumVLRs(uint numVlrs);

        void UpdateScale(ILasHeader header);
        void UpdateOrigin(ILasHeader header);
        void SetOffsetToPointData(uint offset);
    }
}

    
