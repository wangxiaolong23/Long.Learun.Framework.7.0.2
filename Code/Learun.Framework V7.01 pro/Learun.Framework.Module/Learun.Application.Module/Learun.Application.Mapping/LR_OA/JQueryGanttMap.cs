using Learun.Application.OA.Gantt;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-06-20 15:42
    /// 描 述：项目计划
    /// </summary>
    public class JQueryGanttMap : EntityTypeConfiguration<JQueryGanttEntity>
    {
        public JQueryGanttMap()
        {
            #region 表、主键
            //表
            this.ToTable("JQUERYGANTT");
            //主键
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

