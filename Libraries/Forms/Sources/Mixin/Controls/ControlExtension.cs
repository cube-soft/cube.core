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
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Cube.Forms;

namespace Cube.Mixin.Forms.Controls
{
    /* --------------------------------------------------------------------- */
    ///
    /// ControlExtension
    ///
    /// <summary>
    /// Provides extended methods of the Control class.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public static class ControlExtension
    {
        #region Methods

        /* ----------------------------------------------------------------- */
        ///
        /// HitTest
        ///
        /// <summary>
        /// Invokes a hit-test to confirm where the specified point is in
        /// the control.
        /// </summary>
        ///
        /// <param name="src">Source control.</param>
        /// <param name="point">Point based on the source control.</param>
        /// <param name="grip">Grip size.</param>
        ///
        /// <returns>Position enum. value</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static Position HitTest(this Control src, Point point, int grip)
        {
            var x = point.X;
            var y = point.Y;
            var w = src.ClientSize.Width;
            var h = src.ClientSize.Height;

            var client = (x > grip && x < w - grip && y > grip && y < h - grip);
            var left   = (x >= 0 && x <= grip);
            var top    = (y >= 0 && y <= grip);
            var right  = (x <= w && x >= w - grip);
            var bottom = (y <= h && y >= h - grip);

            return client          ? Position.Client      :
                   top && left     ? Position.TopLeft     :
                   top && right    ? Position.TopRight    :
                   bottom && left  ? Position.BottomLeft  :
                   bottom && right ? Position.BottomRight :
                   top             ? Position.Top         :
                   bottom          ? Position.Bottom      :
                   left            ? Position.Left        :
                   right           ? Position.Right       :
                                     Position.NoWhere     ;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// HasEventHandler
        ///
        /// <summary>
        /// Determines whether any event handlers have been set for the
        /// specified event.
        /// </summary>
        ///
        /// <param name="src">Source control.</param>
        /// <param name="name">Event name to check.</param>
        ///
        /// <returns>true for any event handler has been set.</returns>
        ///
        /* ----------------------------------------------------------------- */
        public static bool HasEventHandler(this Control src, string name)
        {
            var key = GetEventKey(src, name);
            var map = GetEventHandlers(src);
            return key != null && map?[key] != null;
        }

        #endregion

        #region Implementations

        /* ----------------------------------------------------------------- */
        ///
        /// GetEventHandlers
        ///
        /// <summary>
        /// Get a list of event handlers set for the specified object.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static EventHandlerList GetEventHandlers(object obj)
        {
            static MethodInfo method(Type t)
            {
                var mi = t.GetMethod("get_Events", GetAllFlags());
                if (mi == null && t.BaseType != null) mi = method(t.BaseType);
                return mi;
            }
            return method(obj.GetType())?.Invoke(obj, new object[0]) as EventHandlerList;
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetEventKey
        ///
        /// <summary>
        /// Get the object corresponding to the specified event name.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static object GetEventKey(object obj, string name)
        {
            static FieldInfo method(Type t, string n)
            {
                var fi = t.GetField($"Event{n}", GetAllFlags());
                if (fi == null && t.BaseType != null) fi = method(t.BaseType, n);
                return fi;
            }
            return method(obj.GetType(), name)?.GetValue(obj);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// GetAllFlags
        ///
        /// <summary>
        /// Get the BindingFlags object with all values enabled.
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private static BindingFlags GetAllFlags() =>
            BindingFlags.Public     |
            BindingFlags.NonPublic  |
            BindingFlags.Instance   |
            BindingFlags.IgnoreCase |
            BindingFlags.Static     ;

        #endregion
    }
}
