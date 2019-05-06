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
    public class YelpTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetJsonAsyncTest()
        {
            Yelp yelp = new Yelp();
            Dictionary<String, String> dict = null;
            var json = yelp.GetJsonAsync(dict);
        }

        [TestMethod()]
        public void GetJsonAsyncTest2()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("term", "delis");
            Yelp yelp = new Yelp();
            var json = yelp.GetJsonAsync(dict);
            Assert.IsNotNull(json);
        }
    }
}