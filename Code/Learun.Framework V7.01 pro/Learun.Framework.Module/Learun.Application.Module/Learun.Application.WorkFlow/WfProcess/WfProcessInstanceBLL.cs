using Learun.Cache.Base;
using Learun.Cache.Factory;
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
    public class WfProcessInstanceBLL : WfProcessInstanceIBLL
    {
        private WfProcessInstanceService wfProcessService = new WfProcessInstanceService();

        #region 缓存定义
        private ICache cache = CacheFactory.CaChe();
        private string cacheKey = "learun_adms_wfprocess_";// +实例主键
        #endregion

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
                WfProcessInstanceEntity entity = cache.Read<WfProcessInstanceEntity>(cacheKey + keyValue, CacheId.workflow);
                if (entity == null)
                {
                    entity = wfProcessService.GetEntity(keyValue);
                    cache.Write<WfProcessInstanceEntity>(cacheKey + keyValue, entity, CacheId.workflow);
                }
                return entity;
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
        /// 获取流程信息列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public IEnumerable<WfProcessInstanceEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return wfProcessService.GetPageList(pagination, queryJson);
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
                return wfProcessService.GetMyPageList(userId, pagination, queryJson);
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
        /// 获取流程实例信息
        /// </summary>
        /// <param name="processId">实例主键</param>
        /// <returns></returns>
        public IEnumerable<WfProcessInstanceEntity> GetListByProcessIds(string processIds)
        {
            try
            {
                return wfProcessService.GetListByProcessIds(processIds);
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
                return wfProcessService.GetListByAllProcessIds(processIds);
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
                cache.Remove(cacheKey + keyValue, CacheId.workflow);
                wfProcessService.DeleteEntity(keyValue);
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
        /// 保存流程实例
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        public void SaveEntity(string keyValue, WfProcessInstanceEntity entity)
        {
            try
            {
                cache.Remove(cacheKey + keyValue, CacheId.workflow);
                wfProcessService.SaveEntity(keyValue, entity);
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
