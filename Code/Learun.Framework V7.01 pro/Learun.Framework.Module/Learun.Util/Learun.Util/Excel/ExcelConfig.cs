using System.Collections.Generic;
using System.Drawing;

namespace Learun.Util
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.04
    /// 描 述：Excel导入导出设置
    /// </summary>
    public class ExcelConfig
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 前景色
        /// </summary>
        public Color ForeColor { get; set; }
        /// <summary>
        /// 背景色
        /// </summary>
        public Color Background { get; set; }
        private short _titlepoint;
        /// <summary>
        /// 标题字号
        /// </summary>
        public short TitlePoint
        {
            get
            {
                if (_titlepoint == 0)
                {
                    return 20;
                }
                else
                {
                    return _titlepoint;
                }
            }
            set { _titlepoint = value; }
        }
        private short _headpoint;
        /// <summary>
        /// 列头字号
        /// </summary>
        public short HeadPoint
        {
            get
            {
                if (_headpoint == 0)
                {
                    return 10;
                }
                else
                {
                    return _headpoint;
                }
            }
            set { _headpoint = value; }
        }
        /// <summary>
        /// 标题高度
        /// </summary>
        public short TitleHeight { get; set; }
        /// <summary>
        /// 列标题高度
        /// </summary>
        public short HeadHeight { get; set; }
        private string _titlefont;
        /// <summary>
        /// 标题字体
        /// </summary>
        public string TitleFont
        {
            get
            {
                if (_titlefont == null)
                {
                    return "微软雅黑";
                }
                else
                {
                    return _titlefont;
                }
            }
            set { _titlefont = value; }
        }
        private string _headfont;
        /// <summary>
        /// 列头字体
        /// </summary>
        public string HeadFont
        {
            get
            {
                if (_headfont == null)
                {
                    return "微软雅黑";
                }
                else
                {
                    return _headfont;
                }
            }
            set { _headfont = value; }
        }
        /// <summary>
        /// 是否按内容长度来适应表格宽度
        /// </summary>
        public bool IsAllSizeColumn { get; set; }
        /// <summary>
        /// 列设置
        /// </summary>
        public List<ColumnModel> ColumnEntity { get; set; }

    }
}
