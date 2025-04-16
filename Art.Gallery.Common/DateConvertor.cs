using System.Globalization;
namespace Art.Gallery.Common;
public static class PersianDate
{
    public static DateTime ToMiladi(this DateTime dateTime)
    {
        PersianCalendar pc = new PersianCalendar();
        return pc.ToDateTime(dateTime.Year, dateTime.Month, dateTime.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, 0);
    }

    public static string ToShamsiDate(this DateTime dt)
    {
        PersianCalendar PC = new PersianCalendar();
        string hour = dt.Hour + ":" + dt.Minute + ":" + dt.Second;
        int intYear = PC.GetYear(dt);
        int intMonth = PC.GetMonth(dt);
        int intDayOfMonth = PC.GetDayOfMonth(dt);
        DayOfWeek enDayOfWeek = PC.GetDayOfWeek(dt);
        string strMonthName = "";
        string strDayName = "";

        switch (intMonth)
        {
            case 1:
                strMonthName = "فروردین";
                break;
            case 2:
                strMonthName = "اردیبهشت";
                break;
            case 3:
                strMonthName = "خرداد";
                break;
            case 4:
                strMonthName = "تیر";
                break;
            case 5:
                strMonthName = "مرداد";
                break;
            case 6:
                strMonthName = "شهریور";
                break;
            case 7:
                strMonthName = "مهر";
                break;
            case 8:
                strMonthName = "آبان";
                break;
            case 9:
                strMonthName = "آذر";
                break;
            case 10:
                strMonthName = "دی";
                break;
            case 11:
                strMonthName = "بهمن";
                break;
            case 12:
                strMonthName = "اسفند";
                break;
            default:
                strMonthName = "";
                break;
        }

        switch (enDayOfWeek)
        {
            case DayOfWeek.Friday:
                strDayName = "جمعه";
                break;
            case DayOfWeek.Monday:
                strDayName = "دوشنبه";
                break;
            case DayOfWeek.Saturday:
                strDayName = "شنبه";
                break;
            case DayOfWeek.Sunday:
                strDayName = "یکشنبه";
                break;
            case DayOfWeek.Thursday:
                strDayName = "پنجشنبه";
                break;
            case DayOfWeek.Tuesday:
                strDayName = "سه شنبه";
                break;
            case DayOfWeek.Wednesday:
                strDayName = "چهارشنبه";
                break;
            default:
                strDayName = "";
                break;
        }

        return (string.Format("{0} {1} {2} {3} {4}", strDayName, intDayOfMonth, strMonthName, intYear, hour));
    }

    public static string ToHourDate(this DateTime dt)
    {
        string hour = dt.Hour + ":" + dt.Minute + ":" + dt.Second;
        return (string.Format("{0}", hour));
    }

    public static string ToStringShamsiDate(this DateTime dt)
    {
        try
        {
            PersianCalendar PC = new PersianCalendar();
            int intYear = PC.GetYear(dt);
            int intMonth = PC.GetMonth(dt);
            int intDayOfMonth = PC.GetDayOfMonth(dt);
            DayOfWeek enDayOfWeek = PC.GetDayOfWeek(dt);
            string strMonthName = "";
            string strDayName = "";
            switch (intMonth)
            {
                case 1:
                    strMonthName = "فروردین";
                    break;
                case 2:
                    strMonthName = "اردیبهشت";
                    break;
                case 3:
                    strMonthName = "خرداد";
                    break;
                case 4:
                    strMonthName = "تیر";
                    break;
                case 5:
                    strMonthName = "مرداد";
                    break;
                case 6:
                    strMonthName = "شهریور";
                    break;
                case 7:
                    strMonthName = "مهر";
                    break;
                case 8:
                    strMonthName = "آبان";
                    break;
                case 9:
                    strMonthName = "آذر";
                    break;
                case 10:
                    strMonthName = "دی";
                    break;
                case 11:
                    strMonthName = "بهمن";
                    break;
                case 12:
                    strMonthName = "اسفند";
                    break;
                default:
                    strMonthName = "";
                    break;
            }
            switch (enDayOfWeek)
            {
                case DayOfWeek.Friday:
                    strDayName = "جمعه";
                    break;
                case DayOfWeek.Monday:
                    strDayName = "دوشنبه";
                    break;
                case DayOfWeek.Saturday:
                    strDayName = "شنبه";
                    break;
                case DayOfWeek.Sunday:
                    strDayName = "یکشنبه";
                    break;
                case DayOfWeek.Thursday:
                    strDayName = "پنجشنبه";
                    break;
                case DayOfWeek.Tuesday:
                    strDayName = "سه شنبه";
                    break;
                case DayOfWeek.Wednesday:
                    strDayName = "چهارشنبه";
                    break;
                default:
                    strDayName = "";
                    break;
            }
            return (string.Format("{0} {1} {2} {3}", strDayName, intDayOfMonth, strMonthName, intYear));
        }
        catch (Exception)
        {
            return dt.ToString();
            throw;
        }
    }
    public static string GetPersianNumbers(this string s)
    {
        return s.Replace("0", "۰").Replace("1", "۱").Replace("2", "۲").Replace("3", "۳").Replace("4", "۴").Replace("5", "۵").Replace("6", "۶").Replace("7", "۷").Replace("8", "۸").Replace("9", "۹");
    }
    public static string GetEnglishNumbers(this string s)
    {
        return s.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("۷", "7").Replace("۸", "8").Replace("۹", "9");
    }
    public static DateTime ToMiladiDateTime(this string ts)
    {
        var spliteDate = ts.GetEnglishNumbers().Split('/');
        int year = int.Parse(spliteDate[0]);
        int month = int.Parse(spliteDate[1]);
        int day = int.Parse(spliteDate[2]);
        DateTime currentDate = new DateTime(year, month, day, new PersianCalendar());
        return currentDate;
    }
}