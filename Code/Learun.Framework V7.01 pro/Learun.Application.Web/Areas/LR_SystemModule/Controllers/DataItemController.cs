using Learun.Application.Base.SystemModule;
using Learun.Util;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_SystemModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.09
    /// 描 述：数据字典
    /// </summary>
    public class DataItemController : MvcControllerBase
    {
        #region 属性
        private DataItemIBLL dataItemIBLL = new DataItemBLL();

        #endregion

        #region 视图功能
        /*明细管理*/
        /// <summary>
        /// 数据字典管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 明细管理(表单)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        /*分类管理*/
        /// <summary>
        /// 分类管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ClassifyIndex() {
            return View();
        }
        /// <summary>
        /// 分类管理(表单)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ClassifyForm()
        {
            return View();
        }
        /// <summary>
        /// 明细管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DetailIndex()
        {
            return View();
        }
        #endregion

        #region 字典分类
        /// <summary>
        /// 获取字典分类列表
        /// </summary>
        /// <param name="keyword">关键词（名称/编码）</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetClassifyList(string keyword)
        {
            var data = dataItemIBLL.GetClassifyList(keyword, false);
            return this.Success(data);
        }
        /// <summary>
        /// 获取字典分类列表(树结构)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetClassifyTree()
        {
            var data = dataItemIBLL.GetClassifyTree();
            return this.Success(data);
        }
        /// <summary>
        /// 保存分类数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveClassifyForm(string keyValue, DataItemEntity entity) {
            dataItemIBLL.SaveClassifyEntity(keyValue, entity);
            return Success("保存成功！");
        }
        /// <summary>
        /// 删除分类数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteClassifyForm(string keyValue) {
            dataItemIBLL.VirtualDeleteClassify(keyValue);
            return Success("删除成功！");
        }

        /// <summary>
        /// 分类编号不能重复
        /// </summary>
        /// <param name="ItemCode">编码</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult ExistItemCode(string keyValue, string F_ItemCode)
        {
            bool res = dataItemIBLL.ExistItemCode(keyValue, F_ItemCode);
            return Success(res);
        }
        /// <summary>
        /// 分类名称不能重复
        /// </summary>
        /// <param name="ItemName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult ExistItemName(string keyValue, string F_ItemName)
        {
            bool res = dataItemIBLL.ExistItemName(keyValue, F_ItemName);
            return Success(res);
        }

        #endregion

        #region 字典明细
        /// <summary>
        /// 获取数据字典明显根据分类编号
        /// </summary>
        /// <param name="itemCode">分类编号</param>
        /// <param name="keyword">查询条件</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetDetailList(string itemCode, string keyword)
        {
            var data = dataItemIBLL.GetDetailList(itemCode, keyword);
            return Success(data);
        }
        /// <summary>
        /// 获取数据字典明显树形数据
        /// </summary>
        /// <param name="itemCode">分类编号</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetDetailTree(string itemCode)
        {
            var data = dataItemIBLL.GetDetailTree(itemCode);
            return Success(data);
        }
        /// <summary>
        /// 项目值不能重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="F_ItemValue">项目值</param>
        /// <param name="itemCode">分类编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult ExistDetailItemValue(string keyValue, string F_ItemValue, string itemCode)
        {
            bool res = dataItemIBLL.ExistDetailItemValue(keyValue, F_ItemValue, itemCode);
            return Success(res);
        }
        /// <summary>
        /// 项目名不能重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="F_ItemName">项目名</param>
        /// <param name="itemCode">分类编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult ExistDetailItemName(string keyValue, string F_ItemName, string itemCode)
        {
            bool res = dataItemIBLL.ExistDetailItemName(keyValue, F_ItemName, itemCode);
            return Success(res);
        }
        /// <summary>
        /// 保存明细数据实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="itemCode">分类编码</param>
        /// <param name="entity">实体</param>
        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDetailForm(string keyValue, string itemCode, DataItemDetailEntity entity)
        {
            var data = dataItemIBLL.GetClassifyEntityByCode(itemCode);
            entity.F_ItemId = data.F_ItemId;
            dataItemIBLL.SaveDetailEntity(keyValue, entity);
            return Success("保存成功！");
        }
        /// <summary>
        /// 删除明细数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteDetailForm(string keyValue)
        {
            dataItemIBLL.VirtualDeleteDetail(keyValue);
            return Success("删除成功！");
        }
        /// <summary>
        /// 获取映射数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMap(string ver)
        {
            var data = dataItemIBLL.GetModelMap();
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
        #endregion

        #region 明细数据应用于下拉框
        /// <summary>
        /// 获取数据字典明显根据分类编号
        /// </summary>
        /// <param name="itemCode">分类编码</param>
        /// <param name="parentId">父级主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetDetailListByParentId(string itemCode, string parentId)
        {
            var data = dataItemIBLL.GetDetailListByParentId(itemCode, parentId);
            return Success(data);
        }
        #endregion

    }
}