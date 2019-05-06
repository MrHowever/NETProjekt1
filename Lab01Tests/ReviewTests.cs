using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab01;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01.Tests
{
    [TestClass()]
    public class ReviewTests
    {
        [TestMethod()]
        public void ReviewTest()
        {
            Review review = new Review("3.0", "2019-11-10");

            Assert.AreEqual(review.Rating, 3.0);
        }

        [TestMethod()]
        public void ReviewTest2()
        {
            Review review = new Review("3,0", "2019-11-10");

            Assert.AreEqual(review.Rating, 3.0);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ReviewExceptionTest()
        {
            Review review = new Review("abcd", "2019-11-10");
        }
    }
}