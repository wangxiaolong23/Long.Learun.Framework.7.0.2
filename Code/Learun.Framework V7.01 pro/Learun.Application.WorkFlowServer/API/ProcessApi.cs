using Learun.Application.WorkFlow;
using Learun.Util;
using Nancy;
using System.Collections.Generic;

namespace Learun.Application.WorkFlowServer.API
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.05.12
    /// 描 述：流程进程实例API
    /// </summary>
    public class ProcessApi : BaseApi
    {
        /// <summary>
        /// 注册接口
        /// </summary>
        public ProcessApi()
            : base("/workflow")
        {
            Post["/create"] = Create;
            Post["/audit"] = Audit;

            Get["/bootstraper"] = Bootstraper;
            Get["/taskinfo"] = Taskinfo;
            Get["/processinfo"] = ProcessInfo;

            Get["/myprocess"] = GetMyProcess;
            Get["/mytask"] = GetMyTaskList;
            Get["/mytaskmaked"] = GetMyMakeTaskList;
            Get["/schemelist"] = GetSchemeList;

            Get["/auditer"] = GetAuditer;
        }

        /// <summary>
        /// 工作流引擎
        /// </summary>
        private WfEngineIBLL wfEngineIBLL = new WfEngineBLL();
        private WfProcessInstanceIBLL wfProcessInstanceIBLL = new WfProcessInstanceBLL();
        private WfTaskIBLL wfTaskIBLL = new WfTaskBLL();
        private WfSchemeIBLL wfSchemeIBLL = new WfSchemeBLL();

        #region 获取信息
        /// <summary>
        /// 初始化流程模板->获取开始节点数据
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Response Bootstraper(dynamic _)
        {
            WfParameter wfParameter = this.GetReqData<WfParameter>();
            wfParameter.companyId = this.userInfo.companyId;
            wfParameter.departmentId = this.userInfo.departmentId;
            wfParameter.userId = this.userInfo.userId;
            wfParameter.userName = this.userInfo.realName;

            WfResult<WfContent> res = wfEngineIBLL.Bootstraper(wfParameter);
            return this.Success<WfResult<WfContent>>(res);
        }
        /// <summary>
        /// 获取流程审核节点的信息
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Response Taskinfo(dynamic _)
        {
            WfParameter wfParameter = this.GetReqData<WfParameter>();
            wfParameter.companyId = this.userInfo.companyId;
            wfParameter.departmentId = this.userInfo.departmentId;
            wfParameter.userId = this.userInfo.userId;
            wfParameter.userName = this.userInfo.realName;

            WfResult<WfContent> res = wfEngineIBLL.GetTaskInfo(wfParameter);
            return this.Success<WfResult<WfContent>>(res);
        }
        /// <summary>
        /// 获取流程实例信息
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Response ProcessInfo(dynamic _)
        {
            WfParameter wfParameter = this.GetReqData<WfParameter>();
            wfParameter.companyId = this.userInfo.companyId;
            wfParameter.departmentId = this.userInfo.departmentId;
            wfParameter.userId = this.userInfo.userId;
            wfParameter.userName = this.userInfo.realName;

            WfResult<WfContent> res = wfEngineIBLL.GetProcessInfo(wfParameter);
            return this.Success<WfResult<WfContent>>(res);
        }

        /// <summary>
        /// 获取我的流程实例信息
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Response GetMyProcess(dynamic _)
        {
            QueryModel parameter = this.GetReqData<QueryModel>();

            IEnumerable<WfProcessInstanceEntity> list = new List<WfProcessInstanceEntity>();
            list = wfProcessInstanceIBLL.GetMyPageList(this.userInfo.userId, parameter.pagination, parameter.queryJson);
            var jsonData = new
            {
                rows = list,
                total = parameter.pagination.total,
                page = parameter.pagination.page,
                records = parameter.pagination.records,
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 获取我的任务列表
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Response GetMyTaskList(dynamic _)
        {
            QueryModel parameter = this.GetReqData<QueryModel>();

            IEnumerable<WfProcessInstanceEntity> list = new List<WfProcessInstanceEntity>();
            list = wfTaskIBLL.GetActiveList(this.userInfo, parameter.pagination, parameter.queryJson);
            var jsonData = new
            {
                rows = list,
                total = parameter.pagination.total,
                page = parameter.pagination.page,
                records = parameter.pagination.records,
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 获取我已处理的任务列表
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Response GetMyMakeTaskList(dynamic _)
        {
            QueryModel parameter = this.GetReqData<QueryModel>();

            IEnumerable<WfProcessInstanceEntity> list = new List<WfProcessInstanceEntity>();
            list = wfTaskIBLL.GetHasList(this.userInfo.userId, parameter.pagination, parameter.queryJson);
            var jsonData = new
            {
                rows = list,
                total = parameter.pagination.total,
                page = parameter.pagination.page,
                records = parameter.pagination.records,
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取流程模板数据
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Response GetSchemeList(dynamic _)
        {
            var data = wfSchemeIBLL.GetCustmerSchemeInfoList(userInfo);
            return Success(data);
        }
        /// <summary>
        /// 获取下一个节点审核人员信息
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Response GetAuditer(dynamic _) {

            WfParameter wfParameter = this.GetReqData<WfParameter>();
            wfParameter.companyId = this.userInfo.companyId;
            wfParameter.departmentId = this.userInfo.departmentId;
            wfParameter.userId = this.userInfo.userId;
            wfParameter.userName = this.userInfo.realName;

            WfResult<List<object>> res = wfEngineIBLL.GetAuditer(wfParameter);
            return this.Success(res);
        }

        #endregion

        #region 提交信息
        /// <summary>
        /// 创建流程实例
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Response Create(dynamic _)
        {
            WfParameter wfParameter = this.GetReqData<WfParameter>();
            wfParameter.companyId = this.userInfo.companyId;
            wfParameter.departmentId = this.userInfo.departmentId;
            wfParameter.userId = this.userInfo.userId;
            wfParameter.userName = this.userInfo.realName;

            WfResult res = wfEngineIBLL.Create(wfParameter);
            return this.Success<WfResult>(res);
        }
        /// <summary>
        /// 审核流程实例
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Response Audit(dynamic _)
        {
            WfParameter wfParameter = this.GetReqData<WfParameter>();
            wfParameter.companyId = this.userInfo.companyId;
            wfParameter.departmentId = this.userInfo.departmentId;
            wfParameter.userId = this.userInfo.userId;
            wfParameter.userName = this.userInfo.realName;

            WfResult res = wfEngineIBLL.Audit(wfParameter);
            return this.Success<WfResult>(res);
        }
        #endregion

        /// <summary>
        /// 查询条件对象
        /// </summary>
        private class QueryModel
        {
            public Pagination pagination { get; set; }
            public string queryJson { get; set; }
        }

    }
}
