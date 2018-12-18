using Learun.Application.Base.SystemModule;
using Learun.Util;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_SystemModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.01
    /// 描 述：数据源管理
    /// </summary>
    public class DataSourceController : MvcControllerBase
    {
        DataSourceIBLL dataSourceIBLL = new DataSourceBLL();

        #region 获取视图
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
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 测试页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TestForm()
        {
            return View();
        }
        /// <summary>
        /// 选择页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SelectForm()
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
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = dataSourceIBLL.GetPageList(paginationobj, keyword);
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
        /// 获取所有数据源数据列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetList()
        {
            var data = dataSourceIBLL.GetList();
            return Success(data);
        }
        /// <summary>
        /// 获取所有数据源实体根据编号
        /// </summary>
        /// <param name="keyValue">编号</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetEntityByCode(string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                return Success("");
            }
            else
            {
                var data = dataSourceIBLL.GetEntityByCode(keyValue.Split(',')[0]);
                return Success(data);
            }

        }
        /// <summary>
        /// 获取所有数据源实体根据编号
        /// </summary>
        /// <param name="keyValue">编号</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetNameByCode(string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                return SuccessString("");
            }
            else
            {
                var data = dataSourceIBLL.GetEntityByCode(keyValue.Split(',')[0]);
                return SuccessString(data.F_Name);
            }

        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, DataSourceEntity entity)
        {
            bool res = dataSourceIBLL.SaveEntity(keyValue, entity);
            if (res)
            {
                return Success("保存成功！");
            }
            else
            {
                return Fail("保存失败,编码重复！");
            }
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
            dataSourceIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 获取数据源数据
        /// </summary>
        /// <param name="code">数据源编号</param>
        /// <param name="strWhere">sql查询条件语句</param>
        /// <param name="queryJson">数据源请求条件字串</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetDataTable(string code,string strWhere, string queryJson)
        {
            var data = dataSourceIBLL.GetDataTable(code, strWhere, queryJson);
            return Success(data);
        }
        /// <summary>
        /// 获取数据源数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="code">数据源编号</param>
        /// <param name="strWhere">sql查询条件语句</param>
        /// <param name="queryJson">数据源请求条件字串</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetDataTablePage(string pagination, string code, string strWhere, string queryJson)
        {

            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = dataSourceIBLL.GetDataTable(code, paginationobj, strWhere, queryJson);
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
        /// 获取数据源列名
        /// </summary>
        /// <param name="code">数据源编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetDataColName(string code)
        {
            var data = dataSourceIBLL.GetDataColName(code);
            return Success(data);
        }

        /// <summary>
        /// 获取数据源数据
        /// </summary>
        /// <param name="code">数据源编号</param>
        /// <param name="strWhere">sql查询条件语句</param>
        /// <param name="queryJson">数据源请求条件字串</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMap(string code, string ver)
        {
            var data = dataSourceIBLL.GetDataTable(code, "");

            string md5 = Md5Helper.Encrypt(data.ToJson(), 32);
            if (md5 == ver)
            {
                return Success("no update");
            }
            else
            {
                var jsondata = new
                {
                    data = data,
                    ver = md5
                };
                return Success(jsondata);
            }
        }
        /// <summary>
        /// 获取表单数据
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetTree(string code, string parentId, string Id, string showId)
        {
            var data = dataSourceIBLL.GetTree(code, parentId, Id, showId);
            return Success(data);
        }
        #endregion
    }
}