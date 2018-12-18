using Learun.Application.CRM;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-07-11 09:58
    /// 描 述：客户联系人
    /// </summary>
    public class CrmCustomerContactMap : EntityTypeConfiguration<CrmCustomerContactEntity>
    {
        public CrmCustomerContactMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_CRM_CUSTOMERCONTACT");
            //主键
            this.HasKey(t => t.F_CustomerContactId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

