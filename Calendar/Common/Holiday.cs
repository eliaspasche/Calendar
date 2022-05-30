using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable InconsistentNaming

namespace Calendar.Common;

/// <summary>
/// Enum containing all national holidays.
/// </summary>
public enum Holiday
{
    NEW_YEAR,
    GOOD_FRIDAY,
    EASTER,
    EASTER_MONDAY,
    MAYDAY,
    ASCENSION_DAY,
    WHIT_SUNDAY,
    WHIT_MONDAY,
    DAY_OF_GERMANY,
    REFORMATION_DAY,
    CHRISTMAS,
    BOXING_DAY,
}

/// <summary>
/// Contains various extension methods that can be used to calculate and manage holidays.
/// </summary>
public static class HolidayExtensions
{
    /// <summary>
    /// Returns the name of the national holiday to the given enum.
    /// </summary>
    /// <param name="holiday">National holiday for which the name is required</param>
    /// <returns>Name of the national holiday as string</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static string StringLiteral(this Holiday holiday) => holiday switch
    {
        Holiday.NEW_YEAR => "New Year's Day",
        Holiday.GOOD_FRIDAY => "Good Friday",
        Holiday.EASTER => "Easter",
        Holiday.EASTER_MONDAY => "Easter Monday",
        Holiday.MAYDAY => "May Day",
        Holiday.ASCENSION_DAY => "Ascension Day",
        Holiday.WHIT_SUNDAY => "Whit Sunday",
        Holiday.WHIT_MONDAY => "Whit Monday",
        Holiday.DAY_OF_GERMANY => "German Unity Day",
        Holiday.REFORMATION_DAY => "Reformation Day",
        Holiday.CHRISTMAS => "Christmas",
        Holiday.BOXING_DAY => "Boxing Day",
        _ => throw new ArgumentOutOfRangeException(nameof(holiday), holiday, null)
    };

    /// <summary>
    /// Returns a CustomDate for the given national holiday in the year passed.
    /// </summary>
    /// <param name="holiday">National holiday for which the date is required</param>
    /// <param name="year">Year to calculate the date of the holiday</param>
    /// <returns>Day and month of the given national holiday</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static CustomDate ToDate(this Holiday holiday, int year)
    {
        var easter = CalcEasterInt(year);

        return holiday switch
        {
            Holiday.NEW_YEAR => new CustomDate(1, Month.JANUARY),
            Holiday.GOOD_FRIDAY => NthMarchToDate(easter - 2, year),
            Holiday.EASTER => NthMarchToDate(easter, year),
            Holiday.EASTER_MONDAY => NthMarchToDate(easter + 1, year),
            Holiday.MAYDAY => new CustomDate(1, Month.MAY),
            Holiday.ASCENSION_DAY => NthMarchToDate(easter + 39, year),
            Holiday.WHIT_SUNDAY => NthMarchToDate(easter + 49, year),
            Holiday.WHIT_MONDAY => NthMarchToDate(easter + 50, year),
            Holiday.DAY_OF_GERMANY => new CustomDate(3, Month.OCTOBER),
            Holiday.REFORMATION_DAY => new CustomDate(31, Month.OCTOBER),
            Holiday.CHRISTMAS => new CustomDate(25, Month.DECEMBER),
            Holiday.BOXING_DAY => new CustomDate(26, Month.DECEMBER),
            _ => throw new ArgumentOutOfRangeException(nameof(holiday), holiday, null)
        };
    }

    /// <summary>
    /// Returns a list of national holidays for a given month and year.
    /// </summary>
    /// <param name="month">month for which national holidays are required</param>
    /// <param name="year">year for which national holidays are required</param>
    /// <returns>list with national holidays</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static List<Holiday> HolidaysInThisMonth(this Month month, int year)
    {
        return Enum.GetValues(typeof(Holiday)).Cast<Holiday>()
            .Where(holiday => holiday.ToDate(year).Month == month).ToList();
    }

    /// <summary>
    /// Calculate the date for Easter Sunday in a given year and returns this date as the n-th of march.
    /// The calculation is based on the Gaussian Easter Sunday algorithm:
    /// <a href="https://de.wikipedia.org/wiki/Gau%C3%9Fsche_Osterformel#Eine_erg%C3%A4nzte_Osterformel">See on Wikipedia</a>
    /// </summary>
    /// <param name="year"></param>
    /// <returns></returns>
    private static int CalcEasterInt(int year)
    {
        // secular number
        var k = year / 100;

        // secular moon circuit
        var m = 15 + (3 * k + 3) / 4 - (8 * k + 13) / 25;

        // secular sun circuit
        var s = 2 - (3 * k + 3) / 4;

        // moon parameter
        var a = year % 19;

        // germ for the first full moon in the spring
        var d = (19 * a + m) % 30;

        // calendrical correction value
        var r = (d + a / 11) / 29;

        // easter border
        var og = 21 + d - r;

        // first sunday in march
        var sz = 7 - (year + year / 4 + s) % 7;

        // the distance of Easter Sunday from the Easter border
        var oe = 7 - (og - sz) % 7;


        // Easter Sunday as n-th march
        return og + oe;
    }

    /// <summary>
    /// Calculates and returns the n-th march as a CustomDate. This integer is the result of the
    /// Gaussian algorithm for calculating Easter Sunday. 
    /// </summary>
    /// <param name="day">n-th march from Gaussian Easter Sunday algorithm</param>
    /// <param name="year">current year is needed for the calculation of the number of days in February</param>
    /// <returns>calculated n-th march</returns>
    private static CustomDate NthMarchToDate(int day, int year)
    {
        var months = new[] {Month.MARCH, Month.APRIL, Month.MAY, Month.JUNE};

        foreach (var month in months)
        {
            if (month != Month.JANUARY && day <= month.NumberOfDays(year))
            {
                return new CustomDate(day, month);
            }

            day -= month.NumberOfDays(year);
        }

        return new CustomDate(day, Month.JULY);
    }
}