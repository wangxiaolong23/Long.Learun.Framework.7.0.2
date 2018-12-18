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
    /// 描 述：工作流委托规则
    /// </summary>
    public class WfDelegateRuleService : RepositoryFactory
    {
        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字(被委托人)</param>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        public IEnumerable<WfDelegateRuleEntity> GetPageList(Pagination pagination, string keyword, UserInfo userInfo)
        {
            try
            {
                var expression = LinqExtensions.True<WfDelegateRuleEntity>();
                if (!userInfo.isSystem)
                {
                    string userId = userInfo.userId;
                    expression = expression.And(t => t.F_CreateUserId == userId);
                }
                if (!string.IsNullOrEmpty(keyword))
                {
                    expression = expression.And(t => t.F_ToUserName.Contains(keyword));
                }
                return this.BaseRepository().FindList<WfDelegateRuleEntity>(expression, pagination);                
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
        /// 根据委托人获取委托记录
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WfDelegateRuleEntity> GetList(UserInfo userInfo)
        {
            try
            {
                string userId = userInfo.userId;
                DateTime datatime = DateTime.Now;
                return this.BaseRepository().FindList<WfDelegateRuleEntity>(t => t.F_ToUserId == userId && t.F_BeginDate >= datatime && t.F_EndDate <= datatime);
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
        /// 获取关联的模板数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<WfDelegateRuleRelationEntity> GetRelationList(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindList<WfDelegateRuleRelationEntity>(t => t.F_DelegateRuleId == keyValue);
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
        /// 删除实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteEntity(string keyValue)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                db.Delete<WfDelegateRuleEntity>(t => t.F_Id == keyValue);
                db.Delete<WfDelegateRuleRelationEntity>(t => t.F_DelegateRuleId == keyValue);
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
        /// 保存实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="wfDelegateRuleEntity">实体数据</param>
        /// <param name="schemeInfoList">关联模板主键</param>
        public void SaveEntity(string keyValue, WfDelegateRuleEntity wfDelegateRuleEntity, string[] schemeInfoList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    wfDelegateRuleEntity.Create();
                    db.Insert(wfDelegateRuleEntity);
                }
                else
                {
                    wfDelegateRuleEntity.Modify(keyValue);
                    db.Update(wfDelegateRuleEntity);
                    db.Delete<WfDelegateRuleRelationEntity>(t => t.F_DelegateRuleId == keyValue);
                }

                foreach (string schemeInfoId in schemeInfoList)
                {
                    WfDelegateRuleRelationEntity wfDelegateRuleRelationEntity = new WfDelegateRuleRelationEntity();
                    wfDelegateRuleRelationEntity.Create();
                    wfDelegateRuleRelationEntity.F_DelegateRuleId = wfDelegateRuleEntity.F_Id;
                    wfDelegateRuleRelationEntity.F_SchemeInfoId = schemeInfoId;
                    db.Insert(wfDelegateRuleRelationEntity);
                }

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
        /// 更新委托规则状态信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state"></param>
        public void UpdateState(string keyValue, int state)
        {
            try
            {
                WfDelegateRuleEntity wfDelegateRuleEntity = new WfDelegateRuleEntity
                {
                    F_Id = keyValue,
                    F_EnabledMark = state
                };
                this.BaseRepository().Update(wfDelegateRuleEntity);
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
