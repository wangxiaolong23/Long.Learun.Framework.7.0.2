using Learun.Application.WorkFlow;
using Learun.Util;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_WorkFlowModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：工作委托
    /// </summary>
    public class WfDelegateRuleController : MvcControllerBase
    {
        private WfDelegateRuleIBLL wfDelegateRuleIBLL = new WfDelegateRuleBLL();

        #region 视图功能
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageList(string pagination, string keyword)
        {
            UserInfo userInfo = LoginUserInfo.Get();
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = wfDelegateRuleIBLL.GetPageList(paginationobj, keyword, userInfo);
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
        /// 获取关联模板数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetRelationList(string keyValue)
        {
            var relationList = wfDelegateRuleIBLL.GetRelationList(keyValue);
            return Success(relationList);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存流程模板
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="schemeInfo">表单设计模板信息</param>
        /// <param name="shcemeAuthorize">模板权限信息</param>
        /// <param name="scheme">模板内容</param>
        /// <param name="type">类型1.正式2.草稿</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string strEntity, string strSchemeInfo)
        {
            WfDelegateRuleEntity entity = strEntity.ToObject<WfDelegateRuleEntity>();
            wfDelegateRuleIBLL.SaveEntity(keyValue, entity, strSchemeInfo.Split(','));
            return Success("保存成功！");
        }
        /// <summary>
        /// 删除模板数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            wfDelegateRuleIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }

        /// <summary>
        /// 启用/停用表单
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state">状态1启用0禁用</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult UpDateSate(string keyValue, int state)
        {
            wfDelegateRuleIBLL.UpdateState(keyValue, state);
            return Success((state == 1 ? "启用" : "禁用") + "成功！");
        }
        #endregion
    }
}