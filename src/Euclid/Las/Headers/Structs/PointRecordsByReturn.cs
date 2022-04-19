using System;
using System.Runtime.InteropServices;

namespace Euclid.Las.Headers.Structs
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

        public static LegacyPointRecordsByReturn Parse(uint[] values)
        {
            if (values.Length < 5) throw new ArgumentException($"LegacyPointRecordsByReturn array must have at least 5 elements");

            return new LegacyPointRecordsByReturn()
            {
                Return0 = values[0],
                Return1 = values[1],
                Return2 = values[2],
                Return3 = values[3],
                Return4 = values[4]
            };
        }
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

        public static PointRecordsByReturn Parse(uint[] values)
        {
            if (values.Length < 5) throw new ArgumentException($"PointRecordsByReturn array must have at least 15 elements");

            return new PointRecordsByReturn()
            {
                Return0 = values[0],
                Return1 = values[1],
                Return2 = values[2],
                Return3 = values[3],
                Return4 = values[4],
                Return5 = values[5],
                Return6 = values[6],
                Return7 = values[7],
                Return8 = values[8],
                Return9 = values[9],
                Return10 = values[10],
                Return11 = values[11],
                Return12 = values[12],
                Return13 = values[13],
                Return14 = values[14]
            };
        }
    }
}
