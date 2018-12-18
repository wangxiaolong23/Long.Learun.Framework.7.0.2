using Learun.Application.OA.Schedule;
using Learun.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_OAModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.7.11
    /// 描 述：日程管理
    /// </summary>
    public class ScheduleController : MvcControllerBase
    {
        private ScheduleIBLL scheduleIBLL = new ScheduleBLL();

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
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取日程数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetList()
        {
            List<Hashtable> data = new List<Hashtable>();
            foreach (ScheduleEntity entity in scheduleIBLL.GetList().ToList())
            {
                Hashtable ht = new Hashtable();
                ht["id"] = entity.F_ScheduleId;
                ht["title"] = entity.F_ScheduleContent;
                ht["end"] = (entity.F_EndDate.ToDate().ToString("yyyy-MM-dd") + " " + entity.F_EndTime.Substring(0, 2) + ":" + entity.F_EndTime.Substring(2, 2)).ToDate().ToString("yyyy-MM-dd HH:mm:ss");
                ht["start"] = (entity.F_StartDate.ToDate().ToString("yyyy-MM-dd") + " " + entity.F_StartTime.Substring(0, 2) + ":" + entity.F_StartTime.Substring(2, 2)).ToDate().ToString("yyyy-MM-dd HH:mm:ss");
                ht["allDay"] = false;
                data.Add(ht);
            }
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = scheduleIBLL.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            scheduleIBLL.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, ScheduleEntity entity)
        {
            scheduleIBLL.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion
    }
}