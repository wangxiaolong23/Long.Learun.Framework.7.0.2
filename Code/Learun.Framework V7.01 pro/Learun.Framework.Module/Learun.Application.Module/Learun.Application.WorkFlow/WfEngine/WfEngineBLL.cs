using Learun.Application.Organization;
using Learun.Application.Base.SystemModule;
using Learun.Ioc;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：工作流引擎
    /// </summary>
    public class WfEngineBLL : WfEngineIBLL
    {
        private WfProcessInstanceIBLL wfProcessInstanceIBLL = new WfProcessInstanceBLL();
        private WfSchemeIBLL wfSchemeIBLL = new WfSchemeBLL();
        private WfTaskIBLL wfTaskIBLL = new WfTaskBLL();
        private WfTaskHistoryIBLL wfTaskHistoryIBLL = new WfTaskHistoryBLL();
        private WfConfluenceIBLL wfConfluenceBLL = new WfConfluenceBLL();


        private UserIBLL userIBLL = new UserBLL();
        private DatabaseLinkIBLL databaseLinkIBLL = new DatabaseLinkBLL();

        #region 属性值
        /// <summary>
        /// 流程模板数据
        /// </summary>
        private WfSchemeEntity wfSchemeEntity = null;
        /// <summary>
        /// 流程模板信息数据
        /// </summary>
        private WfSchemeInfoEntity wfSchemeInfoEntity = null;

        private UserEntity processCreater = null;
        #endregion

        #region 工作流模板处理
        /// <summary>
        /// 初始化模板
        /// </summary>
        /// <param name="parameter"></param>
        private bool InitScheme(WfParameter parameter)
        {
            bool res = false;
            try
            {
                if (parameter.isNew)
                {
                    wfSchemeInfoEntity = wfSchemeIBLL.GetWfSchemeInfoEntityByCode(parameter.schemeCode);
                    wfSchemeEntity = wfSchemeIBLL.GetWfSchemeEntity(wfSchemeInfoEntity.F_SchemeId);
                }
                else
                {
                    // 如果是重新发起的流程实例(获取当前实例的流程模板)
                    WfProcessInstanceEntity wfProcessInstanceEntity = wfProcessInstanceIBLL.GetEntity(parameter.processId);
                    processCreater = userIBLL.GetEntityByUserId(wfProcessInstanceEntity.F_CreateUserId);
                    wfSchemeInfoEntity = wfSchemeIBLL.GetWfSchemeInfoEntityByCode(wfProcessInstanceEntity.F_SchemeCode);
                    wfSchemeEntity = wfSchemeIBLL.GetWfSchemeEntity(wfProcessInstanceEntity.F_SchemeId);
                }
                wfSchemeIBLL.SchemeInit(wfSchemeEntity);
                res = true;
                return res;
            }
            catch (Exception)
            {
                return res;
            }
        }

        #endregion

        #region 流程实例处理
        /// <summary>
        /// 保存实例数据
        /// </summary>
        /// <param name="parameter">参数</param>
        private void SaveProcess(WfParameter parameter)
        {
            try
            {
                WfProcessInstanceEntity wfProcessInstanceEntity = new WfProcessInstanceEntity();
                if (parameter.isNew)
                {
                    wfProcessInstanceEntity.F_Id = parameter.processId;
                    wfProcessInstanceEntity.F_SchemeId = wfSchemeEntity.F_Id;
                    wfProcessInstanceEntity.F_SchemeCode = wfSchemeInfoEntity.F_Code;
                    wfProcessInstanceEntity.F_SchemeName = wfSchemeInfoEntity.F_Name;
                    wfProcessInstanceEntity.F_ProcessName = parameter.processName;
                    wfProcessInstanceEntity.F_ProcessLevel = parameter.processLevel;

                    wfProcessInstanceEntity.F_CompanyId = parameter.companyId;
                    wfProcessInstanceEntity.F_DepartmentId = parameter.departmentId;
                    wfProcessInstanceEntity.F_CreateUserId = parameter.userId;
                    wfProcessInstanceEntity.F_CreateUserName = parameter.userName;
                    wfProcessInstanceEntity.F_Description = parameter.description;

                    wfProcessInstanceIBLL.SaveEntity("",wfProcessInstanceEntity);
                }
                else
                {
                    wfProcessInstanceEntity.F_IsAgain = 0;
                    wfProcessInstanceEntity.F_ProcessLevel = parameter.processLevel;
                    wfProcessInstanceEntity.F_Description = parameter.description;
                    wfProcessInstanceIBLL.SaveEntity(parameter.processId, wfProcessInstanceEntity);
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
        #endregion

        #region 流程节点处理
        /// <summary>
        /// 会签节点判断
        /// </summary>
        /// <param name="wfNodeInfo">节点信息</param>
        /// <param name="parameter">参数</param>
        /// <returns>0 不做处理 1 通过 -1 不通过</returns>
        private int CalcConfluenceNode(WfNodeInfo wfNodeInfo, WfNodeInfo preWfNodeInfo, WfParameter parameter, bool isOk)
        {
            int res = 0;
            try
            {
                List<WfConfluenceEntity> list = (List<WfConfluenceEntity>)wfConfluenceBLL.GetList(parameter.processId, wfNodeInfo.id);
                int notOkNum =  list.FindAll(t => t.F_IsOk == 0).Count;
                int okNum = list.FindAll(t => t.F_IsOk == 1).Count;
                int allNum = wfSchemeIBLL.GetPreNodeNum(wfNodeInfo.id);

                switch (wfNodeInfo.confluenceType)//会签策略1-所有步骤通过，2-一个步骤通过即可，3-按百分比计算
                {
                    case 1://所有步骤通过
                        if (isOk)
                        {
                            if (allNum == okNum + 1)
                            {
                                res = 1;
                                wfConfluenceBLL.DeleteEntity(parameter.processId, wfNodeInfo.id);
                            }
                            else
                            {
                                WfConfluenceEntity wfConfluenceEntity = new WfConfluenceEntity();
                                wfConfluenceEntity.F_ProcessId = parameter.processId;
                                wfConfluenceEntity.F_NodeId = wfNodeInfo.id;
                                wfConfluenceEntity.F_FormNodeId = preWfNodeInfo.id;
                                wfConfluenceEntity.F_IsOk = 1;
                                // 存储一条记录
                                wfConfluenceBLL.SaveEntity(wfConfluenceEntity);
                            }
                        }
                        else
                        {
                            res = -1;
                            wfConfluenceBLL.DeleteEntity(parameter.processId, wfNodeInfo.id);
                        }
                        break;
                    case 2:
                        if (isOk)
                        {
                            res = 1;
                            wfConfluenceBLL.DeleteEntity(parameter.processId, wfNodeInfo.id);
                        }
                        else
                        {
                            if ((list.Count + 1) == allNum)
                            {
                                res = -1;
                                wfConfluenceBLL.DeleteEntity(parameter.processId, wfNodeInfo.id);
                            }
                            else
                            {
                                WfConfluenceEntity wfConfluenceEntity = new WfConfluenceEntity();
                                wfConfluenceEntity.F_ProcessId = parameter.processId;
                                wfConfluenceEntity.F_NodeId = wfNodeInfo.id;
                                wfConfluenceEntity.F_FormNodeId = preWfNodeInfo.id;
                                wfConfluenceEntity.F_IsOk = 0;
                                // 存储一条记录
                                wfConfluenceBLL.SaveEntity(wfConfluenceEntity);
                            }
                        }
                        break;
                    case 3:
                        if (isOk)
                        {
                            if ((okNum + 1) * 100 / allNum >= Convert.ToInt32(wfNodeInfo.confluenceRate))
                            {
                                res = 1;
                                wfConfluenceBLL.DeleteEntity(parameter.processId, wfNodeInfo.id);
                            }
                            else
                            {
                                WfConfluenceEntity wfConfluenceEntity = new WfConfluenceEntity();
                                wfConfluenceEntity.F_ProcessId = parameter.processId;
                                wfConfluenceEntity.F_NodeId = wfNodeInfo.id;
                                wfConfluenceEntity.F_FormNodeId = preWfNodeInfo.id;
                                wfConfluenceEntity.F_IsOk = 1;
                                // 存储一条记录
                                wfConfluenceBLL.SaveEntity(wfConfluenceEntity);
                            }
                        }
                        else
                        {
                            if ((allNum - notOkNum - 1) * 100 / allNum < Convert.ToInt32(wfNodeInfo.confluenceRate))
                            {
                                res = -1;
                                wfConfluenceBLL.DeleteEntity(parameter.processId, wfNodeInfo.id);
                            }
                            else
                            {
                                WfConfluenceEntity wfConfluenceEntity = new WfConfluenceEntity();
                                wfConfluenceEntity.F_ProcessId = parameter.processId;
                                wfConfluenceEntity.F_NodeId = wfNodeInfo.id;
                                wfConfluenceEntity.F_FormNodeId = preWfNodeInfo.id;
                                wfConfluenceEntity.F_IsOk = 0;
                                // 存储一条记录
                                wfConfluenceBLL.SaveEntity(wfConfluenceEntity);
                            }
                        }

                        break;
                }

                if (res != 0)
                {
                    // 需要清除下此会签其他发布的任务
                    ClearConfluenceTask(wfNodeInfo.id, preWfNodeInfo, parameter.processId);
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
        /// <summary>
        /// 清除会签其他发布的任务
        /// </summary>
        /// <param name="nodeId">会签节点主键</param>
        /// <param name="preWfNodeInfo">上一个节点</param>
        /// <param name="processId">流程实例主键</param>
        /// <returns></returns>
        private void ClearConfluenceTask(string nodeId, WfNodeInfo preWfNodeInfo, string processId)
        {
            try
            {
                Dictionary<string, string> hasMap = new Dictionary<string, string>();// 记录已经处理的节点ID
                var taskList = wfTaskIBLL.GetList(processId);
                foreach (var task in taskList)
                {
                    if (task.F_IsFinished == 0 && task.F_NodeId != preWfNodeInfo.id)
                    {
                        if (hasMap.ContainsKey(task.F_NodeId))
                        {
                            wfTaskIBLL.UpdateState(hasMap[task.F_NodeId], 2);
                        }
                        else
                        {
                            if (wfSchemeIBLL.IsToNode(task.F_NodeId, nodeId))
                            {
                                hasMap.Add(task.F_NodeId, task.F_Id);
                                wfTaskIBLL.UpdateState(task.F_Id, 2);
                            }
                        }
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
        /// 条件节点判断
        /// </summary>
        /// <param name="wfNodeInfo">节点信息</param>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        private bool CalcConditionNode(WfNodeInfo wfNodeInfo, WfParameter parameter)
        {
            bool res = false;
            try
            {
                if (wfNodeInfo.conditions.Count > 0)
                {
                    #region 字段条件判断
                    var formData = parameter.formData.ToJObject();
                    foreach (var condition in wfNodeInfo.conditions)
                    {
                        res = false;
                        if (!formData[condition.fieldId].IsEmpty()) {
                            string v1 = Convert.ToString(formData[condition.fieldId].ToString());
                            switch (condition.compareType)//比较类型1.等于2.不等于3.大于4.大于等于5.小于6.小于等于7.包含8.不包含9.包含于10.不包含于
                            {
                                case 1:
                                    if (v1 == condition.value)
                                    {
                                        res = true;
                                    }
                                    break;
                                case 2:
                                    if (v1 != condition.value)
                                    {
                                        res = true;
                                    }
                                    break;
                                case 3:
                                    if (Convert.ToDecimal(v1) > Convert.ToDecimal(condition.value))
                                    {
                                        res = true;
                                    }
                                    break;
                                case 4:
                                    if (Convert.ToDecimal(v1) >= Convert.ToDecimal(condition.value))
                                    {
                                        res = true;
                                    }
                                    break;
                                case 5:
                                    if (Convert.ToDecimal(v1) < Convert.ToDecimal(condition.value))
                                    {
                                        res = true;
                                    }
                                    break;
                                case 6:
                                    if (Convert.ToDecimal(v1) <= Convert.ToDecimal(condition.value))
                                    {
                                        res = true;
                                    }
                                    break;
                                case 7:
                                    if (v1.Contains(condition.value))
                                    {
                                        res = true;
                                    }
                                    break;
                                case 8:
                                    if (!v1.Contains(condition.value))
                                    {
                                        res = true;
                                    }
                                    break;
                                case 9:
                                    if (condition.value.Contains(v1))
                                    {
                                        res = true;
                                    }
                                    break;
                                case 10:
                                    if (!condition.value.Contains(v1))
                                    {
                                        res = true;
                                    }
                                    break;
                            }
                        }
                    }
                    #endregion
                }
                else if (!string.IsNullOrEmpty(wfNodeInfo.conditionSql))
                {
                    string conditionSql = wfNodeInfo.conditionSql.Replace("{processId}", "@processId");
                    DataTable dataTable = databaseLinkIBLL.FindTable(wfNodeInfo.dbConditionId, conditionSql, new { processId = parameter.processId });
                    if (dataTable.Rows.Count > 0)
                    {
                        res = true;
                    }
                }
                else
                {
                    res = true;
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

        #region 流程实例任务处理
        /// <summary>
        /// 创建流程实例节点任务
        /// </summary>
        /// <param name="wfNodeInfo">节点信息</param>
        /// <param name="preWfNodeInfo">上一节点信息</param>
        /// <param name="parameter">参数</param>
        /// <returns></returns>
        private WfTaskEntity CreateTask(WfNodeInfo wfNodeInfo, WfNodeInfo preWfNodeInfo, WfParameter parameter)
        {
            try
            {
                WfTaskEntity wfTaskEntity = new WfTaskEntity();
                wfTaskEntity.F_ProcessId = parameter.processId;
                wfTaskEntity.F_NodeId = wfNodeInfo.id;
                wfTaskEntity.F_NodeName = wfNodeInfo.name;
                wfTaskEntity.auditors = wfNodeInfo.auditors;
                switch (wfNodeInfo.type)//开始startround;结束endround;一般stepnode;会签节点:confluencenode;条件判断节点：conditionnode;查阅节点：auditornode;
                {
                    case "startround":
                        wfTaskEntity.auditors = new List<WfAuditor>();
                        WfAuditor wfAuditor = new WfAuditor();
                        wfAuditor.id = Guid.NewGuid().ToString();
                        var wfProcessInstanceEntity = wfProcessInstanceIBLL.GetEntity(parameter.processId);
                        wfAuditor.auditorId = wfProcessInstanceEntity.F_CreateUserId;
                        wfAuditor.auditorName = wfProcessInstanceEntity.F_CreateUserName;
                        wfTaskEntity.auditors.Add(wfAuditor);

                        wfTaskEntity.F_TaskType = 2;
                        break;
                    case "stepnode":
                        wfTaskEntity.F_TaskType = 1;
                        break;
                    case "auditornode":
                        wfTaskEntity.F_TaskType = 3;
                        break;
                }
               
                wfTaskEntity.F_TimeoutAction = wfNodeInfo.timeoutAction;
                wfTaskEntity.F_TimeoutNotice = wfNodeInfo.timeoutNotice;

                wfTaskEntity.F_PreviousId = preWfNodeInfo.id;
                wfTaskEntity.F_PreviousName = preWfNodeInfo.name;
                
                wfTaskEntity.F_CreateUserId = parameter.userId;
                wfTaskEntity.F_CreateUserName = parameter.userName;

                return wfTaskEntity;
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
        /// 创建一个加签任务
        /// </summary>
        /// <param name="preWfNodeInfo">上一节点信息</param>
        /// <param name="parameter">流程参数信息</param>
        /// <returns></returns>
        private WfTaskEntity CreateTaskSign(WfNodeInfo preWfNodeInfo, WfParameter parameter)
        {
            try
            {
                WfTaskEntity wfTaskEntity = new WfTaskEntity();
                wfTaskEntity.F_ProcessId = parameter.processId;
                wfTaskEntity.F_NodeId = preWfNodeInfo.id;
                wfTaskEntity.F_NodeName = preWfNodeInfo.name;
                wfTaskEntity.F_TaskType = 4;
                wfTaskEntity.auditors = new List<WfAuditor>();
                WfAuditor wfAuditor = new WfAuditor();
                wfAuditor.id = Guid.NewGuid().ToString();
                wfAuditor.auditorId = parameter.auditorId;
                wfAuditor.auditorName = parameter.auditorName;
                wfTaskEntity.auditors.Add(wfAuditor);

                wfTaskEntity.F_PreviousId = preWfNodeInfo.id;
                wfTaskEntity.F_PreviousName = preWfNodeInfo.name;

                wfTaskEntity.F_CreateUserId = parameter.userId;
                wfTaskEntity.F_CreateUserName = parameter.userName;

                return wfTaskEntity;
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
        /// 获取接下来需要处理的任务列表
        /// </summary>
        /// <param name="nodeId">当前节点主键</param>
        /// <param name="transportType">节点流转类型</param>
        /// <param name="wfTaskList">任务列表</param>
        /// <param name="wfReadTaskList">任务列表</param>
        /// <param name="parameter">参数</param>
        /// <returns>false 表示流程结束 true 表示流程还未运行完</returns>
        private bool GetNextTaskes(WfNodeInfo currentNode, WfTransportType transportType, List<WfTaskEntity> wfTaskList, List<WfTaskEntity> wfReadTaskList, WfParameter parameter)
        {
            try
            {
                List<WfNodeInfo> nextNodes = wfSchemeIBLL.GetNextNodes(currentNode.id, transportType);
                if (nextNodes.Count == 0)
                {
                    return false;
                }
                foreach (var node in nextNodes)
                {
                    if (node.type == "conditionnode")// 条件节点
                    {
                        if (CalcConditionNode(node, parameter))
                        {
                            GetNextTaskes(node, WfTransportType.Agree, wfTaskList, wfReadTaskList, parameter);
                        }
                        else
                        {
                            GetNextTaskes(node, WfTransportType.Disagree, wfTaskList, wfReadTaskList, parameter);
                        }
                    }
                    else if (node.type == "confluencenode")// 会签节点
                    {
                        if (parameter.isGetAuditer)
                        {
                            GetNextTaskes(node, WfTransportType.Agree, wfTaskList, wfReadTaskList, parameter);
                        }
                        else
                        {
                            int confluenceRes;
                            if (transportType == WfTransportType.Agree)
                            {
                                confluenceRes = CalcConfluenceNode(node, currentNode, parameter, true);
                            }
                            else
                            {
                                confluenceRes = CalcConfluenceNode(node, currentNode, parameter, false);
                            }

                            if (confluenceRes == 1)
                            {
                                GetNextTaskes(node, WfTransportType.Agree, wfTaskList, wfReadTaskList, parameter);
                                currentNode.cfres = true;
                                currentNode.cfIocName = node.iocName;
                                currentNode.cfDbId = node.dbSuccessId;
                                currentNode.cfDbSql = node.dbSuccessSql;

                            }
                            else if (confluenceRes == -1)
                            {
                                GetNextTaskes(node, WfTransportType.Disagree, wfTaskList, wfReadTaskList, parameter);

                                currentNode.cfres = false;
                                currentNode.cfIocName = node.iocName;
                                currentNode.cfDbId = node.dbFailId;
                                currentNode.cfDbSql = node.dbFailSql;
                            }
                            else
                            {
                                // 添加一个空数据的任务项表示当前流程还未结束
                                WfTaskEntity wfTaskEntity = new WfTaskEntity();
                                wfTaskList.Add(wfTaskEntity);
                            }
                        }



                    }
                    else if (node.type == "auditornode") {
                        WfTaskEntity wfTaskEntity = CreateTask(node, currentNode, parameter);
                        wfReadTaskList.Add(wfTaskEntity);
                    }
                    else if (node.type != "endround")
                    {
                        WfTaskEntity wfTaskEntity = CreateTask(node, currentNode, parameter);
                        wfTaskList.Add(wfTaskEntity);
                    }
                }
                return true;
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

        #region 流程实例任务历史处理
        /// <summary>
        /// 创建一个任务处理记录
        /// </summary>
        /// <param name="parameter">工作流传递参数</param>
        /// <param name="wfTaskEntity">当前任务信息</param>
        /// <param name="wfNodeInfo">节点信息</param>
        /// <param name="result">节点信息 1.同意 2.反对 3.超时</param>
        /// <param name="description">处理意见</param>
        private void CreateTaskHistory(WfParameter parameter, WfNodeInfo wfNodeInfo, WfTaskEntity wfTaskEntity, int result, string description)
        {
            try
            {
                WfTaskHistoryEntity wfTaskHistoryEntity = new WfTaskHistoryEntity();
                wfTaskHistoryEntity.F_ProcessId = parameter.processId;
                wfTaskHistoryEntity.F_NodeId = wfNodeInfo.id;
                wfTaskHistoryEntity.F_NodeName = wfNodeInfo.name;
                wfTaskHistoryEntity.F_Result = result;
                wfTaskHistoryEntity.F_Description = description;

                if (wfTaskEntity == null)// 表示新创建流程实例
                {
                    wfTaskHistoryEntity.F_TaskType = 0;
                }
                else
                {
                    wfTaskHistoryEntity.F_TaskType = wfTaskEntity.F_TaskType;
                    wfTaskHistoryEntity.F_PreviousId = wfTaskEntity.F_PreviousId;
                    wfTaskHistoryEntity.F_PreviousName = wfTaskEntity.F_PreviousName;
                }
                wfTaskHistoryEntity.F_CreateUserId = parameter.userId;
                wfTaskHistoryEntity.F_CreateUserName = parameter.userName;

                wfTaskHistoryIBLL.SaveEntity(wfTaskHistoryEntity);
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

        #region 执行sql语句
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="processId">流程实例Id</param>
        /// <param name="dbId">数据库主键ID</param>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        private bool ExecuteSql(string processId, string dbId, string strSql)
        {
            bool res = false;
            try
            {
                if (!string.IsNullOrEmpty(dbId) && !string.IsNullOrEmpty(strSql))
                {
                    strSql = strSql.Replace("{processId}", "@processId");
                    databaseLinkIBLL.ExecuteBySql(dbId, strSql, new { processId = processId });
                }
                res = true;
            }
            catch (Exception)
            {
            }
            return res;
        }
        #endregion

        #region 对外API
        /// <summary>
        /// 流程发起初始化接口(用于流程发起前的数据获取)
        /// </summary>
        /// <param name="parameter">流程参数</param>
        /// <returns></returns>
        public WfResult<WfContent> Bootstraper(WfParameter parameter)
        {
            WfResult<WfContent> wfResult = new WfResult<WfContent>();
            try
            {
                // 初始化流程模板
                bool res = InitScheme(parameter);
                if (res)
                {
                    // 获取开始节点
                    WfNodeInfo startNode = wfSchemeIBLL.GetStartNode();
                    if (startNode == null)
                    {
                        wfResult.status = 2;
                        wfResult.desc = "获取不到开始节点信息!";
                    }
                    else
                    {
                        wfResult.status = 1;
                        wfResult.desc = "流程发起初始化成功!";
                        wfResult.data = new WfContent();
                        wfResult.data.currentNode = startNode;
                        wfResult.data.scheme = wfSchemeEntity.F_Scheme;

                        if (!parameter.isNew)
                        {
                            wfResult.data.currentNodeIds = wfTaskIBLL.GetCurrentNodeIds(parameter.processId);
                            wfResult.data.history = (List<WfTaskHistoryEntity>)wfTaskHistoryIBLL.GetList(parameter.processId);
                        }
                        else
                        {
                            wfResult.data.currentNodeIds = new List<string>();
                            wfResult.data.currentNodeIds.Add(startNode.id);
                        }
                    }
                }
                else
                {
                    wfResult.status = 2;
                    wfResult.desc = "获取流程模板失败!";
                }
                return wfResult;
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
        /// 创建流程实例
        /// </summary>
        /// <param name="parameter">流程参数</param>
        /// <returns></returns>
        public WfResult Create(WfParameter parameter)
        {
            WfResult wfResult = new WfResult();
            try
            {
                // 初始化流程模板
                InitScheme(parameter);
                // 获取开始节点
                WfNodeInfo startNode = wfSchemeIBLL.GetStartNode();

                // 获取下一个节点任务
                List<WfTaskEntity> wfTaskList = new List<WfTaskEntity>();
                List<WfTaskEntity> wfReadTaskList = new List<WfTaskEntity>();
                GetNextTaskes(startNode, WfTransportType.Agree, wfTaskList, wfReadTaskList, parameter);

                // 保存一个流程实例
                SaveProcess(parameter);

                // 创建一个任务处理记录
                if (!parameter.isNew)
                {
                    // 如果是重新发起的流程需要获取当前任务
                    // 更新当前任务节点
                    WfTaskEntity currentTask = wfTaskIBLL.GetEntityUnFinish(parameter.processId, startNode.id);
                    wfTaskIBLL.UpdateStateByNodeId(parameter.processId, startNode.id, currentTask.F_Id, parameter.userId, parameter.userName);
                    CreateTaskHistory(parameter, startNode, currentTask, 1, "【重新发起】" + parameter.description);
                }
                else
                {
                    CreateTaskHistory(parameter, startNode, null, 1, "【发起】" + parameter.description);
                }

                // 记录查阅节点
                foreach (var wfTask in wfReadTaskList)
                {
                    Dictionary<string, AuditerModel> auditers = parameter.auditers.ToObject<Dictionary<string, AuditerModel>>();


                    if (auditers != null && auditers.ContainsKey(wfTask.F_NodeId))
                    {
                        wfTask.auditors = new List<WfAuditor>();
                        WfAuditor wfAuditor = new WfAuditor()
                        {
                            id = Guid.NewGuid().ToString(),
                            auditorId = auditers[wfTask.F_NodeId].userId,
                            auditorName = auditers[wfTask.F_NodeId].userName
                        };
                        wfTask.auditors.Add(wfAuditor);
                    }

                    wfTaskIBLL.SaveEntitys(wfTask, parameter.companyId, parameter.departmentId);
                }



                if (wfTaskList.Count == 0)
                {
                    // 没有任务了表示该流程已经结束了
                    WfProcessInstanceEntity wfProcessInstanceEntity = new WfProcessInstanceEntity();
                    wfProcessInstanceEntity.F_IsFinished = 1;
                    wfProcessInstanceIBLL.SaveEntity(parameter.processId, wfProcessInstanceEntity);
                }
                else
                {
                    // 记录下面节点任务
                    foreach (var wfTask in wfTaskList)
                    {
                        Dictionary<string, AuditerModel> auditers = parameter.auditers.ToObject<Dictionary<string, AuditerModel>>();


                        if (auditers != null && auditers.ContainsKey(wfTask.F_NodeId)) {
                            wfTask.auditors = new List<WfAuditor>();
                            WfAuditor wfAuditor = new WfAuditor()
                            {
                                id = Guid.NewGuid().ToString(),
                                auditorId = auditers[wfTask.F_NodeId].userId,
                                auditorName = auditers[wfTask.F_NodeId].userName
                            };
                            wfTask.auditors.Add(wfAuditor);
                        }

                        wfTaskIBLL.SaveEntitys(wfTask, parameter.companyId, parameter.departmentId);
                        if (wfTask.F_TaskType == 2)
                        {
                            WfProcessInstanceEntity wfProcessInstanceEntity = new WfProcessInstanceEntity();
                            wfProcessInstanceEntity.F_IsAgain = 1;
                            wfProcessInstanceIBLL.SaveEntity(parameter.processId, wfProcessInstanceEntity);
                        }
                    }
                }

                wfResult.status = 1;
                wfResult.desc = "创建流程成功";

                // 触发执行sql语句
                if (!ExecuteSql(parameter.processId, startNode.dbSuccessId, startNode.dbSuccessSql))
                {
                    CreateTaskHistory(parameter, startNode, null, 1, "新发起一个流程实例【执行sql语句异常】");
                }
                // 触发接口方法
                if (!string.IsNullOrEmpty(startNode.iocName))
                {
                   INodeMethod iNodeMethod = UnityIocHelper.WfInstance.GetService<INodeMethod>(startNode.iocName);
                   iNodeMethod.Sucess(parameter.processId);
                }
               

                return wfResult;
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
        /// 审核流程节点
        /// </summary>
        /// <param name="parameter">流程参数verifyType：1.审核同意2.审核不同意3.加签4.加签-同意5.加签-不同意6.确认阅读7.保存草稿</param>
        /// <returns></returns>
        public WfResult Audit(WfParameter parameter)
        {
            WfResult wfResult = new WfResult();
            try
            {
                WfTaskEntity currentTask = wfTaskIBLL.GetEntity(parameter.taskId);
                if (currentTask == null)
                {
                    wfResult.desc = "找不到当前任务信息";
                    wfResult.status = 2;
                }
                else if (currentTask.F_IsFinished == 1)
                {
                    wfResult.desc = "当前任务已经被处理完";
                    wfResult.status = 2;
                }
                else {
                    // 初始化流程模板
                    parameter.processId = currentTask.F_ProcessId;
                    parameter.isNew = false;
                    InitScheme(parameter);

                    WfNodeInfo currentNode = wfSchemeIBLL.GetNode(currentTask.F_NodeId);
                    if (currentNode == null)
                    {
                        wfResult.desc = "获取不到当前审核节点信息,请检查流程模板是否被改动";
                        wfResult.status = 2;
                    }
                    else
                    {
                        // 获取下一个节点任务
                        List<WfTaskEntity> wfTaskList = new List<WfTaskEntity>();
                        List<WfTaskEntity> wfReadTaskList = new List<WfTaskEntity>();
                        bool res = true;
                        switch (parameter.verifyType)//1.审核同意2.审核不同意3.加签4.加签-同意5.加签-不同意6.确认阅读7.保存草稿(暂时不做处理)
                        {
                            case "1":
                                res = GetNextTaskes(currentNode, WfTransportType.Agree, wfTaskList, wfReadTaskList, parameter);
                                CreateTaskHistory(parameter, currentNode, currentTask, 1, parameter.description);
                                wfResult.desc = "流程审核成功";
                                break;
                            case "2":
                                res = GetNextTaskes(currentNode, WfTransportType.Disagree, wfTaskList, wfReadTaskList, parameter);
                                CreateTaskHistory(parameter, currentNode, currentTask, 2, parameter.description);
                                wfResult.desc = "流程审核成功";
                                break;
                            case "3":
                                // 创建一个任务节点
                                WfTaskEntity taskEntity = CreateTaskSign(currentNode, parameter);
                                wfTaskList.Add(taskEntity);
                                parameter.description = "请【" + parameter.auditorName + "】审核;" + parameter.description;
                                currentTask.F_TaskType = 4;
                                CreateTaskHistory(parameter, currentNode, currentTask, 1, parameter.description);
                                wfResult.desc = "流程加签成功";
                                break;
                            case "4":
                                res = GetNextTaskes(currentNode, WfTransportType.Agree, wfTaskList, wfReadTaskList, parameter);
                                parameter.description = "【加签】"+parameter.description;
                                currentTask.F_TaskType = 1;
                                CreateTaskHistory(parameter, currentNode, currentTask, 1, parameter.description);
                                wfResult.desc = "流程审核成功";
                                break;
                            case "5":
                                WfTaskEntity taskEntity2 = CreateTask(currentNode, currentNode, parameter);
                                wfTaskList.Add(taskEntity2);
                                parameter.description = "【加签】"+parameter.description;
                                currentTask.F_TaskType = 1;
                                CreateTaskHistory(parameter, currentNode, currentTask, 2, parameter.description);
                                wfResult.desc = "流程审核成功";
                                break;
                            case "6":
                                res = GetNextTaskes(currentNode, WfTransportType.Agree, wfTaskList, wfReadTaskList, parameter);
                                CreateTaskHistory(parameter, currentNode, currentTask, 1, parameter.description);
                                wfResult.desc = "流程确认成功";
                                break;
                        }


                        // 更新当前任务节点
                        wfTaskIBLL.UpdateStateByNodeId(currentTask.F_ProcessId, currentTask.F_NodeId, currentTask.F_Id, parameter.userId, parameter.userName);

                        if (parameter.verifyType == "6")
                        {
                            wfResult.status = 1;
                            return wfResult;
                        }

                        // 记录查阅节点
                        foreach (var wfTask in wfReadTaskList)
                        {
                            Dictionary<string, AuditerModel> auditers = parameter.auditers.ToObject<Dictionary<string, AuditerModel>>();
                            if (auditers != null && auditers.ContainsKey(wfTask.F_NodeId))
                            {
                                wfTask.auditors = new List<WfAuditor>();
                                WfAuditor wfAuditor = new WfAuditor()
                                {
                                    id = Guid.NewGuid().ToString(),
                                    auditorId = auditers[wfTask.F_NodeId].userId,
                                    auditorName = auditers[wfTask.F_NodeId].userName
                                };
                                wfTask.auditors.Add(wfAuditor);
                            }
                            wfTaskIBLL.SaveEntitys(wfTask, processCreater.F_CompanyId, processCreater.F_DepartmentId);
                        }


                        if (wfTaskList.Count == 0)
                        {
                            // 没有任务了表示该流程已经结束了
                            WfProcessInstanceEntity wfProcessInstanceEntity = new WfProcessInstanceEntity();
                            wfProcessInstanceEntity.F_IsFinished = 1;
                            wfProcessInstanceIBLL.SaveEntity(parameter.processId, wfProcessInstanceEntity);
                        }
                        else
                        {
                            // 记录下面节点任务
                            foreach (var wfTask in wfTaskList)
                            {
                                if (wfTask.auditors != null)
                                {
                                    switch (parameter.verifyType)//1.审核同意2.审核不同意3.加签4.加签-同意5.加签-不同意6.确认阅读7.保存草稿(暂时不做处理)
                                    {
                                        case "1":
                                        case "4":
                                        case "5":
                                            Dictionary<string, AuditerModel> auditers = parameter.auditers.ToObject<Dictionary<string, AuditerModel>>();
                                            if (auditers != null && auditers.ContainsKey(wfTask.F_NodeId))
                                            {
                                                wfTask.auditors = new List<WfAuditor>();
                                                WfAuditor wfAuditor = new WfAuditor()
                                                {
                                                    id = Guid.NewGuid().ToString(),
                                                    auditorId = auditers[wfTask.F_NodeId].userId,
                                                    auditorName = auditers[wfTask.F_NodeId].userName
                                                };
                                                wfTask.auditors.Add(wfAuditor);
                                            }
                                            break;
                                        case "2": // 驳回，原来谁审核就谁审核
                                            WfTaskEntity _wfTaskEntity = wfTaskIBLL.GetEntity(parameter.processId, wfTask.F_NodeId);
                                            if (_wfTaskEntity != null)
                                            {
                                                wfTask.auditors = new List<WfAuditor>();
                                                WfAuditor wfAuditor = new WfAuditor()
                                                {
                                                    id = Guid.NewGuid().ToString(),
                                                    auditorId = _wfTaskEntity.F_ModifyUserId,
                                                    auditorName = _wfTaskEntity.F_ModifyUserName
                                                };
                                                wfTask.auditors.Add(wfAuditor);
                                            }
                                            break;
                                    }

                                    wfTaskIBLL.SaveEntitys(wfTask, processCreater.F_CompanyId, processCreater.F_DepartmentId);
                                    if (wfTask.F_TaskType == 2)
                                    {
                                        WfProcessInstanceEntity wfProcessInstanceEntity = new WfProcessInstanceEntity();
                                        wfProcessInstanceEntity.F_IsAgain = 1;
                                        wfProcessInstanceIBLL.SaveEntity(parameter.processId, wfProcessInstanceEntity);
                                    }
                                }
                            }
                        }

                        #region 执行sql语句触发
                        int _res = 1;
                        switch (parameter.verifyType)//1.审核同意2.审核不同意3.加签4.加签-同意5.加签-不同意6.确认阅读7.保存草稿(暂时不做处理)
                        {
                            case "1":
                            case "4":
                            case "6":
                                if (!ExecuteSql(parameter.processId, currentNode.dbSuccessId, currentNode.dbSuccessSql))
                                {
                                    CreateTaskHistory(parameter, currentNode, currentTask, 1, parameter.description + "【执行sql语句异常】");
                                }
                                // 触发接口方法
                                if (!string.IsNullOrEmpty(currentNode.iocName))
                                {
                                    INodeMethod iNodeMethod = UnityIocHelper.WfInstance.GetService<INodeMethod>(currentNode.iocName);
                                    iNodeMethod.Sucess(parameter.processId);
                                }
                                break;
                            case "2":
                            case "5":
                                _res = 2;
                                if (!ExecuteSql(parameter.processId, currentNode.dbFailId, currentNode.dbFailSql))
                                {
                                    CreateTaskHistory(parameter, currentNode, currentTask, 2, parameter.description + "【执行sql语句异常】");
                                }
                                // 触发接口方法
                                if (!string.IsNullOrEmpty(currentNode.iocName))
                                {
                                    INodeMethod iNodeMethod = UnityIocHelper.WfInstance.GetService<INodeMethod>(currentNode.iocName);
                                    iNodeMethod.Fail(parameter.processId);
                                }
                                break;
                        }
                        // 会签
                        if (!ExecuteSql(parameter.processId, currentNode.cfDbId, currentNode.cfDbSql))
                        {
                            CreateTaskHistory(parameter, currentNode, currentTask, _res, parameter.description + "【执行sql语句异常】");
                        }
                        // 触发接口方法
                        if (!string.IsNullOrEmpty(currentNode.cfIocName))
                        {
                            INodeMethod iNodeMethod = UnityIocHelper.WfInstance.GetService<INodeMethod>(currentNode.cfIocName);
                            if (currentNode.cfres)
                            {
                                iNodeMethod.Sucess(parameter.processId);
                            }
                            else
                            {
                                iNodeMethod.Fail(parameter.processId);
                            }
                        }


                        #endregion

                        wfResult.status = 1;
                    }                
                }


                
                return wfResult;
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
        /// 获取下一个节点审核者信息
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public WfResult<List<object>> GetAuditer(WfParameter parameter)
        {
            WfResult<List<object>> wfResult = new WfResult<List<object>>();
            string companyId = parameter.companyId;
            string departmentId = parameter.departmentId;

            try
            {  
                // 获取下一个节点任务
                List<WfTaskEntity> wfTaskList = new List<WfTaskEntity>();
                List<WfTaskEntity> wfReadTaskList = new List<WfTaskEntity>();
                parameter.isGetAuditer = true;
                // 获取下一节点
                if (parameter.isNew)// 开始发起节点的人
                {
                    // 初始化流程模板
                    InitScheme(parameter);
                    // 获取开始节点
                    WfNodeInfo startNode = wfSchemeIBLL.GetStartNode();
                    GetNextTaskes(startNode, WfTransportType.Agree, wfTaskList, wfReadTaskList, parameter);
                }
                else
                {
                    if (!string.IsNullOrEmpty(parameter.taskId))// 审核节点
                    {
                        WfTaskEntity currentTask = wfTaskIBLL.GetEntity(parameter.taskId);
                        parameter.processId = currentTask.F_ProcessId;
                        InitScheme(parameter);
                        companyId = processCreater.F_CompanyId;
                        departmentId = processCreater.F_DepartmentId;
                        WfNodeInfo currentNode = wfSchemeIBLL.GetNode(currentTask.F_NodeId);
                        GetNextTaskes(currentNode, WfTransportType.Agree, wfTaskList, wfReadTaskList, parameter);
                    }
                    else {// 重新发起节点
                        InitScheme(parameter);
                        WfNodeInfo startNode = wfSchemeIBLL.GetStartNode();
                        GetNextTaskes(startNode, WfTransportType.Agree, wfTaskList, wfReadTaskList, parameter);
                    }
                }
                if (wfTaskList.Count <= 0 && wfReadTaskList.Count <= 0)
                {
                    wfResult.status = 3;// 表示没有一下节点
                }
                else
                {
                    wfResult.status = 1;// 表示有一下节点
                    List<object> list = new List<object>();
                    wfResult.data = list;
                    foreach (var item in wfReadTaskList)
                    {
                        if (item.auditors.Count > 0)
                        {
                            var data = new
                            {
                                auditors = item.auditors,
                                name = item.F_NodeName,
                                companyId = companyId,
                                departmentId = departmentId,
                                nodeId = item.F_NodeId,

                            };
                            list.Add(data);
                        }
                        else
                        {
                            var data = new
                            {
                                all = true,
                                name = item.F_NodeName,
                                nodeId = item.F_NodeId
                            };
                            list.Add(data);
                        }


                    }
                    foreach (var item in wfTaskList)
                    {
                        if (item.auditors.Count > 0)
                        {
                            var data = new
                            {
                                auditors = item.auditors,
                                name = item.F_NodeName,
                                companyId = companyId,
                                departmentId = departmentId,
                                nodeId = item.F_NodeId,
                               
                            };
                            list.Add(data);
                        }
                        else
                        {
                            var data = new
                            {
                                all = true,
                                name = item.F_NodeName,
                                nodeId = item.F_NodeId
                            };
                            list.Add(data);
                        }


                    }
                }

                return wfResult;
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
        /// 获取某个任务节点的信息
        /// </summary>
        /// <param name="parameter">流程参数</param>
        /// <returns></returns>
        public WfResult<WfContent> GetTaskInfo(WfParameter parameter)
        {
            WfResult<WfContent> wfResult = new WfResult<WfContent>();
            try
            {
                // 初始化模板信息
                bool res = InitScheme(parameter);
                if (res)
                {
                    // 获取任务实体信息
                    WfTaskEntity wfTaskEntity = wfTaskIBLL.GetEntity(parameter.taskId);
                    if (wfTaskEntity.F_IsFinished != 0)
                    {
                        wfResult.status = 2;
                        wfResult.desc = "该任务已经处理!";
                    }
                    else
                    {
                        // 获取任务所在节点信息
                        WfNodeInfo currentNode = wfSchemeIBLL.GetNode(wfTaskEntity.F_NodeId);
                        if (currentNode == null)
                        {
                            wfResult.status = 2;
                            wfResult.desc = "获取不到节点信息!";
                        }
                        else
                        {
                            wfResult.status = 1;
                            wfResult.desc = "获取流程任务信息成功!";

                            wfResult.data = new WfContent();
                            wfResult.data.currentNodeIds = wfTaskIBLL.GetCurrentNodeIds(parameter.processId);
                            wfResult.data.currentNode = currentNode;

                            // 获取下一个节点所有审核者信息
                            //List<WfTaskEntity> wfTaskList = new List<WfTaskEntity>();
                            //res = GetNextTaskes(currentNode, WfTransportType.Agree, wfTaskList, parameter);



                            wfResult.data.scheme = wfSchemeEntity.F_Scheme;
                            if (!parameter.isNew)
                            {
                                wfResult.data.history = (List<WfTaskHistoryEntity>)wfTaskHistoryIBLL.GetList(parameter.processId);
                            }
                        }
                    }
                }
                else
                {
                    wfResult.status = 2;
                    wfResult.desc = "获取流程模板失败!";
                }
                return wfResult;
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
        /// <param name="parameter">流程参数</param>
        /// <returns></returns>
        public WfResult<WfContent> GetProcessInfo(WfParameter parameter)
        {
            WfResult<WfContent> wfResult = new WfResult<WfContent>();
            try
            {
                // 初始化模板信息
                bool res = InitScheme(parameter);
                if (res)
                {
                    // 获取任务实体信息
                    WfTaskEntity wfTaskEntity;
                    WfNodeInfo currentNode;
                    WfNodeInfo startNode = wfSchemeIBLL.GetStartNode();
                    if (!string.IsNullOrEmpty(parameter.taskId))
                    {
                        wfTaskEntity = wfTaskIBLL.GetEntity(parameter.taskId);
                    }
                    else
                    {
                        
                        wfTaskEntity = wfTaskIBLL.GetEntity(parameter.processId, startNode.id);
                    }


                    // 获取任务所在节点信息
                    if (wfTaskEntity != null)
                    {
                        currentNode = wfSchemeIBLL.GetNode(wfTaskEntity.F_NodeId);
                    }
                    else
                    {
                        currentNode = wfSchemeIBLL.GetNode(startNode.id);
                    }
                    
                    if (currentNode == null)
                    {
                        wfResult.status = 2;
                        wfResult.desc = "获取不到节点信息!";
                    }
                    else
                    {
                        wfResult.status = 1;
                        wfResult.desc = "获取流程实例数据成功!";
                        wfResult.data = new WfContent();
                        wfResult.data.currentNodeIds = wfTaskIBLL.GetCurrentNodeIds(parameter.processId);
                        wfResult.data.currentNode = currentNode;
                        wfResult.data.scheme = wfSchemeEntity.F_Scheme;
                        wfResult.data.history = (List<WfTaskHistoryEntity>)wfTaskHistoryIBLL.GetList(parameter.processId);
                    }
                }
                else
                {
                    wfResult.status = 2;
                    wfResult.desc = "获取流程模板失败!";
                }
                return wfResult;
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
        /// 获取流程实例信息(流程监控)
        /// </summary>
        /// <param name="parameter">流程参数</param>
        /// <returns></returns>
        public WfResult<WfContent> GetProcessInfoByMonitor(WfParameter parameter)
        {
            WfResult<WfContent> wfResult = new WfResult<WfContent>();
            try
            {
                // 初始化模板信息
                bool res = InitScheme(parameter);
                if (res)
                {
                    // 获取任务实体信息
                    WfTaskEntity wfTaskEntity;
                    WfNodeInfo currentNode;
                    WfNodeInfo startNode = wfSchemeIBLL.GetStartNode();

                    var currentNodeIds = wfTaskIBLL.GetCurrentNodeIds(parameter.processId);

                    if (!string.IsNullOrEmpty(parameter.taskId))
                    {
                        wfTaskEntity = wfTaskIBLL.GetEntity(parameter.taskId);
                    }
                    else
                    {

                        wfTaskEntity = wfTaskIBLL.GetEntity(parameter.processId, currentNodeIds[0]);
                    }


                    // 获取任务所在节点信息
                    if (wfTaskEntity != null)
                    {
                        currentNode = wfSchemeIBLL.GetNode(wfTaskEntity.F_NodeId);
                    }
                    else
                    {
                        currentNode = wfSchemeIBLL.GetNode(startNode.id);
                    }

                    if (currentNode == null)
                    {
                        wfResult.status = 2;
                        wfResult.desc = "获取不到节点信息!";
                    }
                    else
                    {
                        wfResult.status = 1;
                        wfResult.desc = "获取流程实例数据成功!";
                        wfResult.data = new WfContent();
                        wfResult.data.currentNodeIds = currentNodeIds;
                        wfResult.data.currentNode = currentNode;
                        wfResult.data.scheme = wfSchemeEntity.F_Scheme;
                        wfResult.data.history = (List<WfTaskHistoryEntity>)wfTaskHistoryIBLL.GetList(parameter.processId);
                    }
                }
                else
                {
                    wfResult.status = 2;
                    wfResult.desc = "获取流程模板失败!";
                }
                return wfResult;
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

