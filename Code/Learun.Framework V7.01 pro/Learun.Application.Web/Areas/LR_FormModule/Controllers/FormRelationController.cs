using Learun.Application.Base.SystemModule;
using Learun.Application.Form;
using Learun.Util;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_FormModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.01
    /// 描 述：表单关联功能
    /// </summary>
    public class FormRelationController : MvcControllerBase
    {
        private FormRelationIBLL formRelationIBLL = new FormRelationBLL();
        private ModuleIBLL moduleIBLL = new ModuleBLL();
        private FormSchemeIBLL formSchemeIBLL = new FormSchemeBLL();


        #region 视图功能
        /// <summary>
        /// 主页面
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
        /// <summary>
        /// 添加条件查询字段
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult QueryFieldForm()
        {
            return View();
        }
        /// <summary>
        /// 列表设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ColFieldForm() {
            return View();
        }

        /// <summary>
        /// 发布的功能页面（主页面）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PreviewIndex(string id)
        {
            string currentUrl = (string)WebHelper.GetHttpItems("currentUrl");
            currentUrl = currentUrl + "?id=" + id;
            WebHelper.UpdateHttpItem("currentUrl", currentUrl);

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
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = formRelationIBLL.GetPageList(paginationobj, keyword);
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
        /// 获取关系数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetFormData(string keyValue)
        {
            var relation = formRelationIBLL.GetEntity(keyValue);
            var module = moduleIBLL.GetModuleEntity(relation.F_ModuleId);

            var jsonData = new
            {
                relation = relation,
                module = module
            };
            return Success(jsonData);
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="relationJson"></param>
        /// <param name="moduleJson"></param>
        /// <param name="moduleColumnJson"></param>
        /// <param name="moduleFormJson"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string relationJson, string moduleJson, string moduleColumnJson,string moduleFormJson)
        {
            FormRelationEntity formRelationEntity = relationJson.ToObject<FormRelationEntity>();
            ModuleEntity moduleEntity = moduleJson.ToObject<ModuleEntity>();
            moduleEntity.F_IsMenu = 1;
            moduleEntity.F_EnabledMark = 1;
            if (string.IsNullOrEmpty(keyValue))// 新增
            {
                formRelationEntity.Create();
                moduleEntity.F_Target = "iframe";
                moduleEntity.F_UrlAddress = "/LR_FormModule/FormRelation/PreviewIndex?id=" + formRelationEntity.F_Id;
            }

            List<ModuleButtonEntity> moduleButtonList = new List<ModuleButtonEntity>();
            ModuleButtonEntity addButtonEntity = new ModuleButtonEntity();
            addButtonEntity.Create();
            addButtonEntity.F_EnCode = "lr_add";
            addButtonEntity.F_FullName = "新增";
            moduleButtonList.Add(addButtonEntity);
            ModuleButtonEntity editButtonEntity = new ModuleButtonEntity();
            editButtonEntity.Create();
            editButtonEntity.F_EnCode = "lr_edit";
            editButtonEntity.F_FullName = "编辑";
            moduleButtonList.Add(editButtonEntity);
            ModuleButtonEntity deleteButtonEntity = new ModuleButtonEntity();
            deleteButtonEntity.Create();
            deleteButtonEntity.F_EnCode = "lr_delete";
            deleteButtonEntity.F_FullName = "删除";
            moduleButtonList.Add(deleteButtonEntity);
            ModuleButtonEntity printButtonEntity = new ModuleButtonEntity();
            printButtonEntity.Create();
            printButtonEntity.F_EnCode = "lr_print";
            printButtonEntity.F_FullName = "打印";
            moduleButtonList.Add(printButtonEntity);

            List<ModuleColumnEntity> moduleColumnList = moduleColumnJson.ToObject<List<ModuleColumnEntity>>();
            List<ModuleFormEntity> moduleFormEntitys = moduleFormJson.ToObject<List<ModuleFormEntity>>();

            moduleIBLL.SaveEntity(formRelationEntity.F_ModuleId, moduleEntity, moduleButtonList, moduleColumnList, moduleFormEntitys);
            formRelationEntity.F_ModuleId = moduleEntity.F_ModuleId;
            formRelationIBLL.SaveEntity(keyValue, formRelationEntity);

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
            var relation = formRelationIBLL.GetEntity(keyValue);
            formRelationIBLL.DeleteEntity(keyValue);
            moduleIBLL.Delete(relation.F_ModuleId);
            return Success("删除成功！");
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 获取自定义表单设置内容和表单模板
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public ActionResult GetCustmerFormData(string keyValue)
        {
            var relation = formRelationIBLL.GetEntity(keyValue);

            FormSchemeInfoEntity schemeInfoEntity = formSchemeIBLL.GetSchemeInfoEntity(relation.F_FormId);
            FormSchemeEntity schemeEntity = formSchemeIBLL.GetSchemeEntity(schemeInfoEntity.F_SchemeId);

            var jsonData = new
            {
                relation = relation,
                schemeInfo = schemeInfoEntity,
                scheme = schemeEntity
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyValue">主键</param>
        /// <param name="queryJson">关键字</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPreviewPageList(string pagination, string keyValue, string queryJson)
        {
            var relation = formRelationIBLL.GetEntity(keyValue);
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = formSchemeIBLL.GetFormPageList(relation.F_FormId, paginationobj, queryJson);
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
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPreviewList(string keyValue, string queryJson)
        {
            var relation = formRelationIBLL.GetEntity(keyValue);
            var data = formSchemeIBLL.GetFormList(relation.F_FormId, queryJson);
            return Success(data);
        }
        #endregion
    }
}