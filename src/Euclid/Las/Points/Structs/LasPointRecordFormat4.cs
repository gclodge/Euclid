using System.Runtime.InteropServices;

using Euclid.Las.Points.Interfaces;

namespace Euclid.Las.Points.Structs
{
    [StructLayout(LayoutKind.Explicit, Size = 57)]
    public struct LasPointRecordFormat4 : ILasPointStruct, ILasTime, ILasWaveform
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
        public byte _GlobalEncoding;
        [FieldOffset(15)]
        public byte _Classification;
        [FieldOffset(16)]
        public byte _ScanAngle;
        [FieldOffset(17)]
        public byte _UserData;

        [FieldOffset(18)]
        public ushort _FlightLine;

        [FieldOffset(20)]
        public double _Timestamp;

        [FieldOffset(28)]
        public byte _WavePacketDescriptorIndex;

        [FieldOffset(29)]
        public ulong _ByteOffsetToWaveformData;

        [FieldOffset(37)]
        public uint _WaveformPacketSizeBytes;

        [FieldOffset(41)]
        public float _ReturnPointWaveformLocation;
        [FieldOffset(45)]
        public float _X_t;
        [FieldOffset(49)]
        public float _Y_t;
        [FieldOffset(53)]
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
        public short ScanAngle => FieldUpdater.ScanAngle(_ScanAngle);
        public double Timestamp => _Timestamp;

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
