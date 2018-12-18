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
    /// 描 述：任务实例处理记录
    /// </summary>
    public class WfTaskHistoryService : RepositoryFactory
    {
        #region 获取数据
        private string fieldSql = "";

        public WfTaskHistoryService()
        {
            fieldSql = @"
                    t.F_Id,
                    t.F_ProcessId,
                    t.F_NodeId,
                    t.F_NodeName,
                    t.F_TaskType,
                    t.F_Result,
                    t.F_PreviousId,
                    t.F_PreviousName,
                    t.F_CreateDate,
                    t.F_CreateUserId,
                    t.F_CreateUserName,
                    t.F_Description";
        }

        /// <summary>
        /// 获取流程实例
        /// </summary>
        /// <param name="processId">流程实例主键</param>
        /// <returns></returns>
        public IEnumerable<WfTaskHistoryEntity> GetList(string processId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_WF_TaskHistory t WHERE t.F_ProcessId = @processId ORDER BY t.F_CreateDate DESC ");
                return this.BaseRepository().FindList<WfTaskHistoryEntity>(strSql.ToString(), new { processId = processId });
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
        /// 保存流程实例任务处理完记录
        /// </summary>
        /// <param name="entity">实体</param>
        public void SaveEntity(WfTaskHistoryEntity entity)
        {
            try
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
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
