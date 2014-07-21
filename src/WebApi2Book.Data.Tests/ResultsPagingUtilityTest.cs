// ResultsPagingUtilityTest.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System;
using NUnit.Framework;

namespace WebApi2Book.Data.Tests
{
    [TestFixture]
    public class ResultsPagingUtilityTest
    {
        [Test]
        [ExpectedException(typeof (ArgumentOutOfRangeException))]
        public void CalculatePageCount_invalid_pageSize()
        {
            ResultsPagingUtility.CalculatePageCount(25, 0);
        }

        [Test]
        [ExpectedException(typeof (ArgumentOutOfRangeException))]
        public void CalculatePageCount_invalid_totalItemCount()
        {
            ResultsPagingUtility.CalculatePageCount(-1, 50);
        }

        [Test]
        public void CalculatePageCount_three_pages()
        {
            const int pageSize = 25;
            const int totalItemCount = (pageSize*2) + 1;
            Assert.AreEqual(3, ResultsPagingUtility.CalculatePageCount(totalItemCount, pageSize));
        }

        [Test]
        public void CalculatePageCount_totalItemCount_equal_to_pageSize()
        {
            const int totalItemCount = 25;
            const int pageSize = totalItemCount;
            Assert.AreEqual(1, ResultsPagingUtility.CalculatePageCount(totalItemCount, pageSize));
        }

        [Test]
        public void CalculatePageCount_totalItemCount_greater_than_pageSize()
        {
            const int pageSize = 25;
            const int totalItemCount = pageSize + 1;
            Assert.AreEqual(2, ResultsPagingUtility.CalculatePageCount(totalItemCount, pageSize));
        }

        [Test]
        public void CalculatePageCount_totalItemCount_less_than_pageSize()
        {
            const int totalItemCount = 25;
            const int pageSize = 50;
            Assert.AreEqual(1, ResultsPagingUtility.CalculatePageCount(totalItemCount, pageSize));
        }

        [Test]
        [ExpectedException(typeof (ArgumentOutOfRangeException))]
        public void CalculatePageSize_invalid_maxValue()
        {
            ResultsPagingUtility.CalculatePageSize(25, 0);
        }

        [Test]
        [ExpectedException(typeof (ArgumentOutOfRangeException))]
        public void CalculatePageSize_invalid_requestedValue()
        {
            ResultsPagingUtility.CalculatePageSize(0, 50);
        }

        [Test]
        public void CalculatePageSize_requestedValue_greater_than_maxValue()
        {
            const int requestedValue = 50;
            const int maxValue = 25;
            Assert.AreEqual(maxValue, ResultsPagingUtility.CalculatePageSize(requestedValue, maxValue));
        }

        [Test]
        public void CalculatePageSize_requestedValue_less_than_maxValue()
        {
            const int requestedValue = 25;
            const int maxValue = 50;
            Assert.AreEqual(requestedValue, ResultsPagingUtility.CalculatePageSize(requestedValue, maxValue));
        }

        [Test]
        public void CalculateStartIndex_first_page()
        {
            Assert.AreEqual(0, ResultsPagingUtility.CalculateStartIndex(1, 50));
        }

        [Test]
        [ExpectedException(typeof (ArgumentOutOfRangeException))]
        public void CalculateStartIndex_invalid_pageNumber()
        {
            ResultsPagingUtility.CalculateStartIndex(0, 50);
        }

        [Test]
        [ExpectedException(typeof (ArgumentOutOfRangeException))]
        public void CalculateStartIndex_invalid_pageSize()
        {
            ResultsPagingUtility.CalculateStartIndex(25, 0);
        }

        [Test]
        public void CalculateStartIndex_second_page()
        {
            const int pageSize = 50;
            Assert.AreEqual(pageSize, ResultsPagingUtility.CalculateStartIndex(2, pageSize));
        }
    }
}