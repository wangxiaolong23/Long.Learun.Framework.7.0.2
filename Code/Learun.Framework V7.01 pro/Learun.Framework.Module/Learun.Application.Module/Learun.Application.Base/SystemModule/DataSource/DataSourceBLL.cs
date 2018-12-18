using Learun.Cache.Base;
using Learun.Cache.Factory;
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
    /// 描 述：数据源
    /// </summary>
    public class DataSourceBLL : DataSourceIBLL
    {
        private DataSourceService dataSourceService = new DataSourceService();
        private DatabaseLinkIBLL databaseLinkIBLL = new DatabaseLinkBLL();

        #region 缓存定义
        private ICache cache = CacheFactory.CaChe();
        private string cacheKey = "learun_adms_datasource_";// +编号
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public IEnumerable<DataSourceEntity> GetPageList(Pagination pagination, string keyword)
        {
            try
            {
                return dataSourceService.GetPageList(pagination, keyword);
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
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataSourceEntity> GetList()
        {
            try
            {
                return dataSourceService.GetList();
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
        /// 获取实体
        /// </summary>
        /// <param name="code">编号</param>
        /// <returns></returns>
        public DataSourceEntity GetEntityByCode(string code)
        {
            try
            {
                DataSourceEntity entity = cache.Read<DataSourceEntity>(cacheKey + code, CacheId.dataSource);
                if (entity == null)
                {
                    entity = dataSourceService.GetEntityByCode(code);
                    cache.Write<DataSourceEntity>(cacheKey + code, entity, CacheId.dataSource);
                }
                return entity;
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

        #region 提交数据
        /// <summary>
        /// 删除数据源
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                DataSourceEntity entity = dataSourceService.GetEntity(keyValue);
                cache.Remove(cacheKey + entity.F_Code, CacheId.dataSource);
                dataSourceService.DeleteEntity(keyValue);
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
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="dataSourceEntity">数据源实体</param>
        /// <returns></returns>
        public bool SaveEntity(string keyValue, DataSourceEntity dataSourceEntity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    cache.Remove(cacheKey + dataSourceEntity.F_Code, CacheId.dataSource);
                }
                return dataSourceService.SaveEntity(keyValue, dataSourceEntity);
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
        /// 获取数据源的数据
        /// </summary>
        /// <param name="code">数据源编码</param>
        /// <param name="strWhere">sql查询条件语句</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public DataTable GetDataTable(string code, string strWhere, string queryJson = "{}")
        {
            try
            {
                DataSourceEntity entity = GetEntityByCode(code);
                if (entity == null)
                {
                    return new DataTable();
                }
                else
                {
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        strWhere = " where " + strWhere;
                    }
                    else
                    {
                        strWhere = "";
                    }
                    var queryParam = queryJson.ToJObject();
                    string sql = string.Format(" select * From ({0})t {1} ", entity.F_Sql, strWhere);
                    return databaseLinkIBLL.FindTable(entity.F_DbId, sql, queryParam);
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
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="parentId">父级ID</param>
        /// <param name="Id">ID</param>
        /// <param name="showId">显示ID</param>
        /// <returns></returns>
        public List<TreeModel> GetTree(string code, string parentId, string Id, string showId)
        {
            try
            {
                DataSourceEntity entity = GetEntityByCode(code);
                if (entity == null)
                {
                    return new List<TreeModel>();
                }
                else
                {
                    DataTable list = databaseLinkIBLL.FindTable(entity.F_DbId, entity.F_Sql, new { });
                    List<TreeModel> treeList = new List<TreeModel>();
                    foreach (DataRow item in list.Rows)
                    {
                        TreeModel node = new TreeModel
                        {
                            id = item[Id].ToString(),
                            text = item[showId].ToString(),
                            value = item[Id].ToString(),
                            showcheck = false,
                            checkstate = 0,
                            isexpand = true,
                            parentId = item[parentId].ToString()
                        };
                        treeList.Add(node);
                    }
                    return treeList.ToTree();
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
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取数据源的数据(分页)
        /// </summary>
        /// <param name="code">数据源编码</param>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public DataTable GetDataTable(string code, Pagination pagination,string strWhere, string queryJson = "{}")
        {
            try
            {
                DataSourceEntity entity = GetEntityByCode(code);
                if (entity == null)
                {
                    return new DataTable();
                }
                else
                {
                    if (!string.IsNullOrEmpty(strWhere))
                    {
                        strWhere = " where " + strWhere;
                    }
                    else
                    {
                        strWhere = "";
                    }
                    var queryParam = queryJson.ToJObject();
                    string sql = string.Format(" select t.* From ({0})t {1} ", entity.F_Sql, strWhere);
                    return databaseLinkIBLL.FindTable(entity.F_DbId, sql, queryParam, pagination);
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
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取数据源列名
        /// </summary>
        /// <param name="code">数据源编码</param>
        /// <returns></returns>
        public List<string> GetDataColName(string code)
        {
            try
            {
                Pagination pagination = new Pagination()
                {
                    rows = 1,
                    page = 0,
                    sord = "",
                    sidx = ""
                };
                DataTable dt = GetDataTable(code, pagination, "");
                List<string> res = new List<string>();
                foreach (DataColumn item in dt.Columns)
                {
                    if (item.ColumnName != "rownum") {
                        res.Add(item.ColumnName);
                    }
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
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        #endregion
    }
}
