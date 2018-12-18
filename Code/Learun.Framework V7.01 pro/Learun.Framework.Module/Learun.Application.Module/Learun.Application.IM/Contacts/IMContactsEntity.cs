using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.IM
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.05.31
    /// 描 述：最近联系人列表
    /// </summary>
    public class IMContactsEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 发送者ID
        /// </summary>
        /// <returns></returns>
        [Column("F_MYUSERID")]
        public string F_MyUserId { get; set; }
        /// <summary>
        /// 接收者ID
        /// </summary>
        /// <returns></returns>
        [Column("F_OTHERUSERID")]
        public string F_OtherUserId { get; set; }
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
        [Column("F_TIME")]
        public DateTime? F_Time { get; set; }

        /// <summary>
        /// 是否已读1 未读 2 已读
        /// </summary>
        /// <returns></returns>
        [Column("F_ISREAD")]
        public int? F_IsRead { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.F_Id = Guid.NewGuid().ToString();
            this.F_Time = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.F_Id = keyValue;
            this.F_Time = DateTime.Now;
        }
        #endregion
    }
}
