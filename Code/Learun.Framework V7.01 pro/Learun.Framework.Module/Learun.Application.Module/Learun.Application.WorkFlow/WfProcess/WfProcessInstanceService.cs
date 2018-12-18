using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：工作流实例
    /// </summary>
    public class WfProcessInstanceService : RepositoryFactory
    {
        #region 获取数据
        /// <summary>
        /// 获取流程实例
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public WfProcessInstanceEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<WfProcessInstanceEntity>(keyValue);
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
        /// 获取流程信息列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public IEnumerable<WfProcessInstanceEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var expression = LinqExtensions.True<WfProcessInstanceEntity>();
                var queryParam = queryJson.ToJObject();
                // 分类
                if (!queryParam["categoryId"].IsEmpty()) // 1:未完成 2:已完成
                {
                    if (queryParam["categoryId"].ToString() == "1")
                    {
                        expression = expression.And(t => t.F_IsFinished == 0);
                    }
                    else
                    {
                        expression = expression.And(t => t.F_IsFinished == 1);
                    }
                }

                // 操作时间
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    DateTime startTime = queryParam["StartTime"].ToDate();
                    DateTime endTime = queryParam["EndTime"].ToDate();
                    expression = expression.And(t => t.F_CreateDate >= startTime && t.F_CreateDate <= endTime);
                }
                // 关键字
                if (!queryParam["keyword"].IsEmpty())
                {
                    string keyword = queryParam["keyword"].ToString();
                    expression = expression.And(t => t.F_ProcessName.Contains(keyword) || t.F_SchemeName.Contains(keyword));
                }
                return this.BaseRepository().FindList<WfProcessInstanceEntity>(expression, pagination);
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
        /// 获取我的流程信息列表
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public IEnumerable<WfProcessInstanceEntity> GetMyPageList(string userId, Pagination pagination, string queryJson)
        {
            try
            {
                var expression = LinqExtensions.True<WfProcessInstanceEntity>();
                var queryParam = queryJson.ToJObject();
                expression = expression.And(t => t.F_CreateUserId == userId);
                // 操作时间
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    DateTime startTime = queryParam["StartTime"].ToDate();
                    DateTime endTime = queryParam["EndTime"].ToDate();
                    expression = expression.And(t => t.F_CreateDate >= startTime && t.F_CreateDate <= endTime);
                }
                // 关键字
                if (!queryParam["keyword"].IsEmpty())
                {
                    string keyword = queryParam["keyword"].ToString();
                    expression = expression.And(t => t.F_ProcessName.Contains(keyword) || t.F_SchemeName.Contains(keyword));
                }
                return this.BaseRepository().FindList<WfProcessInstanceEntity>(expression, pagination);
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
        /// 获取流程实例信息（正在运行的）
        /// </summary>
        /// <param name="processId">实例主键</param>
        /// <returns></returns>
        public IEnumerable<WfProcessInstanceEntity> GetListByProcessIds(string processIds)
        {
            try
            {
                return this.BaseRepository().FindList<WfProcessInstanceEntity>(t => processIds.Contains(t.F_Id) && t.F_IsFinished == 0 && t.F_EnabledMark == 1);
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
        /// 获取全部流程实例
        /// </summary>
        /// <param name="processIds"></param>
        /// <returns></returns>
        public IEnumerable<WfProcessInstanceEntity> GetListByAllProcessIds(string processIds)
        {
            try
            {
                return this.BaseRepository().FindList<WfProcessInstanceEntity>(t => processIds.Contains(t.F_Id));
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
        /// 删除流程实例
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                this.BaseRepository().Delete<WfProcessInstanceEntity>(t => t.F_Id == keyValue);
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
        /// 保存流程实例
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        public void SaveEntity(string keyValue, WfProcessInstanceEntity entity)
        {
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    entity.Create();
                    this.BaseRepository().Insert(entity);
                }
                else
                {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
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
        #endregion
    }
}
