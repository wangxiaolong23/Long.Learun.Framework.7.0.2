using System.Collections.Generic;

namespace Learun.Util
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.07
    /// 描 述：数据库查询拼接数据模型
    /// </summary>
    public class DbWhere
    {
        /// <summary>
        /// sql语句
        /// </summary>
        public string sql { get; set; }
        /// <summary>
        /// 查询参数
        /// </summary>
        public List<FieldValueParam> dbParameters { get; set; }
    }
}
