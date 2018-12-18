using Learun.Application.OA.Email;
using Learun.Application.OA.Email.EmailConfig;
using Learun.Application.OA.Email.EmailReceive;
using Learun.Application.OA.Email.EmailSend;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_OAModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.06.04
    /// 描 述：邮件管理
    /// </summary>
    public class EmailController : MvcControllerBase
    {
        private EmailIBLL emailIBLL = new EmailBLL();
        private EmailConfigIBLL emailConfigIBLL = new EmailConfigBLL();
        private EmailReceiveIBLL emailReceiveIBLL = new EmailReceiveBLL();
        private EmailSendIBLL emailSendIBLL = new EmailSendBLL();

        #region 视图功能
        /// <summary>
        /// 管理页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 写邮件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 收件详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DetailForm()
        {
            return View();
        }

        /// <summary>
        /// 配置信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ConfigForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取发送邮件数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">关键词</param>
        /// <returns></returns>
        public ActionResult GetSendList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = emailSendIBLL.GetSendList(paginationobj, queryJson);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records,
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 获取收取邮件数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">关键词</param>
        /// <returns></returns>
        public ActionResult GetReceiveList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = emailReceiveIBLL.GetReceiveList(paginationobj, queryJson);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records,
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="queryJson">关键词</param>
        /// <returns></returns>
        public ActionResult GetConfigList(string queryJson)
        {
            var data = emailConfigIBLL.GetConfigList(queryJson);
            return Success(data);
        }

        /// <summary>
        /// 获取邮件发送实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public ActionResult GetSendEntity(string keyValue)
        {
            var data = emailSendIBLL.GetSendEntity(keyValue);
            return Success(data);
        }

        /// <summary>
        /// 获取邮件接收实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public ActionResult GetReceiveEntity(string keyValue)
        {
            var data = emailReceiveIBLL.GetReceiveEntity(keyValue);
            return Success(data);
        }

        /// <summary>
        /// 获取邮件配置实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public ActionResult GetConfigEntity(string keyValue)
        {
            var data = emailConfigIBLL.GetConfigEntity(keyValue);
            return Success(data);
        }


        /// <summary>
        /// 获取邮件
        /// </summary>
        /// <param name="receiveCount">主键</param>
        /// <returns></returns>
        public ActionResult GetMail()
        {
            EmailConfigEntity entity = emailConfigIBLL.GetCurrentConfig();
            MailAccount account = new MailAccount();
            account.POP3Host = entity.F_POP3Host;
            account.POP3Port = entity.F_POP3Port.ToInt();
            account.SMTPHost = entity.F_SMTPHost;
            account.SMTPPort = entity.F_SMTPPort.ToInt();
            account.Account = entity.F_Account;
            account.AccountName = entity.F_SenderName;
            account.Password = entity.F_Password;
            account.Ssl = entity.F_Ssl == 1 ? true : false;

            var receiveCount = emailReceiveIBLL.GetCount();
            List<MailModel> data = emailIBLL.GetMail(account, receiveCount);
            for (var i = 0; i < data.Count; i++)
            {
                EmailReceiveEntity receiveEntity = new EmailReceiveEntity();
                receiveEntity.F_Sender = data[i].To;
                receiveEntity.F_SenderName = data[i].ToName;
                receiveEntity.F_MID = data[i].UID;
                receiveEntity.F_Subject = data[i].Subject;
                receiveEntity.F_BodyText = data[i].BodyText;
                //receiveEntity.Attachment = data[i].Attachment;
                receiveEntity.F_Date = data[i].Date;
                emailReceiveIBLL.SaveReceiveEntity("", receiveEntity);
            }
            return Success(data);
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="receiveCount">主键</param>
        /// <returns></returns>
        public ActionResult SendMail(EmailSendEntity entity)
        {
            EmailConfigEntity configEntity = emailConfigIBLL.GetCurrentConfig();
            MailAccount account = new MailAccount();
            account.POP3Host = configEntity.F_POP3Host;
            account.POP3Port = configEntity.F_POP3Port.ToInt();
            account.SMTPHost = configEntity.F_SMTPHost;
            account.SMTPPort = configEntity.F_SMTPPort.ToInt();
            account.Account = configEntity.F_Account;
            account.AccountName = configEntity.F_SenderName;
            account.Password = configEntity.F_Password;
            account.Ssl = configEntity.F_Ssl == 1 ? true : false;
            MailModel model = new MailModel();
            model.UID = Guid.NewGuid().ToString();
            entity.F_Id = model.UID;
            model.To = entity.F_To;
            //model.ToName = entity.F_To;
            model.CC = entity.F_CC;
            //model.CCName = entity.F_CC;
            model.Bcc = entity.F_BCC;
            //model.BccName = entity.F_BCC;
            model.Subject = entity.F_Subject;
            model.BodyText = entity.F_BodyText;
            //model.Attachment = entity.F_Attachment;
            model.Date = entity.F_Date.ToDate();
            emailIBLL.SendMail(account, model);

            entity.F_Sender = configEntity.F_Account;
            entity.F_SenderName = configEntity.F_SenderName;
            emailSendIBLL.SaveSendEntity("", entity);
            return Success("发送成功");
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="sendEntity">邮件发送实体</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken, AjaxOnly, ValidateInput(false)]
        public ActionResult SaveSendEntity(string keyValue, EmailSendEntity sendEntity)
        {
            emailSendIBLL.SaveSendEntity(keyValue, sendEntity);
            return Success("保存成功！");
        }

        /// <summary>
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="receiveEntity">邮件接收实体</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken, AjaxOnly, ValidateInput(false)]
        public ActionResult SaveReceiveEntity(string keyValue, EmailReceiveEntity receiveEntity)
        {
            emailReceiveIBLL.SaveReceiveEntity(keyValue, receiveEntity);
            return Success("保存成功！");
        }

        /// <summary>
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="configEntity">邮件配置实体</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken, AjaxOnly, ValidateInput(false)]
        public ActionResult SaveConfigEntity(string keyValue, EmailConfigEntity configEntity)
        {
            emailConfigIBLL.SaveConfigEntity(keyValue, configEntity);
            return Success("保存成功！");
        }

        /// <summary>
        /// 删除表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteForm(string keyValue, string type)
        {
            EmailConfigEntity configEntity = emailConfigIBLL.GetCurrentConfig();
            MailAccount account = new MailAccount();
            account.POP3Host = configEntity.F_POP3Host;
            account.POP3Port = configEntity.F_POP3Port.ToInt();
            account.SMTPHost = configEntity.F_SMTPHost;
            account.SMTPPort = configEntity.F_SMTPPort.ToInt();
            account.Account = configEntity.F_Account;
            account.AccountName = configEntity.F_SenderName;
            account.Password = configEntity.F_Password;
            account.Ssl = configEntity.F_Ssl == 1 ? true : false;
            if (type == "1")
            {
                //emailIBLL.DeleteMail(account, keyValue);
                emailSendIBLL.DeleteEntity(keyValue);
            }
            else
            {
                var entity = emailReceiveIBLL.GetReceiveEntity(keyValue);
                emailIBLL.DeleteMail(account, entity.F_MID);
                emailReceiveIBLL.DeleteEntity(keyValue);
            }
            return Success("删除成功！");
        }
        #endregion
    }
}