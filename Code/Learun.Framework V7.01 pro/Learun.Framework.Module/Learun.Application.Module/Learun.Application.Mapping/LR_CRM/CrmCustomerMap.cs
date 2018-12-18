using Learun.Application.CRM;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-07-11 09:43
    /// 描 述：客户管理
    /// </summary>
    public class CrmCustomerMap : EntityTypeConfiguration<CrmCustomerEntity>
    {
        public CrmCustomerMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_CRM_CUSTOMER");
            //主键
            this.HasKey(t => t.F_CustomerId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

