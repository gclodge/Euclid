using System.Runtime.InteropServices;

using Euclid.Las.Points.Interfaces;

namespace Euclid.Las.Points.Structs
{
    [StructLayout(LayoutKind.Explicit, Size = 63)]
    public struct LasPointRecordFormat5 : ILasPointStruct, ILasTime, ILasRgb, ILasWaveform
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
        public ushort _R;
        [FieldOffset(30)]
        public ushort _G;
        [FieldOffset(32)]
        public ushort _B;

        [FieldOffset(34)]
        public byte _WavePacketDescriptorIndex;

        [FieldOffset(35)]
        public ulong _ByteOffsetToWaveformData;

        [FieldOffset(43)]
        public uint _WaveformPacketSizeBytes;

        [FieldOffset(47)]
        public float _ReturnPointWaveformLocation;
        [FieldOffset(51)]
        public float _X_t;
        [FieldOffset(55)]
        public float _Y_t;
        [FieldOffset(59)]
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
        public short ScanAngle => FieldUpdater.ScanAngleShort(_ScanAngle);
        public double Timestamp => _Timestamp;
        public ushort R => _R;
        public ushort G => _G;
        public ushort B => _B;
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
