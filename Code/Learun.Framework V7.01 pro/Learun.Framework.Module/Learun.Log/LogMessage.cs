using System;
namespace Learun.Loger
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.04
    /// 描 述：log日志消息模型类
    /// </summary>
    public class LogMessage
    {
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationTime { get; set; }
        /// <summary>
        /// Url地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 类名
        /// </summary>
        public string Class { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 主机
        /// </summary>
        public string Host { get; set; }
        /// <summary>
        /// 浏览器
        /// </summary>
        public string Browser { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        public string ExceptionInfo { get; set; }
        /// <summary>
        /// 异常来源
        /// </summary>
        public string ExceptionSource { get; set; }
        /// <summary>
        /// 异常信息备注
        /// </summary>
        public string ExceptionRemark { get; set; }
    }
}
