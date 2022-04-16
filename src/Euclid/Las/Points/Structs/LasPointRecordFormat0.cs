using System.Runtime.InteropServices;

using Euclid.Las.Points.Interfaces;

namespace Euclid.Las.Points.Structs
{
    [StructLayout(LayoutKind.Explicit, Size = 20)]
    public struct LasPointRecordFormat0 : ILasPointStruct
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
        private byte _GlobalEncoding;

        [FieldOffset(15)]
        private byte _Classification;

        [FieldOffset(16)]
        private byte _ScanAngle;

        [FieldOffset(17)]
        private byte _UserData;

        [FieldOffset(18)]
        private ushort _FlightLine;
        #endregion

        #region Public Fields
        public int X => _X;
        public int Y => _Y;
        public int Z => _Z;

        public byte Classification => _Classification;
        public byte UserData => _UserData;

        public ushort Intensity => _Intensity;
        public ushort FlightLine => _FlightLine;
        public ushort GlobalEncoding => _GlobalEncoding;

        public short ScanAngle => FieldUpdater.ScanAngle(_ScanAngle);
        #endregion
    }
}
