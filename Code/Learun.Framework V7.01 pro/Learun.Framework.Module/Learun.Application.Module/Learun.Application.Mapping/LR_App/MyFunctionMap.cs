using Learun.Application.AppMagager;
using System.Data.Entity.ModelConfiguration;

namespace Learun.Application.Mapping.LR_App
{
    /// <summary> 
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员 
    /// 日 期：2018-06-26 10:32 
    /// 描 述：测试 
    /// </summary> 
    public class MyFunctionMap : EntityTypeConfiguration<MyFunctionEntity>
    {
        public MyFunctionMap()
        {
            #region 表、主键 
            //表 
            this.ToTable("LR_APP_MYFUNCTION");
            //主键 
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系 
            #endregion
        }
    }
}
