using Learun.Util;
using System.Collections.Generic;

namespace Learun.Application.AppMagager
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.03.16
    /// 描 述：移动端功能管理
    /// </summary>
    public interface FunctionIBLL
    {

        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <param name="type">分类</param>
        /// <returns></returns>
        IEnumerable<FunctionEntity> GetPageList(Pagination pagination, string keyword, string type);
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        IEnumerable<FunctionEntity> GetList(UserInfo userInfo);
        /// <summary>
        /// 获取实体对象
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        FunctionEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取移动功能模板
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        FunctionSchemeEntity GetScheme(string keyValue);
        /// <summary>
        /// 获取树形移动功能列表
        /// </summary>
        /// <returns></returns>
        List<TreeModel> GetCheckTree();
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        void Delete(string keyValue);
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="functionEntity">功能信息</param>
        /// <param name="functionSchemeEntity">功能模板信息</param>
        void SaveEntity(string keyValue, FunctionEntity functionEntity, FunctionSchemeEntity functionSchemeEntity);
        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="keyValue">模板信息主键</param>
        /// <param name="state">状态1启用0禁用</param>
        void UpdateState(string keyValue, int state);
        #endregion
    }
}
