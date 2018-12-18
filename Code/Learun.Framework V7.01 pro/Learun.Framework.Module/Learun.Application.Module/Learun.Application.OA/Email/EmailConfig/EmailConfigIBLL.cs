using Learun.Util;
using System.Collections.Generic;

namespace Learun.Application.OA.Email.EmailConfig
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.06.04
    /// 描 述：邮件配置管理
    /// </summary>
    public interface EmailConfigIBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="queryJson">关键词</param>
        /// <returns></returns>
        IEnumerable<EmailConfigEntity> GetConfigList(string queryJson);

        /// <summary>
        /// 获取邮件配置实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        EmailConfigEntity GetConfigEntity(string keyValue);

        /// <summary>
        /// 获取当前有效邮件配置实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        EmailConfigEntity GetCurrentConfig();

        #endregion

        #region 提交数据
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        void DeleteEntity(string keyValue);
        /// <summary>
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="configEntity">邮件配置实体</param>
        /// <returns></returns>
        void SaveConfigEntity(string keyValue, EmailConfigEntity configEntity);
        #endregion
    }
}