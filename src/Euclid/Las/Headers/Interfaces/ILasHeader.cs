using System.Collections.Generic;

namespace Euclid.Las.Headers.Interfaces
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
        void CheckExtrema(IEnumerable<double> pos);
        void SetNumVLRs(uint numVlrs);

        void UpdateScale(ILasHeader header);
        void UpdateScale(double x, double y, double z);
        void UpdateOrigin(ILasHeader header);
        void UpdateOrigin(double x, double y, double z);
        void SetOffsetToPointData(uint offset);
    }
}

    
