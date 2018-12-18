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
    /// 日 期：2017.03.04
    /// 描 述：岗位管理
    /// </summary>
    public class PostBLL : PostIBLL
    {
        private PostService postService = new PostService();
        private DepartmentIBLL departmentIBLL = new DepartmentBLL();

        #region 缓存定义
        private ICache cache = CacheFactory.CaChe();
        private string cacheKey = "learun_adms_post_"; // +公司主键
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取岗位数据列表（根据公司列表）
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <returns></returns>
        public List<PostEntity> GetList(string companyId)
        {
            try
            {
                List<PostEntity> list = cache.Read<List<PostEntity>>(cacheKey + companyId, CacheId.post);
                if (list == null) {
                    list = (List<PostEntity>)postService.GetList(companyId);
                    cache.Write<List<PostEntity>>(cacheKey + companyId, list, CacheId.post);
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
        /// 获取岗位数据列表（根据公司列表）
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <param name="keyword">关键词</param>
        /// <param name="keyword">部门Id</param>
        /// <returns></returns>
        public List<PostEntity> GetList(string companyId, string keyword, string departmentId)
        {
            try
            {
                List<PostEntity> list = GetList(companyId);

                if (!string.IsNullOrEmpty(departmentId))
                {
                    list = list.FindAll(t => t.F_DepartmentId.ContainsEx(departmentId));
                }

                if (!string.IsNullOrEmpty(keyword))
                {
                    list = list.FindAll(t => t.F_Name.Contains(keyword));
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
        /// 获取岗位数据列表(根据主键串)
        /// </summary>
        /// <param name="postIds">根据主键串</param>
        /// <returns></returns>
        public IEnumerable<PostEntity> GetListByPostIds(string postIds)
        {
            try
            {
                return postService.GetListByPostIds(postIds);
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
        /// 获取树形结构数据
        /// </summary>
        /// <param name="companyId">公司主键</param>
        /// <returns></returns>
        public List<TreeModel> GetTree(string companyId)
        {
            try
            {
                if (string.IsNullOrEmpty(companyId))
                {
                    return new List<TreeModel>();
                }
                List<PostEntity> list = GetList(companyId);
                List<DepartmentEntity> departmentList = departmentIBLL.GetList(companyId);


                List<TreeModel> treeList = new List<TreeModel>();
                foreach (var item in list)
                {
                    TreeModel node = new TreeModel();
                    node.id = item.F_PostId;
                    node.text = item.F_Name;
                    DepartmentEntity departmentEntity = departmentList.Find(t=>t.F_DepartmentId == item.F_DepartmentId);
                    if (departmentEntity != null)
                    {
                        node.text = "【" + departmentEntity.F_FullName + "】" + node.text;
                    }


                    node.value = item.F_PostId;
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
        /// 获取岗位实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public PostEntity GetEntity(string keyValue) {
            try
            {
                return postService.GetEntity(keyValue);
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
        /// 虚拟删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void VirtualDelete(string keyValue)
        {
            try
            {
                PostEntity entity = GetEntity(keyValue);
                cache.Remove(cacheKey + entity.F_CompanyId, CacheId.post);
                postService.VirtualDelete(keyValue); 
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
        /// 保存岗位（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="postEntity">岗位实体</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, PostEntity postEntity)
        {
            try
            {
                cache.Remove(cacheKey + postEntity.F_CompanyId, CacheId.post);
                postService.SaveEntity(keyValue, postEntity);
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
