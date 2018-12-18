using Learun.Application.Base.SystemModule;
using Learun.Util;
using System.Web;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_SystemModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：附件管理
    /// </summary>
    public class AnnexesController : MvcControllerBase
    {
        private AnnexesFileIBLL annexesFileIBLL = new AnnexesFileBLL();

        #region 视图功能
        /// <summary>
        /// 上传列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UploadForm()
        {
            return View();
        }
        /// <summary>
        /// 下载列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DownForm()
        {
            return View();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 上传附件分片数据
        /// </summary>
        /// <param name="fileGuid">文件主键</param>
        /// <param name="chunk">分片序号</param>
        /// <param name="Filedata">文件数据</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadAnnexesFileChunk(string fileGuid, int chunk, int chunks, HttpPostedFileBase Filedata)
        {
            //没有文件上传，直接返回
            if (Filedata == null || string.IsNullOrEmpty(Filedata.FileName) || Filedata.ContentLength == 0)
            {
                if (Request.Files.Count > 0)
                {
                    Filedata = Request.Files[0];
                }
                else
                {
                    return HttpNotFound();
                }
            }
            annexesFileIBLL.SaveChunkAnnexes(fileGuid, chunk, Filedata.InputStream);
            return Success("保存成功");
        }
        /// <summary>
        /// 移除附件分片数据
        /// </summary>
        /// <param name="fileGuid">文件主键</param>
        /// <param name="chunks">总分片数</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveAnnexesFileChunk(string fileGuid, int chunks)
        {
            annexesFileIBLL.RemoveChunkAnnexes(fileGuid, chunks);
            return Success("移除成功");
        }
        /// <summary>
        /// 合并上传附件的分片数据
        /// </summary>
        /// <param name="folderId">附件夹主键</param>
        /// <param name="fileGuid">文件主键</param>
        /// <param name="fileName">文件名</param>
        /// <param name="chunks">文件总分片数</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MergeAnnexesFile(string folderId, string fileGuid, string fileName, int chunks)
        {
            UserInfo userInfo = LoginUserInfo.Get();
            bool res = annexesFileIBLL.SaveAnnexes(folderId, fileGuid, fileName, chunks, userInfo);
            if (res)
            {
                return Success("保存文件成功");

            }
            else
            {
                return Fail("保存文件失败");
            }
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileId">文件主键</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAnnexesFile(string fileId)
        {
            AnnexesFileEntity fileInfoEntity = annexesFileIBLL.GetEntity(fileId);
            annexesFileIBLL.DeleteEntity(fileId);
            //删除文件
            if (System.IO.File.Exists(fileInfoEntity.F_FilePath))
            {
                System.IO.File.Delete(fileInfoEntity.F_FilePath);
            }
            return Success("删除附件成功");
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileId">文件id</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public void DownAnnexesFile(string fileId)
        {
            var data = annexesFileIBLL.GetEntity(fileId);
            string filename = Server.UrlDecode(data.F_FileName);//返回客户端文件名称
            string filepath = data.F_FilePath;
            if (FileDownHelper.FileExists(filepath))
            {
                FileDownHelper.DownLoadold(filepath, filename);
            }
        }
        /// <summary>
        /// 获取附件列表
        /// </summary>
        /// <param name="folderId">附件夹主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetAnnexesFileList(string folderId)
        {
            var data = annexesFileIBLL.GetList(folderId);
            return Success(data);
        }
        /// <summary>
        /// 获取附件夹信息
        /// </summary>
        /// <param name="folderId">附件夹主键</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetFileNames(string folderId)
        {
            var data = annexesFileIBLL.GetFileNames(folderId);
            return Success(data);
        }
        #endregion
    }
}