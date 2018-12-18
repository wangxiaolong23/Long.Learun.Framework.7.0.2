using Learun.Cache.Base;
using Learun.Cache.Factory;
using Learun.Util;
using System;
using System.Collections.Generic;

namespace Learun.Application.Organization
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：公司管理
    /// </summary>
    public class CompanyBLL : CompanyIBLL
    {
        #region 属性
        private CompanyService companyService = new CompanyService();        
        #endregion

        #region 缓存定义
        private ICache cache = CacheFactory.CaChe();
        private string cacheKey = "learun_adms_company";
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取公司列表数据
        /// </summary>
        /// <returns></returns>
        public List<CompanyEntity> GetList()
        {
            try
            {
                List<CompanyEntity> list = cache.Read<List<CompanyEntity>>(cacheKey, CacheId.company);
                if (list == null)
                {
                    list = (List<CompanyEntity>)companyService.GetList();
                    cache.Write<List<CompanyEntity>>(cacheKey, list, CacheId.company);
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
        /// 获取公司列表信息（微信用）
        /// </summary>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        public List<CompanyEntity> GetWeChatList(string keyWord)
        {
            try
            {
                return (List<CompanyEntity>)companyService.GetWeChatList(keyWord);
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
        /// 获取微信人员树形数据
        /// </summary>
        /// <param name="parentId">父级id</param>
        /// <returns></returns>
        public List<TreeModel> GetWeChatTree(string parentId)
        {
            try
            {
                List<CompanyEntity> list = GetWeChatList("");
                List<TreeModel> treeList = new List<TreeModel>();
                foreach (var item in list)
                {
                    TreeModel node = new TreeModel
                    {
                        id = item.F_CompanyId,
                        text = item.F_FullName,
                        value = item.F_CompanyId,
                        showcheck = false,
                        checkstate = 0,
                        isexpand = true,
                        parentId = item.F_ParentId
                    };
                    treeList.Add(node);
                }
                return treeList.ToTree(parentId);
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
        /// 获取公司映射数据
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,CompanyModel> GetModelMap() {
            try
            {
                Dictionary<string, CompanyModel> dic = cache.Read<Dictionary<string, CompanyModel>>(cacheKey + "dic", CacheId.company);
                if (dic == null) {
                    dic = new Dictionary<string, CompanyModel>();
                    List<CompanyEntity> list = GetList();
                    foreach (var item in list)
                    {
                        CompanyModel model = new CompanyModel
                        {
                            parentId = item.F_ParentId,
                            name = item.F_FullName
                        };
                        dic.Add(item.F_CompanyId, model);
                        cache.Write(cacheKey + "dic", dic, CacheId.company);
                    }
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
        /// 获取公司列表数据
        /// </summary>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        public List<CompanyEntity> GetList(string keyWord)
        {
            try
            {
                List<CompanyEntity> list = GetList();
                if (!string.IsNullOrEmpty(keyWord)) {
                    list = list.FindAll(t => t.F_FullName.Contains(keyWord) || t.F_EnCode.Contains(keyWord) || t.F_ShortName.Contains(keyWord));
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
        /// 获取公司信息实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public CompanyEntity GetEntity(string keyValue)
        {
            try
            {
                List<CompanyEntity> list = GetList();
                CompanyEntity entity = list.Find(t => t.F_CompanyId.Equals(keyValue));
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
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <param name="parentId">父级id</param>
        /// <returns></returns>
        public List<TreeModel> GetTree(string parentId) {
            try
            {
                List<CompanyEntity> list = GetList();
                List<TreeModel> treeList = new List<TreeModel>();
                foreach (var item in list) {
                    TreeModel node = new TreeModel
                    {
                        id = item.F_CompanyId,
                        text = item.F_FullName,
                        value = item.F_CompanyId,
                        showcheck = false,
                        checkstate = 0,
                        isexpand = true,
                        parentId = item.F_ParentId
                    };
                    treeList.Add(node);
                }
                return treeList.ToTree(parentId);
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
        /// 获取公司本身和子公司的id
        /// </summary>
        /// <param name="parentId">父级ID</param>
        /// <returns></returns>
        public List<string> GetSubNodes(string parentId)
        {
            try
            {
                if (string.IsNullOrEmpty(parentId)) {
                    return new List<string>();
                }
                List<string> res = new List<string>();
                res.Add(parentId);
                List<TreeModel> list = GetTree(parentId);
                GetSubNodes(list, res);
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
        /// <summary>
        /// 遍历树形数据获取全部子节点ID
        /// </summary>
        /// <param name="list">树形数据列表</param>
        /// <param name="ourList">输出数据列表</param>
        private void GetSubNodes(List<TreeModel> list, List<string> ourList) {
            foreach (var item in list) {
                ourList.Add(item.id);
                if (item.hasChildren) {
                    GetSubNodes(item.ChildNodes, ourList);
                }
            }
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除公司信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void VirtualDelete(string keyValue)
        {
            try
            {
                cache.Remove(cacheKey, CacheId.company);
                cache.Remove(cacheKey + "dic", CacheId.company);

                companyService.VirtualDelete(keyValue);
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
        /// 保存公司信息（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="companyEntity">公司实体</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, CompanyEntity companyEntity)
        {
            try
            {
                cache.Remove(cacheKey, CacheId.company);
                cache.Remove(cacheKey + "dic", CacheId.company);

                companyService.SaveEntity(keyValue, companyEntity);
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
