using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.Excel
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.01
    /// 描 述：Excel数据导入设置字段
    /// </summary>
    public class ExcelImportFieldEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 导入模板Id
        /// </summary>
        /// <returns></returns>
        [Column("F_IMPORTID")]
        public string F_ImportId { get; set; }
        /// <summary>
        /// 字典名字
        /// </summary>
        /// <returns></returns>
        [Column("F_NAME")]
        public string F_Name { get; set; }
        /// <summary>
        /// excel名字
        /// </summary>
        /// <returns></returns>
        [Column("F_COLNAME")]
        public string F_ColName { get; set; }
        /// <summary>
        /// 唯一性验证:0要,1需要
        /// </summary>
        /// <returns></returns>
        [Column("F_ONLYONE")]
        public int? F_OnlyOne { get; set; }
        /// <summary>
        /// 关联类型0:无关联,1:GUID,2:数据字典3:数据表;4:固定数值;5:操作人ID;6:操作人名字;7:操作时间;
        /// </summary>
        /// <returns></returns>
        [Column("F_RELATIONTYPE")]
        public int? F_RelationType { get; set; }
        /// <summary>
        /// 数据字典编号
        /// </summary>
        /// <returns></returns>
        [Column("F_DATAITEMCODE")]
        public string F_DataItemCode { get; set; }
        /// <summary>
        /// 固定数据
        /// </summary>
        /// <returns></returns>
        [Column("F_VALUE")]
        public string F_Value { get; set; }
        /// <summary>
        /// 关联库id
        /// </summary>
        /// <returns></returns>
        [Column("F_DATASOURCEID")]
        public string F_DataSourceId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        /// <returns></returns>
        [Column("F_SORTCODE")]
        public int? F_SortCode { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.F_Id = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.F_Id = keyValue;
        }
        #endregion
    }
}
