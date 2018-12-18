using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.Base.AuthorizeModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：授權功能
    /// </summary>
    public class AuthorizeEntity
    {
        #region 实体成员
        /// <summary>
        /// 授权功能主键
        /// </summary>
        /// <returns></returns>
        [Column("F_AUTHORIZEID")]
        public string F_AuthorizeId { get; set; }
        /// <summary>
        /// 对象分类:1-角色2-用户
        /// </summary>
        /// <returns></returns>
        [Column("F_OBJECTTYPE")]
        public int? F_ObjectType { get; set; }
        /// <summary>
        /// 对象主键
        /// </summary>
        /// <returns></returns>
        [Column("F_OBJECTID")]
        public string F_ObjectId { get; set; }
        /// <summary>
        /// 项目类型:1-菜单2-按钮3-视图
        /// </summary>
        /// <returns></returns>
        [Column("F_ITEMTYPE")]
        public int? F_ItemType { get; set; }
        /// <summary>
        /// 项目主键
        /// </summary>
        /// <returns></returns>
        [Column("F_ITEMID")]
        public string F_ItemId { get; set; }
        /// <summary>
        /// 创建时间
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
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            UserInfo userInfo = LoginUserInfo.Get();
            this.F_CreateUserId = userInfo.userId;
            this.F_CreateUserName = userInfo.realName;

            this.F_AuthorizeId = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;

        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void Modify(string keyValue)
        {
            this.F_AuthorizeId = keyValue;
        }
        #endregion
    }
}
