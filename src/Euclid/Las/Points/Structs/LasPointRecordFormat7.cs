using System.Runtime.InteropServices;

using Euclid.Las.Points.Interfaces;

namespace Euclid.Las.Points.Structs
{
    [StructLayout(LayoutKind.Explicit, Size = 36)]
    public struct LasPointRecordFormat7 : ILasPointStruct, ILasTime, ILasRgb
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
        #endregion
    }
}
