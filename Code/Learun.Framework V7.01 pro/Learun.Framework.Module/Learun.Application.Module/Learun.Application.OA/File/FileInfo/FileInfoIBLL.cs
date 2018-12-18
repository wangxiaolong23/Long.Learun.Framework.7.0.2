using System.Collections.Generic;

namespace Learun.Application.OA.File.FileInfo
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.06.20
    /// 描 述：文件管理
    /// </summary>
    public interface FileInfoIBLL
    {
        #region 获取数据
        /// <summary>
        /// 所有文件（夹）列表
        /// </summary>
        /// <param name="folderId">文件夹Id</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        IEnumerable<FileInfoEntity> GetList(string folderId, string userId);
        /// <summary>
        /// 文档列表
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        IEnumerable<FileInfoEntity> GetDocumentList(string userId);
        /// <summary>
        /// 图片列表
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        IEnumerable<FileInfoEntity> GetImageList(string userId);
        /// <summary>
        /// 回收站文件（夹）列表
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        IEnumerable<FileInfoEntity> GetRecycledList(string userId);
        /// <summary>
        /// 我的文件（夹）共享列表
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        IEnumerable<FileInfoEntity> GetMyShareList(string userId);
        /// <summary>
        /// 他人文件（夹）共享列表
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        IEnumerable<FileInfoEntity> GetOthersShareList(string userId);
        /// <summary>
        /// 文件信息实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        FileInfoEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 还原文件
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RestoreFile(string keyValue);
        /// <summary>
        /// 删除文件信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 彻底删除文件信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        void ThoroughRemoveForm(string keyValue);
        /// <summary>
        /// 保存文件信息表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="fileInfoEntity">文件信息实体</param>
        /// <returns></returns>
        void SaveForm(string keyValue, FileInfoEntity fileInfoEntity);
        /// <summary>
        /// 共享文件
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="IsShare">是否共享：1-共享 0取消共享</param>
        void ShareFile(string keyValue, int IsShare = 1);
        #endregion
    }
}