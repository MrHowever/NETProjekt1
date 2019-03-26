using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{ /// <summary>
/// The stucture of JSON
/// </summary>
    public class News
    {
        public class RootObject
        {
            public string status { get; set; }
            public int totalResults { get; set; }
            public Article [] articles { get; set; }
        }

        public class Article
        {
            public Source source { get; set; }
            public string author { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public string url { get; set; }
            public string urlToImage { get; set; }
            public DateTime publishedAt { get; set; }
            public string content { get; set; }

        }

        public class Source
        {
            public string id { get; set; }
            public string name { get; set; }
        }




    }
}
