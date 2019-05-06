using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    public class Review
    {
        public Date Date;
        public double Rating;

        public Review(String rating, String date)
        {
            if(!double.TryParse(rating, out Rating))
             if (!double.TryParse(rating, NumberStyles.Number, CultureInfo.CreateSpecificCulture("en-US"), out Rating))
                throw new ArgumentException();

            Date = new Date(date);
        }
    }
}
