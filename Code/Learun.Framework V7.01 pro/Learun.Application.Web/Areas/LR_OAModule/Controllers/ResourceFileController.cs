using Learun.Application.Base.SystemModule;
using Learun.Application.OA.File.FileFolder;
using Learun.Application.OA.File.FileInfo;
using Learun.Application.OA.File.FilePreview;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_OAModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.06.19
    /// 描 述：文件管理
    /// </summary>
    public class ResourceFileController : MvcControllerBase
    {
        private FileFolderIBLL fileFolderBLL = new FileFolderBLL();
        private FileInfoIBLL fileInfoBLL = new FileInfoBLL();
        private FilePreviewIBLL filePreviewIBLL = new FilePreviewBLL();
        private AnnexesFileIBLL annexesFileIBLL = new AnnexesFileBLL();

        #region 视图功能
        /// <summary>
        /// 文件管理
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UploadifyForm()
        {
            return View();
        }
        /// <summary>
        /// 文件夹表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FolderForm()
        {
            return View();
        }
        /// <summary>
        /// 文件表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult FileForm()
        {
            return View();
        }
        /// <summary>
        /// 文件（夹）移动表单  
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MoveForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 文件夹列表 
        /// </summary>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        public ActionResult GetTreeJson()
        {
            string userId = LoginUserInfo.Get().userId;
            var data = fileFolderBLL.GetList(userId);
            var treeList = new List<TreeModel>();
            foreach (FileFolderEntity item in data)
            {
                TreeModel tree = new TreeModel();
                bool hasChildren = data.Count(t => t.F_ParentId == item.F_FolderId) == 0 ? false : true;
                tree.id = item.F_FolderId;
                tree.text = item.F_FolderName;
                tree.value = item.F_FolderId;
                tree.parentId = item.F_ParentId;
                tree.isexpand = true;
                tree.complete = true;
                tree.hasChildren = hasChildren;
                if (hasChildren == false)
                {
                    tree.icon = "fa fa-folder";
                }
                treeList.Add(tree);
            }
            return Success(treeList);
        }
        /// <summary>
        /// 所有文件（夹）列表
        /// </summary>
        /// <param name="folderId">文件夹Id</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string folderId)
        {
            string userId = LoginUserInfo.Get().userId;
            var data = fileInfoBLL.GetList(folderId, userId);
            return Success(data);
        }
        /// <summary>
        /// 文档列表
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetDocumentListJson()
        {
            string userId = LoginUserInfo.Get().userId;
            var data = fileInfoBLL.GetDocumentList(userId);
            return Success(data);
        }
        /// <summary>
        /// 图片列表
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetImageListJson()
        {
            string userId = LoginUserInfo.Get().userId;
            var data = fileInfoBLL.GetImageList(userId);
            return Success(data);
        }
        /// <summary>
        /// 回收站文件（夹）列表
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetRecycledListJson()
        {
            string userId = LoginUserInfo.Get().userId;
            var data = fileInfoBLL.GetRecycledList(userId);
            return Success(data);
        }
        /// <summary>
        /// 我的文件（夹）共享列表
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetMyShareListJson()
        {
            string userId = LoginUserInfo.Get().userId;
            var data = fileInfoBLL.GetMyShareList(userId);
            return Success(data);
        }
        /// <summary>
        /// 他人文件（夹）共享列表
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetOthersShareListJson()
        {
            string userId = LoginUserInfo.Get().userId;
            var data = fileInfoBLL.GetOthersShareList(userId);
            return Success(data);
        }
        /// <summary>
        /// 文件夹实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFolderFormJson(string keyValue)
        {
            var data = fileFolderBLL.GetEntity(keyValue);
            return Success(data);
        }
        /// <summary>
        /// 文件实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFileFormJson(string keyValue)
        {
            var data = fileInfoBLL.GetEntity(keyValue);
            return Success(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 还原文件（夹）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="fileType">文件类型</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult RestoreFile(string keyValue, string fileType)
        {
            if (fileType == "folder")
            {
                fileFolderBLL.RestoreFile(keyValue);
            }
            else
            {
                fileInfoBLL.RestoreFile(keyValue);
            }
            return Success("还原成功。");
        }
        /// <summary>
        /// 删除文件（夹）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="fileType">文件类型</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue, string fileType)
        {
            if (fileType == "folder")
            {
                fileFolderBLL.RemoveForm(keyValue);
            }
            else
            {
                fileInfoBLL.RemoveForm(keyValue);
            }
            return Success("删除成功。");
        }
        /// <summary>
        /// 彻底删除文件（夹）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="fileType">文件类型</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult ThoroughRemoveForm(string keyValue, string fileType)
        {
            if (fileType == "folder")
            {
                fileFolderBLL.ThoroughRemoveForm(keyValue);
            }
            else
            {
                fileInfoBLL.ThoroughRemoveForm(keyValue);
            }
            return Success("删除成功。");
        }

        /// <summary>
        /// 清空回收站
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult EmptyRecycledForm()
        {
            fileFolderBLL.EmptyRecycledForm();
            return Success("操作成功。");
        }

        /// <summary>
        /// 保存文件夹表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="fileFolderEntity">文件夹实体</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult SaveFolderForm(string keyValue, FileFolderEntity fileFolderEntity)
        {
            fileFolderBLL.SaveForm(keyValue, fileFolderEntity);
            return Success("操作成功。");
        }
        /// <summary>
        /// 保存文件表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="fileInfoEntity">文件实体</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult SaveFileForm(string keyValue, FileInfoEntity fileInfoEntity)
        {
            fileInfoBLL.SaveForm(keyValue, fileInfoEntity);
            return Success("操作成功。");
        }
        /// <summary>
        /// 保存文件（夹）移动位置
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="moveFolderId">要移动文件夹Id</param>
        /// <param name="fileType">文件类型</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult SaveMoveForm(string keyValue, string moveFolderId, string fileType)
        {
            if (fileType == "folder")
            {
                FileFolderEntity fileFolderEntity = new FileFolderEntity();
                fileFolderEntity.F_FolderId = keyValue;
                fileFolderEntity.F_ParentId = moveFolderId;
                fileFolderBLL.SaveForm(keyValue, fileFolderEntity);
            }
            else
            {
                FileInfoEntity fileInfoEntity = new FileInfoEntity();
                fileInfoEntity.F_FileId = keyValue;
                fileInfoEntity.F_FolderId = moveFolderId;
                fileInfoBLL.SaveForm(keyValue, fileInfoEntity);
            }
            return Success("操作成功。");
        }
        /// <summary>
        /// 共享文件（夹）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="fileType">文件类型</param>
        /// <param name="IsShare">是否共享：1-共享 0取消共享</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult ShareFile(string keyValue, int IsShare, string fileType)
        {
            if (fileType == "folder")
            {
                fileFolderBLL.ShareFolder(keyValue, IsShare);
            }
            else
            {
                fileInfoBLL.ShareFile(keyValue, IsShare);
            }
            return Success("共享成功。");
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="folderId">文件夹Id</param>
        /// <param name="Filedata">文件对象</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UploadifyFile(string folderId, HttpPostedFileBase Filedata)
        {
            try
            {
                Thread.Sleep(500);////延迟500毫秒
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
                //获取文件完整文件名(包含绝对路径)
                //文件存放路径格式：/Resource/ResourceFile/{userId}{data}/{guid}.{后缀名}
                string userId = LoginUserInfo.Get().userId;
                string fileGuid = Guid.NewGuid().ToString();
                long filesize = Filedata.ContentLength;
                string FileEextension = Path.GetExtension(Filedata.FileName);
                string uploadDate = DateTime.Now.ToString("yyyyMMdd");
                string virtualPath = string.Format("~/Resource/DocumentFile/{0}/{1}/{2}{3}", userId, uploadDate, fileGuid, FileEextension);
                string fullFileName = this.Server.MapPath(virtualPath);
                //创建文件夹
                string path = Path.GetDirectoryName(fullFileName);
                Directory.CreateDirectory(path);
                if (!System.IO.File.Exists(fullFileName))
                {
                    //保存文件
                    Filedata.SaveAs(fullFileName);
                    //文件信息写入数据库
                    FileInfoEntity fileInfoEntity = new FileInfoEntity();
                    fileInfoEntity.Create();
                    fileInfoEntity.F_FileId = fileGuid;
                    if (!string.IsNullOrEmpty(folderId))
                    {
                        fileInfoEntity.F_FolderId = folderId;
                    }
                    else
                    {
                        fileInfoEntity.F_FolderId = "0";
                    }
                    fileInfoEntity.F_FileName = Filedata.FileName;
                    fileInfoEntity.F_FilePath = virtualPath;
                    fileInfoEntity.F_FileSize = filesize.ToString();
                    fileInfoEntity.F_FileExtensions = FileEextension;
                    fileInfoEntity.F_FileType = FileEextension.Replace(".", "");
                    fileInfoBLL.SaveForm("", fileInfoEntity);
                }
                return Success("上传成功。");
            }
            catch (Exception ex)
            {
                return Fail(ex.Message);
            }
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        public void DownloadFile(string keyValue)
        {
            var data = fileInfoBLL.GetEntity(keyValue);
            string filename = Server.UrlDecode(data.F_FileName);//返回客户端文件名称
            string filepath = this.Server.MapPath(data.F_FilePath);
            if (FileDownHelper.FileExists(filepath))
            {
                FileDownHelper.DownLoadold(filepath, filename);
            }
        }
        #endregion


        #region

        /// <summary>
        /// 文件预览
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <returns></returns>
        public void PreviewFile(string fileId)
        {
            var data = fileInfoBLL.GetEntity(fileId);
            string filename = Server.UrlDecode(data.F_FileName);//客户端保存的文件名  
            string filepath = DirFileHelper.GetAbsolutePath(data.F_FilePath);//路径 
            if (data.F_FileType == "xlsx" || data.F_FileType == "xls")
            {
                filepath = filepath.Substring(0, filepath.LastIndexOf(".")) + ".pdf";//文件名
                if (!DirFileHelper.IsExistFile(filepath))
                {
                    filePreviewIBLL.GetExcelData(DirFileHelper.GetAbsolutePath(data.F_FilePath));
                }
            }
            if (data.F_FileType == "docx" || data.F_FileType == "doc")
            {
                filepath = filepath.Substring(0, filepath.LastIndexOf(".")) + ".pdf";//文件名
                if (!DirFileHelper.IsExistFile(filepath))
                {
                    filePreviewIBLL.GetWordData(DirFileHelper.GetAbsolutePath(data.F_FilePath));
                }
            }
            FileStream files = new FileStream(filepath, FileMode.Open);
            byte[] fileByte = new byte[files.Length];
            files.Read(fileByte, 0, fileByte.Length);
            files.Close();
            System.IO.MemoryStream ms = new MemoryStream(fileByte, 0, fileByte.Length);
            Response.ClearContent();
            switch (data.F_FileType)
            {
                case "jpg":
                    Response.ContentType = "image/jpeg";
                    break;
                case "gif":
                    Response.ContentType = "image/gif";
                    break;
                case "png":
                    Response.ContentType = "image/png";
                    break;
                case "bmp":
                    Response.ContentType = "application/x-bmp";
                    break;
                case "jpeg":
                    Response.ContentType = "image/jpeg";
                    break;
                case "doc":
                    Response.ContentType = "application/pdf";
                    break;
                case "docx":
                    Response.ContentType = "application/pdf";
                    break;
                case "ppt":
                    Response.ContentType = "application/x-ppt";
                    break;
                case "pptx":
                    Response.ContentType = "application/x-ppt";
                    break;
                case "xls":
                    Response.ContentType = "application/pdf";
                    break;
                case "xlsx":
                    Response.ContentType = "application/pdf";
                    break;
                case "pdf":
                    Response.ContentType = "application/pdf";
                    break;
                case "txt":
                    Response.ContentType = "text/plain";
                    break;
                case "csv":
                    Response.ContentType = "";
                    break;
                default:
                    Response.ContentType = "application/pdf";
                    break;
            }
            Response.Charset = "GB2312";
            Response.BinaryWrite(ms.ToArray());
        }
        #endregion
    }
}