using Learun.Application.Base.SystemModule;
using Learun.Application.CRM;
using Learun.Util;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_CRMModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-07-11 11:30
    /// 描 述：商机管理
    /// </summary>
    public class ChanceController : MvcControllerBase
    {
        private CrmChanceIBLL crmChanceIBLL = new CrmChanceBLL();
        private CodeRuleIBLL codeRuleIBLL = new CodeRuleBLL();

        #region 视图功能
        /// <summary>
        /// 商机列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 商机表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            if (Request["keyValue"] == null)
            {
                ViewBag.EnCode = codeRuleIBLL.GetBillCode(((int)CodeRuleEnum.CrmChanceCode).ToString());
            }
            return View();
        }
        /// <summary>
        /// 商机明细页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Detail()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取商机列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = crmChanceIBLL.GetPageList(paginationobj, queryJson);
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
        /// 获取商机实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = crmChanceIBLL.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 商机名称不能重复
        /// </summary>
        /// <param name="FullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult ExistFullName(string FullName, string keyValue)
        {
            bool IsOk = crmChanceIBLL.ExistFullName(FullName, keyValue);
            return Success(IsOk.ToString());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除商机数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            crmChanceIBLL.DeleteEntity(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存商机表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, CrmChanceEntity entity)
        {
            crmChanceIBLL.SaveEntity(keyValue, entity);
            return Success("操作成功。");
        }
        /// <summary>
        /// 商机作废
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult Invalid(string keyValue)
        {
            crmChanceIBLL.Invalid(keyValue);
            return Success("作废成功。");
        }
        /// <summary>
        /// 商机转换客户
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult ToCustomer(string keyValue)
        {
            string enCode = codeRuleIBLL.GetBillCode(((int)CodeRuleEnum.CrmChanceCode).ToString());
            crmChanceIBLL.ToCustomer(keyValue, enCode);
            codeRuleIBLL.UseRuleSeed(((int)CodeRuleEnum.CrmChanceCode).ToString());
            return Success("转换成功。");
        }
        #endregion
    }
}