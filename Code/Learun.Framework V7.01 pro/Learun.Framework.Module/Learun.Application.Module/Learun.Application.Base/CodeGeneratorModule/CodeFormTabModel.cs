using System.Collections.Generic;

namespace Learun.Application.BaseModule.CodeGeneratorModule
{
    /// <summary>
    /// 表单模板模型-选项卡数据模型
    /// </summary>
    public class CodeFormTabModel
    {
        /// <summary>
        /// 选项卡名称
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 选项卡主键列表
        /// </summary>
        public List<CodeFormCompontModel> componts { get; set; }
    }
    /// <summary>
    /// 表单模板模型-组件数据模型
    /// </summary>
    public class CodeFormCompontModel
    {
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
        /// 所在行比例
        /// </summary>
        public string proportion { get; set; }
        /// <summary>
        /// 绑定数据表
        /// </summary>
        public string table { get; set; }
        /// <summary>
        /// 绑定的字段
        /// </summary>
        public string field { get; set; }
        /// <summary>
        /// 字段验证类型
        /// </summary>
        public string verify { get; set; }
        /// <summary>
        /// 是否隐藏
        /// </summary>
        public string isHide { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string dfvalue { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        public string height { get; set; }
        /// <summary>
        ///  0数据字典1数据源
        /// </summary>
        public string dataSource { get; set; }
        /// <summary>
        /// 数据源ID
        /// </summary>
        public string dataSourceId { get; set; }
        /// <summary>
        /// 数据字典编码
        /// </summary>
        public string itemCode { get; set; }
        /// <summary>
        /// 日期格式 0'yyyy-MM-dd' 1 'yyyy-MM-dd HH:mm'
        /// </summary>
        public string dateformat { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public string startTime { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string endTime { get; set; }
        /// <summary>
        /// 编码规则
        /// </summary>
        public string rulecode { get; set; }
        /// <summary>
        /// 单位组织数据类型
        /// </summary>
        public string dataType { get; set; }
        /// <summary>
        /// 单位组织关联字段
        /// </summary>
        public string relation { get; set; }

        /// <summary>
        /// 最小高度
        /// </summary>
        public string minheight { get; set; }
        /// <summary>
        /// 表格设置信息
        /// </summary>
        public List<CodeGridFieldModel> fieldsData { get; set; }
        /// <summary>
        /// 预加载数据库ID
        /// </summary>
        public string preloadDb { get; set; }
        /// <summary>
        /// 预加载数据库表
        /// </summary>
        public string preloadTable { get; set; }      
    }
    public class CodeGridFieldModel
    {
        /// <summary>
        /// id主键
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 父级id
        /// </summary>
        public string parentId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 绑定字段
        /// </summary>
        public string field { get; set; }
        /// <summary>
        /// 字段类型 label 文本； input 输入框； select 下拉框；radio 单选框；checkbox 多选框；datetime 日期；layer 弹层选择框；GUID ；
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public string width { get; set; }
        /// <summary>
        /// 对齐方式
        /// </summary>
        public string align { get; set; }
      
        /// <summary>
        /// 弹层宽度
        /// </summary>
        public string layerW { get; set; }
        /// <summary>
        /// 弹层高度
        /// </summary>
        public string layerH { get; set; }
        /// <summary>
        /// 弹层列表设置数据
        /// </summary>
        public List<CodeLayerDataModel> layerData { get; set; }
        /// <summary>
        /// 数据来源0 数据字典 1数据源
        /// </summary>
        public string dataSource { get; set; }
        /// <summary>
        /// 数据字典编码
        /// </summary>
        public string itemCode { get; set; }
        /// <summary>
        /// 数据源主键
        /// </summary>
        public string dataSourceId { get; set; }
        /// <summary>
        /// 显示字段
        /// </summary>
        public string showField { get; set; }
        /// <summary>
        /// 保存字段
        /// </summary>
        public string saveField { get; set; }
        /// <summary>
        /// 默认值
        /// </summary>
        public string dfvalue { get; set; }
        /// <summary>
        /// 日期格式 datetime 日期和时间 date 仅日期
        /// </summary>
        public string datetime { get; set; }
    }
    /// <summary>
    /// 弹层表格设置
    /// </summary>
    public class CodeLayerDataModel {
        /// <summary>
        /// 列表名
        /// </summary>
        public string label { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 对应赋值字段
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        public string width { get; set; }

        /// <summary>
        /// 对齐方式
        /// </summary>
        public string align { get; set; }

        /// <summary>
        /// 是否隐藏
        /// </summary>
        public string hide { get; set; }
    }
}
