using System;
using System.Linq;
using System.Collections.Generic;

using Euclid.Las.Interfaces;
using Euclid.Las.Builders.Interfaces;

namespace Euclid.Las.Builders
{
    public class LasHeaderBuilder : ILasHeaderBuilder
    {
        private LasHeader header = new();

        #region ILasHeaderBuilder Methods
        public ILasHeaderBuilder SetVersion(byte major, byte minor)
        {
            header.VersionMajor = major;
            header.VersionMinor = minor;
            return this;
        }
        public ILasHeaderBuilder SetVersionMajor(byte versionMajor)
        {
            header.VersionMajor = versionMajor;
            return this;
        }
        public ILasHeaderBuilder SetVersionMinor(byte versionMinor)
        {
            header.VersionMinor = versionMinor;
            return this;
        }

        public ILasHeaderBuilder SetMinima(IEnumerable<double> minima)
        {
            if (minima.Count() < 3) throw new ArgumentException($"Minima must be at least 3D");

            return SetMinima(minima.ElementAt(0), minima.ElementAt(1), minima.ElementAt(2));
        }
        public ILasHeaderBuilder SetMinima(double minX, double minY, double minZ)
        {
            header.MinX = minX;
            header.MinY = minY;
            header.MinZ = minZ;
            return this;
        }
        public ILasHeaderBuilder SetMaxima(IEnumerable<double> maxima)
        {
            if (maxima.Count() < 3) throw new ArgumentException($"Maxima must be at least 3D");

            return SetMaxima(maxima.ElementAt(0), maxima.ElementAt(1), maxima.ElementAt(2));
        }
        public ILasHeaderBuilder SetMaxima(double maxX, double maxY, double maxZ)
        {
            header.MaxX = maxX;
            header.MaxY = maxY;
            header.MaxZ = maxZ;
            return this;
        }
        public ILasHeaderBuilder SetScale(IEnumerable<double> scale)
        {
            if (scale.Count() < 3) throw new ArgumentException($"Scale must be at least 3D");

            return SetScale(scale.ElementAt(0), scale.ElementAt(1), scale.ElementAt(2));
        }
        public ILasHeaderBuilder SetScale(double scaleX, double scaleY, double scaleZ)
        {
            header.ScaleX = scaleX;
            header.ScaleY = scaleY;
            header.ScaleZ = scaleZ;
            return this;
        }
        public ILasHeaderBuilder SetOrigin(IEnumerable<double> origin)
        {
            if (origin.Count() < 3) throw new ArgumentException($"Scale must be at least 3D");

            return SetOrigin(origin.ElementAt(0), origin.ElementAt(1), origin.ElementAt(2));
        }
        public ILasHeaderBuilder SetOrigin(double originX, double originY, double originZ)
        {
            header.OriginX = originX;
            header.OriginY = originY;
            header.OriginZ = originZ;
            return this;
        }

        public ILasHeaderBuilder SetHeaderSize(ushort size)
        {
            header.HeaderSize = size;
            return this;
        }
        public ILasHeaderBuilder SetNumberOfVLRs(uint numberOfVLRs)
        {
            header.NumberOfVLRs = numberOfVLRs;
            return this;
        }
        public ILasHeaderBuilder SetOffsetToPointData(uint offset)
        {
            header.OffsetToPointData = offset;
            return this;
        }

        public ILasHeaderBuilder SetPointCount(ulong count)
        {
            header.SetPointCount(count);
            return this;
        }
        public ILasHeaderBuilder SetPointDataFormat(byte format)
        {
            header.SetPointDataFormat(format);
            return this;
        }

        public ILasHeaderBuilder SetCreationDate(ushort year, ushort doy)
        {
            header.CreationYear = year;
            header.CreationDOY = doy;
            return this;
        }
        public ILasHeaderBuilder SetCreationYear(ushort year)
        {
            header.CreationYear = year;
            return this;
        }
        public ILasHeaderBuilder SetCreationDayOfYear(ushort dayOfYear)
        {
            header.CreationDOY = dayOfYear;
            return this;
        }
        #endregion

        #region IFluentBuilder<T> Methods
        public ILasHeader Build(bool reset = true)
        {
            LasHeader result = header;

            if (reset) Reset();

            return result;
        }

        public void Reset()
        {
            header = new();
        }
        #endregion
    }
}
