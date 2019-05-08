using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Lab01
{
    public class YelpScraper
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

        async public Task<Tuple<String,String,String>> GetRandomPlace()
        {
            String randomPlace = locations[new Random().Next() % locations.Count()];
            int randomPage = new Random().Next() % 10;  //TODO add a check for maximum page count and use it instead of hardcoded value

            HtmlDocument page = await new HtmlWeb().LoadFromWebAsync(@"https://www.yelp.pl/search?find_desc=&find_loc=" + randomPlace + "&ns=1&start=" + randomPage);
            if (page == null)
                throw new UriFormatException("Randomly generated is invalid. Cannot proceed.");

            var nodes = page.DocumentNode.SelectNodes("//a[@class='lemon--a__373c0__IEZFH link__373c0__29943 photo-box-link__373c0__1AvT5 link-color--blue-dark__373c0__1mhJo link-size--default__373c0__1skgq']");
            int randomNum = new Random().Next() % nodes.Count();
            String bizPage = nodes[randomNum].Attributes["href"].Value;
            
            page = new HtmlWeb().Load(baseUrl + bizPage);
            if (page == null || bizPage == null)
                throw new UriFormatException("Bussiness site Url parsed incorrectly. Cannot proceed.");

            String name = page.DocumentNode.SelectSingleNode("//meta[@property='og:title']").Attributes["content"].Value.Split('-')[0];
            String image = page.DocumentNode.SelectSingleNode("//meta[@property='og:image']").Attributes["content"].Value;

            Regex regex1 = new Regex(@"https:\/\/yelp\.pl\/biz\/.*");
            Regex regex2 = new Regex(@"https:\/\/s3-media3\.fl\.yelpcdn\.com\/bphoto\/.*\/.*\.jpg");

           // if (!regex1.IsMatch(baseUrl + bizPage) || !regex2.IsMatch(image))
             //   throw new UriFormatException("Bussiness site or image Url hasn't been parsed correctly. Cannot proceed");

            return new Tuple<String, String,String>(name, image, baseUrl+bizPage);
        }

        public async Task<IList<Review>> GetReviews(String url)
        {
            IList<Review> reviewsArr = new List<Review>();
            int reviewCounter = 0;
            while (reviewCounter <= 40)
            {
                HtmlDocument page = await new HtmlWeb().LoadFromWebAsync(url+"?start="+reviewCounter);
                var reviews = page.DocumentNode.SelectNodes("//div[@itemprop='review']");

                if (reviews == null)    //No more reviews to be loaded
                    break;

                foreach (var review in reviews)
                {
                    String rating = review.SelectSingleNode("div[@itemprop='reviewRating']").SelectSingleNode("meta[@itemprop='ratingValue']").Attributes["content"].Value;
                    String date = review.SelectSingleNode("meta[@itemprop='datePublished']").Attributes["content"].Value;
                    reviewsArr.Add(new Review(rating, date));
                }

                reviewCounter += 20;    //Default amount of reviews per page equals 20
            }
            
            return reviewsArr.OrderBy(f => f.Date, new DateComparator()).ToList();
        }
    }
}
