﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Lab01
{
    class YelpScraper
    {
        private String baseUrl;
        public IList<String> locations;

        public YelpScraper()
        {
            baseUrl = "https://yelp.pl";
            locations = new List<String>()
            {
                "Warsaw",
                "London",
                "Berlin",
                "Vienna",
                "Prague",
                "Paris",
                "Madrid",
                "Rome",
                "Moscow"
            };
        }

        public Tuple<String,String> GetRandomPlace()
        {
            String randomPlace = locations[new Random().Next() % locations.Count()];
            int randomPage = new Random().Next() % 10;  //TODO add a check for maximum page count and use it instead of hardcoded value
            HtmlDocument page = new HtmlWeb().Load(@"https://www.yelp.pl/search?find_desc=&find_loc="+randomPlace+"&ns=1&start="+randomPage);
            String bizPage = String.Empty;
            if (page != null)
            {
                var nodes = page.DocumentNode.SelectNodes("//a[@class='lemon--a__373c0__IEZFH link__373c0__29943 photo-box-link__373c0__1AvT5 link-color--blue-dark__373c0__1mhJo link-size--default__373c0__1skgq']");//.Attributes["href"].Value;
                int randomNum = new Random().Next() % nodes.Count();
                bizPage = nodes[randomNum].Attributes["href"].Value;
            }

            page = new HtmlWeb().Load(baseUrl + bizPage);
            String name = page.DocumentNode.SelectSingleNode("//meta[@property='og:title']").Attributes["content"].Value.Split('-')[0];
            String image = page.DocumentNode.SelectSingleNode("//meta[@property='og:image']").Attributes["content"].Value;

            return new Tuple<String, String>(name, image);
        }
    }
}
