
using Learun.Application.OA.Email.EmailSend;
using Learun.Util;
using System;
using System.Collections.Generic;

namespace Learun.Application.OA.Email
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.06.04
    /// 描 述：邮件管理
    /// </summary>
    public class EmailBLL:EmailIBLL
    {
        private EmailService emailService = new EmailService();

        private EmailSendService emailSendService = new EmailSendService();

        #region 获取数据
        /// <summary>
        /// 获取邮件
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public List<MailModel> GetMail(MailAccount account, int receiveCount)
        {
            try
            {
                return MailHelper.Get(account,receiveCount);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="account">邮箱账户</param>
        /// <param name="mailModel">邮箱类</param>
        /// <returns></returns>
        public void SendMail(MailAccount account, MailModel mailModel)
        {
            try
            {
                MailHelper.Send(account, mailModel);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }


        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="account">邮箱账户</param>
        /// <param name="UID">UID</param>
        /// <returns></returns>
        public void DeleteMail(MailAccount account, string UID)
        {
            try
            {
                MailHelper.Delete(account, UID);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        #endregion

        #region 提交数据

        #endregion
    }
}