using Learun.Util;
using System;
using System.Collections.Generic;

namespace Learun.Application.OA.Email.EmailReceive
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.06.04
    /// 描 述：邮件接收管理
    /// </summary>
    public class EmailReceiveBLL : EmailReceiveIBLL
    {
        private EmailReceiveService emailReceiveService = new EmailReceiveService();

        #region 获取数据
        /// <summary>
        /// 获取收取邮件数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">关键词</param>
        /// <returns></returns>
        public IEnumerable<EmailReceiveEntity> GetReceiveList(Pagination pagination, string queryJson)
        {
            try
            {
                return emailReceiveService.GetReceiveList(pagination, queryJson);
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
        /// 获取邮件接收实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public EmailReceiveEntity GetReceiveEntity(string keyValue)
        {
            try
            {
                return emailReceiveService.GetReceiveEntity(keyValue);
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
        /// 获取邮件接收个数
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            try
            {
                return emailReceiveService.GetCount();
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
                emailReceiveService.DeleteEntity(keyValue);
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
        /// <param name="receiveEntity">邮件接收实体</param>
        /// <returns></returns>
        public void SaveReceiveEntity(string keyValue, EmailReceiveEntity receiveEntity)
        {
            try
            {
                emailReceiveService.SaveReceiveEntity(keyValue, receiveEntity);
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