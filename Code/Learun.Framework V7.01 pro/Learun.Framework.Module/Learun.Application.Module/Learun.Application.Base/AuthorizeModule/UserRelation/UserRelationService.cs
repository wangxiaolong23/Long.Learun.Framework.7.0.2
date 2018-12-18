using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learun.Application.Base.AuthorizeModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：用户关联对象
    /// </summary>
    public class UserRelationService : RepositoryFactory
    {
        #region 构造函数和属性
        private string fieldSql;
        public UserRelationService()
        {
            fieldSql = @"
                t.F_UserRelationId,
                t.F_UserId,
                t.F_Category,
                t.F_ObjectId,
                t.F_CreateDate,
                t.F_CreateUserId,
                t.F_CreateUserName
            ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取对象主键列表信息
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="category">分类:1-角色2-岗位</param>
        /// <returns></returns>
        public IEnumerable<UserRelationEntity> GetObjectIdList(string userId, int category)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(" SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_Base_UserRelation t WHERE t.F_UserId = @userId AND t.F_Category =  @category ");
                return this.BaseRepository().FindList<UserRelationEntity>(strSql.ToString(), new { userId = userId,category=category });
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
        /// 获取用户主键列表信息
        /// </summary>
        /// <param name="objectId">用户主键</param>
        /// <returns></returns>
        public IEnumerable<UserRelationEntity> GetUserIdList(string objectId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(" SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_Base_UserRelation t WHERE t.F_ObjectId = @objectId");
                return this.BaseRepository().FindList<UserRelationEntity>(strSql.ToString(), new { objectId = objectId });
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
        /// 保存用户对应对象数据
        /// </summary>
        /// <param name="userRelationEntityList">列表</param>
        public void SaveEntityList(string objectId, IEnumerable<UserRelationEntity> userRelationEntityList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                db.Delete<UserRelationEntity>(t => t.F_ObjectId.Equals(objectId));
                foreach (UserRelationEntity userRelationEntity in userRelationEntityList)
                {
                    db.Insert(userRelationEntity);
                }
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
