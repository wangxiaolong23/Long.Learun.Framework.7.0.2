namespace Learun.Util
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.06
    /// 扩展 - 可空类型
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// 安全返回值
        /// </summary>
        /// <param name="value">可空值</param>
        public static T SafeValue<T>(this T? value) where T : struct
        {
            return value ?? default(T);
        }
        /// <summary>
        /// 是否包含
        /// </summary>
        /// <param name="obj">字串</param>
        /// <param name="value">包含字串</param>
        /// <returns></returns>
        public static bool ContainsEx(this string obj,string value)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return false;
            }
            else {
                return obj.Contains(value);
            }
        }
        /// <summary>
        /// 字串是否在指定字串中存在
        /// </summary>
        /// <param name="obj">字串</param>
        /// <param name="value">被包含字串</param>
        /// <returns></returns>
        public static bool Like(this string obj, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            else if (string.IsNullOrEmpty(obj))
            {
                return false;
            }
            else
            {
                if (value.IndexOf(obj) != -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
