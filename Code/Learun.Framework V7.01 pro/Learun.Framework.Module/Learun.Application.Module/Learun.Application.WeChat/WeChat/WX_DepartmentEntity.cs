using System;
using System.ComponentModel.DataAnnotations.Schema;
using Learun.Util;

namespace Learun.Application.WeChat.WeChat
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-12-11 12:03
    /// 描 述：微信管理
    /// </summary>
    public class WX_DepartmentEntity
    {
        #region 实体成员
        /// <summary>
        /// F_Id
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 微信部门id
        /// </summary>
        /// <returns></returns>
        [Column("F_WXId")]
        public int F_WXId { get; set; }
        /// <summary>
        /// 微信父级Id
        /// </summary>
        /// <returns></returns>
        [Column("F_ParentId")]
        public int F_ParentId { get; set; }
        /// <summary>
        /// 系统部门Id
        /// </summary>
        /// <returns></returns>
        [Column("F_DepartmentId")]
        public string F_DepartmentId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        /// <returns></returns>
        [Column("F_Name")]
        public string F_Name { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        /// <returns></returns>
        [Column("F_Order")]
        public int? F_Order { get; set; }
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
        /// 创建人名字
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
            this.F_Id = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
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
        }
        /// <summary>
        /// 设置微信ID
        /// </summary>
        /// <param name="keyValue"></param>
        public void SetWXId()
        {
            Random rd = new Random();
            this.F_WXId = rd.Next(100, 200);
        }
        #endregion
    }
}
