using Learun.Application.Base.AuthorizeModule;
using Learun.Application.Base.SystemModule;
using Learun.Util;
using System;
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
    public class FunctionBLL: FunctionIBLL
    {
        private FunctionSerivce functionSerivce = new FunctionSerivce();
        private DataItemIBLL dataItemIBLL = new DataItemBLL();
        private AuthorizeIBLL authorizeIBLL = new AuthorizeBLL();


        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字</param>
        /// <param name="type">分类</param>
        /// <returns></returns>
        public IEnumerable<FunctionEntity> GetPageList(Pagination pagination, string keyword, string type)
        {
            try
            {
                return functionSerivce.GetPageList(pagination, keyword, type);
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
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<FunctionEntity> GetList(UserInfo userInfo)
        {
            try
            {
                List<FunctionEntity> list = (List<FunctionEntity>)functionSerivce.GetList();
                /*关联权限*/
                if (!userInfo.isSystem)
                {
                    string objectIds = userInfo.userId + (string.IsNullOrEmpty(userInfo.roleIds) ? "" : ("," + userInfo.roleIds));
                    List<string> itemIdList = authorizeIBLL.GetItemIdListByobjectIds(objectIds, 5);
                    list = list.FindAll(t => itemIdList.IndexOf(t.F_Id) >= 0);
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
        /// 获取实体对象
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public FunctionEntity GetEntity(string keyValue)
        {
            try
            {
                return functionSerivce.GetEntity(keyValue);
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
        /// 获取移动功能模板
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public FunctionSchemeEntity GetScheme(string keyValue)
        {
            try
            {
                return functionSerivce.GetScheme(keyValue);
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
        /// 获取树形移动功能列表
        /// </summary>
        /// <returns></returns>
        public List<TreeModel> GetCheckTree()
        {
            try
            {
                List<TreeModel> treeList = new List<TreeModel>();
                var dataItemList = dataItemIBLL.GetDetailList("function");
                UserInfo userInfo = LoginUserInfo.Get();
                var list = GetList(userInfo);
                Dictionary<string, List<TreeModel>> map = new Dictionary<string, List<TreeModel>>();
                foreach (var item in list)
                {
                    if (!map.ContainsKey(item.F_Type))
                    {
                        map[item.F_Type] = new List<TreeModel>();
                    }
                    TreeModel treeItem = new TreeModel();
                    treeItem.id = item.F_Id;
                    treeItem.text = item.F_Name;
                    treeItem.value = item.F_Id;
                    treeItem.showcheck = true;
                    treeItem.checkstate = 0;
                    treeItem.parentId = item.F_Type + "_LRDataItem";
                    map[item.F_Type].Add(treeItem);
                    treeItem.complete = true;
                }
                foreach (var item in dataItemList)
                {
                    if (map.ContainsKey(item.F_ItemValue))
                    {
                        TreeModel treeItem = new TreeModel();
                        treeItem.id = item.F_ItemValue + "_LRDataItem";
                        treeItem.text = item.F_ItemName;
                        treeItem.value = item.F_ItemValue + "_LRDataItem";
                        treeItem.showcheck = true;
                        treeItem.checkstate = 0;
                        treeItem.parentId = "0";
                        treeItem.hasChildren = true;
                        treeItem.complete = true;
                        treeItem.isexpand = true;
                        treeItem.ChildNodes = map[item.F_ItemValue];
                        treeList.Add(treeItem);
                    }
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
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void Delete(string keyValue)
        {
            try
            {
                functionSerivce.Delete(keyValue);
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
        /// 保存
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="functionEntity">功能信息</param>
        /// <param name="functionSchemeEntity">功能模板信息</param>
        public void SaveEntity(string keyValue, FunctionEntity functionEntity, FunctionSchemeEntity functionSchemeEntity)
        {
            try
            {

                functionSerivce.SaveEntity(keyValue, functionEntity, functionSchemeEntity);
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
        /// 更新状态
        /// </summary>
        /// <param name="keyValue">模板信息主键</param>
        /// <param name="state">状态1启用0禁用</param>
        public void UpdateState(string keyValue, int state)
        {
            try
            {
                functionSerivce.UpdateState(keyValue, state);

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
