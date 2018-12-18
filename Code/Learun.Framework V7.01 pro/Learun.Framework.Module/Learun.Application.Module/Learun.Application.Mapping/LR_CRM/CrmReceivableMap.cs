using Learun.Application.CRM;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-07-11 14:48
    /// 描 述：应收账款
    /// </summary>
    public class LR_CRM_ReceivableMap : EntityTypeConfiguration<CrmReceivableEntity>
    {
        public LR_CRM_ReceivableMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_CRM_RECEIVABLE");
            //主键
            this.HasKey(t => t.F_ReceivableId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

