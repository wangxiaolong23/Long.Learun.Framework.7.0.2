using Learun.Cache.Base;
using Learun.Cache.Factory;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace Learun.Application.Base.SystemModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：附件管理
    /// </summary>
    public class AnnexesFileBLL : AnnexesFileIBLL
    {
        AnnexesFileService annexesFileService = new AnnexesFileService();
        /*缓存文件分片信息*/
        private ICache cache = CacheFactory.CaChe();
        private string cacheKey = "learun_adms_annexes_";
        #region 获取数据
        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="keyValue">附件夹主键</param>
        /// <returns></returns>
        public IEnumerable<AnnexesFileEntity> GetList(string keyValue)
        {
            try
            {
                return annexesFileService.GetList(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取附件名称集合
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public string GetFileNames(string keyValue)
        {
            try
            {
                return annexesFileService.GetFileNames(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取附件实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public AnnexesFileEntity GetEntity(string keyValue)
        {
            try
            {
                return annexesFileService.GetEntity(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存数据实体
        /// </summary>
        /// <param name="folderId">附件夹主键</param>
        /// <param name="annexesFileEntity">附件实体数据</param>
        public void SaveEntity(string folderId, AnnexesFileEntity annexesFileEntity)
        {
            try
            {
                annexesFileService.SaveEntity(folderId, annexesFileEntity);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="fileId">文件主键</param>
        public void DeleteEntity(string fileId)
        {
            try
            {
                annexesFileService.DeleteEntity(fileId);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 保存附件（支持大文件分片传输）
        /// </summary>
        /// <param name="folderId">附件夹主键</param>
        /// <param name="fileGuid">文件主键</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="chunks">文件总共分多少片</param>
        /// <param name="fileStream">文件二进制流</param>
        /// <returns></returns>
        public bool SaveAnnexes(string folderId, string fileGuid, string fileName, int chunks, UserInfo userInfo)
        {
            try
            {
                //获取文件完整文件名(包含绝对路径)
                //文件存放路径格式：/Resource/ResourceFile/{userId}/{date}/{guid}.{后缀名}
                string filePath = Config.GetValue("AnnexesFile");
                string uploadDate = DateTime.Now.ToString("yyyyMMdd");
                string FileEextension = Path.GetExtension(fileName);
                string virtualPath = string.Format("{0}/{1}/{2}/{3}{4}", filePath, userInfo.userId, uploadDate, fileGuid, FileEextension);
                //创建文件夹
                string path = Path.GetDirectoryName(virtualPath);
                Directory.CreateDirectory(path);
                AnnexesFileEntity fileAnnexesEntity = new AnnexesFileEntity();
                if (!System.IO.File.Exists(virtualPath))
                {
                    long filesize = SaveAnnexesToFile(fileGuid, virtualPath, chunks);
                    if (filesize == -1)// 表示保存失败
                    {
                        RemoveChunkAnnexes(fileGuid, chunks);
                        return false;
                    }
                    //文件信息写入数据库
                    fileAnnexesEntity.F_Id = fileGuid;
                    fileAnnexesEntity.F_FileName = fileName;
                    fileAnnexesEntity.F_FilePath = virtualPath;
                    fileAnnexesEntity.F_FileSize = filesize.ToString();
                    fileAnnexesEntity.F_FileExtensions = FileEextension;
                    fileAnnexesEntity.F_FileType = FileEextension.Replace(".", "");
                    fileAnnexesEntity.F_CreateUserId = userInfo.userId;
                    fileAnnexesEntity.F_CreateUserName = userInfo.realName;


                    SaveEntity(folderId, fileAnnexesEntity);
                }
                return true;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 保存附件（支持大文件分片传输）
        /// </summary>
        /// <param name="fileGuid">文件主键</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="chunks">文件总共分多少片</param>
        /// <param name="fileStream">文件二进制流</param>
        /// <returns></returns>
        public string SaveAnnexes(string fileGuid, string fileName, int chunks, UserInfo userInfo)
        {
            try
            {
                //获取文件完整文件名(包含绝对路径)
                //文件存放路径格式：/Resource/ResourceFile/{userId}/{date}/{guid}.{后缀名}
                string filePath = Config.GetValue("AnnexesFile");
                string uploadDate = DateTime.Now.ToString("yyyyMMdd");
                string FileEextension = Path.GetExtension(fileName);
                string virtualPath = string.Format("{0}/{1}/{2}/{3}{4}", filePath, userInfo.userId, uploadDate, fileGuid, FileEextension);
                //创建文件夹
                string path = Path.GetDirectoryName(virtualPath);
                Directory.CreateDirectory(path);
                AnnexesFileEntity fileAnnexesEntity = new AnnexesFileEntity();
                if (!System.IO.File.Exists(virtualPath))
                {
                    long filesize = SaveAnnexesToFile(fileGuid, virtualPath, chunks);
                    if (filesize == -1)// 表示保存失败
                    {
                        RemoveChunkAnnexes(fileGuid, chunks);
                        return "";
                    }
                }
                return virtualPath;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 保存附件到文件中
        /// </summary>
        /// <param name="fileGuid">文件主键</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="chunks">总共分片数</param>
        /// <param name="buffer">文件二进制流</param>
        /// <returns>-1:表示保存失败</returns>
        public long SaveAnnexesToFile(string fileGuid, string filePath, int chunks)
        {
            try
            {
                long filesize = 0;
                //创建一个FileInfo对象
                FileInfo file = new FileInfo(filePath);
                //创建文件
                FileStream fs = file.Create();
                for (int i = 0; i < chunks; i++)
                {
                    byte[] bufferByRedis = cache.Read<byte[]>(cacheKey + i + "_" + fileGuid, CacheId.annexes);
                    if (bufferByRedis == null)
                    {
                        return -1;
                    }
                    //写入二进制流
                    fs.Write(bufferByRedis, 0, bufferByRedis.Length);
                    filesize += bufferByRedis.Length;
                    cache.Remove(cacheKey + i + "_" + fileGuid, CacheId.annexes);
                }
                //关闭文件流
                fs.Close();

                return filesize;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 保存分片附件
        /// </summary>
        /// <param name="fileGuid">文件主键</param>
        /// <param name="chunk">分片文件序号</param>
        /// <param name="fileStream">文件流</param>
        public void SaveChunkAnnexes(string fileGuid, int chunk,Stream fileStream)
        {
            try
            {
                byte[] bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, bytes.Length);
                cache.Write<byte[]>(cacheKey + chunk + "_" + fileGuid, bytes, CacheId.annexes);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 移除文件分片数据
        /// </summary>
        /// <param name="fileGuid">文件主键</param>
        /// <param name="chunks">文件分片数</param>
        public void RemoveChunkAnnexes(string fileGuid, int chunks)
        {
            try
            {
                for (int i = 0; i < chunks; i++)
                {
                    cache.Remove(cacheKey + i + "_" + fileGuid, CacheId.annexes);
                }
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        #endregion
    }
}
