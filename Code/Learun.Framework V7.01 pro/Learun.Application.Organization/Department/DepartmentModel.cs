namespace Learun.Application.Organization
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.03.27
    /// 描 述：部门数据模型
    /// </summary>
    public class DepartmentModel
    {
        /// <summary>
        /// 部门上级id
        /// </summary>
        public string parentId { get; set; }
        /// <summary>
        /// 公司主键
        /// </summary>
        public string companyId { get; set; }
        /// <summary>
        /// 公司名字
        /// </summary>
        public string name { get; set; }
    }
}
