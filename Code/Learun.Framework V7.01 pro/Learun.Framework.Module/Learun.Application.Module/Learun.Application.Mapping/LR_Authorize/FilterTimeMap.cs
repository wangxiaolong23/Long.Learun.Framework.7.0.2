using Learun.Application.Base.AuthorizeModule;
using System.Data.Entity.ModelConfiguration;

namespace Learun.Application.Mapping
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：时段过滤
    /// </summary>
    public class FilterTimeMap : EntityTypeConfiguration<FilterTimeEntity>
    {
        public FilterTimeMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_BASE_FILTERTIME");
            //主键
            this.HasKey(t => t.F_FilterTimeId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
