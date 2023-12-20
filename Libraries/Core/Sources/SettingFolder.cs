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
namespace Cube;

using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;
using Cube.DataContract;
using Cube.FileSystem;
using Cube.Reflection.Extensions;
using Cube.Tasks.Extensions;

/* ------------------------------------------------------------------------- */
///
/// SettingFolder(T)
///
/// <summary>
/// Provides functionality to load and save user settings.
/// </summary>
///
/* ------------------------------------------------------------------------- */
public class SettingFolder<T> : ObservableBase where T : INotifyPropertyChanged, new()
{
    #region Constructors

    /* --------------------------------------------------------------------- */
    ///
    /// SettingFolder(T)
    ///
    /// <summary>
    /// Initializes a new instance of the SettingsFolder class with
    /// the specified arguments.
    /// </summary>
    ///
    /// <param name="assembly">Assembly information.</param>
    /// <param name="format">Serialization format.</param>
    ///
    /* --------------------------------------------------------------------- */
    public SettingFolder(Format format, Assembly assembly) :
        this(format, GetLocation(format, assembly), assembly.GetSoftwareVersion()) { }

    /* --------------------------------------------------------------------- */
    ///
    /// SettingFolder(T)
    ///
    /// <summary>
    /// Initializes a new instance of the SettingsFolder class with
    /// the specified arguments.
    /// </summary>
    ///
    /// <param name="format">Serialization format.</param>
    /// <param name="location">Saved data location.</param>
    /// <param name="version">Software version.</param>
    ///
    /* --------------------------------------------------------------------- */
    public SettingFolder(Format format, string location, SoftwareVersion version)
    {
        _autosaver.AutoReset = false;
        _autosaver.Elapsed += (_, _) => Task.Run(Save).Forget();

        Format   = format;
        Location = location;
        Version  = version;
        Value    = new();

        Value.PropertyChanged += WhenChanged;
    }

    #endregion

    #region Properties

    /* --------------------------------------------------------------------- */
    ///
    /// Value
    ///
    /// <summary>
    /// Gets a value that represents user settings.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public T Value { get; protected set; }

    /* --------------------------------------------------------------------- */
    ///
    /// Format
    ///
    /// <summary>
    /// Gets or sets the serialization format.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public Format Format { get; protected set; }

    /* --------------------------------------------------------------------- */
    ///
    /// Location
    ///
    /// <summary>
    /// Gets or sets the location that the serialized data is saved in.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public string Location { get; protected set; }

    /* --------------------------------------------------------------------- */
    ///
    /// Version
    ///
    /// <summary>
    /// Gets the software version.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public SoftwareVersion Version { get; protected set; }

    /* --------------------------------------------------------------------- */
    ///
    /// AutoSave
    ///
    /// <summary>
    /// Gets or sets the value indicating whether saving automatically
    /// when user settings are changed.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public bool AutoSave { get; set; }

    /* --------------------------------------------------------------------- */
    ///
    /// AutoSaveDelay
    ///
    /// <summary>
    /// Gets or sets the delay between detecting changed in user
    /// settings and saving them.
    /// </summary>
    ///
    /// <remarks>
    /// In the case of AutoSave mode, there is a possibility that a large
    /// number of saves will be performed in a short period of time,
    /// and SettingsFolder avoids these problems by holding off on saving
    /// for a certain amount of time since the last property change.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    public TimeSpan AutoSaveDelay { get; set; } = TimeSpan.FromSeconds(1);

    #endregion

    #region Methods

    /* --------------------------------------------------------------------- */
    ///
    /// Load
    ///
    /// <summary>
    /// Loads the user settings.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public void Load() => OnLoad();

    /* --------------------------------------------------------------------- */
    ///
    /// OnLoad
    ///
    /// <summary>
    /// Loads the user settings.
    /// </summary>
    ///
    /// <remarks>
    /// If the method fails, the AutoSave property will forcibly be
    /// disabled in order to prevent unexpected values from being
    /// automatically saved.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    protected virtual void OnLoad()
    {
        try
        {
            var dest = Format == Format.Registry || Io.Exists(Location) ?
                       Format.Deserialize<T>(Location) :
                       default;
            if (dest is null) return;

            Value.PropertyChanged -= WhenChanged;
            Value = dest;
            Value.PropertyChanged += WhenChanged;
        }
        catch { AutoSave = false; throw; }
    }

    /* --------------------------------------------------------------------- */
    ///
    /// Save
    ///
    /// <summary>
    /// Saves user settings.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public void Save() => OnSave();

    /* --------------------------------------------------------------------- */
    ///
    /// OnSave
    ///
    /// <summary>
    /// Saves user settings.
    /// </summary>
    ///
    /// <remarks>
    /// If the method fails, the AutoSave property will forcibly be
    /// disabled not to fail to invoke the Dispose method.
    /// </remarks>
    ///
    /* --------------------------------------------------------------------- */
    protected virtual void OnSave() => Format.Serialize(Location, Value);

    #endregion

    #region Implementations

    /* --------------------------------------------------------------------- */
    ///
    /// Dispose
    ///
    /// <summary>
    /// Releases the unmanaged resources used by the SettingsFolder
    /// and optionally releases the managed resources.
    /// </summary>
    ///
    /// <param name="disposing">
    /// true to release both managed and unmanaged resources;
    /// false to release only unmanaged resources.
    /// </param>
    ///
    /* --------------------------------------------------------------------- */
    protected override void Dispose(bool disposing)
    {
        if (disposing) _autosaver.Dispose();
        if (AutoSave) Logger.Try(Save);
    }

    /* --------------------------------------------------------------------- */
    ///
    /// GetLocation
    ///
    /// <summary>
    /// Gets the location of serialization data.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private static string GetLocation(Format fmt, Assembly asm)
    {
        var root = fmt != Format.Registry ?
                   Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) :
                   string.Empty;
        return Io.Combine(root, asm.GetCompany(), asm.GetProduct());
    }

    /* --------------------------------------------------------------------- */
    ///
    /// WhenChanged
    ///
    /// <summary>
    /// Occurs when the PropertyChanged event of the Value property
    /// is fired.
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    private void WhenChanged(object s, PropertyChangedEventArgs e)
    {
        try
        {
            _autosaver.Stop();

            if (!AutoSave) return;
            if (AutoSaveDelay > TimeSpan.Zero)
            {
                _autosaver.Interval = AutoSaveDelay.TotalMilliseconds;
                _autosaver.Start();
            }
            else Logger.Try(Save);
        }
        finally { OnPropertyChanged(e); }
    }

    #endregion

    #region Fields
    private readonly Timer _autosaver = new();
    #endregion
}
