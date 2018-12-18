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
    /// 描 述：授權功能
    /// </summary>
    public class AuthorizeService : RepositoryFactory
    {
        #region 属性 构造函数
        private string fieldSql;
        public AuthorizeService()
        {
            fieldSql = @" 
                t.F_AuthorizeId,
                t.F_ObjectType,
                t.F_ObjectId,
                t.F_ItemType,
                t.F_ItemId,
                t.F_CreateDate,
                t.F_CreateUserId,
                t.F_CreateUserName
                ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取授权功能主键数据列表
        /// </summary>
        /// <param name="objectId">对象主键（角色,用户）</param>
        /// <param name="itemType">项目类型:1-菜单2-按钮3-视图</param>
        /// <returns></returns>
        public IEnumerable<AuthorizeEntity> GetList(string objectId, int itemType)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT " + fieldSql + " FROM LR_Base_Authorize t WHERE t.F_ObjectId = @objectId  AND t.F_ItemType = @itemType Order By t.F_ItemType,t.F_CreateDate ");
                return this.BaseRepository().FindList<AuthorizeEntity>(strSql.ToString(), new { objectId = objectId, itemType = itemType });
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
        /// 添加授权
        /// </summary>
        /// <param name="objectType">权限分类-1角色2用户</param>
        /// <param name="objectId">对象Id</param>
        /// <param name="appModuleIds">功能Id</param>
        public void SaveAppAuthorize(int objectType, string objectId, string[] appModuleIds )
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<AuthorizeEntity>(t => t.F_ObjectId == objectId);

                #region 功能
                foreach (string item in appModuleIds)
                {
                    AuthorizeEntity authorizeEntity = new AuthorizeEntity();
                    authorizeEntity.Create();
                    authorizeEntity.F_ObjectType = objectType;
                    authorizeEntity.F_ObjectId = objectId;
                    authorizeEntity.F_ItemType = 5;
                    authorizeEntity.F_ItemId = item;
                    db.Insert(authorizeEntity);
                }
                #endregion

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


        /// <summary>
        /// 添加授权
        /// </summary>
        /// <param name="objectType">权限分类-1角色2用户</param>
        /// <param name="objectId">对象Id</param>
        /// <param name="moduleIds">功能Id</param>
        /// <param name="moduleButtonIds">按钮Id</param>
        /// <param name="moduleColumnIds">视图Id</param>
        public void SaveAuthorize(int objectType, string objectId, string[] moduleIds, string[] moduleButtonIds, string[] moduleColumnIds, string[] moduleForms)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<AuthorizeEntity>(t => t.F_ObjectId == objectId);

                #region 功能
                foreach (string item in moduleIds)
                {
                    AuthorizeEntity authorizeEntity = new AuthorizeEntity();
                    authorizeEntity.Create();
                    authorizeEntity.F_ObjectType = objectType;
                    authorizeEntity.F_ObjectId = objectId;
                    authorizeEntity.F_ItemType = 1;
                    authorizeEntity.F_ItemId = item;
                    db.Insert(authorizeEntity);
                }
                #endregion

                #region 按钮
                foreach (string item in moduleButtonIds)
                {
                    AuthorizeEntity authorizeEntity = new AuthorizeEntity();
                    authorizeEntity.Create();
                    authorizeEntity.F_ObjectType = objectType;
                    authorizeEntity.F_ObjectId = objectId;
                    authorizeEntity.F_ItemType = 2;
                    authorizeEntity.F_ItemId = item;
                    db.Insert(authorizeEntity);
                }
                #endregion

                #region 视图
                foreach (string item in moduleColumnIds)
                {
                    AuthorizeEntity authorizeEntity = new AuthorizeEntity();
                    authorizeEntity.Create();
                    authorizeEntity.F_ObjectType = objectType;
                    authorizeEntity.F_ObjectId = objectId;
                    authorizeEntity.F_ItemType = 3;
                    authorizeEntity.F_ItemId = item;
                    db.Insert(authorizeEntity);
                }
                #endregion

                #region 表单
                foreach (string item in moduleForms)
                {
                    AuthorizeEntity authorizeEntity = new AuthorizeEntity();
                    authorizeEntity.Create();
                    authorizeEntity.F_ObjectType = objectType;
                    authorizeEntity.F_ObjectId = objectId;
                    authorizeEntity.F_ItemType = 4;
                    authorizeEntity.F_ItemId = item;
                    db.Insert(authorizeEntity);
                }
                #endregion

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
