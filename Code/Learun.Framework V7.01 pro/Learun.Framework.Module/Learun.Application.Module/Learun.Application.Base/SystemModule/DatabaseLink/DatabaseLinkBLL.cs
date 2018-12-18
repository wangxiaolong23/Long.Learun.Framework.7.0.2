using Learun.Cache.Base;
using Learun.Cache.Factory;
using Learun.DataBase.Repository;
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
    /// 描 述：数据库连接
    /// </summary>
    public class DatabaseLinkBLL : DatabaseLinkIBLL
    {
        #region 属性
        private DatabaseLinkService databaseLinkService = new DatabaseLinkService();
        #endregion

        #region 缓存定义
        private ICache cache = CacheFactory.CaChe();
        private string cacheKey = "learun_adms_databaseLink";
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public List<DatabaseLinkEntity> GetList()
        {
            try
            {
                List<DatabaseLinkEntity> list = cache.Read<List<DatabaseLinkEntity>>(cacheKey, CacheId.database);
                if (list == null)
                {
                    list = (List<DatabaseLinkEntity>)databaseLinkService.GetList();
                    cache.Write<List<DatabaseLinkEntity>>(cacheKey, list, CacheId.database);
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
        /// 获取映射数据
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, DatabaseLinkModel> GetMap() {
            try
            {
                Dictionary<string, DatabaseLinkModel> dic = cache.Read<Dictionary<string, DatabaseLinkModel>>(cacheKey + "dic", CacheId.database);
                if (dic == null)
                {
                    dic = new Dictionary<string, DatabaseLinkModel>();
                    List<DatabaseLinkEntity> list = GetList();
                    foreach (var item in list)
                    {
                        dic.Add(item.F_DatabaseLinkId, new DatabaseLinkModel()
                        {
                            alias = item.F_DBAlias,
                            name = item.F_DBName
                        });
                    }
                    cache.Write(cacheKey + "dic", dic, CacheId.database);
                }
                return dic;
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
        /// 获取列表数据(去掉连接串地址信息)
        /// </summary>
        /// <returns></returns>
        public List<DatabaseLinkEntity> GetListByNoConnection()
        {
            try
            {
                List<DatabaseLinkEntity> list = GetList();
                foreach (var item in list) {
                    item.F_DbConnection = "******";
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
        /// 获取列表数据
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public List<DatabaseLinkEntity> GetListByNoConnection(string keyword)
        {
            try
            {
                List<DatabaseLinkEntity> list = GetListByNoConnection();
                if (!string.IsNullOrEmpty(keyword))
                {
                    list = list.FindAll(t => t.F_DBName.Contains(keyword) || t.F_DBAlias.Contains(keyword) || t.F_ServerAddress.Contains(keyword));
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
        /// <returns></returns>
        public List<TreeModel> GetTreeList() {
            List<DatabaseLinkEntity> list = GetList();
            List<TreeModel> treelist = new List<TreeModel>();
            Dictionary<string, List<TreeModel>> dic = new Dictionary<string, List<TreeModel>>();

            foreach (var item in list) {
                TreeModel node = new TreeModel();
                node.id = item.F_DatabaseLinkId;
                node.text = item.F_DBAlias;
                node.value = item.F_DBName;
                node.complete = true;
                node.hasChildren = false;

                if (!dic.ContainsKey(item.F_ServerAddress))
                {
                    TreeModel pnode = new TreeModel();
                    pnode.id = item.F_ServerAddress;
                    pnode.text = item.F_ServerAddress;
                    pnode.value = "learunServerAddress";
                    pnode.isexpand = true;
                    pnode.complete = true;
                    pnode.hasChildren = true;
                    pnode.ChildNodes = new List<TreeModel>();
                    treelist.Add(pnode);
                    dic.Add(item.F_ServerAddress, pnode.ChildNodes);
                }
                dic[item.F_ServerAddress].Add(node);
            }
            return treelist;
        }
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <returns></returns>
        public List<TreeModel> GetTreeListEx()
        {
            List<DatabaseLinkEntity> list = GetList();
            List<TreeModel> treelist = new List<TreeModel>();
            Dictionary<string, List<TreeModel>> dic = new Dictionary<string, List<TreeModel>>();
            foreach (var item in list)
            {
                TreeModel node = new TreeModel();
                node.id = item.F_DatabaseLinkId;
                node.text = item.F_DBAlias;
                node.value = item.F_DBName;
                node.complete = false;
                node.isexpand = false;
                node.hasChildren = true;

                if (!dic.ContainsKey(item.F_ServerAddress))
                {
                    TreeModel pnode = new TreeModel();
                    pnode.id = item.F_ServerAddress;
                    pnode.text = item.F_ServerAddress;
                    pnode.value = "learunServerAddress";
                    pnode.isexpand = true;
                    pnode.complete = true;
                    pnode.hasChildren = true;
                    pnode.ChildNodes = new List<TreeModel>();
                    treelist.Add(pnode);
                    dic.Add(item.F_ServerAddress, pnode.ChildNodes);
                }
                dic[item.F_ServerAddress].Add(node);
            }
            return treelist;
        }
        /// <summary>
        /// 获取数据连接实体
        /// </summary>
        /// <param name="databaseLinkId">主键</param>
        /// <returns></returns>
        public DatabaseLinkEntity GetEntity(string databaseLinkId) {
            try
            {
                List<DatabaseLinkEntity> list = GetList();
                return list.Find(t => t.F_DatabaseLinkId.Equals(databaseLinkId));
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
        /// 删除自定义查询条件
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void VirtualDelete(string keyValue)
        {
            try
            {
                cache.Remove(cacheKey, CacheId.database);
                cache.Remove(cacheKey + "dic", CacheId.database);
                databaseLinkService.VirtualDelete(keyValue);
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
        /// 保存自定义查询（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="departmentEntity">部门实体</param>
        /// <returns></returns>
        public bool SaveEntity(string keyValue, DatabaseLinkEntity databaseLinkEntity)
        {
            try
            {
                cache.Remove(cacheKey, CacheId.database);
                cache.Remove(cacheKey+"dic", CacheId.database);
                return databaseLinkService.SaveEntity(keyValue, databaseLinkEntity);
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
        /// 测试数据数据库是否能连接成功
        /// </summary>
        /// <param name="connection">连接串</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="keyValue">主键</param>
        public bool TestConnection(string connection, string dbType, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue) && connection == "******") {
                DatabaseLinkEntity entity = GetEntity(keyValue);
                connection = entity.F_DbConnection;
            }

            return databaseLinkService.TestConnection(connection, dbType);
        }
        /// <summary>
        /// 根据指定数据库执行sql语句
        /// </summary>
        /// <param name="databaseLinkId">数据库主键</param>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        public void ExecuteBySql(string databaseLinkId, string sql,object dbParameter=null)
        {
            try
            {
                DatabaseLinkEntity entity = GetEntity(databaseLinkId);
                databaseLinkService.ExecuteBySql(entity, sql, dbParameter);
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
        /// 根据数据库执行sql语句,查询数据->datatable
        /// </summary>
        /// <param name="databaseLinkId">数据库主键</param>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        public DataTable FindTable(string databaseLinkId, string sql, object dbParameter = null)
        {
            try
            {
                DatabaseLinkEntity entity = GetEntity(databaseLinkId);
                return databaseLinkService.FindTable(entity, sql, dbParameter);
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
        /// 根据数据库执行sql语句,查询数据->datatable（分页）
        /// </summary>
        /// <param name="databaseLinkId">数据库主键</param>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public DataTable FindTable(string databaseLinkId, string sql, object dbParameter, Pagination pagination)
        {
            try
            {

                DatabaseLinkEntity entity = GetEntity(databaseLinkId);
                return databaseLinkService.FindTable(entity, sql, dbParameter, pagination);
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
        /// 开启事务
        /// </summary>
        /// <param name="entity">数据库连接信息</param>
        /// <returns></returns>
        public IRepository BeginTrans(string databaseLinkId)
        {
            try
            {
                DatabaseLinkEntity entity = GetEntity(databaseLinkId);
                return databaseLinkService.BeginTrans(entity);
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
        /// 根据指定数据库执行sql语句(事务)
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <param name="数据库连接">db</param>
        public void ExecuteBySqlTrans(string sql, object dbParameter,IRepository db)
        {
            try
            {
                databaseLinkService.ExecuteBySqlTrans(sql, dbParameter, db);
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
    }
}
