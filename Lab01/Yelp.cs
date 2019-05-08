using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Microsoft.Json.Schema.Validation;
using Microsoft.Json.Schema;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;

namespace Lab01
{
    public class Yelp
    {
        private HttpClient client;
        private const String baseUri = @"https://api.yelp.com/v3/businesses/search?";
        public JSchema schema;
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
            //String jsonSchema = File.ReadAllText(@"C:\Users\Waldemar\Desktop\Platormy Programistyczne .NET i JAVA\NETProjekt1\Lab01\YelpJSONSchema.json", Encoding.UTF8);
            String jsonSchema = File.ReadAllText(@"E:\Programming\VS\NETProjekt1\Lab01\YelpJSONSchema.json", Encoding.UTF8);
            schema = JSchema.Parse(jsonSchema);
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
            JObject jsonObj = JObject.Parse(@json);

            System.Diagnostics.Debug.Write(json);

           if (!jsonObj.IsValid(schema))
               throw new FileNotFoundException("API has returned invalid JSON object. Cannot proceed.");

            if (json == null)
                throw new FileNotFoundException("API returned null JSON object. Cannot proceed.");

            return JsonConvert.DeserializeObject<YelpJSON.RootObject>(json);
        }
    }
}
