using MathNet.Numerics.LinearAlgebra;

namespace Euclid.Las.Interfaces
{
    /// <summary>
    /// Represents any geometry that contains a single 3D position as a vector (and exposes each dimensional coordinate)
    /// </summary>
    public interface IPosition
    {
        Vector<double> Position { get; }
        double X { get; }
        double Y { get; }
        double Z { get; }
    }
}
