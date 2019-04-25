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
using System.Runtime.Serialization;
using System.Threading;

namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// DispatchableBase
    ///
    /// <summary>
    /// Provides functionality to invoke methods with the provided
    /// SynchronizationContext.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [DataContract]
    [Serializable]
    public abstract class DispatchableBase
    {
        #region Constructors

        /* ----------------------------------------------------------------- */
        ///
        /// DispatchableBase
        ///
        /// <summary>
        /// Initializes a new instance of the DispatchableBase class
        /// with the specified context.
        /// </summary>
        ///
        /// <param name="context">Synchronization context.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected DispatchableBase(SynchronizationContext context)
        {
            Context = context;
        }

        #endregion

        #region Properties

        /* ----------------------------------------------------------------- */
        ///
        /// Context
        ///
        /// <summary>
        /// Gets or sets the synchronization context.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [IgnoreDataMember]
        public SynchronizationContext Context
        {
            get => _context;
            set => _context = value;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Synchronous
        ///
        /// <summary>
        /// Gets or sets the value indicating whether the event is fired
        /// as synchronously.
        /// </summary>
        ///
        /// <remarks>
        /// true の場合は Send メソッド、false の場合は Post メソッドを
        /// 用いてイベントを伝搬します。Context が null の場合、
        /// このプロパティは無視されます。
        /// </remarks>
        ///
        /* ----------------------------------------------------------------- */
        [IgnoreDataMember]
        public bool Synchronous
        {
            get => _synchronous;
            set => _synchronous = value;
        }

        #endregion

        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// Invoke
        ///
        /// <summary>
        /// Invokes the specified action with the Synchronization context.
        /// </summary>
        ///
        /// <param name="action">Invoked action.</param>
        ///
        /* ----------------------------------------------------------------- */
        protected void Invoke(Action action)
        {
            if (Context != null)
            {
                if (Synchronous) Context.Send(e => action(), null);
                else Context.Post(e => action(), null);
            }
            else action();
        }

        #endregion

        #region Fields
        [NonSerialized] private SynchronizationContext _context;
        [NonSerialized] private bool _synchronous = true;
        #endregion
    }
}
