using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Euclid.Las.Headers.Interfaces;

namespace Euclid.Las.Headers.Structs
{
    [StructLayout(LayoutKind.Explicit, Size = 227)]
    public struct LasHeader : ILasHeader
    {
        #region Fields
        [FieldOffset(0)]
        public uint _Signature;

        [FieldOffset(4)]
        public ushort _FileSourceID;
        [FieldOffset(6)]
        public ushort _GlobalEncoding;

        [FieldOffset(8)]
        public ulong ProjID_NonCompliant1;
        [FieldOffset(16)]
        public ulong ProjID_NonCompliant2;

        [FieldOffset(24)]
        public byte _VersionMajor;
        [FieldOffset(25)]
        public byte _VersionMinor;

        [FieldOffset(90)]
        public ushort _FileCreationDOY;
        [FieldOffset(92)]
        public ushort _FileCreationYear;
        [FieldOffset(94)]
        public ushort _HeaderSize;

        [FieldOffset(96)]
        public uint _OffsetToPointData;
        [FieldOffset(100)]
        public uint _NumberOfVLRs;

        [FieldOffset(104)]
        public byte _PointDataFormat;

        [FieldOffset(105)]
        public ushort _PointDataRecordLength;

        [FieldOffset(107)]
        public uint _LegacyNumberPointRecords;
        [FieldOffset(111)]
        public LegacyPointRecordsByReturn _LegacyNumberPointRecordsByReturn;

        [FieldOffset(131 + 8 * 0)]
        public double _ScaleX;
        [FieldOffset(131 + 8 * 1)]
        public double _ScaleY;
        [FieldOffset(131 + 8 * 2)]
        public double _ScaleZ;

        [FieldOffset(131 + 8 * 3)]
        public double _OriginX;
        [FieldOffset(131 + 8 * 4)]
        public double _OriginY;
        [FieldOffset(131 + 8 * 5)]
        public double _OriginZ;

        [FieldOffset(131 + 8 * 6)]
        public double _MaxX;
        [FieldOffset(131 + 8 * 7)]
        public double _MinX;
        [FieldOffset(131 + 8 * 8)]
        public double _MaxY;
        [FieldOffset(131 + 8 * 9)]
        public double _MinY;
        [FieldOffset(131 + 8 * 10)]
        public double _MaxZ;
        [FieldOffset(131 + 8 * 11)]
        public double _MinZ;

        #endregion

        #region 'Get' Operators
        public ulong PointCount => _LegacyNumberPointRecords;
        public uint[] PointCountByReturn => _LegacyNumberPointRecordsByReturn.ToArray();
        public double ScaleX => _ScaleX;
        public double ScaleY => _ScaleY;
        public double ScaleZ => _ScaleZ;
        public double OriginX => _OriginX;
        public double OriginY => _OriginY;
        public double OriginZ => _OriginZ;
        public double MaxX => _MaxX;
        public double MinX => _MinX;
        public double MaxY => _MaxY;
        public double MinY => _MinY;
        public double MaxZ => _MaxZ;
        public double MinZ => _MinZ;
        public ushort PointDataRecordLength => _PointDataRecordLength;
        public byte PointDataFormat => _PointDataFormat;
        public uint OffsetToPointData => _OffsetToPointData;
        public uint NumberOfVLRs => _NumberOfVLRs;
        public byte VersionMajor => _VersionMajor;
        public byte VersionMinor => _VersionMinor;

        public ushort CreationDOY => _FileCreationDOY;
        public ushort CreationYear => _FileCreationYear;
        public ushort HeaderSize => _HeaderSize;
        #endregion

        #region ILasHeader Methods
        public void CheckExtrema(IEnumerable<double> pos)
        {
            if (pos.Count() != 3) throw new ArgumentException($"Input position was not 3-Dimensional!");

            _MinX = Math.Min(MinX, pos.ElementAt(0));
            _MinY = Math.Min(MinY, pos.ElementAt(1));
            _MinZ = Math.Min(MinZ, pos.ElementAt(2));

            _MaxX = Math.Max(MaxX, pos.ElementAt(0));
            _MaxY = Math.Max(MaxY, pos.ElementAt(1));
            _MaxZ = Math.Max(MaxZ, pos.ElementAt(2));
        }

        public void SetPointCount(ulong count)
        {
            _LegacyNumberPointRecords = (uint)count;
        }

        public void SetNumVLRs(uint numVlrs)
        {
            _NumberOfVLRs = numVlrs;
        }

        public void SetOffsetToPointData(uint offset)
        {
            _OffsetToPointData = offset;
        }

        public void UpdateScale(ILasHeader header)
        {
            UpdateScale(header.ScaleX, header.ScaleY, header.ScaleZ);
        }

        public void UpdateScale(double x, double y, double z)
        {
            _ScaleX = x;
            _ScaleY = y;
            _ScaleZ = z;
        }

        public void UpdateOrigin(ILasHeader header)
        {
            UpdateOrigin(header.OriginX, header.OriginY, header.OriginZ);
        }

        public void UpdateOrigin(double x, double y, double z)
        {
            _OriginX = x;
            _OriginY = y;
            _OriginZ = z;
        }
        #endregion

        #region Static Helper Methods
        public static LasHeader CreateForWriting(Type pointType)
        {
            LasHeader res = new();
            res._VersionMajor = 1;
            res._VersionMinor = 2;
            res._Signature = Constants.LasHeaderSignature;
            res._HeaderSize = Constants.LasHeaderSize;
            res._OffsetToPointData = Constants.LasHeaderSize;
            res._MaxX = res._MaxY = res._MaxZ = double.MinValue;
            res._MinX = res._MinY = res._MinZ = double.MaxValue;
            res._ScaleX = res._ScaleY = res._ScaleZ = Constants.DefaultScale;
            res._OriginX = res._OriginY = res._OriginZ = Constants.DefaultOffset;
            res._LegacyNumberPointRecords = 0;
            res._LegacyNumberPointRecordsByReturn = new();
            res._NumberOfVLRs = 0;
            res._PointDataFormat = Points.PointTypeMap.PointDataFormatByType[pointType];
            res._PointDataRecordLength = Points.PointTypeMap.SizeByType[pointType];
            return res;
        }

        public static LasHeader CreateForWriting(byte pointRecordFormat)
        {
            var type = Points.PointTypeMap.TypeByPointDataFormat[pointRecordFormat];
            return CreateForWriting(type);
        }
        #endregion
    }
}
