using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learun.Application.Base.SystemModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：附件管理
    /// </summary>
    public class AnnexesFileService:RepositoryFactory
    {
        #region 属性 构造函数
        private string fieldSql;
        public AnnexesFileService()
        {
            fieldSql = @" 
                   t.F_Id,
                   t.F_FolderId,
                   t.F_FileName,
                   t.F_FilePath,
                   t.F_FileSize,
                   t.F_FileExtensions,
                   t.F_FileType,
                   t.F_DownloadCount,
                   t.F_CreateDate,
                   t.F_CreateUserId,
                   t.F_CreateUserName
                    ";
        }
        #endregion


        #region 获取数据
        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="keyValues">主键值串</param>
        /// <returns></returns>
        public IEnumerable<AnnexesFileEntity> GetList(string folderId)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT " + fieldSql + " FROM LR_Base_AnnexesFile t WHERE t.F_FolderId = (@folderId) Order By t.F_CreateDate ");
                return this.BaseRepository().FindList<AnnexesFileEntity>(strSql.ToString(), new { folderId = folderId });
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
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
                string res = "";
                IEnumerable<AnnexesFileEntity> list = GetList(keyValue);
                foreach (var item in list)
                {
                    if (!string.IsNullOrEmpty(res))
                    {
                        res += ",";
                    }
                    res += item.F_FileName;
                }
                return res;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
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
                return this.BaseRepository().FindEntity<AnnexesFileEntity>(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
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
                annexesFileEntity.Create();
                annexesFileEntity.F_FolderId = folderId;
                this.BaseRepository().Insert(annexesFileEntity);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="fileId">文件主键</param>
        /// <param name="folderId">文件夹主键</param>
        public void DeleteEntity(string fileId)
        {
            try
            {
                this.BaseRepository().Delete(new AnnexesFileEntity() { F_Id = fileId });
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        #endregion
    }
}
