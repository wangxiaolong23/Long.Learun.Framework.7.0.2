using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.OA.Email.EmailConfig
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.06.04
    /// 描 述：邮箱配置
    /// </summary>
    public class EmailConfigEntity
    {
        #region 实体成员
        /// <summary>
        /// 邮件账户主键
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// POP3服务
        /// </summary>
        /// <returns></returns>
        [Column("F_POP3HOST")]
        public string F_POP3Host { get; set; }
        /// <summary>
        /// POP3端口
        /// </summary>
        /// <returns></returns>
        [Column("F_POP3PORT")]
        public int? F_POP3Port { get; set; }
        /// <summary>
        /// SMTP服务
        /// </summary>
        /// <returns></returns>
        [Column("F_SMTPHOST")]
        public string F_SMTPHost { get; set; }
        /// <summary>
        /// SMTP端口
        /// </summary>
        /// <returns></returns>
        [Column("F_SMTPPORT")]
        public int? F_SMTPPort { get; set; }
        /// <summary>
        /// 账户
        /// </summary>
        /// <returns></returns>
        [Column("F_ACCOUNT")]
        public string F_Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        /// <returns></returns>
        [Column("F_PASSWORD")]
        public string F_Password { get; set; }
        /// <summary>
        /// SSL登录
        /// </summary>
        /// <returns></returns>
        [Column("F_SSL")]
        public int? F_Ssl { get; set; }
        /// <summary>
        /// 发件人名称
        /// </summary>
        /// <returns></returns>
        [Column("F_SENDERNAME")]
        public string F_SenderName { get; set; }
        /// <summary>
        /// 我的文件夹
        /// </summary>
        /// <returns></returns>
        [Column("F_FOLDERJSON")]
        public string F_FolderJson { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        /// <returns></returns>
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        [Column("F_SORTCODE")]
        public int? F_SortCode { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        /// <returns></returns>
        [Column("F_ENABLEDMARK")]
        public int? F_EnabledMark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATORTIME")]
        public DateTime? F_CreatorTime { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATORUSERID")]
        public string F_CreatorUserId { get; set; }

        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.F_Id = Guid.NewGuid().ToString();
            this.F_CreatorTime = DateTime.Now;
            this.F_EnabledMark = 1;

            UserInfo userInfo = LoginUserInfo.Get();
            this.F_CreatorUserId = userInfo.userId;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void Modify(string keyValue)
        {
            this.F_Id = keyValue;
        }
        #endregion
    }
}
