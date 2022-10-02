using System.Collections.Generic;

namespace Euclid.Las.Interfaces
{
    public interface ILasHeader
    {
        #region ASPRS Specification Fields
        /// <summary>
        /// 4-byte 'LASF' character array that starts each LAS file
        /// </summary>
        uint FileSignature { get; }
        /// <summary>
        /// ID assigned to the LAS file based on generating flightline in range [0, 65535]
        /// </summary>
        ushort FileSourceID { get; }
        ushort GlobalEncoding { get; }

        uint Guid1 { get; }
        ushort Guid2 { get; }
        ushort Guid3 { get; }
        char[] Guid4 { get; }

        byte VersionMajor { get; }
        byte VersionMinor { get; }

        char[] SystemIdentifier { get; }
        char[] GeneratingSoftware { get; }

        ushort CreationYear { get; }
        ushort CreationDOY { get; }

        ushort HeaderSize { get; }
        uint OffsetToPointData { get; }
        uint NumberOfVLRs { get; }
        byte PointDataFormat { get; }
        ushort PointDataRecordLength { get; }
        uint LegacyNumPointRecords { get; }
        uint[] LegacyNumPointRecordsByReturn { get; }

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

        ulong StartOfWaveformDataPacketRecord { get; }
        ulong StartOfFirstExtendedVLR { get; }
        uint NumExtendedVLRs { get; }
        ulong NumPointRecords { get; }
        ulong[] NumPointRecordsByReturn { get; }
        #endregion

        ulong PointCount { get; }

        void SetNumVLRs(uint numVlrs);
        void SetPointCount(ulong count);
        void SetPointDataFormat(byte format);
        void SetOffsetToPointData(uint offset);

        void CheckExtrema(IEnumerable<double> pos);

        void UpdateScale(ILasHeader header);
        void UpdateScale(double x, double y, double z);
        void UpdateOrigin(ILasHeader header);
        void UpdateOrigin(double x, double y, double z);
    }
}
