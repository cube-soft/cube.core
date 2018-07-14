/* ------------------------------------------------------------------------- */
//
// Copyright (c) 2010 CubeSoft, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
/* ------------------------------------------------------------------------- */
using System;
using System.IO;

namespace Cube.FileSystem
{
    /* --------------------------------------------------------------------- */
    ///
    /// StreamProxy
    ///
    /// <summary>
    /// Provides a proxy of the original Stream instance.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class StreamProxy : Stream
    {
        /* ----------------------------------------------------------------- */
        ///
        /// StreamProxy
        ///
        /// <summary>
        /// Initializes a new instance of the StreamProxy class with the
        /// specified stream.
        /// </summary>
        ///
        /// <param name="stream">Original stream.</param>
        ///
        /* ----------------------------------------------------------------- */
        public StreamProxy(Stream stream) : this(stream, false) { }

        /* ----------------------------------------------------------------- */
        ///
        /// StreamProxy
        ///
        /// <summary>
        /// Initializes a new instance of the StreamProxy class with the
        /// specified parameters.
        /// </summary>
        ///
        /// <param name="stream">Original stream.</param>
        /// <param name="leaveOpen">
        /// true to leave the stream open after the StreamProxy object
        /// is disposed; otherwise, false.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        public StreamProxy(Stream stream, bool leaveOpen)
        {
            BaseStream = stream;
            _leave = leaveOpen;
        }

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// StreamProxy
        ///
        /// <summary>
        /// Gets the original stream.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Stream BaseStream { get; private set; }

        /* ----------------------------------------------------------------- */
        ///
        /// CanRead
        ///
        /// <summary>
        /// Gets a value indicating whether the current stream supports
        /// reading.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public override bool CanRead => BaseStream?.CanRead ?? false;

        /* ----------------------------------------------------------------- */
        ///
        /// CanSeek
        ///
        /// <summary>
        /// Gets a value indicating whether the current stream supports
        /// seeking.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public override bool CanSeek => BaseStream?.CanSeek ?? false;

        /* ----------------------------------------------------------------- */
        ///
        /// CanWrite
        ///
        /// <summary>
        /// Gets a value indicating whether the current stream supports
        /// writing.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public override bool CanWrite => BaseStream?.CanWrite ?? false;

        /* ----------------------------------------------------------------- */
        ///
        /// Length
        ///
        /// <summary>
        /// Gets the length of the stream in bytes.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public override long Length => Invoke(() => BaseStream.Length);

        /* ----------------------------------------------------------------- */
        ///
        /// Position
        ///
        /// <summary>
        /// Gets or sets the current position within the stream.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public override long Position
        {
            get => Invoke(() => BaseStream.Position);
            set => Invoke(() => BaseStream.Position = value);
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Flush
        ///
        /// <summary>
        /// Clears all buffers for this stream and causes any buffered
        /// data to be written to the underlying device.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public override void Flush() => Invoke(() => BaseStream.Flush());

        /* ----------------------------------------------------------------- */
        ///
        /// Read
        ///
        /// <summary>
        /// Reads a sequence of bytes from the current stream and advances
        /// the position within the stream by the number of bytes read.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public override int Read(byte[] buffer, int offset, int count) =>
            Invoke(() => BaseStream.Read(buffer, offset, count));

        /* ----------------------------------------------------------------- */
        ///
        /// Seek
        ///
        /// <summary>
        /// Sets the position within the current stream.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public override long Seek(long offset, SeekOrigin origin) =>
            Invoke(() => BaseStream.Seek(offset, origin));

        /* ----------------------------------------------------------------- */
        ///
        /// SetLength
        ///
        /// <summary>
        /// Sets the length of the current stream.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public override void SetLength(long value) =>
            Invoke(() => BaseStream.SetLength(value));

        /* ----------------------------------------------------------------- */
        ///
        /// Write
        ///
        /// <summary>
        /// writes a sequence of bytes to the current stream and advances
        /// the current position within this stream by the number of
        /// bytes written.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public override void Write(byte[] buffer, int offset, int count) =>
            Invoke(() => BaseStream.Write(buffer, offset, count));

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Releases the unmanaged resources used by the StreamProxy
        /// and optionally releases the managed resources.
        /// </summary>
        ///
        /// <param name="disposing">
        /// true to release both managed and unmanaged resources;
        /// false to release only unmanaged resources.
        /// </param>
        ///
        /* ----------------------------------------------------------------- */
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (_disposed) return;
                _disposed = true;

                if (disposing)
                {
                    if (!_leave) BaseStream.Dispose();
                    BaseStream = null;
                }
            }
            finally { base.Dispose(disposing); }
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the specified action object or throws an
        /// ObjectDisposedException.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private void Invoke(Action action)
        {
            if (_disposed) throw new ObjectDisposedException(GetType().Name);
            else action();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the specified function object or throws an
        /// ObjectDisposedException.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private T Invoke<T>(Func<T> func)
        {
            if (_disposed) throw new ObjectDisposedException(GetType().Name);
            return func();
        }

        #endregion

        #region Fields
        private bool _disposed = false;
        private readonly bool _leave;
        #endregion
    }
}
