using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：任务实例处理记录
    /// </summary>
    public class WfTaskHistoryEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键Id
        /// </summary>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 关联的实例Id
        /// </summary>
        [Column("F_PROCESSID")]
        public string F_ProcessId { get; set; }
        /// <summary>
        /// 节点Id
        /// </summary>
        [Column("F_NODEID")]
        public string F_NodeId { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        [Column("F_NODENAME")]
        public string F_NodeName { get; set; }
        /// <summary>
        /// 完成任务类型 0.创建 1.审批 2.重新创建 3.确认阅读 4.加签
        /// </summary>
        [Column("F_TASKTYPE")]
        public int? F_TaskType { get; set; }
        /// <summary>
        /// 处理结果1.同意 2.反对 3.超时
        /// </summary>
        [Column("F_RESULT")]
        public int? F_Result { get; set; }
        /// <summary>
        /// 上一个节点
        /// </summary>
        [Column("F_PREVIOUSID")]
        public string F_PreviousId { get; set; }
        /// <summary>
        /// 上一个节点名称
        /// </summary>
        [Column("F_PREVIOUSNAME")]
        public string F_PreviousName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// 处理意见
        /// </summary>
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.F_Id = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
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
