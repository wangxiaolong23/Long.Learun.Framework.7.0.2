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
    /// 日 期：2017.04.01
    /// 描 述：编号规则
    /// </summary>
    public class CodeRuleService : RepositoryFactory
    {
        #region 构造函数和属性
        private string fieldSql;
        public CodeRuleService()
        {
            fieldSql = @"
                    t.F_RuleId,
                    t.F_EnCode,
                    t.F_FullName,
                    t.F_CurrentNumber,
                    t.F_RuleFormatJson,
                    t.F_SortCode,
                    t.F_DeleteMark,
                    t.F_EnabledMark,
                    t.F_Description,
                    t.F_CreateDate,
                    t.F_CreateUserId,
                    t.F_CreateUserName,
                    t.F_ModifyDate,
                    t.F_ModifyUserId,
                    t.F_ModifyUserName
                     ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 规则列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="keyword">查询参数</param>
        /// <returns></returns>
        public IEnumerable<CodeRuleEntity> GetPageList(Pagination pagination, string keyword)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(" SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_Base_CodeRule t WHERE t.F_EnabledMark = 1 AND t.F_DeleteMark = 0 ");
                if (!string.IsNullOrEmpty(keyword)) {
                    strSql.Append(" AND ( F_EnCode LIKE @keyword OR F_FullName LIKE @keyword )  ");
                    keyword = '%' + keyword + '%';
                }
                return this.BaseRepository().FindList<CodeRuleEntity>(strSql.ToString(), new { keyword = keyword }, pagination);
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
        /// 规则列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CodeRuleEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(" SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_Base_CodeRule t WHERE t.F_EnabledMark = 1 AND t.F_DeleteMark = 0 ");
                return this.BaseRepository().FindList<CodeRuleEntity>(strSql.ToString());
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
        /// 规则实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public CodeRuleEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<CodeRuleEntity>(keyValue);
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
        /// 规则实体
        /// </summary>
        /// <param name="enCode">规则编码</param>
        /// <returns></returns>
        public CodeRuleEntity GetEntityByCode(string enCode) {
            try
            {
                return this.BaseRepository().FindEntity<CodeRuleEntity>(t => t.F_EnCode == enCode);
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
        /// 获取种子
        /// </summary>
        /// <param name="ruleId"></param>
        /// <returns></returns>
        public CodeRuleSeedEntity GetSeedEntity(string ruleId)
        {
            try
            {
                return this.BaseRepository().FindEntity<CodeRuleSeedEntity>(t => t.F_RuleId == ruleId);
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
        /// 删除规则
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void VirtualDelete(string keyValue)
        {
            try
            {
                CodeRuleEntity entity = new CodeRuleEntity()
                {
                    F_RuleId = keyValue,
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
        /// 保存规则表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="codeRuleEntity">规则实体</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, CodeRuleEntity codeRuleEntity, UserInfo userInfo = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    codeRuleEntity.Modify(keyValue, userInfo);
                    this.BaseRepository().Update(codeRuleEntity);
                }
                else
                {
                    codeRuleEntity.Create(userInfo);
                    this.BaseRepository().Insert(codeRuleEntity);
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

        #region 验证数据
        /// <summary>
        /// 规则编号不能重复
        /// </summary>
        /// <param name="enCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistEnCode(string enCode, string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(" SELECT t.F_RuleId FROM LR_Base_CodeRule t WHERE t.F_DeleteMark = 0 AND t.F_EnCode = @enCode ");
                if (!string.IsNullOrEmpty(keyValue))
                {
                    strSql.Append(" AND t.F_RuleId != @keyValue  ");
                }
                CodeRuleEntity entity = this.BaseRepository().FindEntity<CodeRuleEntity>(strSql.ToString(), new { enCode = enCode, keyValue = keyValue });

                return entity == null ? true : false;
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
        /// 规则名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(" SELECT t.F_RuleId FROM LR_Base_CodeRule t WHERE t.F_DeleteMark = 0 AND t.F_FullName = @fullName ");
                if (!string.IsNullOrEmpty(keyValue))
                {
                    strSql.Append(" AND t.F_RuleId != @keyValue  ");
                }
                CodeRuleEntity entity = this.BaseRepository().FindEntity<CodeRuleEntity>(strSql.ToString(), new { fullName = fullName, keyValue = keyValue });

                return entity == null ? true : false;
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

        #region 单据编码处理
        /// <summary>
        /// 获取当前编号规则种子列表
        /// </summary>
        /// <param name="ruleId">编号规则主键</param>
        /// <returns></returns>
        public List<CodeRuleSeedEntity> GetSeedList(string ruleId, UserInfo userInfo)
        {
            try
            {
                //获取当前最大种子
                List<CodeRuleSeedEntity> codeRuleSeedList = (List<CodeRuleSeedEntity>)this.BaseRepository().FindList<CodeRuleSeedEntity>(t => t.F_RuleId.Equals(ruleId));
                if (codeRuleSeedList.Count == 0)
                {
                    //说明没有种子，插入一条种子
                    CodeRuleSeedEntity codeRuleSeedEntity = new CodeRuleSeedEntity();
                    codeRuleSeedEntity.Create(userInfo);
                    codeRuleSeedEntity.F_SeedValue = 1;
                    codeRuleSeedEntity.F_RuleId = ruleId;
                    this.BaseRepository().Insert<CodeRuleSeedEntity>(codeRuleSeedEntity);
                    codeRuleSeedList.Add(codeRuleSeedEntity);
                }
                return codeRuleSeedList;
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
        /// 保存单据编号规则种子
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="codeRuleSeedEntity">种子实体</param>
        public void SaveSeed(string keyValue, CodeRuleSeedEntity codeRuleSeedEntity, UserInfo userInfo)
        {
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    codeRuleSeedEntity.Create(userInfo);
                    this.BaseRepository().Insert(codeRuleSeedEntity);
                }
                else
                {
                    codeRuleSeedEntity.Modify(keyValue, userInfo);
                    this.BaseRepository().Update(codeRuleSeedEntity);
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
        /// 删除种子，表示被占用了
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="ruleId">规则主键</param>
        public void DeleteSeed(string userId, string ruleId)
        {
            try
            {
                this.BaseRepository().Delete<CodeRuleSeedEntity>(t => t.F_UserId == userId && t.F_RuleId == ruleId);
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
