using Learun.Application.Base.AuthorizeModule;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_AuthorizeModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：IP过滤
    /// </summary>
    public class FilterIPController : MvcControllerBase
    {
        private FilterIPIBLL filterIPIBLL = new FilterIPBLL();

        #region 视图功能
        /// <summary>
        /// 过滤IP管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 过滤IP表单
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
        /// 过滤IP列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <param name="visitType">访问:0-拒绝，1-允许</param>
        /// <returns>返回树形列表Json</returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetList(string objectId, string visitType)
        {
            var data = filterIPIBLL.GetList(objectId, visitType);
            return Success(data);
        }
        /// <summary>
        /// 过滤IP实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetFormData(string keyValue)
        {
            var data = filterIPIBLL.GetEntity(keyValue);
            return Success(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存过滤IP表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="filterIPEntity">过滤IP实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, FilterIPEntity filterIPEntity)
        {
            filterIPIBLL.SaveForm(keyValue, filterIPEntity);
            return Success("操作成功。");
        }
        /// <summary>
        /// 删除过滤IP
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            filterIPIBLL.DeleteEntiy(keyValue);
            return Success("删除成功。");
        }
        #endregion
    }
}