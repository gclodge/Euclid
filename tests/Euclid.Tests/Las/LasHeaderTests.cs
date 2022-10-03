using Bogus;
using Xunit;
using Assert = Xunit.Assert;

using Euclid.Las;

namespace Euclid.Tests.Las
{
    public class LasHeaderTests
    {
        readonly Faker Faker = new();

        const double MinValue = -1337.0;
        const double MaxValue = 1337.0;

        const byte MinFormat = 0;
        const byte MaxFormat = 10;

        [Fact]
        public void CheckExtremaTest()
        {
            var Minima = new double[] { MinValue, MinValue, MinValue };
            var Maxima = new double[] { MaxValue, MaxValue, MaxValue };

            LasHeader Header = new();
            Header.CheckExtrema(Minima);
            Header.CheckExtrema(Maxima);

            Assert.Equal(Header.MinX, MinValue);
            Assert.Equal(Header.MinY, MinValue);
            Assert.Equal(Header.MinZ, MinValue);

            Assert.Equal(Header.MaxX, MaxValue);
            Assert.Equal(Header.MaxY, MaxValue);
            Assert.Equal(Header.MaxZ, MaxValue);
        }

        [Fact]
        public void SetPointCountTest()
        {
            ulong ExpectedPointCount = Faker.Random.ULong();
            uint ExpectedLegacyPointCount = (uint)ExpectedPointCount;

            LasHeader Header = new();
            Header.SetPointCount(ExpectedPointCount);

            Assert.Equal(ExpectedPointCount, Header.PointCount);
            Assert.Equal(ExpectedPointCount, Header.NumPointRecords);
            Assert.Equal(ExpectedLegacyPointCount, Header.LegacyNumPointRecords);
        }

        [Fact]
        public void OnlyLegacyPointCountSetTest()
        {
            uint ExpectedLegacyPointCount = Faker.Random.UInt();
            ulong ExpectedNumPointRecords = 0;

            LasHeader Header = new()
            {
                LegacyNumPointRecords = ExpectedLegacyPointCount
            };

            Assert.Equal(ExpectedLegacyPointCount, Header.PointCount);
            Assert.Equal(ExpectedLegacyPointCount, Header.LegacyNumPointRecords);
            Assert.Equal(ExpectedNumPointRecords, Header.NumPointRecords);
        }

        [Fact]
        public void SetPointDataFormatTest()
        {
            byte ExpectedFormat = Faker.Random.Byte(MinFormat, MaxFormat);
            ushort ExpectedLength = PointTypeMap.GetPointRecordLength(ExpectedFormat);

            LasHeader Header = new();
            Header.SetPointDataFormat(ExpectedFormat);

            Assert.Equal(ExpectedFormat, Header.PointDataFormat);
            Assert.Equal(ExpectedLength, Header.PointDataRecordLength);
        }

        [Fact]
        public void SetNumVLRs()
        {
            uint ExpectedNumber = Faker.Random.UInt();

            LasHeader Header = new();
            Header.SetNumVLRs(ExpectedNumber);

            Assert.Equal(ExpectedNumber, Header.NumberOfVLRs);
        }

        [Fact]
        public void SetOffsetToPointData()
        {
            uint ExpectedNumber = Faker.Random.UInt();

            LasHeader Header = new();
            Header.SetOffsetToPointData(ExpectedNumber);

            Assert.Equal(ExpectedNumber, Header.OffsetToPointData);
        }

        [Fact]
        public void UpdateScaleTest()
        {
            double ExpectedX = Faker.Random.Double();
            double ExpectedY = Faker.Random.Double();
            double ExpectedZ = Faker.Random.Double();

            LasHeader HeaderA = new();
            HeaderA.SetScale(ExpectedX, ExpectedY, ExpectedZ);

            Assert.Equal(ExpectedX, HeaderA.ScaleX);
            Assert.Equal(ExpectedY, HeaderA.ScaleY);
            Assert.Equal(ExpectedZ, HeaderA.ScaleZ);

            LasHeader HeaderB = new();
            HeaderB.SetScale(HeaderA);

            Assert.Equal(ExpectedX, HeaderB.ScaleX);
            Assert.Equal(ExpectedY, HeaderB.ScaleY);
            Assert.Equal(ExpectedZ, HeaderB.ScaleZ);
        }

        [Fact]
        public void UpdateOriginTest()
        {
            double ExpectedX = Faker.Random.Double();
            double ExpectedY = Faker.Random.Double();
            double ExpectedZ = Faker.Random.Double();

            LasHeader HeaderA = new();
            HeaderA.SetOrigin(ExpectedX, ExpectedY, ExpectedZ);

            Assert.Equal(ExpectedX, HeaderA.OriginX);
            Assert.Equal(ExpectedY, HeaderA.OriginY);
            Assert.Equal(ExpectedZ, HeaderA.OriginZ);

            LasHeader HeaderB = new();
            HeaderB.SetOrigin(HeaderA);

            Assert.Equal(ExpectedX, HeaderB.OriginX);
            Assert.Equal(ExpectedY, HeaderB.OriginY);
            Assert.Equal(ExpectedZ, HeaderB.OriginZ);
        }
    }
}
