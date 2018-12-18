using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Learun.Application.Base.AuthorizeModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：力软框架开发组
    /// 日 期：2017-06-21 16:30
    /// 描 述：数据权限
    /// </summary>
    public class DataAuthorizeConditionEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 数据权限对应关系主键
        /// </summary>
        /// <returns></returns>
        [Column("F_DATAAUTHORIZERELATIONID")]
        public string F_DataAuthorizeRelationId { get; set; }
        /// <summary>
        /// 字段ID
        /// </summary>
        /// <returns></returns>
        [Column("F_FIELDID")]
        public string F_FieldId { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        /// <returns></returns>
        [Column("F_FIELDNAME")]
        public string F_FieldName { get; set; }
        /// <summary>
        /// 字段类型
        /// </summary>
        [Column("F_FIELDTYPE")]
        public int F_FieldType { get; set; }
        /// <summary>
        /// 比较符1.等于2.包含3.包含于4.不等于5.不包含6.不包含于
        /// </summary>
        /// <returns></returns>
        [Column("F_SYMBOL")]
        public int? F_Symbol { get; set; }
        /// <summary>
        /// 比较符名称
        /// </summary>
        /// <returns></returns>
        [Column("F_SYMBOLNAME")]
        public string F_SymbolName { get; set; }
        /// <summary>
        /// 字段值类型1.文本2.登录者ID3.登录者账号4.登录者公司5.登录者部门6.登录者岗位7.登录者角色
        /// </summary>
        [Column("F_FILEDVALUETYPE")]
        public int? F_FiledValueType { get; set; }
        /// <summary>
        /// 字段值
        /// </summary>
        /// <returns></returns>
        [Column("F_FILEDVALUE")]
        public string F_FiledValue { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        [Column("F_SORT")]
        public int F_Sort { get; set; }
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

