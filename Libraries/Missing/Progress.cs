/* ------------------------------------------------------------------------- */
///
/// Progress.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///  http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.
///
/* ------------------------------------------------------------------------- */
using System.Threading;

namespace System
{
    /* --------------------------------------------------------------------- */
    ///
    /// System.Progress(T)
    /// 
    /// <summary>
    /// 報告済みの進行状況の各値へのコールバックを呼び出す IProgress(T) を
    /// 提供します。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class Progress<T> : IProgress<T> where T : EventArgs
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Progress
        /// 
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Progress()
        {
            _context = SynchronizationContext.Current;
        }

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// ProgressChanged
        /// 
        /// <summary>
        /// 進行状況が更新された時に発生するイベントです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event EventHandler<T> ProgressChanged;

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Report
        /// 
        /// <summary>
        /// 進行状況の更新を報告します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Report(T value)
        {
            OnReport(value);
        }

        #endregion

        #region Virtual methods

        /* ----------------------------------------------------------------- */
        ///
        /// OnReport
        /// 
        /// <summary>
        /// 進行状況の更新を報告します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnReport(T value)
        {
            if (ProgressChanged == null) return;
            _context.Post(_ => ProgressChanged(this, value), null);
        }

        #endregion

        #region Fields
        private readonly SynchronizationContext _context;
        #endregion
    }
}
