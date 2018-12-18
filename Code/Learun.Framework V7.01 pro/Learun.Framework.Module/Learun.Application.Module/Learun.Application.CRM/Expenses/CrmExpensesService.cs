using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;

namespace Learun.Application.CRM
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-07-11 14:28
    /// 描 述：费用支出
    /// </summary>
    public class CrmExpensesService : RepositoryFactory
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<CrmExpensesEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var expression = LinqExtensions.True<CrmExpensesEntity>();
                var queryParam = queryJson.ToJObject();
                //支出日期
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    DateTime startTime = queryParam["StartTime"].ToDate();
                    DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                    expression = expression.And(t => t.F_ExpensesDate >= startTime && t.F_ExpensesDate <= endTime);
                }
                //支出种类
                if (!queryParam["ExpensesType"].IsEmpty())
                {
                    string CustomerName = queryParam["ExpensesType"].ToString();
                    expression = expression.And(t => t.F_ExpensesType.Equals(CustomerName));
                }
                //经手人
                if (!queryParam["Managers"].IsEmpty())
                {
                    string SellerName = queryParam["Managers"].ToString();
                    expression = expression.And(t => t.F_Managers.Contains(SellerName));
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
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<CrmExpensesEntity> GetList(string queryJson)
        {
            try
            {
                return this.BaseRepository().FindList<CrmExpensesEntity>();
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
        public CrmExpensesEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<CrmExpensesEntity>(keyValue);
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
        /// 保存表单（新增）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveEntity(CrmExpensesEntity entity)
        {
            CrmCashBalanceService crmCashBalanceService = new CrmCashBalanceService();

            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                //支出
                entity.Create();
                db.Insert(entity);

                //添加账户余额
                crmCashBalanceService.AddBalance(db, new CrmCashBalanceEntity
                {
                    F_ObjectId = entity.F_ExpensesId,
                    F_ExecutionDate = entity.F_ExpensesDate,
                    F_CashAccount = entity.F_ExpensesAccount,
                    F_Expenses = entity.F_ExpensesPrice,
                    F_Abstract = entity.F_ExpensesAbstract
                });

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
