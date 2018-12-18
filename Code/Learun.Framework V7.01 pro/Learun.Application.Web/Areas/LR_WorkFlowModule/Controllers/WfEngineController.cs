using Learun.Application.Base.AuthorizeModule;
using Learun.Application.Organization;
using Learun.Application.WorkFlow;
using Learun.Util;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_WorkFlowModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.01.16
    /// 描 述：流程引擎（流程接口）
    /// </summary>
    public class WfEngineController : MvcControllerBase
    {
        private UserRelationIBLL userRelationIBLL = new UserRelationBLL();
        private UserIBLL userIBLL = new UserBLL();
        
        /// <summary>
        /// 工作流引擎
        /// </summary>
        private WfEngineIBLL wfEngineIBLL = new WfEngineBLL();
        private WfProcessInstanceIBLL wfProcessInstanceIBLL = new WfProcessInstanceBLL();
        private WfTaskIBLL wfTaskIBLL = new WfTaskBLL();
        private WfSchemeIBLL wfSchemeIBLL = new WfSchemeBLL();

        #region 获取数据
        /// <summary>
        /// 初始化流程模板->获取开始节点数据
        /// </summary>
        /// <param name="isNew">是否是新发起的实例</param>
        /// <param name="processId">流程实例ID</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult Bootstraper(bool isNew, string processId, string schemeCode)
        {
            WfParameter wfParameter = new WfParameter();
            UserInfo userInfo = LoginUserInfo.Get();

            wfParameter.companyId = userInfo.companyId;
            wfParameter.departmentId = userInfo.departmentId;
            wfParameter.userId = userInfo.userId;
            wfParameter.userName = userInfo.realName;
            wfParameter.isNew = isNew;
            wfParameter.processId = processId;
            wfParameter.schemeCode = schemeCode;

            WfResult<WfContent> res = wfEngineIBLL.Bootstraper(wfParameter);
            return Success(res);
        }
        /// <summary>
        /// 流程任务信息
        /// </summary>
        /// <param name="processId">流程实例ID</param>
        /// <param name="taskId">流程模板编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult Taskinfo(string processId, string taskId)
        {
            WfParameter wfParameter = new WfParameter();
            UserInfo userInfo = LoginUserInfo.Get();

            wfParameter.companyId = userInfo.companyId;
            wfParameter.departmentId = userInfo.departmentId;
            wfParameter.userId = userInfo.userId;
            wfParameter.userName = userInfo.realName;
            wfParameter.processId = processId;
            wfParameter.taskId = taskId;

            WfResult<WfContent> res = wfEngineIBLL.GetTaskInfo(wfParameter);
            return Success(res);
        }
        /// <summary>
        /// 获取流程实例信息
        /// </summary>
        /// <param name="processId">流程实例ID</param>
        /// <param name="taskId">流程模板编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult Processinfo(string processId, string taskId)
        {
            WfParameter wfParameter = new WfParameter();
            UserInfo userInfo = LoginUserInfo.Get();

            wfParameter.companyId = userInfo.companyId;
            wfParameter.departmentId = userInfo.departmentId;
            wfParameter.userId = userInfo.userId;
            wfParameter.userName = userInfo.realName;
            wfParameter.processId = processId;
            wfParameter.taskId = taskId;

            WfResult<WfContent> res = wfEngineIBLL.GetProcessInfo(wfParameter);
            return Success(res);
        }
        /// <summary>
        /// 获取流程实例信息(流程监控)
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult ProcessinfoByMonitor(string processId, string taskId)
        {
            WfParameter wfParameter = new WfParameter();
            UserInfo userInfo = LoginUserInfo.Get();

            wfParameter.companyId = userInfo.companyId;
            wfParameter.departmentId = userInfo.departmentId;
            wfParameter.userId = userInfo.userId;
            wfParameter.userName = userInfo.realName;
            wfParameter.processId = processId;
            wfParameter.taskId = taskId;

            WfResult<WfContent> res = wfEngineIBLL.GetProcessInfoByMonitor(wfParameter);
            return Success(res);
        }
        /// <summary>
        /// 获取下一个节点审核人员
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult Auditer(bool isNew, string processId, string schemeCode, string taskId,string formData)
        {
            WfParameter wfParameter = new WfParameter();
            UserInfo userInfo = LoginUserInfo.Get();

            wfParameter.companyId = userInfo.companyId;
            wfParameter.departmentId = userInfo.departmentId;
            wfParameter.userId = userInfo.userId;
            wfParameter.userName = userInfo.realName;

            wfParameter.isNew = isNew;
            wfParameter.processId = processId;
            wfParameter.schemeCode = schemeCode;
            wfParameter.taskId = taskId;
            wfParameter.formData = formData;

            WfResult<List<object>> res = wfEngineIBLL.GetAuditer(wfParameter);

            if (res.status == 1)
            {
                List<object> nodelist = new List<object>();
                var list = res.data;
                foreach (var item1 in list) {
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
                    else {
                        List<object> userlist = new List<object>();
                        foreach (var auditor in item["auditors"]) {
                            switch (auditor["type"].ToString()) {//获取人员信息1.岗位2.角色3.用户
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
                                    if (userList != null) {
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
                return Fail("获取数据失败！");
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 创建流程实例
        /// </summary>
        /// <param name="isNew">是否是新发起的实例</param>
        /// <param name="processId">流程实例ID</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="processName">流程实例名称</param>
        /// <param name="processLevel">流程重要等级</param>
        /// <param name="description">备注说明</param>
        /// <param name="formData">表单数据</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult Create(bool isNew,string processId,string schemeCode,string processName,int processLevel,string description,string auditers, string formData) {

            WfParameter wfParameter = new WfParameter();
            UserInfo userInfo = LoginUserInfo.Get();

            wfParameter.companyId = userInfo.companyId;
            wfParameter.departmentId = userInfo.departmentId;
            wfParameter.userId = userInfo.userId;
            wfParameter.userName = userInfo.realName;

            wfParameter.isNew = isNew;
            wfParameter.processId = processId;
            wfParameter.schemeCode = schemeCode;
            wfParameter.processName = processName;
            wfParameter.processLevel = processLevel;
            wfParameter.description = description;
            wfParameter.auditers = auditers;
            wfParameter.formData = formData;

            WfResult res = wfEngineIBLL.Create(wfParameter);
            return Success(res);
        }

        /// <summary>
        /// 创建流程实例
        /// </summary>
        /// <param name="taskId">流程实例ID</param>
        /// <param name="verifyType">流程模板编码</param>
        /// <param name="description">流程实例名称</param>
        /// <param name="auditorId">加签人员Id</param>
        /// <param name="auditorName">备注说明</param>
        /// <param name="formData">表单数据</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult Audit(string taskId, string verifyType, string description, string auditorId, string auditorName,string auditers, string formData)
        {

            WfParameter wfParameter = new WfParameter();
            UserInfo userInfo = LoginUserInfo.Get();

            wfParameter.companyId = userInfo.companyId;
            wfParameter.departmentId = userInfo.departmentId;
            wfParameter.userId = userInfo.userId;
            wfParameter.userName = userInfo.realName;

            wfParameter.taskId = taskId;
            wfParameter.verifyType = verifyType;
            wfParameter.auditorId = auditorId;
            wfParameter.auditorName = auditorName;
            wfParameter.description = description;
            wfParameter.auditers = auditers;
            wfParameter.formData = formData;

            WfResult res = wfEngineIBLL.Audit(wfParameter);
            return Success(res);
        }
        #endregion

    }
}