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
    /// 描 述：工作流实例
    /// </summary>
    public class WfProcessInstanceEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键Id
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 流程模板Scheme
        /// </summary>
        /// <returns></returns>
        [Column("F_SCHEMEID")]
        public string F_SchemeId { get; set; }
        /// <summary>
        /// 实例编号
        /// </summary>
        /// <returns></returns>
        [Column("F_SCHEMECODE")]
        public string F_SchemeCode { get; set; }
        /// <summary>
        /// 实例编号
        /// </summary>
        /// <returns></returns>
        [Column("F_SCHEMENAME")]
        public string F_SchemeName { get; set; }
        /// <summary>
        /// 自定定义标题
        /// </summary>
        /// <returns></returns>
        [Column("F_PROCESSNAME")]
        public string F_ProcessName { get; set; }
        /// <summary>
        /// 重要等级0-普通，1-重要，2-紧急
        /// </summary>
        /// <returns></returns>
        [Column("F_PROCESSLEVEL")]
        public int? F_ProcessLevel { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建用户所在公司ID
        /// </summary>
        /// <returns></returns>
        [Column("F_COMPANYID")]
        public string F_CompanyId { get; set; }
        /// <summary>
        /// 创建用户所在部门ID
        /// </summary>
        /// <returns></returns>
        [Column("F_DEPARTMENTID")]
        public string F_DepartmentId { get; set; }

        /// <summary>
        /// 有效标志（1正常，0暂停）
        /// </summary>
        /// <returns></returns>
        [Column("F_ENABLEDMARK")]
        public int? F_EnabledMark { get; set; }
        /// <summary>
        /// 是否结束（0正常，1结束）
        /// </summary>
        /// <returns></returns>
        [Column("F_ISFINISHED")]
        public int? F_IsFinished { get; set; }
        /// <summary>
        /// 是否需要重新发起（0正常，1需要）
        /// </summary>
        /// <returns></returns>
        [Column("F_ISAGAIN")]
        public int? F_IsAgain { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }

        #region 预留字段
        /// <summary>
        /// 是不是子流程 1是 0 否
        /// </summary>
        /// <returns></returns>
        [Column("F_ISCHILDFLOW")]
        public int? F_IsChildFlow { get; set; }
        /// <summary>
        /// 父流程实例主键
        /// </summary>
        /// <returns></returns>
        [Column("F_PROCESSPARENTID")]
        public string F_ProcessParentId { get; set; }
        #endregion

        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.F_EnabledMark = 1;
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
        }
        #endregion

        #region 扩展字段
        /// <summary>
        /// 任务名称
        /// </summary>
        [NotMapped]
        public string F_TaskName { get; set; }
        /// <summary>
        /// 任务主键
        /// </summary>
        [NotMapped]
        public string F_TaskId { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        [NotMapped]
        public int? F_TaskType { get; set; }
        #endregion
    }
}
