using Dapper;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Learun.DataBase.Repository
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.04
    /// 描 述：定义仓储模型中的数据标准操作
    /// </summary>
    public class Repository : IRepository
    {
        private DbWhere dbWhere = (DbWhere)WebHelper.GetHttpItems("DataAhthorCondition");

        #region 构造
        /// <summary>
        /// 数据库操作接口
        /// </summary>
        public IDatabase db;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="idatabase"></param>
        public Repository(IDatabase idatabase)
        {
            this.db = idatabase;
        }
        #endregion

        #region 连接信息
        /// <summary>
        /// 获取连接上下文
        /// </summary>
        /// <returns></returns>
        public DbConnection getDbConnection()
        {
            return db.getDbConnection();
        }
        #endregion

        #region 事物提交
        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns></returns>
        public IRepository BeginTrans()
        {
            db.BeginTrans();
            return this;
        }
        /// <summary>
        /// 提交
        /// </summary>
        public void Commit()
        {
            db.Commit();
        }
        /// <summary>
        /// 回滚
        /// </summary>
        public void Rollback()
        {
            db.Rollback();
        }
        #endregion

        #region 执行 SQL 语句
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        public int ExecuteBySql(string strSql)
        {
            return db.ExecuteBySql(strSql);
        }
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public int ExecuteBySql(string strSql, object dbParameter)
        {
            return db.ExecuteBySql(strSql, dbParameter);
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <returns></returns>
        public int ExecuteByProc(string procName)
        {
            return db.ExecuteByProc(procName);
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public int ExecuteByProc(string procName, object dbParameter)
        {
            return db.ExecuteByProc(procName, dbParameter);
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <returns></returns>
        public T ExecuteByProc<T>(string procName) where T : class
        {
            return db.ExecuteByProc<T>(procName);
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public T ExecuteByProc<T>(string procName, object dbParameter) where T : class
        {
            return db.ExecuteByProc<T>(procName, dbParameter);
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <returns></returns>
        public IEnumerable<T> QueryByProc<T>(string procName) where T : class
        {
            return db.QueryByProc<T>(procName);
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public IEnumerable<T> QueryByProc<T>(string procName, object dbParameter) where T : class
        {

            return db.QueryByProc<T>(procName, dbParameter);
        }
        #endregion

        #region 对象实体 添加、修改、删除
        /// <summary>
        /// 插入实体数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        public int Insert<T>(T entity) where T : class
        {
            return db.Insert<T>(entity);
        }
        /// <summary>
        /// 批量插入实体数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entities">实体数据列表</param>
        /// <returns></returns>
        public int Insert<T>(List<T> entity) where T : class
        {
            return db.Insert<T>(entity);
        }
        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体数据（需要主键赋值）</param>
        /// <returns></returns>
        public int Delete<T>(T entity) where T : class
        {
            return db.Delete<T>(entity);
        }
        /// <summary>
        /// 批量删除实体数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entities">实体数据列表</param>
        /// <returns></returns>
        public int Delete<T>(List<T> entity) where T : class
        {
            return db.Delete<T>(entity);
        }
        /// <summary>
        /// 删除表数据（根据Lambda表达式）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <returns></returns>
        public int Delete<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return db.Delete<T>(condition);
        }
        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        public int Update<T>(T entity) where T : class
        {
            return db.Update<T>(entity);
        }
        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        public int UpdateEx<T>(T entity) where T : class
        {
            return db.UpdateEx<T>(entity);
        }
        /// <summary>
        /// 批量更新实体数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entities">实体数据列表</param>
        /// <returns></returns>
        public int Update<T>(List<T> entity) where T : class
        {
            return db.Update<T>(entity);
        }
        #endregion

        #region 对象实体 查询
        /// <summary>
        /// 查找一个实体根据主键
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        public T FindEntity<T>(object keyValue) where T : class
        {
            return db.FindEntity<T>(keyValue);
        }
        /// <summary>
        /// 查找一个实体（根据表达式）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <returns></returns>
        public T FindEntity<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return db.FindEntity<T>(condition);
        }
        /// <summary>
        /// 查找一个实体（根据sql）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public T FindEntity<T>(string strSql, object dbParameter) where T : class, new()
        {
            if (dbWhere != null)
            {
                int orderIndex = strSql.ToUpper().IndexOf("ORDER BY");
                if (orderIndex > 0)
                {
                    strSql = strSql.Substring(0, orderIndex);
                    string orderString = strSql.Substring(orderIndex);
                    strSql = string.Format(" select * From ({0})t Where {1} {2} ", strSql, dbWhere.sql,orderString);
                }
                else
                {
                    strSql = string.Format(" select * From ({0})t Where {1} ", strSql, dbWhere.sql);
                }

                DynamicParameters dynamicParameters = SqlHelper.FieldValueParamToParameter(dbWhere.dbParameters);
                dynamicParameters.AddDynamicParams(dbParameter);
                return db.FindEntity<T>(strSql, dynamicParameters);
            }
            else
            {
                return db.FindEntity<T>(strSql, dbParameter);
            }
        }
        /// <summary>
        /// 获取IQueryable表达式
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public IQueryable<T> IQueryable<T>() where T : class, new()
        {
            return db.IQueryable<T>();
        }
        /// <summary>
        /// 获取IQueryable表达式(根据表达式)
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <returns></returns>
        public IQueryable<T> IQueryable<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return db.IQueryable<T>(condition);
        }
        /// <summary>
        /// 查询列表（获取表所有数据）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>() where T : class, new()
        {
            return db.FindList<T>();
        }
        /// <summary>
        /// 查询列表根据sql语句
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(string strSql) where T : class
        {
            if (dbWhere != null)
            {
                int orderIndex = strSql.ToUpper().IndexOf("ORDER BY");
                if (orderIndex > 0)
                {
                    strSql = strSql.Substring(0, orderIndex);
                    string orderString = strSql.Substring(orderIndex);
                    strSql = string.Format(" select * From ({0})t Where {1} {2} ", strSql, dbWhere.sql, orderString);
                }
                else
                {
                    strSql = string.Format(" select * From ({0})t Where {1} ", strSql, dbWhere.sql);
                }
                DynamicParameters dynamicParameters = SqlHelper.FieldValueParamToParameter(dbWhere.dbParameters);
                return db.FindList<T>(strSql, dynamicParameters);
            }
            else
            {
                return db.FindList<T>(strSql);
            }
        }
        /// <summary>
        /// 查询列表根据sql语句(带参数)
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(string strSql, object dbParameter) where T : class
        {
            if (dbWhere != null)
            {
                int orderIndex = strSql.ToUpper().IndexOf("ORDER BY");
                if (orderIndex > 0)
                {
                    strSql = strSql.Substring(0, orderIndex);
                    string orderString = strSql.Substring(orderIndex);
                    strSql = string.Format(" select * From ({0})t Where {1} {2} ", strSql, dbWhere.sql, orderString);
                }
                else
                {
                    strSql = string.Format(" select * From ({0})t Where {1} ", strSql, dbWhere.sql);
                }
                DynamicParameters dynamicParameters = SqlHelper.FieldValueParamToParameter(dbWhere.dbParameters);
                dynamicParameters.AddDynamicParams(dbParameter);
                return db.FindList<T>(strSql, dynamicParameters);
            }
            else
            {
                return db.FindList<T>(strSql, dbParameter);
            }
        }
        /// <summary>
        /// 查询列表(分页)
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="pagination">分页数据</param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(Pagination pagination) where T : class, new()
        {
            int total = pagination.records;
            if (string.IsNullOrEmpty(pagination.sidx))
            {
                pagination.sidx = "";
                pagination.sord = "asc";
            }
            var data = db.FindList<T>(pagination.sidx, pagination.sord.ToLower() == "asc" ? true : false, pagination.rows, pagination.page, out total);
            pagination.records = total;
            return data;
        }
        /// <summary>
        /// 查询列表(分页)
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <param name="pagination">分页数据</param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, Pagination pagination) where T : class, new()
        {
            int total = pagination.records;
            if (string.IsNullOrEmpty(pagination.sidx))
            {
                pagination.sidx = "";
                pagination.sord = "asc";
            }
            var data = db.FindList<T>(condition, pagination.sidx, pagination.sord.ToLower() == "asc" ? true : false, pagination.rows, pagination.page, out total);
            pagination.records = total;
            return data;
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition) where T : class, new()
        {
            return db.FindList<T>(condition);
        }
        /// <summary>
        /// 查询列表(分页)
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="strSql">SQL语句</param>
        /// <param name="pagination">分页数据</param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(string strSql, Pagination pagination) where T : class
        {
            int total = pagination.records;
            if (string.IsNullOrEmpty(pagination.sidx))
            {
                pagination.sidx = "";
                pagination.sord = "asc";
            }

            if (dbWhere != null)
            {
                int orderIndex = strSql.ToUpper().IndexOf("ORDER BY");
                if (orderIndex > 0)
                {
                    strSql = strSql.Substring(0, orderIndex);
                    string orderString = strSql.Substring(orderIndex);
                    strSql = string.Format(" select * From ({0})t Where {1} {2} ", strSql, dbWhere.sql, orderString);
                }
                else
                {
                    strSql = string.Format(" select * From ({0})t Where {1} ", strSql, dbWhere.sql);
                }
                DynamicParameters dynamicParameters = SqlHelper.FieldValueParamToParameter(dbWhere.dbParameters);
                var data = db.FindList<T>(strSql, dynamicParameters, pagination.sidx, pagination.sord.ToLower() == "asc" ? true : false, pagination.rows, pagination.page, out total);
                pagination.records = total;
                return data;
            }
            else
            {
                var data = db.FindList<T>(strSql, pagination.sidx, pagination.sord.ToLower() == "asc" ? true : false, pagination.rows, pagination.page, out total);
                pagination.records = total;
                return data;
            }
        }
        /// <summary>
        /// 查询列表(分页)
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="strSql">SQL语句</param>
        /// <param name="dbParameter">参数</param>
        /// <param name="pagination">分页数据</param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(string strSql, object dbParameter, Pagination pagination) where T : class
        {
            int total = pagination.records;
            if (string.IsNullOrEmpty(pagination.sidx))
            {
                pagination.sidx = "";
                pagination.sord = "asc";
            }
            if (dbWhere != null)
            {
                int orderIndex = strSql.ToUpper().IndexOf("ORDER BY");
                if (orderIndex > 0)
                {
                    strSql = strSql.Substring(0, orderIndex);
                    string orderString = strSql.Substring(orderIndex);
                    strSql = string.Format(" select * From ({0})t Where {1} {2} ", strSql, dbWhere.sql, orderString);
                }
                else
                {
                    strSql = string.Format(" select * From ({0})t Where {1} ", strSql, dbWhere.sql);
                }
                DynamicParameters dynamicParameters = SqlHelper.FieldValueParamToParameter(dbWhere.dbParameters);
                dynamicParameters.AddDynamicParams(dbParameter);
                var data = db.FindList<T>(strSql, dynamicParameters, pagination.sidx, pagination.sord.ToLower() == "asc" ? true : false, pagination.rows, pagination.page, out total);
                pagination.records = total;
                return data;
            }
            else
            {
                var data = db.FindList<T>(strSql, dbParameter, pagination.sidx, pagination.sord.ToLower() == "asc" ? true : false, pagination.rows, pagination.page, out total);
                pagination.records = total;
                return data;
            }
        }
        #endregion

        #region 数据源 查询
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        public DataTable FindTable(string strSql)
        {
            if (dbWhere != null)
            {
                int orderIndex = strSql.ToUpper().IndexOf("ORDER BY");
                if (orderIndex > 0)
                {
                    strSql = strSql.Substring(0, orderIndex);
                    string orderString = strSql.Substring(orderIndex);
                    strSql = string.Format(" select * From ({0})t Where {1} {2} ", strSql, dbWhere.sql, orderString);
                }
                else
                {
                    strSql = string.Format(" select * From ({0})t Where {1} ", strSql, dbWhere.sql);
                }
                return db.FindTable(strSql, SqlHelper.FieldValueParamToParameter(dbWhere.dbParameters));
            }
            else
            {
                return db.FindTable(strSql);
            }
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public DataTable FindTable(string strSql, object dbParameter)
        {
            if (dbWhere != null)
            {
                int orderIndex = strSql.ToUpper().IndexOf("ORDER BY");
                if (orderIndex > 0)
                {
                    strSql = strSql.Substring(0, orderIndex);
                    string orderString = strSql.Substring(orderIndex);
                    strSql = string.Format(" select * From ({0})t Where {1} {2} ", strSql, dbWhere.sql, orderString);
                }
                else
                {
                    strSql = string.Format(" select * From ({0})t Where {1} ", strSql, dbWhere.sql);
                }
                DynamicParameters dynamicParameters = SqlHelper.FieldValueParamToParameter(dbWhere.dbParameters);
                dynamicParameters.AddDynamicParams(dbParameter);
                return db.FindTable(strSql, dynamicParameters);
            }
            else
            {
                return db.FindTable(strSql, dbParameter);
            }
        }
        /// <summary>
        /// 查询列表(分页)
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="pagination">分页数据</param>
        /// <returns></returns>
        public DataTable FindTable(string strSql, Pagination pagination)
        {
            int total = pagination.records;
            DataTable data;
            if (dbWhere != null)
            {
                int orderIndex = strSql.ToUpper().IndexOf("ORDER BY");
                if (orderIndex > 0)
                {
                    strSql = strSql.Substring(0, orderIndex);
                    string orderString = strSql.Substring(orderIndex);
                    strSql = string.Format(" select * From ({0})t Where {1} {2} ", strSql, dbWhere.sql, orderString);
                }
                else
                {
                    strSql = string.Format(" select * From ({0})t Where {1} ", strSql, dbWhere.sql);
                }
                DynamicParameters dynamicParameters = SqlHelper.FieldValueParamToParameter(dbWhere.dbParameters);
                data = db.FindTable(strSql, dynamicParameters, pagination.sidx, pagination.sord.ToLower() == "asc" ? true : false, pagination.rows, pagination.page, out total);
            }
            else
            {
                data = db.FindTable(strSql, pagination.sidx, pagination.sord.ToLower() == "asc" ? true : false, pagination.rows, pagination.page, out total);
            }
            pagination.records = total;
            return data;
        }
        /// <summary>
        /// 查询列表(分页)
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <param name="pagination">分页数据</param>
        /// <returns></returns>
        public DataTable FindTable(string strSql, object dbParameter, Pagination pagination)
        {
            int total = pagination.records;
            DataTable data;
            if (dbWhere != null)
            {
                int orderIndex = strSql.ToUpper().IndexOf("ORDER BY");
                if (orderIndex > 0)
                {
                    strSql = strSql.Substring(0, orderIndex);
                    string orderString = strSql.Substring(orderIndex);
                    strSql = string.Format(" select * From ({0})t Where {1} {2} ", strSql, dbWhere.sql, orderString);
                }
                else
                {
                    strSql = string.Format(" select * From ({0})t Where {1} ", strSql, dbWhere.sql);
                }
                DynamicParameters dynamicParameters = SqlHelper.FieldValueParamToParameter(dbWhere.dbParameters);
                dynamicParameters.AddDynamicParams(dbParameter);
                data = db.FindTable(strSql, dynamicParameters, pagination.sidx, pagination.sord.ToLower() == "asc" ? true : false, pagination.rows, pagination.page, out total);
            }
            else
            {
                data = db.FindTable(strSql, dbParameter, pagination.sidx, pagination.sord.ToLower() == "asc" ? true : false, pagination.rows, pagination.page, out total);
            }
            pagination.records = total;
            return data;
        }
        /// <summary>
        /// 获取查询对象
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <returns></returns>
        public object FindObject(string strSql)
        {
            if (dbWhere != null)
            {
                int orderIndex = strSql.ToUpper().IndexOf("ORDER BY");
                if (orderIndex > 0)
                {
                    strSql = strSql.Substring(0, orderIndex);
                    string orderString = strSql.Substring(orderIndex);
                    strSql = string.Format(" select * From ({0})t Where {1} {2} ", strSql, dbWhere.sql, orderString);
                }
                else
                {
                    strSql = string.Format(" select * From ({0})t Where {1} ", strSql, dbWhere.sql);
                }
                DynamicParameters dynamicParameters = SqlHelper.FieldValueParamToParameter(dbWhere.dbParameters);
                return db.FindObject(strSql, dynamicParameters);
            }
            else
            {
                return db.FindObject(strSql);
            }
        }
        /// <summary>
        /// 获取查询对象
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public object FindObject(string strSql, object dbParameter)
        {
            if (dbWhere != null)
            {
                int orderIndex = strSql.ToUpper().IndexOf("ORDER BY");
                if (orderIndex > 0)
                {
                    strSql = strSql.Substring(0, orderIndex);
                    string orderString = strSql.Substring(orderIndex);
                    strSql = string.Format(" select * From ({0})t Where {1} {2} ", strSql, dbWhere.sql, orderString);
                }
                else
                {
                    strSql = string.Format(" select * From ({0})t Where {1} ", strSql, dbWhere.sql);
                }
                DynamicParameters dynamicParameters = SqlHelper.FieldValueParamToParameter(dbWhere.dbParameters);
                dynamicParameters.AddDynamicParams(dbParameter);
                return db.FindObject(strSql, dynamicParameters);
            }
            else
            {
                return db.FindObject(strSql, dbParameter);
            }
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 获取数据库表数据
        /// </summary>
        /// <typeparam name="T">反序列化类型</typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetDBTable<T>() where T : class, new()
        {
            return db.GetDBTable<T>();
        }
        /// <summary>
        /// 获取数据库表字段数据
        /// </summary>
        /// <typeparam name="T">反序列化类型</typeparam>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public IEnumerable<T> GetDBTableFields<T>(string tableName) where T : class, new()
        {
            return db.GetDBTableFields<T>(tableName);
        }
        #endregion
    }
}
