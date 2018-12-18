using Learun.Application.Base.AuthorizeModule;
using Learun.Application.Form;
using Learun.Application.Organization;
using Learun.Application.WorkFlow;
using Learun.Util;
using Nancy;
using System.Collections.Generic;

namespace Learun.Application.WebApi.Modules
{
    public class WorkFlowApi : BaseApi
    {
        /// <summary>
        /// 注册接口
        /// </summary>
        public WorkFlowApi()
            : base("/learun/adms/workflow")
        {
            Get["/bootstraper"] = GetBootstraper;
            Get["/taskinfo"] = Taskinfo;
            Get["/processinfo"] = ProcessInfo;


            Get["/scheme"] = GetScheme;

            Get["/mylist"] = GetMyProcess;// 获取数据字典详细列表
            Get["/mytask"] = GetMyTaskList;
            Get["/mytaskmaked"] = GetMyMakeTaskList;

            Get["/auditer"] = GetAuditer;

            Post["/create"] = Create;
            Post["/audit"] = Audit;

        }

        private UserRelationIBLL userRelationIBLL = new UserRelationBLL();
        private UserIBLL userIBLL = new UserBLL();

        private WfEngineIBLL wfEngineIBLL = new WfEngineBLL();
        private WfProcessInstanceIBLL wfProcessInstanceIBLL = new WfProcessInstanceBLL();
        private WfTaskIBLL wfTaskIBLL = new WfTaskBLL();
        private WfSchemeIBLL wfSchemeIBLL = new WfSchemeBLL();

        private FormSchemeIBLL formSchemeIBLL = new FormSchemeBLL();


        /// <summary>
        /// 获取流程模板
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Response GetScheme(dynamic _)
        {
            QueryModel parameter = this.GetReqData<QueryModel>();

            IEnumerable<WfSchemeInfoEntity> list = new List<WfSchemeInfoEntity>();
            list = wfSchemeIBLL.GetAppSchemeInfoPageList(parameter.pagination, this.userInfo,  parameter.queryJson);
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
        /// 初始化流程模板->获取开始节点数据
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Response GetBootstraper(dynamic _)
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
        /// 获取下一个节点审核人员
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
            List<object> nodelist = new List<object>();
            if (res.status == 1)
            {
                var list = res.data;
                foreach (var item1 in list)
                {
                    var item = item1.ToJson().ToJObject();
                    if (item["auditors"].IsEmpty())
                    {
                        var point = new
                        {
                            all = true,
                            name = item["name"],
                            nodeId = item["nodeId"]
                        };
                        nodelist.Add(point);
                    }
                    else
                    {
                        List<object> userlist = new List<object>();
                        foreach (var auditor in item["auditors"])
                        {
                            switch (auditor["type"].ToString())
                            {//获取人员信息1.岗位2.角色3.用户
                                case "1":
                                case "2":
                                    var userRelationList = userRelationIBLL.GetUserIdList(auditor["auditorId"].ToString());
                                    string userIds = "";
                                    foreach (var userRelation in userRelationList)
                                    {
                                        if (userIds != "")
                                        {
                                            userIds += ",";
                                        }
                                        userIds += userRelation.F_UserId;
                                    }
                                    var userList = userIBLL.GetListByUserIds(userIds);
                                    if (userList != null)
                                    {
                                        foreach (var user in userList)
                                        {
                                            if (user != null)
                                            {
                                                userlist.Add(new { id = user.F_UserId, name = user.F_RealName });
                                            }
                                        }
                                    }
                                    break;
                                case "3":
                                    userlist.Add(new { id = auditor["auditorId"], name = auditor["auditorName"] });
                                    break;
                            }
                        }
                        var point = new
                        {
                            name = item["name"],
                            nodeId = item["nodeId"],
                            list = userlist
                        };
                        nodelist.Add(point);
                    }
                }

                return Success(nodelist);
            }
            else
            {
                return Success(nodelist);
            }

        }

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

            List<FormParam> req = wfParameter.formreq.ToObject<List<FormParam>>();// 获取模板请求数据
            foreach (var item in req)
            {
                formSchemeIBLL.SaveInstanceForm(item.schemeInfoId, item.processIdName, item.keyValue, item.formData);
            }

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

            List<FormParam> req = wfParameter.formreq.ToObject<List<FormParam>>();// 获取模板请求数据
            foreach (var item in req)
            {
                formSchemeIBLL.SaveInstanceForm(item.schemeInfoId, item.processIdName, item.keyValue, item.formData);
            }

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
        /// <summary>
        /// 自定义表单提交参数
        /// </summary>
        private class FormParam
        {
            /// <summary>
            /// 流程模板id
            /// </summary>
            public string schemeInfoId { get; set; }
            /// <summary>
            /// 关联字段名称
            /// </summary>
            public string processIdName { get; set; }
            /// <summary>
            /// 数据主键值
            /// </summary>
            public string keyValue { get; set; }
            /// <summary>
            /// 表单数据
            /// </summary>
            public string formData { get; set; }
        }
    }
}