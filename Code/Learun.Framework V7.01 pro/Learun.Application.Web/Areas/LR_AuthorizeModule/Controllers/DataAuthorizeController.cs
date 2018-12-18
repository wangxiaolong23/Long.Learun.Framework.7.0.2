using Learun.Application.Base.AuthorizeModule;
using Learun.Util;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_AuthorizeModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：力软框架开发组
    /// 日 期：2017-06-21 16:30
    /// 描 述：数据权限
    /// </summary>
    public class DataAuthorizeController : MvcControllerBase
    {
        private DataAuthorizeIBLL dataAuthorizeIBLL = new DataAuthorizeBLL();

        #region 视图功能

        /// <summary>
        /// 主页面
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
             return View();
        }
        /// <summary>
        /// 表单页
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
             return View();
        }
        /// <summary>
        /// 添加条件表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult QueryForm()
        {
             return View();
        }
        

        #endregion

        #region 获取数据
        /// <summary>
        /// 获取条件列表数据
        /// </summary>
        /// <param name="relationId">关系主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetDataAuthorizeConditionList(string relationId)
        {
            var data = dataAuthorizeIBLL.GetDataAuthorizeConditionList(relationId);
            return Success(data);
        }
        /// <summary>
        /// 获取数据权限对应关系数据列表
        /// <param name="pagination">分页参数</param>
        /// <param name="interfaceId">接口主键</param>
        /// <param name="keyword">查询关键词</param>
        /// <param name="objectId">对象主键</param>
        /// <summary>
        /// <returns></returns>
        public ActionResult GetRelationPageList(string pagination, string interfaceId, string keyword, string objectId)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = dataAuthorizeIBLL.GetRelationPageList(paginationobj, interfaceId, keyword, objectId);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 获取条件列表数据
        /// </summary>
        /// <param name="relationId">关系主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetDataAuthorizeEntity(string keyValue)
        {
            var relationEntity = dataAuthorizeIBLL.GetRelationEntity(keyValue);
            var conditionEntity = dataAuthorizeIBLL.GetDataAuthorizeConditionList(keyValue);
            var jsonData = new
            {
                relationEntity = relationEntity,
                conditionEntity = conditionEntity
            };

            return Success(jsonData);
        } 

        #endregion

        #region 提交数据
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="relation">对应关系数据</param>
        /// <param name="conditions">条件数据</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string relation, string conditions)
        {
            DataAuthorizeRelationEntity relationEntity = relation.ToObject<DataAuthorizeRelationEntity>();
            List<DataAuthorizeConditionEntity> conditionEntityList = conditions.ToObject<List<DataAuthorizeConditionEntity>>();

            dataAuthorizeIBLL.SaveEntity(keyValue, relationEntity, conditionEntityList);
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
            dataAuthorizeIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        #endregion
    }
}
