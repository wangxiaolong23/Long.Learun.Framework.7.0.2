using Learun.Cache.Base;
using Learun.Cache.Factory;
using Learun.DataBase.Repository;
using Learun.Util;
using System;
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
    public class DesktopBLL : DesktopIBLL
    {
        #region 属性
        private DatabaseLinkService databaseLinkService = new DatabaseLinkService();
        private DatabaseLinkIBLL databaseLinkIBLL = new DatabaseLinkBLL();
        #endregion

        #region 缓存定义
        private ICache cache = CacheFactory.CaChe();
        private string cacheKey = "learun_adms_databaseLink";
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetSqlData(string databaseLinkId, string sql)
        {
            try
            {
                DatabaseLinkEntity entity = databaseLinkIBLL.GetEntity(databaseLinkId);
                return databaseLinkService.FindTable(entity, sql, null);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        #endregion
    }
}
