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

        public static LasHeader CreateForWriting(Type pointType)
        {
            LasHeader res = new LasHeader();
            res._VersionMajor = 1;
            res._VersionMinor = 2;
            res._Signature = Constants.LasHeaderSignature;
            res._HeaderSize = Constants.LasHeaderSize;
            res._OffsetToPointData = Constants.LasHeaderSize;
            res._MaxX = res._MaxY = res._MaxZ = double.MinValue;
            res._MinX = res._MinY = res._MinZ = double.MaxValue;
            res._ScaleX = res._ScaleY = res._ScaleZ = Constants.DefaultScale;
            res._OriginX = res._OriginY = res._OriginZ = Constants.DefaultOffset;
            res._LegacyNumberPointRecords = 0;
            res._LegacyNumberPointRecordsByReturn = new LegacyPointRecordsByReturn();
            res._NumberOfVLRs = 0;
            res._PointDataFormat = Points.PointTypeMap.PointDataFormatByType[pointType];
            res._PointDataRecordLength = Points.PointTypeMap.SizeByType[pointType];
            return res;
        }

        public static LasHeader CreateForWriting(byte pointRecordFormat)
        {
            var type = Points.PointTypeMap.TypeByPointDataFormat[pointRecordFormat];
            return CreateForWriting(type);
        }
    }
}
