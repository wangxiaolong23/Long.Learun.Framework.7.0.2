using System;
using System.Globalization;

namespace Learun.Util
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.04
    /// 描 述：时间操作
    /// </summary>
    public class Time
    {

        #region 当前时间
        /// <summary>
        /// 当前时间
        /// </summary>
        public static DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }
        /// <summary>
        /// 短日期格式(yyyy/MM/dd)
        /// </summary>
        public static string ShortDate
        {
            get
            {
                return Now.ToString("yyyy/MM/dd");
            }
        }
        /// <summary>
        /// 长日期格式(yyyy月MM日dd日)
        /// </summary>
        public static string LongDate
        {
            get
            {
                return Now.ToString("yyyy月MM日dd日");
            }
        }
        /// <summary>
        /// 日期时间(yyyy/MM/dd HH:mm)
        /// </summary>
        public static string ShortDateTime
        {
            get
            {
                return Now.ToString("yyyy/MM/dd HH:mm");
            }
        }
        /// <summary>
        /// 日期时间(yyyy年MM月dd日 HH时mm分)
        /// </summary>
        public static string LongDateTime
        {
            get
            {
                return Now.ToString("yyyy年MM月dd日 HH时mm分");
            }
        }

        /// <summary>
        /// 日期时间(yyyy年MM月dd日 HH时mm分)
        /// </summary>
        public static string LongTime
        {
            get
            {
                return Now.ToString("HH时mm分");
            }
        }

        /// <summary>
        /// 日期时间(yyyy年MM月dd日 HH时mm分)
        /// </summary>
        public static string ShortTime
        {
            get
            {
                return Now.ToString("HH:mm");
            }
        }
        /// <summary>
        /// 当前日期
        /// </summary>
        /// <returns></returns>
        public static string GetToday()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 当前日期自定义格式
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetToday(string format)
        {
            return DateTime.Now.ToString(format);
        }
        /// <summary>
        /// 当前日期 加添加，减天数 -1、1
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string GetDate(int i)
        {
            DateTime dt = DateTime.Now;
            dt = dt.AddDays(i);
            return dt.ToString("yyyy-MM-dd");
        }
        /// <summary>
        ///  当前日期 加添加，减天数 -1、1
        /// </summary>
        /// <param name="i"></param>
        /// <param name="dateformat">返回的时间格式</param>
        /// <returns></returns>
        public static string GetDate(int i, string dateformat)
        {
            DateTime dt = DateTime.Now;
            dt = dt.AddDays(i);
            return dt.ToString(dateformat);
        }
        #endregion

        #region 返回本年有多少天

        /// <summary>返回本年有多少天</summary>
        /// <param name="iYear">年份</param>
        /// <returns>本年的天数</returns>
        public static int GetDaysOfYear(int iYear)
        {
            return IsRuYear(iYear) ? 366 : 365;
        }

        /// <summary>本年有多少天</summary>
        /// <param name="dt">日期</param>
        /// <returns>本天在当年的天数</returns>
        public static int GetDaysOfYear(DateTime dt)
        {
            return IsRuYear(dt.Year) ? 366 : 365;
        }

        #endregion

        #region 返回本月有多少天
        /// <summary>本月有多少天</summary>
        /// <param name="iYear">年</param>
        /// <param name="Month">月</param>
        /// <returns>天数</returns>
        public static int GetDaysOfMonth(int iYear, int Month)
        {
            var days = 0;
            switch (Month)
            {
                case 1:
                    days = 31;
                    break;
                case 2:
                    days = IsRuYear(iYear) ? 29 : 28;
                    break;
                case 3:
                    days = 31;
                    break;
                case 4:
                    days = 30;
                    break;
                case 5:
                    days = 31;
                    break;
                case 6:
                    days = 30;
                    break;
                case 7:
                    days = 31;
                    break;
                case 8:
                    days = 31;
                    break;
                case 9:
                    days = 30;
                    break;
                case 10:
                    days = 31;
                    break;
                case 11:
                    days = 30;
                    break;
                case 12:
                    days = 31;
                    break;
            }

            return days;
        }


        /// <summary>本月有多少天</summary>
        /// <param name="dt">日期</param>
        /// <returns>天数</returns>
        public static int GetDaysOfMonth(DateTime dt)
        {
            //--------------------------------//
            //--从dt中取得当前的年，月信息  --//
            //--------------------------------//
            int days = 0;
            int year = dt.Year;
            int month = dt.Month;

            //--利用年月信息，得到当前月的天数信息。
            switch (month)
            {
                case 1:
                    days = 31;
                    break;
                case 2:
                    days = IsRuYear(year) ? 29 : 28;
                    break;
                case 3:
                    days = 31;
                    break;
                case 4:
                    days = 30;
                    break;
                case 5:
                    days = 31;
                    break;
                case 6:
                    days = 30;
                    break;
                case 7:
                    days = 31;
                    break;
                case 8:
                    days = 31;
                    break;
                case 9:
                    days = 30;
                    break;
                case 10:
                    days = 31;
                    break;
                case 11:
                    days = 30;
                    break;
                case 12:
                    days = 31;
                    break;
            }
            return days;
        }
        #endregion

        #region 返回当前日期的 （星期名称or星期编号）
        /// <summary>返回当前日期的星期名称</summary>
        /// <param name="dt">日期</param>
        /// <returns>星期名称</returns>
        public static string GetWeekNameOfDay(DateTime dt)
        {
            string week = string.Empty;
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    week = "星期一";
                    break;
                case DayOfWeek.Tuesday:
                    week = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    week = "星期三";
                    break;
                case DayOfWeek.Thursday:
                    week = "星期四";
                    break;
                case DayOfWeek.Friday:
                    week = "星期五";
                    break;
                case DayOfWeek.Saturday:
                    week = "星期六";
                    break;
                case DayOfWeek.Sunday:
                    week = "星期日";
                    break;
            }
            return week;
        }


        /// <summary>返回当前日期的星期编号</summary>
        /// <param name="dt">日期</param>
        /// <returns>星期数字编号</returns>
        public static int GetWeekNumberOfDay(DateTime dt)
        {
            int week = 0;
            switch (dt.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    week = 1;
                    break;
                case DayOfWeek.Tuesday:
                    week = 2;
                    break;
                case DayOfWeek.Wednesday:
                    week = 3;
                    break;
                case DayOfWeek.Thursday:
                    week = 4;
                    break;
                case DayOfWeek.Friday:
                    week = 5;
                    break;
                case DayOfWeek.Saturday:
                    week = 6;
                    break;
                case DayOfWeek.Sunday:
                    week = 7;
                    break;
            }
            return week;
        }
        #endregion

        #region 获取某一年有多少周
        /// <summary>
        /// 获取某一年有多少周
        /// </summary>
        /// <param name="year">年份</param>
        /// <returns>该年周数</returns>
        public static int GetWeekAmount(int year)
        {
            var end = new DateTime(year, 12, 31); //该年最后一天
            var gc = new GregorianCalendar();
            return gc.GetWeekOfYear(end, CalendarWeekRule.FirstDay, DayOfWeek.Monday); //该年星期数
        }
        #endregion

        #region 获取某一日期是该年中的第几周
        /// <summary>
        /// 获取某一日期是该年中的第几周
        /// </summary>
        /// <param name="dt">日期</param>
        /// <returns>该日期在该年中的周数</returns>
        public static int GetWeekOfYear(DateTime dt)
        {
            var gc = new GregorianCalendar();
            return gc.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }
        #endregion

        #region 根据某年的第几周获取这周的起止日期
        /// <summary>
        /// 根据某年的第几周获取这周的起止日期
        /// </summary>
        /// <param name="year"></param>
        /// <param name="weekOrder"></param>
        /// <param name="firstDate"></param>
        /// <param name="lastDate"></param>
        /// <returns></returns>
        public static void WeekRange(int year, int weekOrder, ref DateTime firstDate, ref DateTime lastDate)
        {
            //当年的第一天
            var firstDay = new DateTime(year, 1, 1);

            //当年的第一天是星期几
            int firstOfWeek = Convert.ToInt32(firstDay.DayOfWeek);

            //计算当年第一周的起止日期，可能跨年
            int dayDiff = (-1) * firstOfWeek + 1;
            int dayAdd = 7 - firstOfWeek;

            firstDate = firstDay.AddDays(dayDiff).Date;
            lastDate = firstDay.AddDays(dayAdd).Date;

            //如果不是要求计算第一周
            if (weekOrder != 1)
            {
                int addDays = (weekOrder - 1) * 7;
                firstDate = firstDate.AddDays(addDays);
                lastDate = lastDate.AddDays(addDays);
            }
        }
        #endregion

        #region 返回两个日期之间相差的天数
        /// <summary>
        /// 返回两个日期之间相差的天数
        /// </summary>
        /// <param name="dtfrm">两个日期参数</param>
        /// <param name="dtto">两个日期参数</param>
        /// <returns>天数</returns>
        public static int DiffDays(DateTime dtfrm, DateTime dtto)
        {
            TimeSpan tsDiffer = dtto.Date - dtfrm.Date;
            return tsDiffer.Days;
        }
        #endregion

        #region 返回两个日期之间相差的小时数
        /// <summary>
        /// 返回两个日期之间相差的小时数
        /// </summary>
        /// <param name="dtfrm">两个日期参数</param>
        /// <param name="dtto">两个日期参数</param>
        /// <returns>小时数</returns>
        public static int DiffHours(DateTime dtfrm, DateTime dtto)
        {
            TimeSpan tsDiffer = dtto.Date - dtfrm.Date;
            return tsDiffer.TotalHours.ToInt();
        }
        #endregion

        #region 返回两个日期之间相差的分钟数
        /// <summary>
        /// 返回两个日期之间相差的分钟数
        /// </summary>
        /// <param name="dtfrm">两个日期参数</param>
        /// <param name="dtto">两个日期参数</param>
        /// <returns>分钟数</returns>
        public static int DiffMinutes(DateTime dtfrm, DateTime dtto)
        {
            TimeSpan tsDiffer = dtto.Date - dtfrm.Date;
            return tsDiffer.TotalMinutes.ToInt();
        }
        #endregion

        #region 判断当前年份是否是闰年
        /// <summary>判断当前年份是否是闰年，私有函数</summary>
        /// <param name="iYear">年份</param>
        /// <returns>是闰年：True ，不是闰年：False</returns>
        private static bool IsRuYear(int iYear)
        {
            //形式参数为年份
            //例如：2003
            int n = iYear;
            return (n % 400 == 0) || (n % 4 == 0 && n % 100 != 0);
        }
        #endregion

        #region 将输入的字符串转化为日期。如果字符串的格式非法，则返回当前日期
        /// <summary>
        /// 将输入的字符串转化为日期。如果字符串的格式非法，则返回当前日期。
        /// </summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>日期对象</returns>
        public static DateTime ToDate(string strInput)
        {
            DateTime oDateTime;

            try
            {
                oDateTime = DateTime.Parse(strInput);
            }
            catch (Exception)
            {
                oDateTime = DateTime.Today;
            }

            return oDateTime;
        }
        #endregion

        #region 将日期对象转化为格式字符串
        /// <summary>
        /// 将日期对象转化为格式字符串
        /// </summary>
        /// <param name="oDateTime">日期对象</param>
        /// <param name="strFormat">
        /// 格式：
        ///		"SHORTDATE"===短日期
        ///		"LONGDATE"==长日期
        ///		其它====自定义格式
        /// </param>
        /// <returns>日期字符串</returns>
        public static string ToString(DateTime oDateTime, string strFormat)
        {
            string strDate;

            try
            {
                switch (strFormat.ToUpper())
                {
                    case "SHORTDATE":
                        strDate = oDateTime.ToShortDateString();
                        break;
                    case "LONGDATE":
                        strDate = oDateTime.ToLongDateString();
                        break;
                    default:
                        strDate = oDateTime.ToString(strFormat);
                        break;
                }
            }
            catch (Exception)
            {
                strDate = oDateTime.ToShortDateString();
            }

            return strDate;
        }
        #endregion
        /// <summary>
        /// 获取当前日期是星期几
        /// </summary>
        /// <param name="dt">时间对象</param>
        /// <returns></returns>
        public static int GetNumberWeekDay(DateTime dt)
        {
            return (int)dt.DayOfWeek;
        }
        /// <summary>
        /// 获取中式星期信息
        /// </summary>
        /// <param name="y">年</param>
        /// <param name="m">月</param>
        /// <param name="d">日</param>
        /// <returns></returns>
        public static string GetChineseWeekDay(int y, int m, int d)
        {
            string[] weekstr = { "日", "一", "二", "三", "四", "五", "六" };
            if (m < 3)
            {
                m += 12;
                y--;
            }
            if (y % 400 == 0 || y % 100 != 0 && y % 4 == 0)
                d--;
            else
                d += 1;
            return "星期" + weekstr[(d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7];
        }
        /// <summary>
        /// 获取中国时间
        /// </summary>
        /// <param name="dt">时间对象</param>
        /// <returns></returns>
        public static string GetChineseDateTime(DateTime dt)
        {
            string dateTime = dt.ToString("yyyy-MM-dd");
            dateTime = dateTime + string.Format("({0})", GetChineseWeekDay(dt.Year, dt.Month, dt.Day));
            string time = string.Format("{0:  tt hh:mm:ss }", dt);
            time = time.Replace("am", "上午").Replace("pm", "下午");
            dateTime = dateTime + time;
            return dateTime;
        }
    }
}
