using Learun.Application.TwoDevelopment.SystemDemo;
using System.Data.Entity.ModelConfiguration;

namespace Learun.Application.Mapping
{
    public class DemoleaveMap : EntityTypeConfiguration<DemoleaveEntity>
    {
        public DemoleaveMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_DEMO_FORMLEAVE");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion
 
            #region 配置关系
            #endregion
        }
    }
}
