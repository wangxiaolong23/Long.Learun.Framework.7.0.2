using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learun.Application.OA.File.FileFolder
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.06.20
    /// 描 述：文件管理
    /// </summary>
    public interface FileFolderIBLL
    {
        # region 获取数据
        /// <summary>
        /// 文件夹列表
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        IEnumerable<FileFolderEntity> GetList(string userId);

        /// <summary>
        /// 文件夹实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        FileFolderEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 还原文件夹
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RestoreFile(string keyValue);
        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 彻底删除文件夹
        /// </summary>
        /// <param name="keyValue">主键</param>
        void ThoroughRemoveForm(string keyValue);
        /// <summary>
        /// 清空回收站
        /// </summary>
        void EmptyRecycledForm();
        /// <summary>
        /// 保存文件夹表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="fileFolderEntity">文件夹实体</param>
        /// <returns></returns>
        void SaveForm(string keyValue, FileFolderEntity fileFolderEntity);
        /// <summary>
        /// 共享文件夹
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="IsShare">是否共享：1-共享 0取消共享</param>
        void ShareFolder(string keyValue, int IsShare = 1);
        #endregion
    }
}