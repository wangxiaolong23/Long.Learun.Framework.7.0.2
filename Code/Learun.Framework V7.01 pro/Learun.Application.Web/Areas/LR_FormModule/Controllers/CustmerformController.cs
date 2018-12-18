using Learun.Application.Form;
using Learun.Util;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_FormModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.01
    /// 描 述：普通表单设计
    /// </summary>
    public class CustmerformController : MvcControllerBase
    {
        private FormSchemeIBLL formSchemeIBLL = new FormSchemeBLL();

        #region 视图功能
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
        /// 表单设计页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 表单预览
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PreviewForm()
        {
            return View();
        }
        /// <summary>
        /// 表单模板历史记录查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult HistoryForm()
        {
            return View();
        } 
        /// <summary>
        /// 数据库表增改
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DataTableForm()
        {
            return View();
        }



        /// <summary>
        /// 设置表格字段
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SetFieldIndex()
        {
            return View();
        }
        /// <summary>
        /// 设置表格字段
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SetFieldForm()
        {
            return View();
        }
        /// <summary>
        /// 设置表格字段
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SetSelectFieldForm()
        {
            return View();
        }


        /// <summary>
        /// 自定义表单弹层实例
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult LayerInstanceForm()
        {
            return View();
        }

        /// <summary>
        /// 自定义表单窗口页实例
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TabInstanceForm()
        {
            return View();
        }

        /// <summary>
        /// 自定义表单用于工作流实例
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult WorkflowInstanceForm()
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
        public ActionResult GetPageList(string pagination, string keyword, string category)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = formSchemeIBLL.GetSchemeInfoPageList(paginationobj, keyword, category, 0);
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
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="schemeInfoId"></param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetSchemePageList(string pagination, string schemeInfoId)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = formSchemeIBLL.GetSchemePageList(paginationobj, schemeInfoId);
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
        /// 获取设计表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetFormData(string keyValue)
        {
            FormSchemeInfoEntity schemeInfoEntity = formSchemeIBLL.GetSchemeInfoEntity(keyValue);
            FormSchemeEntity schemeEntity = formSchemeIBLL.GetSchemeEntity(schemeInfoEntity.F_SchemeId);
            var jsonData = new
            {
                schemeInfoEntity = schemeInfoEntity,
                schemeEntity = schemeEntity
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取自定义表单模板数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetSchemeEntity(string keyValue)
        {
            var data = formSchemeIBLL.GetSchemeEntity(keyValue);
            return Success(data);
        }
        /// <summary>
        /// 获取自定义表单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetSchemeInfoList()
        {
            var data = formSchemeIBLL.GetCustmerSchemeInfoList();
            return Success(data);
        }

        /// <summary>
        /// 获取自定义表单数据
        /// </summary>
        /// <param name="schemeInfoId">表单模板主键</param>
        /// <param name="processIdName">流程关联字段名</param>
        /// <param name="keyValue">数据主键值</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetInstanceForm(string schemeInfoId,string processIdName, string keyValue)
        {
            if (string.IsNullOrEmpty(processIdName))
            {
                var data = formSchemeIBLL.GetInstanceForm(schemeInfoId, keyValue);
                return Success(data);
            }
            else
            {
                var data = formSchemeIBLL.GetInstanceForm(schemeInfoId, processIdName, keyValue);
                return Success(data);
            }
           
           
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="schemeInfo">表单设计模板信息</param>
        /// <param name="scheme">模板内容</param>
        /// <param name="type">类型1.正式2.草稿</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string schemeInfo, string scheme, int type)
        {
            FormSchemeInfoEntity schemeInfoEntity = schemeInfo.ToObject<FormSchemeInfoEntity>();
            FormSchemeEntity schemeEntity = new FormSchemeEntity();
            schemeEntity.F_Scheme = scheme;
            schemeEntity.F_Type = type;

            formSchemeIBLL.SaveEntity(keyValue, schemeInfoEntity, schemeEntity);
            return Success("保存成功！");
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
            formSchemeIBLL.VirtualDelete(keyValue);
            return Success("删除成功！");
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
            formSchemeIBLL.UpdateScheme(schemeInfoId, schemeId);
            return Success("更新成功！");
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
            formSchemeIBLL.UpdateState(keyValue, state);
            return Success((state == 1 ? "启用" : "禁用") + "成功！");
        }

        /// <summary>
        /// 保存自定义表单数据
        /// </summary>
        /// <param name="schemeInfoId">表单模板主键</param>
        /// <param name="processIdName">流程关联字段名</param>
        /// <param name="keyValue">数据主键值</param>
        /// <param name="formData">自定义表单数据</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        [ValidateInput(false)]
        public ActionResult SaveInstanceForm(string schemeInfoId, string processIdName, string keyValue, string formData)
        {
            formSchemeIBLL.SaveInstanceForm(schemeInfoId, processIdName, keyValue, formData);
            return Success("保存成功！");
        }
       

        /// <summary>
        /// 删除自定义表单数据
        /// </summary>
        /// <param name="schemeInfoId">表单模板主键</param>
        /// <param name="keyValue">数据主键值</param>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteInstanceForm(string schemeInfoId, string keyValue)
        {
            formSchemeIBLL.DeleteInstanceForm(schemeInfoId, keyValue);
            return Success("删除成功！");
        }
        #endregion
    }
}