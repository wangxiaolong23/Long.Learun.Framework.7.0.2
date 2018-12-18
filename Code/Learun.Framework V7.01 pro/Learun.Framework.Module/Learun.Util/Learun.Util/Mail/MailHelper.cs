#pragma warning disable 0618
using LumiSoft.Net.Mail;
using LumiSoft.Net.MIME;
using LumiSoft.Net.POP3.Client;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;

namespace Learun.Util
{
    /// <summary>
    /// 邮件收发组件
    /// </summary>
    public class MailHelper
    {
        /// <summary>
        /// 邮件服务器地址
        /// </summary>
        private static string MailServer = Config.GetValue("MailHost");
        /// <summary>
        /// 用户名
        /// </summary>
        private static string MailUserName = Config.GetValue("MailUserName");
        /// <summary>
        /// 密码
        /// </summary>
        private static string MailPassword = Config.GetValue("MailPassword");
        /// <summary>
        /// 名称
        /// </summary>
        private static string MailName = Config.GetValue("MailName");
        /// <summary>
        /// 同步发送邮件
        /// </summary>
        /// <param name="to">收件人邮箱地址</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="encoding">编码</param>
        /// <param name="isBodyHtml">是否Html</param>
        /// <param name="enableSsl">是否SSL加密连接</param>
        /// <returns>是否成功</returns>
        public static bool Send(string to, string subject, string body, string encoding = "UTF-8", bool isBodyHtml = true, bool enableSsl = false)
        {
            try
            {
                MailMessage message = new MailMessage();
                // 接收人邮箱地址
                message.To.Add(new MailAddress(to));
                message.From = new MailAddress(MailUserName, MailName);
                message.BodyEncoding = Encoding.GetEncoding(encoding);
                message.Body = body;
                //GB2312
                message.SubjectEncoding = Encoding.GetEncoding(encoding);
                message.Subject = subject;
                message.IsBodyHtml = isBodyHtml;

                SmtpClient smtpclient = new SmtpClient(MailServer, 25);
                smtpclient.Credentials = new System.Net.NetworkCredential(MailUserName, MailPassword);
                //SSL连接
                smtpclient.EnableSsl = enableSsl;
                smtpclient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 异步发送邮件 独立线程
        /// </summary>
        /// <param name="to">邮件接收人</param>
        /// <param name="title">邮件标题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="port">端口号</param>
        /// <returns></returns>
        public static void SendByThread(string to, string title, string body, int port = 25)
        {
            new Thread(new ThreadStart(delegate ()
            {
                try
                {
                    SmtpClient smtp = new SmtpClient();
                    //邮箱的smtp地址
                    smtp.Host = MailServer;
                    //端口号
                    smtp.Port = port;
                    //构建发件人的身份凭据类
                    smtp.Credentials = new NetworkCredential(MailUserName, MailPassword);
                    //构建消息类
                    MailMessage objMailMessage = new MailMessage();
                    //设置优先级
                    objMailMessage.Priority = MailPriority.High;
                    //消息发送人
                    objMailMessage.From = new MailAddress(MailUserName, "提醒", System.Text.Encoding.UTF8);
                    //收件人
                    objMailMessage.To.Add(to);
                    //标题
                    objMailMessage.Subject = title.Trim();
                    //标题字符编码
                    objMailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
                    //正文
                    objMailMessage.Body = body.Trim();
                    objMailMessage.IsBodyHtml = true;
                    //内容字符编码
                    objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                    //发送
                    smtp.Send(objMailMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            })).Start();
        }
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="account">配置</param>
        /// <param name="mailModel">信息</param>
        public static void Send(MailAccount account, MailModel mailModel)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(account.Account, account.AccountName);
                //发件人
                if (!string.IsNullOrEmpty(mailModel.To))
                {
                    var ToArray = mailModel.To.Split(',');
                    foreach (var item in ToArray)
                    {
                        mailMessage.To.Add(new MailAddress(item));
                    }
                }
                //抄送人
                if (!string.IsNullOrEmpty(mailModel.CC))
                {
                    var CCArray = mailModel.CC.Split(',');
                    foreach (var item in CCArray)
                    {
                        mailMessage.CC.Add(new MailAddress(item));
                    }
                }
                //密送人
                if (!string.IsNullOrEmpty(mailModel.Bcc))
                {
                    var BccArray = mailModel.Bcc.Split(',');
                    foreach (var item in BccArray)
                    {
                        mailMessage.Bcc.Add(new MailAddress(item));
                    }
                }
                //附件
                //var filePath = DirFileHelper.GetAbsolutePath("~/Resource/EmailFile/");
                //foreach (MailFile item in mailModel.Attachment)
                //{
                //    var attachment = new Attachment(filePath + item.FileId);
                //    attachment.Name = item.FileName;
                //    mailMessage.Attachments.Add(attachment);
                //}
                mailMessage.Subject = mailModel.Subject;
                mailMessage.Body = mailModel.BodyText;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.Normal;
                mailMessage.SubjectEncoding = Encoding.UTF8;
                mailMessage.BodyEncoding = Encoding.UTF8;
                //不被当作垃圾邮件的关键代码--Begin            
                mailMessage.Headers.Add("X-Priority", "3");
                mailMessage.Headers.Add("X-MSMail-Priority", "Normal");
                mailMessage.Headers.Add("X-Mailer", "Microsoft Outlook Express 6.00.2900.2869");
                mailMessage.Headers.Add("X-MimeOLE", "Produced By Microsoft MimeOLE V6.00.2900.2869");
                mailMessage.Headers.Add("ReturnReceipt", "1");
                //不被当作垃圾邮件的关键代码--End        
                using (SmtpClient smtpClient = new SmtpClient(account.SMTPHost, account.SMTPPort))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(account.Account, account.Password);
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="account">配置</param>
        /// <param name="UID">UID</param>
        public static void Delete(MailAccount account, string UID)
        {
            try
            {
                using (POP3_Client pop3Client = new POP3_Client())
                {
                    pop3Client.Connect(account.POP3Host, account.POP3Port, false);
                    pop3Client.Login(account.Account, account.Password);
                    if (pop3Client.Messages.Count > 0)
                    {
                        foreach (POP3_ClientMessage messages in pop3Client.Messages)
                        {
                            if (messages.UID == UID)
                            {
                                messages.MarkForDeletion();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="account">配置</param>
        /// <param name="receiveCount">已收邮件数、注意：如果已收邮件数和邮件数量一致则不获取</param>
        /// <returns></returns>
        public static List<MailModel> Get(MailAccount account, int receiveCount)
        {
            try
            {
                var filePath = DirFileHelper.GetAbsolutePath("~/Resource/EmailFile/");
                var resultList = new List<MailModel>();
                using (POP3_Client pop3Client = new POP3_Client())
                {
                    pop3Client.Connect(account.POP3Host, account.POP3Port, account.Ssl);
                    pop3Client.Login(account.Account, account.Password);
                    var messages = pop3Client.Messages;
                    if (receiveCount == messages.Count)
                    {
                        return resultList;
                    }
                    for (int i = messages.Count - 1; receiveCount <= i; i--)
                    {
                        var messageItem = messages[i];
                        var messageHeader = Mail_Message.ParseFromByte(messageItem.MessageToByte());
                        resultList.Add(new MailModel()
                        {
                            UID = messageItem.UID,
                            To = messageHeader.From == null ? "" : messageHeader.From[0].Address,
                            ToName = messageHeader.From == null ? "" : messageHeader.From[0].DisplayName,
                            Subject = messageHeader.Subject,
                            BodyText = messageHeader.BodyHtmlText,
                            Attachment = GetFile(filePath, messageHeader.GetAttachments(true, true), messageItem.UID),
                            Date = messageHeader.Date,
                        });
                    }
                }
                return resultList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 解析附件并且下载到本地目录
        /// </summary>
        /// <param name="filePath">路径</param>
        /// <param name="messageFile">附件对象</param>
        /// <param name="UID"></param>
        /// <returns></returns>
        private static List<MailFile> GetFile(string filePath, MIME_Entity[] messageFile, string UID)
        {
            var resultList = new List<MailFile>();
            foreach (MIME_Entity entity in messageFile)
            {
                var fileName = entity.ContentType.Param_Name;
                var fileByte = (MIME_b_SinglepartBase)entity.Body;
                var fileId = UID + "_" + fileName;
                DirFileHelper.CreateFile(filePath + fileId, fileByte.Data);
                var fileSize = DirFileHelper.GetFileSize(filePath + fileId);
                resultList.Add(new MailFile
                {
                    FileId = fileId,
                    FileName = fileName,
                    FileSize = DirFileHelper.ToFileSize(fileSize)
                });
            }
            return resultList;
        }
    }
}
