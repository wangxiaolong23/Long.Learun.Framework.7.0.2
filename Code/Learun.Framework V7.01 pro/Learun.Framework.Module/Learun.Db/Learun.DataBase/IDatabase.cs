using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace Learun.DataBase
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.04
    /// 描 述：数据库方法操作接口
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// 获取连接上下文
        /// </summary>
        /// <returns></returns>
        DbConnection getDbConnection();

        #region 事务
        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        IDatabase BeginTrans();
        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        int Commit();
        /// <summary>
        /// 回滚
        /// </summary>
        void Rollback();
        /// <summary>
        /// 关闭
        /// </summary>
        void Close();
        #endregion

        #region 执行 SQL 语句
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        int ExecuteBySql(string strSql);
        /// <summary>
        /// 执行sql语句(带参数)
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        int ExecuteBySql(string strSql, object dbParameter);
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <returns></returns>
        int ExecuteByProc(string procName);
        /// <summary>
        /// 执行存储过程 
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        int ExecuteByProc(string procName, object dbParameter);
         /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <returns></returns>
        T ExecuteByProc<T>(string procName) where T : class;
         /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        T ExecuteByProc<T>(string procName, object dbParameter) where T : class;
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <returns></returns>
        IEnumerable<T> QueryByProc<T>(string procName) where T : class;
        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        IEnumerable<T> QueryByProc<T>(string procName, object dbParameter) where T : class;
        #endregion

        #region 对象实体 添加、修改、删除
        /// <summary>
        /// 插入实体数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        int Insert<T>(T entity) where T : class;
        /// <summary>
        /// 批量插入实体数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entities">实体数据列表</param>
        /// <returns></returns>
        int Insert<T>(IEnumerable<T> entities) where T : class;
        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体数据（需要主键赋值）</param>
        /// <returns></returns>
        int Delete<T>(T entity) where T : class;
        /// <summary>
        /// 批量删除实体数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entities">实体数据列表</param>
        /// <returns></returns>
        int Delete<T>(IEnumerable<T> entities) where T : class;
        /// <summary>
        /// 删除表数据（根据Lambda表达式）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <returns></returns>
        int Delete<T>(Expression<Func<T, bool>> condition) where T : class,new();
        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        int Update<T>(T entity) where T : class;
        /// <summary>
        /// 更新实体数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体数据</param>
        /// <returns></returns>
        int UpdateEx<T>(T entity) where T : class;
        /// <summary>
        /// 批量更新实体数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entities">实体数据列表</param>
        /// <returns></returns>
        int Update<T>(IEnumerable<T> entities) where T : class;
        #endregion

        #region 对象实体 查询
        /// <summary>
        /// 查找一个实体根据主键
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        T FindEntity<T>(object KeyValue) where T : class;
        /// <summary>
        /// 查找一个实体（根据表达式）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <returns></returns>
        T FindEntity<T>(Expression<Func<T, bool>> condition) where T : class,new();
        /// <summary>
        /// 查找一个实体（根据sql）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        T FindEntity<T>(string strSql, object dbParameter = null) where T : class,new();
        /// <summary>
        /// 获取IQueryable表达式
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        IQueryable<T> IQueryable<T>() where T : class,new();
        /// <summary>
        /// 获取IQueryable表达式(根据表达式)
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <returns></returns>
        IQueryable<T> IQueryable<T>(Expression<Func<T, bool>> condition) where T : class,new();
        /// <summary>
        /// 查询列表（获取表所有数据）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        IEnumerable<T> FindList<T>() where T : class,new();
        /// <summary>
        /// 查询列表（获取表所有数据）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="orderby">排序</param>
        /// <returns></returns>
        IEnumerable<T> FindList<T>(Func<T, object> orderby) where T : class,new();
        /// <summary>
        /// 查询列表根据表达式
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <returns></returns>
        IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition) where T : class,new();
        /// <summary>
        /// 查询列表根据sql语句
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        IEnumerable<T> FindList<T>(string strSql) where T : class;
        /// <summary>
        /// 查询列表根据sql语句(带参数)
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        IEnumerable<T> FindList<T>(string strSql, object dbParameter) where T : class;
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
        IEnumerable<T> FindList<T>(string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class,new();
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
        IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class,new();
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
        IEnumerable<T> FindList<T>(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class;
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
        IEnumerable<T> FindList<T>(string strSql, object dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class;
        #endregion

        #region 数据源查询
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        DataTable FindTable(string strSql);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        DataTable FindTable(string strSql, object dbParameter);
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
        DataTable FindTable(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex, out int total);
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
        DataTable FindTable(string strSql, object dbParameter, string orderField, bool isAsc, int pageSize, int pageIndex, out int total);
        /// <summary>
        /// 获取查询对象
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <returns></returns>
        object FindObject(string strSql);
        /// <summary>
        /// 获取查询对象
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        object FindObject(string strSql, object dbParameter);
        #endregion

        #region 扩展方法
        /// <summary>
        /// 获取数据库表数据
        /// </summary>
        /// <typeparam name="T">反序列化类型</typeparam>
        /// <returns></returns>
        IEnumerable<T> GetDBTable<T>() where T : class,new();
        /// <summary>
        /// 获取数据库表字段数据
        /// </summary>
        /// <typeparam name="T">反序列化类型</typeparam>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        IEnumerable<T> GetDBTableFields<T>(string tableName) where T : class,new();
        #endregion
    }
}
