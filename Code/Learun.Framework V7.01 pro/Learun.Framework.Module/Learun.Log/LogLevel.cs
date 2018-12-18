using System.ComponentModel;
namespace Learun.Loger
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.04
    /// 描 述：日志级别
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 错误
        /// </summary>
        [Description("错误")]
        Error,
        /// <summary>
        /// 警告
        /// </summary>
        [Description("警告")]
        Warning,
        /// <summary>
        /// 信息
        /// </summary>
        [Description("信息")]
        Info,
        /// <summary>
        /// 调试
        /// </summary>
        [Description("调试")]
        Debug
    }
}
