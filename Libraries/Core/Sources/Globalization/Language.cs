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
namespace Cube.Globalization;

/* ------------------------------------------------------------------------- */
///
/// Language
///
/// <summary>
/// Specifies kinds of language.
/// </summary>
///
/// <seealso href="https://learn.microsoft.com/en-us/openspecs/office_standards/ms-oe376/6c085406-a698-4e12-9d4d-c3b0ee3dbc4a" />
///
/* ------------------------------------------------------------------------- */
public enum Language
{
    /// <summary>Same as the system locale</summary>
    Auto = 0x0000,
    /// <summary>Bulgarian (bg-BG)</summary>
    Bulgarian = 0x0402,
    /// <summary>Traditional Chinese (zh-TW)</summary>
    TraditionalChinese = 0x0404,
    /// <summary>Czech (cs-CZ)</summary>
    Czech = 0x0405,
    /// <summary>Danish (da-DK)</summary>
    Danish = 0x0406,
    /// <summary>German (de-DE)</summary>
    German = 0x0407,
    /// <summary>Greek (el-GR)</summary>
    Greek = 0x0408,
    /// <summary>English (en-US)</summary>
    English = 0x0409,
    /// <summary>Spanish (es-ES)</summary>
    Spanish = 0x040A,
    /// <summary>Finnish (fi-FI)</summary>
    Finnish = 0x040B,
    /// <summary>French (fr-FR)</summary>
    French = 0x040C,
    /// <summary>Hebrew (he-IL)</summary>
    Hebrew = 0x040D,
    /// <summary>Hungarian (hu-HU)</summary>
    Hungarian = 0x040E,
    /// <summary>Icelandic (is-IS)</summary>
    Icelandic = 0x040F,
    /// <summary>Italian (it-IT)</summary>
    Italian = 0x0410,
    /// <summary>Japanese (ja-JP)</summary>
    Japanese = 0x0411,
    /// <summary>Korean (ko-KR)</summary>
    Korean = 0x0412,
    /// <summary>Dutch (nl-NL)</summary>
    Dutch = 0x0413,
    /// <summary>Norwegian (nb-NO)</summary>
    Norwegian = 0x0414,
    /// <summary>Polish (pl-PL)</summary>
    Polish = 0x0415,
    /// <summary>Romanian (ro-RO)</summary>
    Romanian = 0x0418,
    /// <summary>Russian (ru-RU)</summary>
    Russian = 0x0419,
    /// <summary>Croatian (hr-HR)</summary>
    Croatian = 0x041A,
    /// <summary>Slovak (sk-SK)</summary>
    Slovak = 0x041B,
    /// <summary>Albanian (sq-AL)</summary>
    Albanian = 0x041C,
    /// <summary>Swedish (sv-SE)</summary>
    Swedish = 0x041D,
    /// <summary>Thai (th-TH)</summary>
    Thai = 0x041E,
    /// <summary>Turkish (tr-TR)</summary>
    Turkish = 0x041F,
    /// <summary>Indonesian (id-ID)</summary>
    Indonesian = 0x0421,
    /// <summary>Ukrainian (uk-UA)</summary>
    Ukrainian = 0x0422,
    /// <summary>Belarusian (be-BY)</summary>
    Belarusian = 0x0423,
    /// <summary>Slovenian (sl-SI)</summary>
    Slovenian = 0x0424,
    /// <summary>Estonian (et-EE)</summary>
    Estonian = 0x0425,
    /// <summary>Latvian (lv-LV)</summary>
    Latvian = 0x0426,
    /// <summary>Lithuanian (lt-LT)</summary>
    Lithuanian = 0x0427,
    /// <summary>Persian (fa-IR)</summary>
    Persian = 0x0429,
    /// <summary>Vietnamese (vi-VN)</summary>
    Vietnamese = 0x042A,
    /// <summary>Armenian (hy-AM)</summary>
    Armenian = 0x042B,
    /// <summary>Simplified Chinese (zh-CN)</summary>
    SimplifiedChinese = 0x804,
    /// <summary>Portuguese (pt-PT)</summary>
    Portuguese = 0x0816,
    /// <summary>Unknown value</summary>
    Unknown = -1,
}
