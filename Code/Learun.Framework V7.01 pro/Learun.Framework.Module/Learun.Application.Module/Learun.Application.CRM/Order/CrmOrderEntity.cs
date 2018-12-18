using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Learun.Application.CRM

{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-07-10 17:47
    /// 描 述：订单管理
    /// </summary>
    public class CrmOrderEntity 
    {
        #region 实体成员
        /// <summary>
        /// 订单主键
        /// </summary>
        /// <returns></returns>
        [Column("F_ORDERID")]
        public string F_OrderId { get; set; }
        /// <summary>
        /// 客户主键
        /// </summary>
        /// <returns></returns>
        [Column("F_CUSTOMERID")]
        public string F_CustomerId { get; set; }
        /// <summary>
        /// 销售人员Id
        /// </summary>
        /// <returns></returns>
        [Column("F_SELLERID")]
        public string F_SellerId { get; set; }
        /// <summary>
        /// 单据日期
        /// </summary>
        /// <returns></returns>
        [Column("F_ORDERDATE")]
        public DateTime? F_OrderDate { get; set; }
        /// <summary>
        /// 单据编号
        /// </summary>
        /// <returns></returns>
        [Column("F_ORDERCODE")]
        public string F_OrderCode { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        /// <returns></returns>
        [Column("F_DISCOUNTSUM")]
        public decimal? F_DiscountSum { get; set; }
        /// <summary>
        /// 应收金额
        /// </summary>
        /// <returns></returns>
        [Column("F_ACCOUNTS")]
        public decimal? F_Accounts { get; set; }
        /// <summary>
        /// 已收金额
        /// </summary>
        /// <returns></returns>
        [Column("F_RECEIVEDAMOUNT")]
        public decimal? F_ReceivedAmount { get; set; }
        /// <summary>
        /// 收款日期
        /// </summary>
        /// <returns></returns>
        [Column("F_PAYMENTDATE")]
        public DateTime? F_PaymentDate { get; set; }
        /// <summary>
        /// 收款方式
        /// </summary>
        /// <returns></returns>
        [Column("F_PAYMENTMODE")]
        public string F_PaymentMode { get; set; }
        /// <summary>
        /// 收款状态（1-未收款2-部分收款3-全部收款）
        /// </summary>
        /// <returns></returns>
        [Column("F_PAYMENTSTATE")]
        public int? F_PaymentState { get; set; }
        /// <summary>
        /// 销售费用
        /// </summary>
        /// <returns></returns>
        [Column("F_SALECOST")]
        public decimal? F_SaleCost { get; set; }
        /// <summary>
        /// 摘要信息
        /// </summary>
        /// <returns></returns>
        [Column("F_ABSTRACTINFO")]
        public string F_AbstractInfo { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        /// <returns></returns>
        [Column("F_CONTRACTCODE")]
        public string F_ContractCode { get; set; }
        /// <summary>
        /// 合同附件
        /// </summary>
        /// <returns></returns>
        [Column("F_CONTRACTFILE")]
        public string F_ContractFile { get; set; }
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
            this.F_OrderId = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.F_OrderId = keyValue;
            this.F_ModifyDate = DateTime.Now;
        }
        #endregion
    }
}

