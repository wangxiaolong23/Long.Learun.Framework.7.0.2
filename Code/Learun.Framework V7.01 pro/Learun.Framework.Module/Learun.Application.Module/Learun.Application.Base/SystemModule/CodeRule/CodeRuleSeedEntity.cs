using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.Base.SystemModule
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 上海力软信息技术有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.12.21 16:19
    /// 描 述：编号规则种子
    /// </summary>
    public class CodeRuleSeedEntity
    {
        #region 实体成员
        /// <summary>
        /// 编号规则种子主键
        /// </summary>
        /// <returns></returns>
        [Column("F_RULESEEDID")]
        public string F_RuleSeedId { get; set; }
        /// <summary>
        /// 编码规则主键
        /// </summary>
        /// <returns></returns>
        [Column("F_RULEID")]
        public string F_RuleId { get; set; }
        /// <summary>
        /// 用户主键
        /// </summary>
        /// <returns></returns>
        [Column("F_USERID")]
        public string F_UserId { get; set; }
        /// <summary>
        /// 种子值
        /// </summary>
        /// <returns></returns>
        [Column("F_SEEDVALUE")]
        public int? F_SeedValue { get; set; }
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
        public void Create(UserInfo userInfo)
        {
            this.F_RuleSeedId = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
            this.F_ModifyDate = DateTime.Now;

            if (userInfo == null) {
                userInfo = LoginUserInfo.Get();
            }
            
            this.F_CreateUserId = userInfo.userId;
            this.F_CreateUserName = userInfo.realName;
        }

        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void Modify(string keyValue, UserInfo userInfo)
        {
            this.F_RuleSeedId = keyValue;
            this.F_ModifyDate = DateTime.Now;

            if (userInfo == null)
            {
                userInfo = LoginUserInfo.Get();
            }

            this.F_ModifyUserId = userInfo.userId;
            this.F_ModifyUserName = userInfo.realName;
        }
        #endregion
    }
}
