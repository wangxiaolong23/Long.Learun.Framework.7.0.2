using Learun.DataBase.Repository;
using Learun.Util;
using System.Collections.Generic;
using System.Data;

namespace Learun.Application.Base.SystemModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：王小龙 wxl 
    /// 日 期：2018.12.15
    /// 描 述：我的桌面
    /// </summary>
    public interface DesktopIBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        DataTable GetSqlData(string databaseLinkId, string sql);
        #endregion
    }
}
