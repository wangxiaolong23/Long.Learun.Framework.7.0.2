using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Learun.Application.CRM
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-07-11 11:30
    /// 描 述：商机管理
    /// </summary>
    public class CrmChanceService : RepositoryFactory
    {
        private CrmTrailRecordService crmTrailRecordService = new CrmTrailRecordService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<CrmChanceEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var expression = LinqExtensions.True<CrmChanceEntity>();
                var queryParam = queryJson.ToJObject();
                //查询条件
                if (!queryParam["keyword"].IsEmpty())
                {
                    string keyword = queryParam["keyword"].ToString();
                    expression = expression.And(t => t.F_FullName.Contains(keyword)
                                 || t.F_Contacts.Contains(keyword)
                                 || t.F_Mobile.Contains(keyword)
                                 || t.F_Tel.Contains(keyword)
                                 || t.F_QQ.Contains(keyword)
                                 || t.F_Wechat.Contains(keyword)
                                 || t.F_CompanyName.Contains(keyword)
                                 || t.F_Contacts.Contains(keyword)
                                 || t.F_City.Contains(keyword)
                                 );
                }
                return this.BaseRepository().FindList(expression, pagination);
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
        public CrmChanceEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<CrmChanceEntity>(keyValue);
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

        #region 验证数据
        /// <summary>
        /// 商机名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            try
            {
                var expression = LinqExtensions.True<CrmChanceEntity>();
                expression = expression.And(t => t.F_FullName == fullName);
                if (!string.IsNullOrEmpty(keyValue))
                {
                    expression = expression.And(t => t.F_ChanceId != keyValue);
                }
                return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
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
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<CrmChanceEntity>(t => t.F_ChanceId == keyValue);
                db.Delete<CrmTrailRecordEntity>(t => t.F_ObjectId.Equals(keyValue));
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
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, CrmChanceEntity entity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
                }
                else
                {
                    IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                    try
                    {
                        entity.Create();
                        db.Insert(entity);
                        db.Commit();
                    }
                    catch (Exception)
                    {
                        db.Rollback();
                        throw;
                    }
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
        /// <summary>
        /// 商机作废
        /// </summary>
        /// <param name="keyValue">主键值</param>
        public void Invalid(string keyValue)
        {
            try
            {
                CrmChanceEntity entity = new CrmChanceEntity();
                entity.Modify(keyValue);
                entity.F_ChanceState = 0;
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
        /// 商机转换客户
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="customerCode">客户编号</param>
        public void ToCustomer(string keyValue,string customerCode)
        {
            CrmChanceEntity chanceEntity = this.GetEntity(keyValue);
            IEnumerable<CrmTrailRecordEntity> trailRecordList = crmTrailRecordService.GetList(keyValue);
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                chanceEntity.Modify(keyValue);
                chanceEntity.F_IsToCustom = 1;
                db.Update<CrmChanceEntity>(chanceEntity);
                CrmCustomerEntity customerEntity = new CrmCustomerEntity();
                customerEntity.Create();
                customerEntity.F_EnCode = customerCode;
                customerEntity.F_FullName = chanceEntity.F_CompanyName;
                customerEntity.F_TraceUserId = chanceEntity.F_TraceUserId;
                customerEntity.F_TraceUserName = chanceEntity.F_TraceUserName;
                customerEntity.F_CustIndustryId = chanceEntity.F_CompanyNatureId;
                customerEntity.F_CompanySite = chanceEntity.F_CompanySite;
                customerEntity.F_CompanyDesc = chanceEntity.F_CompanyDesc;
                customerEntity.F_CompanyAddress = chanceEntity.F_CompanyAddress;
                customerEntity.F_Province = chanceEntity.F_Province;
                customerEntity.F_City = chanceEntity.F_City;
                customerEntity.F_Contact = chanceEntity.F_Contacts;
                customerEntity.F_Mobile = chanceEntity.F_Mobile;
                customerEntity.F_Tel = chanceEntity.F_Tel;
                customerEntity.F_Fax = chanceEntity.F_Fax;
                customerEntity.F_QQ = chanceEntity.F_QQ;
                customerEntity.F_Email = chanceEntity.F_Email;
                customerEntity.F_Wechat = chanceEntity.F_Wechat;
                customerEntity.F_Hobby = chanceEntity.F_Hobby;
                customerEntity.F_Description = chanceEntity.F_Description;
                customerEntity.F_CustLevelId = "C";
                customerEntity.F_CustDegreeId = "往来客户";
                db.Insert<CrmCustomerEntity>(customerEntity);

                foreach (CrmTrailRecordEntity item in trailRecordList)
                {
                    item.F_TrailId = Guid.NewGuid().ToString();
                    item.F_ObjectId = customerEntity.F_CustomerId;
                    item.F_ObjectSort = 2;
                    db.Insert<CrmTrailRecordEntity>(item);
                }
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
