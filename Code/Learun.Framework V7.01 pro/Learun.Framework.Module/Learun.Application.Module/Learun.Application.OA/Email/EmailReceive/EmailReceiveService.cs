using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learun.Application.OA.Email.EmailReceive
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.06.04
    /// 描 述：邮件接收管理
    /// </summary>
    public class EmailReceiveService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取收取邮件数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        public IEnumerable<EmailReceiveEntity> GetReceiveList(Pagination pagination,string queryJson)
        {
            try
            {
                UserInfo userInfo = LoginUserInfo.Get();
                var strSql = new StringBuilder();
                strSql.Append("SELECT * FROM LR_EmailReceive t WHERE t.F_DeleteMark = 0 AND t.F_EnabledMark = 0 AND F_CreatorUserId=@userId");

                var queryParam = queryJson.ToJObject();
                DateTime startTime = new DateTime(), endTime = new DateTime();
                // 日期
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    startTime = queryParam["StartTime"].ToDate();
                    endTime = queryParam["EndTime"].ToDate().AddDays(1);
                    strSql.Append(" AND ( t.F_Date >= @startTime AND t.F_Date <= @endTime ) ");
                }
                // 关键字
                string keyword = "";
                if (!queryParam["keyword"].IsEmpty())
                {
                    keyword = queryParam["keyword"].ToString();
                    strSql.Append(" AND (t.F_Subject like @keyword OR t.F_Sender like @keyword )");
                }
                return this.BaseRepository().FindList<EmailReceiveEntity>(strSql.ToString(), new { userId=userInfo.userId, startTime = startTime, endTime = endTime, keyword = "%" + keyword + "%" }, pagination);
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
        /// 获取邮件接收实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public EmailReceiveEntity GetReceiveEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<EmailReceiveEntity>(keyValue);
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
        /// 获取邮件接收个数
        /// </summary>
        /// <returns></returns>
        public int GetCount()
        {
            try
            {
                string sql = "SELECT F_Id FROM LR_EmailReceive";
                var list = this.BaseRepository().FindTable(sql);
                return list.Rows.Count;
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
                this.BaseRepository().Delete<EmailReceiveEntity>(t => t.F_Id == keyValue);
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
        /// <param name="receiveEntity">邮件接收实体</param>
        /// <returns></returns>
        public void SaveReceiveEntity(string keyValue, EmailReceiveEntity receiveEntity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    receiveEntity.Modify(keyValue);
                    this.BaseRepository().Update(receiveEntity);
                }
                else
                {
                    receiveEntity.Create();
                    this.BaseRepository().Insert(receiveEntity);
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