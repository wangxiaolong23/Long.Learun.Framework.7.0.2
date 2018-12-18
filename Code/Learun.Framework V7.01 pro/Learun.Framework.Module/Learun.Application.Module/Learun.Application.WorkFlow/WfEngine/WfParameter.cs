using System.Collections.Generic;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：工作流创建审核传递参数
    /// </summary>
    public class WfParameter
    {
        /// <summary>
        /// 流程实例主键
        /// </summary>
        public string processId { get; set; }
        /// <summary>
        /// 流程实例名称
        /// </summary>
        public string processName { get; set; }
        /// <summary>
        /// 重要等级0-普通，1-重要，2-紧急
        /// </summary>
        /// <returns></returns>
        public int processLevel { get; set; } 
        /// <summary>
        /// 是否是新的实例
        /// </summary>
        public bool isNew { get; set; }
        /// <summary>
        /// 流程模板编码（编码具有唯一性）
        /// </summary>
        public string schemeCode { get; set; }
        /// <summary>
        /// 审核类型（0.发起1.审核同意2.审核不同意3.加签4.加签-同意5.加签-不同意6.确认阅读7.保存草稿）
        /// </summary>
        public string verifyType { get; set; }
        /// <summary>
        /// 处理任务的主键
        /// </summary>
        public string taskId { get; set; }
        /// <summary>
        /// 当前处理用户主键
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 当前处理用户名字
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 当前处理用户角色信息
        /// </summary>
        public string roleIds { get; set; }
        /// <summary>
        /// 当前处理用户岗位信息
        /// </summary>
        public string postIds { get; set; }
        /// <summary>
        /// 当前处理用户所在公司主键
        /// </summary>
        public string companyId { get; set; }
        /// <summary>
        /// 当前处理用户所在部门主键
        /// </summary>
        public string departmentId { get; set; }
        /// <summary>
        /// 加签人员ID
        /// </summary>
        public string auditorId { get; set; }
        /// <summary>
        /// 加签人员名称
        /// </summary>
        public string auditorName { get; set; }
        /// <summary>
        /// 表单数据
        /// </summary>
        public string formData { get; set; }
        /// <summary>
        /// 处理意见
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 是否是获取审核人员
        /// </summary>
        public bool isGetAuditer { get; set; }

        /// <summary>
        /// 下一个节点的审核人员
        /// </summary>
        public string auditers { get; set; }

        /// <summary>
        /// 表单提交数据
        /// </summary>
        public string formreq { get; set; }
    }

    public class AuditerModel {
        public string userId { get; set; }

        public string userName { get; set; }
    }
}
