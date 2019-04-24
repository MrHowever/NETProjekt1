using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    class ConnectionAPI
    {
        public static async Task<string> GetAsync(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Headers["Authorization"] = "Bearer MrNJDxdn4NwNw0Q5qzeNLl8SpJvUmEv7zoZIj8MCA5FVjkUSCyqyjxlS8_qjzBQMVSZSy5SIGYentHW_0JbfZQEpoiu2LlOXdASfJtg9qKLbp2mn925tkOyPYZ6kXHYx";

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }

        }
    }
}
