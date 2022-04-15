using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Euclid.Las.Points;
using Euclid.Las.Headers;
using Euclid.Las.Headers.Structs;
using Euclid.Las.Headers.Interfaces;
using Euclid.Las.Reader.Stream.Interfaces;

namespace Euclid.Las.Reader.Stream
{
    internal class AsyncStreamHandler : IStreamHandler, IDisposable
    {
        #region Public Fields
        public ulong ReadTicks { get; private set; } = 0;

        public ulong PointsRead { get; private set; } = 0;
        public ulong PointsYielded { get; private set; } = 0;
        public ulong PointsReturned { get; private set; } = 0;

        public long BytesRemaining => _StreamReader.BaseStream.Length - _StreamReader.BaseStream.Position;

        public uint BufferCount { get; private set; }
        public uint BufferSize { get; private set; }

        public Type PointType { get; private set; }

        public bool EOF => PointsReturned >= Header.PointCount;

        public ILasHeader Header { get; private set; } = null;
        public IList<LasVariableLengthRecord> VLRs { get; private set; } = null;
        #endregion

        #region Internal Fields / Streams
        private readonly StreamReader _StreamReader;
        private readonly BinaryReader _BinaryReader;

        private Thread _ReaderThread;
        private readonly AutoResetEvent _NeedDataEvent = new AutoResetEvent(false);
        private readonly AutoResetEvent _DataReadyEvent = new AutoResetEvent(false);

        private IStreamBuffer _A;
        private IStreamBuffer _B;
        private IStreamBuffer _Active;

        private byte[] _Buffer;

        private bool _Disposing;
        private bool _Disposed;
        #endregion

        public AsyncStreamHandler(string filePath, uint pointBufferCount = Constants.DefaultReaderBufferCount)
        {
            _StreamReader = new StreamReader(filePath);
            _BinaryReader = new BinaryReader(_StreamReader.BaseStream);

            BufferCount = pointBufferCount;
        }

        #region Fluent Interface
        public IStreamHandler Initialize()
        {
            //< Parse the incoming ILasHeader and get the buffer size
            ReadLasHeader();
            
            BufferSize = BufferCount * Header.PointDataRecordLength;
            PointType = PointTypeMap.GetPointType(Header);
            _Buffer = new byte[BufferSize];

            //< Parse any available LasVariableLengthRecords
            ReadVariableLengthRecords();

            //< Ensure we've 'seeked' the underlying stream to the start of point data
            Seek(Header.OffsetToPointData);

            //< Init (and start filling) the AsyncStreamHandler
            InitializeAsyncStreamHandler();

            return this;
        }
        #endregion

        #region Public Methods
        public void Seek(long pos)
        {
            if (pos < 0) throw new ArgumentOutOfRangeException(nameof(pos));
            if (pos > _BinaryReader.BaseStream.Length) throw new ArgumentOutOfRangeException(nameof(pos));
            
            _BinaryReader.BaseStream.Seek(pos, SeekOrigin.Begin);
        }

        public T ReadOfType<T>()
        {
            //< Get the number of bytes (by Type) and read the initial buffer into memory
            int count = Marshal.SizeOf(typeof(T));
            byte[] buff = _BinaryReader.ReadBytes(count);

            //< Generate the handle, marshal it, then free it
            GCHandle handle = GCHandle.Alloc(buff, GCHandleType.Pinned);
            T result = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();

            //< Get the frig outta here with our object, m'lord(s)
            return result;
        }

        public LasPoint GetNextPoint()
        {
            CheckBufferStatus();
            return _Active.GetNext(Header);
        }

        public void GetNextPoint(ref LasPoint lpt)
        {
            CheckBufferStatus();
            _Active.GetNext(Header, ref lpt);
        }
        #endregion

        #region Internal Methods
        void ReadLasHeader()
        {
            Header = ReadOfType<LasHeader>();

            if (Header.VersionMajor == 4 && Header.VersionMinor == 1)
            {
                Seek(0);
                Header = ReadOfType<LasHeader14>();
            }
        }

        void ReadVariableLengthRecords()
        {
            VLRs = Enumerable.Range(0, (int)Header.NumberOfVLRs)
                             .Select(_ => LasVariableLengthRecord.ReadFromStream(_BinaryReader))
                             .ToList();
        }

        void InitializeAsyncStreamHandler()
        {
            _ReaderThread = new Thread(new ThreadStart(ReadPointsAsync));
            _ReaderThread.Start();

            _A = new AsyncStreamBuffer(BufferCount, PointType);
            _B = new AsyncStreamBuffer(BufferCount, PointType);
            SetActive(_A);
        }

        void CheckBufferStatus()
        {
            if (_Active.Available == 0)
            {
                _NeedDataEvent.Set();
                _DataReadyEvent.WaitOne();
            }
            PointsReturned++;
        }

        void SetActive(IStreamBuffer buff)
        {
            _Active = buff;
        }

        void ReadPointsAsync()
        {
            while (PointsYielded < Header.PointCount && !_Disposing)
            {
                long startTicks = DateTime.Now.Ticks;
                _NeedDataEvent.WaitOne();

                if (_A.Available > 0)
                {
                    SetActive(_A);
                    PointsYielded += (ulong)_Active.Loaded;
                    _DataReadyEvent.Set();
                }
                else if (_B.Available > 0)
                {
                    SetActive(_B);
                    PointsYielded += (ulong)_Active.Loaded;
                    _DataReadyEvent.Set();
                }
                else //< Neither AsyncStreamBuffer has points ready
                {
                    MarshalPoints(_A);
                    SetActive(_A);
                    PointsYielded += (ulong)_Active.Loaded;

                    _DataReadyEvent.Set();
                    MarshalPoints(_B);
                }
                
                if (_A.Available == 0 && _Active != _A)
                {
                    MarshalPoints(_A);
                }
                if (_B.Available == 0 && _Active != _B)
                {
                    MarshalPoints(_B);
                }

                ReadTicks += (ulong)(DateTime.Now.Ticks - startTicks);
            }
        }

        void MarshalPoints(IStreamBuffer buff)
        {
            if (BytesRemaining > 0)
            {
                long remainingRecs = (long)(Header.PointCount - PointsRead);
                long toRead = Math.Min(Math.Min(BytesRemaining, BufferSize), remainingRecs * (long)Header.PointDataRecordLength);

                _BinaryReader.Read(_Buffer, 0, (int)toRead);
                ulong numRead = (ulong)(toRead / Header.PointDataRecordLength);
                var ph = GCHandle.Alloc(buff.Data, GCHandleType.Pinned);
                Marshal.Copy(_Buffer, 0, ph.AddrOfPinnedObject(), (int)toRead);
                ph.Free();

                buff.SetLoaded((int)numRead);
                buff.SetConsumed(0);
                PointsRead += numRead;
            }
        }
        #endregion

        #region IDisposable
        protected virtual void Dispose(bool disposing)
        {
            _Disposing = disposing;
            if (!_Disposed)
            {
                if (_Disposing)
                {
                    _NeedDataEvent.Set();
                    _ReaderThread.Join();
                    if (_StreamReader != null)
                    {
                        _StreamReader.Dispose();
                    }
                }

                _Disposed = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
