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
    /// 描 述：工作流模板处理
    /// </summary>
    public class WfSchemeBLL : WfSchemeIBLL
    {

        private WfSchemeService wfSchemeService = new WfSchemeService();

        #region 缓存定义
        private ICache cache = CacheFactory.CaChe();
        private string cacheKey = "learun_adms_wfscheme_";// +模板主键
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取流程分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <param name="category">分类</param>
        /// <returns></returns>
        public IEnumerable<WfSchemeInfoEntity> GetSchemeInfoPageList(Pagination pagination, string keyword, string category)
        {
            try
            {
                return wfSchemeService.GetSchemeInfoPageList(pagination, keyword, category);
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
        /// 获取流程模板分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userInfo">登录者信息</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<WfSchemeInfoEntity> GetAppSchemeInfoPageList(Pagination pagination, UserInfo userInfo, string queryJson)
        {
            try
            {
                return wfSchemeService.GetAppSchemeInfoPageList(pagination, userInfo, queryJson);
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
        /// 获取自定义流程列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        public IEnumerable<WfSchemeInfoEntity> GetCustmerSchemeInfoList(UserInfo userInfo)
        {
            try
            {
                return wfSchemeService.GetCustmerSchemeInfoList(userInfo);
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
        /// 获取自定义流程列表(app)
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        public IEnumerable<WfSchemeInfoEntity> GetAppCustmerSchemeInfoList(UserInfo userInfo)
        {
            try
            {
                return wfSchemeService.GetAppCustmerSchemeInfoList(userInfo);
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
        /// 获取模板列表
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <returns></returns>
        public IEnumerable<WfSchemeEntity> GetWfSchemeList(string schemeInfoId)
        {
            try
            {
                return wfSchemeService.GetWfSchemeList(schemeInfoId);
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
        /// 获取模板列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <returns></returns>
        public IEnumerable<WfSchemeEntity> GetSchemePageList(Pagination pagination, string schemeInfoId)
        {
            try
            {
                return wfSchemeService.GetSchemePageList(pagination, schemeInfoId);
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
        /// 获取模板基础信息的实体
        /// </summary>
        /// <param name="code">流程编号</param>
        /// <returns></returns>
        public WfSchemeInfoEntity GetWfSchemeInfoEntityByCode(string code)
        {
            try
            {
                WfSchemeInfoEntity wfSchemeInfoEntity = cache.Read<WfSchemeInfoEntity>(cacheKey + code, CacheId.workflow);
                if (wfSchemeInfoEntity == null)
                {
                    wfSchemeInfoEntity = wfSchemeService.GetWfSchemeInfoEntityByCode(code);
                    cache.Write<WfSchemeInfoEntity>(cacheKey + code, wfSchemeInfoEntity, CacheId.workflow);
                }
                return wfSchemeInfoEntity;
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
        /// 获取模板的实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public WfSchemeEntity GetWfSchemeEntity(string keyValue)
        {
            try
            {
                WfSchemeEntity wfSchemeEntity = cache.Read<WfSchemeEntity>(cacheKey + keyValue, CacheId.workflow);
                if (wfSchemeEntity == null)
                {
                    wfSchemeEntity = wfSchemeService.GetWfSchemeEntity(keyValue);
                    cache.Write<WfSchemeEntity>(cacheKey + keyValue, wfSchemeEntity, CacheId.workflow);
                }

                return wfSchemeEntity;
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
        /// 获取模板的实体通过流程编号
        /// </summary>
        /// <param name="code">流程编号</param>
        /// <returns></returns>
        public WfSchemeEntity GetWfSchemeEntityByCode(string code)
        {
            try
            {
                WfSchemeInfoEntity wfSchemeInfoEntity = GetWfSchemeInfoEntityByCode(code);

                WfSchemeEntity wfSchemeEntity = cache.Read<WfSchemeEntity>(cacheKey + wfSchemeInfoEntity.F_SchemeId, CacheId.workflow);
                if (wfSchemeEntity == null)
                {
                    wfSchemeEntity = wfSchemeService.GetWfSchemeEntity(wfSchemeInfoEntity.F_SchemeId);
                    cache.Write<WfSchemeEntity>(cacheKey + wfSchemeInfoEntity.F_SchemeId, wfSchemeEntity, CacheId.workflow);
                }

                return wfSchemeEntity;
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
        /// 获取流程模板权限列表
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <returns></returns>
        public IEnumerable<WfSchemeAuthorizeEntity> GetWfSchemeAuthorizeList(string schemeInfoId)
        {
            try
            {
                return wfSchemeService.GetWfSchemeAuthorizeList(schemeInfoId);
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
        /// 虚拟删除模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void VirtualDelete(string keyValue)
        {
            try
            {
                WfSchemeInfoEntity wfSchemeInfoEntity = wfSchemeService.GetWfSchemeInfoEntity(keyValue);
                cache.Remove(cacheKey + wfSchemeInfoEntity.F_Code, CacheId.workflow);
                cache.Remove(cacheKey + wfSchemeInfoEntity.F_SchemeId, CacheId.workflow);
                wfSchemeService.VirtualDelete(keyValue);
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
        /// 保存模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="wfSchemeInfoEntity">模板基础信息</param>
        /// <param name="wfSchemeEntity">模板信息</param>
        public void SaveEntity(string keyValue, WfSchemeInfoEntity wfSchemeInfoEntity, WfSchemeEntity wfSchemeEntity, List<WfSchemeAuthorizeEntity> wfSchemeAuthorizeList)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    WfSchemeEntity wfSchemeEntityOld = GetWfSchemeEntity(wfSchemeInfoEntity.F_SchemeId);
                    if (wfSchemeEntityOld.F_Scheme == wfSchemeEntity.F_Scheme && wfSchemeEntityOld.F_Type == wfSchemeEntity.F_Type)
                    {
                        wfSchemeEntity = null;
                    }
                    cache.Remove(cacheKey + wfSchemeInfoEntity.F_Code, CacheId.workflow);
                    cache.Remove(cacheKey + wfSchemeInfoEntity.F_SchemeId, CacheId.workflow);
                }
                wfSchemeService.SaveEntity(keyValue, wfSchemeInfoEntity, wfSchemeEntity, wfSchemeAuthorizeList);
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
        /// 更新流程模板
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="schemeId">模板主键</param>
        public void UpdateScheme(string schemeInfoId, string schemeId)
        {
            try
            {
                WfSchemeInfoEntity wfSchemeInfoEntity = wfSchemeService.GetWfSchemeInfoEntity(schemeInfoId);
                cache.Remove(cacheKey + wfSchemeInfoEntity.F_Code, CacheId.workflow);
                cache.Remove(cacheKey + wfSchemeInfoEntity.F_SchemeId, CacheId.workflow);
                wfSchemeService.UpdateScheme(schemeInfoId, schemeId);
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
        /// 保存模板基础信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="schemeInfoEntity">模板基础信息</param>
        public void SaveSchemeInfoEntity(string keyValue, WfSchemeInfoEntity schemeInfoEntity)
        {
            try
            {
                wfSchemeService.SaveSchemeInfoEntity(keyValue, schemeInfoEntity);
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
        /// 更新自定义表单模板状态
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="state">状态1启用0禁用</param>
        public void UpdateState(string schemeInfoId, int state)
        {
            try
            {
                wfSchemeService.UpdateState(schemeInfoId, state);
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

        #region 扩展方法
        private WfSchemeModel wfSchemeModel;
        private Dictionary<string, WfNodeInfo> nodesMap;
        /// <summary>
        /// 初始化模板数据
        /// </summary>
        /// <param name="wfSchemeEntity">模板数据</param>
        public void SchemeInit(WfSchemeEntity wfSchemeEntity)
        {
            try
            {
                wfSchemeModel = wfSchemeEntity.F_Scheme.ToObject<WfSchemeModel>();
                nodesMap = new Dictionary<string, WfNodeInfo>();
                foreach (var node in wfSchemeModel.nodes)
                {
                    if (!nodesMap.ContainsKey(node.id))
                    {
                        nodesMap.Add(node.id, node);
                    }
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
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        /// <summary>
        /// 获取开始节点
        /// </summary>
        /// <returns></returns>
        public WfNodeInfo GetStartNode()
        {
            try
            {
                WfNodeInfo startnode = null;
                foreach (var node in wfSchemeModel.nodes)
                {
                    if (node.type == "startround")
                    {
                        startnode = node;
                    }
                }
                return startnode;
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
        /// 获取流程处理节点
        /// </summary>
        /// <param name="nodeId">流程处理节点主键</param>
        /// <returns></returns>
        public WfNodeInfo GetNode(string nodeId)
        {
            try
            {
                return nodesMap[nodeId];
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
        /// 寻找到下一个节点
        /// </summary>
        /// <param name="nodeId">当前Id</param>
        /// <param name="transportType">流转类型1.同意2.不同意3.超时</param>
        /// <returns></returns>
        public List<WfNodeInfo> GetNextNodes(string nodeId, WfTransportType transportType)
        {
            try
            {
                List<WfNodeInfo> nextNodes= new List<WfNodeInfo>();
                // 找到与当前节点相连的线条
                foreach (var line in wfSchemeModel.lines)
                {
                    if (line.from == nodeId)
                    {
                        bool isOk = false;
                        switch (transportType)
                        {
                            case WfTransportType.Agree:
                                if (line.wftype == 1 || line.wftype == 4 || line.wftype == 6)
                                {
                                    isOk = true;
                                }
                                break;
                            case WfTransportType.Disagree:
                                if (line.wftype == 2 || line.wftype == 5 || line.wftype == 6)
                                {
                                    isOk = true;
                                }
                                break;
                            case WfTransportType.Overtime:
                                if (line.wftype == 3 || line.wftype == 4 || line.wftype == 5)
                                {
                                    isOk = true;
                                }
                                break;
                        }
                        if (isOk)
                        {
                            WfNodeInfo nextNode = nodesMap[line.to];
                            if (nextNode != null)
                            {
                                nextNodes.Add(nextNode);
                            }
                        }
                    }
                }
                return nextNodes;
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
        /// 获取上一节点数据
        /// </summary>
        /// <param name="nodeId">节点主键</param>
        /// <returns></returns>
        public int GetPreNodeNum(string nodeId)
        {
            int num = 0;
            try
            {
                // 找到与当前节点相连的线条
                foreach (var line in wfSchemeModel.lines)
                {
                    if (line.to == nodeId)
                    {
                        num++;
                    }
                }
                return num;
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
        /// 判断两节点是否连接
        /// </summary>
        /// <param name="formNodeId">开始节点</param>
        /// <param name="toNodeId">结束节点</param>
        /// <returns></returns>
        public bool IsToNode(string formNodeId, string toNodeId)
        {
            bool res = false;
            try
            {
                foreach (var line in wfSchemeModel.lines)
                {
                    if (line.from == formNodeId)
                    {
                        if (line.to == toNodeId)
                        {
                            res = true;
                            break;
                        }
                        else
                        {
                            if (line.to == formNodeId || nodesMap[line.to] == null || nodesMap[line.to].type == "endround")
                            {
                                break;
                            }
                            else
                            {
                                if (IsToNode(line.to, toNodeId))
                                {
                                    res = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                return res;
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
