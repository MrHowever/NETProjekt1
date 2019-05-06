using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Lab01
{
    public class Yelp
    {
        private HttpClient client;
        private const String baseUri = @"https://api.yelp.com/v3/businesses/search?";
        private IList<String> AvailableParams = new List<string>
        {
            "term",
            "location",
            "latitude",
            "longitude",
            "radius",
            "categories",
            "locale",
            "limit",
            "offset",
            "sort_by",
            "price",
            "open_now",
            "open_at",
            "attributes"
        };

        public Yelp()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "MrNJDxdn4NwNw0Q5qzeNLl8SpJvUmEv7zoZIj8MCA5FVjkUSCyqyjxlS8_qjzBQMVSZSy5SIGYentHW_0JbfZQEpoiu2LlOXdASfJtg9qKLbp2mn925tkOyPYZ6kXHYx");
        }

        private async Task<string> GetResponseAsync(String uri)
        {
            HttpResponseMessage response = await client.GetAsync(uri);
            Stream stream = await response.Content.ReadAsStreamAsync();
            StreamReader reader = new StreamReader(stream);

            return await reader.ReadToEndAsync();
        }

        private bool isCorrect(String inputParam)
        {
            foreach (var param in AvailableParams)
                if (inputParam == param)
                    return true;

            return false;
        }

        public async Task<YelpJSON.RootObject> GetJsonAsync(Dictionary<string,string> queryParams = null)
        {
            if (queryParams == null)
                throw new ArgumentNullException("At least one query param must be specified");

            StringBuilder stringBuilder = new StringBuilder(baseUri);

            foreach(var queryParam in queryParams)
            {
                if (isCorrect(queryParam.Key))
                {
                    if (stringBuilder.ToString().Last<char>() != '?')
                        stringBuilder.Append("&");

                    stringBuilder.Append(queryParam.Key + "=" + queryParam.Value);
                }
            }
            
            var json = await GetResponseAsync("\n\n"+stringBuilder.ToString()+"\n\n");

            if (json == null)
                throw new FileNotFoundException("API returned null JSON object. Cannot proceed.");

            return JsonConvert.DeserializeObject<YelpJSON.RootObject>(json);
        }
    }
}
