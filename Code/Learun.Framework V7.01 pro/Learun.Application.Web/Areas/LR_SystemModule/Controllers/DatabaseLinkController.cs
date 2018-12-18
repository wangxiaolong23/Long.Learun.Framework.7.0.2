using Learun.Application.Base.SystemModule;
using Learun.Util;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_SystemModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.01
    /// 描 述：数据库连接
    /// </summary>
    public class DatabaseLinkController : MvcControllerBase
    {
        DatabaseLinkIBLL databaseLinkIBLL = new  DatabaseLinkBLL();

        #region 获取视图
        /// <summary>
        /// 管理页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetList(string keyword)
        {
            var data = databaseLinkIBLL.GetListByNoConnection(keyword);
            return Success(data);
        }

        /// <summary>
        /// 获取映射数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMap(string ver)
        {
            var data = databaseLinkIBLL.GetMap();
            string md5 = Md5Helper.Encrypt(data.ToJson(), 32);
            if (md5 == ver)
            {
                return Success("no update");
            }
            else
            {
                var jsondata = new
                {
                    data = data,
                    ver = md5
                };
                return Success(jsondata);
            }
        }

        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetTreeList()
        {
            var data = databaseLinkIBLL.GetTreeList();
            return Success(data);
        } 

        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, DatabaseLinkEntity entity)
        {
            bool res = databaseLinkIBLL.SaveEntity(keyValue, entity);
            if (res)
            {
                return Success("保存成功！");
            }
            else
            {
                return Fail("保存失败,连接串信息有误！");
            }
        }
        /// <summary>
        /// 删除表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            databaseLinkIBLL.VirtualDelete(keyValue);
            return Success("删除成功！");
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 测试连接串是否正确
        /// </summary>
        /// <param name="connection">连接串</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult TestConnection(string connection, string dbType,string keyValue)
        {
            bool res = databaseLinkIBLL.TestConnection(connection, dbType,keyValue);
            if (res)
            {
                return Success("连接成功！");
            }
            else
            {
                return Fail("连接失败,连接串信息有误！");
            }

        }
        #endregion
    }
}