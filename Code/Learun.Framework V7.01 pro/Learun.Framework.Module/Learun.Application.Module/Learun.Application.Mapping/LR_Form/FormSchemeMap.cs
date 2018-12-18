using Learun.Application.Form;
using System.Data.Entity.ModelConfiguration;

namespace Learun.Application.Mapping
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.01
    /// 描 述：表单模板
    /// </summary>
    public class FormSchemeMap : EntityTypeConfiguration<FormSchemeEntity>
    {
        public FormSchemeMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_FORM_SCHEME");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
