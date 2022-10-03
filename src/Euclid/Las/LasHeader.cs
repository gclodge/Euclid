using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using Euclid.Las.Interfaces;

namespace Euclid.Las
{
    public class LasHeader : ILasHeader
    {
        /// <summary>
        /// The currently supported 'VersionMinor' flags - assuming a 'VersionMajor' of 1
        /// </summary>
        public static readonly HashSet<int> SupportedVersionMinors = new() { 2, 4 };

        public const int IdentifierLength = 32;

        #region ILasHeader Fields & Properties
        public uint FileSignature { get; private set; } = Constants.LasHeaderSignature;

        public ushort FileSourceID { get; set; } = 0;
        public ushort GlobalEncoding { get; set; } = LasHelper.GetGlobalEncoding();

        public uint Guid1 { get; set; } = 0;
        public ushort Guid2 { get; set; } = 0;
        public ushort Guid3 { get; set; } = 0;
        public char[] Guid4 { get; set; } = new char[8];

        public byte VersionMajor { get; set; } = 1;
        public byte VersionMinor { get; set; } = 2;

        public char[] SystemIdentifier { get; set; } = new char[IdentifierLength];
        public char[] GeneratingSoftware { get; set; } = new char[IdentifierLength];

        public ushort CreationYear { get; set; } = (ushort)DateTime.Now.Year;
        public ushort CreationDOY { get; set; } = (ushort)DateTime.Now.DayOfYear;
        public ushort HeaderSize { get; set; } = Constants.LasHeader12Size;

        public uint OffsetToPointData { get; set; } = Constants.LasHeader12Size;
        public uint NumberOfVLRs { get; set; } = 0;

        public byte PointDataFormat { get; private set; } = 0;
        public ushort PointDataRecordLength { get; private set; } = PointTypeMap.GetPointRecordLength(0);

        public uint LegacyNumPointRecords { get; set; } = 0;
        public uint[] LegacyNumPointRecordsByReturn { get; set; } = new uint[5];

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

        public ulong PointCount => NumPointRecords < LegacyNumPointRecords ? LegacyNumPointRecords : NumPointRecords;
        #endregion

        #region ILasHeader Methods
        public void SetPointCount(ulong count)
        {
            LegacyNumPointRecords = (uint)count;
            NumPointRecords = count;
        }

        public void SetSystemIdentifier(string systemIdentifier)
        {
            if (systemIdentifier == null) throw new ArgumentNullException(nameof(systemIdentifier));
            
            char[] sid = systemIdentifier.ToCharArray();
            SetSystemIdentifier(sid);
        }

        public void SetSystemIdentifier(char[] systemIdentifier)
        {
            if (systemIdentifier.Length > IdentifierLength)
                SystemIdentifier = systemIdentifier.Take(IdentifierLength).ToArray();
            else if (systemIdentifier.Length < IdentifierLength)
                SystemIdentifier = systemIdentifier.PadRight(IdentifierLength, Constants.DefaultChar);
            else
                SystemIdentifier = systemIdentifier;
        }

        public void SetGeneratingSoftware(string generatingSoftware)
        {
            if (generatingSoftware == null) throw new ArgumentNullException(nameof(generatingSoftware));

            char[] gen = generatingSoftware.ToCharArray();
            SetGeneratingSoftware(gen);
        }

        public void SetGeneratingSoftware(char[] generatingSoftware)
        {
            if (generatingSoftware.Length > IdentifierLength)
                GeneratingSoftware = generatingSoftware.Take(IdentifierLength).ToArray();
            else if (generatingSoftware.Length < IdentifierLength)
                GeneratingSoftware = generatingSoftware.PadRight(IdentifierLength, Constants.DefaultChar);
            else
                GeneratingSoftware = generatingSoftware;
        }

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

        public void SetPointDataFormat(byte format)
        {
            if (format < 0 || format > 10) throw new ArgumentException($"PointRecordFormat must be between range of [0, 10].");

            PointDataFormat = format;
            PointDataRecordLength = PointTypeMap.GetPointRecordLength(PointDataFormat);
        }

        public void SetNumVLRs(uint numVlrs)
        {
            if (numVlrs < 0) throw new ArgumentException($"Cannot set Number of Variable Length Records to a negative value");

            NumberOfVLRs = numVlrs;
        }

        public void SetOffsetToPointData(uint offset)
        {
            OffsetToPointData = offset;
        }

        public void SetScale(ILasHeader header)
        {
            SetScale(header.ScaleX, header.ScaleY, header.ScaleZ);
        }

        public void SetScale(double x, double y, double z)
        {
            ScaleX = x;
            ScaleY = y;
            ScaleZ = z;
        }

        public void SetOrigin(ILasHeader header)
        {
            SetOrigin(header.OriginX, header.OriginY, header.OriginZ);
        }

        public void SetOrigin(double x, double y, double z)
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
            if (header.FileSignature != Constants.LasHeaderSignature) throw new Exception($"Invalid 'FileSignature' encountered: {header.FileSignature} - should only ever be {Constants.LasHeaderSignature}");

            header.FileSourceID = reader.ReadUInt16();
            header.GlobalEncoding = reader.ReadUInt16();
            header.Guid1 = reader.ReadUInt32();
            header.Guid2 = reader.ReadUInt16();
            header.Guid3 = reader.ReadUInt16();
            header.Guid4 = reader.ReadChars(8);
            header.VersionMajor = reader.ReadByte();
            header.VersionMinor = reader.ReadByte();

            if (header.VersionMajor != 1) throw new Exception($"Unknown 'VersionMajor' encountered: {header.VersionMajor}");
            if (!SupportedVersionMinors.Contains(header.VersionMinor)) throw new Exception($"Unsupported 'VersionMinor' encountered: {header.VersionMinor} - currently we support only LAS1.2 & 1.4");

            header.SystemIdentifier = reader.ReadChars(32);
            header.GeneratingSoftware = reader.ReadChars(32);
            header.CreationDOY = reader.ReadUInt16();
            header.CreationYear = reader.ReadUInt16();
            header.HeaderSize = reader.ReadUInt16();
            header.OffsetToPointData = reader.ReadUInt32();
            header.NumberOfVLRs = reader.ReadUInt32();
            header.PointDataFormat = reader.ReadByte();
            header.PointDataRecordLength = reader.ReadUInt16();
            header.LegacyNumPointRecords = reader.ReadUInt32();
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
            if (header.VersionMinor == 2) return header;

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
