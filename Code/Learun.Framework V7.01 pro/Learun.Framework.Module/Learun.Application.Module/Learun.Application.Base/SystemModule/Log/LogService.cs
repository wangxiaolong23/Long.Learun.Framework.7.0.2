using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;

namespace Learun.Application.Base.SystemModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：系统日志数据库服务类
    /// </summary>
    public class LogService : RepositoryFactory
    {
        #region 获取数据
        /// <summary>
        /// 日志列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <param name="userId">操作用户Id</param>
        /// <returns></returns>
        public IEnumerable<LogEntity> GetPageList(Pagination pagination, string queryJson,string userId)
        {
            try
            {
                var expression = LinqExtensions.True<LogEntity>();
                var queryParam = queryJson.ToJObject();
                // 日志分类
                if (!queryParam["CategoryId"].IsEmpty())
                {
                    int categoryId = queryParam["CategoryId"].ToInt();
                    expression = expression.And(t => t.F_CategoryId == categoryId);
                }
                
                // 操作时间
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    DateTime startTime = queryParam["StartTime"].ToDate();
                    DateTime endTime = queryParam["EndTime"].ToDate();
                    expression = expression.And(t => t.F_OperateTime >= startTime && t.F_OperateTime <= endTime);
                }
                // 操作用户Id
                if (!queryParam["OperateUserId"].IsEmpty())
                {
                    string OperateUserId = queryParam["OperateUserId"].ToString();
                    expression = expression.And(t => t.F_OperateUserId == OperateUserId);
                }
                // 操作用户账户
                if (!queryParam["OperateAccount"].IsEmpty())
                {
                    string OperateAccount = queryParam["OperateAccount"].ToString();
                    expression = expression.And(t => t.F_OperateAccount.Contains(OperateAccount));
                }
                // 操作类型
                if (!queryParam["OperateType"].IsEmpty())
                {
                    string operateType = queryParam["OperateType"].ToString();
                    expression = expression.And(t => t.F_OperateType == operateType);
                }
                // 功能模块
                if (!queryParam["Module"].IsEmpty())
                {
                    string module = queryParam["Module"].ToString();
                    expression = expression.And(t => t.F_Module.Contains(module));
                }
                // 关键字
                if (!queryParam["keyword"].IsEmpty())
                {
                    string keyword = queryParam["keyword"].ToString();
                    expression = expression.And(t => t.F_Module.Contains(keyword) || t.F_OperateType.Contains(keyword) || t.F_IPAddress.Contains(keyword));
                }
                // 登录用户id
                if (!string.IsNullOrEmpty(userId))
                {
                    expression = expression.And(t => t.F_OperateUserId == userId);
                }
                return this.BaseRepository().FindList(expression, pagination);
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
        /// 日志实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public LogEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<LogEntity>(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 清空日志
        /// </summary>
        /// <param name="categoryId">日志分类Id</param>
        /// <param name="keepTime">保留时间段内</param>
        public void RemoveLog(int categoryId, string keepTime)
        {
            try
            {
                DateTime operateTime = DateTime.Now;
                if (keepTime == "7")//保留近一周
                {
                    operateTime = DateTime.Now.AddDays(-7);
                }
                else if (keepTime == "1")//保留近一个月
                {
                    operateTime = DateTime.Now.AddMonths(-1);
                }
                else if (keepTime == "3")//保留近三个月
                {
                    operateTime = DateTime.Now.AddMonths(-3);
                }
                var expression = LinqExtensions.True<LogEntity>();
                expression = expression.And(t => t.F_OperateTime <= operateTime);
                expression = expression.And(t => t.F_CategoryId == categoryId);
                this.BaseRepository().Delete(expression);
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
        /// 写日志
        /// </summary>
        /// <param name="logEntity">对象</param>
        public void WriteLog(LogEntity logEntity)
        {
            try
            {
                logEntity.F_LogId = Guid.NewGuid().ToString();
                logEntity.F_OperateTime = DateTime.Now;
                logEntity.F_DeleteMark = 0;
                logEntity.F_EnabledMark = 1;
                logEntity.F_IPAddress = Net.Ip;
                logEntity.F_Host = Net.Host;
                logEntity.F_Browser = Net.Browser;
                this.BaseRepository().Insert(logEntity);
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
