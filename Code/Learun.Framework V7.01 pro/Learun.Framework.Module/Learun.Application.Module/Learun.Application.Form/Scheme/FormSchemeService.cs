using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learun.Application.Form
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.01
    /// 描 述：表单模板
    /// </summary>
    public class FormSchemeService : RepositoryFactory
    {
        #region 属性 构造函数
        private string schemeInfoFieldSql;
        private string schemeFieldSql;
        public FormSchemeService()
        {
            schemeInfoFieldSql = @" 
                        t.F_Id,
                        t.F_Name,
                        t.F_Category,
                        t.F_SchemeId,
                        t.F_DeleteMark,
                        t.F_EnabledMark,
                        t.F_Description
            ";

            schemeFieldSql = @" 
                        t.F_Id,
                        t.F_SchemeInfoId,
                        t.F_Type,
                        t.F_CreateDate,
                        t.F_CreateUserId,
                        t.F_CreateUserName
                        ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取自定义表单列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FormSchemeInfoEntity> GetCustmerSchemeInfoList()
        {
            try
            {
                return this.BaseRepository().FindList<FormSchemeInfoEntity>(t => t.F_Type == 0 && t.F_DeleteMark == 0);
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
        /// 获取表单分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <param name="category">分类</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public IEnumerable<FormSchemeInfoEntity> GetSchemeInfoPageList(Pagination pagination, string keyword, string category, int type)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(schemeInfoFieldSql);
                strSql.Append(",t1.F_Type,t1.F_CreateDate,t1.F_CreateUserId,t1.F_CreateUserName ");
                strSql.Append(" FROM LR_Form_SchemeInfo t LEFT JOIN LR_Form_Scheme t1 ON t.F_SchemeId = t1.F_Id WHERE t.F_Type = @type AND t.F_DeleteMark = 0 ");

                if (!string.IsNullOrEmpty(keyword))
                {
                    strSql.Append(" AND t.F_Name like @keyword  ");
                    keyword = "%" + keyword + "%";
                }
                if (!string.IsNullOrEmpty(category))
                {
                    strSql.Append(" AND t.F_Category = @category ");
                }

                return this.BaseRepository().FindList<FormSchemeInfoEntity>(strSql.ToString(), new { keyword = keyword, type = type, category = category }, pagination);
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
        /// 获取表单分页列表(用于系统表单)
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <param name="category">分类</param>
        /// <returns></returns>
        public IEnumerable<FormSchemeInfoEntity> GetSchemeInfoPageList(Pagination pagination, string keyword, string category)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(schemeInfoFieldSql);
                strSql.Append(",t.F_CreateDate,t.F_CreateUserId,t.F_CreateUserName,t.F_ModifyDate,t.F_ModifyUserId,t.F_ModifyUserName,t.F_UrlAddress ");
                strSql.Append(" FROM LR_Form_SchemeInfo t  WHERE t.F_DeleteMark = 0 AND t.F_Type = 2 ");

                if (!string.IsNullOrEmpty(keyword))
                {
                    strSql.Append(" AND t.F_Name like @keyword  ");
                    keyword = "%" + keyword + "%";
                }
                if (!string.IsNullOrEmpty(category))
                {
                    strSql.Append(" AND t.F_Category = @category ");
                }

                return this.BaseRepository().FindList<FormSchemeInfoEntity>(strSql.ToString(), new { keyword = keyword, category = category }, pagination);
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
        /// 获取模板列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <returns></returns>
        public IEnumerable<FormSchemeEntity> GetSchemePageList(Pagination pagination, string schemeInfoId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(schemeFieldSql);
                strSql.Append(" FROM LR_Form_Scheme t WHERE 1=1 ");
                strSql.Append(" AND t.F_SchemeInfoId = @schemeInfoId ");

                return this.BaseRepository().FindList<FormSchemeEntity>(strSql.ToString(), new { schemeInfoId = schemeInfoId }, pagination);
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
        /// 获取模板基础信息的实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public FormSchemeInfoEntity GetSchemeInfoEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<FormSchemeInfoEntity>(keyValue);
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
        /// 获取模板的实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public FormSchemeEntity GetSchemeEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<FormSchemeEntity>(keyValue);
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
        /// 虚拟删除模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void VirtualDelete(string keyValue)
        {
            try
            {
                FormSchemeInfoEntity entity = new FormSchemeInfoEntity()
                {
                    F_Id = keyValue,
                    F_DeleteMark = 1
                };
                this.BaseRepository().Update(entity);
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
        /// 保存模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="schemeInfoEntity">模板基础信息</param>
        /// <param name="schemeEntity">模板信息</param>
        public void SaveEntity(string keyValue, FormSchemeInfoEntity schemeInfoEntity, FormSchemeEntity schemeEntity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    schemeInfoEntity.Create();
                }
                else
                {
                    schemeInfoEntity.Modify(keyValue);
                }
                #region 模板信息
                if (schemeEntity != null)
                {
                    schemeEntity.F_SchemeInfoId = schemeInfoEntity.F_Id;
                    schemeEntity.Create();
                    db.Insert(schemeEntity);
                    schemeInfoEntity.F_SchemeId = schemeEntity.F_Id;
                }
                #endregion

                #region 模板基础信息
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update(schemeInfoEntity);
                }
                else
                {
                    db.Insert(schemeInfoEntity);
                }
                #endregion

                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
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
        /// 保存模板基础信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="schemeInfoEntity">模板基础信息</param>
        public void SaveSchemeInfoEntity(string keyValue, FormSchemeInfoEntity schemeInfoEntity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    schemeInfoEntity.Modify(keyValue);
                    this.BaseRepository().Update(schemeInfoEntity);
                }
                else
                {
                    schemeInfoEntity.Create();
                    this.BaseRepository().Insert(schemeInfoEntity);
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
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 更新模板
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="schemeId">模板主键</param>
        public void UpdateScheme(string schemeInfoId, string schemeId)
        {
            try
            {
                FormSchemeEntity formSchemeEntity = GetSchemeEntity(schemeId);
                FormSchemeInfoEntity entity = new FormSchemeInfoEntity
                {
                    F_Id = schemeInfoId,
                    F_SchemeId = schemeId
                };
                if (formSchemeEntity.F_Type != 1)
                {
                    entity.F_EnabledMark = 0;
                }
                this.BaseRepository().Update(entity);
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
        /// 更新自定义表单模板状态
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="state">状态1启用0禁用</param>
        public void UpdateState(string schemeInfoId, int state)
        {
            try
            {
                FormSchemeInfoEntity entity = new FormSchemeInfoEntity
                {
                    F_Id = schemeInfoId,
                    F_EnabledMark = state
                };
                this.BaseRepository().Update(entity);
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
