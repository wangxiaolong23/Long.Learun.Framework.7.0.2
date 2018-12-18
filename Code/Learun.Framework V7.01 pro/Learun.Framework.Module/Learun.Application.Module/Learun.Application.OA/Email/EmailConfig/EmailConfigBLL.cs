using Learun.Util;
using System;
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
    public class EmailConfigBLL : EmailConfigIBLL
    {
        private EmailConfigService emailConfigService = new EmailConfigService();

        #region 获取数据
        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="queryJson">关键词</param>
        /// <returns></returns>
        public IEnumerable<EmailConfigEntity> GetConfigList(string queryJson)
        {
            try
            {
                return emailConfigService.GetConfigList(queryJson);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        /// <summary>
        /// 获取邮件配置实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public EmailConfigEntity GetConfigEntity(string keyValue)
        {
            try
            {
                return emailConfigService.GetConfigEntity(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        /// <summary>
        /// 获取当前有效邮件配置实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>

        public EmailConfigEntity GetCurrentConfig()
        {
            try
            {
                return emailConfigService.GetCurrentConfig();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                emailConfigService.DeleteEntity(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="configEntity">邮件配置实体</param>
        /// <returns></returns>
        public void SaveConfigEntity(string keyValue, EmailConfigEntity configEntity)
        {
            try
            {
                emailConfigService.SaveConfigEntity(keyValue, configEntity);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        #endregion
    }
}