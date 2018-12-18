using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Learun.Application.CRM

{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-07-11 14:20
    /// 描 述：现金余额
    /// </summary>
    public class CrmCashBalanceEntity 
    {
        #region 实体成员
        /// <summary>
        /// 现金余额主键
        /// </summary>
        /// <returns></returns>
        [Column("F_CASHBALANCEID")]
        public string F_CashBalanceId { get; set; }
        /// <summary>
        /// 对象主键
        /// </summary>
        /// <returns></returns>
        [Column("F_OBJECTID")]
        public string F_ObjectId { get; set; }
        /// <summary>
        /// F_ExecutionDate
        /// </summary>
        /// <returns></returns>
        [Column("F_EXECUTIONDATE")]
        public DateTime? F_ExecutionDate { get; set; }
        /// <summary>
        /// F_CashAccount
        /// </summary>
        /// <returns></returns>
        [Column("F_CASHACCOUNT")]
        public string F_CashAccount { get; set; }
        /// <summary>
        /// F_Receivable
        /// </summary>
        /// <returns></returns>
        [Column("F_RECEIVABLE")]
        public decimal? F_Receivable { get; set; }
        /// <summary>
        /// F_Expenses
        /// </summary>
        /// <returns></returns>
        [Column("F_EXPENSES")]
        public decimal? F_Expenses { get; set; }
        /// <summary>
        /// F_Balance
        /// </summary>
        /// <returns></returns>
        [Column("F_BALANCE")]
        public decimal? F_Balance { get; set; }
        /// <summary>
        /// F_Abstract
        /// </summary>
        /// <returns></returns>
        [Column("F_ABSTRACT")]
        public string F_Abstract { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        [Column("F_SORTCODE")]
        public int? F_SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        [Column("F_DELETEMARK")]
        public int? F_DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        /// <returns></returns>
        [Column("F_ENABLEDMARK")]
        public int? F_EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }
        /// <summary>
        /// 创建日期
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
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYDATE")]
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYUSERID")]
        public string F_ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
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
            this.F_CashBalanceId = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.F_CashBalanceId = keyValue;
            this.F_ModifyDate = DateTime.Now;
        }
        #endregion
    }
}

