using System;

namespace Calendar;

public class CalendarService
{
    public static void Test()
    {
        GetWeekday(1, Month.JANUARY, 0);
    }

    public static bool IsLeapYear(int year)
    {
        if (year % 4 == 0)
        {
            if (year % 100 == 0)
            {
                return year % 400 == 0;
            }

            return true;
        }

        return false;
    }

    public static Weekday GetWeekday(int day, Month month, int year)
    {
        var offsetMonth = IsLeapYear(year) ? month.Offset().leapYear : month.Offset().commonYear;
        var correctedYear = year - 1;

        var weekday = (day + offsetMonth + 5 * (correctedYear % 4) + 4 * (correctedYear % 100) +
                      6 * (correctedYear % 400)) % 7;
        
        return weekday.ToWeekday();
    }
}