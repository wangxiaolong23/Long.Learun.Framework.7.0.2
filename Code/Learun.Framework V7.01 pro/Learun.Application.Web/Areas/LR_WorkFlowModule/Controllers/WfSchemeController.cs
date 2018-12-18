using Learun.Application.WorkFlow;
using Learun.Util;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_WorkFlowModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：工作流模板处理
    /// </summary>
    public class WfSchemeController : MvcControllerBase
    {
        private WfSchemeIBLL wfSchemeIBLL = new WfSchemeBLL();

        #region 视图功能
        /// <summary>
        /// 流程模板管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 流程模板设计
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 流程模板设计历史记录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult HistoryForm()
        {
            return View();
        }

        /// <summary>
        /// 预览流程模板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PreviewForm()
        {
            return View();
        }

        /// <summary>
        /// 节点信息设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NodeForm()
        {
            return View();
        }



        #region 审核人员添加
        /// <summary>
        /// 添加岗位
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PostForm()
        {
            return View();
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RoleForm()
        {
            return View();
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UserForm()
        {
            return View();
        }
        #endregion

        #region 表单添加
        /// <summary>
        /// 表单添加
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult WorkformForm()
        {
            return View();
        }
        #endregion

        #region 表单权限设置
        /// <summary>
        /// 权限添加
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AuthorizeForm()
        {
            return View();
        }
        #endregion

        #region 条件字段
        /// <summary>
        /// 条件字段添加
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ConditionFieldForm()
        {
            return View();
        }
        /// <summary>
        /// 字段选择
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FieldSelectForm()
        {
            return View();
        }

        #endregion


        /// <summary>
        /// 线段信息设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LineForm()
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
        public ActionResult GetSchemeInfoPageList(string pagination, string keyword, string category)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = wfSchemeIBLL.GetSchemeInfoPageList(paginationobj, keyword, category);
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
        /// 获取自定义流程列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetCustmerSchemeInfoList()
        {
            UserInfo userInfo = LoginUserInfo.Get();
            var data = wfSchemeIBLL.GetCustmerSchemeInfoList(userInfo);
            return Success(data);
        }
        /// <summary>
        /// 获取设计表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetFormData(string schemeCode)
        {
            WfSchemeInfoEntity schemeInfoEntity = wfSchemeIBLL.GetWfSchemeInfoEntityByCode(schemeCode);
            if (schemeInfoEntity == null) {
                return Success(new { });
            }

            WfSchemeEntity schemeEntity = wfSchemeIBLL.GetWfSchemeEntity(schemeInfoEntity.F_SchemeId);
            var wfSchemeAuthorizeList = wfSchemeIBLL.GetWfSchemeAuthorizeList(schemeInfoEntity.F_Id);
            var jsonData = new
            {
                schemeInfoEntity = schemeInfoEntity,
                schemeEntity = schemeEntity,
                wfSchemeAuthorizeList = wfSchemeAuthorizeList
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 获取模板分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="schemeInfoId"></param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetSchemePageList(string pagination, string schemeInfoId)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = wfSchemeIBLL.GetSchemePageList(paginationobj, schemeInfoId);
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
        /// 获取流程模板数据
        /// </summary>
        /// <param name="schemeId">模板主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetScheme(string schemeId)
        {
            var data = wfSchemeIBLL.GetWfSchemeEntity(schemeId);
            return Success(data);
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
        public ActionResult SaveForm(string keyValue, string schemeInfo, string shcemeAuthorize, string scheme, int type)
        {
            WfSchemeInfoEntity schemeInfoEntity = schemeInfo.ToObject<WfSchemeInfoEntity>();
            List<WfSchemeAuthorizeEntity> wfSchemeAuthorizeList = shcemeAuthorize.ToObject<List<WfSchemeAuthorizeEntity>>();
            WfSchemeEntity schemeEntity = new WfSchemeEntity();
            schemeEntity.F_Scheme = scheme;
            schemeEntity.F_Type = type;

            wfSchemeIBLL.SaveEntity(keyValue, schemeInfoEntity, schemeEntity, wfSchemeAuthorizeList);
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
            wfSchemeIBLL.VirtualDelete(keyValue);
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
            wfSchemeIBLL.UpdateState(keyValue, state);
            return Success((state == 1 ? "启用" : "禁用") + "成功！");
        }
        /// <summary>
        /// 更新表单模板版本
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state">状态1启用0禁用</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult UpdateScheme(string schemeInfoId, string schemeId)
        {
            wfSchemeIBLL.UpdateScheme(schemeInfoId, schemeId);
            return Success("更新成功！");
        }
        #endregion
    }
}