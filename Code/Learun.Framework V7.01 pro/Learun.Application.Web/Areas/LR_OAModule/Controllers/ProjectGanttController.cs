using Learun.Application.OA.Gantt;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_OAModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.06.19
    /// 描 述：项目计划管理
    /// </summary>
    public class ProjectGanttController : MvcControllerBase
    {
        private JQueryGanttIBLL jQueryGanttIBLL = new JQueryGanttBLL();

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
        /// 添加页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetList(string queryJson)
        {
            var data = jQueryGanttIBLL.GetList(queryJson);
            return Success(data);
        }

        #region 提交数据
        /// <summary>
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken, AjaxOnly, ValidateInput(false)]
        public ActionResult SaveEntity(string keyValue, JQueryGanttEntity entity)
        {
            jQueryGanttIBLL.SaveEntity(keyValue, entity);
            return Success("保存成功！");
        }
        #endregion
    }
}