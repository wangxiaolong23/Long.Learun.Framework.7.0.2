using Learun.Application.Base.SystemModule;
using Learun.Application.CRM;
using Learun.Util;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_CRMModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.09
    /// 描 述：订单管理
    /// </summary>
    public class CrmOrderController : MvcControllerBase
    {
        private CrmOrderIBLL crmOrderIBLL = new CrmOrderBLL();
        private CodeRuleIBLL codeRuleIBLL = new CodeRuleBLL();


        #region 视图功能
        /// <summary>
        /// 订单管理页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 订单增加编辑表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            if (Request["keyValue"] == null)
            {
                ViewBag.OrderCode = codeRuleIBLL.GetBillCode(((int)CodeRuleEnum.CrmOrderCode).ToString());
            }
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        ///  分页查询
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件函数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = crmOrderIBLL.GetPageList(paginationobj, queryJson, "");
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records,
            };
            return Success(jsonData);
        }
        /// <summary>
        ///  获取数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件函数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetFormData(string keyValue)
        {
            var orderData = crmOrderIBLL.GetCrmOrderEntity(keyValue);
            var orderProductData = crmOrderIBLL.GetCrmOrderProductEntity(keyValue);
            var jsonData = new {
                orderData = orderData,
                orderProductData = orderProductData
            };
            return Success(jsonData);
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 删除订单数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            crmOrderIBLL.DeleteEntity(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存订单表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="crmOrderJson">实体对象</param>
        /// <param name="crmOrderProductJson">明细实体对象Json</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string crmOrderJson, string crmOrderProductJson)
        {
            var crmOrderEntity = crmOrderJson.ToObject<CrmOrderEntity>();
            var crmOrderProductEntity = crmOrderProductJson.ToObject<List<CrmOrderProductEntity>>();
            crmOrderIBLL.SaveEntity(keyValue, crmOrderEntity, crmOrderProductEntity);
            if (string.IsNullOrEmpty(keyValue))
            {
                codeRuleIBLL.UseRuleSeed(((int)CodeRuleEnum.CrmOrderCode).ToString());
            }
            return Success("保存成功。", crmOrderEntity.F_OrderId);
        }
        #endregion
    }
}