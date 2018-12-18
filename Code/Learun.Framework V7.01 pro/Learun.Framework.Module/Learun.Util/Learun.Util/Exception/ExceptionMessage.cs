using System;
using System.Text;

namespace Learun.Util
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.04
    /// 描 述：异常信息封装类
    /// </summary>
    public class ExceptionMessage
    {
        #region 构造函数

        /// <summary>
        ///以自定义用户信息和异常对象实例化一个异常信息对象
        /// </summary>
        /// <param name="e"> 异常对象 </param>
        /// <param name="userMessage"> 自定义用户信息 </param>
        /// <param name="isHideStackTrace"> 是否隐藏异常堆栈信息 </param>
        public ExceptionMessage(Exception e, string userMessage = null, bool isHideStackTrace = false)
        {
            UserMessage = string.IsNullOrEmpty(userMessage) ? e.Message : userMessage;

            StringBuilder sb = new StringBuilder();
            ExMessage = string.Empty;
            int count = 0;
            string appString = "";
            while (e != null)
            {
                if (count > 0)
                {
                    appString += "　";
                }
                ExMessage = e.Message;
                sb.AppendLine(appString + "异常消息：" + e.Message);
                sb.AppendLine(appString + "异常类型：" + e.GetType().FullName);
                sb.AppendLine(appString + "异常方法：" + (e.TargetSite == null ? null : e.TargetSite.Name));
                sb.AppendLine(appString + "异常源：" + e.Source);
                if (!isHideStackTrace && e.StackTrace != null)
                {
                    sb.AppendLine(appString + "异常堆栈：" + e.StackTrace);
                }
                if (e.InnerException != null)
                {
                    sb.AppendLine(appString + "内部异常：");
                    count++;
                }
                e = e.InnerException;
            }
            ErrorDetails = sb.ToString();
            sb.Clear();
        }

        #region 属性

        /// <summary>
        ///用户信息，用于报告给用户的异常消息
        /// </summary>
        public string UserMessage { get; set; }
        /// <summary>
        ///直接的Exception异常信息，即e.Message属性值
        /// </summary>
        public string ExMessage { get; private set; }
        /// <summary>
        ///异常输出的详细描述，包含异常消息，规模信息，异常类型，异常源，引发异常的方法及内部异常信息
        /// </summary>
        public string ErrorDetails { get; private set; }
        #endregion

        #endregion
    }
}
