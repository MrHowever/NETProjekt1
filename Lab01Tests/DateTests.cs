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
    public class DateTests
    {
        [TestMethod()]
        public void DateTest()
        {
            String text1 = "2019-10-11";
            Date constructed = new Date(text1);
            Date expected = new Date();
            expected.Year = 2019;
            expected.Month = 10;
            expected.Day = 11;

            Assert.AreEqual(expected.Year, constructed.Year);
            Assert.AreEqual(expected.Month, constructed.Month);
            Assert.AreEqual(expected.Day, constructed.Day);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void DateTest2()
        {
            Date date = new Date("ab-19-2000");
            Assert.Fail();
        }
        [TestMethod()]
        public void toStringTest()
        {
            Date date = new Date("2019-10-11");
            Assert.AreEqual(date.toString(), "11-10-2019");
        }

    
    }
}