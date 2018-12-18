using Learun.Application.AppMagager;
using Learun.Util;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.AppManager.Controllers
{
    /// <summary> 
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架 
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司 
    /// 创 建：超级管理员 
    /// 日 期：2018-07-02 15:31 
    /// 描 述：App首页图片管理 
    /// </summary> 
    public class DTImgController : MvcControllerBase
    {
        private DTImgIBLL dTImgIBLL = new DTImgBLL();

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
        public ActionResult GetList()
        {
            var data = dTImgIBLL.GetList();
            return Success(data);
        }
        /// <summary> 
        /// 获取列表分页数据 
        /// <param name="pagination">分页参数</param> 
        /// <summary> 
        /// <returns></returns> 
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = dTImgIBLL.GetPageList(paginationobj, queryJson);
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
            var data = dTImgIBLL.GetEntity(keyValue);
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
            dTImgIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, DTImgEntity entity)
        {
            dTImgIBLL.SaveEntity(keyValue, entity);
            return Success("保存成功！");
        }

        /// <summary>
        /// 保存图片和存储数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadFile(string keyValue, DTImgEntity entity)
        {
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            //没有文件上传，直接返回
            if (files[0].ContentLength == 0 || string.IsNullOrEmpty(files[0].FileName))
            {
                return HttpNotFound();
            }
           

            string FileEextension = Path.GetExtension(files[0].FileName);
            entity.F_FileName = FileEextension;
            dTImgIBLL.SaveEntity(keyValue, entity);

            string fileHeadImg = Config.GetValue("fileAppDTImg");
            string fullFileName = string.Format("{0}/{1}{2}", fileHeadImg, entity.F_Id, FileEextension);

           

            //创建文件夹，保存文件
            string path = Path.GetDirectoryName(fullFileName);
            Directory.CreateDirectory(path);
            files[0].SaveAs(fullFileName);

            return Success("保存成功。");
        }

        /// <summary>
        /// 获取设置图片
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetImg(string keyValue)
        {
            dTImgIBLL.GetImg(keyValue);
            return Success("获取成功。");
        }

        /// <summary>
        /// 启用/停用
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state">状态1启用0禁用</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult UpDateSate(string keyValue, int state)
        {
            dTImgIBLL.UpdateState(keyValue, state);
            return Success((state == 1 ? "启用" : "禁用") + "成功！");
        }
        #endregion


    }
}