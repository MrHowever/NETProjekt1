using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab01
{
    public class Date
    {
        public int Year, Month, Day;

        public Date()
        {
            Year = 0;
            Month = 0;
            Day = 0;
        }

        public Date(String textDate)
        {
            var components = textDate.Split('-');
            if (!(int.TryParse(components[0], out Year) && int.TryParse(components[1], out Month) &&
                int.TryParse(components[2], out Day)))
                    throw new ArgumentException();
        }

        public String toString()
        {
            return Day.ToString("D2") + "-" + Month.ToString("D2") + "-" + Year;
        }
    }

    public class DateComparator : IComparer<Date>
    {
        public int Compare(Date date01, Date date02)
        {
            Date date1 = date01 as Date;
            Date date2 = date02 as Date;

            if (date1.Year < date2.Year)
                return -1;
            else if (date1.Year > date2.Year)
                return 1;
            else
            {
                if (date1.Month < date2.Month)
                    return -1;
                else if (date1.Month < date2.Month)
                    return 1;
                else
                {
                    if (date1.Day < date2.Day)
                        return -1;
                    else if (date1.Day > date2.Day)
                        return 1;
                    else
                        return 0;
                }
            }
        }
    }
    }
