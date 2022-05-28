namespace Calendar.Common;

/// <summary>
/// Custom date object that contains a day and a month.
/// </summary>
public class CustomDate
{
    public int Day { get; }
    public Month Month { get; }

    public CustomDate(int day, Month month)
    {
        Day = day;
        Month = month;
    }
}