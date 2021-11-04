using System;

namespace Euclid.Points.Interfaces
{
    /// <summary>
    /// Point that contains a (R, G, B, NIR) colour value
    /// </summary>
    public interface ILas4Band : ILasRgb
    {
        ushort NIR { get; }
    }
}
