using Calendar.Common;

namespace Calendar.Services;

/// <summary>
/// Helper methods which are needed for calendar calculations.
/// </summary>
public static class CalendarService
{
    /// <summary>
    /// Returns true if the given year is a leap year.
    /// </summary>
    /// <param name="year"></param>
    /// <returns></returns>
    public static bool IsLeapYear(int year)
    {
        if (year % 4 != 0) return false;

        if (year % 100 == 0)
        {
            return year % 400 == 0;
        }

        return true;
    }

    /// <summary>
    /// Returns the correct weekday of a given date. The calculation is based on the Determination of weekdays by Gauss:
    /// <a href="https://en.wikipedia.org/wiki/Determination_of_the_day_of_the_week#Gauss's_algorithm">See on Wikipedia</a>
    /// </summary>
    /// <param name="day"></param>
    /// <param name="month"></param>
    /// <param name="year"></param>
    /// <returns>the calculated weekday</returns>
    public static Weekday GetWeekday(int day, Month month, int year)
    {
        // Set month offset depending on leap year or common year
        var offsetMonth = IsLeapYear(year) ? month.Offset().LeapYear : month.Offset().CommonYear;

        // correct the year for the calculation
        var correctedYear = year - 1;

        // calculate the weekday using the Gaussian expression for gregorian calendars
        var weekday = (day + offsetMonth + 5 * (correctedYear % 4) + 4 * (correctedYear % 100) +
                       6 * (correctedYear % 400)) % 7;

        return weekday.ToWeekday();
    }
}