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
        do
        {
            if (date.DayOfWeek == DayOfWeek.Thursday)
                date = date.AddDays(3);

            else if (GetDayType(date) == "Odd" || date.DayOfWeek == DayOfWeek.Friday)
                date = date.AddDays(2);
            else

                date = date.AddDays(2);
        } while (date < DateOnly.FromDateTime(DateTime.Now));
        return date;
    }


    public static DateOnly FindNextEvenDay(DateOnly date)
    {
        do
        {
            if (date.DayOfWeek == DayOfWeek.Wednesday)
                date = date.AddDays(3);

            if (GetDayType(date) == "Even" || date.DayOfWeek == DayOfWeek.Thursday)
                date = date.AddDays(2);
            else
                date = date.AddDays(1);


        } while (date <= DateOnly.FromDateTime(DateTime.Now));
        return date;
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
