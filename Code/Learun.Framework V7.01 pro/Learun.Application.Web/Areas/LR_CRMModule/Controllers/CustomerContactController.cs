using Learun.Application.CRM;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_CRMModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-07-11 09:58
    /// 描 述：客户联系人
    /// </summary>
    public class CustomerContactController : MvcControllerBase
    {
        
        private CrmCustomerContactIBLL crmCustomerContactIBLL = new CrmCustomerContactBLL();

        #region 视图功能
        /// <summary>
        /// 联系人列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ContactIndex()
        {
            return View();
        }
        /// <summary>
        /// 联系人表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ContactForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取联系人列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetContactListJson(string queryJson)
        {
            var data = crmCustomerContactIBLL.GetList(queryJson);
            return Success(data);
        }
        /// <summary>
        /// 获取联系人实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetContactFormJson(string keyValue)
        {
            var data = crmCustomerContactIBLL.GetEntity(keyValue);
            return Success(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除联系人数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteContactForm(string keyValue)
        {
            crmCustomerContactIBLL.DeleteEntity(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存联系人表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveContactForm(string keyValue, CrmCustomerContactEntity entity)
        {
            crmCustomerContactIBLL.SaveEntity(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion
    }
}