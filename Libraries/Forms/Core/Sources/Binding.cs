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
namespace Cube.Forms.Binding;

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

/* ------------------------------------------------------------------------- */
///
/// Methods
///
/// <summary>
/// Provides extended methods about Binding functions.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public static class Methods
{
    #region Bind

    /* --------------------------------------------------------------------- */
    ///
    /// Bind
    ///
    /// <summary>
    /// Invokes the binding settings with the specified arguments.
    /// </summary>
    ///
    /// <param name="src">Binding source.</param>
    /// <param name="name">Property name of the binding source.</param>
    /// <param name="view">View object to bind.</param>
    /// <param name="viewName">Property name of the view to bind.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void Bind(this BindingSource src, string name, Control view, string viewName) =>
        Bind(src, name, view, viewName, false);

    /* --------------------------------------------------------------------- */
    ///
    /// Bind
    ///
    /// <summary>
    /// Invokes the binding settings with the specified arguments.
    /// </summary>
    ///
    /// <param name="src">Binding source.</param>
    /// <param name="name">Property name of the binding source.</param>
    /// <param name="view">View object to bind.</param>
    /// <param name="viewName">Property name of the view to bind.</param>
    /// <param name="oneway">
    /// Value indicating whether to update view only.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    public static void Bind(this BindingSource src, string name, Control view, string viewName, bool oneway) => Bind(
        src, name, view, viewName,
        oneway ? DataSourceUpdateMode.Never : DataSourceUpdateMode.OnPropertyChanged
    );

    /* --------------------------------------------------------------------- */
    ///
    /// Bind
    ///
    /// <summary>
    /// Invokes the binding settings with the specified arguments.
    /// </summary>
    ///
    /// <param name="src">Binding source.</param>
    /// <param name="name">Property name of the binding source.</param>
    /// <param name="view">View object to bind.</param>
    /// <param name="viewName">Property name of the view to bind.</param>
    /// <param name="viewMode">Mode to update source.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void Bind(this BindingSource src,
        string name,
        Control view,
        string viewName,
        DataSourceUpdateMode viewMode
    ) => view.DataBindings.Add(new(viewName, src, name, false, viewMode));

    /* --------------------------------------------------------------------- */
    ///
    /// Bind
    ///
    /// <summary>
    /// Binds the specified ComboBox object with the specified data.
    /// </summary>
    ///
    /// <param name="src">ComboBox object.</param>
    /// <param name="data">Collection of binding data.</param>
    ///
    /* --------------------------------------------------------------------- */
    public static void Bind<T>(this ComboBox src, ComboListSource<T> data)
    {
        var obj = src.SelectedValue;
        var cmp = EqualityComparer<T>.Default;

        src.DataSource    = data;
        src.DisplayMember = "Key";
        src.ValueMember   = "Value";

        if (obj is T v && data.Any(e => cmp.Equals(e.Value, v))) src.SelectedValue = v;
        else if (data.Any()) src.SelectedValue = data.First().Value;
    }

    #endregion

    #region ToBindingSource

    /* --------------------------------------------------------------------- */
    ///
    /// ToBindingSource
    ///
    /// <summary>
    /// Converts the specified INotifyCollectionChanged implementation
    /// object to a BindingSource object.
    /// </summary>
    ///
    /// <param name="src">
    /// INotifyCollectionChanged implementation object.
    /// </param>
    ///
    /// <returns>BindingSource object.</returns>
    ///
    /// <remarks>
    /// Invokes the ResetBindings(false) method when CollectionChanged
    /// event is fired.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public static BindingSource ToBindingSource(this INotifyCollectionChanged src) =>
        ToBindingSource(src, SynchronizationContext.Current);

    /* --------------------------------------------------------------------- */
    ///
    /// ToBindingSource
    ///
    /// <summary>
    /// Converts the specified INotifyCollectionChanged implementation
    /// object to a BindingSource object.
    /// </summary>
    ///
    /// <param name="src">
    /// INotifyCollectionChanged implementation object.
    /// </param>
    ///
    /// <param name="context">Synchronization context.</param>
    ///
    /// <returns>BindingSource object.</returns>
    ///
    /// <remarks>
    /// Invokes the ResetBindings(false) method when CollectionChanged
    /// event is fired.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public static BindingSource ToBindingSource(this INotifyCollectionChanged src,
        SynchronizationContext context)
    {
        var dest = new BindingSource { DataSource = src };
        src.CollectionChanged += (_, _) => context.Send(_ => dest.ResetBindings(false), null);
        return dest;
    }

    #endregion
}
