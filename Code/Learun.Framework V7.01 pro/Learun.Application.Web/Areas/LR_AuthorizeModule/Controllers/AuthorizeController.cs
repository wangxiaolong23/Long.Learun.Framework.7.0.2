using Learun.Application.Base.AuthorizeModule;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_AuthorizeModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：功能权限设置
    /// </summary>
    public class AuthorizeController : MvcControllerBase
    {
        private AuthorizeIBLL authorizeIBLL = new AuthorizeBLL();

        #region 获取视图
        /// <summary>
        /// 功能权限设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 移动功能权限设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AppForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取设置信息
        /// </summary>
        /// <param name="objectId">设置对象</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetFormData(string objectId)
        {
            var modules = authorizeIBLL.GetItemIdList(objectId, 1);
            var buttons = authorizeIBLL.GetItemIdList(objectId, 2);
            var columns = authorizeIBLL.GetItemIdList(objectId, 3);
            var forms = authorizeIBLL.GetItemIdList(objectId, 4);

            var datajson = new
            {
                modules,
                buttons,
                columns,
                forms
            };
            return Success(datajson);
        }
        /// <summary>
        /// 获取设置信息(移动App)
        /// </summary>
        /// <param name="objectId">设置对象</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetAppFormData(string objectId)
        {
            var data = authorizeIBLL.GetItemIdList(objectId, 5);
            return Success(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <param name="objectType">权限分类-1角色2用户</param>
        /// <param name="moduleIds">功能Id</param>
        /// <param name="moduleButtonIds">按钮Id</param>
        /// <param name="moduleColumnIds">视图Id</param>
        /// <param name="strModuleFormId">表单Id</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string objectId,int objectType, string strModuleId, string strModuleButtonId, string strModuleColumnId,string strModuleFormId)
        {
            string[] moduleIds = strModuleId.Split(',');
            string[] moduleButtonIds = strModuleButtonId.Split(',');
            string[] moduleColumnIds = strModuleColumnId.Split(',');
            string[] moduleFormIds = strModuleFormId.Split(',');

            authorizeIBLL.SaveAuthorize(objectType, objectId, moduleIds, moduleButtonIds, moduleColumnIds, moduleFormIds);
            return Success("保存成功！");
        }
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <param name="objectType">权限分类-1角色2用户</param>
        /// <param name="strFormId">移动功能Id</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveAppForm(string objectId, int objectType, string strFormId)
        {
            string[] formIds = strFormId.Split(',');

            authorizeIBLL.SaveAppAuthorize(objectType, objectId, formIds);
            return Success("保存成功！");
        }
        #endregion
    }
}