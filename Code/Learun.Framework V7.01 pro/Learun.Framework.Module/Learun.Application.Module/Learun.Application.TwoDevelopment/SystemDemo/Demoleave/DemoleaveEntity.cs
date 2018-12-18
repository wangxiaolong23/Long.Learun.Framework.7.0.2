using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.SystemDemo
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：系统表单-请假单
    /// </summary>
    public class DemoleaveEntity
    {
        #region 实体成员
        /// <summary>
        /// F_Id
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// F_Type
        /// </summary>
        /// <returns></returns>
        [Column("F_TYPE")]
        public int? F_Type { get; set; }
        /// <summary>
        /// F_Num
        /// </summary>
        /// <returns></returns>
        [Column("F_NUM")]
        public int? F_Num { get; set; }
        /// <summary>
        /// F_Reason
        /// </summary>
        /// <returns></returns>
        [Column("F_REASON")]
        public string F_Reason { get; set; }
        /// <summary>
        /// F_Begin
        /// </summary>
        /// <returns></returns>
        [Column("F_BEGIN")]
        public DateTime? F_Begin { get; set; }
        /// <summary>
        /// F_End
        /// </summary>
        /// <returns></returns>
        [Column("F_END")]
        public DateTime? F_End { get; set; }
        /// <summary>
        /// F_FileId
        /// </summary>
        /// <returns></returns>
        [Column("F_FILEID")]
        public string F_FileId { get; set; }
        /// <summary>
        /// F_CreateDate
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// F_CreateUserId
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.F_CreateDate = DateTime.Now;
            UserInfo userInfo = LoginUserInfo.Get();
            this.F_CreateUserId = userInfo.userId;
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
