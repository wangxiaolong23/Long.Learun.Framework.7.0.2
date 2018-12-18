using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace Learun.Application.Base.SystemModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：数据库表管理
    /// </summary>
    public class DatabaseTableBLL : DatabaseTableIBLL
    {
        #region 属性
        private DatabaseTableService databaseTableService = new DatabaseTableService();

        private DatabaseLinkIBLL databaseLinkIBLL = new DatabaseLinkBLL();
        #endregion

        #region 获取数据
        /// <summary>
        /// 数据表列表
        /// </summary>
        /// <param name="databaseLinkId">数据库连接主键</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public List<DatabaseTableModel> GetTableList(string databaseLinkId, string tableName)
        {
            try
            {
                DatabaseLinkEntity databaseLinkEntity = databaseLinkIBLL.GetEntity(databaseLinkId);

                List<DatabaseTableModel> list = (List<DatabaseTableModel>)databaseTableService.GetTableList(databaseLinkEntity);
                if (!string.IsNullOrEmpty(tableName))
                {
                    list = list.FindAll(t => t.name.Contains(tableName));
                }
                return list;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="databaseLinkId">数据库连接主键</param>
        /// <returns></returns>
        public List<TreeModel> GetTreeList(string databaseLinkId)
        {
            try
            {
                List<TreeModel> list = null;
                if (string.IsNullOrEmpty(databaseLinkId))
                {
                    list = databaseLinkIBLL.GetTreeListEx();
                }
                else
                {
                    list = new List<TreeModel>();
                    List<DatabaseTableModel> databaseTableList = GetTableList(databaseLinkId, "");
                    foreach (var item in databaseTableList)
                    {
                        TreeModel node = new TreeModel();
                        node.id = databaseLinkId + item.name;
                        node.text = item.name;
                        node.value = databaseLinkId;
                        node.title = item.tdescription;
                        node.complete = true;
                        node.isexpand = false;
                        node.hasChildren = false;
                        list.Add(node);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        /// <summary>
        /// 数据表字段列表
        /// </summary>
        /// <param name="databaseLinkId">数据库连接主键</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public IEnumerable<DatabaseTableFieldModel> GetTableFiledList(string databaseLinkId, string tableName)
        {
            try
            {
                DatabaseLinkEntity databaseLinkEntity = databaseLinkIBLL.GetEntity(databaseLinkId);

                return databaseTableService.GetTableFiledList(databaseLinkEntity, tableName);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取数据表字段树形数据
        /// </summary>
        /// <param name="databaseLinkId">数据库连接主键</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public List<TreeModel> GetFiledTreeList(string databaseLinkId, string tableName)
        {
            try
            {
                var list = GetTableFiledList(databaseLinkId, tableName);
                List<TreeModel> treeList = new List<TreeModel>();
                foreach (var item in list)
                {
                    TreeModel node = new TreeModel();
                    node.id = item.f_column;
                    node.text = item.f_column;
                    node.value = item.f_column;
                    node.title = item.f_remark;
                    node.complete = true;
                    node.isexpand = false;
                    node.hasChildren = false;
                    node.showcheck = true;
                    treeList.Add(node);
                }
                return treeList;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 数据库表数据列表
        /// </summary>
        /// <param name="databaseLinkId">数据库连接主键</param>
        /// <param name="field">表明</param>
        /// <param name="switchWhere">条件</param>
        /// <param name="logic">逻辑</param>
        /// <param name="keyword">关键字</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public DataTable GetTableDataList(string databaseLinkId, string tableName, string field, string logic, string keyword, Pagination pagination)
        {
            try
            {
                DatabaseLinkEntity databaseLinkEntity = databaseLinkIBLL.GetEntity(databaseLinkId);
                return databaseTableService.GetTableDataList(databaseLinkEntity, tableName, field, logic, keyword, pagination);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 数据库表数据列表
        /// </summary>
        /// <param name="databaseLinkId">数据库连接主键</param>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public DataTable GetTableDataList(string databaseLinkId, string tableName)
        {
            try
            {
                DatabaseLinkEntity databaseLinkEntity = databaseLinkIBLL.GetEntity(databaseLinkId);
                return databaseTableService.GetTableDataList(databaseLinkEntity, tableName);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 给定查询语句查询字段
        /// </summary>
        /// <param name="databaseLinkId">数据库连接主键</param>
        /// <param name="strSql">表名</param>
        /// <returns></returns>
        public List<string> GetSqlColName(string databaseLinkId, string strSql)
        {
            try
            {
                DatabaseLinkEntity databaseLinkEntity = databaseLinkIBLL.GetEntity(databaseLinkId);
                return databaseTableService.GetSqlColName(databaseLinkEntity, strSql);
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
        public string CreateTable(string databaseLinkId, string tableName, string tableRemark, List<DatabaseTableFieldModel> colList)
        {
            try
            {
                DatabaseLinkEntity databaseLinkEntity = databaseLinkIBLL.GetEntity(databaseLinkId);
                return databaseTableService.CreateTable(databaseLinkEntity, tableName, tableRemark, colList);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// C#实体数据类型
        /// </summary>
        /// <param name="datatype">数据库字段类型</param>
        /// <returns></returns>
        public string FindModelsType(string datatype)
        {
            string res = "string";
            datatype = datatype.ToLower();
            switch (datatype)
            {
                case "int":
                case "number":
                case "integer":
                case "smallint":
                    res = "int?";
                    break;
                case "tinyint":
                    res = "byte?";
                    break;
                case "numeric":
                case "real":
                case "float":
                case "decimal":
                case "number(8,2)":
                case "money":
                case "smallmoney":
                    res = "decimal?";
                    break;
                case "char":
                case "varchar":
                case "nvarchar2":
                case "text":
                case "nchar":
                case "nvarchar":
                case "ntext":
                    res = "string";
                    break;
                case "bit":
                    res = "bool?";
                    break;
                case "datetime":
                case "date":
                case "smalldatetime":
                    res = "DateTime?";
                    break;
                default:
                    res = "string";
                    break;
            }
            return res;
        }
        #endregion
    }
}
