using Learun.DataBase.Repository;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
namespace Learun.Application.OA.File.FileInfo
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.06.20
    /// 描 述：文件管理
    /// </summary>
    public class FileInfoService : RepositoryFactory
    {
        #region 获取数据
        /// <summary>
        /// 所有文件（夹）列表
        /// </summary>
        /// <param name="folderId">文件夹Id</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<FileInfoEntity> GetList(string folderId, string userId)
        {
            var strSql = new StringBuilder();
            string folderCondition = "";
            string fileCondition = "";
            if (!string.IsNullOrEmpty(folderId))
            {
                folderCondition = " AND F_ParentId = @folderId";
                fileCondition=" AND F_FolderId = @folderId";
            }
            else
            {
                fileCondition = " AND F_FolderId = '0'";
            }
            strSql.Append(@"SELECT  *
                            FROM    ( SELECT    F_FolderId AS F_FileId ,
                                                F_ParentId AS F_FolderId ,
                                                F_FolderName AS F_FileName ,
                                                '' AS F_FileSize ,
                                                'folder' AS F_FileType ,
                                                F_CreateUserId,
                                                F_ModifyDate,
                                                F_IsShare 
                                      FROM      LR_OA_FileFolder  where F_DeleteMark = 0");
            strSql.Append(folderCondition);
            strSql.Append(" UNION ");
            strSql.Append(@"SELECT              F_FileId ,
                                                F_FolderId ,
                                                F_FileName ,
                                                F_FileSize ,
                                                F_FileType ,
                                                F_CreateUserId,
                                                F_ModifyDate,
                                                F_IsShare
                                      FROM      LR_OA_FileInfo where F_DeleteMark = 0 ");
            strSql.Append(fileCondition);
            strSql.Append(") t WHERE F_CreateUserId = @userId"); 
 
            strSql.Append(" ORDER BY CASE WHEN F_FileType = 'folder' THEN 1 ELSE 2 END, F_ModifyDate ASC");
            return this.BaseRepository().FindList<FileInfoEntity>(strSql.ToString(), new { userId = userId, folderId = folderId });
        }
        /// <summary>
        /// 文档列表
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<FileInfoEntity> GetDocumentList(string userId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  F_FileId ,
                                    F_FolderId ,
                                    F_FileName ,
                                    F_FileSize ,
                                    F_FileType ,
                                    F_CreateUserId ,
                                    F_ModifyDate,
                                    F_IsShare
                            FROM    LR_OA_FileInfo
                            WHERE   F_DeleteMark = 0
                                    AND F_FileType IN ( 'log', 'txt', 'pdf', 'doc', 'docx', 'ppt', 'pptx',
                                                      'xls', 'xlsx' )
                                    AND F_CreateUserId = @userId");
            strSql.Append(" ORDER BY F_ModifyDate ASC");
            return this.BaseRepository().FindList<FileInfoEntity>(strSql.ToString(), new { userId = userId });
        }
        /// <summary>
        /// 图片列表
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<FileInfoEntity> GetImageList(string userId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  F_FileId ,
                                    F_FolderId ,
                                    F_FileName ,
                                    F_FileSize ,
                                    F_FileType ,
                                    F_CreateUserId ,
                                    F_ModifyDate,
                                    F_IsShare
                            FROM    LR_OA_FileInfo
                            WHERE   F_DeleteMark = 0
                                    AND F_FileType IN ( 'ico', 'gif', 'jpeg', 'jpg', 'png', 'psd' )
                                    AND F_CreateUserId = @userId");
            strSql.Append(" ORDER BY F_ModifyDate ASC");
            return this.BaseRepository().FindList<FileInfoEntity>(strSql.ToString(), new { userId = userId });
        }
        /// <summary>
        /// 回收站文件（夹）列表
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<FileInfoEntity> GetRecycledList(string userId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    ( SELECT    F_FolderId AS F_FileId ,
                                                F_ParentId AS F_FolderId ,
                                                F_FolderName AS F_FileName ,
                                                '' AS F_FileSize ,
                                                'folder' AS F_FileType ,
                                                F_CreateUserId,
                                                F_ModifyDate
                                      FROM      LR_OA_FileFolder  where F_DeleteMark = 1
                                      UNION
                                      SELECT    F_FileId ,
                                                F_FolderId ,
                                                F_FileName ,
                                                F_FileSize ,
                                                F_FileType ,
                                                F_CreateUserId,
                                                F_ModifyDate
                                      FROM      LR_OA_FileInfo where F_DeleteMark = 1
                                    ) t WHERE F_CreateUserId = @userId");
            strSql.Append(" ORDER BY F_ModifyDate DESC");
            return this.BaseRepository().FindList<FileInfoEntity>(strSql.ToString(), new { userId = userId });
        }
        /// <summary>
        /// 我的文件（夹）共享列表
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<FileInfoEntity> GetMyShareList(string userId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    ( SELECT    F_FolderId AS F_FileId ,
                                                F_ParentId AS F_FolderId ,
                                                F_FolderName AS F_FileName ,
                                                '' AS F_FileSize ,
                                                'folder' AS F_FileType ,
                                                F_CreateUserId,
                                                F_ModifyDate
                                      FROM      LR_OA_FileFolder  WHERE F_DeleteMark = 0 AND F_IsShare = 1
                                      UNION
                                      SELECT    F_FileId ,
                                                F_FolderId ,
                                                F_FileName ,
                                                F_FileSize ,
                                                F_FileType ,
                                                F_CreateUserId,
                                                F_ModifyDate
                                      FROM      LR_OA_FileInfo WHERE F_DeleteMark = 0 AND F_IsShare = 1
                                    ) t WHERE F_CreateUserId = @userId");
            strSql.Append(" ORDER BY F_ModifyDate DESC");
            return this.BaseRepository().FindList<FileInfoEntity>(strSql.ToString(), new { userId = userId });
        }
        /// <summary>
        /// 他人文件（夹）共享列表
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<FileInfoEntity> GetOthersShareList(string userId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    ( SELECT    F_FolderId AS F_FileId ,
                                                F_ParentId AS F_FolderId ,
                                                F_FolderName AS F_FileName ,
                                                '' AS F_FileSize ,
                                                'folder' AS F_FileType ,
                                                F_CreateUserId,
                                                F_CreateUserName,
                                                F_ShareTime AS F_ModifyDate
                                      FROM      LR_OA_FileFolder  WHERE F_DeleteMark = 0 AND F_IsShare = 1
                                      UNION
                                      SELECT    F_FileId ,
                                                F_FolderId ,
                                                F_FileName ,
                                                F_FileSize ,
                                                F_FileType ,
                                                F_CreateUserId,
                                                F_CreateUserName,
                                                F_ShareTime AS F_ModifyDate
                                      FROM      LR_OA_FileInfo WHERE F_DeleteMark = 0 AND F_IsShare = 1
                                    ) t WHERE F_CreateUserId != @userId");
            strSql.Append(" ORDER BY F_ModifyDate DESC");
            return this.BaseRepository().FindList<FileInfoEntity>(strSql.ToString(), new { userId = userId });
        }
        /// <summary>
        /// 文件实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FileInfoEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<FileInfoEntity>(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 还原文件
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RestoreFile(string keyValue)
        {
            FileInfoEntity fileInfoEntity = new FileInfoEntity();
            fileInfoEntity.Modify(keyValue);
            fileInfoEntity.F_DeleteMark = 0;
            this.BaseRepository().Update(fileInfoEntity);
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            FileInfoEntity fileInfoEntity = new FileInfoEntity();
            fileInfoEntity.Modify(keyValue);
            fileInfoEntity.F_DeleteMark = 1;
            this.BaseRepository().Update(fileInfoEntity);
        }
        /// <summary>
        /// 彻底删除文件
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void ThoroughRemoveForm(string keyValue)
        {
            this.BaseRepository().Delete<FileInfoEntity>(t => t.F_FileId == keyValue);
        }
        /// <summary>
        /// 保存文件表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="fileInfoEntity">文件信息实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, FileInfoEntity fileInfoEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                fileInfoEntity.Modify(keyValue);
                this.BaseRepository().Update(fileInfoEntity);
            }
            else
            {
                fileInfoEntity.Create();
                this.BaseRepository().Insert(fileInfoEntity);
            }
        }
        /// <summary>
        /// 共享文件
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="IsShare">是否共享：1-共享 0取消共享</param>
        public void ShareFile(string keyValue, int IsShare)
        {
            FileInfoEntity fileInfoEntity = new FileInfoEntity();
            fileInfoEntity.F_FileId = keyValue;
            fileInfoEntity.F_IsShare = IsShare;
            fileInfoEntity.F_ShareTime = DateTime.Now;
            this.BaseRepository().Update(fileInfoEntity);
        }
        #endregion
    }
}
