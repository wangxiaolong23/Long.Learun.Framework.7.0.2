using Learun.Util;
using System.Collections.Generic;

namespace Learun.Application.Base.SystemModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：功能模块
    /// </summary>
    public interface ModuleIBLL
    {
        #region 功能模块
        /// <summary>
        /// 功能列表
        /// </summary>
        /// <returns></returns>
        List<ModuleEntity> GetModuleList();
        /// <summary>
        /// 功能列表
        /// </summary>
        /// <returns></returns>
        ModuleEntity GetModuleByUrl(string url);
        /// <summary>
        /// 获取功能列表的树形数据
        /// </summary>
        /// <returns></returns>
        List<TreeModel> GetModuleTree();
        /// <summary>
        ///  获取功能列表的树形数据(带勾选框)
        /// </summary>
        /// <returns></returns>
        List<TreeModel> GetModuleCheckTree();
        /// <summary>
        /// 获取功能列表的树形数据(只有展开项)
        /// </summary>
        /// <returns></returns>
        List<TreeModel> GetExpendModuleTree();
        /// <summary>
        /// 根据父级主键获取数据
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <param name="parentId">父级主键</param>
        /// <returns></returns>
        List<ModuleEntity> GetModuleListByParentId(string keyword, string parentId);
        /// <summary>
        /// 功能实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        ModuleEntity GetModuleEntity(string keyValue);
        #endregion

        #region 模块按钮
        /// <summary>
        /// 获取按钮列表数据
        /// </summary>
        /// <param name="moduleId">模块Id</param>
        /// <returns></returns>
        List<ModuleButtonEntity> GetButtonListNoAuthorize(string moduleId);
        /// <summary>
        /// 获取按钮列表数据
        /// </summary>
        /// <param name="moduleId">模块Id</param>
        /// <returns></returns>
        List<ModuleButtonEntity> GetButtonList(string moduleId);
        /// <summary>
        /// 获取按钮列表数据
        /// </summary>
        /// <param name="url">功能模块地址</param>
        /// <returns></returns>
        List<ModuleButtonEntity> GetButtonListByUrl(string url);
        /// <summary>
        /// 获取按钮列表树形数据（基于功能模块）
        /// </summary>
        /// <returns></returns>
        List<TreeModel> GetButtonCheckTree();
        #endregion

        #region 模块视图
        /// <summary>
        /// 获取视图列表数据
        /// </summary>
        /// <param name="moduleId">模块Id</param>
        /// <returns></returns>
        List<ModuleColumnEntity> GetColumnList(string moduleId);
        
        /// <summary>
        /// 获取视图列表数据
        /// </summary>
        /// <param name="url">功能模块地址</param>
        /// <returns></returns>
        List<ModuleColumnEntity> GetColumnListByUrl(string url);
        /// <summary>
        /// 获取按钮列表树形数据（基于功能模块）
        /// </summary>
        /// <returns></returns>
        List<TreeModel> GetColumnCheckTree();
        #endregion

        #region 模块表单
        /// <summary>
        /// 获取表单字段数据
        /// </summary>
        /// <param name="moduleId">模块Id</param>
        /// <returns></returns>
        List<ModuleFormEntity> GetFormList(string moduleId);
        /// <summary>
        /// 获取表单字段数据
        /// </summary>
        /// <param name="url">功能模块地址</param>
        /// <returns></returns>
        List<ModuleFormEntity> GetFormListByUrl(string url);
        /// <summary>
        /// 获取表单字段树形数据（基于功能模块）
        /// </summary>
        /// <returns></returns>
        List<TreeModel> GetFormCheckTree();
        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除模块功能
        /// </summary>
        /// <param name="keyValue">主键值</param>
        bool Delete(string keyValue);
        /// <summary>
        /// 保存模块功能实体（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="moduleEntity">实体</param>
        /// <param name="moduleButtonEntitys">按钮列表</param>
        /// <param name="moduleColumnEntitys">视图列集合</param>
        /// <param name="moduleFormEntitys">视图列集合</param>
        void SaveEntity(string keyValue, ModuleEntity moduleEntity, List<ModuleButtonEntity> moduleButtonEntitys, List<ModuleColumnEntity> moduleColumnEntitys, List<ModuleFormEntity> moduleFormEntitys);
        #endregion

        #region 权限验证
        /// <summary>
        /// 验证当前访问的地址是否有权限
        /// </summary>
        /// <param name="url">访问地址</param>
        /// <returns></returns>
        bool FilterAuthorize(string url);
        #endregion
    }
}
