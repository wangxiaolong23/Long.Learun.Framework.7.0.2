using System.Collections.Generic;

namespace Learun.Application.Form
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.01
    /// 描 述：表单模板模型
    /// </summary>
    public class FormSchemeModel
    {
        /// <summary>
        /// 表单模板-选项卡数据
        /// </summary>
        public List<FormTabModel> data { get; set; }
        /// <summary>
        /// 表单模板-绑定数据库主键
        /// </summary>
        public string dbId { get; set; }
        /// <summary>
        /// 表单模板-绑定数据库表信息
        /// </summary>
        public List<FormTableModel> dbTable { get; set; }

        /// <summary>
        /// 主表表面
        /// </summary>
        public string mainTableName { get; set; }
        /// <summary>
        /// 主表主键
        /// </summary>
        public string mainTablePkey { get; set; }
        /// <summary>
        /// 主表主键数据
        /// </summary>
        public string mainTablePkeyValue { get; set; }
    }
    /// <summary>
    /// 表单模板模型-选项卡数据模型
    /// </summary>
    public class FormTabModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public string sort { get; set; }
        /// <summary>
        /// 选项卡名称
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 选项卡主键列表
        /// </summary>
        public List<FormCompontModel> componts { get; set; }
    }
    /// <summary>
    /// 表单模板模型-组件数据模型
    /// </summary>
    public class FormCompontModel
    {
        /// <summary>
        /// 组件的主键Id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 显示标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 组件类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 绑定数据表
        /// </summary>
        public string table { get; set; }
        /// <summary>
        /// 绑定的字段
        /// </summary>
        public string field { get; set; }
        /// <summary>
        /// 编码规则
        /// </summary>
        public string rulecode { get; set; }
        /// <summary>
        /// 数据值
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 表格设置信息
        /// </summary>
        public List<GridFieldModel> fieldsData { get; set; }
             
    }

    public class FormTableModel
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 字段名
        /// </summary>
        public string field { get; set; }
        /// <summary>
        /// 关联的表名
        /// </summary>
        public string relationName { get; set; }
        /// <summary>
        /// 关联表字段
        /// </summary>
        public string relationField { get; set; }
    }

    public class GridFieldModel {
        /// <summary>
        /// 绑定字段
        /// </summary>
        public string field { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        public string type { get; set; }
    }
}
