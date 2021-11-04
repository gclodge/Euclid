using System;

namespace Euclid.Points.Interfaces
{
    /// <summary>
    /// Represents an in-memory LAS point that exposes the actual position. a timestamp, and all other available fields
    /// </summary>
    public interface ILasPoint : IPosition, ILasPointBase, ILasTime
    {
    }
}
