using Learun.Application.Base.SystemModule;
using System.Data.Entity.ModelConfiguration;

namespace Learun.Application.Mapping.LR_System.Module
{
    public class ModuleFormMap : EntityTypeConfiguration<ModuleFormEntity>
    {
        public ModuleFormMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_BASE_MODULEFORM");
            //主键
            this.HasKey(t => t.F_ModuleFormId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
