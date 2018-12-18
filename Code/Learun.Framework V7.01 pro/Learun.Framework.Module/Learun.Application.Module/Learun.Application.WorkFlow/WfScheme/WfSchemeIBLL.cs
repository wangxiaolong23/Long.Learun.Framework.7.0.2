using Learun.Util;
using System.Collections.Generic;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：工作流模板处理
    /// </summary>
    public interface WfSchemeIBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取流程分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <param name="category">分类</param>
        /// <returns></returns>
        IEnumerable<WfSchemeInfoEntity> GetSchemeInfoPageList(Pagination pagination, string keyword, string category);

        /// <summary>
        /// 获取流程模板分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userInfo">登录者信息</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<WfSchemeInfoEntity> GetAppSchemeInfoPageList(Pagination pagination, UserInfo userInfo, string queryJson);
        /// <summary>
        /// 获取自定义流程列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        IEnumerable<WfSchemeInfoEntity> GetCustmerSchemeInfoList(UserInfo userInfo);
        /// <summary>
        /// 获取自定义流程列表(app)
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        IEnumerable<WfSchemeInfoEntity> GetAppCustmerSchemeInfoList(UserInfo userInfo);
        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <returns></returns>
        IEnumerable<WfSchemeEntity> GetWfSchemeList(string schemeInfoId);
        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <returns></returns>
        IEnumerable<WfSchemeEntity> GetSchemePageList(Pagination pagination, string schemeInfoId);
        /// <summary>
        /// 获取模板基础信息的实体
        /// </summary>
        /// <param name="code">流程编号</param>
        /// <returns></returns>
        WfSchemeInfoEntity GetWfSchemeInfoEntityByCode(string code);
        /// <summary>
        /// 获取模板的实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        WfSchemeEntity GetWfSchemeEntity(string keyValue);
        /// <summary>
        /// 获取模板的实体通过流程编号
        /// </summary>
        /// <param name="code">流程编号</param>
        /// <returns></returns>
        WfSchemeEntity GetWfSchemeEntityByCode(string code);
        /// <summary>
        /// 获取流程模板权限列表
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <returns></returns>
        IEnumerable<WfSchemeAuthorizeEntity> GetWfSchemeAuthorizeList(string schemeInfoId);
        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        void VirtualDelete(string keyValue);
        /// <summary>
        /// 保存模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="wfSchemeInfoEntity">模板基础信息</param>
        /// <param name="wfSchemeEntity">模板信息</param>
        void SaveEntity(string keyValue, WfSchemeInfoEntity wfSchemeInfoEntity, WfSchemeEntity wfSchemeEntity, List<WfSchemeAuthorizeEntity>  wfSchemeAuthorizeList);
        /// <summary>
        /// 更新流程模板
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="schemeId">模板主键</param>
        void UpdateScheme(string schemeInfoId, string schemeId);

        /// <summary>
        /// 保存模板基础信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="schemeInfoEntity">模板基础信息</param>
        void SaveSchemeInfoEntity(string keyValue, WfSchemeInfoEntity schemeInfoEntity);
        /// <summary>
        /// 更新自定义表单模板状态
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="state">状态1启用0禁用</param>
        void UpdateState(string schemeInfoId, int state);

        #endregion

        #region 扩展方法
        /// <summary>
        /// 初始化模板数据
        /// </summary>
        /// <param name="wfSchemeEntity">模板数据</param>
        void SchemeInit(WfSchemeEntity wfSchemeEntity);
        /// <summary>
        /// 获取开始节点
        /// </summary>
        /// <returns></returns>
        WfNodeInfo GetStartNode();
         /// <summary>
        /// 获取流程处理节点
        /// </summary>
        /// <param name="nodeId">流程处理节点主键</param>
        /// <returns></returns>
        WfNodeInfo GetNode(string nodeId);
        /// <summary>
        /// 寻找到下一个节点
        /// </summary>
        /// <param name="nodeId">当前Id</param>
        /// <param name="transportType">流转类型1.同意2.不同意3.超时</param>
        /// <returns></returns>
        List<WfNodeInfo> GetNextNodes(string nodeId, WfTransportType transportType);
        /// <summary>
        /// 获取上一节点数据
        /// </summary>
        /// <param name="nodeId">节点主键</param>
        /// <returns></returns>
        int GetPreNodeNum(string nodeId);

        /// <summary>
        /// 判断两节点是否连接
        /// </summary>
        /// <param name="formNodeId">开始节点</param>
        /// <param name="toNodeId">结束节点</param>
        /// <returns></returns>
        bool IsToNode(string formNodeId, string toNodeId);
        #endregion
    }
}
