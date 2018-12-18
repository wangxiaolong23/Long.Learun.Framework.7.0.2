using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.IM
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：即时通讯消息内容
    /// </summary>
    public class IMMsgEntity
    {
        #region 实体成员
        /// <summary>
        /// 消息主键
        /// </summary>
        /// <returns></returns>
        [Column("F_MSGID")]
        public string F_MsgId { get; set; }
        /// <summary>
        /// 发送者ID
        /// </summary>
        /// <returns></returns>
        [Column("F_SENDUSERID")]
        public string F_SendUserId { get; set; }
        /// <summary>
        /// 接收者ID
        /// </summary>
        /// <returns></returns>
        [Column("F_RECVUSERID")]
        public string F_RecvUserId { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        /// <returns></returns>
        [Column("F_CONTENT")]
        public string F_Content { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 是否是系统消息
        /// </summary>
        [Column("F_ISSYSTEM")]
        public int? F_IsSystem { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.F_MsgId = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.F_MsgId = keyValue;
        }
        #endregion
    }
}
