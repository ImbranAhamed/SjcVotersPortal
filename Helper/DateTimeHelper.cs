namespace SjcVotersPortal.Helper;

public static class DateTimeHelper
{
    public static DateTime Now => DateTime.Now.AddDays(2).AddHours(-10);
}