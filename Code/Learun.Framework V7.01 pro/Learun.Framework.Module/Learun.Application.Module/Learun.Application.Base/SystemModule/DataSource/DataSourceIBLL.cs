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
    /// 描 述：数据源
    /// </summary>
    public interface DataSourceIBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        IEnumerable<DataSourceEntity> GetPageList(Pagination pagination, string keyword);
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        IEnumerable<DataSourceEntity> GetList();
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="code">编号</param>
        /// <returns></returns>
        DataSourceEntity GetEntityByCode(string code);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据源
        /// </summary>
        /// <param name="keyValue">主键</param>
        void DeleteEntity(string keyValue);
        /// <summary>
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="dataSourceEntity">数据源实体</param>
        /// <returns></returns>
        bool SaveEntity(string keyValue, DataSourceEntity dataSourceEntity);
        #endregion

        #region 扩展方法
        /// <summary>
        /// 获取数据源的数据
        /// </summary>
        /// <param name="code">数据源编码</param>
        /// <param name="strWhere">sql查询条件语句</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        DataTable GetDataTable(string code, string strWhere, string queryJson = "{}");

        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="parentId">父级ID</param>
        /// <param name="Id">ID</param>
        /// <param name="showId">显示ID</param>
        /// <returns></returns>
        List<TreeModel> GetTree(string code, string parentId, string Id, string showId);
        /// <summary>
        /// 获取数据源的数据(分页)
        /// </summary>
        /// <param name="code">数据源编码</param>
        /// <param name="pagination">分页参数</param>
        /// <param name="strWhere">sql查询条件语句</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        DataTable GetDataTable(string code, Pagination pagination, string strWhere, string queryJson = "{}");
        /// <summary>
        /// 获取数据源列名
        /// </summary>
        /// <param name="code">数据源编码</param>
        /// <returns></returns>
        List<string> GetDataColName(string code);
        #endregion
    }
}
