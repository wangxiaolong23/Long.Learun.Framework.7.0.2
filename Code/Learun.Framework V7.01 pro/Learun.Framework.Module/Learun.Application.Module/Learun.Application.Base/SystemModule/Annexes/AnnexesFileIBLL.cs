using Learun.Util;
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
    public interface AnnexesFileIBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="keyValue">附件夹主键</param>
        /// <returns></returns>
        IEnumerable<AnnexesFileEntity> GetList(string keyValue);
        /// <summary>
        /// 获取附件名称集合
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        string GetFileNames(string keyValue);
        /// <summary>
        /// 获取附件实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        AnnexesFileEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存数据实体
        /// </summary>
        /// <param name="folderId">附件夹主键</param>
        /// <param name="annexesFileEntity">附件实体数据</param>
        void SaveEntity(string folderId, AnnexesFileEntity annexesFileEntity);
        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="fileId">文件主键</param>
        void DeleteEntity(string fileId);
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
        bool SaveAnnexes(string folderId, string fileGuid, string fileName, int chunks, UserInfo userInfo);
         /// <summary>
        /// 保存附件（支持大文件分片传输）
        /// </summary>
        /// <param name="fileGuid">文件主键</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="chunks">文件总共分多少片</param>
        /// <param name="fileStream">文件二进制流</param>
        /// <returns></returns>
        string SaveAnnexes(string fileGuid, string fileName, int chunks, UserInfo userInfo);
        /// <summary>
        /// 保存附件到文件中
        /// </summary>
        /// <param name="fileGuid">文件主键</param>
        /// <param name="filePath">文件路径</param>
        /// <param name="chunks">总共分片数</param>
        /// <param name="buffer">文件二进制流</param>
        /// <returns></returns>
        long SaveAnnexesToFile(string fileGuid, string filePath, int chunks);
        /// <summary>
        /// 保存分片附件
        /// </summary>
        /// <param name="fileGuid">文件主键</param>
        /// <param name="chunk">分片文件序号</param>
        /// <param name="fileStream">文件流</param>
        void SaveChunkAnnexes(string fileGuid, int chunk, Stream fileStream);
        /// <summary>
        /// 移除文件分片数据
        /// </summary>
        /// <param name="fileGuid">文件主键</param>
        /// <param name="chunks">文件分片数</param>
        void RemoveChunkAnnexes(string fileGuid, int chunks);
        #endregion
    }
}
