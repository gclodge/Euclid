using System.Runtime.InteropServices;

namespace Euclid.Headers.Structs
{
    [StructLayout(LayoutKind.Explicit, Size = 20)]
    public struct LegacyPointRecordsByReturn
    {
        [FieldOffset(0)]
        public uint Return0;
        [FieldOffset(4)]
        public uint Return1;
        [FieldOffset(8)]
        public uint Return2;
        [FieldOffset(12)]
        public uint Return3;
        [FieldOffset(16)]
        public uint Return4;
    }

    [StructLayout(LayoutKind.Explicit, Size = 120)]
    public struct PointRecordsByReturn
    {
        [FieldOffset(0)]
        public ulong Return0;
        [FieldOffset(1 * 8)]
        public ulong Return1;
        [FieldOffset(2 * 8)]
        public ulong Return2;
        [FieldOffset(3 * 8)]
        public ulong Return3;
        [FieldOffset(4 * 8)]
        public ulong Return4;
        [FieldOffset(5 * 8)]
        public ulong Return5;
        [FieldOffset(6 * 8)]
        public ulong Return6;
        [FieldOffset(7 * 8)]
        public ulong Return7;
        [FieldOffset(8 * 8)]
        public ulong Return8;
        [FieldOffset(9 * 8)]
        public ulong Return9;
        [FieldOffset(10 * 8)]
        public ulong Return10;
        [FieldOffset(11 * 8)]
        public ulong Return11;
        [FieldOffset(12 * 8)]
        public ulong Return12;
        [FieldOffset(13 * 8)]
        public ulong Return13;
        [FieldOffset(14 * 8)]
        public ulong Return14;
    }
}
