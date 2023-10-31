using MD.PersianDateTime.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PlateDelivery.Core.Convertors
{
    public static class DateConvertor
    {
        public static string ToShamsi(this DateTime value)
        {
            PersianCalendar pc=new PersianCalendar();
            return pc.GetYear(value).ToString() + "/" + pc.GetMonth(value).ToString("00") + "/"+
                   pc.GetDayOfMonth(value).ToString("00") + " ساعت   " + pc.GetHour(value).ToString("00") + ":" + pc.GetMinute(value).ToString("00");

        }

        //برای فیلدهای مربوط به تازیخ با قابلیت نال

        public static string ToShamsi(this DateTime? value)
        {
            PersianCalendar pc = new PersianCalendar();
            if (value != null)
            {
                return pc.GetYear((DateTime)value).ToString() + "/" + pc.GetMonth((DateTime)value).ToString("00") + "/" +
                pc.GetDayOfMonth((DateTime)value).ToString("00") + " ساعت   " + pc.GetHour((DateTime)value).ToString("00") + ":" + pc.GetMinute((DateTime)value).ToString("00");
            }
            return "ارسال نشده";
        }
        public static string ToNewShamsiConvert(this DateTime? value)
        {
            PersianCalendar pc = new PersianCalendar();
            if (value != null)
            {
                return pc.GetYear((DateTime)value).ToString() + "/" + pc.GetMonth((DateTime)value).ToString("00") + "/" +
                pc.GetDayOfMonth((DateTime)value).ToString("00") + " ساعت   " + pc.GetHour((DateTime)value).ToString("00") + ":" + pc.GetMinute((DateTime)value).ToString("00");
            }
            return "ارسال نشده";
        }
        public static string GetShamsiMonthName(string Month)
        {
            string MonthName = string.Empty;
            switch(Month)
            {
                case "1":
                    MonthName = "فروردین";
                    break;
                case "01":
                    MonthName = "فروردین";
                    break;
                case "2":
                    MonthName = "اردیبهشت";
                    break;
                case "02":
                    MonthName = "اردیبهشت";
                    break;
                case "3":
                    MonthName = "خرداد";
                    break;
                case "03":
                    MonthName = "خرداد";
                    break;
                case "4":
                    MonthName = "تیر";
                    break;
                case "04":
                    MonthName = "تیر";
                    break;
                case "5":
                    MonthName = "مرداد";
                    break;
                case "05":
                    MonthName = "مرداد";
                    break;
                case "6":
                    MonthName = "شهریور";
                    break;
                case "06":
                    MonthName = "شهریور";
                    break;
                case "7":
                    MonthName = "مهر";
                    break;
                case "07":
                    MonthName = "مهر";
                    break;
                case "8":
                    MonthName = "آبان";
                    break;
                case "08":
                    MonthName = "آبان";
                    break;
                case "9":
                    MonthName = "آذر";
                    break;
                case "09":
                    MonthName = "آذر";
                    break;
                case "10":
                    MonthName = "دی";
                    break;
                case "11":
                    MonthName = "بهمن";
                    break;
                case "12":
                    MonthName = "اسفند";
                    break;
            }
            return MonthName;
        }

        public static string ToNewShamsi(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(value).ToString() + "/" + pc.GetMonth(value).ToString("00") + "/" +
                   pc.GetDayOfMonth(value).ToString("00");

        }

        public static DateTime ToNewDateShamsi(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            string s = pc.GetYear(value).ToString() + "/" + pc.GetMonth(value).ToString("00") + "/" +
                   pc.GetDayOfMonth(value).ToString("00");
            return DateTime.Parse(s);

        }
        public static string ToNewDateJalali(this DateTime? value)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear((DateTime)value).ToString() + "/" + pc.GetMonth((DateTime)value).ToString("00") + "/" +
                   pc.GetDayOfMonth((DateTime)value).ToString("00");

        }
        public static string ToTotalShamsi(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetMonth(value).ToString("00") + "/" + pc.GetDayOfMonth(value).ToString() + "/" +
                   pc.GetYear(value);

        }

        public static string ConvertMiladiToShamsi(this DateTime date, string Format)
        {
            PersianDateTime persianDateTime = new PersianDateTime(date);
            return persianDateTime.ToString(Format);
        }

        public static string ToShamsiForEdit(this DateTime value)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetMonth(value).ToString() + "/" + pc.GetDayOfMonth(value).ToString("00") + "/" +
                   pc.GetYear(value);

        }

        public static string ToShamsiForEdit(this DateTime? value)
        {
            DateTime date = (DateTime)value;
            PersianCalendar pc = new PersianCalendar();
            return pc.GetMonth(date).ToString() + "/" + pc.GetDayOfMonth(date).ToString("00") + "/" +
                   pc.GetYear(date);

        }

        public static string DTP(DateTime? dtn, bool time = true)
        {
            PersianCalendar pc = new PersianCalendar();
            if (dtn != null)
            {
                DateTime dt = dtn ?? DateTime.Now;
                if (time)
                    return string.Format("{0}/{1}/{2} {3}:{4}:{5}", pc.GetYear(dt), pc.GetMonth(dt), pc.GetDayOfMonth(dt), pc.GetHour(dt), pc.GetMinute(dt), pc.GetSecond(dt));
                else
                    return string.Format("{0}/{1}/{2} ", pc.GetYear(dt), pc.GetMonth(dt), pc.GetDayOfMonth(dt));
            }
            else return " ";
        }

    }
}
