using Learun.Application.Base.SystemModule;
using Learun.Util;
using Nancy;
using System;
using System.Collections.Generic;
using System.IO;

namespace Learun.Application.WebApi.Modules
{
    public class AnnexesApi : BaseApi
    {
        public AnnexesApi()
            : base("/learun/adms/annexes")
        {
            Get["/list"] = GetList;
            Get["/down"] = DownAnnexesFile;

            Post["/upload"] = Upload;
            Post["/delete"] = DeleteFile;
        }
        private AnnexesFileIBLL annexesFileIBLL = new AnnexesFileBLL();

        /// <summary>
        /// 获取附件列表
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public Response GetList(dynamic _)
        {
            var keyValue = this.GetReqData();
            var list = annexesFileIBLL.GetList(keyValue);

            return Success(list);
        }

        /// <summary>
        /// 上传附件图片文件
        /// <summary>
        /// <returns></returns>
        public Response Upload(dynamic _)
        {
            var files = (List<HttpFile>)this.Context.Request.Files;
            var folderId = this.GetReqData();

            string filePath = Config.GetValue("AnnexesFile");
            string uploadDate = DateTime.Now.ToString("yyyyMMdd");
            string FileEextension = Path.GetExtension(files[0].Name);
            string fileGuid = Guid.NewGuid().ToString();

            string virtualPath = string.Format("{0}/{1}/{2}/{3}{4}", filePath, userInfo.userId, uploadDate, fileGuid, FileEextension);

            //创建文件夹
            string path = Path.GetDirectoryName(virtualPath);
            Directory.CreateDirectory(path);
            AnnexesFileEntity fileAnnexesEntity = new AnnexesFileEntity();
            if (!System.IO.File.Exists(virtualPath))
            {
                byte[] bytes = new byte[files[0].Value.Length];
                files[0].Value.Read(bytes, 0, bytes.Length);
                FileInfo file = new FileInfo(virtualPath);
                FileStream fs = file.Create();
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();

                //文件信息写入数据库
                fileAnnexesEntity.F_Id = fileGuid;
                fileAnnexesEntity.F_FileName = files[0].Name;
                fileAnnexesEntity.F_FilePath = virtualPath;
                fileAnnexesEntity.F_FileSize = files[0].Value.Length.ToString();
                fileAnnexesEntity.F_FileExtensions = FileEextension;
                fileAnnexesEntity.F_FileType = FileEextension.Replace(".", "");
                fileAnnexesEntity.F_CreateUserId = userInfo.userId;
                fileAnnexesEntity.F_CreateUserName = userInfo.realName;

                annexesFileIBLL.SaveEntity(folderId, fileAnnexesEntity);
            }

            return SuccessString(fileGuid);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public Response DeleteFile(dynamic _)
        {
            var fileId = this.GetReqData();
            AnnexesFileEntity fileInfoEntity = annexesFileIBLL.GetEntity(fileId);
            annexesFileIBLL.DeleteEntity(fileId);
            //删除文件
            if (System.IO.File.Exists(fileInfoEntity.F_FilePath))
            {
                System.IO.File.Delete(fileInfoEntity.F_FilePath);
            }

            return Success("删除成功");
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public Response DownAnnexesFile(dynamic _) {
            string name = this.GetReqData();
            string fileId = name.Split('.')[0];
            var data = annexesFileIBLL.GetEntity(fileId);
            string filepath = data.F_FilePath;
            if (FileDownHelper.FileExists(filepath))
            {
                FileDownHelper.DownLoadnew(filepath);
            }
            return Success("");
        }
    }
}