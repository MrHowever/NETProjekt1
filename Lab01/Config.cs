using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    class Config
    {
        public static async Task<News.RootObject> Deserialize(string Uri)
        {
            var json = await ConnectionAPI.GetAsync(Uri);

            System.Diagnostics.Debug.Write(json);

            return JsonConvert.DeserializeObject<News.RootObject>(json);
        }
    }
}
