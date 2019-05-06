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
    public class ChartTests
    {
        [TestMethod()]
        public void CreateChartTest()
        {
            IList<Review> reviews = new List<Review>()
            {
                new Review("3.0","2019-03-11"),
                new Review("4.0","2019-04-11"),
                new Review("5,0","2019-05-11")
            };

            String url = Chart.CreateChart(reviews);
            Assert.AreEqual(url, "https://chart.googleapis.com/chart?cht=lc&chco=00FF00&chof=png&chtt=Zmiana+oceny+w+czasie&chxt=x,y&chg=16.6,5&chds=a&chs=600x480&chxr=1,0,5&chd=t:3.00,3.50,4.00&chxl=0:|11-03-2019|11-04-2019|11-05-2019");
        }
    }
}