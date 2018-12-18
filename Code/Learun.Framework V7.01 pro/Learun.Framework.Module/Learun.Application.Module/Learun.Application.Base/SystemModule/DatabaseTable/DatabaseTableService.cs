using Learun.DataBase.Repository;
using Learun.Util;
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
    /// 描 述：数据库表管理
    /// </summary>
    public class DatabaseTableService : RepositoryFactory
    {
        #region 获取数据
        /// <summary>
        /// 数据表列表
        /// </summary>
        /// <param name="databaseLinkEntity">数据库连接信息</param>
        /// <returns></returns>
        public IEnumerable<DatabaseTableModel> GetTableList(DatabaseLinkEntity databaseLinkEntity)
        {
            try
            {
                if (databaseLinkEntity == null)
                {
                    return new List<DatabaseTableModel>();
                }
                return this.BaseRepository(databaseLinkEntity.F_DbConnection, databaseLinkEntity.F_DbType).GetDBTable<DatabaseTableModel>();
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
        /// 数据表字段列表
        /// </summary>
        /// <param name="databaseLinkEntity">数据库连接信息</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public IEnumerable<DatabaseTableFieldModel> GetTableFiledList(DatabaseLinkEntity databaseLinkEntity, string tableName)
        {
            try
            {
                if (databaseLinkEntity == null)
                {
                    return new List<DatabaseTableFieldModel>();
                }
                return this.BaseRepository(databaseLinkEntity.F_DbConnection, databaseLinkEntity.F_DbType).GetDBTableFields<DatabaseTableFieldModel>(tableName);
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
        /// 数据库表数据列表
        /// </summary>
        /// <param name="databaseLinkEntity">库连接信息</param>
        /// <param name="field">表明</param>
        /// <param name="switchWhere">条件</param>
        /// <param name="logic">逻辑</param>
        /// <param name="keyword">关键字</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public DataTable GetTableDataList(DatabaseLinkEntity databaseLinkEntity, string tableName, string field, string logic, string keyword, Pagination pagination)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT * FROM " + tableName + " WHERE 1=1");
                if (!string.IsNullOrEmpty(keyword) && !string.IsNullOrEmpty(logic) && logic != "Null" && logic != "NotNull")
                {
                    strSql.Append(" AND " + field + " ");
                    switch (logic)
                    {
                        case "Equal":           //等于
                            strSql.Append(" = ");
                            break;
                        case "NotEqual":        //不等于
                            strSql.Append(" <> ");
                            break;
                        case "Greater":         //大于
                            strSql.Append(" > ");
                            break;
                        case "GreaterThan":     //大于等于
                            strSql.Append(" >= ");
                            break;
                        case "Less":            //小于
                            strSql.Append(" < ");
                            break;
                        case "LessThan":        //小于等于
                            strSql.Append(" >= ");
                            break;
                        case "Like":            //包含
                            strSql.Append(" like ");
                            keyword = "%" + keyword + "%";
                            break;
                        default:
                            break;
                    }
                    strSql.Append("@keyword ");
                }
                if (logic == "Null")
                {
                    strSql.Append(" AND " + field + " is null ");
                }
                else if (logic == "NotNull")
                {
                    strSql.Append(" AND " + field + " is not null ");
                }
                return this.BaseRepository(databaseLinkEntity.F_DbConnection, databaseLinkEntity.F_DbType).FindTable(strSql.ToString(), new { keyword = keyword }, pagination);
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
        /// 数据库表数据列表
        /// </summary>
        /// <param name="databaseLinkEntity">库连接信息</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public DataTable GetTableDataList(DatabaseLinkEntity databaseLinkEntity, string tableName)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append(" SELECT * FROM " + tableName);
                return this.BaseRepository(databaseLinkEntity.F_DbConnection, databaseLinkEntity.F_DbType).FindTable(strSql.ToString());
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
        /// 数据库表数据列表
        /// </summary>
        /// <param name="databaseLinkEntity">库连接信息</param>
        /// <param name="strSql">表名</param>
        /// <returns></returns>
        public List<string> GetSqlColName(DatabaseLinkEntity databaseLinkEntity, string strSql)
        {
            try
            {
                var dt = this.BaseRepository(databaseLinkEntity.F_DbConnection, databaseLinkEntity.F_DbType).FindTable(strSql);
                List<string> res = new List<string>();
                foreach (DataColumn item in dt.Columns)
                {
                    res.Add(item.ColumnName);
                }
                return res;
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
        /// 创建数据库表
        /// </summary>
        /// <param name="databaseLinkEntity"></param>
        /// <param name="tableName"></param>
        /// <param name="tableRemark"></param>
        /// <param name="colList"></param>
        /// <returns></returns>
        public string CreateTable(DatabaseLinkEntity databaseLinkEntity, string tableName, string tableRemark, List<DatabaseTableFieldModel> colList) {
            try
            {
                if (databaseLinkEntity == null)
                {
                    return "当前数据库不存在";
                }

                List<DatabaseTableModel> tableList = (List<DatabaseTableModel>)GetTableList(databaseLinkEntity);
                if (tableList.Find(t => t.name == tableName) != null)
                {
                    return "表名重复";
                }
                else {
                    var db = this.BaseRepository(databaseLinkEntity.F_DbConnection, databaseLinkEntity.F_DbType).BeginTrans();
                    try
                    {
                        string sql = "";
                        switch (databaseLinkEntity.F_DbType)
                        {
                            case "SqlServer":
                                sql = GetSqlServerCreateTableSql(tableName, tableRemark, colList);
                                db.ExecuteBySql(sql);
                                break;
                            case "MySql":
                                sql = GetMySqlCreateTableSql(tableName, tableRemark, colList);
                                db.ExecuteBySql(sql);
                                break;
                            case "Oracle":
                                GetOracleCreateTableSql(tableName, tableRemark, colList,db);
                                break;
                        }
                        db.Commit();
                    }
                    catch (Exception)
                    {
                        db.Rollback();
                        throw;
                    }

                    return "建表成功";
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
        /// 获取sqlserver建表语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="tableRemark">表备注</param>
        /// <param name="colList">字段集合</param>
        /// <returns></returns>
        private string GetSqlServerCreateTableSql(string tableName, string tableRemark, List<DatabaseTableFieldModel> colList) {
            StringBuilder sql = new StringBuilder();

            sql.Append("CREATE TABLE " + tableName + " ( ");//表名
            foreach (var item in colList)
            {
                sql.Append(item.f_column + " " + item.f_datatype);//列名+类型
                if (item.f_datatype == "varchar" && item.f_length != 0)
                {
                    sql.Append("(" + item.f_length + ") ");//长度
                }
                if (item.f_key == "1")
                {
                    sql.Append(" PRIMARY KEY ");//是否主键
                }
                else if (item.f_isnullable == "0")
                {
                    sql.Append(" NOT NULL ");//是否为空
                }
                sql.Append(",");
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append(" )");
            foreach (var item in colList)
            {
                if (!string.IsNullOrEmpty(item.f_remark))
                { 
                    //添加列备注
                    sql.Append(" execute sp_addextendedproperty 'MS_Description','" + item.f_remark + "','user','dbo','table','" + tableName + "','column','" + item.f_column + "';");
                }
            }
            //添加表备注
            if (!string.IsNullOrEmpty(tableRemark))
            {
                sql.Append(" execute sp_addextendedproperty 'MS_Description','" + tableRemark + "','user','dbo','table','" + tableName + "',null,null;  ");
            }

            return sql.ToString();
        }
        /// <summary>
        /// 获取MySql建表语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="tableRemark">表备注</param>
        /// <param name="colList">字段集合</param>
        /// <returns></returns>
        private string GetMySqlCreateTableSql(string tableName, string tableRemark, List<DatabaseTableFieldModel> colList)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("CREATE TABLE " + tableName + " ( ");//表名
            foreach (var item in colList)
            {
                sql.Append(item.f_column + " " + item.f_datatype);//列名+类型
                if (item.f_datatype == "varchar" && item.f_length != 0)
                {
                    sql.Append("(" + item.f_length + ") ");//长度
                }
                if (item.f_key == "1")
                {
                    sql.Append(" PRIMARY KEY ");//是否主键
                }
                else if (item.f_isnullable == "0")
                {
                    sql.Append(" NOT NULL ");//是否为空
                }
                if (!string.IsNullOrEmpty(item.f_remark))
                {
                    sql.Append(" COMMENT '" + item.f_remark + "'");//列备注
                }
                sql.Append(",");
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append(" )");
            //添加表备注
            if (!string.IsNullOrEmpty(tableRemark))
            {
                sql.Append(" ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='"+ tableRemark + "';");
            }

            return sql.ToString();
        }
        /// <summary>
        /// 获取Oracle建表语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="tableRemark">表备注</param>
        /// <param name="colList">字段集合</param>
        /// <returns></returns>
        private void GetOracleCreateTableSql(string tableName, string tableRemark, List<DatabaseTableFieldModel> colList, IRepository db)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("CREATE TABLE \"" + tableName.ToUpper() + "\" ( ");//表名
            foreach (var item in colList)
            {
                sql.Append("\"" +item.f_column.ToUpper() + "\" " + GetOracleDataType(item.f_datatype));//列名+类型
                if (item.f_datatype == "varchar" && item.f_length != 0)
                {
                    sql.Append("(" + item.f_length + " CHAR) ");//长度
                }
                if (item.f_key == "1")
                {
                    sql.Append(" PRIMARY KEY NOT NULL ");//是否主键
                }
                else if (item.f_isnullable == "0")
                {
                    sql.Append(" NOT NULL ");//是否为空
                }
                else {
                    sql.Append(" NULL ");//是否为空
                }
                sql.Append(",");
            }
            sql.Remove(sql.Length - 1, 1);
            sql.Append(" )");

            db.ExecuteBySql(sql.ToString());

            ////添加表备注
            if (!string.IsNullOrEmpty(tableRemark))
            {
                db.ExecuteBySql(" COMMENT ON TABLE \"" + tableName.ToUpper() + "\" is '" + tableRemark + "'  ");
            }

            foreach (var item in colList)
            {
                if (!string.IsNullOrEmpty(item.f_remark))
                {
                    //添加列备注
                    db.ExecuteBySql(" COMMENT ON COLUMN \"" + tableName.ToUpper() + "\".\"" + item.f_column.ToUpper() + "\" is '" + item.f_remark + "'");
                }
            }
        }
        /// <summary>
        /// 获取字段类型
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        private string GetOracleDataType(string dataType) {
            string res = "";
            switch (dataType) {
                case "varchar":
                    res = "VARCHAR2";
                    break;
                case "datetime":
                    res = "DATE";
                    break;
                case "int":
                case "decimal":
                    res = "NUMBER(11)";
                    break;
            }

            return res;
        }
        #endregion
    }
}
