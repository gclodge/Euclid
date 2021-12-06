using System.Runtime.InteropServices;

using Euclid.Las.Points.Interfaces;

namespace Euclid.Las.Points.Structs
{
    [StructLayout(LayoutKind.Explicit, Size = 34)]
    public struct LasPointRecordFormat3 : ILasPointStruct, ILasTime, ILasRgb
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
        public ushort R => _R;
        public ushort G => _G;
        public ushort B => _B;
        public double Timestamp => _Timestamp;
        #endregion
    }
}
