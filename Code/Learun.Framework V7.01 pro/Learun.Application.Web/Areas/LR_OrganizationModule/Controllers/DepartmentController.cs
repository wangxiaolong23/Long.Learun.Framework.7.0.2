using Learun.Application.Organization;
using Learun.Util;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_OrganizationModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：部门管理
    /// </summary>
    public class DepartmentController : MvcControllerBase
    {
        private DepartmentIBLL departmentIBLL = new DepartmentBLL();
        private CompanyIBLL companyIBLL = new CompanyBLL();

        #region 获取视图
        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单
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
        /// 获取部门列表信息(根据公司Id)
        /// </summary>
        /// <param name="companyId">公司Id</param>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetList(string companyId,string keyword)
        {
            var data = departmentIBLL.GetList(companyId, keyword);
            return Success(data);
        }
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="companyId">公司id</param>
        /// <param name="parentId">父级id</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetTree(string companyId, string parentId)
        {
            if (string.IsNullOrEmpty(companyId))
            {
                var companylist = companyIBLL.GetList();
                var data = departmentIBLL.GetTree(companylist);
                return Success(data);
            }
            else
            {
                var data = departmentIBLL.GetTree(companyId, parentId);
                return Success(data);
            }
        }
        /// <summary>
        /// 获取部门实体数据
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetEntity(string departmentId)
        {
            var data = departmentIBLL.GetEntity(departmentId);
            return Success(data);
        }
        /// <summary>
        /// 获取映射数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMap(string ver)
        {
            var data = departmentIBLL.GetModelMap();
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

        #region 提交数据
        /// <summary>
        /// 保存表单数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, DepartmentEntity entity)
        {
            departmentIBLL.SaveEntity(keyValue, entity);
            return Success("保存成功！");
        }
        /// <summary>
        /// 删除表单数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            departmentIBLL.VirtualDelete(keyValue);
            return Success("删除成功！");
        }
        #endregion
    }
}