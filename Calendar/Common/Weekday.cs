using System;

namespace Calendar;

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

public static class WeekdayExtension
{
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