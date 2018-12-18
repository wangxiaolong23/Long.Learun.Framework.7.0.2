namespace Learun.Application.Base.SystemModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.04
    /// 描 述：数据库表模型
    /// </summary>
    public class DatabaseTableModel
    {
        /// <summary>
        /// 数据库表名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 数据库使用大小
        /// </summary>
        public string reserved { get; set; }
        /// <summary>
        /// 数据大小
        /// </summary>
        public string data { get; set; }
        /// <summary>
        /// 索引使用大小
        /// </summary>
        public string index_size { get; set; }
        /// <summary>
        /// 记录条数
        /// </summary>
        public int sumrows { get; set; }
        /// <summary>
        /// 未使用大小
        /// </summary>
        public string unused { get; set; }
        /// <summary>
        /// 表备注
        /// </summary>
        public string tdescription { get; set; }
        /// <summary>
        /// 主键
        /// </summary>
        public string pk { get; set; }
    }
}
