
using System.ComponentModel.DataAnnotations.Schema;
namespace Learun.Application.WorkFlow
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.08.08
    /// 描 述：工作流模板权限信息
    /// </summary>
    public class WfSchemeAuthorizeEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 流程模板信息主键
        /// </summary>
        /// <returns></returns>
        [Column("F_SCHEMEINFOID")]
        public string F_SchemeInfoId { get; set; }
        /// <summary>
        /// 对应对象名称
        /// </summary>
        /// <returns></returns>
        [Column("F_OBJECTNAME")]
        public string F_ObjectName { get; set; }
        /// <summary>
        /// 对应对象主键
        /// </summary>
        /// <returns></returns>
        [Column("F_OBJECTID")]
        public string F_ObjectId { get; set; }
        /// <summary>
        /// 对应对象类型
        /// </summary>
        /// <returns></returns>
        [Column("F_OBJECTTYPE")]
        public int? F_ObjectType { get; set; }
        #endregion
    }
}
