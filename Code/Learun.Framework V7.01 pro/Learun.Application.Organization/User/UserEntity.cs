using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.Organization
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.04
    /// 描 述：用户数据库实体类
    /// </summary>
    public class UserEntity
    {
        #region 实体成员
        /// <summary>
        /// 用户主键
        /// </summary>		
        [Column("F_USERID")]
        public string F_UserId { get; set; }
        /// <summary>
        /// 工号
        /// </summary>	
        [Column("F_ENCODE")]
        public string F_EnCode { get; set; }
        /// <summary>
        /// 账户
        /// </summary>	
        [Column("F_ACCOUNT")]
        public string F_Account { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>		
        [Column("F_PASSWORD")]
        public string F_Password { get; set; }
        /// <summary>
        /// 密码秘钥
        /// </summary>	
        [Column("F_SECRETKEY")]
        public string F_Secretkey { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [Column("F_REALNAME")]
        public string F_RealName { get; set; }
        /// <summary>
        /// 呢称
        /// </summary>	
        [Column("F_NICKNAME")]
        public string F_NickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>	
        [Column("F_HEADICON")]
        public string F_HeadIcon { get; set; }
        /// <summary>
        /// 快速查询
        /// </summary>	
        [Column("F_QUICKQUERY")]
        public string F_QuickQuery { get; set; }
        /// <summary>
        /// 简拼
        /// </summary>	
        [Column("F_SIMPLESPELLING")]
        public string F_SimpleSpelling { get; set; }
        /// <summary>
        /// 性别
        /// </summary>	
        [Column("F_GENDER")]
        public int? F_Gender { get; set; }
        /// <summary>
        /// 生日
        /// </summary>	
        [Column("F_BIRTHDAY")]
        public DateTime? F_Birthday { get; set; }
        /// <summary>
        /// 手机
        /// </summary>	
        [Column("F_MOBILE")]
        public string F_Mobile { get; set; }
        /// <summary>
        /// 电话
        /// </summary>		
        [Column("F_TELEPHONE")]
        public string F_Telephone { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>	
        [Column("F_EMAIL")]
        public string F_Email { get; set; }
        /// <summary>
        /// QQ号
        /// </summary>		
        [Column("F_OICQ")]
        public string F_OICQ { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>	
        [Column("F_WECHAT")]
        public string F_WeChat { get; set; }
        /// <summary>
        /// MSN
        /// </summary>		
        [Column("F_MSN")]
        public string F_MSN { get; set; }
        /// <summary>
        /// 公司主键
        /// </summary>		
        [Column("F_COMPANYID")]
        public string F_CompanyId { get; set; }
        /// <summary>
        /// 部门主键
        /// </summary>		
        [Column("F_DEPARTMENTID")]
        public string F_DepartmentId { get; set; }
        /// <summary>
        /// 安全级别
        /// </summary>	
        [Column("F_SECURITYLEVEL")]
        public int? F_SecurityLevel { get; set; }
        /// <summary>
        /// 单点登录标识
        /// </summary>		
        [Column("F_OPENID")]
        public int? F_OpenId { get; set; }
        /// <summary>
        /// 密码提示问题
        /// </summary>		
        [Column("F_QUESTION")]
        public string F_Question { get; set; }
        /// <summary>
        /// 密码提示答案
        /// </summary>	
        [Column("F_ANSWERQUESTION")]
        public string F_AnswerQuestion { get; set; }
        /// <summary>
        /// 允许多用户同时登录
        /// </summary>		
        [Column("F_CHECKONLINE")]
        public int? F_CheckOnLine { get; set; }
        /// <summary>
        /// 允许登录时间开始
        /// </summary>		
        [Column("F_ALLOWSTARTTIME")]
        public DateTime? F_AllowStartTime { get; set; }
        /// <summary>
        /// 允许登录时间结束
        /// </summary>		
        [Column("F_ALLOWENDTIME")]
        public DateTime? F_AllowEndTime { get; set; }
        /// <summary>
        /// 暂停用户开始日期
        /// </summary>		
        [Column("F_LOCKSTARTDATE")]
        public DateTime? F_LockStartDate { get; set; }
        /// <summary>
        /// 暂停用户结束日期
        /// </summary>		
        [Column("F_LOCKENDDATE")]
        public DateTime? F_LockEndDate { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>	
        [Column("F_SORTCODE")]
        public int? F_SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>	
        [Column("F_DELETEMARK")]
        public int? F_DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>	
        [Column("F_ENABLEDMARK")]
        public int? F_EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>		
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>		
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>		
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>	
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>	
        [Column("F_MODIFYDATE")]
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>		
        [Column("F_MODIFYUSERID")]
        public string F_ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>		
        [Column("F_MODIFYUSERNAME")]
        public string F_ModifyUserName { get; set; }
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

            this.F_UserId = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
            this.F_DeleteMark = 0;
            this.F_EnabledMark = 1;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void Modify(string keyValue)
        {
            UserInfo userInfo = LoginUserInfo.Get();
            this.F_ModifyUserId = userInfo.userId;
            this.F_ModifyUserName = userInfo.realName;

            this.F_UserId = keyValue;
            this.F_ModifyDate = DateTime.Now;
        }
        #endregion

        #region 扩展属性
        /// <summary>
        /// 登录信息
        /// </summary>
        [NotMapped]
        public string LoginMsg { get; set; }
        /// <summary>
        /// 登录状态
        /// </summary>
        [NotMapped]
        public bool LoginOk { get; set; }
        #endregion
    }
}
