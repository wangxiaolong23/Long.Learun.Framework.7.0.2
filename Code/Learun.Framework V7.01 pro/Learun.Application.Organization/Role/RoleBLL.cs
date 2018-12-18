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
    /// 描 述：角色管理
    /// </summary>
    public class RoleBLL : RoleIBLL
    {
        #region 属性
        private RoleService roleService = new RoleService();
        #endregion

        #region 缓存定义
        private ICache cache = CacheFactory.CaChe();
        private string cacheKey = "learun_adms_role";
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取角色数据列表
        /// </summary>
        /// <returns></returns>
        public List<RoleEntity> GetList()
        {
            try
            {

                List<RoleEntity> list = cache.Read<List<RoleEntity>>(cacheKey, CacheId.role);
                if (list == null)
                {
                    list = (List<RoleEntity>)roleService.GetList();
                    cache.Write<List<RoleEntity>>(cacheKey, list, CacheId.role);
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
        /// 获取角色数据列表
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public List<RoleEntity> GetList(string keyword)
        {
            try
            {
                List<RoleEntity> list = GetList();
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
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">查询关键词</param>
        /// <returns></returns>
        public List<RoleEntity> GetPageList(Pagination pagination, string keyword)
        {
            try
            {
                return (List<RoleEntity>)roleService.GetPageList(pagination, keyword);
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
        /// 获取角色数据列表
        /// </summary>
        /// <param name="roleIds">主键串</param>
        /// <returns></returns>
        public IEnumerable<RoleEntity> GetListByRoleIds(string roleIds)
        {
            try
            {
               List<RoleEntity> list = GetList();
               list = list.FindAll(t => t.F_RoleId.Like(roleIds));
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
        #endregion

        #region 提交数据
        /// <summary>
        /// 虚拟删除角色
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void VirtualDelete(string keyValue)
        {
            try
            {
                cache.Remove(cacheKey, CacheId.role);
                roleService.VirtualDelete(keyValue);
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
        /// 保存角色（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="roleEntity">角色实体</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, RoleEntity roleEntity)
        {
            try
            {
                cache.Remove(cacheKey, CacheId.role);
                roleService.SaveEntity(keyValue, roleEntity);
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
