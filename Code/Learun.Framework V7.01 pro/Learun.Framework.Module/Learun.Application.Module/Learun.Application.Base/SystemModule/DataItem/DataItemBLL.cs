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
    /// 描 述：数据字典管理
    /// </summary>
    public class DataItemBLL : DataItemIBLL
    {
        #region 属性
        private DataItemService dataItemService = new DataItemService();
        #endregion

        #region 缓存定义
        private ICache cache = CacheFactory.CaChe();
        private string cacheKeyClassify = "learun_adms_dataItem_classify";// 字典分类
        private string cacheKeyDetail = "learun_adms_dataItem_detail_";   // 字典分类明显+分类编码
        #endregion

        #region 数据字典分类
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <returns></returns>
        public List<DataItemEntity> GetClassifyList()
        {
            try
            {
                List<DataItemEntity> list = cache.Read<List<DataItemEntity>>(cacheKeyClassify, CacheId.dataItem);
                if (list == null)
                {
                    list = (List<DataItemEntity>)dataItemService.GetClassifyList();
                    cache.Write<List<DataItemEntity>>(cacheKeyClassify, list, CacheId.dataItem);
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
        /// 分类列表
        /// </summary>
        /// <param name="keyword">关键词（名称/编码）</param>
        /// <param name="enabledMark">是否只取有效</param>
        /// <returns></returns>
        public List<DataItemEntity> GetClassifyList(string keyword, bool enabledMark = true)
        {
            try
            {
                List<DataItemEntity> list = GetClassifyList();
                if (enabledMark)
                {
                    list = list.FindAll(t => t.F_EnabledMark.Equals(1));
                }
                if (!string.IsNullOrEmpty(keyword))
                {
                    list = list.FindAll(t => t.F_ItemName.Contains(keyword) || t.F_ItemCode.Contains(keyword));
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
        /// 获取分类树形数据
        /// </summary>
        /// <returns></returns>
        public List<TreeModel> GetClassifyTree()
        {
            try
            {
                List<DataItemEntity> classifyList = GetClassifyList();
                List<TreeModel> treeList = new List<TreeModel>();
                foreach (var item in classifyList)
                {
                    TreeModel node = new TreeModel();
                    node.id = item.F_ItemId;
                    node.text = item.F_ItemName;
                    node.value = item.F_ItemCode;
                    node.showcheck = false;
                    node.checkstate = 0;
                    node.isexpand = true;
                    node.parentId = item.F_ParentId;
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
        /// 判断分类编号是否重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="itemCode">编码</param>
        /// <returns></returns>
        public bool ExistItemCode(string keyValue, string itemCode) {
            try
            {
                bool res = false;
                List<DataItemEntity> list = GetClassifyList();
                if (string.IsNullOrEmpty(keyValue))
                {
                    res = list.FindAll(t => t.F_ItemCode.Equals(itemCode)).Count <= 0;
                }
                else
                {
                    res = list.FindAll(t => t.F_ItemCode.Equals(itemCode) && !t.F_ItemId.Equals(keyValue)).Count <= 0;
                }

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
        /// 判断分类名称是否重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="itemName">名称</param>
        /// <returns></returns>
        public bool ExistItemName(string keyValue, string itemName)
        {
            try
            {
                bool res = false;
                List<DataItemEntity> list = GetClassifyList();
                if (string.IsNullOrEmpty(keyValue))
                {
                    res = list.FindAll(t => t.F_ItemName.Equals(itemName)).Count <= 0;
                }
                else
                {
                    res = list.FindAll(t => t.F_ItemName.Equals(itemName) && !t.F_ItemId.Equals(keyValue)).Count <= 0;
                }

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
        /// 保存分类数据实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        public void SaveClassifyEntity(string keyValue, DataItemEntity entity) {
            try
            {
                dataItemService.SaveClassifyEntity(keyValue, entity);
                cache.Remove(cacheKeyClassify, CacheId.dataItem);
                cache.Remove(cacheKeyDetail + "dic", CacheId.dataItem);
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
        /// 虚拟删除分类数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void VirtualDeleteClassify(string keyValue)
        {
            try
            {
                dataItemService.VirtualDeleteClassify(keyValue);
                cache.Remove(cacheKeyClassify, CacheId.dataItem);
                cache.Remove(cacheKeyDetail + "dic", CacheId.dataItem);
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
        /// 通过编号获取字典分类实体
        /// </summary>
        /// <param name="itemCode">编码</param>
        /// <returns></returns>
        public DataItemEntity GetClassifyEntityByCode(string itemCode)
        {
            try
            {
                List<DataItemEntity> list = GetClassifyList();
                return list.Find(t => t.F_ItemCode.Equals(itemCode));
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

        #region 字典明细
        /// <summary>
        /// 获取数据字典明显
        /// </summary>
        /// <param name="itemCode">分类编码</param>
        /// <returns></returns>
        public List<DataItemDetailEntity> GetDetailList(string itemCode)
        {
            try
            {
                List<DataItemDetailEntity> list = cache.Read<List<DataItemDetailEntity>>(cacheKeyDetail + itemCode, CacheId.dataItem);
                if (list == null)
                {
                    list = (List<DataItemDetailEntity>)dataItemService.GetDetailList(itemCode);
                    cache.Write<List<DataItemDetailEntity>>(cacheKeyDetail + itemCode, list, CacheId.dataItem);
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
        /// 获取数据字典详细映射数据
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Dictionary<string,DataItemModel>> GetModelMap()
        {
            try
            {
                Dictionary<string, Dictionary<string,DataItemModel>> dic = cache.Read<Dictionary<string, Dictionary<string,DataItemModel>>>(cacheKeyDetail + "dic", CacheId.dataItem);
                if (dic == null) {
                    dic = new Dictionary<string, Dictionary<string,DataItemModel>>();
                    var list = GetClassifyList();
                    foreach (var item in list) {
                        var detailList = GetDetailList(item.F_ItemCode);
                        if (!dic.ContainsKey(item.F_ItemCode)) {
                            dic.Add(item.F_ItemCode,new Dictionary<string,DataItemModel>());
                        }
                        foreach (var detailItem in detailList) {
                            dic[item.F_ItemCode].Add(detailItem.F_ItemDetailId, new DataItemModel()
                            {
                                parentId = detailItem.F_ParentId,
                                text = detailItem.F_ItemName,
                                value = detailItem.F_ItemValue
                            });
                        }
                    }
                    cache.Write(cacheKeyDetail + "dic", dic, CacheId.dataItem);
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
        /// 获取数据字典明显
        /// </summary>
        /// <param name="itemCode">分类编码</param>
        /// <param name="keyword">关键词（名称/值）</param>
        /// <returns></returns>
        public List<DataItemDetailEntity> GetDetailList(string itemCode, string keyword)
        {
            try
            {
                List<DataItemDetailEntity> list = GetDetailList(itemCode);
                if (!string.IsNullOrEmpty(keyword)) {
                    list = list.FindAll(t => t.F_ItemName.Contains(keyword) || t.F_ItemValue.Contains(keyword));
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
        /// 获取数据字典明显
        /// </summary>
        /// <param name="itemCode">分类编号</param>
        /// <param name="parentId">父级主键</param>
        /// <returns></returns>
        public List<DataItemDetailEntity> GetDetailListByParentId(string itemCode, string parentId)
        {
            try
            {
                List<DataItemDetailEntity> list = GetDetailList(itemCode);
                if (!string.IsNullOrEmpty(parentId))
                {
                    list = list.FindAll(t => t.F_ParentId.ContainsEx(parentId));
                }
                else
                {
                    list = list.FindAll(t => t.F_ParentId.ContainsEx("0"));
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
        /// 获取字典明细树形数据
        /// </summary>
        /// <param name="itemCode">分类编号</param>
        /// <returns></returns>
        public List<TreeModel> GetDetailTree(string itemCode)
        {
            try
            {
                List<DataItemDetailEntity> list = GetDetailList(itemCode);
                List<TreeModel> treeList = new List<TreeModel>();
                foreach (var item in list) {
                    TreeModel node = new TreeModel();
                    node.id = item.F_ItemDetailId;
                    node.text = item.F_ItemName;
                    node.value = item.F_ItemValue;
                    node.showcheck = false;
                    node.checkstate = 0;
                    node.isexpand = true;
                    node.parentId = item.F_ParentId == null ? "0" : item.F_ParentId;
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
        /// 项目值不能重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="itemValue">项目值</param>
        /// <param name="itemCode">分类编码</param>
        /// <returns></returns>
        public bool ExistDetailItemValue(string keyValue, string itemValue, string itemCode)
        {
            try
            {
                bool res = false;
                List<DataItemDetailEntity> list = GetDetailList(itemCode);

                if (string.IsNullOrEmpty(keyValue))
                {
                    res = list.FindAll(t => t.F_ItemValue.Equals(itemValue)).Count <= 0;
                }
                else
                {
                    res = list.FindAll(t => t.F_ItemValue.Equals(itemValue) && !t.F_ItemDetailId.Equals(keyValue)).Count <= 0;
                }
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
        /// 项目名不能重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="itemName">项目名</param>
        /// <param name="itemCode">分类编码</param>
        /// <returns></returns>
        public bool ExistDetailItemName(string keyValue, string itemName, string itemCode)
        {
            try
            {
                bool res = false;
                List<DataItemDetailEntity> list = GetDetailList(itemCode);

                if (string.IsNullOrEmpty(keyValue))
                {
                    res = list.FindAll(t => t.F_ItemName.Equals(itemName)).Count <= 0;
                }
                else
                {
                    res = list.FindAll(t => t.F_ItemName.Equals(itemName) && !t.F_ItemDetailId.Equals(keyValue)).Count <= 0;
                }
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
        /// 保存明细数据实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        public void SaveDetailEntity(string keyValue, DataItemDetailEntity entity)
        {
            try
            {
                List<DataItemEntity> list = GetClassifyList();
                string itemId = entity.F_ItemId;
                DataItemEntity classifyEntity = list.Find(t => t.F_ItemId.Equals(itemId));
                if (classifyEntity.F_IsTree != 1 || string.IsNullOrEmpty(entity.F_ParentId))
                {
                    entity.F_ParentId = "0";
                }
                dataItemService.SaveDetailEntity(keyValue, entity);

                cache.Remove(cacheKeyDetail + "dic", CacheId.dataItem);
                cache.Remove(cacheKeyDetail + classifyEntity.F_ItemCode, CacheId.dataItem);
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
        /// 虚拟删除明细数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void VirtualDeleteDetail(string keyValue)
        {
            try
            {
                dataItemService.VirtualDeleteDetail(keyValue);
                DataItemDetailEntity entity = dataItemService.GetDetailEntity(keyValue);
                List<DataItemEntity> list = GetClassifyList();
                string itemId = entity.F_ItemId;
                DataItemEntity classifyEntity = list.Find(t => t.F_ItemId.Equals(itemId));
                cache.Remove(cacheKeyDetail + "dic", CacheId.dataItem);
                cache.Remove(cacheKeyDetail + classifyEntity.F_ItemCode, CacheId.dataItem);
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
