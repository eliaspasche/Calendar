using System;
using System.Collections;
using System.Collections.Generic;

namespace Calendar;

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

public static class HolidayExtensions
{
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

    public static CustomDate ToDate(this Holiday holiday, int year)
    {
        int easter = CalcEasterInt(year);

        return holiday switch
        {
            Holiday.NEW_YEAR => new CustomDate(1, Month.JANUARY),
            Holiday.GOOD_FRIDAY => FromIntToDate(easter - 2),
            Holiday.EASTER => FromIntToDate(easter),
            Holiday.EASTER_MONDAY => FromIntToDate(easter + 1),
            Holiday.MAYDAY => new CustomDate(1, Month.MAY),
            Holiday.ASCENSION_DAY => FromIntToDate(easter + 39),
            Holiday.WHIT_SUNDAY => FromIntToDate(easter + 49),
            Holiday.WHIT_MONDAY => FromIntToDate(easter + 50),
            Holiday.DAY_OF_GERMANY => new CustomDate(3, Month.OCTOBER),
            Holiday.REFORMATION_DAY => new CustomDate(31, Month.OCTOBER),
            Holiday.CHRISTMAS => new CustomDate(25, Month.DECEMBER),
            Holiday.BOXING_DAY => new CustomDate(26, Month.DECEMBER),
            _ => throw new ArgumentOutOfRangeException(nameof(holiday), holiday, null)
        };
    }

    public static List<Holiday> HolidaysInThisMonth(this Month month, int year)
    {
        List<Holiday> holidays;

        switch (month)
        {
            case Month.JANUARY:
                holidays = new List<Holiday> {Holiday.NEW_YEAR};
                return holidays;
            case Month.FEBRUARY:
                return new List<Holiday>();
            case Month.MARCH:
                holidays = new List<Holiday>();
                if (Holiday.GOOD_FRIDAY.ToDate(year).month == Month.MARCH)
                {
                    holidays.Add(Holiday.GOOD_FRIDAY);
                }

                if (Holiday.EASTER.ToDate(year).month == Month.MARCH)
                {
                    holidays.Add(Holiday.EASTER);
                }

                if (Holiday.EASTER_MONDAY.ToDate(year).month == Month.MARCH)
                {
                    holidays.Add(Holiday.EASTER_MONDAY);
                }

                return holidays;
            case Month.APRIL:
                holidays = new List<Holiday>();
                if (Holiday.GOOD_FRIDAY.ToDate(year).month == Month.APRIL)
                {
                    holidays.Add(Holiday.GOOD_FRIDAY);
                }

                if (Holiday.EASTER.ToDate(year).month == Month.APRIL)
                {
                    holidays.Add(Holiday.EASTER);
                }

                if (Holiday.EASTER_MONDAY.ToDate(year).month == Month.APRIL)
                {
                    holidays.Add(Holiday.EASTER_MONDAY);
                }

                if (Holiday.ASCENSION_DAY.ToDate(year).month == Month.APRIL)
                {
                    holidays.Add(Holiday.ASCENSION_DAY);
                }

                return holidays;
            case Month.MAY:
                holidays = new List<Holiday> {Holiday.MAYDAY};
                
                if (Holiday.ASCENSION_DAY.ToDate(year).month == Month.MAY)
                {
                    holidays.Add(Holiday.ASCENSION_DAY);
                }
                
                if (Holiday.WHIT_SUNDAY.ToDate(year).month == Month.MAY)
                {
                    holidays.Add(Holiday.WHIT_SUNDAY);
                }

                if (Holiday.WHIT_MONDAY.ToDate(year).month == Month.MAY)
                {
                    holidays.Add(Holiday.WHIT_MONDAY);
                }

                return holidays;
            case Month.JUNE:
                holidays = new List<Holiday>();
                if (Holiday.ASCENSION_DAY.ToDate(year).month == Month.JUNE)
                {
                    holidays.Add(Holiday.ASCENSION_DAY);
                }

                if (Holiday.WHIT_SUNDAY.ToDate(year).month == Month.JUNE)
                {
                    holidays.Add(Holiday.WHIT_SUNDAY);
                }

                if (Holiday.WHIT_MONDAY.ToDate(year).month == Month.JUNE)
                {
                    holidays.Add(Holiday.WHIT_MONDAY);
                }

                return holidays;
            case Month.JULY:
                return new List<Holiday>();

            case Month.AUGUST:
                return new List<Holiday>();

            case Month.SEPTEMBER:
                return new List<Holiday>();

            case Month.OCTOBER:
                holidays = new List<Holiday>
                {
                    Holiday.DAY_OF_GERMANY,
                    Holiday.REFORMATION_DAY
                };
                return holidays;
            case Month.NOVEMBER:
                return new List<Holiday>();

            case Month.DECEMBER:
                holidays = new List<Holiday>
                {
                    Holiday.CHRISTMAS,
                    Holiday.BOXING_DAY
                };
                return holidays;
            default:
                throw new ArgumentOutOfRangeException(nameof(month), month, null);
        }
    }

    private static int CalcEasterInt(int year)
    {
        int k = year / 100;
        int m = 15 + (3 * k + 3) / 4 - (8 * k + 13) / 25;
        int s = 2 - (3 * k + 3) / 4;
        int a = year % 19;
        int d = (19 * a + m) % 30;
        int r = (d + a / 11) / 29;
        int og = 21 + d - r;
        int sz = 7 - (year + year / 4 + s) % 7;
        int oe = 7 - (og - sz) % 7;

        return og + oe;
    }

    private static CustomDate FromIntToDate(int day)
    {
        if (day <= 31)
        {
            return new CustomDate(day, Month.MARCH);
        }

        int dayApril = day - 31;
        if (dayApril <= 30)
        {
            return new CustomDate(dayApril, Month.APRIL);
        }

        int dayMay = dayApril - 30;
        if (dayMay <= 31)
        {
            return new CustomDate(dayMay, Month.MAY);
        }

        int dayJune = dayMay - 31;
        return new CustomDate(dayJune, Month.JUNE);
    }
}