using Learun.Util;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data;

namespace Learun.Application.Form
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.01
    /// 描 述：表单模板
    /// </summary>
    public interface FormSchemeIBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取自定义表单列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<FormSchemeInfoEntity> GetCustmerSchemeInfoList();
        /// <summary>
        /// 获取表单分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <param name="category">分类</param>
        /// <param name="type">表单类型0自定义表单,1自定义表单（OA），2系统表单</param>
        /// <returns></returns>
        IEnumerable<FormSchemeInfoEntity> GetSchemeInfoPageList(Pagination pagination, string keyword, string category, int type);
        /// <summary>
        /// 获取表单分页列表(用于系统表单)
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <param name="category">分类</param>
        /// <returns></returns>
        IEnumerable<FormSchemeInfoEntity> GetSchemeInfoPageList(Pagination pagination, string keyword, string category);
        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <returns></returns>
        IEnumerable<FormSchemeEntity> GetSchemePageList(Pagination pagination, string schemeInfoId);
        /// <summary>
        /// 获取模板基础信息的实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        FormSchemeInfoEntity GetSchemeInfoEntity(string keyValue);
        /// <summary>
        /// 获取模板的实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        FormSchemeEntity GetSchemeEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        void VirtualDelete(string keyValue);
        /// <summary>
        /// 保存模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="schemeInfoEntity">模板基础信息</param>
        /// <param name="schemeEntity">模板信息</param>
        void SaveEntity(string keyValue, FormSchemeInfoEntity schemeInfoEntity, FormSchemeEntity schemeEntity);
        /// <summary>
        /// 保存模板基础信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="schemeInfoEntity">模板基础信息</param>
        void SaveSchemeInfoEntity(string keyValue, FormSchemeInfoEntity schemeInfoEntity);
        /// <summary>
        /// 更新模板
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="schemeId">模板主键</param>
        void UpdateScheme(string schemeInfoId, string schemeId);
        /// <summary>
        /// 更新自定义表单模板状态
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="state">状态1启用0禁用</param>
        void UpdateState(string schemeInfoId, int state);
        #endregion

        #region 扩展方法
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        DataTable GetFormPageList(string schemeInfoId, Pagination pagination, string queryJson);
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        DataTable GetFormList(string schemeInfoId, string queryJson);
        /// <summary>
        /// 获取自定义表单数据
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Dictionary<string, DataTable> GetInstanceForm(string schemeInfoId, string keyValue);
        /// <summary>
        /// 获取自定义表单数据
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Dictionary<string, DataTable> GetInstanceForm(string schemeInfoId, string processIdName, string keyValue);
        /// <summary>
        /// 保存自定义表单数据
        /// </summary>
        /// <param name="schemeInfoId">表单模板主键</param>
        /// <param name="processIdName">流程关联字段名</param>
        /// <param name="keyValue">数据主键值</param>
        /// <param name="formData">自定义表单数据</param>
        void SaveInstanceForm(string schemeInfoId, string processIdName, string keyValue, string formData);

        /// <summary>
        /// 删除自定义表单数据
        /// </summary>
        /// <param name="schemeInfoId">表单模板主键</param>
        /// <param name="keyValue">数据主键值</param>
        void DeleteInstanceForm(string schemeInfoId, string keyValue);
        #endregion
    }
}
