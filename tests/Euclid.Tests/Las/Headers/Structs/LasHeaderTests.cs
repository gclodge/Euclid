using Bogus;
using Xunit;
using Assert = Xunit.Assert;

using Euclid.Las.Headers.Structs;

namespace Euclid.Tests.Las.Headers.Structs
{
    public class LasHeaderTests
    {
        Faker Faker = new();

        const double MinValue = -1337.0;
        const double MaxValue = 1337.0;

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
            //< Note that since this is a non-1.4 LasHeader - we can't have more than uint.MaxValue points
            ulong ExpectedPointCount = Faker.Random.ULong(0, uint.MaxValue);

            LasHeader Header = new();
            Header.SetPointCount(ExpectedPointCount);

            Assert.Equal(ExpectedPointCount, Header.PointCount);
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
            HeaderA.UpdateScale(ExpectedX, ExpectedY, ExpectedZ);

            Assert.Equal(ExpectedX, HeaderA.ScaleX);
            Assert.Equal(ExpectedY, HeaderA.ScaleY);
            Assert.Equal(ExpectedZ, HeaderA.ScaleZ);

            LasHeader HeaderB = new();
            HeaderB.UpdateScale(HeaderA);

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
            HeaderA.UpdateOrigin(ExpectedX, ExpectedY, ExpectedZ);

            Assert.Equal(ExpectedX, HeaderA.OriginX);
            Assert.Equal(ExpectedY, HeaderA.OriginY);
            Assert.Equal(ExpectedZ, HeaderA.OriginZ);

            LasHeader HeaderB = new();
            HeaderB.UpdateOrigin(HeaderA);

            Assert.Equal(ExpectedX, HeaderB.OriginX);
            Assert.Equal(ExpectedY, HeaderB.OriginY);
            Assert.Equal(ExpectedZ, HeaderB.OriginZ);
        }
    }
}
