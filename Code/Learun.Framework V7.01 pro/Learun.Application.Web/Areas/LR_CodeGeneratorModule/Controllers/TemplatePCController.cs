using Learun.Application.AppMagager;
using Learun.Application.Base.CodeGeneratorModule;
using Learun.Application.Base.SystemModule;
using Learun.Application.BaseModule.CodeGeneratorModule;
using Learun.Util;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_CodeGeneratorModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.09
    /// 描 述：PC端代码生成器
    /// </summary>
    public class TemplatePCController : MvcControllerBase
    {
        private CodeGenerator codeGenerator = new CodeGenerator();
        private CodeGeneratorApp codeGeneratorApp = new CodeGeneratorApp();

        private ModuleIBLL moduleIBLL = new ModuleBLL();
        private DatabaseTableIBLL databaseTableIBLL = new DatabaseTableBLL();
        private FunctionIBLL functionIBLL = new FunctionBLL();


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
        /// 自定义开发模板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CustmerCodeIndex()
        {
            ViewBag.rootDirectory = GetRootDirectory();
            ViewBag.mappingDirectory = ConfigurationManager.AppSettings["MappingDirectory"].ToString();
            ViewBag.serviceDirectory = ConfigurationManager.AppSettings["ServiceDirectory"].ToString();
            ViewBag.webDirectory = ConfigurationManager.AppSettings["WebDirectory"].ToString();

            return View();
        }
        /// <summary>
        /// 添加扩展按钮
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddBtnForm()
        { 
            return View();
        }

        /// <summary>
        /// 快速开发模板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FastCodeIndex()
        {
            ViewBag.rootDirectory = GetRootDirectory();
            ViewBag.mappingDirectory = ConfigurationManager.AppSettings["MappingDirectory"].ToString();
            ViewBag.serviceDirectory = ConfigurationManager.AppSettings["ServiceDirectory"].ToString();
            ViewBag.webDirectory = ConfigurationManager.AppSettings["WebDirectory"].ToString();
            return View();
        }
        /// <summary>
        /// 快速生成类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult EntityCodeIndex()
        {
            ViewBag.mappingDirectory = ConfigurationManager.AppSettings["MappingDirectory"].ToString();
            ViewBag.serviceDirectory = ConfigurationManager.AppSettings["ServiceDirectory"].ToString();
            ViewBag.rootDirectory = GetRootDirectory();
            return View();
        }

        /// <summary>
        /// 流程系统表单开发模板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult WorkflowCodeIndex()
        {
            ViewBag.rootDirectory = GetRootDirectory();
            ViewBag.mappingDirectory = ConfigurationManager.AppSettings["MappingDirectory"].ToString();
            ViewBag.serviceDirectory = ConfigurationManager.AppSettings["ServiceDirectory"].ToString();
            ViewBag.webDirectory = ConfigurationManager.AppSettings["WebDirectory"].ToString();

            return View();
        }


        /// <summary>
        /// 编辑列表开发模板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GridEditCodeIndex()
        {
            ViewBag.rootDirectory = GetRootDirectory();
            ViewBag.mappingDirectory = ConfigurationManager.AppSettings["MappingDirectory"].ToString();
            ViewBag.serviceDirectory = ConfigurationManager.AppSettings["ServiceDirectory"].ToString();
            ViewBag.webDirectory = ConfigurationManager.AppSettings["WebDirectory"].ToString();

            return View();
        }

        /// <summary>
        /// 报表开发模板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ReportCodeIndex()
        {
            ViewBag.rootDirectory = GetRootDirectory();
            ViewBag.mappingDirectory = ConfigurationManager.AppSettings["MappingDirectory"].ToString();
            ViewBag.serviceDirectory = ConfigurationManager.AppSettings["ServiceDirectory"].ToString();
            ViewBag.webDirectory = ConfigurationManager.AppSettings["WebDirectory"].ToString();

            return View();
        }

        /// <summary>
        /// 移动端开发模板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AppCustmerCodeIndex()
        {
            ViewBag.rootDirectory = GetRootDirectory();
            ViewBag.mappingDirectory = ConfigurationManager.AppSettings["MappingDirectory"].ToString();
            ViewBag.serviceDirectory = ConfigurationManager.AppSettings["ServiceDirectory"].ToString();
            ViewBag.webDirectory = ConfigurationManager.AppSettings["WebDirectory"].ToString();
            ViewBag.apiDirectory = ConfigurationManager.AppSettings["ApiDirectory"].ToString();
            ViewBag.appDirectory = ConfigurationManager.AppSettings["AppDirectory"].ToString();

            return View();
        }

        #endregion

        #region 自定义开发模板
        /// <summary>
        /// 自定义开发模板代码生成
        /// </summary>
        /// <param name="databaseLinkId">数据库连接ID</param>
        /// <param name="dbTable">数据表</param>
        /// <param name="formData">表单数据</param>
        /// <param name="queryData">查询数据</param>
        /// <param name="colData">列表数据</param>
        /// <param name="baseInfo">基础信息</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult LookCustmerCode(string databaseLinkId, string dbTable, string formData, string queryData, string colData, string baseInfo)
        {
            // 数据实体化
            List<DbTableModel> dbTableObj = dbTable.ToObject<List<DbTableModel>>();

            List<CodeFormTabModel> formDataObj = formData.ToObject<List<CodeFormTabModel>>();
            QueryModel queryDataObj = queryData.ToObject<QueryModel>();
            ColModel colDataObj = colData.ToObject<ColModel>();
            BaseModel baseInfoObj = baseInfo.ToObject<BaseModel>();

            // 将表单数据进行一次转化
            Dictionary<string, CodeFormCompontModel> compontMap = new Dictionary<string, CodeFormCompontModel>();
            Dictionary<string, CodeFormCompontModel> compontMap2 = new Dictionary<string, CodeFormCompontModel>();
            foreach (var tab in formDataObj)
            {
                foreach (var compont in tab.componts)
                {
                    if (!string.IsNullOrEmpty(compont.table)) {
                        compontMap.Add(compont.table + compont.field, compont);
                        compontMap2.Add(compont.id, compont);
                    }
                }
            }

            // 实体类 映射类
            string entityCode = "";
            string mapCode = "";

            string mainTable = "";
            foreach (var tableOne in dbTableObj)
            {
                bool isMain = false;
                if (string.IsNullOrEmpty(tableOne.relationName))
                {
                    mainTable = tableOne.name;
                    isMain = true;
                }
                // 实体类
                entityCode += codeGenerator.EntityCreate(databaseLinkId, tableOne.name, tableOne.pk, baseInfoObj, colDataObj, isMain);
                // 映射类
                mapCode += codeGenerator.MappingCreate(tableOne.name, tableOne.pk, baseInfoObj);
            }
            // 服务类
            string serviceCode = codeGenerator.ServiceCreate(databaseLinkId, dbTableObj, compontMap, queryDataObj, colDataObj, baseInfoObj);
            // 业务类
            string bllCode = codeGenerator.BllCreate(baseInfoObj, dbTableObj, compontMap, colDataObj);
            // 业务接口类
            string ibllCode = codeGenerator.IBllCreate(baseInfoObj, dbTableObj, compontMap, colDataObj);
            // 控制器类
            string controllerCode = codeGenerator.ControllerCreate(baseInfoObj, dbTableObj, compontMap, colDataObj);
            // 页面类
            string indexCode = codeGenerator.IndexCreate(baseInfoObj, compontMap, queryDataObj, colDataObj);
            // 页面js类
            string indexJsCode = codeGenerator.IndexJSCreate(baseInfoObj, dbTableObj, compontMap, colDataObj, queryDataObj);
            // 表单类
            string formCode = codeGenerator.FormCreate(baseInfoObj, formDataObj, compontMap);
            // 表单js类
            string formJsCode = codeGenerator.FormJsCreate(baseInfoObj, dbTableObj, formDataObj, compontMap2);

            var jsonData = new
            {
                entityCode = entityCode,
                mapCode = mapCode,
                serviceCode = serviceCode,
                bllCode = bllCode,
                ibllCode = ibllCode,
                controllerCode = controllerCode,
                indexCode = indexCode,
                indexJsCode = indexJsCode,
                formCode = formCode,
                formJsCode = formJsCode
            };

            return Success(jsonData);
        }
        /// <summary>
        /// 自定义开发模板代码生成(创建)
        /// </summary>
        /// <param name="databaseLinkId">数据库连接ID</param>
        /// <param name="dbTable">数据表</param>
        /// <param name="formData">表单数据</param>
        /// <param name="queryData">查询数据</param>
        /// <param name="colData">列表数据</param>
        /// <param name="baseInfo">基础信息</param>
        /// <param name="moduleEntityJson">系统功能配置信息</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult CreateCustmerCode(string databaseLinkId, string dbTable, string formData, string queryData, string colData, string baseInfo, string moduleEntityJson)
        {
            // 数据
            List<DbTableModel> dbTableObj = dbTable.ToObject<List<DbTableModel>>();
            List<CodeFormTabModel> formDataObj = formData.ToObject<List<CodeFormTabModel>>();
            QueryModel queryDataObj = queryData.ToObject<QueryModel>();
            ColModel colDataObj = colData.ToObject<ColModel>();
            BaseModel baseInfoObj = baseInfo.ToObject<BaseModel>();


            var moduleEntity = moduleEntityJson.ToObject<ModuleEntity>();
            moduleEntity.F_Target = "iframe";
            moduleEntity.F_UrlAddress = "/" + baseInfoObj.outputArea + "/" + baseInfoObj.name + "/Index";

            // 按钮
            List<ModuleButtonEntity> moduleButtonList = new List<ModuleButtonEntity>();
            foreach (var btn in colDataObj.btns)
            {
                switch (btn)
                {
                    case "add":
                        ModuleButtonEntity addButtonEntity = new ModuleButtonEntity();
                        addButtonEntity.Create();
                        addButtonEntity.F_EnCode = "lr_add";
                        addButtonEntity.F_FullName = "新增";
                        moduleButtonList.Add(addButtonEntity);
                        break;
                    case "edit":
                        ModuleButtonEntity editButtonEntity = new ModuleButtonEntity();
                        editButtonEntity.Create();
                        editButtonEntity.F_EnCode = "lr_edit";
                        editButtonEntity.F_FullName = "编辑";
                        moduleButtonList.Add(editButtonEntity);
                        break;
                    case "delete":
                        ModuleButtonEntity deleteButtonEntity = new ModuleButtonEntity();
                        deleteButtonEntity.Create();
                        deleteButtonEntity.F_EnCode = "lr_delete";
                        deleteButtonEntity.F_FullName = "删除";
                        moduleButtonList.Add(deleteButtonEntity);
                        break;
                    case "print":
                        ModuleButtonEntity printButtonEntity = new ModuleButtonEntity();
                        printButtonEntity.Create();
                        printButtonEntity.F_EnCode = "lr_print";
                        printButtonEntity.F_FullName = "打印";
                        moduleButtonList.Add(printButtonEntity);
                        break;
                }
            }
            foreach (var btn in colDataObj.btnexs)
            {
                ModuleButtonEntity printButtonEntity = new ModuleButtonEntity();
                printButtonEntity.Create();
                printButtonEntity.F_EnCode = btn.id;
                printButtonEntity.F_FullName = btn.name;
                moduleButtonList.Add(printButtonEntity);
            }

            // 列表
            List<ModuleColumnEntity> moduleColumnList = new List<ModuleColumnEntity>();
            foreach (var col in colDataObj.fields)
            {
                ModuleColumnEntity moduleColumnEntity = new ModuleColumnEntity();
                moduleColumnEntity.Create();
                moduleColumnEntity.F_EnCode = col.field;
                moduleColumnEntity.F_FullName = col.fieldName;
                moduleColumnEntity.F_ParentId = "0";
                moduleColumnList.Add(moduleColumnEntity);
            }

            // 表单字段
            List<ModuleFormEntity> moduleFormEntitys = new List<ModuleFormEntity>();

            // 将表单数据进行一次转化
            Dictionary<string, CodeFormCompontModel> compontMap = new Dictionary<string, CodeFormCompontModel>();
            Dictionary<string, CodeFormCompontModel> compontMap2 = new Dictionary<string, CodeFormCompontModel>();
            foreach (var tab in formDataObj)
            {
                foreach (var compont in tab.componts)
                {
                    ModuleFormEntity moduleFormEntity = new ModuleFormEntity();
                    moduleFormEntity.Create();
                    moduleFormEntity.F_EnCode = compont.field;
                    moduleFormEntity.F_FullName = compont.title;
                    moduleFormEntitys.Add(moduleFormEntity);

                    if (!string.IsNullOrEmpty(compont.table))
                    {
                        compontMap.Add(compont.table + compont.field, compont);
                        compontMap2.Add(compont.id, compont);
                    }
                }
            }

            var moduleEntityTemp = moduleIBLL.GetModuleByUrl(moduleEntity.F_UrlAddress);
            if (moduleEntityTemp == null)
            {
                moduleIBLL.SaveEntity("", moduleEntity, moduleButtonList, moduleColumnList, moduleFormEntitys);//模块功能cbb
            }

            // 实体类 映射类
            string codeContent = "";
            string mainTable = "";

            foreach (var tableOne in dbTableObj)
            {
                bool isMain = false;
                if (string.IsNullOrEmpty(tableOne.relationName))
                {
                    mainTable = tableOne.name;
                    isMain = true;
                }
                // 实体类
                codeContent = codeGenerator.EntityCreate(databaseLinkId, tableOne.name, tableOne.pk, baseInfoObj, colDataObj, isMain);
                codeGenerator.CreateEntityCodeFile(baseInfoObj, tableOne.name, codeContent);
                // 映射类
                codeContent = codeGenerator.MappingCreate(tableOne.name, tableOne.pk, baseInfoObj);
                codeGenerator.CreateMapCodeFile(baseInfoObj, tableOne.name, codeContent);
            }

            // 服务类
            codeContent = codeGenerator.ServiceCreate(databaseLinkId, dbTableObj, compontMap, queryDataObj, colDataObj, baseInfoObj);
            codeGenerator.CreateSerivceCodeFile(baseInfoObj, codeContent);
            // 业务类
            codeContent = codeGenerator.BllCreate(baseInfoObj, dbTableObj, compontMap, colDataObj);
            codeGenerator.CreateBLLCodeFile(baseInfoObj, codeContent);
            // 业务接口类
            codeContent = codeGenerator.IBllCreate(baseInfoObj, dbTableObj, compontMap, colDataObj);
            codeGenerator.CreateIBLLCodeFile(baseInfoObj, codeContent);
            // 控制器类
            codeContent = codeGenerator.ControllerCreate(baseInfoObj, dbTableObj, compontMap, colDataObj);
            codeGenerator.CreateControllerCodeFile(baseInfoObj, codeContent);
            // 页面类
            codeContent = codeGenerator.IndexCreate(baseInfoObj, compontMap, queryDataObj, colDataObj);
            codeGenerator.CreateIndexCodeFile(baseInfoObj, codeContent);
            // 页面js类
            codeContent = codeGenerator.IndexJSCreate(baseInfoObj, dbTableObj, compontMap, colDataObj, queryDataObj);
            codeGenerator.CreateIndexJSCodeFile(baseInfoObj, codeContent);
            // 表单类
            codeContent = codeGenerator.FormCreate(baseInfoObj, formDataObj, compontMap);
            codeGenerator.CreateFormCodeFile(baseInfoObj, codeContent);
            // 表单js类
            codeContent = codeGenerator.FormJsCreate(baseInfoObj, dbTableObj, formDataObj, compontMap2);
            codeGenerator.CreateFormJSCodeFile(baseInfoObj, codeContent);

            return Success("创建成功");
        }
        #endregion

        #region 快速开发模板
        /// <summary>
        /// 快速开发代码查看
        /// </summary>
        /// <param name="codeBaseConfigModel">配置信息</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult LookFastCode(CodeBaseConfigModel codeBaseConfigModel)
        {
            string entityCode = "";
            string mapCode = "";
            codeBaseConfigModel.backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
            // 实体类
            entityCode += codeGenerator.EntityCreate(codeBaseConfigModel, codeBaseConfigModel.tableName);
            // 映射类
            mapCode += codeGenerator.MappingCreate(codeBaseConfigModel, codeBaseConfigModel.tableName);
            // 服务类
            string serviceCode = codeGenerator.ServiceCreate(codeBaseConfigModel);
            // 业务类
            string bllCode = codeGenerator.BllCreate(codeBaseConfigModel);
            // 业务接口类
            string ibllCode = codeGenerator.IBllCreate(codeBaseConfigModel);
            // 控制器类
            string controllerCode = codeGenerator.ControllerCreate(codeBaseConfigModel);
            // 页面类
            string indexCode = codeGenerator.IndexCreate(codeBaseConfigModel);
            // 页面js类
            string indexJsCode = codeGenerator.IndexJSCreate(codeBaseConfigModel);
            // 表单类
            string formCode = codeGenerator.FormCreate(codeBaseConfigModel);
            // 表单js类
            string formJsCode = codeGenerator.FormJsCreate(codeBaseConfigModel);
          
            var jsonData = new
            {
                entityCode = entityCode,
                mapCode = mapCode,
                serviceCode = serviceCode,
                bllCode = bllCode,
                ibllCode = ibllCode,
                controllerCode = controllerCode,
                indexCode = indexCode,
                indexJsCode = indexJsCode,
                formCode = formCode,
                formJsCode = formJsCode
            };

            return Success(jsonData);
        }
        /// <summary>
        /// 快速开发代码创建
        /// </summary>
        /// <param name="codeBaseConfigModel">配置信息</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult CreateFastCode(CodeBaseConfigModel codeBaseConfigModel)
        {
            string codeContent = "";
            codeBaseConfigModel.backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
            // 实体类
            codeContent = codeGenerator.EntityCreate(codeBaseConfigModel, codeBaseConfigModel.tableName);
            codeGenerator.CreateEntityCodeFile(codeBaseConfigModel, codeBaseConfigModel.tableName, codeContent);
            // 映射类
            codeContent = codeGenerator.MappingCreate(codeBaseConfigModel, codeBaseConfigModel.tableName);
            codeGenerator.CreateMapCodeFile(codeBaseConfigModel, codeBaseConfigModel.tableName, codeContent);
            // 服务类
            codeContent = codeGenerator.ServiceCreate(codeBaseConfigModel);
            codeGenerator.CreateSerivceCodeFile(codeBaseConfigModel, codeContent);
            // 业务接口类
            codeContent = codeGenerator.IBllCreate(codeBaseConfigModel);
            codeGenerator.CreateIBLLCodeFile(codeBaseConfigModel, codeContent);
            // 业务类
            codeContent = codeGenerator.BllCreate(codeBaseConfigModel);
            codeGenerator.CreateBLLCodeFile(codeBaseConfigModel, codeContent);
            // 控制器类
            codeContent = codeGenerator.ControllerCreate(codeBaseConfigModel);
            codeGenerator.CreateControllerCodeFile(codeBaseConfigModel, codeContent);
            // 页面类
            codeContent = codeGenerator.IndexCreate(codeBaseConfigModel);
            codeGenerator.CreateIndexCodeFile(codeBaseConfigModel, codeContent);
            // 页面js类
            codeContent = codeGenerator.IndexJSCreate(codeBaseConfigModel);
            codeGenerator.CreateIndexJSCodeFile(codeBaseConfigModel, codeContent);
            // 表单类
            codeContent = codeGenerator.FormCreate(codeBaseConfigModel);
            codeGenerator.CreateFormCodeFile(codeBaseConfigModel, codeContent);
            // 表单js类
            codeContent = codeGenerator.FormJsCreate(codeBaseConfigModel);
            codeGenerator.CreateFormJSCodeFile(codeBaseConfigModel, codeContent);

            var moduleEntity = codeBaseConfigModel.moduleEntityJson.ToObject<ModuleEntity>();
            moduleEntity.F_Target = "iframe";
            moduleEntity.F_UrlAddress = "/" + codeBaseConfigModel.area + "/" + codeBaseConfigModel.name + "/Index";

            List<ModuleButtonEntity> moduleButtonList = new List<ModuleButtonEntity>();
            ModuleButtonEntity addButtonEntity = new ModuleButtonEntity();
            addButtonEntity.Create();
            addButtonEntity.F_ActionAddress = "/" + codeBaseConfigModel.area + "/" + codeBaseConfigModel.name + "/Form";
            addButtonEntity.F_EnCode = "lr_add";
            addButtonEntity.F_FullName = "新增";
            moduleButtonList.Add(addButtonEntity);
            ModuleButtonEntity editButtonEntity = new ModuleButtonEntity();
            editButtonEntity.Create();
            editButtonEntity.F_ActionAddress = "/" + codeBaseConfigModel.area + "/" + codeBaseConfigModel.name + "/Form";
            editButtonEntity.F_EnCode = "lr_edit";
            editButtonEntity.F_FullName = "编辑";
            moduleButtonList.Add(editButtonEntity);
            ModuleButtonEntity deleteButtonEntity = new ModuleButtonEntity();
            deleteButtonEntity.Create();
            deleteButtonEntity.F_ActionAddress = "/" + codeBaseConfigModel.area + "/" + codeBaseConfigModel.name + "/DeleteForm";
            deleteButtonEntity.F_EnCode = "lr_delete";
            deleteButtonEntity.F_FullName = "删除";
            moduleButtonList.Add(deleteButtonEntity);

            List<ModuleColumnEntity> moduleColumnList = new List<ModuleColumnEntity>();
            IEnumerable<DatabaseTableFieldModel> fieldList = databaseTableIBLL.GetTableFiledList(codeBaseConfigModel.databaseLinkId, codeBaseConfigModel.tableName);
            // 表单字段
            List<ModuleFormEntity> moduleFormEntitys = new List<ModuleFormEntity>();
            foreach (var fileditem in fieldList)
            {
                ModuleColumnEntity moduleColumnEntity = new ModuleColumnEntity();
                moduleColumnEntity.Create();
                moduleColumnEntity.F_EnCode = fileditem.f_column;
                moduleColumnEntity.F_FullName = fileditem.f_remark;
                moduleColumnEntity.F_ParentId = "0";

                ModuleFormEntity moduleFormEntity = new ModuleFormEntity();
                moduleFormEntity.Create();
                moduleFormEntity.F_EnCode = fileditem.f_column;
                moduleFormEntity.F_FullName = fileditem.f_remark;
                moduleFormEntitys.Add(moduleFormEntity);

                moduleColumnList.Add(moduleColumnEntity);
            }
           


            moduleIBLL.SaveEntity("", moduleEntity, moduleButtonList, moduleColumnList, moduleFormEntitys);

            return Success("创建成功");
        }
        #endregion

        #region 快速生成实体类和映射类
        /// <summary>
        /// 快速开发代码查看
        /// </summary>
        /// <param name="codeBaseConfigModel">配置信息</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult LookEntityCode(CodeBaseConfigModel codeBaseConfigModel)
        {
            string entityCode = "";
            string mapCode = "";
            codeBaseConfigModel.backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
            string[] tableNameList = codeBaseConfigModel.tableNames.Split(',');
            foreach (string tableName in tableNameList)
            {
                // 实体类
                entityCode += codeGenerator.EntityCreate(codeBaseConfigModel, tableName);
                // 映射类
                mapCode += codeGenerator.MappingCreate(codeBaseConfigModel, tableName);
            }
            var jsonData = new
            {
                entityCode = entityCode,
                mapCode = mapCode
            };

            return Success(jsonData);
        }
        /// <summary>
        /// 快速开发代码创建
        /// </summary>
        /// <param name="codeBaseConfigModel">配置信息</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult CreateEntityCode(CodeBaseConfigModel codeBaseConfigModel)
        {
            string codeContent = "";
            codeBaseConfigModel.backProject = ConfigurationManager.AppSettings["BackProject"].ToString();
            string[] tableNameList = codeBaseConfigModel.tableNames.Split(',');
            foreach (string tableName in tableNameList)
            {
                // 实体类
                codeContent = codeGenerator.EntityCreate(codeBaseConfigModel, tableName);
                codeGenerator.CreateEntityCodeFile(codeBaseConfigModel, tableName, codeContent);
                // 映射类
                codeContent = codeGenerator.MappingCreate(codeBaseConfigModel, tableName);
                codeGenerator.CreateMapCodeFile(codeBaseConfigModel, tableName, codeContent);
            }
            return Success("创建成功");
        }
        #endregion

        #region  流程系统表单模板
        /// <summary>
        /// 流程系统表单代码生成
        /// </summary>
        /// <param name="codeBaseConfigModel">配置信息</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult LookWorkflowCode(string databaseLinkId, string dbTable, string formData, string queryData, string colData, string baseInfo)
        {
            // 数据
            List<DbTableModel> dbTableObj = dbTable.ToObject<List<DbTableModel>>();
            List<CodeFormTabModel> formDataObj = formData.ToObject<List<CodeFormTabModel>>();
            QueryModel queryDataObj = queryData.ToObject<QueryModel>();
            ColModel colDataObj = colData.ToObject<ColModel>();
            BaseModel baseInfoObj = baseInfo.ToObject<BaseModel>();

            // 将表单数据进行一次转化
            Dictionary<string, CodeFormCompontModel> compontMap = new Dictionary<string, CodeFormCompontModel>();
            Dictionary<string, CodeFormCompontModel> compontMap2 = new Dictionary<string, CodeFormCompontModel>();
            foreach (var tab in formDataObj)
            {
                foreach (var compont in tab.componts)
                {
                    if (!string.IsNullOrEmpty(compont.table))
                    {
                        compontMap.Add(compont.table + compont.field, compont);
                        compontMap2.Add(compont.id, compont);
                    }
                }
            }

            // 实体类 映射类
            string entityCode = "";
            string mapCode = "";

            string mainTable = "";

            foreach (var tableOne in dbTableObj)
            {
                bool isMain = false;
                if (string.IsNullOrEmpty(tableOne.relationName))
                {
                    mainTable = tableOne.name;
                    isMain = true;
                }
                // 实体类
                entityCode += codeGenerator.WfEntityCreate(databaseLinkId, tableOne.name, tableOne.pk, baseInfoObj, colDataObj, isMain, colDataObj.workField);
                // colDataObj
                mapCode += codeGenerator.MappingCreate(tableOne.name, tableOne.pk, baseInfoObj);
            }

            // 服务类
            string serviceCode = codeGenerator.WfServiceCreate(databaseLinkId, dbTableObj, compontMap, queryDataObj, colDataObj, baseInfoObj, colDataObj.workField);
            // 业务类
            string bllCode = codeGenerator.WfBllCreate(baseInfoObj, dbTableObj, compontMap, colDataObj);
            // 业务接口类
            string ibllCode = codeGenerator.WfIBllCreate(baseInfoObj, dbTableObj, compontMap, colDataObj);
            // 控制器类
            string controllerCode = codeGenerator.WfControllerCreate(baseInfoObj, dbTableObj, compontMap, colDataObj);
            // 页面类
            string indexCode = codeGenerator.IndexCreate(baseInfoObj, compontMap, queryDataObj, colDataObj);
            // 页面js类
            string indexJsCode = codeGenerator.WfIndexJSCreate(baseInfoObj, dbTableObj, compontMap, colDataObj, queryDataObj);
            // 表单类
            string formCode = codeGenerator.FormCreate(baseInfoObj, formDataObj, compontMap);
            // 表单js类
            string formJsCode = codeGenerator.WfFormJsCreate(baseInfoObj, dbTableObj, formDataObj, compontMap2, colDataObj.workField);


            var jsonData = new
            {
                entityCode = entityCode,
                mapCode = mapCode,
                serviceCode = serviceCode,
                bllCode = bllCode,
                ibllCode = ibllCode,
                controllerCode = controllerCode,
                indexCode = indexCode,
                indexJsCode = indexJsCode,
                formCode = formCode,
                formJsCode = formJsCode
            };

            return Success(jsonData);
        }
        /// <summary>
        /// 流程系统表单代码创建
        /// </summary>
        /// <param name="codeBaseConfigModel">配置信息</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult CreateWorkflowCode(string databaseLinkId, string dbTable, string formData, string queryData, string colData, string baseInfo, string moduleEntityJson)
        {
            // 数据
            List<DbTableModel> dbTableObj = dbTable.ToObject<List<DbTableModel>>();
            List<CodeFormTabModel> formDataObj = formData.ToObject<List<CodeFormTabModel>>();
            QueryModel queryDataObj = queryData.ToObject<QueryModel>();
            ColModel colDataObj = colData.ToObject<ColModel>();
            BaseModel baseInfoObj = baseInfo.ToObject<BaseModel>();


            var moduleEntity = moduleEntityJson.ToObject<ModuleEntity>();
            moduleEntity.F_Target = "iframe";
            moduleEntity.F_UrlAddress = "/" + baseInfoObj.outputArea + "/" + baseInfoObj.name + "/Index";

            // 按钮
            List<ModuleButtonEntity> moduleButtonList = new List<ModuleButtonEntity>();
            foreach (var btn in colDataObj.btns)
            {
                switch (btn)
                {
                    case "add":
                        ModuleButtonEntity addButtonEntity = new ModuleButtonEntity();
                        addButtonEntity.Create();
                        addButtonEntity.F_EnCode = "lr_add";
                        addButtonEntity.F_FullName = "新增";
                        moduleButtonList.Add(addButtonEntity);
                        break;
                    case "edit":
                        ModuleButtonEntity editButtonEntity = new ModuleButtonEntity();
                        editButtonEntity.Create();
                        editButtonEntity.F_EnCode = "lr_edit";
                        editButtonEntity.F_FullName = "编辑";
                        moduleButtonList.Add(editButtonEntity);
                        break;
                    case "delete":
                        ModuleButtonEntity deleteButtonEntity = new ModuleButtonEntity();
                        deleteButtonEntity.Create();
                        deleteButtonEntity.F_EnCode = "lr_delete";
                        deleteButtonEntity.F_FullName = "删除";
                        moduleButtonList.Add(deleteButtonEntity);
                        break;
                    case "print":
                        ModuleButtonEntity printButtonEntity = new ModuleButtonEntity();
                        printButtonEntity.Create();
                        printButtonEntity.F_EnCode = "lr_print";
                        printButtonEntity.F_FullName = "打印";
                        moduleButtonList.Add(printButtonEntity);
                        break;
                }
            }
            foreach (var btn in colDataObj.btnexs)
            {
                ModuleButtonEntity printButtonEntity = new ModuleButtonEntity();
                printButtonEntity.Create();
                printButtonEntity.F_EnCode = btn.id;
                printButtonEntity.F_FullName = btn.name;
                moduleButtonList.Add(printButtonEntity);
            }

            // 列表
            List<ModuleColumnEntity> moduleColumnList = new List<ModuleColumnEntity>();
            foreach (var col in colDataObj.fields)
            {
                ModuleColumnEntity moduleColumnEntity = new ModuleColumnEntity();
                moduleColumnEntity.Create();
                moduleColumnEntity.F_EnCode = col.field;
                moduleColumnEntity.F_FullName = col.fieldName;
                moduleColumnEntity.F_ParentId = "0";
                moduleColumnList.Add(moduleColumnEntity);
            }

            // 表单字段
            List<ModuleFormEntity> moduleFormEntitys = new List<ModuleFormEntity>();

            // 将表单数据进行一次转化
            Dictionary<string, CodeFormCompontModel> compontMap = new Dictionary<string, CodeFormCompontModel>();
            Dictionary<string, CodeFormCompontModel> compontMap2 = new Dictionary<string, CodeFormCompontModel>();
            foreach (var tab in formDataObj)
            {
                foreach (var compont in tab.componts)
                {
                    ModuleFormEntity moduleFormEntity = new ModuleFormEntity();
                    moduleFormEntity.Create();
                    moduleFormEntity.F_EnCode = compont.field;
                    moduleFormEntity.F_FullName = compont.title;
                    moduleFormEntitys.Add(moduleFormEntity);

                    if (!string.IsNullOrEmpty(compont.table))
                    {
                        compontMap.Add(compont.table + compont.field, compont);
                        compontMap2.Add(compont.id, compont);
                    }
                }
            }

            var moduleEntityTemp = moduleIBLL.GetModuleByUrl(moduleEntity.F_UrlAddress);
            if (moduleEntityTemp == null)
            {
                moduleIBLL.SaveEntity("", moduleEntity, moduleButtonList, moduleColumnList, moduleFormEntitys);
            }


            // 实体类 映射类
            string codeContent = "";
            string mainTable = "";

            foreach (var tableOne in dbTableObj)
            {
                bool isMain = false;
                if (string.IsNullOrEmpty(tableOne.relationName))
                {
                    mainTable = tableOne.name;
                    isMain = true;
                }
                // 实体类
                codeContent = codeGenerator.WfEntityCreate(databaseLinkId, tableOne.name, tableOne.pk, baseInfoObj, colDataObj, isMain, colDataObj.workField);
                codeGenerator.CreateEntityCodeFile(baseInfoObj, tableOne.name, codeContent);
                // 映射类
                codeContent = codeGenerator.MappingCreate(tableOne.name, tableOne.pk, baseInfoObj);
                codeGenerator.CreateMapCodeFile(baseInfoObj, tableOne.name, codeContent);
            }

            // 服务类
            codeContent = codeGenerator.WfServiceCreate(databaseLinkId, dbTableObj, compontMap, queryDataObj, colDataObj, baseInfoObj, colDataObj.workField);
            codeGenerator.CreateSerivceCodeFile(baseInfoObj, codeContent);
            // 业务类
            codeContent = codeGenerator.WfBllCreate(baseInfoObj, dbTableObj, compontMap, colDataObj);
            codeGenerator.CreateBLLCodeFile(baseInfoObj, codeContent);
            // 业务接口类
            codeContent = codeGenerator.WfIBllCreate(baseInfoObj, dbTableObj, compontMap, colDataObj);
            codeGenerator.CreateIBLLCodeFile(baseInfoObj, codeContent);
            // 控制器类
            codeContent = codeGenerator.WfControllerCreate(baseInfoObj, dbTableObj, compontMap, colDataObj);
            codeGenerator.CreateControllerCodeFile(baseInfoObj, codeContent);
            // 页面类
            codeContent = codeGenerator.IndexCreate(baseInfoObj, compontMap, queryDataObj, colDataObj);
            codeGenerator.CreateIndexCodeFile(baseInfoObj, codeContent);
            // 页面js类
            codeContent = codeGenerator.WfIndexJSCreate(baseInfoObj, dbTableObj, compontMap, colDataObj, queryDataObj);
            codeGenerator.CreateIndexJSCodeFile(baseInfoObj, codeContent);
            // 表单类
            codeContent = codeGenerator.FormCreate(baseInfoObj, formDataObj, compontMap);
            codeGenerator.CreateFormCodeFile(baseInfoObj, codeContent);
            // 表单js类
            codeContent = codeGenerator.WfFormJsCreate(baseInfoObj, dbTableObj, formDataObj, compontMap2, colDataObj.workField);
            codeGenerator.CreateFormJSCodeFile(baseInfoObj, codeContent);

            return Success("创建成功");
        }

        #endregion

        #region 移动开发模板
        /// <summary>
        /// 移动开发模板代码生成
        /// </summary>
        /// <param name="databaseLinkId">数据库连接ID</param>
        /// <param name="dbTable">数据表</param>
        /// <param name="formData">表单数据</param>
        /// <param name="queryData">查询数据</param>
        /// <param name="colData">列表数据</param>
        /// <param name="baseInfo">基础信息</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult LookAppCustmerCode(string databaseLinkId, string dbTable, string formData, string queryData, string colData, string baseInfo)
        {
            // 数据实体化
            List<DbTableModel> dbTableObj = dbTable.ToObject<List<DbTableModel>>();

            List<CodeFormTabModel> formDataObj = formData.ToObject<List<CodeFormTabModel>>();
            QueryModel queryDataObj = queryData.ToObject<QueryModel>();
            ColModel colDataObj = colData.ToObject<ColModel>();
            BaseModel baseInfoObj = baseInfo.ToObject<BaseModel>();

            // 将表单数据进行一次转化
            Dictionary<string, CodeFormCompontModel> compontMap = new Dictionary<string, CodeFormCompontModel>();
            Dictionary<string, CodeFormCompontModel> compontMap2 = new Dictionary<string, CodeFormCompontModel>();
            foreach (var tab in formDataObj)
            {
                foreach (var compont in tab.componts)
                {
                    if (!string.IsNullOrEmpty(compont.table))
                    {
                        compontMap.Add(compont.table + compont.field, compont);
                        compontMap2.Add(compont.id, compont);
                    }
                }
            }

            // 实体类 映射类
            string entityCode = "";
            string mapCode = "";

            string mainTable = "";
            foreach (var tableOne in dbTableObj)
            {
                bool isMain = false;
                if (string.IsNullOrEmpty(tableOne.relationName))
                {
                    mainTable = tableOne.name;
                    isMain = true;
                }
                // 实体类
                entityCode += codeGeneratorApp.EntityCreate(databaseLinkId, tableOne.name, tableOne.pk, baseInfoObj, colDataObj, isMain);
                // 映射类
                mapCode += codeGeneratorApp.MappingCreate(tableOne.name, tableOne.pk, baseInfoObj);
            }
            // 服务类
            string serviceCode = codeGeneratorApp.ServiceCreate(databaseLinkId, dbTableObj, compontMap, queryDataObj, colDataObj, baseInfoObj);
            // 业务类
            string bllCode = codeGeneratorApp.BllCreate(baseInfoObj, dbTableObj, compontMap, colDataObj);
            // 业务接口类
            string ibllCode = codeGeneratorApp.IBllCreate(baseInfoObj, dbTableObj, compontMap, colDataObj);
            // 控制器类
            string controllerCode = codeGeneratorApp.ControllerCreate(baseInfoObj, dbTableObj, compontMap, colDataObj);
            // 页面类
            string indexCode = codeGeneratorApp.IndexCreate(baseInfoObj, compontMap, queryDataObj, colDataObj);
            // 页面js类
            string indexJsCode = codeGeneratorApp.IndexJSCreate(baseInfoObj, dbTableObj, compontMap, colDataObj, queryDataObj);
            // 表单类
            string formCode = codeGeneratorApp.FormCreate(baseInfoObj, formDataObj, compontMap);
            // 表单js类
            string formJsCode = codeGeneratorApp.FormJsCreate(baseInfoObj, dbTableObj, formDataObj, compontMap2);

            // api接口类
            string apiCode = codeGeneratorApp.ApiCreate(baseInfoObj, dbTableObj, compontMap, colDataObj);
            // 移动页面
            string appIndexCode = codeGeneratorApp.AppIndexCreate(baseInfoObj, compontMap, queryDataObj);

            // 移动页面JS
            string appIndexJsCode = codeGeneratorApp.AppIndexJSCreate(baseInfoObj, dbTableObj, compontMap, colDataObj, queryDataObj);

            // 移动页面表单
            string appFormCode = codeGeneratorApp.AppFormCreate(baseInfoObj, formDataObj, compontMap);

            // 移动表单页面JS
            string appFormJsCode = codeGeneratorApp.AppFormJsCreate(baseInfoObj, dbTableObj, formDataObj, compontMap2);

            var jsonData = new
            {
                entityCode,
                mapCode,
                serviceCode,
                bllCode,
                ibllCode,
                controllerCode,
                indexCode,
                indexJsCode,
                formCode,
                formJsCode,

                apiCode,
                appIndexCode = appIndexCode + appIndexJsCode,
                appFormCode = appFormCode + appFormJsCode
            };

            return Success(jsonData);
        }
        /// <summary>
        /// 自定义开发模板代码生成(创建)
        /// </summary>
        /// <param name="databaseLinkId">数据库连接ID</param>
        /// <param name="dbTable">数据表</param>
        /// <param name="formData">表单数据</param>
        /// <param name="queryData">查询数据</param>
        /// <param name="colData">列表数据</param>
        /// <param name="baseInfo">基础信息</param>
        /// <param name="moduleEntityJson">系统功能配置信息</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult CreateAppCustmerCode(string databaseLinkId, string dbTable, string formData, string queryData, string colData, string baseInfo, string moduleEntityJson,string functionEntityJson)
        {
            // 数据
            List<DbTableModel> dbTableObj = dbTable.ToObject<List<DbTableModel>>();
            List<CodeFormTabModel> formDataObj = formData.ToObject<List<CodeFormTabModel>>();
            QueryModel queryDataObj = queryData.ToObject<QueryModel>();
            ColModel colDataObj = colData.ToObject<ColModel>();
            BaseModel baseInfoObj = baseInfo.ToObject<BaseModel>();

            FunctionEntity functionEntity = functionEntityJson.ToObject<FunctionEntity>();
            functionEntity.F_Url = baseInfoObj.outputArea + "/" + baseInfoObj.name;
            functionIBLL.SaveEntity("", functionEntity, null);



            var moduleEntity = moduleEntityJson.ToObject<ModuleEntity>();
            moduleEntity.F_Target = "iframe";
            moduleEntity.F_UrlAddress = "/" + baseInfoObj.outputArea + "/" + baseInfoObj.name + "/Index";

            // 按钮
            List<ModuleButtonEntity> moduleButtonList = new List<ModuleButtonEntity>();
            foreach (var btn in colDataObj.btns)
            {
                switch (btn)
                {
                    case "add":
                        ModuleButtonEntity addButtonEntity = new ModuleButtonEntity();
                        addButtonEntity.Create();
                        addButtonEntity.F_EnCode = "lr_add";
                        addButtonEntity.F_FullName = "新增";
                        moduleButtonList.Add(addButtonEntity);
                        break;
                    case "edit":
                        ModuleButtonEntity editButtonEntity = new ModuleButtonEntity();
                        editButtonEntity.Create();
                        editButtonEntity.F_EnCode = "lr_edit";
                        editButtonEntity.F_FullName = "编辑";
                        moduleButtonList.Add(editButtonEntity);
                        break;
                    case "delete":
                        ModuleButtonEntity deleteButtonEntity = new ModuleButtonEntity();
                        deleteButtonEntity.Create();
                        deleteButtonEntity.F_EnCode = "lr_delete";
                        deleteButtonEntity.F_FullName = "删除";
                        moduleButtonList.Add(deleteButtonEntity);
                        break;
                    case "print":
                        ModuleButtonEntity printButtonEntity = new ModuleButtonEntity();
                        printButtonEntity.Create();
                        printButtonEntity.F_EnCode = "lr_print";
                        printButtonEntity.F_FullName = "打印";
                        moduleButtonList.Add(printButtonEntity);
                        break;
                }
            }
            foreach (var btn in colDataObj.btnexs)
            {
                ModuleButtonEntity printButtonEntity = new ModuleButtonEntity();
                printButtonEntity.Create();
                printButtonEntity.F_EnCode = btn.id;
                printButtonEntity.F_FullName = btn.name;
                moduleButtonList.Add(printButtonEntity);
            }

            // 列表
            List<ModuleColumnEntity> moduleColumnList = new List<ModuleColumnEntity>();
            foreach (var col in colDataObj.fields)
            {
                ModuleColumnEntity moduleColumnEntity = new ModuleColumnEntity();
                moduleColumnEntity.Create();
                moduleColumnEntity.F_EnCode = col.field;
                moduleColumnEntity.F_FullName = col.fieldName;
                moduleColumnEntity.F_ParentId = "0";
                moduleColumnList.Add(moduleColumnEntity);
            }

            // 表单字段
            List<ModuleFormEntity> moduleFormEntitys = new List<ModuleFormEntity>();

            // 将表单数据进行一次转化
            Dictionary<string, CodeFormCompontModel> compontMap = new Dictionary<string, CodeFormCompontModel>();
            Dictionary<string, CodeFormCompontModel> compontMap2 = new Dictionary<string, CodeFormCompontModel>();
            foreach (var tab in formDataObj)
            {
                foreach (var compont in tab.componts)
                {
                    ModuleFormEntity moduleFormEntity = new ModuleFormEntity();
                    moduleFormEntity.Create();
                    moduleFormEntity.F_EnCode = compont.field;
                    moduleFormEntity.F_FullName = compont.title;
                    moduleFormEntitys.Add(moduleFormEntity);

                    if (!string.IsNullOrEmpty(compont.table))
                    {
                        compontMap.Add(compont.table + compont.field, compont);
                        compontMap2.Add(compont.id, compont);
                    }
                }
            }

            var moduleEntityTemp = moduleIBLL.GetModuleByUrl(moduleEntity.F_UrlAddress);
            if (moduleEntityTemp == null)
            {
                moduleIBLL.SaveEntity("", moduleEntity, moduleButtonList, moduleColumnList, moduleFormEntitys);//模块功能cbb
            }

            // 实体类 映射类
            string codeContent = "";
            string mainTable = "";

            foreach (var tableOne in dbTableObj)
            {
                bool isMain = false;
                if (string.IsNullOrEmpty(tableOne.relationName))
                {
                    mainTable = tableOne.name;
                    isMain = true;
                }
                // 实体类
                codeContent = codeGeneratorApp.EntityCreate(databaseLinkId, tableOne.name, tableOne.pk, baseInfoObj, colDataObj, isMain);
                codeGenerator.CreateEntityCodeFile(baseInfoObj, tableOne.name, codeContent);
                // 映射类
                codeContent = codeGeneratorApp.MappingCreate(tableOne.name, tableOne.pk, baseInfoObj);
                codeGenerator.CreateMapCodeFile(baseInfoObj, tableOne.name, codeContent);
            }

            // 服务类
            codeContent = codeGeneratorApp.ServiceCreate(databaseLinkId, dbTableObj, compontMap, queryDataObj, colDataObj, baseInfoObj);
            codeGenerator.CreateSerivceCodeFile(baseInfoObj, codeContent);
            // 业务类
            codeContent = codeGeneratorApp.BllCreate(baseInfoObj, dbTableObj, compontMap, colDataObj);
            codeGenerator.CreateBLLCodeFile(baseInfoObj, codeContent);
            // 业务接口类
            codeContent = codeGeneratorApp.IBllCreate(baseInfoObj, dbTableObj, compontMap, colDataObj);
            codeGenerator.CreateIBLLCodeFile(baseInfoObj, codeContent);
            // 控制器类
            codeContent = codeGeneratorApp.ControllerCreate(baseInfoObj, dbTableObj, compontMap, colDataObj);
            codeGenerator.CreateControllerCodeFile(baseInfoObj, codeContent);
            // 页面类
            codeContent = codeGeneratorApp.IndexCreate(baseInfoObj, compontMap, queryDataObj, colDataObj);
            codeGenerator.CreateIndexCodeFile(baseInfoObj, codeContent);
            // 页面js类
            codeContent = codeGeneratorApp.IndexJSCreate(baseInfoObj, dbTableObj, compontMap, colDataObj, queryDataObj);
            codeGenerator.CreateIndexJSCodeFile(baseInfoObj, codeContent);
            // 表单类
            codeContent = codeGeneratorApp.FormCreate(baseInfoObj, formDataObj, compontMap);
            codeGenerator.CreateFormCodeFile(baseInfoObj, codeContent);
            // 表单js类
            codeContent = codeGeneratorApp.FormJsCreate(baseInfoObj, dbTableObj, formDataObj, compontMap2);
            codeGenerator.CreateFormJSCodeFile(baseInfoObj, codeContent);


            // api接口类
            codeContent = codeGeneratorApp.ApiCreate(baseInfoObj, dbTableObj, compontMap, colDataObj);
            codeGenerator.CreateApiFile(baseInfoObj, codeContent);

            // 移动页面
            codeContent = codeGeneratorApp.AppIndexCreate(baseInfoObj, compontMap, queryDataObj);
            codeGenerator.CreateAppIndexCodeFile(baseInfoObj, codeContent,"html");
            // 移动页面JS
            codeContent = codeGeneratorApp.AppIndexJSCreate(baseInfoObj, dbTableObj, compontMap, colDataObj, queryDataObj);
            codeGenerator.CreateAppIndexCodeFile(baseInfoObj, codeContent, "js");
            codeGenerator.CreateAppIndexCodeFile(baseInfoObj, "", "css");


            // 移动页面表单
            codeContent = codeGeneratorApp.AppFormCreate(baseInfoObj, formDataObj, compontMap);
            codeGenerator.CreateAppFormCodeFile(baseInfoObj, codeContent, "html");

            // 移动表单页面JS
            codeContent = codeGeneratorApp.AppFormJsCreate(baseInfoObj, dbTableObj, formDataObj, compontMap2);
            codeGenerator.CreateAppFormCodeFile(baseInfoObj, codeContent, "js");
            codeGenerator.CreateAppFormCodeFile(baseInfoObj, "", "css");


            return Success("创建成功");
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 获取项目根目录
        /// </summary>
        /// <returns></returns>
        private string GetRootDirectory() {
            string rootDirectory = Server.MapPath("~/Web.config"); ;
            for (int i = 0; i < 2; i++) {
                rootDirectory = rootDirectory.Substring(0, rootDirectory.LastIndexOf('\\')); 
            }
            return rootDirectory;
        }
        #endregion
    }
}