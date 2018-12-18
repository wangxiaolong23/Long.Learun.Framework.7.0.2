using Learun.Util;
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
    public interface WfTaskIBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取未完成的流程实例任务列表
        /// </summary>
        /// <param name="processId">流程实例主键</param>
        /// <returns></returns>
        IEnumerable<WfTaskEntity> GetList(string processId);
        /// <summary>
        /// 获取当前任务节点主键
        /// </summary>
        /// <param name="processId">流程实例主键</param>
        /// <returns></returns>
        List<string> GetCurrentNodeIds(string processId);
        /// <summary>
        /// 获取任务实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        WfTaskEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取任务实体
        /// </summary>
        /// <param name="processId">流程实例主键</param>
        /// <param name="nodeId">节点主键</param>
        /// <returns></returns>
        WfTaskEntity GetEntity(string processId, string nodeId);
          /// <summary>
        /// 获取任务实体
        /// </summary>
        /// <param name="processId">流程实例主键</param>
        /// <param name="nodeId">节点主键</param>
        /// <returns></returns>
        WfTaskEntity GetEntityUnFinish(string processId, string nodeId);
        /// <summary>
        /// 获取未处理任务列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="pagination">翻页信息</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        IEnumerable<WfProcessInstanceEntity> GetActiveList(UserInfo userInfo, Pagination pagination, string queryJson);
        /// <summary>
        /// 获取已处理任务列表
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="pagination">翻页信息</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        IEnumerable<WfProcessInstanceEntity> GetHasList(string userId, Pagination pagination, string queryJson);
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存或更新流程实例任务
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        void SaveEntity(WfTaskEntity entity);
        /// <summary>
        /// 保存或更新流程实例任务
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="companyId">公司主键</param>
        /// <param name="departmentId">部门主键</param>
        void SaveEntitys(WfTaskEntity entity, string companyId, string departmentId);
        /// <summary>
        /// 更新任务状态
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state">状态 1 完成 2 关闭（会签 </param>
        void UpdateState(string keyValue, int state);
        /// <summary>
        /// 更新任务完成状态
        /// </summary>
        /// <param name="processId">流程实例主键</param>
        /// <param name="nodeId">节点主键</param>
        /// <param name="taskId">任务节点Id</param>
        /// <param name="userId">用户主键</param>
        /// <param name="userName">用户名称</param>
        void UpdateStateByNodeId(string processId, string nodeId, string taskId, string userId, string userName);
        #endregion
    }
}
