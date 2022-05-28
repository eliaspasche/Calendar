namespace Calendar.Common;

/// <summary>
/// Custom data structure to hold the offset of a month for common and leap year. This offset is needed for the
/// determination of weekdays by Gauss.
/// </summary>
public class MonthOffset
{
    public int CommonYear { get; }
    public int LeapYear { get; }

    public MonthOffset(int commonYear, int leapYear)
    {
        CommonYear = commonYear;
        LeapYear = leapYear;
    }
}