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
    /// 日 期：2017-07-11 14:20
    /// 描 述：现金余额
    /// </summary>
    public class CrmCashBalanceService : RepositoryFactory
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<CrmCashBalanceEntity> GetList(string queryJson)
        {
            try
            {
                var expression = LinqExtensions.True<CrmCashBalanceEntity>();
                var queryParam = queryJson.ToJObject();
                //单据日期
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    DateTime startTime = queryParam["StartTime"].ToDate();
                    DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                    expression = expression.And(t => t.F_ExecutionDate >= startTime && t.F_ExecutionDate <= endTime);
                }
                return this.BaseRepository().IQueryable(expression).OrderBy(t => t.F_CreateDate).ToList();
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
        /// 添加收支余额
        /// </summary>
        /// <param name="db"></param>
        /// <param name="cashBalanceEntity"></param>
        public void AddBalance(IRepository db, CrmCashBalanceEntity cashBalanceEntity)
        {
            try
            {
                decimal balance = 0;
                var data = db.IQueryable<CrmCashBalanceEntity>(t => t.F_CashAccount == cashBalanceEntity.F_CashAccount).OrderByDescending(t => t.F_CreateDate);
                if (data.Count() > 0)
                {
                    balance = data.First().F_Balance.ToDecimal();
                }
                if (cashBalanceEntity.F_Receivable != null)
                {
                    cashBalanceEntity.F_Balance = cashBalanceEntity.F_Receivable + balance;
                }
                if (cashBalanceEntity.F_Expenses != null)
                {
                    cashBalanceEntity.F_Balance = balance - cashBalanceEntity.F_Expenses;
                }
                cashBalanceEntity.Create();
                db.Insert(cashBalanceEntity);
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
