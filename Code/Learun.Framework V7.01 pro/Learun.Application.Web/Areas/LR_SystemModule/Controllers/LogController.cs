using Learun.Application.Base.SystemModule;
using Learun.Util;
using Learun.Util.Operat;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_SystemModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.01
    /// 描 述：系统日志
    /// </summary>
    public class LogController : MvcControllerBase
    {
        #region 视图功能
        /// <summary>
        /// 日志管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 清空
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 详情页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DetailForm()
        {
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
            var data = LogBLL.GetPageList(paginationobj, queryJson,"");
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
        /// 分页查询(本人数据)
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageListByMy(string pagination, string queryJson)
        {

            Pagination paginationobj = pagination.ToObject<Pagination>();
            UserInfo userInfo = LoginUserInfo.Get();
            var data = LogBLL.GetPageList(paginationobj, queryJson, userInfo.userId);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records,
            };
            return Success(jsonData);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 清空日志
        /// </summary>
        /// <param name="categoryId">日志分类Id</param>
        /// <param name="keepTime">保留时间段内</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveRemoveLog(int categoryId, string keepTime)
        {
            LogBLL.RemoveLog(categoryId, keepTime);
            return Success("清空成功。");
        }
        #endregion

        #region 接收访问日志信息
        /// <summary>
        /// 访问功能
        /// </summary>
        /// <param name="moduleId">功能Id</param>
        /// <param name="moduleName">功能模块</param>
        /// <param name="moduleUrl">访问路径</param>
        /// <returns></returns>
        [AjaxOnly]
        [HttpPost]
        public ActionResult VisitModules(string moduleName, string moduleUrl)
        {
            LogEntity logEntity = new LogEntity();
            var userInfo = LoginUserInfo.Get();

            logEntity.F_CategoryId = 2;
            logEntity.F_OperateTypeId = ((int)OperationType.Visit).ToString();
            logEntity.F_OperateType = EnumAttribute.GetDescription(OperationType.Visit);

            logEntity.F_OperateAccount = userInfo.account + "(" + userInfo.realName + ")";
            logEntity.F_OperateUserId = userInfo.userId;
            logEntity.F_Module = moduleName;
            logEntity.F_ExecuteResult = 1;
            logEntity.F_ExecuteResultJson = "访问地址：" + moduleUrl;
            logEntity.WriteLog();
            return Success("");
        }
        #endregion

        #region 接收操作日志信息

        #endregion
    }
}