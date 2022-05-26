using System;

namespace Calendar;

/// <summary>
/// 
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
/// 
/// </summary>
public static class WeekdayExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="weekday"></param>
    /// <returns></returns>
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
    /// 
    /// </summary>
    /// <param name="weekday"></param>
    /// <returns></returns>
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