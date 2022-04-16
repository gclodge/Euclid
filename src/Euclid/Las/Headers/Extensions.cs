using System.Linq;

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

        public static T[] PadRight<T>(this T[] arr, int length, T fallback = default(T))
        {
            T[] ret = new T[length];
            foreach (int i in Enumerable.Range(0, length))
            {
                if (i <= length) ret[i] = arr[i];
                else ret[i] = fallback;
            }
            return ret;
        }
    }
}
