using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：工作流模板信息
    /// </summary>
    public class WfSchemeInfoEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 流程模板编号
        /// </summary>
        /// <returns></returns>
        [Column("F_CODE")]
        public string F_Code { get; set; }
        /// <summary>
        /// 流程模板名称
        /// </summary>
        /// <returns></returns>
        [Column("F_NAME")]
        public string F_Name { get; set; }
        /// <summary>
        /// 流程模板分类
        /// </summary>
        /// <returns></returns>
        [Column("F_CATEGORY")]
        public string F_Category { get; set; }
        /// <summary>
        /// 流程类型1自定义流程0系统流程
        /// </summary>
        [Column("F_KIND")]
        public int? F_Kind { get; set; }
        /// <summary>
        /// 是否在移動端顯示
        /// </summary>
        [Column("F_ISAPP")]
        public int? F_IsApp { get; set; }
        /// <summary>
        /// 流程模板主键
        /// </summary>
        /// <returns></returns>
        [Column("F_SCHEMEID")]
        public string F_SchemeId { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        /// <returns></returns>
        [Column("F_DELETEMARK")]
        public int? F_DeleteMark { get; set; }
        /// <summary>
        /// 有效
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
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {

            this.F_DeleteMark = 0;
            this.F_EnabledMark = 1;

            this.F_Id = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void Modify(string keyValue)
        {
            this.F_Id = keyValue;
        }
        #endregion

        #region 扩展字段
        /// <summary>
        /// 1.正式（已发布）2.草稿
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public int? F_Type { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public string F_CreateUserName { get; set; }
        #endregion
    }
}
