using Learun.Application.Organization;
using Learun.Util;
using System.Collections.Generic;

namespace Learun.Application.Base.SystemModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：自定义查询
    /// </summary>
    public interface CustmerQueryIBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取自定义查询（公共）分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        IEnumerable<CustmerQueryEntity> GetPageList(Pagination pagination, string keyword);
        /// <summary>
        /// 获取自定义查询条件
        /// </summary>
        /// <param name="moduleUrl">访问的功能链接地址</param>
        /// <param name="userId">用户ID（用户ID为null表示公共）</param>
        /// <returns></returns>
        List<CustmerQueryEntity> GetList(string moduleUrl, string userId);
        /// <summary>
        /// 获取自定义查询条件(用于具体使用)
        /// </summary>
        /// <param name="moduleUrl"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        List<CustmerQueryEntity> GetCustmerList(string moduleUrl, string userId);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除自定义查询条件
        /// </summary>
        /// <param name="keyValue">主键</param>
        void DeleteEntity(string keyValue);
        /// <summary>
        /// 保存自定义查询（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="departmentEntity">部门实体</param>
        /// <returns></returns>
        void SaveEntity(string keyValue, CustmerQueryEntity custmerQueryEntity);
        #endregion

        #region 扩展方法
        /// <summary>
        /// 将条件转化成sql语句
        /// </summary>
        /// <param name="queryJson">查询条件</param>
        /// <param name="formula">公式，没有就默认采用and连接</param>
        /// <param name="userEntity">账号信息</param>
        /// <returns></returns>
        string ConditionToSql(string queryJson, string formula, UserEntity userEntity);
        #endregion
    }
}
