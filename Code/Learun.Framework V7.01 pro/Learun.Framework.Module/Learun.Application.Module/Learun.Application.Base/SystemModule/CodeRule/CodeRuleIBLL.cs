using Learun.Util;
using System.Collections.Generic;
namespace Learun.Application.Base.SystemModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.01
    /// 描 述：编号规则
    /// </summary>
    public interface CodeRuleIBLL
    {
        #region 获取数据
        /// <summary>
        /// 规则列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="keyword">查询参数</param>
        /// <returns></returns>
        IEnumerable<CodeRuleEntity> GetPageList(Pagination pagination, string keyword);
        /// <summary>
        /// 规则列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<CodeRuleEntity> GetList();
        /// <summary>
        /// 规则实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        CodeRuleEntity GetEntity(string keyValue);
        /// <summary>
        /// 规则实体
        /// </summary>
        /// <param name="enCode">规则编码</param>
        /// <returns></returns>
        CodeRuleEntity GetEntityByCode(string enCode);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除规则
        /// </summary>
        /// <param name="keyValue">主键</param>
        void VirtualDelete(string keyValue);
        /// <summary>
        /// 保存规则表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="codeRuleEntity">规则实体</param>
        /// <returns></returns>
        void SaveEntity(string keyValue, CodeRuleEntity codeRuleEntity);
        #endregion

        #region 验证数据
        /// <summary>
        /// 规则编号不能重复
        /// </summary>
        /// <param name="enCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        bool ExistEnCode(string enCode, string keyValue);
        /// <summary>
        /// 规则名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        bool ExistFullName(string fullName, string keyValue);
        #endregion

        #region 单据编码处理
        /// <summary>
        /// 获取当前编号规则种子列表
        /// </summary>
        /// <param name="ruleId">编号规则主键</param>
        /// <returns></returns>
        List<CodeRuleSeedEntity> GetSeedList(string ruleId, UserInfo userInfo);
        /// <summary>
        /// 保存单据编号规则种子
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="codeRuleSeedEntity">种子实体</param>
        void SaveSeed(string keyValue, CodeRuleSeedEntity codeRuleSeedEntity, UserInfo userInfo);
        /// <summary>
        /// 获得指定模块或者编号的单据号
        /// </summary>
        /// <param name="enCode">编码</param>
        /// <param name="userId">用户ID</param>
        /// <returns>单据号</returns>
        string GetBillCode(string enCode, string userId = "");
        /// <summary>
        /// 占用单据号
        /// </summary>
        /// <param name="enCode">单据编码</param>
        /// <param name="userId">用户ID</param>
        /// <returns>true/false</returns>
        void UseRuleSeed(string enCode, string userId = "");
        #endregion
    }
}
