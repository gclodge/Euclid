﻿using System.Runtime.InteropServices;

using Euclid.Points.Interfaces;

namespace Euclid.Points.Structs
{
    [StructLayout(LayoutKind.Explicit, Size = 67)]
    public struct LasPointRecordFormat10 : ILasPointStruct, ILasTime, ILas4Band, ILasWaveform
    {
        [FieldOffset(4 * 0)]
        public int _X;
        [FieldOffset(4 * 1)]
        public int _Y;
        [FieldOffset(4 * 2)]
        public int _Z;

        [FieldOffset(4 * 3)]
        public ushort _Intensity;
        [FieldOffset(14)]
        public ushort _GlobalEncoding;

        [FieldOffset(16)]
        public byte _Classification;
        [FieldOffset(17)]
        public byte _UserData;
        [FieldOffset(18)]
        public short _ScanAngle;

        [FieldOffset(20)]
        public ushort _FlightLine;

        [FieldOffset(22)]
        public double _Timestamp;

        [FieldOffset(30)]
        public ushort _R;
        [FieldOffset(32)]
        public ushort _G;
        [FieldOffset(34)]
        public ushort _B;
        [FieldOffset(36)]
        public ushort _NIR;

        [FieldOffset(38)]
        public byte _WavePacketDescriptorIndex;

        [FieldOffset(39)]
        public ulong _ByteOffsetToWaveformData;

        [FieldOffset(47)]
        public uint _WaveformPacketSizeBytes;

        [FieldOffset(51)]
        public float _ReturnPointWaveformLocation;
        [FieldOffset(55)]
        public float _X_t;
        [FieldOffset(59)]
        public float _Y_t;
        [FieldOffset(63)]
        public float _Z_t;

        #region Field Exposition
        public int X => _X;
        public int Y => _Y;
        public int Z => _Z;
        public byte Classification => _Classification;
        public byte UserData => _UserData;

        public ushort Intensity => _Intensity;
        public ushort FlightLine => _FlightLine;
        public ushort GlobalEncoding => _GlobalEncoding;
        public short ScanAngle => _ScanAngle;

        public double Timestamp => _Timestamp;

        public ushort R => _R;
        public ushort G => _G;
        public ushort B => _B;
        public ushort NIR => _NIR;

        public byte WavePacketDescriptorIndex => _WavePacketDescriptorIndex;
        public ulong ByteOffsetToWaveformData => _ByteOffsetToWaveformData;
        public uint WaveformPacketSizeBytes => _WavePacketDescriptorIndex;

        public float ReturnPointWaveformLocation => _ReturnPointWaveformLocation;
        public float X_t => _X_t;
        public float Y_t => _Y_t;
        public float Z_t => _Z_t;
        #endregion
    }
}
