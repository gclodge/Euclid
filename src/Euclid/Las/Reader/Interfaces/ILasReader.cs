﻿using System;

using Euclid.Las.Points;
using Euclid.Las.Headers.Interfaces;

namespace Euclid.Las.Reader.Interfaces
{
    public interface ILasReader : IDisposable
    {
        /// <summary>
        /// Boolean flag indicating if the ILasReader has reached the end of the underlying LAS file
        /// </summary>
        bool EOF { get; }
        /// <summary>
        /// The total number of LAS point records stored within the underlying LAS file
        /// </summary>
        ulong PointCount { get; }
        /// <summary>
        /// The ILasHeader as recorded within the underlying LAS file
        /// </summary>
        ILasHeader Header { get; }

        /// <summary>
        /// Create a new LasPoint instance of the 'next' point to be scanned in the LAS file
        /// </summary>
        /// <returns>LasPoint as parsed from underlying LAS file</returns>
        LasPoint GetNextPoint();
        /// <summary>
        /// Update input LasPoint with the 'next' point to be scanned in the LAS file
        /// </summary>
        /// <param name="lpt">Existing LasPoint object to be updated</param>
        void GetNextPoint(ref LasPoint lpt);
    }
}
