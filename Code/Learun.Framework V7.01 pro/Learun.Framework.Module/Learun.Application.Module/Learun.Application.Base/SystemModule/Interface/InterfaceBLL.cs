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
    /// 日 期：2017.04.01
    /// 描 述：接口管理
    /// </summary>
    public class InterfaceBLL : InterfaceIBLL
    {
        private InterfaceService interfaceService = new InterfaceService();
        private ICache cache = CacheFactory.CaChe();
        private string cacheKey = "learun_adms_interface";

        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public List<InterfaceEntity> GetList()
        {
            try
            {
                List<InterfaceEntity> list = cache.Read<List<InterfaceEntity>>(cacheKey, CacheId.Interface);
                if (list == null)
                {
                    list = (List<InterfaceEntity>)interfaceService.GetList();
                    cache.Write<List<InterfaceEntity>>(cacheKey, list, CacheId.Interface);
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
        /// 获取树形数据列表
        /// </summary>
        /// <returns></returns>
        public List<TreeModel> GetTree()
        {
            try
            {
                List<InterfaceEntity> list = GetList();
                List<TreeModel> treeList = new List<TreeModel>();
                foreach (var item in list)
                {
                    TreeModel node = new TreeModel();
                    node.id = item.F_Id;
                    node.text = item.F_Name;
                    node.value = item.F_Name;
                    node.showcheck = false;
                    node.checkstate = 0;
                    node.isexpand = true;
                    node.parentId = "0";
                    treeList.Add(node);
                }
                return treeList.ToTree();
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
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        public List<InterfaceEntity> GetPageList(Pagination pagination, string keyword)
        {
            try
            {
                List<InterfaceEntity> list = GetList();
                if (!string.IsNullOrEmpty(keyword))
                {
                    list = list.FindAll(t => t.F_Name.ContainsEx(keyword) || t.F_Address.ContainsEx(keyword));
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
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public InterfaceEntity GetEntity(string keyValue)
        {
            try
            {
                List<InterfaceEntity> list = GetList();
                return list.Find(t => t.F_Id == keyValue);
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
        /// 获取实体数据
        /// </summary>
        /// <param name="url">接口地址</param>
        /// <returns></returns>
        public InterfaceEntity GetEntityByUrl(string url)
        {
            try
            {
                List<InterfaceEntity> list = GetList();
                return list.Find(t => t.F_Address == url);
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
        /// 虚拟删除分类数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                cache.Remove(cacheKey, CacheId.Interface);
                interfaceService.DeleteEntity(keyValue);
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
        /// 保存分类数据实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        public void SaveEntity(string keyValue, InterfaceEntity entity)
        {
            try
            {
                cache.Remove(cacheKey, CacheId.Interface);
                interfaceService.SaveEntity(keyValue, entity);
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
