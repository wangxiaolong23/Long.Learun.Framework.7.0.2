using System.Drawing;

namespace Learun.Util
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.04
    /// 描 述：Excel导入导出列设置模型
    /// </summary>
    public class ColumnModel
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string Column { get; set; }
        /// <summary>
        /// Excel列名
        /// </summary>
        public string ExcelColumn { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 前景色
        /// </summary>
        public Color ForeColor { get; set; }
        /// <summary>
        /// 背景色
        /// </summary>
        public Color Background { get; set; }
        /// <summary>
        /// 字体
        /// </summary>
        public string Font { get; set; }
        /// <summary>
        /// 字号
        /// </summary>
        public short Point { get; set; }
        /// <summary>
        ///对齐方式
        ///left 左
        ///center 中间
        ///right 右
        ///fill 填充
        ///justify 两端对齐
        ///centerselection 跨行居中
        ///distributed
        /// </summary>
        public string Alignment { get; set; }
    }
}
