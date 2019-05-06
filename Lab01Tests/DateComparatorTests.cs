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
    public class DateComparatorTests
    {
        [TestMethod()]
        public void CompareBiggerTest()
        {
            Date date1 = new Date("2019-10-11");
            Date date2 = new Date("2018-10-11");
            DateComparator comparator = new DateComparator();
            Assert.AreEqual(comparator.Compare(date1,date2),1);
        }

        [TestMethod()]
        public void CompareSmallerTest()
        {
            Date date1 = new Date("2018-10-11");
            Date date2 = new Date("2019-10-11");
            DateComparator comparator = new DateComparator();
            Assert.AreEqual(comparator.Compare(date1, date2), -1);
        }

        [TestMethod()]
        public void CompareEqualTest()
        {
            Date date1 = new Date("2019-10-11");
            Date date2 = new Date("2019-10-11");
            DateComparator comparator = new DateComparator();
            Assert.AreEqual(comparator.Compare(date1, date2), 0);
        }
    }
}