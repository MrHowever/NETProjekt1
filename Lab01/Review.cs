using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    class Review
    {
        public Date Date;
        public int Rating;

        public Review(String rating, String date)
        {
            int.TryParse(rating, out Rating);
            Date = new Date(date);
        }
    }
}
