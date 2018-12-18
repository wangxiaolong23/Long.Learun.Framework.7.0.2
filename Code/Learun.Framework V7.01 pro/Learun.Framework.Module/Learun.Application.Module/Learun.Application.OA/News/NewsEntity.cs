using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.OA
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：新闻公告
    /// </summary>
    public class NewsEntity
    {
        #region 实体成员
        /// <summary>
        /// 新闻主键
        /// </summary>
        /// <returns></returns>
        [Column("F_NEWSID")]
        public string F_NewsId { get; set; }
        /// <summary>
        /// 类型（1-新闻2-公告）
        /// </summary>
        /// <returns></returns>
        [Column("F_TYPEID")]
        public int? F_TypeId { get; set; }
        /// <summary>
        /// 所属类别主键
        /// </summary>
        /// <returns></returns>
        [Column("F_CATEGORYID")]
        public string F_CategoryId { get; set; }
        /// <summary>
        /// 所属类别
        /// </summary>
        /// <returns></returns>
        [Column("F_CATEGORY")]
        public string F_Category { get; set; }
        /// <summary>
        /// 完整标题
        /// </summary>
        /// <returns></returns>
        [Column("F_FULLHEAD")]
        public string F_FullHead { get; set; }
        /// <summary>
        /// 标题颜色
        /// </summary>
        /// <returns></returns>
        [Column("F_FULLHEADCOLOR")]
        public string F_FullHeadColor { get; set; }
        /// <summary>
        /// 简略标题
        /// </summary>
        /// <returns></returns>
        [Column("F_BRIEFHEAD")]
        public string F_BriefHead { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        /// <returns></returns>
        [Column("F_AUTHORNAME")]
        public string F_AuthorName { get; set; }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        [Column("F_COMPILENAME")]
        public string F_CompileName { get; set; }
        /// <summary>
        /// Tag词
        /// </summary>
        /// <returns></returns>
        [Column("F_TAGWORD")]
        public string F_TagWord { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        /// <returns></returns>
        [Column("F_KEYWORD")]
        public string F_Keyword { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        /// <returns></returns>
        [Column("F_SOURCENAME")]
        public string F_SourceName { get; set; }
        /// <summary>
        /// 来源地址
        /// </summary>
        /// <returns></returns>
        [Column("F_SOURCEADDRESS")]
        public string F_SourceAddress { get; set; }
        /// <summary>
        /// 新闻内容
        /// </summary>
        /// <returns></returns>
        [Column("F_NEWSCONTENT")]
        public string F_NewsContent { get; set; }
        /// <summary>
        /// 浏览量
        /// </summary>
        /// <returns></returns>
        [Column("F_PV")]
        public int? F_PV { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        /// <returns></returns>
        [Column("F_RELEASETIME")]
        public DateTime? F_ReleaseTime { get; set; }
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
            this.F_NewsId = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
            this.F_ReleaseTime = DateTime.Now;
            this.F_DeleteMark = 0;
            this.F_EnabledMark = 1;
            this.F_PV = 0;

            UserInfo userInfo = LoginUserInfo.Get();
            this.F_CreateUserId = userInfo.userId;
            this.F_CreateUserName = userInfo.realName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void Modify(string keyValue)
        {
            this.F_NewsId = keyValue;
            this.F_ModifyDate = DateTime.Now;

            UserInfo userInfo = LoginUserInfo.Get();
            this.F_ModifyUserId = userInfo.userId;
            this.F_ModifyUserName = userInfo.realName;
        }
        #endregion
    }
}
