using System.Collections.Generic;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：工作流节点
    /// </summary>
    public class WfNodeInfo
    {
        /// <summary>
        /// 节点Id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 节点类型-》开始startround;结束endround;一般stepnode;会签节点:confluencenode;条件判断节点：conditionnode;查阅节点：auditornode;子流程节点：childwfnode
        /// </summary>
        public string type { get; set; }

        /*节点设置信息*/
        /// <summary>
        /// 超时时间（超时后可流转下一节点）默认24小时
        /// </summary>
        public int timeoutAction { get; set; }
        /// <summary>
        /// 超时时间（发出通知）默认24小时
        /// </summary>
        public int timeoutNotice { get; set; }

        /// <summary>
        /// 会签策略1-所有步骤通过，2-一个步骤通过即可，3-按百分比计算
        /// </summary>
        public int confluenceType { get; set; }
        /// <summary>
        /// 会签比例
        /// </summary>
        public string confluenceRate { get; set; }

        /// <summary>
        /// 审核者们
        /// </summary>
        public List<WfAuditor> auditors { get; set; }

        /// <summary>
        /// 字段权限数据
        /// </summary>
        public List<WfAuthorizeField> authorizeFields { get; set; }

        /// <summary>
        /// 节点绑定的表单
        /// </summary>
        public List<WfForm> wfForms { get; set; }

        /// <summary>
        /// 绑定的方法（通过ioc注册实现）
        /// </summary>
        public string iocName { get; set; }
        /// <summary>
        /// 成功后需要执行的sql语句对应的数据库主键
        /// </summary>
        public string dbSuccessId { get; set; }
        /// <summary>
        /// 成功后需要执行的sql语句
        /// </summary>
        public string dbSuccessSql { get; set; }
        /// <summary>
        /// 驳回后需要执行的sql语句对应的数据库主键
        /// </summary>
        public string dbFailId { get; set; }
        /// <summary>
        /// 驳回后需要执行的sql语句
        /// </summary>
        public string dbFailSql { get; set; }

        #region 会签
        /// <summary>
        /// 绑定的方法（通过ioc注册实现）会签
        /// </summary>
        public string cfIocName { get; set; }
        /// <summary>
        /// 会签后需要执行的sql语句对应的数据库主键
        /// </summary>
        public string cfDbId { get; set; }
        /// <summary>
        /// 会签后需要执行的sql语句
        /// </summary>
        public string cfDbSql { get; set; }
        /// <summary>
        /// 会签结果
        /// </summary>
        public bool cfres { get; set; }
        #endregion


        #region 条件判断
        /// <summary>
        /// 工作流条件节点-条件字段（优先执行）
        /// </summary>
        public List<WfCondition> conditions { get; set; }
        /// <summary>
        /// 条件判断sql语句所在数据库主键
        /// </summary>
        public string dbConditionId { get; set; }
        /// <summary>
        /// 条件判断sql语句
        /// </summary>
        public string conditionSql { get; set; }
        #endregion
    }
}
