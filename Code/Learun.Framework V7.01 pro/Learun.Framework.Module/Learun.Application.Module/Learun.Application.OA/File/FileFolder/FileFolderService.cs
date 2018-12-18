using Learun.Application.OA.File.FileInfo;
using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Learun.Application.OA.File.FileFolder
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.06.20
    /// 描 述：文件管理
    /// </summary>
    public class FileFolderService : RepositoryFactory
    {
        #region 获取数据
        /// <summary>
        /// 文件夹列表
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<FileFolderEntity> GetList(string userId)
        {
            var expression = LinqExtensions.True<FileFolderEntity>();
            expression = expression.And(t => t.F_CreateUserId == userId);
            return this.BaseRepository().IQueryable(expression).ToList();
        }
        /// <summary>
        /// 文件夹实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public FileFolderEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<FileFolderEntity>(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 还原文件夹
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RestoreFile(string keyValue)
        {
            FileFolderEntity fileFolderEntity = new FileFolderEntity();
            fileFolderEntity.Modify(keyValue);
            fileFolderEntity.F_DeleteMark = 0;
            this.BaseRepository().Update(fileFolderEntity);
        }
        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            FileFolderEntity fileFolderEntity = new FileFolderEntity();
            fileFolderEntity.Modify(keyValue);
            fileFolderEntity.F_DeleteMark = 1;
            this.BaseRepository().Update(fileFolderEntity);
        }
        /// <summary>
        /// 彻底删除文件夹
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void ThoroughRemoveForm(string keyValue)
        {
            this.BaseRepository().Delete<FileFolderEntity>(t => t.F_FolderId == keyValue);
        }
        /// <summary>
        /// 清空回收站
        /// </summary>
        public void EmptyRecycledForm()
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                db.Delete<FileFolderEntity>(t => t.F_DeleteMark == 1);
                db.Delete<FileInfoEntity>(t => t.F_DeleteMark == 1);
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
                throw ExceptionEx.ThrowServiceException(ex);
            }
        }
        /// <summary>
        /// 保存文件夹表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="fileFolderEntity">文件夹实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, FileFolderEntity fileFolderEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                fileFolderEntity.Modify(keyValue);
                this.BaseRepository().Update(fileFolderEntity);
            }
            else
            {
                fileFolderEntity.Create();
                this.BaseRepository().Insert(fileFolderEntity);
            }
        }
        /// <summary>
        /// 共享文件夹
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="IsShare">是否共享：1-共享 0取消共享</param>
        public void ShareFolder(string keyValue, int IsShare)
        {
            FileFolderEntity fileFolderEntity = new FileFolderEntity();
            fileFolderEntity.F_FolderId = keyValue;
            fileFolderEntity.F_IsShare = IsShare;
            fileFolderEntity.F_ShareTime = DateTime.Now;
            this.BaseRepository().Update(fileFolderEntity);
        }
        #endregion
    }
}
