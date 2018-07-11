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
using GalaSoft.MvvmLight.Messaging;
using NUnit.Framework;
using System;
using System.Reflection;
using System.Windows;

namespace Cube.Xui.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// MessengerTest
    ///
    /// <summary>
    /// Messenger に関連するテスト用クラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class MessengerTest
    {
        #region Tests

        /* ----------------------------------------------------------------- */
        ///
        /// Send_CloseMessage
        ///
        /// <summary>
        /// CloseMessage を送信するテストを実行します。。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Send_CloseMessage()
        {
            var src   = new TestViewModel();
            var count = 0;

            Messenger.Default.Register<CloseMessage>(this, e =>
            {
                Assert.That(e, Is.Not.Null);
                ++count;
            });

            src.Test<CloseMessage>();
            Assert.That(count, Is.EqualTo(1));

            Messenger.Default.Unregister<CloseMessage>(this);
            src.Test<CloseMessage>();
            Assert.That(count, Is.EqualTo(1));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Send_ErrorMessage
        ///
        /// <summary>
        /// エラーメッセージを送信するテストを実行します。。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Send_ErrorMessage()
        {
            var src   = new TestViewModel();
            var count = 0;

            Messenger.Default.Register<DialogMessage>(this, e =>
            {
                Assert.That(e.Content.Length, Is.AtLeast(1));
                Assert.That(e.Title,          Is.EqualTo("Cube.Xui"));
                Assert.That(e.Button,         Is.EqualTo(MessageBoxButton.OK));
                Assert.That(e.Image,          Is.EqualTo(MessageBoxImage.Error));
                Assert.That(e.Result,         Is.True);
                ++count;
            });

            src.Test(() => throw new ArgumentException("Test"));
            Assert.That(count, Is.EqualTo(1));

            Messenger.Default.Unregister<DialogMessage>(this);
            src.Test(() => throw new ArgumentException("Test 2"));
            Assert.That(count, Is.EqualTo(1));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Send_DialogMessage
        ///
        /// <summary>
        /// DialogMessage を送信するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Send_DialogMessage()
        {
            var asm  = Assembly.GetExecutingAssembly();
            var src  = new Messenger();
            var dest = default(DialogMessage);

            src.Register<DialogMessage>(this, e => dest = e);
            src.Send(new DialogMessage(nameof(Send_DialogMessage), asm));

            Assert.That(dest,          Is.Not.Null);
            Assert.That(dest.Content,  Is.EqualTo(nameof(Send_DialogMessage)));
            Assert.That(dest.Title,    Is.EqualTo(asm.GetReader().Title));
            Assert.That(dest.Callback, Is.Null);
            Assert.That(dest.Button,   Is.EqualTo(MessageBoxButton.OK));
            Assert.That(dest.Image,    Is.EqualTo(MessageBoxImage.Error));
            Assert.That(dest.Result,   Is.True);
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Send_OpenFileDialogMessage
        ///
        /// <summary>
        /// OpenFileDialogMessage を送信するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Send_OpenFileDialogMessage()
        {
            var src  = new Messenger();
            var dest = default(OpenFileMessage);

            src.Register<OpenFileMessage>(this, e => e.Callback(e));
            src.Send(new OpenFileMessage(e => dest = e));

            Assert.That(dest,                  Is.Not.Null);
            Assert.That(dest.Callback,         Is.Not.Null);
            Assert.That(dest.CheckPathExists,  Is.True, nameof(dest.CheckPathExists));
            Assert.That(dest.FileName,         Is.Null, nameof(dest.FileName));
            Assert.That(dest.FileNames,        Is.Null, nameof(dest.FileNames));
            Assert.That(dest.InitialDirectory, Is.Null, nameof(dest.InitialDirectory));
            Assert.That(dest.Multiselect,      Is.False, nameof(dest.Multiselect));
            Assert.That(dest.Result,           Is.False, nameof(dest.Result));
            Assert.That(dest.Title,            Is.Null, nameof(dest.Title));
            Assert.That(dest.Filter,           Does.StartWith("All Files"));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Send_SaveFileDialogMessage
        ///
        /// <summary>
        /// SaveFileDialogMessage を送信するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Send_SaveFileDialogMessage()
        {
            var src  = new Messenger();
            var dest = default(SaveFileMessage);

            src.Register<SaveFileMessage>(this, e => e.Callback(e));
            src.Send(new SaveFileMessage(e => dest = e));

            Assert.That(dest,                  Is.Not.Null);
            Assert.That(dest.Callback,         Is.Not.Null);
            Assert.That(dest.CheckPathExists,  Is.False, nameof(dest.CheckPathExists));
            Assert.That(dest.FileName,         Is.Null, nameof(dest.FileName));
            Assert.That(dest.InitialDirectory, Is.Null, nameof(dest.InitialDirectory));
            Assert.That(dest.OverwritePrompt,  Is.True, nameof(dest.OverwritePrompt));
            Assert.That(dest.Result,           Is.False, nameof(dest.Result));
            Assert.That(dest.Title,            Is.Null, nameof(dest.Title));
            Assert.That(dest.Filter,           Does.StartWith("All Files"));
        }

        /* ----------------------------------------------------------------- */
        ///
        /// Send_DirectoryDialogMessage
        ///
        /// <summary>
        /// DirectoryDialogMessage を送信するテストを実行します。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void Send_DirectoryDialogMessage()
        {
            var src = new Messenger();
            var dest = default(OpenDirectoryMessage);

            src.Register<OpenDirectoryMessage>(this, e => e.Callback(e));
            src.Send(new OpenDirectoryMessage(e => dest = e));

            Assert.That(dest,           Is.Not.Null);
            Assert.That(dest.Callback,  Is.Not.Null);
            Assert.That(dest.FileName,  Is.Null, nameof(dest.FileName));
            Assert.That(dest.NewButton, Is.True, nameof(dest.NewButton));
            Assert.That(dest.Result,    Is.False, nameof(dest.Result));
            Assert.That(dest.Title,     Is.Null, nameof(dest.Title));
        }

        #endregion

        #region Helper classes and methods

        /* ----------------------------------------------------------------- */
        ///
        /// TestViewModel
        ///
        /// <summary>
        /// テスト用の ViewModel クラスです。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        private class TestViewModel : MessengerViewModel
        {
            public void Test<T>() where T : new() => Send<T>();
            public void Test(Action action) => Send(action);
        }

        #endregion
    }
}
