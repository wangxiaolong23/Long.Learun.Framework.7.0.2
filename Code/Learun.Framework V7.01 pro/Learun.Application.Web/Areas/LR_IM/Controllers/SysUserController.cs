using Learun.Application.IM;
using Learun.Util;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_IM.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-05-24 10:00
    /// 描 述：即时通讯系统用户注册
    /// </summary>
    public class SysUserController : MvcControllerBase
    {
        private IMSysUserIBLL iMSysUserIBLL = new IMSysUserBLL();

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
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetList(string queryJson)
        {
            var data = iMSysUserIBLL.GetList(queryJson);
            return Success(data);
        }
        /// <summary>
        /// 获取列表分页数据
        /// <param name="pagination">分页参数</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageList(string pagination, string keyWord)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = iMSysUserIBLL.GetPageList(paginationobj, keyWord);
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
        /// 获取表单数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetFormData(string keyValue)
        {
            var data = iMSysUserIBLL.GetEntity(keyValue);
            return Success(data);
        }
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            iMSysUserIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, IMSysUserEntity entity)
        {
            iMSysUserIBLL.SaveEntity(keyValue, entity);
            return Success("保存成功！");
        }
        #endregion

        #region 数据验证
        /// <summary>
        /// 编码不能重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="F_Code">编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult ExistCode(string keyValue, string F_Code)
        {
            bool res = iMSysUserIBLL.ExistEnCode(F_Code, keyValue);
            return Success(res);
        }
        #endregion
    }
}