using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    public class Chart
    {
        const String baseUrl = "https://chart.googleapis.com/chart?";
        const String chartType = "cht=lc";
        const String lineColor = "chco=00FF00";
        const String outputFormat = "chof=png";
        const String chartTitle = "chtt=Zmiana+oceny+w+czasie";
        const String showAxes = "chxt=x,y";
        const String gridLines = "chg=16.6,5";
        const String scaling = "chds=a";
        const String chartSize = "chs=600x480";
        const String axisRange = "chxr=1,0,5";
        
        static public String CreateChart(IList<Review> reviews)
        {
            StringBuilder url = new StringBuilder(baseUrl);
            url.Append(chartType);
            url.Append("&" + lineColor);
            url.Append("&" + outputFormat);
            url.Append("&" + chartTitle);
            url.Append("&" + showAxes);
            url.Append("&" + gridLines);
            url.Append("&" + scaling);
            url.Append("&" + chartSize);
            url.Append("&" + axisRange);
            url.Append("&chd=t:");

            IList<double> ratingHistory = new List<double>(reviews.Count);

            for (int i = 0; i < reviews.Count; i++)
            {
                ratingHistory.Add(reviews[i].Rating);
                for (int j = 0; j < i; j++)
                {
                    ratingHistory[i] += reviews[j].Rating;
                }

                ratingHistory[i] /= (i + 1);
            }

            foreach (var rating in ratingHistory)
            {
                url.Append(rating.ToString("0.00", CultureInfo.InvariantCulture) + ",");
            }
            url.Remove(url.Length - 1, 1);

            int interval = reviews.Count / 6;
            interval = interval == 0 ? 1 : interval;
            url.Append("&chxl=0:");

            for (int i = 0; i < reviews.Count; i += interval)
            {
                url.Append("|"+reviews[i].Date.toString());
            }

            return url.ToString();
        }
    }
}
