using Learun.DataBase;
using Learun.DataBase.Repository;
using Learun.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Learun.Application.Base.SystemModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：数据库连接
    /// </summary>
    public class DatabaseLinkService : RepositoryFactory
    {
        #region 构造函数和属性
        private string fieldSql;
        public DatabaseLinkService()
        {
            fieldSql = @"
                    t.F_DatabaseLinkId,
                    t.F_ServerAddress,
                    t.F_DBName,
                    t.F_DBAlias,
                    t.F_DbType,
                    t.F_DbConnection,
                    t.F_DESEncrypt,
                    t.F_SortCode,
                    t.F_DeleteMark,
                    t.F_EnabledMark,
                    t.F_Description,
                    t.F_CreateDate,
                    t.F_CreateUserId,
                    t.F_CreateUserName,
                    t.F_ModifyDate,
                    t.F_ModifyUserId,
                    t.F_ModifyUserName
                ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取自定义查询条件（公共）
        /// </summary>
        /// <param name="moduleUrl">访问的功能链接地址</param>
        /// <param name="userId">用户ID（用户ID为null表示公共）</param>
        /// <returns></returns>
        public IEnumerable<DatabaseLinkEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_Base_DatabaseLink t WHERE  t.F_DeleteMark = 0 ");
                return this.BaseRepository().FindList<DatabaseLinkEntity>(strSql.ToString());
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
        /// 删除自定义查询条件
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void VirtualDelete(string keyValue)
        {
            try
            {
                DatabaseLinkEntity entity = new DatabaseLinkEntity()
                {
                    F_DatabaseLinkId = keyValue,
                    F_DeleteMark = 1
                };
                this.BaseRepository().Update(entity);
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
        /// 保存自定义查询（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="departmentEntity">部门实体</param>
        /// <returns></returns>
        public bool SaveEntity(string keyValue, DatabaseLinkEntity databaseLinkEntity)
        {
            try
            {
                /*测试数据库连接串"******";*/
                if (!string.IsNullOrEmpty(keyValue) && databaseLinkEntity.F_DbConnection == "******")
                {
                    databaseLinkEntity.F_DbConnection = null;// 不更新连接串地址
                }
                else
                {
                    try
                    {
                        databaseLinkEntity.F_ServerAddress = this.BaseRepository(databaseLinkEntity.F_DbConnection, databaseLinkEntity.F_DbType).getDbConnection().DataSource;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(keyValue))
                {
                    databaseLinkEntity.Modify(keyValue);
                    this.BaseRepository().Update(databaseLinkEntity);
                }
                else
                {
                    databaseLinkEntity.Create();
                    this.BaseRepository().Insert(databaseLinkEntity);
                }
                return true;
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

        #region 扩展方法
        /// <summary>
        /// 测试数据数据库是否能连接成功
        /// </summary>
        /// <param name="connection">连接串</param>
        /// <param name="dbType">数据库类型</param>
        public bool TestConnection(string connection, string dbType)
        {
            try
            {
                var db = this.BaseRepository(connection, dbType).BeginTrans();
                db.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 根据指定数据库执行sql语句
        /// </summary>
        /// <param name="entity">数据库实体</param>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        public void ExecuteBySql(DatabaseLinkEntity entity, string sql, object dbParameter)
        {
            try
            {
                if (dbParameter is JObject)
                {
                    var paramter = SqlHelper.JObjectToParameter((JObject)dbParameter);
                    this.BaseRepository(entity.F_DbConnection, entity.F_DbType).ExecuteBySql(sql, paramter);
                }
                else if (dbParameter is List<FieldValueParam>)
                {
                    var paramter = SqlHelper.FieldValueParamToParameter((List<FieldValueParam>)dbParameter);
                    this.BaseRepository(entity.F_DbConnection, entity.F_DbType).ExecuteBySql(sql, paramter);
                }
                else
                {
                    this.BaseRepository(entity.F_DbConnection, entity.F_DbType).ExecuteBySql(sql, dbParameter);
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
        /// <summary>
        /// 根据数据库执行sql语句,查询数据->datatable
        /// </summary>
        /// <param name="entity">数据库实体</param>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public DataTable FindTable(DatabaseLinkEntity entity, string sql, object dbParameter)
        {
            try
            {

                if (dbParameter is JObject)
                {
                    var paramter = SqlHelper.JObjectToParameter((JObject)dbParameter);
                    return this.BaseRepository(entity.F_DbConnection, entity.F_DbType).FindTable(sql, paramter);
                }
                else if (dbParameter is List<FieldValueParam>)
                {
                    var paramter = SqlHelper.FieldValueParamToParameter((List<FieldValueParam>)dbParameter);
                    return this.BaseRepository(entity.F_DbConnection, entity.F_DbType).FindTable(sql, paramter);
                }
                else
                {
                    return this.BaseRepository(entity.F_DbConnection, entity.F_DbType).FindTable(sql, dbParameter);
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
        /// <summary>
        /// 根据数据库执行sql语句,查询数据->datatable（分页）
        /// </summary>
        /// <param name="entity">数据库实体</param>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public DataTable FindTable(DatabaseLinkEntity entity, string sql, object dbParameter, Pagination pagination) {
            try
            {

                if (dbParameter is JObject)
                {
                    var paramter = SqlHelper.JObjectToParameter((JObject)dbParameter);
                    return this.BaseRepository(entity.F_DbConnection, entity.F_DbType).FindTable(sql, paramter, pagination);
                }
                else if (dbParameter is List<FieldValueParam>)
                {
                    var paramter = SqlHelper.FieldValueParamToParameter((List<FieldValueParam>)dbParameter);
                    return this.BaseRepository(entity.F_DbConnection, entity.F_DbType).FindTable(sql, paramter, pagination);
                }
                else
                {

                    return this.BaseRepository(entity.F_DbConnection, entity.F_DbType).FindTable(sql, dbParameter, pagination);
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

        #region 事务执行
        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="entity">数据库连接信息</param>
        /// <returns></returns>
        public IRepository BeginTrans(DatabaseLinkEntity entity)
        {
            try
            {
                return this.BaseRepository(entity.F_DbConnection, entity.F_DbType).BeginTrans();
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
        /// 根据指定数据库执行sql语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        public void ExecuteBySqlTrans(string sql, object dbParameter, IRepository db)
        {
            try
            {
                if (dbParameter is JObject)
                {
                    var paramter = SqlHelper.JObjectToParameter((JObject)dbParameter);
                    if (db != null)
                    {
                        db.ExecuteBySql(sql, paramter);
                    }
                }
                else if (dbParameter is List<FieldValueParam>)
                {
                    var paramter = SqlHelper.FieldValueParamToParameter((List<FieldValueParam>)dbParameter);
                    if (db != null)
                    {
                        db.ExecuteBySql(sql, paramter);
                    }
                }
                else
                {
                    if (db != null)
                    {
                        db.ExecuteBySql(sql, dbParameter);
                    }
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

        #endregion
    }
}
