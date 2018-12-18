using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Learun.Application.CRM

{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-07-11 11:30
    /// 描 述：商机管理
    /// </summary>
    public class CrmChanceEntity 
    {
        #region 实体成员
        /// <summary>
        /// 商机主键
        /// </summary>
        /// <returns></returns>
        [Column("F_CHANCEID")]
        public string F_ChanceId { get; set; }
        /// <summary>
        /// 商机编号
        /// </summary>
        /// <returns></returns>
        [Column("F_ENCODE")]
        public string F_EnCode { get; set; }
        /// <summary>
        /// 商机名称
        /// </summary>
        /// <returns></returns>
        [Column("F_FULLNAME")]
        public string F_FullName { get; set; }
        /// <summary>
        /// 商机来源
        /// </summary>
        /// <returns></returns>
        [Column("F_SOURCEID")]
        public string F_SourceId { get; set; }
        /// <summary>
        /// 商机阶段
        /// </summary>
        /// <returns></returns>
        [Column("F_STAGEID")]
        public string F_StageId { get; set; }
        /// <summary>
        /// 成功率
        /// </summary>
        /// <returns></returns>
        [Column("F_SUCCESSRATE")]
        public decimal? F_SuccessRate { get; set; }
        /// <summary>
        /// 预计金额
        /// </summary>
        /// <returns></returns>
        [Column("F_AMOUNT")]
        public decimal? F_Amount { get; set; }
        /// <summary>
        /// 预计利润
        /// </summary>
        /// <returns></returns>
        [Column("F_PROFIT")]
        public decimal? F_Profit { get; set; }
        /// <summary>
        /// 商机类型
        /// </summary>
        /// <returns></returns>
        [Column("F_CHANCETYPEID")]
        public string F_ChanceTypeId { get; set; }
        /// <summary>
        /// 销售费用
        /// </summary>
        /// <returns></returns>
        [Column("F_SALECOST")]
        public decimal? F_SaleCost { get; set; }
        /// <summary>
        /// 预计成交时间
        /// </summary>
        /// <returns></returns>
        [Column("F_DEALDATE")]
        public DateTime? F_DealDate { get; set; }
        /// <summary>
        /// 转换客户
        /// </summary>
        /// <returns></returns>
        [Column("F_ISTOCUSTOM")]
        public int? F_IsToCustom { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        /// <returns></returns>
        [Column("F_COMPANYNAME")]
        public string F_CompanyName { get; set; }
        /// <summary>
        /// 公司性质
        /// </summary>
        /// <returns></returns>
        [Column("F_COMPANYNATUREID")]
        public string F_CompanyNatureId { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        /// <returns></returns>
        [Column("F_COMPANYADDRESS")]
        public string F_CompanyAddress { get; set; }
        /// <summary>
        /// 公司网站
        /// </summary>
        /// <returns></returns>
        [Column("F_COMPANYSITE")]
        public string F_CompanySite { get; set; }
        /// <summary>
        /// 公司情况
        /// </summary>
        /// <returns></returns>
        [Column("F_COMPANYDESC")]
        public string F_CompanyDesc { get; set; }
        /// <summary>
        /// 所在省份
        /// </summary>
        /// <returns></returns>
        [Column("F_PROVINCE")]
        public string F_Province { get; set; }
        /// <summary>
        /// 所在城市
        /// </summary>
        /// <returns></returns>
        [Column("F_CITY")]
        public string F_City { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        /// <returns></returns>
        [Column("F_CONTACTS")]
        public string F_Contacts { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        /// <returns></returns>
        [Column("F_MOBILE")]
        public string F_Mobile { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        /// <returns></returns>
        [Column("F_TEL")]
        public string F_Tel { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        /// <returns></returns>
        [Column("F_FAX")]
        public string F_Fax { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        /// <returns></returns>
        [Column("F_QQ")]
        public string F_QQ { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        /// <returns></returns>
        [Column("F_EMAIL")]
        public string F_Email { get; set; }
        /// <summary>
        /// 微信
        /// </summary>
        /// <returns></returns>
        [Column("F_WECHAT")]
        public string F_Wechat { get; set; }
        /// <summary>
        /// 爱好
        /// </summary>
        /// <returns></returns>
        [Column("F_HOBBY")]
        public string F_Hobby { get; set; }
        /// <summary>
        /// 跟进人员Id
        /// </summary>
        /// <returns></returns>
        [Column("F_TRACEUSERID")]
        public string F_TraceUserId { get; set; }
        /// <summary>
        /// 跟进人员
        /// </summary>
        /// <returns></returns>
        [Column("F_TRACEUSERNAME")]
        public string F_TraceUserName { get; set; }
        /// <summary>
        /// 商机状态编码
        /// </summary>
        /// <returns></returns>
        [Column("F_CHANCESTATE")]
        public int? F_ChanceState { get; set; }
        /// <summary>
        /// 提醒日期
        /// </summary>
        /// <returns></returns>
        [Column("F_ALERTDATETIME")]
        public DateTime? F_AlertDateTime { get; set; }
        /// <summary>
        /// 提醒状态
        /// </summary>
        /// <returns></returns>
        [Column("F_ALERTSTATE")]
        public int? F_AlertState { get; set; }
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
            this.F_ChanceId = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.F_ChanceId = keyValue;
            this.F_ModifyDate = DateTime.Now;
        }
        #endregion
    }
}

