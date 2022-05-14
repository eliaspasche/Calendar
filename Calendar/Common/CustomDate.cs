namespace Calendar;

public class CustomDate
{
    public int day { get; set; }
    public Month month { get; set; }

    public CustomDate(int day, Month month)
    {
        this.day = day;
        this.month = month;
    }
    
}