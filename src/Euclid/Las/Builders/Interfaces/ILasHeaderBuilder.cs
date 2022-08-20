﻿using System;
using System.Collections.Generic;

using Euclid.Las.Headers.Interfaces;

namespace Euclid.Las.Builders.Interfaces
{
    public interface ILasHeaderBuilder : IFluentBuilder<ILasHeader>
    {
        ILasHeaderBuilder SetVersion(byte major, byte minor);
        ILasHeaderBuilder SetVersionMajor(byte versionMajor);
        ILasHeaderBuilder SetVersionMinor(byte versionMinor);

        ILasHeaderBuilder SetPointCount(ulong count);

        ILasHeaderBuilder SetScale(IEnumerable<double> scale);
        ILasHeaderBuilder SetScale(double scaleX, double scaleY, double scaleZ);
        ILasHeaderBuilder SetOrigin(IEnumerable<double> origin);
        ILasHeaderBuilder SetOrigin(double originX, double originY, double originZ);

        ILasHeaderBuilder SetMinima(IEnumerable<double> minima);
        ILasHeaderBuilder SetMinima(double minX, double minY, double minZ);
        ILasHeaderBuilder SetMaxima(IEnumerable<double> maxima);
        ILasHeaderBuilder SetMaxima(double maxX, double maxY, double maxZ);

        ILasHeaderBuilder SetPointDataRecordLength(ushort length);
        ILasHeaderBuilder SetPointDataFormat(byte format);
        ILasHeaderBuilder SetPointType(Type T);

        ILasHeaderBuilder SetHeaderSize(ushort size);
        ILasHeaderBuilder SetOffsetToPointData(uint offset);
        ILasHeaderBuilder SetNumberOfVLRs(uint numberOfVLRs);

        ILasHeaderBuilder SetCreationDate(ushort year, ushort doy);
        ILasHeaderBuilder SetCreationYear(ushort year);
        ILasHeaderBuilder SetCreationDayOfYear(ushort dayOfYear);
    }
}
