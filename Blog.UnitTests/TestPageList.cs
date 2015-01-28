using Leafing.Web;
using Leafing.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blog.Biz.Models;
using System;
using System.Collections.Generic;

namespace Blog.UnitTests
{
    [TestClass]
    public class TestPageList : TestBase
    {
        private static long[] GetLinkNumList(ListStyle style, long pageCount, long pageIndex, long linkCount = 5)
        {
            var itemList = new ItemList<Article> { PageCount = pageCount, PageIndex = pageIndex };
            var list = new List<long>(itemList.PageLinks(style, linkCount));
            return list.ToArray();
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestOutRange1()
        {
            GetLinkNumList(ListStyle.Default, 99, 100);
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestOutRange2()
        {
            GetLinkNumList(ListStyle.Default, 99, -1);
        }

        [TestMethod, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestOutRange3()
        {
            GetLinkNumList(ListStyle.Default, 10, 5, -1);
        }

        [TestMethod]
        public void TestDefault1()
        {
            var list = GetLinkNumList(ListStyle.Default, 99, 7);
            Assert.AreEqual(new[] {1L, -1L, 6L, 7L, 8L, -1L, 99L}, list);
        }

        [TestMethod]
        public void TestDefault2()
        {
            var list = GetLinkNumList(ListStyle.Default, 99, 1);
            Assert.AreEqual(new[] { 1L, 2L, 3L, 4L, -1L, 99L }, list);
            list = GetLinkNumList(ListStyle.Default, 99, 2);
            Assert.AreEqual(new[] { 1L, 2L, 3L, 4L, -1L, 99L }, list);
            list = GetLinkNumList(ListStyle.Default, 99, 3);
            Assert.AreEqual(new[] { 1L, 2L, 3L, 4L, -1L, 99L }, list);
        }

        [TestMethod]
        public void TestDefault3()
        {
            var list = GetLinkNumList(ListStyle.Default, 99, 4);
            Assert.AreEqual(new[] { 1L, -1L, 3L, 4L, 5L, -1L, 99L }, list);
        }

        [TestMethod]
        public void TestDefault4()
        {
            var list = GetLinkNumList(ListStyle.Default, 99, 97);
            Assert.AreEqual(new[] { 1L, -1L, 96L, 97L, 98L, 99L }, list);
            list = GetLinkNumList(ListStyle.Default, 99, 98);
            Assert.AreEqual(new[] { 1L, -1L, 96L, 97L, 98L, 99L }, list);
            list = GetLinkNumList(ListStyle.Default, 99, 99);
            Assert.AreEqual(new[] { 1L, -1L, 96L, 97L, 98L, 99L }, list);
        }

        [TestMethod]
        public void TestDefault5()
        {
            var list = GetLinkNumList(ListStyle.Default, 99, 96);
            Assert.AreEqual(new[] { 1L, -1L, 95L, 96L, 97L, -1L, 99L }, list);
        }

        [TestMethod]
        public void TestDefault6()
        {
            var list = GetLinkNumList(ListStyle.Default, 5, 1);
            Assert.AreEqual(new[] { 1L, 2L, 3L, 4L, 5L }, list);
            list = GetLinkNumList(ListStyle.Default, 5, 3);
            Assert.AreEqual(new[] { 1L, 2L, 3L, 4L, 5L }, list);
            list = GetLinkNumList(ListStyle.Default, 5, 5);
            Assert.AreEqual(new[] { 1L, 2L, 3L, 4L, 5L }, list);
        }

        [TestMethod]
        public void TestDefault7()
        {
            var list = GetLinkNumList(ListStyle.Default, 5, 1, 10);
            Assert.AreEqual(new[] { 1L, 2L, 3L, 4L, 5L }, list);
            list = GetLinkNumList(ListStyle.Default, 5, 3, 10);
            Assert.AreEqual(new[] { 1L, 2L, 3L, 4L, 5L }, list);
            list = GetLinkNumList(ListStyle.Default, 5, 5, 10);
            Assert.AreEqual(new[] { 1L, 2L, 3L, 4L, 5L }, list);
        }

        [TestMethod]
        public void TestDefault8()
        {
            var list = GetLinkNumList(ListStyle.Default, 5, 1, 11);
            Assert.AreEqual(new[] { 1L, 2L, 3L, 4L, 5L }, list);
            list = GetLinkNumList(ListStyle.Default, 4, 1, 11);
            Assert.AreEqual(new[] { 1L, 2L, 3L, 4L }, list);
            list = GetLinkNumList(ListStyle.Default, 3, 1, 11);
            Assert.AreEqual(new[] { 1L, 2L, 3L }, list);
            list = GetLinkNumList(ListStyle.Default, 2, 1, 11);
            Assert.AreEqual(new[] { 1L, 2L }, list);
            list = GetLinkNumList(ListStyle.Default, 1, 1, 11);
            Assert.AreEqual(new[] { 1L }, list);
        }

        [TestMethod]
        public void TestStatic1()
        {
            var list = GetLinkNumList(ListStyle.Static, 99, 58);
            Assert.AreEqual(new[] { 99L, -1L, 59L, 58L, 57L, -1L, 1L }, list);
        }

        [TestMethod]
        public void TestStatic2()
        {
            var list = GetLinkNumList(ListStyle.Static, 99, 99);
            Assert.AreEqual(new[] { 99L, 98L, 97L, 96L, -1L, 1L }, list);
            list = GetLinkNumList(ListStyle.Static, 99, 98);
            Assert.AreEqual(new[] { 99L, 98L, 97L, 96L, -1L, 1L }, list);
            list = GetLinkNumList(ListStyle.Static, 99, 97);
            Assert.AreEqual(new[] { 99L, 98L, 97L, 96L, -1L, 1L }, list);
        }

        [TestMethod]
        public void TestStatic3()
        {
            var list = GetLinkNumList(ListStyle.Static, 99, 96);
            Assert.AreEqual(new[] { 99L, -1L, 97L, 96L, 95L, -1L, 1L }, list);
        }

        [TestMethod]
        public void TestStatic4()
        {
            var list = GetLinkNumList(ListStyle.Static, 99, 1);
            Assert.AreEqual(new[] { 99L, -1L, 4L, 3L, 2L, 1L }, list);
            list = GetLinkNumList(ListStyle.Static, 99, 2);
            Assert.AreEqual(new[] { 99L, -1L, 4L, 3L, 2L, 1L }, list);
            list = GetLinkNumList(ListStyle.Static, 99, 3);
            Assert.AreEqual(new[] { 99L, -1L, 4L, 3L, 2L, 1L }, list);
        }

        [TestMethod]
        public void TestStatic5()
        {
            var list = GetLinkNumList(ListStyle.Static, 99, 4);
            Assert.AreEqual(new[] { 99L, -1L, 5L, 4L, 3L, -1L, 1L }, list);
        }

        [TestMethod]
        public void TestStatic6()
        {
            var list = GetLinkNumList(ListStyle.Static, 5, 5);
            Assert.AreEqual(new[] { 5L, 4L, 3L, 2L, 1L }, list);
            list = GetLinkNumList(ListStyle.Static, 5, 3);
            Assert.AreEqual(new[] { 5L, 4L, 3L, 2L, 1L }, list);
            list = GetLinkNumList(ListStyle.Static, 5, 1);
            Assert.AreEqual(new[] { 5L, 4L, 3L, 2L, 1L }, list);
        }

        [TestMethod]
        public void TestStatic7()
        {
            var list = GetLinkNumList(ListStyle.Static, 5, 5, 10);
            Assert.AreEqual(new[] { 5L, 4L, 3L, 2L, 1L }, list);
            list = GetLinkNumList(ListStyle.Static, 5, 3, 10);
            Assert.AreEqual(new[] { 5L, 4L, 3L, 2L, 1L }, list);
            list = GetLinkNumList(ListStyle.Static, 5, 1, 10);
            Assert.AreEqual(new[] { 5L, 4L, 3L, 2L, 1L }, list);
        }

        [TestMethod]
        public void TestStatic8()
        {
            var list = GetLinkNumList(ListStyle.Static, 5, 1, 11);
            Assert.AreEqual(new[] { 5L, 4L, 3L, 2L, 1L }, list);
            list = GetLinkNumList(ListStyle.Static, 4, 1, 11);
            Assert.AreEqual(new[] { 4L, 3L, 2L, 1L }, list);
            list = GetLinkNumList(ListStyle.Static, 3, 1, 11);
            Assert.AreEqual(new[] { 3L, 2L, 1L }, list);
            list = GetLinkNumList(ListStyle.Static, 2, 1, 11);
            Assert.AreEqual(new[] { 2L, 1L }, list);
            list = GetLinkNumList(ListStyle.Static, 1, 1, 11);
            Assert.AreEqual(new[] { 1L }, list);
        }

        [TestMethod]
        public void TestHybird1()
        {
            var list = GetLinkNumList(ListStyle.Hybird, 100, 58);
            Assert.AreEqual(new[] { 0L, -1L, 59L, 58L, 57L, -1L, 1L }, list);
        }

        [TestMethod]
        public void TestHybird2()
        {
            var list = GetLinkNumList(ListStyle.Hybird, 100, 99);
            Assert.AreEqual(new[] { 0L, 99L, 98L, 97L, -1L, 1L }, list);
            list = GetLinkNumList(ListStyle.Hybird, 100, 98);
            Assert.AreEqual(new[] { 0L, 99L, 98L, 97L, -1L, 1L }, list);
            list = GetLinkNumList(ListStyle.Hybird, 100, 0);
            Assert.AreEqual(new[] { 0L, 99L, 98L, 97L, -1L, 1L }, list);
        }

        [TestMethod]
        public void TestHybird3()
        {
            var list = GetLinkNumList(ListStyle.Hybird, 100, 97);
            Assert.AreEqual(new[] { 0L, -1L, 98L, 97L, 96L, -1L, 1L }, list);
        }

        [TestMethod]
        public void TestHybird4()
        {
            var list = GetLinkNumList(ListStyle.Hybird, 100, 1);
            Assert.AreEqual(new[] { 0L, -1L, 4L, 3L, 2L, 1L }, list);
            list = GetLinkNumList(ListStyle.Hybird, 100, 2);
            Assert.AreEqual(new[] { 0L, -1L, 4L, 3L, 2L, 1L }, list);
            list = GetLinkNumList(ListStyle.Hybird, 100, 3);
            Assert.AreEqual(new[] { 0L, -1L, 4L, 3L, 2L, 1L }, list);
        }

        [TestMethod]
        public void TestHybird5()
        {
            var list = GetLinkNumList(ListStyle.Hybird, 100, 4);
            Assert.AreEqual(new[] { 0L, -1L, 5L, 4L, 3L, -1L, 1L }, list);
        }

        [TestMethod]
        public void TestHybird6()
        {
            var list = GetLinkNumList(ListStyle.Hybird, 5, 0);
            Assert.AreEqual(new[] { 0L, 4L, 3L, 2L, 1L }, list);
            list = GetLinkNumList(ListStyle.Hybird, 5, 3);
            Assert.AreEqual(new[] { 0L, 4L, 3L, 2L, 1L }, list);
            list = GetLinkNumList(ListStyle.Hybird, 5, 1);
            Assert.AreEqual(new[] { 0L, 4L, 3L, 2L, 1L }, list);
        }

        [TestMethod]
        public void TestHybird7()
        {
            var list = GetLinkNumList(ListStyle.Hybird, 5, 0, 10);
            Assert.AreEqual(new[] { 0L, 4L, 3L, 2L, 1L }, list);
            list = GetLinkNumList(ListStyle.Hybird, 5, 3, 10);
            Assert.AreEqual(new[] { 0L, 4L, 3L, 2L, 1L }, list);
            list = GetLinkNumList(ListStyle.Hybird, 5, 1, 10);
            Assert.AreEqual(new[] { 0L, 4L, 3L, 2L, 1L }, list);
        }

        [TestMethod]
        public void TestHybird8()
        {
            var list = GetLinkNumList(ListStyle.Hybird, 12, 0, 11);
            Assert.AreEqual(new[] { 0L, 11L, 10L, 9L, 8L, 7L, 6L, 5L, 4L, 3L, -1L, 1L }, list);
            list = GetLinkNumList(ListStyle.Hybird, 11, 0, 11);
            Assert.AreEqual(new[] { 0L, 10L, 9L, 8L, 7L, 6L, 5L, 4L, 3L, 2L, 1L }, list);
            list = GetLinkNumList(ListStyle.Hybird, 6, 0, 11);
            Assert.AreEqual(new[] { 0L, 5L, 4L, 3L, 2L, 1L }, list);
            list = GetLinkNumList(ListStyle.Hybird, 5, 0, 11);
            Assert.AreEqual(new[] { 0L, 4L, 3L, 2L, 1L }, list);
            list = GetLinkNumList(ListStyle.Hybird, 4, 0, 11);
            Assert.AreEqual(new[] { 0L, 3L, 2L, 1L }, list);
            list = GetLinkNumList(ListStyle.Hybird, 4, 3, 11);
            Assert.AreEqual(new[] { 0L, 3L, 2L, 1L }, list);
            list = GetLinkNumList(ListStyle.Hybird, 3, 2, 11);
            Assert.AreEqual(new[] { 0L, 2L, 1L }, list);
            list = GetLinkNumList(ListStyle.Hybird, 2, 1, 11);
            Assert.AreEqual(new[] { 0L, 1L }, list);
            list = GetLinkNumList(ListStyle.Hybird, 1, 1, 11);
            Assert.AreEqual(new[] { 0L }, list);
        }
    }
}
