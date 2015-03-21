/* ------------------------------------------------------------------------- */
///
/// UriTester.cs
/// 
/// Copyright (c) 2010 CubeSoft, Inc.
/// 
/// This is distributed under the Microsoft Public License (Ms-PL).
/// See http://www.opensource.org/licenses/ms-pl.html
///
/* ------------------------------------------------------------------------- */
using System;
using System.Runtime.Serialization;
using NUnit.Framework;
using Cube.Extensions;

namespace Cube.Tests
{
    /* --------------------------------------------------------------------- */
    ///
    /// Cube.Tests.UriTester
    /// 
    /// <summary>
    /// Uri クラスの拡張メソッドをテストするためのクラスです。
    /// </summary>
    ///
    /* --------------------------------------------------------------------- */
    [TestFixture]
    class UriTester
    {
        /* ----------------------------------------------------------------- */
        ///
        /// TestWith
        /// 
        /// <summary>
        /// With のテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestWith()
        {
            Assert.DoesNotThrow(() => {
                var uri = new Uri("http://www.cube-soft.jp/");

                var actual = uri.With("key1", "value1");
                var expected = string.Format("{0}?{1}={2}", uri, "key1", "value1");
                Assert.That(actual.ToString(), Is.EqualTo(expected));

                actual = actual.With("key2", "value2");
                expected = string.Format("{0}&{1}={2}", expected, "key2", "value2");
                Assert.That(actual.ToString(), Is.EqualTo(expected));
            });
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestWith
        /// 
        /// <summary>
        /// With のテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [TestCase("string", "value")]
        [TestCase("int", 5)]
        [TestCase("double", 3.14)]
        public void TestWith<T>(string key, T value)
        {
            Assert.DoesNotThrow(() => {
                var uri = new Uri("http://www.cube-soft.jp/index.html");
                var actual = uri.With(key, value);
                var expected = string.Format("{0}?{1}={2}", uri, key, value);
                Assert.That(actual.ToString(), Is.EqualTo(expected));
            });
        }

        /* ----------------------------------------------------------------- */
        ///
        /// TestWithTime
        /// 
        /// <summary>
        /// 時刻を付与するテストを行います。
        /// </summary>
        ///
        /* ----------------------------------------------------------------- */
        [Test]
        public void TestWithTime()
        {
            Assert.DoesNotThrow(() => {
                var time = new DateTime(2015, 3, 19, 23, 57, 57); // 1426777077
                var uri = new Uri("http://www.cube-soft.jp/index.html");
                var actual = uri.With(time);
                var expected = string.Format("{0}?t=1426777077", uri);
                Assert.That(actual.ToString(), Is.EqualTo(expected));
            });
        }
    }
}
