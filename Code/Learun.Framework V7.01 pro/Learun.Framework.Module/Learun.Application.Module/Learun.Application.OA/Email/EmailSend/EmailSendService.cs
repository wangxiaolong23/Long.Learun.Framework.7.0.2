using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learun.Application.OA.Email.EmailSend
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.06.04
    /// 描 述：邮件发送管理
    /// </summary>
    public class EmailSendService : RepositoryFactory
    {
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
                var strSql = new StringBuilder();
                strSql.Append("SELECT * FROM LR_EmailSend t WHERE t.F_DeleteMark = 0 AND t.F_EnabledMark = 0 ");
                var queryParam = queryJson.ToJObject();
                DateTime startTime = new DateTime(), endTime = new DateTime();
                // 日期
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    startTime = queryParam["StartTime"].ToDate();
                    endTime = queryParam["EndTime"].ToDate().AddDays(1);
                    strSql.Append(" AND ( t.F_CreatorTime >= @startTime AND t.F_CreatorTime <= @endTime ) ");
                }
                // 关键字
                string keyword = "";
                if (!queryParam["keyword"].IsEmpty())
                {
                    keyword = queryParam["keyword"].ToString();
                    strSql.Append(" AND F_Subject like @keyword");
                }
                return this.BaseRepository().FindList<EmailSendEntity>(strSql.ToString(), new { startTime = startTime, endTime = endTime, keyword = "%" + keyword + "%" }, pagination);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
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
                return this.BaseRepository().FindEntity<EmailSendEntity>(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
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
                this.BaseRepository().Delete<EmailSendEntity>(t => t.F_Id == keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
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
                if (!string.IsNullOrEmpty(keyValue))
                {
                    sendEntity.Modify(keyValue);
                    this.BaseRepository().Update(sendEntity);
                }
                else
                {
                    sendEntity.Create();
                    this.BaseRepository().Insert(sendEntity);
                }
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        #endregion
    }
}