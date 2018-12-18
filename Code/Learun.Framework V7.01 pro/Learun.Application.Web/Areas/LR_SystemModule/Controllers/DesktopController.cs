using Learun.Application.Base.SystemModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_SystemModule.Controllers
{
    //// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：王小龙 wxl 
    /// 日 期：2018.12.15
    /// 描 述：首页
    /// </summary>
    public class DesktopController : MvcControllerBase
    {
        private DesktopIBLL desktopIBLL = new DesktopBLL();
        private DatabaseLinkIBLL databaseLinkIBLL = new DatabaseLinkBLL();
        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 根据sql语句获取数据
        /// </summary>
        /// <param name="databaselinkid">数据库连接</param>
        /// <param name="sql">sql查询语句</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetSqlData(string databaseLinkId, string sql)
        {
            //var data = desktopIBLL.GetSqlData(databaseLinkId, sql);
            var data = databaseLinkIBLL.FindTable(databaseLinkId, sql);
            return Success(data);
        }
    }
}