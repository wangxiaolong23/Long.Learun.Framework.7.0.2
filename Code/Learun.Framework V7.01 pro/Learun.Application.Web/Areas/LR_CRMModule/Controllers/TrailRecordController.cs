using Learun.Application.CRM;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_CRMModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-07-11 11:20
    /// 描 述：跟进记录
    /// </summary>
    public class TrailRecordController : MvcControllerBase
    {
        private CrmTrailRecordIBLL CrmTrailRecordIBLL = new CrmTrailRecordBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="objectId">Id</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string objectId)
        {
            var data = CrmTrailRecordIBLL.GetList(objectId);
            Dictionary<string, string> dictionaryDate = new Dictionary<string, string>();
            foreach (CrmTrailRecordEntity item in data)
            {
                string key = item.F_CreateDate.ToDate().ToString("yyyy-MM-dd");
                string currentTime = DateTime.Now.ToString("yyyy-MM-dd");
                if (item.F_CreateDate.ToDate().ToString("yyyy-MM-dd") == currentTime)
                {
                    key = "今天";
                }
                if (!dictionaryDate.ContainsKey(key))
                {
                    dictionaryDate.Add(key, item.F_CreateDate.ToDate().ToString("yyyy-MM-dd"));
                }
            }
            var jsonData = new
            {
                timeline = dictionaryDate,
                rows = data,
            };
            return Success(jsonData);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, CrmTrailRecordEntity entity)
        {
            CrmTrailRecordIBLL.SaveEntity(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion
    }
}