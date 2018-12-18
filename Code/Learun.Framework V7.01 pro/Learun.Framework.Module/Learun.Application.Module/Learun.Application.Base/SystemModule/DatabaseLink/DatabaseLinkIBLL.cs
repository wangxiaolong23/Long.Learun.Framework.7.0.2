using Learun.DataBase.Repository;
using Learun.Util;
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
    public interface DatabaseLinkIBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        List<DatabaseLinkEntity> GetList();

        /// <summary>
        /// 获取映射数据
        /// </summary>
        /// <returns></returns>
        Dictionary<string, DatabaseLinkModel> GetMap();
        /// <summary>
        /// 获取列表数据(去掉连接串地址信息)
        /// </summary>
        /// <returns></returns>
        List<DatabaseLinkEntity> GetListByNoConnection();
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        List<DatabaseLinkEntity> GetListByNoConnection(string keyword);
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <returns></returns>
        List<TreeModel> GetTreeList();
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <returns></returns>
        List<TreeModel> GetTreeListEx();
        /// <summary>
        /// 获取数据连接实体
        /// </summary>
        /// <param name="databaseLinkId">主键</param>
        /// <returns></returns>
        DatabaseLinkEntity GetEntity(string databaseLinkId);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除自定义查询条件
        /// </summary>
        /// <param name="keyValue">主键</param>
        void VirtualDelete(string keyValue);
        /// <summary>
        /// 保存自定义查询（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="departmentEntity">部门实体</param>
        /// <returns></returns>
        bool SaveEntity(string keyValue, DatabaseLinkEntity databaseLinkEntity);
        #endregion

        #region 扩展方法
        /// <summary>
        /// 测试数据数据库是否能连接成功
        /// </summary>
        /// <param name="connection">连接串</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="keyValue">主键</param>
        bool TestConnection(string connection, string dbType, string keyValue);
        /// <summary>
        /// 根据指定数据库执行sql语句
        /// </summary>
        /// <param name="databaseLinkId">数据库主键</param>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        void ExecuteBySql(string databaseLinkId, string sql, object dbParameter = null);
        /// <summary>
        /// 根据数据库执行sql语句,查询数据->datatable
        /// </summary>
        /// <param name="databaseLinkId">数据库主键</param>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <returns></returns>
        DataTable FindTable(string databaseLinkId, string sql, object dbParameter = null);
        /// <summary>
        /// 根据数据库执行sql语句,查询数据->datatable（分页）
        /// </summary>
        /// <param name="databaseLinkId">数据库主键</param>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        DataTable FindTable(string databaseLinkId, string sql, object dbParameter, Pagination pagination);
        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="entity">数据库连接信息</param>
        /// <returns></returns>
        IRepository BeginTrans(string databaseLinkId);
        /// <summary>
        /// 根据指定数据库执行sql语句(事务)
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="dbParameter">参数</param>
        /// <param name="数据库连接">db</param>
        void ExecuteBySqlTrans(string sql, object dbParameter, IRepository db);
        #endregion
    }
}
