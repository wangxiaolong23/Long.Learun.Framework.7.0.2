using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learun.Application.Base.SystemModule
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.01
    /// 描 述：行政区域
    /// </summary>
    public class AreaService : RepositoryFactory
    {
        #region 构造函数和属性
        private string fieldSql;
        public AreaService() {
            fieldSql = @"
                        t.F_AreaId,
                        t.F_ParentId,
                        t.F_AreaCode,
                        t.F_AreaName,
                        t.F_QuickQuery,
                        t.F_SimpleSpelling,
                        t.F_Layer,
                        t.F_SortCode,
                        t.F_DeleteMark,
                        t.F_EnabledMark,
                        t.F_Description,
                        t.F_CreateDate,
                        t.F_CreateUserId,
                        t.F_CreateUserName,
                        t.F_ModifyDate,
                        t.F_ModifyUserId,
                        t.F_ModifyUserName ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 区域列表
        /// </summary>
        /// <param name="parentId">父节点Id</param>
        /// <returns></returns>
        public IEnumerable<AreaEntity> GetList(string parentId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_Base_Area t WHERE t.F_EnabledMark = 1 AND t.F_DeleteMark = 0  ");
                if (!string.IsNullOrEmpty(parentId))
                {
                    strSql.Append(" AND F_ParentId = @F_ParentId ");
                }
                strSql.Append(" ORDER BY t.F_AreaCode ");
                return this.BaseRepository().FindList<AreaEntity>(strSql.ToString(), new { F_ParentId = parentId });
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
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
                return this.BaseRepository().FindEntity<AreaEntity>(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
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
                AreaEntity entity = new AreaEntity()
                {
                    F_AreaId = keyValue,
                    F_DeleteMark = 1
                };
                this.BaseRepository().Update(entity);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 保存区域表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="{">区域实体</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, AreaEntity areaEntity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    areaEntity.Modify(keyValue);
                    this.BaseRepository().Update(areaEntity);
                }
                else
                {
                    areaEntity.Create();
                    this.BaseRepository().Insert(areaEntity);
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
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
           
        }
        #endregion
    }
}
