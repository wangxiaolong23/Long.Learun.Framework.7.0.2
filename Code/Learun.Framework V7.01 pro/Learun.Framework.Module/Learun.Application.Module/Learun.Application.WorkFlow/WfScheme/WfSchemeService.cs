using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：工作流模板处理
    /// </summary>
    public class WfSchemeService : RepositoryFactory
    {
        #region 属性 构造函数
        private string schemeInfoFieldSql;
        private string schemeFieldSql;
        public  WfSchemeService()
        {
            schemeInfoFieldSql = @" 
                        t.F_Id,
                        t.F_Code,
                        t.F_Name,
                        t.F_Category,
                        t.F_Kind,
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
        /// 获取流程分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <param name="category">分类</param>
        /// <returns></returns>
        public IEnumerable<WfSchemeInfoEntity> GetSchemeInfoPageList(Pagination pagination, string keyword, string category)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(schemeInfoFieldSql);
                strSql.Append(",t1.F_Type,t1.F_CreateDate,t1.F_CreateUserId,t1.F_CreateUserName ");
                strSql.Append(" FROM LR_WF_SchemeInfo t LEFT JOIN LR_WF_Scheme t1 ON t.F_SchemeId = t1.F_Id WHERE 1=1 AND t.F_DeleteMark = 0 ");

                if (!string.IsNullOrEmpty(keyword))
                {
                    strSql.Append(" AND ( t.F_Name like @keyword OR t.F_Code like @keyword ) ");
                    keyword = "%" + keyword + "%";
                }
                if (!string.IsNullOrEmpty(category))
                {
                    strSql.Append(" AND t.F_Category = @category ");
                }
                return this.BaseRepository().FindList<WfSchemeInfoEntity>(strSql.ToString(), new { keyword = keyword, category = category }, pagination);
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
        /// 获取流程模板分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userInfo">登录者信息</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<WfSchemeInfoEntity> GetAppSchemeInfoPageList(Pagination pagination, UserInfo userInfo, string queryJson)
        {
            try
            {
                string userId = userInfo.userId;
                string postIds = userInfo.postIds;
                string roleIds = userInfo.roleIds;
                List<WfSchemeAuthorizeEntity> list = (List<WfSchemeAuthorizeEntity>)this.BaseRepository().FindList<WfSchemeAuthorizeEntity>(t => t.F_ObjectId == null
                    || userId.Contains(t.F_ObjectId)
                    || postIds.Contains(t.F_ObjectId)
                    || roleIds.Contains(t.F_ObjectId)
                    );
                string schemeinfoIds = "";
                foreach (var item in list)
                {
                    schemeinfoIds += "'" + item.F_SchemeInfoId + "',";
                }
                schemeinfoIds = "(" + schemeinfoIds.Remove(schemeinfoIds.Length - 1, 1) + ")";


                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(schemeInfoFieldSql);
                strSql.Append(" FROM LR_WF_SchemeInfo t WHERE 1=1 AND t.F_DeleteMark = 0 AND t.F_EnabledMark = 1  AND t.F_Kind = 1 AND F_IsApp = 1 AND t.F_Id in " + schemeinfoIds);

                var queryParam = queryJson.ToJObject();
                string keyword = "";
                if (!queryParam["keyword"].IsEmpty())
                {
                    strSql.Append(" AND ( t.F_Name like @keyword OR t.F_Code like @keyword ) ");
                    keyword = "%" + queryParam["keyword"].ToString() + "%";
                }
                return this.BaseRepository().FindList<WfSchemeInfoEntity>(strSql.ToString(), new {keyword}, pagination);
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
        /// 获取自定义流程列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        public IEnumerable<WfSchemeInfoEntity> GetCustmerSchemeInfoList(UserInfo userInfo)
        {
            try
            {
                string userId = userInfo.userId;
                string postIds = userInfo.postIds;
                string roleIds = userInfo.roleIds;
                List<WfSchemeAuthorizeEntity> list = (List<WfSchemeAuthorizeEntity>)this.BaseRepository().FindList<WfSchemeAuthorizeEntity>(t => t.F_ObjectId == null
                    || userId.Contains(t.F_ObjectId)
                    || postIds.Contains(t.F_ObjectId)
                    || roleIds.Contains(t.F_ObjectId)
                    );
                string schemeinfoIds = "";
                foreach (var item in list)
                {
                    schemeinfoIds += "'" + item.F_SchemeInfoId + "',";
                }
                schemeinfoIds = "(" + schemeinfoIds.Remove(schemeinfoIds.Length - 1, 1) + ")";

                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(schemeInfoFieldSql);
                strSql.Append(" FROM LR_WF_SchemeInfo t WHERE 1=1 AND t.F_DeleteMark = 0 AND t.F_EnabledMark = 1  AND t.F_Kind = 1 AND t.F_Id in " + schemeinfoIds);

                return this.BaseRepository().FindList<WfSchemeInfoEntity>(strSql.ToString());
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
        /// 获取自定义流程列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        public IEnumerable<WfSchemeInfoEntity> GetAppCustmerSchemeInfoList(UserInfo userInfo)
        {
            try
            {
                string userId = userInfo.userId;
                string postIds = userInfo.postIds;
                string roleIds = userInfo.roleIds;
                List<WfSchemeAuthorizeEntity> list = (List<WfSchemeAuthorizeEntity>)this.BaseRepository().FindList<WfSchemeAuthorizeEntity>(t => t.F_ObjectId == null
                    || userId.Contains(t.F_ObjectId)
                    || postIds.Contains(t.F_ObjectId)
                    || roleIds.Contains(t.F_ObjectId)
                    );
                string schemeinfoIds = "";
                foreach (var item in list)
                {
                    schemeinfoIds += "'" + item.F_SchemeInfoId + "',";
                }
                schemeinfoIds = "(" + schemeinfoIds.Remove(schemeinfoIds.Length - 1, 1) + ")";

                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(schemeInfoFieldSql);
                strSql.Append(" FROM LR_WF_SchemeInfo t WHERE 1=1 AND t.F_DeleteMark = 0 AND t.F_EnabledMark = 1  AND t.F_Kind = 1 AND F_IsApp = 1 AND t.F_Id in " + schemeinfoIds);

                return this.BaseRepository().FindList<WfSchemeInfoEntity>(strSql.ToString());
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
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <returns></returns>
        public IEnumerable<WfSchemeEntity> GetWfSchemeList(string schemeInfoId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(schemeFieldSql);
                strSql.Append(" FROM LR_WF_Scheme t WHERE 1=1 ");
                strSql.Append(" AND t.F_SchemeInfoId = @schemeInfoId ");

                return this.BaseRepository().FindList<WfSchemeEntity>(strSql.ToString(), new { schemeInfoId = schemeInfoId });
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
        public IEnumerable<WfSchemeEntity> GetSchemePageList(Pagination pagination, string schemeInfoId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(schemeFieldSql);
                strSql.Append(" FROM LR_WF_Scheme t WHERE 1=1 ");
                strSql.Append(" AND t.F_SchemeInfoId = @schemeInfoId ");

                return this.BaseRepository().FindList<WfSchemeEntity>(strSql.ToString(), new { schemeInfoId = schemeInfoId }, pagination);
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
        public WfSchemeInfoEntity GetWfSchemeInfoEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<WfSchemeInfoEntity>(keyValue);
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
        /// <param name="code">流程编号</param>
        /// <returns></returns>
        public WfSchemeInfoEntity GetWfSchemeInfoEntityByCode(string code)
        {
            try
            {
                return this.BaseRepository().FindEntity<WfSchemeInfoEntity>(t => t.F_Code == code && t.F_DeleteMark == 0);
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
        public WfSchemeEntity GetWfSchemeEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<WfSchemeEntity>(keyValue);
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
        /// 获取流程模板权限列表
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <returns></returns>
        public IEnumerable<WfSchemeAuthorizeEntity> GetWfSchemeAuthorizeList(string schemeInfoId)
        {
            try
            {
                return this.BaseRepository().FindList<WfSchemeAuthorizeEntity>(t => t.F_SchemeInfoId == schemeInfoId);
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
                WfSchemeInfoEntity entity = new WfSchemeInfoEntity()
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
        /// <param name="wfSchemeInfoEntity">模板基础信息</param>
        /// <param name="wfSchemeEntity">模板信息</param>
        public void SaveEntity(string keyValue, WfSchemeInfoEntity wfSchemeInfoEntity, WfSchemeEntity wfSchemeEntity, List<WfSchemeAuthorizeEntity> wfSchemeAuthorizeList)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    wfSchemeInfoEntity.Create();
                }
                else
                {
                    wfSchemeInfoEntity.Modify(keyValue);
                }
                #region 模板信息
                if (wfSchemeEntity != null)
                {
                    wfSchemeEntity.F_SchemeInfoId = wfSchemeInfoEntity.F_Id;
                    wfSchemeEntity.Create();
                    db.Insert(wfSchemeEntity);
                    wfSchemeInfoEntity.F_SchemeId = wfSchemeEntity.F_Id;
                }
                #endregion

                #region 模板基础信息
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update(wfSchemeInfoEntity);
                }
                else
                {
                    db.Insert(wfSchemeInfoEntity);
                }
                #endregion

                #region 流程模板权限信息
                string schemeInfoId = wfSchemeInfoEntity.F_Id;
                db.Delete<WfSchemeAuthorizeEntity>(t => t.F_SchemeInfoId == schemeInfoId);
                foreach (var wfSchemeAuthorize in wfSchemeAuthorizeList)
                {
                    wfSchemeAuthorize.F_SchemeInfoId = schemeInfoId;
                    db.Insert(wfSchemeAuthorize);
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
        /// 更新流程模板
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="schemeId">模板主键</param>
        public void UpdateScheme(string schemeInfoId, string schemeId)
        {
            try
            {
                WfSchemeEntity wfSchemeEntity = GetWfSchemeEntity(schemeId);

                WfSchemeInfoEntity entity = new WfSchemeInfoEntity
                {
                    F_Id = schemeInfoId,
                    F_SchemeId = schemeId
                };
                if (wfSchemeEntity.F_Type != 1)
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
        /// 保存模板基础信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="schemeInfoEntity">模板基础信息</param>
        public void SaveSchemeInfoEntity(string keyValue, WfSchemeInfoEntity schemeInfoEntity)
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
        /// 更新自定义表单模板状态
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="state">状态1启用0禁用</param>
        public void UpdateState(string schemeInfoId, int state)
        {
            try
            {
                WfSchemeInfoEntity entity = new WfSchemeInfoEntity
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
