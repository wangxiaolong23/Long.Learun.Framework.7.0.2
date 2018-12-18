using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.AppMagager
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.03.16
    /// 描 述：移动端功能管理
    /// </summary>
    public class FunctionEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        /// <returns></returns>
        [Column("F_TYPE")]
        public string F_Type { get; set; }
        /// <summary>
        /// 自定义表单ID
        /// </summary>
        /// <returns></returns>
        [Column("F_FORMID")]
        public string F_FormId { get; set; }
        /// <summary>
        /// 代码ID
        /// </summary>
        /// <returns></returns>
        [Column("F_CODEID")]
        public string F_CodeId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYDATE")]
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// 修改人ID
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYUSERID")]
        public string F_ModifyUserId { get; set; }
        /// <summary>
        /// 修改人名字
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYUSERNAME")]
        public string F_ModifyUserName { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        /// <returns></returns>
        [Column("F_ICON")]
        public string F_Icon { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        [Column("F_NAME")]
        public string F_Name { get; set; }
        /// <summary>
        /// 模板ID
        /// </summary>
        [Column("F_SCHEMEID")]
        public string F_SchemeId { get; set; }
        /// <summary>
        ///  1 启用 0 停用
        /// </summary>
        [Column("F_ENABLEDMARK")]
        public int? F_EnabledMark { get; set; }
        /// <summary>
        ///  排序
        /// </summary>
        [Column("F_SORTCODE")]
        public int? F_SortCode { get; set; }


        /// <summary>
        /// 是否是代码开发的1是2不是（自定义表单功能）
        /// </summary>
        /// <returns></returns>
        [Column("F_ISSYSTEM")]
        public int? F_IsSystem { get; set; }

        /// <summary>
        /// 功能地址
        /// </summary>
        /// <returns></returns>
        [Column("F_URL")]
        public string F_Url { get; set; }

        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.F_Id = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
            this.F_EnabledMark = 1;
            UserInfo userInfo = LoginUserInfo.Get();
            this.F_CreateUserId = userInfo.userId;
            this.F_CreateUserName = userInfo.realName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.F_Id = keyValue;
            this.F_ModifyDate = DateTime.Now;
            UserInfo userInfo = LoginUserInfo.Get();
            this.F_ModifyUserId = userInfo.userId;
            this.F_ModifyUserName = userInfo.realName;
        }
        #endregion

        #region 扩展属性
        /// <summary>
        /// 模板数据
        /// </summary>
        [NotMapped]
        public string F_Scheme { get; set; }
        #endregion
    }
}