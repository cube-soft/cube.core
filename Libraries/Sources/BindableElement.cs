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
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace Cube.Xui
{
    /* --------------------------------------------------------------------- */
    ///
    /// BindableElement
    ///
    /// <summary>
    /// Represents a bindable element that has text and command.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public class BindableElement : ObservableBase, IElement, IObservePropertyChanged
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// BindableElement
        ///
        /// <summary>
        /// Initializes a new instance of the <c>BindableElement</c>
        /// class with the specified arguments.
        /// </summary>
        ///
        /// <param name="gettext">Function to get text.</param>
        /// <param name="dispatcher">Dispatcher object.</param>
        ///
        /* ----------------------------------------------------------------- */
        public BindableElement(Getter<string> gettext, IDispatcher dispatcher) : base(dispatcher)
        {
            _getter = gettext;
            _observer.Add(Locale.Subscribe(e => React()));
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Text
        ///
        /// <summary>
        /// Gets a text to be displayed in the View.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public string Text => _getter();

        /* ----------------------------------------------------------------- */
        ///
        /// Command
        ///
        /// <summary>
        /// Gets or sets a command to be executed by the View.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        public ICommand Command
        {
            get => _command;
            set => SetProperty(ref _command, value);
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Observe
        ///
        /// <summary>
        /// Observes the PropertyChanged event of the specified object.
        /// </summary>
        ///
        /// <param name="src">Object to be observed.</param>
        /// <param name="names">Target property names.</param>
        ///
        /* ----------------------------------------------------------------- */
        public void Observe(INotifyPropertyChanged src, params string[] names)
        {
            var set = new HashSet<string>(names);
            void handler(object s, PropertyChangedEventArgs e)
            {
                if (set.Count <= 0 || set.Contains(e.PropertyName)) React();
            }

            src.PropertyChanged += handler;
            _observer.Add(Disposable.Create(() => src.PropertyChanged -= handler));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// React
        ///
        /// <summary>
        /// Invokes when any states are changed.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected virtual void React() => Refresh(nameof(Text));

        /* ----------------------------------------------------------------- */
        ///
        /// Dispose
        ///
        /// <summary>
        /// Releases the unmanaged resources used by <c>BindableElement</c>
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
            if (!disposing) return;
            foreach (var obj in _observer) obj.Dispose();
            _observer.Clear();
        }

        /* ----------------------------------------------------------------- */
        ///
        /// OnPropertyChanged
        ///
        /// <summary>
        /// Occurs when the PropertyChanged event is fired.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (!Disposed) base.OnPropertyChanged(e);
        }

        #endregion

        #region Fields
        private readonly Getter<string> _getter;
        private readonly IList<IDisposable> _observer = new List<IDisposable>();
        private ICommand _command;
        #endregion
    }
}
