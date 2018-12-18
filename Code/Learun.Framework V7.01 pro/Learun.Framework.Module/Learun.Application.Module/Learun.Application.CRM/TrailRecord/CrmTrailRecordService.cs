using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Learun.Application.CRM
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-07-11 11:20
    /// 描 述：跟进记录
    /// </summary>
    public class CrmTrailRecordService : RepositoryFactory
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="objectId">对象Id</param>
        /// <returns>返回列表</returns>
        public IEnumerable<CrmTrailRecordEntity> GetList(string objectId)
        {
            try
            {
                return this.BaseRepository().IQueryable<CrmTrailRecordEntity>(t => t.F_ObjectId.Equals(objectId)).OrderByDescending(t => t.F_CreateDate).ToList();
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
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public CrmTrailRecordEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<CrmTrailRecordEntity>(keyValue);
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
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                this.BaseRepository().Delete(keyValue);
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
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, CrmTrailRecordEntity entity)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                switch (entity.F_ObjectSort)
                {
                    case 1:         //商机
                        CrmChanceEntity chanceEntity = new CrmChanceEntity();
                        chanceEntity.Modify(entity.F_ObjectId);
                        db.Update<CrmChanceEntity>(chanceEntity);
                        break;
                    case 2:         //客户
                        CrmCustomerEntity customerEntity = new CrmCustomerEntity();
                        customerEntity.Modify(entity.F_ObjectId);
                        db.Update<CrmCustomerEntity>(customerEntity);
                        break;
                    default:
                        break;
                }
                entity.Create();
                db.Insert(entity);

                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
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
