using Dapper;
using Learun.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using System.Text;

namespace Learun.DataBase
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.04
    /// 描 述：数据访问扩展
    /// </summary>
    public static class SqlHelper
    {
        #region 转换扩展类
        /// <summary>
        ///  将IDataReader转换为DataTable
        /// </summary>
        /// <param name="dr">数据读取接口</param>
        /// <returns></returns>
        public static DataTable IDataReaderToDataTable(IDataReader reader)
        {
            using (reader)
            {
                DataTable objDataTable = new DataTable("Table");
                int intFieldCount = reader.FieldCount;
                for (int intCounter = 0; intCounter < intFieldCount; ++intCounter)
                {
                    objDataTable.Columns.Add(reader.GetName(intCounter).ToLower(), reader.GetFieldType(intCounter));
                }
                objDataTable.BeginLoadData();
                object[] objValues = new object[intFieldCount];
                while (reader.Read())
                {
                    reader.GetValues(objValues);
                    objDataTable.LoadDataRow(objValues, true);
                }
                reader.Close();
                reader.Dispose();
                objDataTable.EndLoadData();
                return objDataTable;
            }
        }
        /// <summary>
        /// 获取实体类键值（缓存）
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public static Hashtable GetPropertyInfo<T>(T entity)
        {
            Type type = entity.GetType();
            //object CacheEntity = CacheHelper.GetCache("CacheEntity_" + EntityAttribute.GetEntityTable<T>());
            object CacheEntity = null;
            if (CacheEntity == null)
            {
                Hashtable ht = new Hashtable();
                PropertyInfo[] props = type.GetProperties();
                foreach (PropertyInfo prop in props)
                {
                    bool flag = true;
                    foreach (Attribute attr in prop.GetCustomAttributes(true))
                    {
                        NotMappedAttribute notMapped = attr as NotMappedAttribute;
                        if (notMapped != null)
                        {
                            flag = false;
                            break;
                        }

                    }

                    if (flag)
                    {
                        string name = prop.Name;
                        object value = prop.GetValue(entity, null);
                        ht[name] = value;
                    }
                }
                //CacheHelper.SetCache("CacheEntity_" + EntityAttribute.GetEntityTable<T>(), ht);
                return ht;
            }
            else
            {
                return (Hashtable)CacheEntity;
            }
        }
        /// <summary>
        /// 将json对象转化成Dapper可认的参数
        /// </summary>
        /// <param name="jObject">json对象</param>
        /// <returns></returns>
        public static DynamicParameters JObjectToParameter(JObject jObject)
        {
            try
            {
                var args = new DynamicParameters(new { });
                foreach (var item in jObject)
                {
                    args.Add(item.Key.ToString(), item.Value.ToString());
                }
                return args;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 将对象转化成Dapper可认的参数
        /// </summary>
        /// <param name="fieldValueParams">对象</param>
        /// <returns></returns>
        public static DynamicParameters FieldValueParamToParameter(List<FieldValueParam> fieldValueParams)
        {
            try
            {
                var args = new DynamicParameters(new { });
                foreach (var item in fieldValueParams)
                {
                    args.Add(item.name, item.value, (DbType)item.type);
                }
                return args;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 分页语句
        /// <summary>
        /// sql分页语句
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="isAsc">排序类型</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public static StringBuilder SqlPageSql(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex)
        {
            StringBuilder sb = new StringBuilder();
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            int num = (pageIndex - 1) * pageSize;
            int num1 = (pageIndex) * pageSize;
            string OrderBy = "";

            if (!string.IsNullOrEmpty(orderField))
            {
                if (orderField.ToUpper().IndexOf("ASC") + orderField.ToUpper().IndexOf("DESC") > 0)
                {
                    OrderBy = " Order By " + orderField;
                }
                else
                {
                    OrderBy = " Order By " + orderField + " " + (isAsc ? "ASC" : "DESC");
                }
            }
            else
            {
                OrderBy = "order by (select 0)";
            }
            sb.Append("Select * From (Select ROW_NUMBER() Over (" + OrderBy + ")");
            sb.Append(" As rowNum, * From (" + strSql + ")  T ) As N Where rowNum > " + num + " And rowNum <= " + num1 + "");
            return sb;
        }
        /// <summary>
        /// oracle分页语句
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="isAsc">排序类型</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public static StringBuilder OraclePageSql(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex)
        {
            StringBuilder sb = new StringBuilder();
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            int num = (pageIndex - 1) * pageSize;
            int num1 = (pageIndex) * pageSize;
            string OrderBy = "";

            if (!string.IsNullOrEmpty(orderField))
            {
                if (orderField.ToUpper().IndexOf("ASC") + orderField.ToUpper().IndexOf("DESC") > 0)
                {
                    OrderBy = " Order By " + orderField;
                }
                else
                {
                    OrderBy = " Order By " + orderField + " " + (isAsc ? "ASC" : "DESC");
                }
            }
            sb.Append("Select * From (Select ROWNUM lrrn,");
            sb.Append(" T.* From (" + strSql + OrderBy + ")  T )  N Where lrrn > " + num + " And lrrn <= " + num1 + "");
            return sb;
        }
        /// <summary>
        /// mysql分页语句
        /// </summary>
        /// <param name="strSql">sql语句</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="isAsc">排序类型</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public static StringBuilder MySqlPageSql(string strSql, string orderField, bool isAsc, int pageSize, int pageIndex)
        {
            StringBuilder sb = new StringBuilder();
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            int num = (pageIndex - 1) * pageSize;
            string OrderBy = "";

            if (!string.IsNullOrEmpty(orderField))
            {
                if (orderField.ToUpper().IndexOf("ASC") + orderField.ToUpper().IndexOf("DESC") > 0)
                {
                    OrderBy = " Order By " + orderField;
                }
                else
                {
                    OrderBy = " Order By " + orderField + " " + (isAsc ? "ASC" : "DESC");
                }
            }
            sb.Append(strSql + OrderBy);
            sb.Append(" limit " + num + "," + pageSize + "");
            return sb;
        }
        #endregion
    }
}
