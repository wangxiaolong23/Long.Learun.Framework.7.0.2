
using System.Collections.Generic;
namespace Learun.Application.BaseModule.CodeGeneratorModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：列表设置
    /// </summary>
    public class ColModel
    {
        /// <summary>
        /// 是否分页
        /// </summary>
        public string isPage { get; set; }
        /// <summary>
        /// 字段设置
        /// </summary>
        public List<ColFieldModel> fields { get; set; }
        /// <summary>
        /// 按钮
        /// </summary>
        public string[] btns { get; set; }

        /// <summary>
        /// 扩展按钮
        /// </summary>
        public List<ColBtnModel> btnexs { get; set; }

        /// <summary>
        /// 是否显示树形数据
        /// </summary>
        public string isTree { get; set; }

        /// <summary>
        /// 树形数据源
        /// </summary>
        public string treeSource { get; set; }

        /// <summary>
        /// 树形数据源ID
        /// </summary>
        public string treeSourceId { get; set; }

        /// <summary>
        /// 树形数据sql
        /// </summary>
        public string treeSql { get; set; }

        /// <summary>
        /// 树形数据ID
        /// </summary>
        public string treefieldId { get; set; }

        /// <summary>
        /// 树形数据父级ID
        /// </summary>
        public string treeParentId { get; set; }

        /// <summary>
        /// 树形数据显示字段
        /// </summary>
        public string treefieldShow { get; set; }

        /// <summary>
        /// 树形数据关联字段
        /// </summary>
        public string treefieldRe { get; set; }
        /// <summary>
        /// 流程关联字段
        /// </summary>
        public string workField { get; set; }
    }
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：列表字段设置
    /// </summary>
    public class ColFieldModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string fieldName { get; set; }
        /// <summary>
        /// 字段ID
        /// </summary>
        public string field { get; set; }
        /// <summary>
        /// 对齐方式  left center right
        /// </summary>
        public string align { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public string width { get; set; }

    }


    public class ColBtnModel {
        public string id { get; set; }

        public string name { get; set; }
    }

    /// <summary>
    /// 报表列表
    /// </summary>
    public class ReportColModel {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 字段ID
        /// </summary>
        public string field { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public string width { get; set; }

    }

}
