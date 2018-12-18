using Learun.Application.Base.AuthorizeModule;
using Learun.Cache.Base;
using Learun.Cache.Factory;
using Learun.Util;
using System;
using System.Collections.Generic;

namespace Learun.Application.Base.SystemModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：功能模块(缓存在0号库)
    /// 功能模块缓存结构
    /// </summary>
    public class ModuleBLL : ModuleIBLL
    {
        #region 属性
        private ModuleService moduleService = new ModuleService();
        private AuthorizeIBLL authorizeIBLL = new AuthorizeBLL();
        #endregion

        #region 缓存定义
        private ICache cache = CacheFactory.CaChe();

        private string cacheKey = "learun_adms_modules";
        private string cacheKeyBtn = "learun_adms_modules_Btn_";      // + 模块主键
        private string cacheKeyColumn = "learun_adms_modules_Column_";// + 模块主键
        private string cacheKeyForm = "learun_adms_modules_Form_";// + 模块主键
        #endregion

        #region 功能模块
        /// <summary>
        /// 功能列表
        /// </summary>
        /// <returns></returns>
        public List<ModuleEntity> GetModuleList()
        {
            try
            {
                List<ModuleEntity> list = cache.Read<List<ModuleEntity>>(cacheKey, CacheId.module);
                if (list == null)
                {
                    list = (List<ModuleEntity>)moduleService.GetList();
                    cache.Write<List<ModuleEntity>>(cacheKey, list, CacheId.module);
                }

                UserInfo userInfo = LoginUserInfo.Get();
                /*关联权限*/
                if (!userInfo.isSystem)
                {
                    string objectIds = userInfo.userId + (string.IsNullOrEmpty(userInfo.roleIds) ? "" : ("," + userInfo.roleIds));
                    List<string> itemIdList = authorizeIBLL.GetItemIdListByobjectIds(objectIds, 1);
                    list = list.FindAll(t => itemIdList.IndexOf(t.F_ModuleId) >= 0);
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
        /// 功能列表
        /// </summary>
        /// <returns></returns>
        public ModuleEntity GetModuleByUrl(string url)
        {
            try
            {
                List<ModuleEntity> list = GetModuleList();
                return list.Find(t => t.F_UrlAddress == url);
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
        /// 获取功能列表的树形数据
        /// </summary>
        /// <returns></returns>
        public List<TreeModel> GetModuleTree()
        {
            List<ModuleEntity> modulelist = GetModuleList();
            List<TreeModel> treeList = new List<TreeModel>();
            foreach (var item in modulelist) {
                TreeModel node = new TreeModel();
                node.id = item.F_ModuleId;
                node.text = item.F_FullName;
                node.value = item.F_EnCode;
                node.showcheck = false;
                node.checkstate = 0;
                node.isexpand = (item.F_AllowExpand == 1);
                node.icon = item.F_Icon;
                node.parentId = item.F_ParentId;
                treeList.Add(node);
            }
            return treeList.ToTree();
        }
        /// <summary>
        ///  获取功能列表的树形数据(带勾选框)
        /// </summary>
        /// <returns></returns>
        public List<TreeModel> GetModuleCheckTree()
        {
            List<ModuleEntity> modulelist = GetModuleList();
            List<TreeModel> treeList = new List<TreeModel>();
            foreach (var item in modulelist)
            {
                TreeModel node = new TreeModel();
                node.id = item.F_ModuleId;
                node.text = item.F_FullName;
                node.value = item.F_EnCode;
                node.showcheck = true;
                node.checkstate = 0;
                node.isexpand = true;
                node.icon = item.F_Icon;
                node.parentId = item.F_ParentId;
                treeList.Add(node);
            }
            return treeList.ToTree();
        }
        /// <summary>
        /// 获取功能列表的树形数据(只有展开项)
        /// </summary>
        /// <returns></returns>
        public List<TreeModel> GetExpendModuleTree()
        {
            List<ModuleEntity> modulelist = GetModuleList();
            List<TreeModel> treeList = new List<TreeModel>();
            foreach (var item in modulelist)
            {
                if (item.F_Target == "expand")
                {
                    TreeModel node = new TreeModel();
                    node.id = item.F_ModuleId;
                    node.text = item.F_FullName;
                    node.value = item.F_EnCode;
                    node.showcheck = false;
                    node.checkstate = 0;
                    node.isexpand = true;
                    node.icon = item.F_Icon;
                    node.parentId = item.F_ParentId;
                    treeList.Add(node);
                }
            }
            return treeList.ToTree();
        }
        /// <summary>
        /// 根据父级主键获取数据
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <param name="parentId">父级主键</param>
        /// <returns></returns>
        public List<ModuleEntity> GetModuleListByParentId(string keyword, string parentId)
        {
            try
            {
                List<ModuleEntity> list = (List<ModuleEntity>)GetModuleList();
                list = list.FindAll(t => t.F_ParentId == parentId);
                if (!string.IsNullOrEmpty(keyword))
                {
                    list = list.FindAll(t => t.F_FullName.Contains(keyword) || t.F_EnCode.Contains(keyword));
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
        /// 功能实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ModuleEntity GetModuleEntity(string keyValue)
        {
            try
            {
                return moduleService.GetEntity(keyValue);
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

        #region 模块按钮
        /// <summary>
        /// 获取按钮列表数据
        /// </summary>
        /// <param name="moduleId">模块Id</param>
        /// <returns></returns>
        public List<ModuleButtonEntity> GetButtonListNoAuthorize(string moduleId)
        {
            try
            {
                List<ModuleButtonEntity> list = cache.Read<List<ModuleButtonEntity>>(cacheKeyBtn + moduleId, CacheId.module);
                if (list == null)
                {
                    list = (List<ModuleButtonEntity>)moduleService.GetButtonList(moduleId);
                    cache.Write<List<ModuleButtonEntity>>(cacheKeyBtn + moduleId, list, CacheId.module);
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
        /// 获取按钮列表数据
        /// </summary>
        /// <param name="moduleId">模块Id</param>
        /// <returns></returns>
        public List<ModuleButtonEntity> GetButtonList(string moduleId)
        {
            try
            {
                List<ModuleButtonEntity> list = cache.Read<List<ModuleButtonEntity>>(cacheKeyBtn + moduleId, CacheId.module);
                if (list == null) {
                   list = (List<ModuleButtonEntity>)moduleService.GetButtonList(moduleId);
                   cache.Write<List<ModuleButtonEntity>>(cacheKeyBtn + moduleId, list, CacheId.module);
                }
                UserInfo userInfo = LoginUserInfo.Get();
                /*关联权限*/
                if (!userInfo.isSystem)
                {
                    string objectIds = userInfo.userId + (string.IsNullOrEmpty(userInfo.roleIds) ? "" : ("," + userInfo.roleIds));
                    List<string> itemIdList = authorizeIBLL.GetItemIdListByobjectIds(objectIds, 2);
                    list = list.FindAll(t => itemIdList.IndexOf(t.F_ModuleButtonId) >= 0);
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
        /// 获取按钮列表数据
        /// </summary>
        /// <param name="url">功能模块地址</param>
        /// <returns></returns>
        public List<ModuleButtonEntity> GetButtonListByUrl(string url)
        {
            try
            {
                ModuleEntity moduleEntity = GetModuleByUrl(url);
                if (moduleEntity == null)
                {
                    return new List<ModuleButtonEntity>();
                }
                return GetButtonList(moduleEntity.F_ModuleId);
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
        /// 获取按钮列表树形数据（基于功能模块）
        /// </summary>
        /// <returns></returns>
        public List<TreeModel> GetButtonCheckTree()
        {
            List<ModuleEntity> modulelist = GetModuleList();
            List<TreeModel> treeList = new List<TreeModel>();
            foreach (var module in modulelist)
            {
                TreeModel node = new TreeModel();
                node.id = module.F_ModuleId + "_learun_moduleId";
                node.text = module.F_FullName;
                node.value = module.F_EnCode;
                node.showcheck = true;
                node.checkstate = 0;
                node.isexpand = true;
                node.icon = module.F_Icon;
                node.parentId = module.F_ParentId + "_learun_moduleId";
                if (module.F_Target != "expand")
                {
                    List<ModuleButtonEntity> buttonList = GetButtonList(module.F_ModuleId);
                    if (buttonList.Count > 0)
                    {
                        treeList.Add(node);
                    }
                    foreach (var button in buttonList)
                    {
                        TreeModel buttonNode = new TreeModel();
                        buttonNode.id = button.F_ModuleButtonId;
                        buttonNode.text = button.F_FullName;
                        buttonNode.value = button.F_EnCode;
                        buttonNode.showcheck = true;
                        buttonNode.checkstate = 0;
                        buttonNode.isexpand = true;
                        buttonNode.icon = "fa fa-wrench";
                        buttonNode.parentId = (button.F_ParentId == "0" ? button.F_ModuleId + "_learun_moduleId" : button.F_ParentId);
                        treeList.Add(buttonNode);
                    };
                }
                else
                {
                    treeList.Add(node);
                }
            }
            return treeList.ToTree();
        }
        #endregion

        #region 模块视图
        /// <summary>
        /// 获取视图列表数据
        /// </summary>
        /// <param name="moduleId">模块Id</param>
        /// <returns></returns>
        public List<ModuleColumnEntity> GetColumnList(string moduleId)
        {
            try
            {
                List<ModuleColumnEntity> list = cache.Read<List<ModuleColumnEntity>>(cacheKeyColumn + moduleId, CacheId.module);
                if (list == null)
                {
                    list = (List<ModuleColumnEntity>)moduleService.GetColumnList(moduleId);
                    cache.Write<List<ModuleColumnEntity>>(cacheKeyColumn + moduleId, list, CacheId.module);
                }
                UserInfo userInfo = LoginUserInfo.Get();
                /*关联权限*/
                if (!userInfo.isSystem)
                {
                    string objectIds = userInfo.userId + (string.IsNullOrEmpty(userInfo.roleIds) ? "" : ("," + userInfo.roleIds));
                    List<string> itemIdList = authorizeIBLL.GetItemIdListByobjectIds(objectIds, 3);
                    list = list.FindAll(t => itemIdList.IndexOf(t.F_ModuleColumnId) >= 0);
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
        /// 获取视图列表数据
        /// </summary>
        /// <param name="url">功能模块地址</param>
        /// <returns></returns>
        public List<ModuleColumnEntity> GetColumnListByUrl(string url)
        {
            try
            {
                ModuleEntity moduleEntity = GetModuleByUrl(url);
                if (moduleEntity == null)
                {
                    return new List<ModuleColumnEntity>();
                }
                return GetColumnList(moduleEntity.F_ModuleId);
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
        /// 获取按钮列表树形数据（基于功能模块）
        /// </summary>
        /// <returns></returns>
        public List<TreeModel> GetColumnCheckTree()
        {
            List<ModuleEntity> modulelist = GetModuleList();
            List<TreeModel> treeList = new List<TreeModel>();
            foreach (var module in modulelist)
            {
                TreeModel node = new TreeModel();
                node.id = module.F_ModuleId + "_learun_moduleId";
                node.text = module.F_FullName;
                node.value = module.F_EnCode;
                node.showcheck = true;
                node.checkstate = 0;
                node.isexpand = true;
                node.icon = module.F_Icon;
                node.parentId = module.F_ParentId + "_learun_moduleId";

                if (module.F_Target != "expand")
                {
                    List<ModuleColumnEntity> columnList = GetColumnList(module.F_ModuleId);
                    if (columnList.Count > 0)
                    {
                        treeList.Add(node);
                    }
                    foreach (var column in columnList)
                    {
                        TreeModel columnNode = new TreeModel();
                        columnNode.id = column.F_ModuleColumnId;
                        columnNode.text = column.F_FullName;
                        columnNode.value = column.F_EnCode;
                        columnNode.showcheck = true;
                        columnNode.checkstate = 0;
                        columnNode.isexpand = true;
                        columnNode.icon = "fa fa-filter";
                        columnNode.parentId = column.F_ModuleId + "_learun_moduleId";
                        treeList.Add(columnNode);
                    };
                }
                else
                {
                    treeList.Add(node);
                }
            }
            return treeList.ToTree();
        }
        #endregion

        #region 模块表单
        /// <summary>
        /// 获取表单字段数据
        /// </summary>
        /// <param name="moduleId">模块Id</param>
        /// <returns></returns>
        public List<ModuleFormEntity> GetFormList(string moduleId)
        {
            try
            {
                List<ModuleFormEntity> list = cache.Read<List<ModuleFormEntity>>(cacheKeyForm + moduleId, CacheId.module);
                if (list == null)
                {
                    list = (List<ModuleFormEntity>)moduleService.GetFormList(moduleId);
                    cache.Write<List<ModuleFormEntity>>(cacheKeyForm + moduleId, list, CacheId.module);
                }
                UserInfo userInfo = LoginUserInfo.Get();
                /*关联权限*/
                if (!userInfo.isSystem)
                {
                    string objectIds = userInfo.userId + (string.IsNullOrEmpty(userInfo.roleIds) ? "" : ("," + userInfo.roleIds));
                    List<string> itemIdList = authorizeIBLL.GetItemIdListByobjectIds(objectIds, 3);
                    list = list.FindAll(t => itemIdList.IndexOf(t.F_ModuleFormId) >= 0);
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
        /// 获取表单字段数据
        /// </summary>
        /// <param name="url">功能模块地址</param>
        /// <returns></returns>
        public List<ModuleFormEntity> GetFormListByUrl(string url)
        {
            try
            {
                ModuleEntity moduleEntity = GetModuleByUrl(url);
                if (moduleEntity == null)
                {
                    return new List<ModuleFormEntity>();
                }
                return GetFormList(moduleEntity.F_ModuleId);
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
        /// 获取表单字段树形数据（基于功能模块）
        /// </summary>
        /// <returns></returns>
        public List<TreeModel> GetFormCheckTree()
        {
            List<ModuleEntity> modulelist = GetModuleList();
            List<TreeModel> treeList = new List<TreeModel>();
            foreach (var module in modulelist)
            {
                TreeModel node = new TreeModel();
                node.id = module.F_ModuleId + "_learun_moduleId";
                node.text = module.F_FullName;
                node.value = module.F_EnCode;
                node.showcheck = true;
                node.checkstate = 0;
                node.isexpand = true;
                node.icon = module.F_Icon;
                node.parentId = module.F_ParentId + "_learun_moduleId";

                if (module.F_Target != "expand")
                {
                    List<ModuleFormEntity> columnList = GetFormList(module.F_ModuleId);
                    if (columnList.Count > 0)
                    {
                        treeList.Add(node);
                    }
                    foreach (var column in columnList)
                    {
                        TreeModel columnNode = new TreeModel();
                        columnNode.id = column.F_ModuleFormId;
                        columnNode.text = column.F_FullName;
                        columnNode.value = column.F_EnCode;
                        columnNode.showcheck = true;
                        columnNode.checkstate = 0;
                        columnNode.isexpand = true;
                        columnNode.icon = "fa fa-filter";
                        columnNode.parentId = column.F_ModuleId + "_learun_moduleId";
                        treeList.Add(columnNode);
                    };
                }
                else
                {
                    treeList.Add(node);
                }
            }
            return treeList.ToTree();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除模块功能
        /// </summary>
        /// <param name="keyValue">主键值</param>
        public bool Delete(string keyValue)
        {
            try
            {
                List<ModuleEntity> list = GetModuleListByParentId("", keyValue);
                if (list.Count > 0)
                {
                    return false;
                }
                moduleService.Delete(keyValue);
                cache.Remove(cacheKey, CacheId.module);
                cache.Remove(cacheKeyBtn + keyValue, CacheId.module);
                cache.Remove(cacheKeyColumn + keyValue, CacheId.module);
                cache.Remove(cacheKeyForm + keyValue, CacheId.module);
                return true;
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
        /// 保存模块功能实体（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="moduleEntity">实体</param>
        /// <param name="moduleButtonEntitys">按钮列表</param>
        /// <param name="moduleColumnEntitys">视图列集合</param>
        /// <param name="moduleFormEntitys">表单字段集合</param>
        public void SaveEntity(string keyValue, ModuleEntity moduleEntity, List<ModuleButtonEntity> moduleButtonEntitys, List<ModuleColumnEntity> moduleColumnEntitys, List<ModuleFormEntity> moduleFormEntitys)
        {
            try
            {
                moduleService.SaveEntity(keyValue, moduleEntity, moduleButtonEntitys, moduleColumnEntitys, moduleFormEntitys);
                /*移除缓存信息*/
                cache.Remove(cacheKey, CacheId.module);
                if (!string.IsNullOrEmpty(keyValue))
                {
                    cache.Remove(cacheKeyBtn + keyValue, CacheId.module);
                    cache.Remove(cacheKeyColumn + keyValue, CacheId.module);
                    cache.Remove(cacheKeyForm + keyValue, CacheId.module);
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
        #endregion

        #region 权限验证
        /// <summary>
        /// 验证当前访问的地址是否有权限
        /// </summary>
        /// <param name="url">访问地址</param>
        /// <returns></returns>
        public bool FilterAuthorize(string url)
        {
            try
            {
                List<ModuleEntity> list = GetModuleList();
                // 验证是否是功能页面
                var modulelist = list.FindAll(t => t.F_UrlAddress == url);
                if (modulelist.Count > 0)
                {
                    return true;
                }
                // 是否是功能按钮的
                foreach (var item in list)
                {
                    List<ModuleButtonEntity> buttonList = GetButtonList(item.F_ModuleId);
                    var buttonList2 = buttonList.FindAll(t => t.F_ActionAddress == url);
                    if (buttonList2.Count > 0)
                    {
                        return true;
                    }
                }
                return false;
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
