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
    /// 日 期：2017.03.08
    /// 描 述：数据字典管理服务类
    /// </summary>
    public class DataItemService : RepositoryFactory
    {
        #region 属性 构造函数
        private string fieldSql;
        private string detailFieldSql;
        public DataItemService()
        {
            fieldSql = @" 
                    t.F_ItemId,
                    t.F_ParentId,
                    t.F_ItemCode,
                    t.F_ItemName,
                    t.F_IsTree,
                    t.F_IsNav,
                    t.F_SortCode,
                    t.F_DeleteMark,
                    t.F_EnabledMark,
                    t.F_Description,
                    t.F_CreateDate,
                    t.F_CreateUserId,
                    t.F_CreateUserName,
                    t.F_ModifyDate,
                    t.F_ModifyUserId,
                    t.F_ModifyUserName
                    ";
            detailFieldSql = @"
                    t.F_ItemDetailId,
                    t.F_ItemId,
                    t.F_ParentId,
                    t.F_ItemCode,
                    t.F_ItemName,
                    t.F_ItemValue,
                    t.F_QuickQuery,
                    t.F_SimpleSpelling,
                    t.F_IsDefault,
                    t.F_SortCode,
                    t.F_DeleteMark,
                    t.F_EnabledMark,
                    t.F_Description,
                    t.F_CreateDate,
                    t.F_CreateUserId,
                    t.F_CreateUserName,
                    t.F_ModifyDate,
                    t.F_ModifyUserId,
                    t.F_ModifyUserName
                    ";
        }
        #endregion

        #region 数据字典分类管理
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataItemEntity> GetClassifyList()
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT " + fieldSql + " FROM LR_Base_DataItem t WHERE t.F_DeleteMark = 0 Order By t.F_ParentId,t.F_SortCode ");
                return this.BaseRepository().FindList<DataItemEntity>(strSql.ToString());
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
        /// 虚拟删除分类数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void VirtualDeleteClassify(string keyValue)
        {
            try
            {
                DataItemEntity entity = new DataItemEntity()
                {
                    F_ItemId = keyValue,
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
        /// 保存分类数据实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        public void SaveClassifyEntity(string keyValue, DataItemEntity entity) {
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    entity.Create();
                    this.BaseRepository().Insert(entity);
                }
                else {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
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

        #region 数据字典明细
        /// <summary>
        /// 获取数据字典明显根据分类编号
        /// </summary>
        /// <param name="itemCode">分类编号</param>
        /// <returns></returns>
        public IEnumerable<DataItemDetailEntity> GetDetailList(string itemCode)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT " + detailFieldSql + @" FROM LR_Base_DataItemDetail t
                            INNER JOIN LR_Base_DataItem t2 ON t.F_ItemId = t2.F_ItemId
                            WHERE t2.F_ItemCode = @itemCode AND t.F_DeleteMark = 0  Order By t.F_SortCode
                           ");
                return this.BaseRepository().FindList<DataItemDetailEntity>(strSql.ToString(), new { itemCode = itemCode });
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
        /// 获取数据字典明细实体类
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public DataItemDetailEntity GetDetailEntity(string keyValue) {
            try
            {
                return this.BaseRepository().FindEntity<DataItemDetailEntity>(keyValue);
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
        /// 虚拟删除明细数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void VirtualDeleteDetail(string keyValue)
        {
            try
            {
                DataItemDetailEntity entity = new DataItemDetailEntity()
                {
                    F_ItemDetailId = keyValue,
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
        /// 保存明细数据实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        public void SaveDetailEntity(string keyValue, DataItemDetailEntity entity)
        {
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    entity.Create();
                    this.BaseRepository().Insert(entity);
                }
                else
                {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
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
