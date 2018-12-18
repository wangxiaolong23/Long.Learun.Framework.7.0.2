using Learun.Application.Base.SystemModule;
using Learun.Loger;
using Learun.Util;
using Learun.Util.Operat;
using System;
using System.Web;
using System.Web.Mvc;

namespace Learun.Application.Web
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：错误日志（Controller发生异常时会执行这里） 
    /// </summary>
    public class HandlerErrorAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// 控制器方法中出现异常，会调用该方法捕获异常
        /// </summary>
        /// <param name="context">提供使用</param>
        public override void OnException(ExceptionContext context)
        {
            try
            {
                WriteLog(context);
            }
            catch (Exception)
            {
            }
            base.OnException(context);
            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = 200;
            string msg = "Learun敏捷框架提醒您：" + context.Exception.Message;
            if (msg == "Learun敏捷框架提醒您：所需的防伪表单字段“__RequestVerificationToken”不存在。")
            {
                msg = "系统貌似出问题了，可联系力软官方敏捷框架售后人员。";
            }
            context.Result = new ContentResult { Content = new ResParameter { code = ResponseCode.exception, info = msg }.ToJson() };
        }
        /// <summary>
        /// 写入日志（log4net）
        /// </summary>
        /// <param name="context">提供使用</param>
        private void WriteLog(ExceptionContext context)
        {
            if (context == null)
                return;
            var userInfo = LoginUserInfo.Get();

           
            var log = LogFactory.GetLogger(context.Controller.ToString());
            Exception Error = context.Exception;
            LogMessage logMessage = new LogMessage();
            logMessage.OperationTime = DateTime.Now;
            logMessage.Url = HttpContext.Current.Request.RawUrl;
            logMessage.Class = context.Controller.ToString();
            logMessage.Ip = Net.Ip;
            logMessage.Host = Net.Host;
            logMessage.Browser = Net.Browser;
            if (userInfo != null)
            {
                logMessage.UserName = userInfo.account + "（" + userInfo.realName + "）";
            }

            if (Error.InnerException == null)
            {
                logMessage.ExceptionInfo = Error.Message;
                logMessage.ExceptionSource = Error.Source;
                logMessage.ExceptionRemark = Error.StackTrace;
            }
            else
            {
                logMessage.ExceptionInfo = Error.InnerException.Message;
                logMessage.ExceptionSource = Error.InnerException.Source;
                logMessage.ExceptionRemark = Error.InnerException.StackTrace;
            }
          
            string strMessage = new LogFormat().ExceptionFormat(logMessage);
            log.Error(strMessage);

            LogEntity logEntity = new LogEntity();
            logEntity.F_CategoryId = 4;
            logEntity.F_OperateTypeId = ((int)OperationType.Exception).ToString();
            logEntity.F_OperateType = EnumAttribute.GetDescription(OperationType.Exception);
            logEntity.F_OperateAccount = logMessage.UserName;
            if (userInfo != null)
            {
                logEntity.F_OperateUserId = userInfo.userId;
            }
            logEntity.F_ExecuteResult = -1;
            logEntity.F_ExecuteResultJson = strMessage;
            logEntity.WriteLog();
            SendMail(strMessage);

        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="body">消息</param>
        private void SendMail(string body)
        {
            bool ErrorToMail = Config.GetValue("ErrorToMail").ToBool();
            if (ErrorToMail == true)
            {
                string SystemName = Config.GetValue("SystemName");//系统名称
                string recMail = Config.GetValue("RereceiveErrorMail");//接收错误信息邮箱
                MailHelper.Send("receivebug@learun.cn", SystemName + " - 发生异常", body.Replace("-", ""));
            }
        }
    }
}