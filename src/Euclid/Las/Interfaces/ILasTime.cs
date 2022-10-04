using System;

namespace Euclid.Las.Interfaces
{
    /// <summary>
    /// Represents a LAS point with an associated decimal timestamp (hopefully GPS Standard, you animals)
    /// </summary>
    public interface ILasTime
    {
        double Timestamp { get; }
    }
}
