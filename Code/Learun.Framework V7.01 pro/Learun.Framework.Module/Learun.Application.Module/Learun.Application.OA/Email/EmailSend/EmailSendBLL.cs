using Learun.Util;
using System;
using System.Collections.Generic;

namespace Learun.Application.OA.Email.EmailSend
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.06.04
    /// 描 述：邮件发送管理
    /// </summary>
    public class EmailSendBLL : EmailSendIBLL
    {
        private EmailSendService emailSendService = new EmailSendService();

        #region 获取数据
        /// <summary>
        /// 获取发送邮件数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">关键词</param>
        /// <returns></returns>
        public IEnumerable<EmailSendEntity> GetSendList(Pagination pagination, string queryJson)
        {
            try
            {
                return emailSendService.GetSendList(pagination, queryJson);
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
        /// 获取邮件发送实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public EmailSendEntity GetSendEntity(string keyValue)
        {
            try
            {
                return emailSendService.GetSendEntity(keyValue);
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
                emailSendService.DeleteEntity(keyValue);
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
        /// <param name="sendEntity">邮件发送实体</param>
        /// <returns></returns>
        public void SaveSendEntity(string keyValue, EmailSendEntity sendEntity)
        {
            try
            {
                emailSendService.SaveSendEntity(keyValue, sendEntity);
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