using System;

namespace Learun.Util {
    public class MailAccount
    {
        /// <summary>
        /// POP3服务
        /// </summary>
        public string POP3Host { get; set; }
        /// <summary>
        /// POP3端口
        /// </summary>
        public int POP3Port { get; set; }
        /// <summary>
        /// SMTP服务
        /// </summary>
        public string SMTPHost { get; set; }
        /// <summary>
        /// SMTP端口
        /// </summary>
        public int SMTPPort { get; set; }
        /// <summary>
        /// 账户
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 账户名称
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// SSL
        /// </summary>
        public bool Ssl { get; set; }
    }
}
