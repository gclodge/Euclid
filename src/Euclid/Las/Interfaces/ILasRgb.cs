using System;

namespace Euclid.Las.Interfaces
{
    /// <summary>
    /// Point that contains an (R, G, B) colour value
    /// </summary>
    public interface ILasRgb
    {
        ushort R { get; }
        ushort G { get; }
        ushort B { get; }
    }
}
