using Dapper;
using Learun.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace Learun.DataBase.SqlServer
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组(力软数据库小组)
    /// 日 期：2017.03.04
    /// 描 述：数据库操作类
    /// </summary>
    public class Database : IDatabase
    {
        #region 构造函数
        ///// <summary>
        ///// 构造方法
        ///// </summary>
        public Database(string connString)
        {
            var obj = ConfigurationManager.ConnectionStrings[connString];
            string connectionString = obj == null ? connString : obj.ConnectionString;
            dbcontext = new DatabaseContext(connectionString);
        }
        #endregion

        #region 属性
        /// <summary>
        /// 获取 当前使用的数据访问上下文对象
        /// </summary>
        public DbContext dbcontext { get; set; }
        /// <summary>
        /// 事务对象
        /// </summary>
        public DbTransaction dbTransaction { get; set; }
        /// <summary>
        /// 获取连接上下文
        /// </summary>
        /// <returns></returns>
        public DbConnection getDbConnection() {
            return dbcontext.Database.Connection;
        }
        #endregion

        #region 事物提交
        /// <summary>
        /// 事务开始
        /// </summary>
        /// <returns></returns>
        public IDatabase BeginTrans()
        {
            if (dbcontext.Database.Connection.State == ConnectionState.Closed)
            {
                dbcontext.Database.Connection.Open();
            }
            dbTransaction = dbcontext.Database.Connection.BeginTransaction();
            dbcontext.Database.UseTransaction(dbTransaction);
            return this;
        }
        /// <summary>
        /// 提交当前操作的结果
        /// </summary>
        public int Commit()
        {
            try
            {
                int returnValue = dbcontext.SaveChanges();
                if (dbTransaction != null)
                {
                    dbTransaction.Commit();
                    this.Close();
                }
                return returnValue;
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.InnerException is SqlException)
                {
                    SqlException sqlEx = ex.InnerException.InnerException as SqlException;
                    throw ExceptionEx.ThrowDataAccessException(sqlEx, sqlEx.Message);
                }
                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 把当前操作回滚成未提交状态
        /// </summary>
        public void Rollback()
        {
            this.dbTransaction.Rollback();
            this.dbTransaction.Dispose();
            this.Close();
        }
        /// <summary>
        /// 关闭连接 内存回收
        /// </summary>
        public void Close()
        {
            dbcontext.Dispose();
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
            try
            {
                return dbcontext.Database.Connection.Execute(strSql, null, dbTransaction);
            }
            catch (Exception)
            {

                throw;
            }
            finally {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 执行sql语句(带参数)
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public int ExecuteBySql(string strSql, object dbParameter)
        {
            try
            {
                return dbcontext.Database.Connection.Execute(strSql, dbParameter, dbTransaction);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }

        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <returns></returns>
        public int ExecuteByProc(string procName)
        {
            try
            {
                return dbcontext.Database.Connection.Execute(procName, null, dbTransaction, null, CommandType.StoredProcedure);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public int ExecuteByProc(string procName, object dbParameter)
        {
            try
            {
                return dbcontext.Database.Connection.Execute(procName, dbParameter, dbTransaction, null, CommandType.StoredProcedure);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <returns></returns>
        public T ExecuteByProc<T>(string procName) where T : class
        {
            try
            {
                return dbcontext.Database.Connection.ExecuteScalar<T>(procName, null, dbTransaction, null, CommandType.StoredProcedure);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public T ExecuteByProc<T>(string procName, object dbParameter) where T : class
        {
            try
            {
                return dbcontext.Database.Connection.ExecuteScalar<T>(procName, dbParameter, dbTransaction, null, CommandType.StoredProcedure);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <returns></returns>
        public IEnumerable<T> QueryByProc<T>(string procName) where T : class
        {
            try
            {
                return dbcontext.Database.Connection.Query<T>(procName, null, dbTransaction, true, null, CommandType.StoredProcedure);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public IEnumerable<T> QueryByProc<T>(string procName, object dbParameter) where T : class
        {
            try
            {
                return dbcontext.Database.Connection.Query<T>(procName, dbParameter, dbTransaction, true, null, CommandType.StoredProcedure);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
           
        }
        /// <summary>
        /// 执行存储过程（获取集）
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public DataTable FindTableByProc(string procName, object dbParameter)
        {
            try
            {
                var IDataReader = dbcontext.Database.Connection.ExecuteReader(procName, dbParameter, dbTransaction, null, CommandType.StoredProcedure);
                return SqlHelper.IDataReaderToDataTable(IDataReader);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
            
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
            dbcontext.Entry<T>(entity).State = EntityState.Added;
            return dbTransaction == null ? this.Commit() : 0;
        }
        /// <summary>
        /// 批量插入实体数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entities">实体数据列表</param>
        /// <returns></returns>
        public int Insert<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                dbcontext.Entry<T>(entity).State = EntityState.Added;
            }
            return dbTransaction == null ? this.Commit() : 0;
        }
        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体数据（需要主键赋值）</param>
        /// <returns></returns>
        public int Delete<T>(T entity) where T : class
        {
            dbcontext.Set<T>().Attach(entity);
            dbcontext.Set<T>().Remove(entity);
            return dbTransaction == null ? this.Commit() : 0;
        }
        /// <summary>
        /// 批量删除实体数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entities">实体数据列表</param>
        /// <returns></returns>
        public int Delete<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                dbcontext.Set<T>().Attach(entity);
                dbcontext.Set<T>().Remove(entity);
            }
            return dbTransaction == null ? this.Commit() : 0;
        }
        /// <summary>
        /// 删除表数据（根据Lambda表达式）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <returns></returns>
        public int Delete<T>(Expression<Func<T, bool>> condition) where T : class,new()
        {
            IEnumerable<T> entities = dbcontext.Set<T>().Where(condition).ToList();
            return entities.Count() > 0 ? Delete(entities) : 0;
        }
        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        public int Update<T>(T entity) where T : class
        {
            this.UpdateEntity(entity);
            return dbTransaction == null ? this.Commit() : 0;
        }
        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        public int UpdateEx<T>(T entity) where T : class
        {
            dbcontext.Set<T>().Attach(entity);
            dbcontext.Entry(entity).State = EntityState.Modified;
            return dbTransaction == null ? this.Commit() : 0;
        }
        /// <summary>
        /// 批量更新实体数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entities">实体数据列表</param>
        /// <returns></returns>
        public int Update<T>(IEnumerable<T> entities) where T : class
        {
            foreach (var entity in entities)
            {
                this.UpdateEntity(entity);
            }
            return dbTransaction == null ? this.Commit() : 0;
        }
        /// <summary>
        /// EF更新实体
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体数据</param>
        private void UpdateEntity<T>(T entity) where T : class
        {
            dbcontext.Set<T>().Attach(entity);
            Hashtable props = SqlHelper.GetPropertyInfo<T>(entity);
            foreach (string item in props.Keys)
            {
                object value = dbcontext.Entry(entity).Property(item).CurrentValue;
                if (value != null)
                {
                    if (value.ToString() == "&nbsp;")
                        dbcontext.Entry(entity).Property(item).CurrentValue = null;
                    dbcontext.Entry(entity).Property(item).IsModified = true;
                }
            }
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
            try
            {
                return dbcontext.Set<T>().Find(keyValue);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 查找一个实体（根据表达式）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <returns></returns>
        public T FindEntity<T>(Expression<Func<T, bool>> condition) where T : class,new()
        {
            try
            {
                return dbcontext.Set<T>().Where(condition).FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 查找一个实体（根据sql）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public T FindEntity<T>(string strSql, object dbParameter = null) where T : class,new()
        {
            try
            {
                var data = dbcontext.Database.Connection.Query<T>(strSql, dbParameter, dbTransaction);
                return data.FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 获取IQueryable表达式
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public IQueryable<T> IQueryable<T>() where T : class,new()
        {
            try
            {
                return dbcontext.Set<T>();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 获取IQueryable表达式(根据表达式)
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <returns></returns>
        public IQueryable<T> IQueryable<T>(Expression<Func<T, bool>> condition) where T : class,new()
        {
            try
            {
                return dbcontext.Set<T>().Where(condition);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 查询列表（获取表所有数据）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>() where T : class,new()
        {
            try
            {
                return dbcontext.Set<T>().ToList();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 查询列表（获取表所有数据）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(Func<T, object> orderby) where T : class,new()
        {
            try
            {
                return dbcontext.Set<T>().OrderBy(orderby).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 查询列表根据表达式
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition) where T : class,new()
        {
            try
            {
                return dbcontext.Set<T>().Where(condition).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 查询列表根据sql语句
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(string strSql) where T : class
        {
            try
            {
                return dbcontext.Database.Connection.Query<T>(strSql, null, dbTransaction);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
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
            try
            {
                return dbcontext.Database.Connection.Query<T>(strSql, dbParameter, dbTransaction);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 查询列表(分页)
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="orderField">排序字段</param>
        /// <param name="isAsc">排序类型</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="total">总共数据条数</param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class,new()
        {
            try
            {
                string[] _order = orderField.Split(',');
                MethodCallExpression resultExp = null;
                var tempData = dbcontext.Set<T>().AsQueryable();
                foreach (string item in _order)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        string _orderPart = item;
                        _orderPart = Regex.Replace(_orderPart, @"\s+", " ");
                        string[] _orderArry = _orderPart.Split(' ');
                        string _orderField = _orderArry[0];
                        bool sort = isAsc;
                        if (_orderArry.Length == 2)
                        {
                            isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
                        }
                        var parameter = Expression.Parameter(typeof(T), "t");
                        var property = typeof(T).GetProperty(_orderField);
                        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                        var orderByExp = Expression.Lambda(propertyAccess, parameter);
                        resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(T), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));

                    }
                }
                if (resultExp != null)
                {
                    tempData = tempData.Provider.CreateQuery<T>(resultExp);
                }
                total = tempData.Count();
                tempData = tempData.Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize).AsQueryable();
                return tempData.ToList();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 查询列表(分页)带表达式条件
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="isAsc">排序类型</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="total">总共数据条数</param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class,new()
        {
            try
            {
                string[] _order = orderField.Split(',');
                MethodCallExpression resultExp = null;
                var tempData = dbcontext.Set<T>().Where(condition);
                foreach (string item in _order)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        string _orderPart = item;
                        _orderPart = Regex.Replace(_orderPart, @"\s+", " ");
                        string[] _orderArry = _orderPart.Split(' ');
                        string _orderField = _orderArry[0];
                        bool sort = isAsc;
                        if (_orderArry.Length == 2)
                        {
                            isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
                        }
                        var parameter = Expression.Parameter(typeof(T), "t");
                        var property = typeof(T).GetProperty(_orderField);
                        var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                        var orderByExp = Expression.Lambda(propertyAccess, parameter);
                        resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(T), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
                    }
                }
                if (resultExp != null)
                {
                    tempData = tempData.Provider.CreateQuery<T>(resultExp);
                }
                total = tempData.Count();
                tempData = tempData.Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize).AsQueryable();
                return tempData.ToList();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 查询列表(分页)根据sql语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="isAsc">排序类型</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="total">总共数据条数</param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class
        {
            return FindList<T>(strSql, null, orderField, isAsc, pageSize, pageIndex, out total);
        }
        /// <summary>
        /// 查询列表(分页)根据sql语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="isAsc">排序类型</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="total">总共数据条数</param>
        /// <returns></returns>
        public IEnumerable<T> FindList<T>(string strSql, object dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(SqlHelper.SqlPageSql(strSql, orderField, isAsc, pageSize, pageIndex));
                total = Convert.ToInt32(dbcontext.Database.Connection.ExecuteScalar("Select Count(1) From (" + strSql + ") As t", dbParameter));
                return dbcontext.Database.Connection.Query<T>(sb.ToString(), dbParameter, dbTransaction);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        #endregion

        #region 数据源查询
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        public DataTable FindTable(string strSql)
        {
            try
            {
                var IDataReader = dbcontext.Database.Connection.ExecuteReader(strSql, null, dbTransaction);
                return SqlHelper.IDataReaderToDataTable(IDataReader);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
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
            try
            {
                var IDataReader = dbcontext.Database.Connection.ExecuteReader(strSql, dbParameter, dbTransaction);
                return SqlHelper.IDataReaderToDataTable(IDataReader);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="isAsc">排序类型</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="total">总共数据条数</param>
        /// <returns></returns>
        public DataTable FindTable(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex, out int total)
        {
            return FindTable(strSql, null, orderField, isAsc, pageSize, pageIndex, out total);
        }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="isAsc">排序类型</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="total">总共数据条数</param>
        /// <returns></returns>
        public DataTable FindTable(string strSql, object dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex, out int total)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(SqlHelper.SqlPageSql(strSql, orderField, isAsc, pageSize, pageIndex));
                total = Convert.ToInt32(dbcontext.Database.Connection.ExecuteScalar("Select Count(1) From (" + strSql + ") As t", dbParameter));
                var IDataReader = dbcontext.Database.Connection.ExecuteReader(sb.ToString(), dbParameter, dbTransaction);
                return SqlHelper.IDataReaderToDataTable(IDataReader);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 获取查询对象
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        public object FindObject(string strSql)
        {

            return FindObject(strSql, null);
        }
        /// <summary>
        /// 获取查询对象
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public object FindObject(string strSql, object dbParameter)
        {
            try
            {
                return dbcontext.Database.Connection.ExecuteScalar(strSql, dbParameter, dbTransaction);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 获取数据库表数据
        /// </summary>
        /// <typeparam name="T">反序列化类型</typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetDBTable<T>() where T : class,new()
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"DECLARE @TableInfo TABLE ( name VARCHAR(50) , sumrows VARCHAR(11) , reserved VARCHAR(50) , data VARCHAR(50) , index_size VARCHAR(50) , unused VARCHAR(50) , pk VARCHAR(50) )
                            DECLARE @TableName TABLE ( name VARCHAR(50) )
                            DECLARE @name VARCHAR(50)
                            DECLARE @pk VARCHAR(50)
                            INSERT INTO @TableName ( name ) SELECT o.name FROM sysobjects o , sysindexes i WHERE o.id = i.id AND o.Xtype = 'U' AND i.indid < 2 ORDER BY i.rows DESC , o.name
                            WHILE EXISTS ( SELECT 1 FROM @TableName ) BEGIN SELECT TOP 1 @name = name FROM @TableName DELETE @TableName WHERE name = @name DECLARE @objectid INT SET @objectid = OBJECT_ID(@name) SELECT @pk = COL_NAME(@objectid, colid) FROM sysobjects AS o INNER JOIN sysindexes AS i ON i.name = o.name INNER JOIN sysindexkeys AS k ON k.indid = i.indid WHERE o.xtype = 'PK' AND parent_obj = @objectid AND k.id = @objectid INSERT INTO @TableInfo ( name , sumrows , reserved , data , index_size , unused ) EXEC sys.sp_spaceused @name UPDATE @TableInfo SET pk = @pk WHERE name = @name END
                            SELECT F.name as name, F.reserved  as reserved, F.data as data, F.index_size as index_size, RTRIM(F.sumrows) AS sumrows , F.unused as unused, ISNULL(p.tdescription, f.name) AS tdescription , F.pk as pk
                            FROM @TableInfo F LEFT JOIN ( SELECT name = CASE WHEN A.COLORDER = 1 THEN D.NAME ELSE '' END , tdescription = CASE WHEN A.COLORDER = 1 THEN ISNULL(F.VALUE, '') ELSE '' END FROM SYSCOLUMNS A LEFT JOIN SYSTYPES B ON A.XUSERTYPE = B.XUSERTYPE INNER JOIN SYSOBJECTS D ON A.ID = D.ID AND D.XTYPE = 'U' AND D.NAME <> 'DTPROPERTIES' LEFT JOIN sys.extended_properties F ON D.ID = F.major_id WHERE a.COLORDER = 1 AND F.minor_id = 0 ) P ON F.name = p.name
                             ORDER BY f.name  ");
                return dbcontext.Database.Connection.Query<T>(strSql.ToString(), null, dbTransaction);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 获取数据库表字段数据
        /// </summary>
        /// <typeparam name="T">反序列化类型</typeparam>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public IEnumerable<T> GetDBTableFields<T>(string tableName) where T : class,new()
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"SELECT [f_number] = a.colorder , [f_column] = a.name , [f_datatype] = b.name , [f_length] = COLUMNPROPERTY(a.id, a.name, 'PRECISION') , [f_identity] = CASE WHEN COLUMNPROPERTY(a.id, a.name, 'IsIdentity') = 1 THEN '1' ELSE '' END , [f_key] = CASE WHEN EXISTS ( SELECT 1 FROM sysobjects WHERE xtype = 'PK' AND parent_obj = a.id AND name IN ( SELECT name FROM sysindexes WHERE indid IN ( SELECT indid FROM sysindexkeys WHERE id = a.id AND colid = a.colid ) ) ) THEN '1' ELSE '' END , [f_isnullable] = CASE WHEN a.isnullable = 1 THEN '1' ELSE '' END , [f_defaults] = ISNULL(e.text, '') , [f_remark] = ISNULL(g.[value], a.name)
                                FROM syscolumns a LEFT JOIN systypes b ON a.xusertype = b.xusertype INNER JOIN sysobjects d ON a.id = d.id AND d.xtype = 'U' AND d.name <> 'dtproperties' LEFT JOIN syscomments e ON a.cdefault = e.id LEFT JOIN sys.extended_properties g ON a.id = g.major_id AND a.colid = g.minor_id LEFT JOIN sys.extended_properties f ON d.id = f.major_id AND f.minor_id = 0
                                WHERE d.name = @tableName
                                ORDER BY a.id , a.colorder");
                return dbcontext.Database.Connection.Query<T>(strSql.ToString(), new { tableName = tableName }, dbTransaction);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (dbTransaction == null)
                {
                    this.Close();
                }
            }
        }
        #endregion
    }
}
