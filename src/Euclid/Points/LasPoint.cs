using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Euclid.Time;
using Euclid.Points.Interfaces;

using MathNet.Numerics.LinearAlgebra;

namespace Euclid.Points
{
    public class LasPoint : ILasPoint
    {
        public byte UserData { get; set; }
        public byte Classification { get; set; }

        public ushort Intensity { get; set; }
        public ushort FlightLine { get; set; }
        public ushort GlobalEncoding { get; set; }

        public short ScanAngle { get; set; }

        #region Position
        public double X => Position[0];
        public double Y => Position[1];
        public double Z => Position[2];
        public Vector<double> Position { get; set; } = CreateVector.Dense(new double[] { double.NaN, double.NaN, double.NaN });
        #endregion

        #region Time
        public double Timestamp { get; set; }
        public DateTime DateTime => GpsTime.Parse(Timestamp);
        #endregion

        #region Colour
        public ushort? R { get; set; } = null;
        public ushort? G { get; set; } = null;
        public ushort? B { get; set; } = null;
        public ushort? NIR { get; set; } = null;
        #endregion

        #region Waveform Data
        public uint? WaveformPacketSizeBytes { get; set; } = null;

        public float? ReturnPointWaveformLocation { get; set; } = null;
        public float? X_t { get; set; } = null;
        public float? Y_t { get; set; } = null;
        public float? Z_t { get; set; } = null;

        public ulong? ByteOffsetToWaveformData { get; set; } = null;
        #endregion

    }
}
