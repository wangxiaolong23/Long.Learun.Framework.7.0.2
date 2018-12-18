using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.Base.SystemModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：系统日志数据库实体类
    /// </summary>
    public class LogEntity
    {
        #region 实体成员
        /// <summary>
        /// 日志主键
        /// </summary>
        /// <returns></returns>
        [Column("F_LOGID")]
        public string F_LogId { get; set; }
        /// <summary>
        /// 分类Id 1-登陆2-访问3-操作4-异常
        /// </summary>
        /// <returns></returns>
        [Column("F_CATEGORYID")]
        public int? F_CategoryId { get; set; }
        /// <summary>
        /// 来源对象主键
        /// </summary>
        /// <returns></returns>
        [Column("F_SOURCEOBJECTID")]
        public string F_SourceObjectId { get; set; }
        /// <summary>
        /// 来源日志内容
        /// </summary>
        /// <returns></returns>
        [Column("F_SOURCECONTENTJSON")]
        public string F_SourceContentJson { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        /// <returns></returns>
        [Column("F_OPERATETIME")]
        public DateTime? F_OperateTime { get; set; }
        /// <summary>
        /// 操作用户Id
        /// </summary>
        /// <returns></returns>
        [Column("F_OPERATEUSERID")]
        public string F_OperateUserId { get; set; }
        /// <summary>
        /// 操作用户
        /// </summary>
        /// <returns></returns>
        [Column("F_OPERATEACCOUNT")]
        public string F_OperateAccount { get; set; }
        /// <summary>
        /// 操作类型Id
        /// </summary>
        /// <returns></returns>
        [Column("F_OPERATETYPEID")]
        public string F_OperateTypeId { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        /// <returns></returns>
        [Column("F_OPERATETYPE")]
        public string F_OperateType { get; set; }
        /// <summary>
        /// 系统功能
        /// </summary>
        /// <returns></returns>
        [Column("F_MODULE")]
        public string F_Module { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        /// <returns></returns>
        [Column("F_IPADDRESS")]
        public string F_IPAddress { get; set; }
        /// <summary>
        /// IP地址所在城市
        /// </summary>
        /// <returns></returns>
        [Column("F_IPADDRESSNAME")]
        public string F_IPAddressName { get; set; }
        /// <summary>
        /// 主机
        /// </summary>
        /// <returns></returns>
        [Column("F_HOST")]
        public string F_Host { get; set; }
        /// <summary>
        /// 浏览器
        /// </summary>
        /// <returns></returns>
        [Column("F_BROWSER")]
        public string F_Browser { get; set; }
        /// <summary>
        /// 执行结果状态
        /// </summary>
        /// <returns></returns>
        [Column("F_EXECUTERESULT")]
        public int? F_ExecuteResult { get; set; }
        /// <summary>
        /// 执行结果信息
        /// </summary>
        /// <returns></returns>
        [Column("F_EXECUTERESULTJSON")]
        public string F_ExecuteResultJson { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }
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
        #endregion
    }
}
