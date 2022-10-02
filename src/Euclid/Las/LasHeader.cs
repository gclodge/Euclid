using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Euclid.Las.Interfaces;

namespace Euclid.Las
{
    public class LasHeader : ILasHeader
    {
        #region ILasHeader Fields & Properties
        public uint FileSignature { get; set; } = Constants.LasHeaderSignature;

        public ushort FileSourceID { get; set; } = 0;
        public ushort GlobalEncoding { get; set; } = LasHelper.GetGlobalEncoding();

        public uint Guid1 { get; set; } = 0;
        public ushort Guid2 { get; set; } = 0;
        public ushort Guid3 { get; set; } = 0;
        public char[] Guid4 { get; set; } = new char[8];

        public byte VersionMajor { get; set; } = 1;
        public byte VersionMinor { get; set; } = 2;

        public char[] SystemIdentifier { get; set; } = new char[32];
        public char[] GeneratingSoftware { get; set; } = new char[32];

        public ushort CreationYear { get; set; } = (ushort)DateTime.Now.Year;
        public ushort CreationDOY { get; set; } = (ushort)DateTime.Now.DayOfYear;
        public ushort HeaderSize { get; set; } = Constants.LasHeader12Size;

        public uint OffsetToPointData { get; set; } = Constants.LasHeader12Size;
        public uint NumberOfVLRs { get; set; } = 0;

        public byte PointDataFormat { get; private set; } = 0;
        public ushort PointDataRecordLength { get; private set; } = PointTypeMap.GetPointRecordLength(0);

        public uint LegacyNumPointRecords { get; private set; } = 0;
        public uint[] LegacyNumPointRecordsByReturn { get; private set; } = new uint[5];

        public double ScaleX { get; set; } = Constants.DefaultScale;
        public double ScaleY { get; set; } = Constants.DefaultScale;
        public double ScaleZ { get; set; } = Constants.DefaultScale;

        public double OriginX { get; set; } = Constants.DefaultOffset;
        public double OriginY { get; set; } = Constants.DefaultOffset;
        public double OriginZ { get; set; } = Constants.DefaultOffset;

        public double MaxX { get; set; } = double.MinValue;
        public double MinX { get; set; } = double.MaxValue;
        public double MaxY { get; set; } = double.MinValue;
        public double MinY { get; set; } = double.MaxValue;
        public double MaxZ { get; set; } = double.MinValue;
        public double MinZ { get; set; } = double.MaxValue;

        public ulong StartOfWaveformDataPacketRecord { get; set; } = 0;
        public ulong StartOfFirstExtendedVLR { get; set; } = 0;
        public uint NumExtendedVLRs { get; set; } = 0;
        public ulong NumPointRecords { get; private set; } = 0;
        public ulong[] NumPointRecordsByReturn { get; set; } = new ulong[15];

        public ulong PointCount { get; private set; } = 0;
        #endregion

        #region ILasHeader Methods
        public void CheckExtrema(IEnumerable<double> pos)
        {
            if (pos.Count() != 3) throw new ArgumentException($"Input position was not 3-Dimensional!");

            MinX = Math.Min(MinX, pos.ElementAt(0));
            MinY = Math.Min(MinY, pos.ElementAt(1));
            MinZ = Math.Min(MinZ, pos.ElementAt(2));

            MaxX = Math.Max(MaxX, pos.ElementAt(0));
            MaxY = Math.Max(MaxY, pos.ElementAt(1));
            MaxZ = Math.Max(MaxZ, pos.ElementAt(2));
        }

        public void SetPointCount(ulong count)
        {
            LegacyNumPointRecords = (uint)count;
            NumPointRecords = count;
            PointCount = count;
        }

        public void SetPointDataFormat(byte format)
        {
            if (format < 0 || format > 10) throw new ArgumentException($"PointRecordFormat must be between range of [0, 10].");

            PointDataFormat = format;
        }

        public void SetNumVLRs(uint numVlrs)
        {
            NumberOfVLRs = numVlrs;
        }

        public void SetOffsetToPointData(uint offset)
        {
            OffsetToPointData = offset;
        }

        public void UpdateScale(ILasHeader header)
        {
            UpdateScale(header.ScaleX, header.ScaleY, header.ScaleZ);
        }

        public void UpdateScale(double x, double y, double z)
        {
            ScaleX = x;
            ScaleY = y;
            ScaleZ = z;
        }

        public void UpdateOrigin(ILasHeader header)
        {
            UpdateOrigin(header.OriginX, header.OriginY, header.OriginZ);
        }

        public void UpdateOrigin(double x, double y, double z)
        {
            OriginX = x;
            OriginY = y;
            OriginZ = z;
        }
        #endregion

        #region Static Helper Methods
        public static LasHeader ReadFromPath(string path)
        {
            using var stream = new StreamReader(path);
            using var reader = new BinaryReader(stream.BaseStream);

            return ReadFromStream(reader);
        }

        public static LasHeader ReadFromStream(BinaryReader reader)
        {
            var header = new LasHeader();

            #region LAS 1.2 Parsing
            header.FileSignature = reader.ReadUInt32();
            header.FileSourceID = reader.ReadUInt16();
            header.GlobalEncoding = reader.ReadUInt16();
            header.Guid1 = reader.ReadUInt32();
            header.Guid2 = reader.ReadUInt16();
            header.Guid3 = reader.ReadUInt16();
            header.Guid4 = reader.ReadChars(8);
            header.VersionMajor = reader.ReadByte();
            header.VersionMinor = reader.ReadByte();
            header.SystemIdentifier = reader.ReadChars(32);
            header.GeneratingSoftware = reader.ReadChars(32);
            header.CreationDOY = reader.ReadUInt16();
            header.CreationYear = reader.ReadUInt16();
            header.HeaderSize = reader.ReadUInt16();
            header.OffsetToPointData = reader.ReadUInt32();
            header.NumberOfVLRs = reader.ReadUInt32();
            header.PointDataFormat = reader.ReadByte();
            header.PointDataRecordLength = reader.ReadUInt16();
            for (int i = 0; i < header.LegacyNumPointRecordsByReturn.Length; i++)
            {
                header.LegacyNumPointRecordsByReturn[i] = reader.ReadUInt32();
            }
            header.ScaleX = reader.ReadDouble();
            header.ScaleY = reader.ReadDouble();
            header.ScaleZ = reader.ReadDouble();
            header.OriginX = reader.ReadDouble();
            header.OriginY = reader.ReadDouble();
            header.OriginZ = reader.ReadDouble();
            header.MaxX = reader.ReadDouble();
            header.MinX = reader.ReadDouble();
            header.MaxY = reader.ReadDouble();
            header.MinY = reader.ReadDouble();
            header.MaxZ = reader.ReadDouble();
            header.MinZ = reader.ReadDouble();
            #endregion

            //< If we're just LAS 1.2 - we can return the header now
            if (header.VersionMajor == 1 && header.VersionMinor == 2) return header;

            //< Otherwise - parse the remaining LAS 1.4 fields
            header.StartOfWaveformDataPacketRecord = reader.ReadUInt64();
            header.StartOfFirstExtendedVLR = reader.ReadUInt64();
            header.NumExtendedVLRs = reader.ReadUInt32();
            header.NumPointRecords = reader.ReadUInt64();
            for (int i = 0; i < header.NumPointRecordsByReturn.Length; i++)
            {
                header.NumPointRecordsByReturn[i] = reader.ReadUInt64();
            }

            return header;
        }
        #endregion
    }
}
