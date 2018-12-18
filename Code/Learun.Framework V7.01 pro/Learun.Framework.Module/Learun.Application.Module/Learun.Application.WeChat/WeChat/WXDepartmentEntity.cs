namespace Learun.Application.WeChat.WeChat
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-09-22 12:01
    /// 描 述：部门实体类
    /// </summary>
    public class WXDepartmentEntity
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 父部门id
        /// </summary>
        public int parentid { get; set; }
        /// <summary>
        /// 在父部门中的次序值
        /// </summary>
        public int? order { get; set; }
        /// <summary>
        /// 部门id
        /// </summary>
        public int id { get; set; }
    }
}
