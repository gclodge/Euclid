﻿using System.Runtime.InteropServices;

using Euclid.Las.Headers.Interfaces;
using Euclid.Las.Points.Interfaces;

namespace Euclid.Las.Points.Structs
{
    [StructLayout(LayoutKind.Explicit, Size = 59)]
    public struct LasPointRecordFormat9 : ILasPointStruct, ILasTime, ILasWaveform
    {
        #region Private Fields
        [FieldOffset(4 * 0)]
        private int _X;
        [FieldOffset(4 * 1)]
        private int _Y;
        [FieldOffset(4 * 2)]
        private int _Z;

        [FieldOffset(4 * 3)]
        private ushort _Intensity;
        [FieldOffset(14)]
        private ushort _GlobalEncoding;

        [FieldOffset(16)]
        private byte _Classification;
        [FieldOffset(17)]
        private byte _UserData;
        [FieldOffset(18)]
        private short _ScanAngle;

        [FieldOffset(20)]
        private ushort _FlightLine;

        [FieldOffset(22)]
        private double _Timestamp;

        [FieldOffset(30)]
        private byte _WavePacketDescriptorIndex;

        [FieldOffset(31)]
        private ulong _ByteOffsetToWaveformData;

        [FieldOffset(39)]
        private uint _WaveformPacketSizeBytes;

        [FieldOffset(43)]
        private float _ReturnPointWaveformLocation;
        [FieldOffset(47)]
        private float _X_t;
        [FieldOffset(51)]
        private float _Y_t;
        [FieldOffset(55)]
        private float _Z_t;
        #endregion

        #region Public Fields
        public int X
        {
            get => _X;
            set => _X = value;
        }
        public int Y
        {
            get => _Y;
            set => _Y = value;
        }
        public int Z
        {
            get => _Z;
            set => _Z = value;
        }
        public byte Classification
        {
            get => _Classification;
            set => _Classification = value;
        }
        public byte UserData
        {
            get => _UserData;
            set => _UserData = value;
        }
        public ushort Intensity
        {
            get => _Intensity;
            set => _Intensity = value;
        }
        public ushort FlightLine
        {
            get => _FlightLine;
            set => _FlightLine = value;
        }
        public ushort GlobalEncoding
        {
            get => _GlobalEncoding;
            set => _GlobalEncoding = value;
        }
        public short ScanAngle
        {
            get => _ScanAngle;
            set => _ScanAngle = value;
        }
        public double Timestamp
        {
            get => _Timestamp;
            set => _Timestamp = value;
        }
        public byte WavePacketDescriptorIndex
        {
            get => _WavePacketDescriptorIndex;
            set => _WavePacketDescriptorIndex = value;
        }
        public ulong ByteOffsetToWaveformData
        {
            get => _ByteOffsetToWaveformData;
            set => _ByteOffsetToWaveformData = value;
        }
        public uint WaveformPacketSizeBytes
        {
            get => _WaveformPacketSizeBytes;
            set => _WaveformPacketSizeBytes = value;
        }
        public float ReturnPointWaveformLocation
        {
            get => _ReturnPointWaveformLocation;
            set => _ReturnPointWaveformLocation = value;
        }
        public float X_t
        {
            get => _X_t;
            set => _X_t = value;
        }
        public float Y_t
        {
            get => _Y_t;
            set => _Y_t = value;
        }
        public float Z_t
        {
            get => _Z_t;
            set => _Z_t = value;
        }
        #endregion

        public static LasPointRecordFormat9 GetLasPointStruct(LasPoint lpt, ILasHeader header)
        {
            return new LasPointRecordFormat9
            {
                _X = LasPoint.GetIntegerPosition(lpt.X, header),
                _Y = LasPoint.GetIntegerPosition(lpt.Y, header),
                _Z = LasPoint.GetIntegerPosition(lpt.Z, header),
                _Intensity = lpt.Intensity,
                _GlobalEncoding = lpt.GlobalEncoding,
                _Classification = lpt.Classification,
                _UserData = lpt.UserData,
                _ScanAngle = lpt.ScanAngle,
                _FlightLine = lpt.FlightLine,
                _Timestamp = lpt.Timestamp,
                _WavePacketDescriptorIndex = lpt.WavePacketDescriptorIndex,
                _ByteOffsetToWaveformData = lpt.ByteOffsetToWaveformData,
                _WaveformPacketSizeBytes = lpt.WaveformPacketSizeBytes,
                _ReturnPointWaveformLocation = lpt.ReturnPointWaveformLocation,
                _X_t = lpt.X_t,
                _Y_t = lpt.Y_t,
                _Z_t = lpt.Z_t
            };
        }
    }
}
