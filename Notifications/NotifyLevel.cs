/* ------------------------------------------------------------------------- */
///
/// NotifyLevel.cs
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
namespace Cube
{
    /* --------------------------------------------------------------------- */
    ///
    /// NotifyLevel
    /// 
    /// <summary>
    /// 通知した項目の重要度を示す値を定義した列挙体です。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    public enum NotifyLevel : int
    {
        None = 0,
        Information = 1,
        Recommended = 2,
        Important = 3,
        Warning = 4,
        Error = 5,
    }
}
