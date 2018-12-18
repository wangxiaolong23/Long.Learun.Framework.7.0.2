using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.OA.Schedule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.07.11
    /// 描 述：日程管理
    /// </summary>
    public class ScheduleEntity
    {
        #region 实体成员
        /// <summary>
        /// 日程主键
        /// </summary>
        /// <returns></returns>
        [Column("F_SCHEDULEID")]
        public string F_ScheduleId { get; set; }
        /// <summary>
        /// 日程名称
        /// </summary>
        /// <returns></returns>
        [Column("F_SCHEDULENAME")]
        public string F_ScheduleName { get; set; }
        /// <summary>
        /// 日程内容
        /// </summary>
        /// <returns></returns>
        [Column("F_SCHEDULECONTENT")]
        public string F_ScheduleContent { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        /// <returns></returns>
        [Column("F_CATEGORY")]
        public string F_Category { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        /// <returns></returns>
        [Column("F_STARTDATE")]
        public DateTime? F_StartDate { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        /// <returns></returns>
        [Column("F_STARTTIME")]
        public string F_StartTime { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        /// <returns></returns>
        [Column("F_ENDDATE")]
        public DateTime? F_EndDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        /// <returns></returns>
        [Column("F_ENDTIME")]
        public string F_EndTime { get; set; }
        /// <summary>
        /// 提前提醒
        /// </summary>
        /// <returns></returns>
        [Column("F_EARLY")]
        public int? F_Early { get; set; }
        /// <summary>
        /// 邮件提醒
        /// </summary>
        /// <returns></returns>
        [Column("F_ISMAILALERT")]
        public int? F_IsMailAlert { get; set; }
        /// <summary>
        /// 手机提醒
        /// </summary>
        /// <returns></returns>
        [Column("F_ISMOBILEALERT")]
        public int? F_IsMobileAlert { get; set; }
        /// <summary>
        /// 微信提醒
        /// </summary>
        /// <returns></returns>
        [Column("F_ISWECHATALERT")]
        public int? F_IsWeChatAlert { get; set; }
        /// <summary>
        /// 日程状态
        /// </summary>
        /// <returns></returns>
        [Column("F_SCHEDULESTATE")]
        public int? F_ScheduleState { get; set; }
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
            UserInfo userInfo = LoginUserInfo.Get();
            this.F_ScheduleId = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
            this.F_CreateUserId = userInfo.userId;
            this.F_CreateUserName = userInfo.realName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            UserInfo userInfo = LoginUserInfo.Get();
            this.F_ScheduleId = keyValue;
            this.F_ModifyDate = DateTime.Now;
            this.F_ModifyUserId = userInfo.userId;
            this.F_ModifyUserName = userInfo.realName;
        }
        #endregion
    }
}
