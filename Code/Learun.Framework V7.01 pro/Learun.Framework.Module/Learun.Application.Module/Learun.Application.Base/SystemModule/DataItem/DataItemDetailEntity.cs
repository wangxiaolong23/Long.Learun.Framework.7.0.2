using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.Base.SystemModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：数据字典明细
    /// </summary>
    public class DataItemDetailEntity
    {
        #region 实体成员
        /// <summary>
        /// 明细主键
        /// </summary>
        /// <returns></returns>
        [Column("F_ITEMDETAILID")]
        public string F_ItemDetailId { get; set; }
        /// <summary>
        /// 分类主键
        /// </summary>
        /// <returns></returns>
        [Column("F_ITEMID")]
        public string F_ItemId { get; set; }
        /// <summary>
        /// 父级主键
        /// </summary>
        /// <returns></returns>
        [Column("F_PARENTID")]
        public string F_ParentId { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        /// <returns></returns>
        [Column("F_ITEMCODE")]
        public string F_ItemCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        /// <returns></returns>
        [Column("F_ITEMNAME")]
        public string F_ItemName { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        /// <returns></returns>
        [Column("F_ITEMVALUE")]
        public string F_ItemValue { get; set; }
        /// <summary>
        /// 快速查询
        /// </summary>
        /// <returns></returns>
        [Column("F_QUICKQUERY")]
        public string F_QuickQuery { get; set; }
        /// <summary>
        /// 简拼
        /// </summary>
        /// <returns></returns>
        [Column("F_SIMPLESPELLING")]
        public string F_SimpleSpelling { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>
        /// <returns></returns>
        [Column("F_ISDEFAULT")]
        public int? F_IsDefault { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        [Column("F_SORTCODE")]
        public int? F_SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        [Column("F_DELETEMARK")]
        public int? F_DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        /// <returns></returns>
        [Column("F_ENABLEDMARK")]
        public int? F_EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYDATE")]
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYUSERID")]
        public string F_ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYUSERNAME")]
        public string F_ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.F_ItemDetailId = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
            this.F_DeleteMark = 0;
            this.F_QuickQuery = Str.ConvertPinYin(this.F_ItemName).ToUpper();
            this.F_SimpleSpelling = Str.PinYin(this.F_ItemName);
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.F_ItemDetailId = keyValue;
            this.F_ModifyDate = DateTime.Now;
            this.F_QuickQuery = Str.ConvertPinYin(this.F_ItemName).ToUpper();
            this.F_SimpleSpelling = Str.PinYin(this.F_ItemName);
        }
        #endregion
    }
}
