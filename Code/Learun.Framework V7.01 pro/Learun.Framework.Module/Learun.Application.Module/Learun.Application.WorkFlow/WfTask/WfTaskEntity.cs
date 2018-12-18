using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：任务实例
    /// </summary>
    public class WfTaskEntity
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
        /// 当前任务类型 0.创建 1.审批 2.重新创建 3.确认阅读 4.加签
        /// </summary>
        [Column("F_TASKTYPE")]
        public int F_TaskType { get; set; }
        /// <summary>
        /// 当前任务是否执行完了 0 未处理 1 已处理 2 关闭
        /// </summary>
        [Column("F_ISFINISHED")]
        public int F_IsFinished { get; set; }
        /// <summary>
        /// 审核者主键
        /// </summary>
        [Column("F_AUDITORID")]
        public string F_AuditorId { get; set; }
        /// <summary>
        /// 审核者名称
        /// </summary>
        [Column("F_AUDITORNAME")]
        public string F_AuditorName { get; set; }
        /// <summary>
        /// 公司主键
        /// </summary>
        [Column("F_COMPANYID")]
        public string F_CompanyId { get; set; }
        /// <summary>
        /// 部门主键
        /// </summary>
        [Column("F_DEPARTMENTID")]
        public string F_DepartmentId { get; set; }
        /// <summary>
        /// 超时时间（超时后可流转下一节点）默认24小时
        /// </summary>
        [Column("F_TIMEOUTACTION")]
        public int? F_TimeoutAction { get; set; }
        /// <summary>
        /// 超时时间（发出通知）默认24小时
        /// </summary>
        [Column("F_TIMEOUTNOTICE")]
        public int? F_TimeoutNotice { get; set; }

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
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYDATE")]
        public DateTime? F_ModifyDate { get; set; }

        /// <summary>
        /// 完成人员主键
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYUSERID")]
        public string F_ModifyUserId { get; set; }
        /// <summary>
        /// 完成人员名称
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYUSERNAME")]
        public string F_ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.F_Id = Guid.NewGuid().ToString();
            this.F_IsFinished = 0;
            this.F_CreateDate = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void Modify(string keyValue)
        {
            this.F_Id = keyValue;
            this.F_ModifyDate = DateTime.Now;
        }
        #endregion

        #region 扩展字段
        /// <summary>
        /// 审核者们
        /// </summary>
        [NotMapped]
        public List<WfAuditor> auditors { get; set; }
        #endregion
    }
}
