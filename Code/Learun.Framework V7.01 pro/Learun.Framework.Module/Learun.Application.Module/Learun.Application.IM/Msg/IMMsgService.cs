using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learun.Application.IM
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：即时通讯消息内容
    /// </summary>
    public class IMMsgService : RepositoryFactory
    {
        
        #region 构造函数和属性
 
        private string fieldSql;
        public IMMsgService()
        {
            fieldSql = @"
                t.F_MsgId,
                t.F_IsSystem,
                t.F_SendUserId,
                t.F_RecvUserId,
                t.F_Content,
                t.F_CreateDate
            ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表数据(最近的10条聊天记录)
        /// <summary>
        /// <returns></returns>
        public IEnumerable<IMMsgEntity> GetList(string sendUserId, string recvUserId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_IM_Msg t where  (t.F_SendUserId = @sendUserId and  t.F_RecvUserId = @recvUserId ) or  (t.F_SendUserId = @recvUserId and  t.F_RecvUserId = @sendUserId ) ");

                Pagination pagination = new Pagination();
                pagination.page = 1;
                pagination.rows = 10;
                pagination.sidx = "F_CreateDate";
                pagination.sord = "DESC";

                this.BaseRepository().ExecuteBySql(" Update LR_IM_Contacts Set F_ISREAD = 2 where  F_MyUserId = @sendUserId AND  F_OtherUserId =  @recvUserId  ",new { sendUserId , recvUserId });

                return this.BaseRepository().FindList<IMMsgEntity>(strSql.ToString(),new { sendUserId, recvUserId }, pagination);
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
        /// 获取列表数据(小于某个时间点的5条记录)
        /// </summary>
        /// <param name="myUserId">我的ID</param>
        /// <param name="otherUserId">对方的ID</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public IEnumerable<IMMsgEntity> GetListByTime(string myUserId, string otherUserId, DateTime time)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_IM_Msg t where  ((t.F_SendUserId = @myUserId and  t.F_RecvUserId = @otherUserId ) or  (t.F_SendUserId = @otherUserId and  t.F_RecvUserId = @myUserId )) AND t.F_CreateDate <= @time ");

                Pagination pagination = new Pagination();
                pagination.page = 1;
                pagination.rows = 5;
                pagination.sidx = "F_CreateDate";
                pagination.sord = "DESC";

                return this.BaseRepository().FindList<IMMsgEntity>(strSql.ToString(), new { myUserId, otherUserId, time }, pagination);
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
        /// 获取列表数据(大于某个时间的所有数据)
        /// </summary>
        /// <param name="myUserId">我的ID</param>
        /// <param name="otherUserId">对方的ID</param>
        /// <param name="time">时间</param>
        /// <returns></returns>
        public IEnumerable<IMMsgEntity> GetListByTime2(string myUserId, string otherUserId, DateTime time)
        {
            try
            {
                time = time.AddSeconds(1);
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_IM_Msg t where  ((t.F_SendUserId = @myUserId and  t.F_RecvUserId = @otherUserId ) or  (t.F_SendUserId = @otherUserId and  t.F_RecvUserId = @myUserId )) AND t.F_CreateDate >= @time Order By  F_CreateDate ASC ");

                return this.BaseRepository().FindList<IMMsgEntity>(strSql.ToString(), new { myUserId, otherUserId, time });
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
        /// 获取列表分页数据
        /// <summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="sendUserId"></param>
        /// <param name="recvUserId"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IEnumerable<IMMsgEntity> GetPageList(Pagination pagination, string sendUserId, string recvUserId,string keyword)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_IM_Msg t where  ((t.F_SendUserId = @sendUserId and  t.F_RecvUserId = @recvUserId ) or  (t.F_SendUserId = @recvUserId and  t.F_RecvUserId = @sendUserId )) ");

                if (!string.IsNullOrEmpty(keyword)) {
                    keyword = "%" + keyword + "%";
                    strSql.Append(" AND F_Content like @keyword ");
                }

                return this.BaseRepository().FindList<IMMsgEntity>(strSql.ToString(), new { sendUserId, recvUserId, keyword }, pagination);
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
        /// 删除实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                this.BaseRepository().Delete<IMMsgEntity>(t=>t.F_MsgId == keyValue);
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
        /// 保存实体数据（新增）
        /// <param name="entity">实体数据</param>
        /// <summary>
        /// <returns></returns>
        public void SaveEntity(IMMsgEntity entity)
        {

            IMContactsEntity myContacts = new IMContactsEntity();
            IMContactsEntity otherContacts = new IMContactsEntity();

            myContacts.F_MyUserId = entity.F_SendUserId;
            myContacts.F_OtherUserId = entity.F_RecvUserId;
            myContacts.F_Content = entity.F_Content;

            otherContacts.F_MyUserId = entity.F_RecvUserId;
            otherContacts.F_OtherUserId = entity.F_SendUserId;
            otherContacts.F_Content = entity.F_Content;

            IMContactsEntity myContactsTmp = this.BaseRepository().FindEntity<IMContactsEntity>(t => t.F_MyUserId.Equals(myContacts.F_MyUserId) && t.F_OtherUserId.Equals(myContacts.F_OtherUserId));
            IMContactsEntity otherContactsTmp = this.BaseRepository().FindEntity<IMContactsEntity>(t => t.F_MyUserId.Equals(otherContacts.F_MyUserId) && t.F_OtherUserId.Equals(otherContacts.F_OtherUserId));
            var db = this.BaseRepository().BeginTrans();
            try
            {
                myContacts.F_IsRead = 2;
                if (myContactsTmp == null)
                {
                    myContacts.Create();
                    db.Insert(myContacts);
                }
                else
                {
                    myContacts.Modify(myContactsTmp.F_Id);
                    db.Update(myContacts);
                }

                otherContacts.F_IsRead = 1;
                if (otherContactsTmp == null)
                {
                    otherContacts.Create();
                    db.Insert(otherContacts);
                }
                else
                {
                    otherContacts.Modify(otherContactsTmp.F_Id);
                    db.Update(otherContacts);
                }

                entity.Create();
                db.Insert(entity);

                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
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
