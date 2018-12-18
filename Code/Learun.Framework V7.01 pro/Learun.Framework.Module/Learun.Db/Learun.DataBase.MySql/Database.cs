using Dapper;
using Learun.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace Learun.DataBase.MySqlEx
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
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="connString">连接串</param>
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
        public DbConnection getDbConnection()
        {
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
            //DbConnection dbConnection = ((IObjectContextAdapter)dbcontext).ObjectContext.Connection;
            //if (dbConnection.State == ConnectionState.Closed)
            //{
            //    dbConnection.Open();
            //}
            //dbTransaction = dbConnection.BeginTransaction();
            //return this;
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
                if (ex.InnerException != null && ex.InnerException.InnerException is MySqlException)
                {
                    MySqlException sqlEx = ex.InnerException.InnerException as MySqlException;
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
            finally
            {
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
                strSql = strSql.Replace("@", "?");
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
                strSql = strSql.Replace("@", "?");
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
        public IEnumerable<T> FindList<T>(Func<T, object> keySelector) where T : class,new()
        {
            try
            {
                return dbcontext.Set<T>().OrderBy(keySelector).ToList();
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
                strSql = strSql.Replace("@", "?");
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
                strSql = strSql.Replace("@", "?");
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
                tempData = tempData.Provider.CreateQuery<T>(resultExp);
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
                tempData = tempData.Provider.CreateQuery<T>(resultExp);
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
                strSql = strSql.Replace("@", "?");
                StringBuilder sb = new StringBuilder();
                sb.Append(SqlHelper.MySqlPageSql(strSql, orderField, isAsc, pageSize, pageIndex));
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
                strSql = strSql.Replace("@", "?");
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
                strSql = strSql.Replace("@", "?");
                StringBuilder sb = new StringBuilder();
                sb.Append(SqlHelper.MySqlPageSql(strSql, orderField, isAsc, pageSize, pageIndex));
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
                strSql = strSql.Replace("@", "?");
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
                strSql.Append(@"
                            SELECT t1.*,
                                (select COLUMN_NAME from information_schema.`COLUMNS` where TABLE_SCHEMA=database() and  TABLE_NAME =t1.name and COLUMN_KEY='PRI') pk
                                 from (
                                SELECT TABLE_NAME name,0 reserved,0 data,0 index_size,TABLE_ROWS sumrows,0 unused, (select IF(LENGTH(TRIM(TABLE_COMMENT))<1,TABLE_NAME,TABLE_COMMENT)) tdescription
                                from information_schema.`TABLES` where TABLE_SCHEMA =database()
                                ) t1 ORDER BY t1.name
                            ");
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
                strSql.Append(@" select  ORDINAL_POSITION f_number, COLUMN_NAME f_column, DATA_TYPE f_datatype, if(CHARACTER_MAXIMUM_LENGTH is null,if(LOCATE('int',column_type)>0,REPLACE(REPLACE(column_type,'int(',''),')',''),0),CHARACTER_MAXIMUM_LENGTH) f_length, '' f_identity, IF(COLUMN_KEY='PRI','1','') f_key,
                                        IF(IS_NULLABLE='YES','1','') f_isnullable,COLUMN_DEFAULT f_default,case when COLUMN_COMMENT='' then COLUMN_NAME else COLUMN_COMMENT END    f_remark from (
                                        select  *
                                         from information_schema.`COLUMNS` t1 where TABLE_SCHEMA=database() and TABLE_NAME = ?tableName 
                                        ) t2 order by f_number ");
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
