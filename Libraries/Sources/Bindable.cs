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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Cube.Xui
{
    /* --------------------------------------------------------------------- */
    ///
    /// Bindable(T)
    ///
    /// <summary>
    /// Provides functionality to make the value as bindable.
    /// </summary>
    ///
    /// <remarks>
    /// Value プロパティを通じて実際の値にアクセスします。
    /// PropertyChanged イベントは、コンストラクタで指定された同期
    /// コンテキストを用いて発生します。
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public class Bindable<T> : INotifyPropertyChanged
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// Bindable
        ///
        /// <summary>
        /// Initializes a new instance of the <c>Bindable</c> class.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public Bindable() : this(default(T)) { }

        /* ----------------------------------------------------------------- */
        ///
        /// Bindable
        ///
        /// <summary>
        /// Initializes a new instance of the <c>Bindable</c> class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="value">Initial value.</param>
        ///
        /* ----------------------------------------------------------------- */
        public Bindable(T value) : this(value, SynchronizationContext.Current) { }

        /* ----------------------------------------------------------------- */
        ///
        /// Bindable
        ///
        /// <summary>
        /// Initializes a new instance of the <c>Bindable</c> class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="context">Synchronization context.</param>
        ///
        /* ----------------------------------------------------------------- */
        public Bindable(SynchronizationContext context) : this(default(T), context) { }

        /* ----------------------------------------------------------------- */
        ///
        /// Bindable
        ///
        /// <summary>
        /// Initializes a new instance of the <c>Bindable</c> class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="value">Initial value.</param>
        /// <param name="context">Synchronization context.</param>
        ///
        /* ----------------------------------------------------------------- */
        public Bindable(T value, SynchronizationContext context)
        {
            var field = value;

            _context = context;
            _getter = () => field;
            _setter = e =>
            {
                if (!Equals(field, default(T)) && field.Equals(e)) return false;
                field = e;
                return true;
            };
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Bindable
        ///
        /// <summary>
        /// Initializes a new instance of the <c>Bindable</c> class
        /// with the specified arguments.
        /// </summary>
        ///
        /// <param name="getter">Delegation to get the value.</param>
        /// <param name="setter">Delegation to set the value.</param>
        /// <param name="context">Synchronization context.</param>
        ///
        /* ----------------------------------------------------------------- */
        public Bindable(Func<T> getter, Func<T, bool> setter, SynchronizationContext context)
        {
            _getter  = getter;
            _setter  = setter;
            _context = context;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Value
        ///
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public T Value
        {
            get => _getter();
            set
            {
                if (_setter(value))
                {
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(HasValue));
                }
            }
        }

        /* ----------------------------------------------------------------- */
        ///
        /// HasValue
        ///
        /// <summary>
        /// Gets the value indicating whether the value is valid.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public bool HasValue => !Equals(Value, default(T));

        /* ----------------------------------------------------------------- */
        ///
        /// IsSynchronous
        ///
        /// <summary>
        /// Gets or sets the value indicating whether the event is fired
        /// as synchronously.
        /// </summary>
        ///
        /// <remarks>
        /// true の場合は Send メソッド、false の場合は Post メソッドを
        /// 用いてイベントを伝搬します。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        public bool IsSynchronous { get; set; } = true;

        #endregion

        #region Events

        /* ----------------------------------------------------------------- */
        ///
        /// PropertyChanged
        ///
        /// <summary>
        /// Occurs when a property of the class is changed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public event PropertyChangedEventHandler PropertyChanged;

        /* ----------------------------------------------------------------- */
        ///
        /// OnPropertyChanged
        ///
        /// <summary>
        /// Raises the <c>PropertyChanged</c> event with the provided
        /// arguments.
        /// </summary>
        ///
        /// <param name="e">Arguments of the event being raised.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) =>
            PropertyChanged?.Invoke(this, e);

        /* ----------------------------------------------------------------- */
        ///
        /// OnPropertyChanged
        ///
        /// <summary>
        /// Raises the <c>PropertyChanged</c> event with the specified
        /// name.
        /// </summary>
        ///
        /// <param name="name">Name of property.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected void RaisePropertyChanged([CallerMemberName] string name = null)
        {
            var e = new PropertyChangedEventArgs(name);
            if (_context != null)
            {
                if (IsSynchronous) _context.Send(_ => OnPropertyChanged(e), null);
                else _context.Post(_ => OnPropertyChanged(e), null);
            }
            else OnPropertyChanged(e);
        }

        #endregion

        #region Fields
        private readonly Func<T> _getter;
        private readonly Func<T, bool> _setter;
        private readonly SynchronizationContext _context;
        #endregion
    }
}
