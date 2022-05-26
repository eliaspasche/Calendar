namespace Calendar;

/// <summary>
/// 
/// </summary>
public class MonthOffset
{
    public int commonYear { get; set; }
    public int leapYear { get; set; }

    public MonthOffset(int commonYear, int leapYear)
    {
        this.commonYear = commonYear;
        this.leapYear = leapYear;
    }
}