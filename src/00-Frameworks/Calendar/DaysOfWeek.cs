namespace Calendar;

public static class DaysOfWeek
{
    public static string GetDayType(DateOnly date)
    {
        var evenDays = new[] { DayOfWeek.Saturday, DayOfWeek.Monday, DayOfWeek.Wednesday };
        var oddDays = new[] { DayOfWeek.Sunday, DayOfWeek.Tuesday, DayOfWeek.Thursday };

        if (evenDays.Contains(date.DayOfWeek))
        {
            return "Even";
        }
        else if (oddDays.Contains(date.DayOfWeek))
        {
            return "Odd";
        }
        else
        {
            return "Unknown"; // به عنوان پیش‌فرض برای روزهایی که احتمالاً نامشخص باشند (یا Friday).
        }
    }
    public static DateOnly FindNextOddDay(DateOnly date)
    {
        if (date.DayOfWeek == DayOfWeek.Thursday)
            return date.AddDays(3);

        else if (GetDayType(date) == "Odd" || date.DayOfWeek == DayOfWeek.Friday)
            return date.AddDays(2);
        else

            return date.AddDays(2);

    }
    public static DateOnly FindNextEvenDay(DateOnly date)
    {
        if (date.DayOfWeek == DayOfWeek.Wednesday)
            return date.AddDays(3);

        if (GetDayType(date) == "Even" || date.DayOfWeek == DayOfWeek.Thursday)
            return date.AddDays(2);
        else
            return date.AddDays(1);


    }

    public static string GetNameOfDay(DateOnly date)
    {
        switch (date.DayOfWeek)
        {
            case DayOfWeek.Saturday:
                return "شنبه";
            case DayOfWeek.Sunday:
                return "یکشنبه";
            case DayOfWeek.Monday:
                return "دوشنبه";
            case DayOfWeek.Tuesday:
                return "سشنبه";
            case DayOfWeek.Wednesday:
                return "جهارشنبه";
            case DayOfWeek.Thursday:
                return "پنجشنبه";
            default: return "Unknown";

        }
    }

}
