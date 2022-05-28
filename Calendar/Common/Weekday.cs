using System;

// ReSharper disable InconsistentNaming

namespace Calendar.Common;

/// <summary>
/// Enum containing all weekdays.
/// </summary>
public enum Weekday
{
    MONDAY,
    TUESDAY,
    WEDNESDAY,
    THURSDAY,
    FRIDAY,
    SATURDAY,
    SUNDAY
}

/// <summary>
/// Contains various extension methods that can be used to calculate and manage weekdays.
/// </summary>
public static class WeekdayExtension
{
    /// <summary>
    /// Returns the integer value of a given weekday from 1 to 7.
    /// </summary>
    /// <param name="weekday"></param>
    /// <returns>integer value of a weekday</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static int IntLiteral(this Weekday weekday) => weekday switch
    {
        Weekday.MONDAY => 1,
        Weekday.TUESDAY => 2,
        Weekday.WEDNESDAY => 3,
        Weekday.THURSDAY => 4,
        Weekday.FRIDAY => 5,
        Weekday.SATURDAY => 6,
        Weekday.SUNDAY => 7,
        _ => throw new ArgumentOutOfRangeException(nameof(weekday), weekday, null)
    };

    /// <summary>
    /// Returns the corresponding weekday of a given integer value between 1 and 7.
    /// </summary>
    /// <param name="weekday"></param>
    /// <returns>corresponding enum item</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static Weekday ToWeekday(this int weekday) => weekday switch
    {
        1 => Weekday.MONDAY,
        2 => Weekday.TUESDAY,
        3 => Weekday.WEDNESDAY,
        4 => Weekday.THURSDAY,
        5 => Weekday.FRIDAY,
        6 => Weekday.SATURDAY,
        0 => Weekday.SUNDAY,
        _ => throw new ArgumentOutOfRangeException(nameof(weekday), weekday, null)
    };
}