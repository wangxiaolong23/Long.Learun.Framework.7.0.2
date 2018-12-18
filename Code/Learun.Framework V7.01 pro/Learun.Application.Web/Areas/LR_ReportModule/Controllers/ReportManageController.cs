using Learun.Application.Report;
using Learun.Util;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_ReportModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-07-12 09:57
    /// 描 述：报表管理
    /// </summary>
    public class ReportManageController : MvcControllerBase
    {
        private ReportTempIBLL reportTempIBLL = new ReportTempBLL();

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
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        /// <summary>
        /// 浏览页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Preview()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPageList(string pagination, string keyword)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = reportTempIBLL.GetPageList(paginationobj, keyword);
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
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetEntity(string keyValue)
        {
            var data = reportTempIBLL.GetEntity(keyValue);
            return Success(data);
        }
        /// <summary>
        /// 获取报表数据
        /// </summary>
        /// <param name="reportId">报表主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetReportData(string reportId)
        {
            ReportTempEntity reportEntity = reportTempIBLL.GetEntity(reportId);
            dynamic paramJson = reportEntity.F_ParamJson.ToJson();
            var data = new
            {
                tempStyle = reportEntity.F_TempStyle,
                chartType = reportEntity.F_TempType,
                chartData = reportTempIBLL.GetReportData(paramJson.F_DataSourceId.ToString(), paramJson.F_ChartSqlString.ToString()),
                listData = reportTempIBLL.GetReportData(paramJson.F_DataSourceId.ToString(), paramJson.F_ListSqlString.ToString())
            };
            return Content(data.ToJson());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken, AjaxOnly]
        public ActionResult SaveForm(string keyValue, ReportTempEntity entity)
        {
            reportTempIBLL.SaveEntity(keyValue, entity);
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
            reportTempIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        #endregion
    }
}