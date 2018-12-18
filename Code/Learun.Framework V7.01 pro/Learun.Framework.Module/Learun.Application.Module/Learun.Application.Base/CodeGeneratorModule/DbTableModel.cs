
namespace Learun.Application.BaseModule.CodeGeneratorModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：数据表配置信息
    /// </summary>
    public class DbTableModel
    {
        /// <summary>
        /// 数据表名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 数据表字段
        /// </summary>
        public string field { get; set; }
        /// <summary>
        /// 被关联表
        /// </summary>
        public string relationName { get; set; }
        /// <summary>
        /// 被关联表字段
        /// </summary>
        public string relationField { get; set; }
        /// <summary>
        /// 数据表主键
        /// </summary>
        public string pk { get; set; }
    }
}
