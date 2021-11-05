using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Euclid.Time;
using Euclid.Points.Interfaces;
using Euclid.Headers.Interfaces;

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
        public Vector<double> Position { get; set; }
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
        public byte? WavePacketDescriptorIndex { get; set; } = null;
        public uint? WaveformPacketSizeBytes { get; set; } = null;

        public float? ReturnPointWaveformLocation { get; set; } = null;
        public float? X_t { get; set; } = null;
        public float? Y_t { get; set; } = null;
        public float? Z_t { get; set; } = null;

        public ulong? ByteOffsetToWaveformData { get; set; } = null;
        #endregion

        public LasPoint()
        {
            Position = CreateVector.Dense(new double[] { double.NaN, double.NaN, double.NaN });
        }

        public void Update(ILasRgb p)
        {
            this.R = p.R;
            this.G = p.G;
            this.B = p.B;

            if (p is ILas4Band)
            {
                this.NIR = ((ILas4Band)p).NIR;
            }
        }

        public void Update(ILasWaveform p)
        {
            this.WaveformPacketSizeBytes = p.WaveformPacketSizeBytes;
            this.ByteOffsetToWaveformData = p.ByteOffsetToWaveformData;
            this.WavePacketDescriptorIndex = p.WavePacketDescriptorIndex;

            this.ReturnPointWaveformLocation = p.ReturnPointWaveformLocation;
            this.X_t = p.X_t;
            this.Y_t = p.Y_t;
            this.Z_t = p.Z_t;
        }

        public void Update(ILasPointStruct p, ILasHeader header)
        {
            this.Position[0] = p.X * header.ScaleX + header.OriginX;
            this.Position[1] = p.Y * header.ScaleY + header.OriginY;
            this.Position[2] = p.Z * header.ScaleZ + header.OriginZ;

            this.ScanAngle = p.ScanAngle;
            this.Intensity = p.Intensity;
            this.FlightLine = p.FlightLine;
            this.GlobalEncoding = p.GlobalEncoding;
            this.Classification = p.Classification;
        }
    }
}
