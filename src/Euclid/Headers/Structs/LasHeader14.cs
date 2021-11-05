using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using MathNet.Numerics.LinearAlgebra;

using Euclid.Headers.Interfaces;

namespace Euclid.Headers.Structs
{
    [StructLayout(LayoutKind.Explicit, Size = 375)]
    public struct LasHeader14 : ILasHeader14
    {
        #region Fields

        [FieldOffset(0)]
        public int _Signature;

        [FieldOffset(4)]
        public ushort FileSourceID;
        [FieldOffset(6)]
        public ushort GlobalEncoding;

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

        [FieldOffset(227)]
        public ulong _StartOfWaveformDataPacketRecord;
        [FieldOffset(235)]
        public ulong _StartOfFirstExtendedVLR;
        [FieldOffset(243)]
        public uint _NumExtendedVLRs;
        [FieldOffset(247)]
        public ulong _NumPointRecords;
        [FieldOffset(255)]
        public PointRecordsByReturn _NumPointRecordsByReturn;

        #endregion

        #region 'Get' Operators
        public ulong PointCount => _NumPointRecords;
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

        public ulong StartOfWaveformDataPacketRecord => _StartOfWaveformDataPacketRecord;
        public ulong StartOfFirstExtendedVLR => _StartOfFirstExtendedVLR;
        public uint NumExtendedVLRs => _NumExtendedVLRs;
        public ulong NumPointRecords => _NumPointRecords;
        public ulong[] NumPointRecordsByReturn => _NumPointRecordsByReturn.ToArray();

        public uint LegacyNumPointRecords => _LegacyNumberPointRecords;
        public uint[] LegacyNumPointRecordsByReturn => _LegacyNumberPointRecordsByReturn.ToArray();

        #endregion

        public void SetPointCount(ulong count)
        {
            _LegacyNumberPointRecords = (uint)count;
            _NumPointRecords = count;
        }

        public void CheckExtrema(Vector<double> pos)
        {
            _MinX = Math.Min(MinX, pos[0]);
            _MinY = Math.Min(MinY, pos[1]);
            _MinZ = Math.Min(MinZ, pos[2]);

            _MaxX = Math.Max(MaxX, pos[0]);
            _MaxY = Math.Max(MaxY, pos[1]);
            _MaxZ = Math.Max(MaxZ, pos[2]);
        }

        public void SetNumVLRs(uint numVlrs)
        {
            _NumberOfVLRs = numVlrs;
        }

        public void UpdateScale(ILasHeader header)
        {
            _ScaleX = header.ScaleX;
            _ScaleY = header.ScaleY;
            _ScaleZ = header.ScaleZ;
        }

        public void UpdateOrigin(ILasHeader header)
        {
            _OriginX = header.OriginX;
            _OriginY = header.OriginY;
            _OriginZ = header.OriginZ;
        }

        public void SetOffsetToPointData(uint offset)
        {
            _OffsetToPointData = offset;
        }
        
        public static LasHeader14 CreateForWriting(Type pointType)
        {
            LasHeader14 res = new LasHeader14();
            res._VersionMajor = 1;
            res._VersionMinor = 4;
            res._Signature = Constants.LasHeaderSignature;
            res._HeaderSize = Constants.LasHeader14Size;
            res._OffsetToPointData = Constants.LasHeader14Size;
            res._MaxX = res._MaxY = res._MaxZ = double.MinValue;
            res._MinX = res._MinY = res._MinZ = double.MaxValue;
            res._ScaleX = res._ScaleY = res._ScaleZ = Constants.DefaultScale;
            res._OriginX = res._OriginY = res._OriginZ = Constants.DefaultOffset;
            res._LegacyNumberPointRecords = 0;
            res._LegacyNumberPointRecordsByReturn = new LegacyPointRecordsByReturn();
            res._NumberOfVLRs = 0;
            res._PointDataFormat = Points.PointTypeMap.PointDataFormatByType[pointType];
            res._PointDataRecordLength = Points.PointTypeMap.SizeByType[pointType];
            res._NumExtendedVLRs = 0;
            res._StartOfFirstExtendedVLR = Constants.LasHeader14Size; //< User will need to change this to file size
            res._NumPointRecords = 0;
            res._NumPointRecordsByReturn = new PointRecordsByReturn();
            return res;
        }

        public static LasHeader14 CreateForWriting(byte pointRecordFormat)
        {
            var type = Points.PointTypeMap.TypeByPointDataFormat[pointRecordFormat];
            return CreateForWriting(type);
        }
    }
}
