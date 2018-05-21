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
using System.Windows.Forms;

namespace Cube.Forms
{
    /* --------------------------------------------------------------------- */
    ///
    /// TaskbarProgress
    ///
    /// <summary>
    /// タスクバー上に進捗状況を表示するためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class TaskbarProgress : ObservableProperty
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// TaskbarProgress
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="window">ウィンドウ・オブジェクト</param>
        ///
        /* ----------------------------------------------------------------- */
        public TaskbarProgress(IWin32Window window) : this(window.Handle) { }

        /* ----------------------------------------------------------------- */
        ///
        /// TaskbarProgress
        ///
        /// <summary>
        /// オブジェクトを初期化します。
        /// </summary>
        ///
        /// <param name="handle">ウィンドウ・ハンドル</param>
        ///
        /* ----------------------------------------------------------------- */
        public TaskbarProgress(IntPtr handle)
        {
            _handle = handle;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// State
        ///
        /// <summary>
        /// 表示状態を示す値を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public TaskbarProgressState State
        {
            get => _state;
            set
            {
                if (SetProperty(ref _state, value)) Refresh();
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Value
        ///
        /// <summary>
        /// 進捗状況を示す値を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int Value
        {
            get => _value;
            set
            {
                if (SetProperty(ref _value, value)) Refresh();
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Maximum
        ///
        /// <summary>
        /// 進捗状況の最大値を取得または設定します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public int Maximum
        {
            get => _maximum;
            set
            {
                if (SetProperty(ref _maximum, value)) Refresh();
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// IsSupported
        ///
        /// <summary>
        /// 実行環境でサポートされているかどうかを示す値を取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool IsSupported { get; } = Environment.OSVersion.Version >= new Version(6, 1);

        /* ----------------------------------------------------------------- */
        ///
        /// Core
        ///
        /// <summary>
        /// コアオブジェクトを取得します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private ITaskbarList3 Core => _core ?? (
            _core = (ITaskbarList3)(new TaskbarListInstance())
        );

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Refresh
        ///
        /// <summary>
        /// 進捗状況を更新します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public void Refresh()
        {
            if (!IsSupported) return;

            var cvt = Math.Min(Value, Maximum);
            Core.SetProgressValue(_handle, (ulong)cvt, (ulong)Maximum);
            Core.SetProgressState(_handle, State);
        }

        #endregion

        #region Fields
        private readonly IntPtr _handle;
        private ITaskbarList3 _core;
        private TaskbarProgressState _state = TaskbarProgressState.None;
        private int _value = 0;
        private int _maximum = 100;
        #endregion
    }

    /* --------------------------------------------------------------------- */
    ///
    /// TaskbarProgressState
    ///
    /// <summary>
    /// 進捗状況の表示状態を示す列挙型です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum TaskbarProgressState
    {
        /// <summary>進捗表示なし</summary>
        None = 0,
        /// <summary>進捗割合は不明</summary>
        Indeterminate = 0x1,
        /// <summary>正常</summary>
        Normal = 0x2,
        /// <summary>エラー</summary>
        Error = 0x4,
        /// <summary>中断</summary>
        Paused = 0x8
    }
}
