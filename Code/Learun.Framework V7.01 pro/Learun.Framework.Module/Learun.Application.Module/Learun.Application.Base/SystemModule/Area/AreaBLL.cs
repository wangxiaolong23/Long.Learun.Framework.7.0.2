using Learun.Cache.Base;
using Learun.Util;
using Learun.Cache.Factory;
using System;
using System.Collections.Generic;

namespace Learun.Application.Base.SystemModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.01
    /// 描 述：行政区域 redis 3号库
    /// </summary>
    public class AreaBLL : AreaIBLL
    {
        #region 属性
        private AreaService areaService = new AreaService();
        #endregion

        #region 缓存定义
        private ICache cache = CacheFactory.CaChe();
        private string cacheKey = "learun_adms_area_"; // +父级Id
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取区域列表数据
        /// </summary>
        /// <param name="parentId">父节点主键（0表示顶层）</param>
        /// <returns></returns>
        public List<AreaEntity> GetList(string parentId)
        {
            try
            {
                if (string.IsNullOrEmpty(parentId))
                {
                    parentId = "0";
                }
                List<AreaEntity> list = cache.Read<List<AreaEntity>>(cacheKey + parentId, CacheId.area);
                if (list == null)
                {
                    list = (List<AreaEntity>)areaService.GetList(parentId);
                    cache.Write<List<AreaEntity>>(cacheKey + parentId, list, CacheId.area);
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
                throw;
            }
        }

        /// <summary>
        /// 获取区域列表数据
        /// </summary>
        /// <param name="parentId">父节点主键（0表示顶层）</param>
        /// <param name="keyword">关键字查询（名称/编号）</param>
        /// <returns></returns>
        public List<AreaEntity> GetList(string parentId, string keyword)
        {
            try
            {
                List<AreaEntity> list = GetList(parentId);
                if (!string.IsNullOrEmpty(keyword))
                {
                    list = list.FindAll(t => t.F_AreaName.Contains(keyword) || t.F_AreaCode.Contains(keyword));
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
                throw;
            }
        }
        /// <summary>
        /// 区域实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public AreaEntity GetEntity(string keyValue)
        {
            try
            {
                return areaService.GetEntity(keyValue);
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
                throw;
            }
        }
        /// <summary>
        /// 获取区域数据树（某一级的）
        /// </summary>
        /// <param name="parentId">父级主键</param>
        /// <returns></returns>
        public List<TreeModel> GetTree(string parentId)
        {
            try
            {
                List<TreeModel> treeList = new List<TreeModel>();
                List<AreaEntity> list = GetList(parentId);

                foreach (var item in list)
                {
                    TreeModel node = new TreeModel();
                    node.id = item.F_AreaId;
                    node.text = item.F_AreaName;
                    node.value = item.F_AreaCode;
                    node.showcheck = false;
                    node.checkstate = 0;
                    node.hasChildren = GetList(item.F_AreaId).Count > 0 ? true : false;
                    node.isexpand = false;
                    node.complete = false;
                    treeList.Add(node);
                }
                return treeList;
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
                throw;
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除区域
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void VirtualDelete(string keyValue)
        {
            try
            {
                AreaEntity entity = areaService.GetEntity(keyValue);
                cache.Remove(cacheKey + entity.F_ParentId,CacheId.area);
                areaService.VirtualDelete(keyValue);
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
        /// 保存区域表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="areaEntity">区域实体</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, AreaEntity areaEntity)
        {
            try
            {
                cache.Remove(cacheKey + areaEntity.F_ParentId, CacheId.area);
                if (!string.IsNullOrEmpty(keyValue))
                {
                    AreaEntity entity = areaService.GetEntity(keyValue);
                    cache.Remove(cacheKey + entity.F_ParentId, CacheId.area);
                }
                if (areaEntity.F_ParentId != "0")
                {
                    AreaEntity entity = GetEntity(areaEntity.F_ParentId);
                    if (entity != null)
                    {
                        areaEntity.F_Layer = entity.F_Layer + 1;
                    }
                }
                else
                {
                    areaEntity.F_Layer = 1;
                }
                areaService.SaveEntity(keyValue, areaEntity);
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
