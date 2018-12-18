using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Learun.Application.CRM

{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-07-11 14:28
    /// 描 述：费用支出
    /// </summary>
    public class CrmExpensesEntity 
    {
        #region 实体成员
        /// <summary>
        /// 支出主键
        /// </summary>
        /// <returns></returns>
        [Column("F_EXPENSESID")]
        public string F_ExpensesId { get; set; }
        /// <summary>
        /// 支出日期
        /// </summary>
        /// <returns></returns>
        [Column("F_EXPENSESDATE")]
        public DateTime? F_ExpensesDate { get; set; }
        /// <summary>
        /// 支出金额
        /// </summary>
        /// <returns></returns>
        [Column("F_EXPENSESPRICE")]
        public decimal? F_ExpensesPrice { get; set; }
        /// <summary>
        /// 支出账户
        /// </summary>
        /// <returns></returns>
        [Column("F_EXPENSESACCOUNT")]
        public string F_ExpensesAccount { get; set; }
        /// <summary>
        /// 支出种类
        /// </summary>
        /// <returns></returns>
        [Column("F_EXPENSESTYPE")]
        public string F_ExpensesType { get; set; }
        /// <summary>
        /// 支出摘要
        /// </summary>
        /// <returns></returns>
        [Column("F_EXPENSESABSTRACT")]
        public string F_ExpensesAbstract { get; set; }
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
        /// <summary>
        /// F_ExpensesObject
        /// </summary>
        /// <returns></returns>
        [Column("F_EXPENSESOBJECT")]
        public int? F_ExpensesObject { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        /// <returns></returns>
        [Column("F_MANAGERS")]
        public string F_Managers { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.F_ExpensesId = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.F_ExpensesId = keyValue;
            this.F_ModifyDate = DateTime.Now;
        }
        #endregion
    }
}

