using System;
using Calendar.Services;

// ReSharper disable InconsistentNaming

namespace Calendar.Common;

/// <summary>
/// Enum containing all months.
/// </summary>
public enum Month
{
    JANUARY,
    FEBRUARY,
    MARCH,
    APRIL,
    MAY,
    JUNE,
    JULY,
    AUGUST,
    SEPTEMBER,
    OCTOBER,
    NOVEMBER,
    DECEMBER
}

/// <summary>
/// Contains various extension methods that can be used to calculate and manage months.
/// </summary>
public static class MonthExtensions
{
    /// <summary>
    /// Returns the given month as integer value from 1 to 12.
    /// </summary>
    /// <param name="month">month for which the int value is needed</param>
    /// <returns>an integer between 1 and 12</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static int IntLiteral(this Month month) => month switch
    {
        Month.JANUARY => 1,
        Month.FEBRUARY => 2,
        Month.MARCH => 3,
        Month.APRIL => 4,
        Month.MAY => 5,
        Month.JUNE => 6,
        Month.JULY => 7,
        Month.AUGUST => 8,
        Month.SEPTEMBER => 9,
        Month.OCTOBER => 10,
        Month.NOVEMBER => 11,
        Month.DECEMBER => 12,
        _ => throw new ArgumentOutOfRangeException(nameof(month), month, null)
    };

    /// <summary>
    /// Returns the month offset for the given month containing the offset for normal as well as leap years.
    /// This offset is used in the weekday calculation by Gauss:
    /// <a href="https://en.wikipedia.org/wiki/Determination_of_the_day_of_the_week#Gauss's_algorithm">See on Wikipedia</a>
    /// </summary>
    /// <param name="month">month for which the offset is needed</param>
    /// <returns>month offset for normal and leap years for a given month</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static MonthOffset Offset(this Month month) => month switch
    {
        Month.JANUARY => new MonthOffset(0, 0),
        Month.FEBRUARY => new MonthOffset(3, 3),
        Month.MARCH => new MonthOffset(3, 4),
        Month.APRIL => new MonthOffset(6, 0),
        Month.MAY => new MonthOffset(1, 2),
        Month.JUNE => new MonthOffset(4, 5),
        Month.JULY => new MonthOffset(6, 0),
        Month.AUGUST => new MonthOffset(2, 3),
        Month.SEPTEMBER => new MonthOffset(5, 6),
        Month.OCTOBER => new MonthOffset(0, 1),
        Month.NOVEMBER => new MonthOffset(3, 4),
        Month.DECEMBER => new MonthOffset(5, 6),
        _ => throw new ArgumentOutOfRangeException(nameof(month), month, null)
    };

    /// <summary>
    /// Returns the name of a given month enum item.
    /// </summary>
    /// <param name="month">month for which the name is needed</param>
    /// <returns>the name of the given month as string</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static string StringLiteral(this Month month) => month switch
    {
        Month.JANUARY => "January",
        Month.FEBRUARY => "February",
        Month.MARCH => "March",
        Month.APRIL => "April",
        Month.MAY => "May",
        Month.JUNE => "June",
        Month.JULY => "July",
        Month.AUGUST => "August",
        Month.SEPTEMBER => "September",
        Month.OCTOBER => "October",
        Month.NOVEMBER => "November",
        Month.DECEMBER => "December",
        _ => throw new ArgumentOutOfRangeException(nameof(month), month, null)
    };

    /// <summary>
    /// Returns the corresponding month to a name of a month.
    /// </summary>
    /// <param name="stringLiteral">name of a month</param>
    /// <returns>corresponding enum item</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static Month ToMonth(this string stringLiteral) => stringLiteral switch
    {
        "January" => Month.JANUARY,
        "February" => Month.FEBRUARY,
        "March" => Month.MARCH,
        "April" => Month.APRIL,
        "May" => Month.MAY,
        "June" => Month.JUNE,
        "July" => Month.JULY,
        "August" => Month.AUGUST,
        "September" => Month.SEPTEMBER,
        "October" => Month.OCTOBER,
        "November" => Month.NOVEMBER,
        "December" => Month.DECEMBER,
        _ => throw new ArgumentOutOfRangeException(nameof(stringLiteral), stringLiteral, null)
    };

    /// <summary>
    /// Returns the corresponding month to a integer value of a month.
    /// </summary>
    /// <param name="intLiteral">integer value between 1 and 12</param>
    /// <returns>corresponding enum item</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static Month ToMonth(this int intLiteral) => intLiteral switch
    {
        1 => Month.JANUARY,
        2 => Month.FEBRUARY,
        3 => Month.MARCH,
        4 => Month.APRIL,
        5 => Month.MAY,
        6 => Month.JUNE,
        7 => Month.JULY,
        8 => Month.AUGUST,
        9 => Month.SEPTEMBER,
        10 => Month.OCTOBER,
        11 => Month.NOVEMBER,
        12 => Month.DECEMBER,
        _ => throw new ArgumentOutOfRangeException(nameof(intLiteral), intLiteral, null)
    };

    /// <summary>
    /// Returns the number of days of a given month. The year is needed to get the correct number of days of February.
    /// </summary>
    /// <param name="month">month for which the number of days is needed</param>
    /// <param name="year"></param>
    /// <returns>number of days in this month</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static int NumberOfDays(this Month month, int year)
    {
        var leapYear = CalendarService.IsLeapYear(year);

        return month switch
        {
            Month.JANUARY => 31,
            Month.FEBRUARY => leapYear ? 29 : 28,
            Month.MARCH => 31,
            Month.APRIL => 30,
            Month.MAY => 31,
            Month.JUNE => 30,
            Month.JULY => 31,
            Month.AUGUST => 31,
            Month.SEPTEMBER => 30,
            Month.OCTOBER => 31,
            Month.NOVEMBER => 30,
            Month.DECEMBER => 31,
            _ => throw new ArgumentOutOfRangeException(nameof(month), month, null)
        };
    }

    /// <summary>
    /// Returns the following month of the given month.
    /// </summary>
    /// <param name="month"></param>
    /// <returns>th following month</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static Month NextMonth(this Month month) => month switch
    {
        Month.JANUARY => Month.FEBRUARY,
        Month.FEBRUARY => Month.MARCH,
        Month.MARCH => Month.APRIL,
        Month.APRIL => Month.MAY,
        Month.MAY => Month.JUNE,
        Month.JUNE => Month.JULY,
        Month.JULY => Month.AUGUST,
        Month.AUGUST => Month.SEPTEMBER,
        Month.SEPTEMBER => Month.OCTOBER,
        Month.OCTOBER => Month.NOVEMBER,
        Month.NOVEMBER => Month.DECEMBER,
        Month.DECEMBER => Month.JANUARY,
        _ => throw new ArgumentOutOfRangeException(nameof(month), month, null)
    };

    /// <summary>
    /// Returns the previous month for a given month.
    /// </summary>
    /// <param name="month"></param>
    /// <returns>the previous month</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static Month PrevMonth(this Month month) => month switch
    {
        Month.JANUARY => Month.DECEMBER,
        Month.FEBRUARY => Month.JANUARY,
        Month.MARCH => Month.FEBRUARY,
        Month.APRIL => Month.MARCH,
        Month.MAY => Month.APRIL,
        Month.JUNE => Month.MAY,
        Month.JULY => Month.JUNE,
        Month.AUGUST => Month.JULY,
        Month.SEPTEMBER => Month.AUGUST,
        Month.OCTOBER => Month.SEPTEMBER,
        Month.NOVEMBER => Month.OCTOBER,
        Month.DECEMBER => Month.NOVEMBER,
        _ => throw new ArgumentOutOfRangeException(nameof(month), month, null)
    };
}