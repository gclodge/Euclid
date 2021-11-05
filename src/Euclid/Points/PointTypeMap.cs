using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Euclid.Points.Structs;

namespace Euclid.Points
{
    static class PointTypeMap
    {
        public static readonly Dictionary<byte, Type> TypeByPointDataFormat = new Dictionary<byte, Type>();
        public static readonly Dictionary<Type, byte> PointDataFormatByType = new Dictionary<Type, byte>();
        public static readonly Dictionary<Type, ushort> SizeByType = new Dictionary<Type, ushort>();

        static PointTypeMap()
        {
            Add<LasPointRecordFormat0>(0);
            Add<LasPointRecordFormat1>(1);
            Add<LasPointRecordFormat2>(2);
            Add<LasPointRecordFormat3>(3);
            Add<LasPointRecordFormat4>(4);
            Add<LasPointRecordFormat5>(5);
            Add<LasPointRecordFormat6>(6);
            Add<LasPointRecordFormat7>(7);
            Add<LasPointRecordFormat8>(8);
            Add<LasPointRecordFormat9>(9);
            Add<LasPointRecordFormat10>(10);
        }

        static void Add<T>(byte pointType)
        {
            TypeByPointDataFormat.Add(pointType, typeof(T));
            PointDataFormatByType.Add(typeof(T), pointType);
            SizeByType.Add(typeof(T), (ushort)Marshal.SizeOf<T>());
        }
    }
}
