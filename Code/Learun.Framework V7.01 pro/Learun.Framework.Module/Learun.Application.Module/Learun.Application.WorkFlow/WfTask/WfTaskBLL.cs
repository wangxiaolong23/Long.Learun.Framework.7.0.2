using Learun.Application.Base.AuthorizeModule;
using Learun.Application.Organization;
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
    /// 描 述：任务实例
    /// </summary>
    public class WfTaskBLL : WfTaskIBLL
    {
        private WfTaskService wfTaskService = new WfTaskService();

        private UserIBLL userIBLL = new UserBLL();
        private UserRelationIBLL userRelationIBLL = new UserRelationBLL();

        #region 获取数据
        /// <summary>
        /// 获取未完成的流程实例任务列表
        /// </summary>
        /// <param name="processId">流程实例主键</param>
        /// <returns></returns>
        public IEnumerable<WfTaskEntity> GetList(string processId)
        {
            try
            {
                return wfTaskService.GetList(processId);
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
        /// 获取当前任务节点主键
        /// </summary>
        /// <param name="processId">流程实例主键</param>
        /// <returns></returns>
        public List<string> GetCurrentNodeIds(string processId)
        {
            try
            {
                return wfTaskService.GetCurrentNodeIds(processId);
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
        /// 获取任务实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public WfTaskEntity GetEntity(string keyValue)
        {
            try
            {
                return wfTaskService.GetEntity(keyValue);
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
        /// 获取任务实体
        /// </summary>
        /// <param name="processId">流程实例主键</param>
        /// <param name="nodeId">节点主键</param>
        /// <returns></returns>
        public WfTaskEntity GetEntity(string processId, string nodeId)
        {
            try
            {
                return wfTaskService.GetEntity(processId, nodeId);
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
        /// 获取任务实体
        /// </summary>
        /// <param name="processId">流程实例主键</param>
        /// <param name="nodeId">节点主键</param>
        /// <returns></returns>
        public WfTaskEntity GetEntityUnFinish(string processId, string nodeId)
        {
            try
            {
                return wfTaskService.GetEntityUnFinish(processId, nodeId);
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
        /// 获取委托信息列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<UserInfo> GetDelegateTask(string userId)
        {
            try
            {
                List<UserInfo> list = wfTaskService.GetDelegateTask(userId);

                foreach (var item in list)
                {
                    UserEntity userEntity = userIBLL.GetEntityByUserId(item.userId);
                    item.companyId = userEntity.F_CompanyId;
                    item.departmentId = userEntity.F_DepartmentId;
                    item.roleIds = userRelationIBLL.GetObjectIds(userEntity.F_UserId, 1);
                    item.postIds = userRelationIBLL.GetObjectIds(userEntity.F_UserId, 2);
                }

                return list;
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
        /// 获取未处理任务列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="pagination">翻页信息</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public IEnumerable<WfProcessInstanceEntity> GetActiveList(UserInfo userInfo, Pagination pagination, string queryJson)
        {
            try
            {
                List<UserInfo> delegateList = GetDelegateTask(userInfo.userId);
                return wfTaskService.GetActiveList(userInfo, pagination, queryJson, delegateList);
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
        /// 获取已处理任务列表
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="pagination">翻页信息</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public IEnumerable<WfProcessInstanceEntity> GetHasList(string userId, Pagination pagination, string queryJson)
        {
            try
            {
                return wfTaskService.GetHasList(userId, pagination, queryJson);
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
        /// 保存或更新流程实例任务
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        public void SaveEntity(WfTaskEntity entity)
        {
            try
            {
                wfTaskService.SaveEntity(entity);
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
        /// 保存或更新流程实例任务
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="companyId">公司主键</param>
        /// <param name="departmentId">部门主键</param>
        public void SaveEntitys(WfTaskEntity entity, string companyId, string departmentId)
        {
            try
            {
                wfTaskService.SaveEntitys(entity, companyId, departmentId);
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
        /// 更新任务状态
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state">状态 1 完成 2 关闭（会签 </param>
        public void UpdateState(string keyValue, int state)
        {
            try
            {
                wfTaskService.UpdateState(keyValue, state);
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
        /// 更新任务完成状态
        /// </summary>
        /// <param name="processId">流程实例主键</param>
        /// <param name="nodeId">节点主键</param>
        /// <param name="taskId">任务节点Id</param>
        /// <param name="userId">用户主键</param>
        /// <param name="userName">用户名称</param>
        public void UpdateStateByNodeId(string processId, string nodeId, string taskId, string userId, string userName)
        {
            try
            {
                wfTaskService.UpdateStateByNodeId(processId, nodeId, taskId, userId, userName);
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
