using System;

using Euclid.Las.Headers.Structs;

namespace Euclid.Las.Headers
{
    public static class Extensions
    {
        public static uint[] ToArray(this LegacyPointRecordsByReturn rec)
        {
            return new uint[]
            {
                rec.Return0,
                rec.Return1,
                rec.Return2,
                rec.Return3,
                rec.Return4
            };
        }

        public static ulong[] ToArray(this PointRecordsByReturn rec)
        {
            return new ulong[]
            {
                rec.Return0,
                rec.Return1,
                rec.Return2,
                rec.Return3,
                rec.Return4,
                rec.Return5,
                rec.Return6,
                rec.Return7,
                rec.Return8,
                rec.Return9,
                rec.Return10,
                rec.Return11,
                rec.Return12,
                rec.Return13,
                rec.Return14,
            };
        }
    }
}
