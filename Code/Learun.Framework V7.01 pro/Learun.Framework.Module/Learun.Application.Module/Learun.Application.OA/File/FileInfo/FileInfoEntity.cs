using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.OA.File.FileInfo
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.06.20
    /// 描 述：文件管理
    /// </summary>
    public class FileInfoEntity
    {
        #region 实体成员
        /// <summary>
        /// 文件主键
        /// </summary>
        /// <returns></returns>
        [Column("F_FILEID")]
        public string F_FileId { get; set; }
        /// <summary>
        /// 文件夹主键
        /// </summary>
        /// <returns></returns>
        [Column("F_FOLDERID")]
        public string F_FolderId { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary>
        /// <returns></returns>
        [Column("F_FILENAME")]
        public string F_FileName { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        /// <returns></returns>
        [Column("F_FILEPATH")]
        public string F_FilePath { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        /// <returns></returns>
        [Column("F_FILESIZE")]
        public string F_FileSize { get; set; }
        /// <summary>
        /// 文件后缀
        /// </summary>
        /// <returns></returns>
        [Column("F_FILEEXTENSIONS")]
        public string F_FileExtensions { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        /// <returns></returns>
        [Column("F_FILETYPE")]
        public string F_FileType { get; set; }
        /// <summary>
        /// 共享
        /// </summary>
        /// <returns></returns>
        [Column("F_ISSHARE")]
        public int? F_IsShare { get; set; }
        /// <summary>
        /// 共享连接
        /// </summary>
        /// <returns></returns>
        [Column("F_SHARELINK")]
        public string F_ShareLink { get; set; }
        /// <summary>
        /// 共享提取码
        /// </summary>
        /// <returns></returns>
        [Column("F_SHARECODE")]
        public int? F_ShareCode { get; set; }
        /// <summary>
        /// 共享日期
        /// </summary>
        /// <returns></returns>
        [Column("F_SHARETIME")]
        public DateTime? F_ShareTime { get; set; }
        /// <summary>
        /// 下载次数
        /// </summary>
        /// <returns></returns>
        [Column("F_DOWNLOADCOUNT")]
        public int? F_DownloadCount { get; set; }
        /// <summary>
        /// 置顶
        /// </summary>
        /// <returns></returns>
        [Column("F_ISTOP")]
        public int? F_IsTop { get; set; }
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
            this.F_FileId = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
            UserInfo userInfo = LoginUserInfo.Get();
            this.F_CreateUserId = userInfo.userId;
            this.F_CreateUserName = userInfo.realName;
            this.F_ModifyDate = DateTime.Now;
            this.F_ModifyUserId = userInfo.userId;
            this.F_ModifyUserName = userInfo.realName;
            this.F_DeleteMark = 0;
            this.F_EnabledMark = 1;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.F_FileId = keyValue;
            this.F_ModifyDate = DateTime.Now;
            UserInfo userInfo = LoginUserInfo.Get();
            this.F_ModifyUserId = userInfo.userId;
            this.F_ModifyUserName = userInfo.realName;
        }
        #endregion
    }
}

