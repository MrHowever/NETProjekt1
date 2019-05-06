using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab01;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Lab01.Tests
{
    [TestClass()]
    public class YelpScraperTests
    {
        [TestMethod()]
        public void GetRandomPlaceTest()
        {
            Regex regex = new Regex(@"https:\/\/yelp\.pl\/biz\/.*");
            YelpScraper scraper = new YelpScraper();
            var place = scraper.GetRandomPlace();
            Assert.IsTrue(regex.IsMatch(place.Item3));
        }

        [TestMethod()]
        public void GetRandomPlaceTest2()
        {
            Regex regex = new Regex(@"https:\/\/s3-media3\.fl\.yelpcdn\.com\/bphoto\/.*\/.*\.jpg");
            YelpScraper scraper = new YelpScraper();
            var place = scraper.GetRandomPlace();
            Assert.IsTrue(regex.IsMatch(place.Item2));
        }
    }
}