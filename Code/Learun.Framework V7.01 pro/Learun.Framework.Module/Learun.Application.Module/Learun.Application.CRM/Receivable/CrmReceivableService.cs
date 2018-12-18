using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Learun.Application.CRM
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-07-11 14:48
    /// 描 述：应收账款
    /// </summary>
    public class CrmReceivableService : RepositoryFactory
    {
        private CrmOrderService crmOrderService = new CrmOrderService();

        #region 获取数据
        /// <summary>
        /// 获取收款单列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<CrmOrderEntity> GetPaymentPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var expression = LinqExtensions.True<CrmOrderEntity>();
                var queryParam = queryJson.ToJObject();
                //单据日期
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    DateTime startTime = queryParam["StartTime"].ToDate();
                    DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                    expression = expression.And(t => t.F_OrderDate >= startTime && t.F_OrderDate <= endTime);
                }
                //单据编号
                if (!queryParam["OrderCode"].IsEmpty())
                {
                    string OrderCode = queryParam["OrderCode"].ToString();
                    expression = expression.And(t => t.F_OrderCode.Contains(OrderCode));
                }
                return  this.BaseRepository().FindList<CrmOrderEntity>(expression, pagination);
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
        /// 获取收款记录列表
        /// </summary>
        /// <param name="orderId">订单主键</param>
        /// <returns></returns>
        public IEnumerable<CrmReceivableEntity> GetPaymentRecord(string orderId)
        {
            try
            {
                return this.BaseRepository().IQueryable<CrmReceivableEntity>(t => t.F_OrderId.Equals(orderId)).OrderByDescending(t => t.F_CreateDate).ToList();
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
        /// 保存表单（新增）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveEntity(CrmReceivableEntity entity)
        {
            CrmCashBalanceService crmCashBalanceService = new CrmCashBalanceService();
            CrmOrderEntity crmOrderEntity = crmOrderService.GetCrmOrderEntity(entity.F_OrderId);

            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                //更改订单状态
                crmOrderEntity.F_ReceivedAmount = crmOrderEntity.F_ReceivedAmount + entity.F_PaymentPrice;
                if (crmOrderEntity.F_ReceivedAmount == crmOrderEntity.F_Accounts)
                {
                    crmOrderEntity.F_PaymentState = 3;
                }
                else
                {
                    crmOrderEntity.F_PaymentState = 2;
                }
                db.Update(crmOrderEntity);
                //添加收款
                entity.Create();
                db.Insert(entity);
                //添加账户余额
                crmCashBalanceService.AddBalance(db, new CrmCashBalanceEntity
                {
                    F_ObjectId = entity.F_ReceivableId,
                    F_ExecutionDate = entity.F_PaymentTime,
                    F_CashAccount = entity.F_PaymentAccount,
                    F_Receivable = entity.F_PaymentPrice,
                    F_Abstract = entity.F_Description
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

        #region 报表
        /// <summary>
        /// 获取收款列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<ReceivableReportModel> GetList(string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"SELECT  r.F_ReceivableId ,
                                    o.F_OrderId ,
                                    o.F_OrderCode ,
                                    c.F_CustomerId ,
                                    c.F_EnCode AS F_CustomerCode ,
                                    c.F_FullName AS F_CustomerName ,
                                    r.F_PaymentTime ,
                                    r.F_PaymentPrice ,
                                    r.F_PaymentMode ,
                                    r.F_PaymentAccount ,
                                    r.F_Description ,
                                    r.F_CreateDate ,
                                    r.F_CreateUserName
                            FROM    Client_Receivable r
                                    LEFT JOIN Client_Order o ON o.F_OrderId = r.F_OrderId
                                    LEFT JOIN Client_Customer c ON c.F_CustomerId = o.F_CustomerId
                            WHERE   1 = 1");
                DateTime startTime = new DateTime();
                DateTime endTime = new DateTime();
                string orderCode = "";
                string customerCode = "";
                string customerName = "";

                //收款日期
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    strSql.Append(" AND r.F_PaymentTime Between @startTime AND @endTime ");
                    startTime = (queryParam["StartTime"].ToString() + " 00:00").ToDate();
                    endTime = (queryParam["EndTime"].ToString() + " 23:59").ToDate();
                }
                //订单单号
                if (!queryParam["OrderCode"].IsEmpty())
                {
                    strSql.Append(" AND o.F_OrderCode like @orderCode");
                    orderCode = queryParam["OrderCode"].ToString();
                }
                //客户编号
                if (!queryParam["CustomerCode"].IsEmpty())
                {
                    strSql.Append(" AND c.F_CustomerCode like @customerCode");
                    customerCode = queryParam["CustomerCode"].ToString();
                }
                //客户名称
                if (!queryParam["CustomerName"].IsEmpty())
                {
                    strSql.Append(" AND c.F_CustomerName like @CustomerName");
                    customerName = '%' + queryParam["CustomerName"].ToString() + '%';
                }
                strSql.Append(" ORDER BY r.F_PaymentTime DESC");
                return this.BaseRepository().FindList<ReceivableReportModel>(strSql.ToString(), new { startTime = startTime, endTime = endTime, orderCode = orderCode, customerCode = customerCode, customerName = customerName });

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
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<ReceivableReportModel> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                StringBuilder strSql = new StringBuilder();
                strSql.Append(@"SELECT TOP(100) PERCENT
                                    r.F_ReceivableId ,
                                    o.F_OrderId ,
                                    o.F_OrderCode ,
                                    c.F_CustomerId ,
                                    c.F_EnCode AS F_CustomerCode ,
                                    c.F_FullName AS F_CustomerName ,
                                    r.F_PaymentTime ,
                                    r.F_PaymentPrice ,
                                    r.F_PaymentMode ,
                                    r.F_PaymentAccount ,
                                    r.F_Description ,
                                    r.F_CreateDate ,
                                    r.F_CreateUserName
                            FROM    Client_Receivable r
                                    LEFT JOIN Client_Order o ON o.F_OrderId = r.F_OrderId
                                    LEFT JOIN Client_Customer c ON c.F_CustomerId = o.F_CustomerId
                            WHERE   1 = 1");
                DateTime startTime = new DateTime();
                DateTime endTime = new DateTime();
                string orderCode = "";
                string customerCode = "";
                string customerName = "";
                //收款日期
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    strSql.Append(" AND r.F_PaymentTime Between @startTime AND @endTime ");
                    startTime = (queryParam["StartTime"].ToString() + " 00:00").ToDate();
                    endTime = (queryParam["EndTime"].ToString() + " 23:59").ToDate();
                }
                //订单单号
                if (!queryParam["OrderCode"].IsEmpty())
                {
                    strSql.Append(" AND o.F_OrderCode like @orderCode");
                    orderCode = queryParam["OrderCode"].ToString();
                }
                //客户编号
                if (!queryParam["CustomerCode"].IsEmpty())
                {
                    strSql.Append(" AND c.F_CustomerCode like @customerCode");
                    customerCode = queryParam["CustomerCode"].ToString();
                }
                //客户名称
                if (!queryParam["CustomerName"].IsEmpty())
                {
                    strSql.Append(" AND c.F_CustomerName like @CustomerName");
                    customerName = '%' + queryParam["CustomerName"].ToString() + '%';
                }
                strSql.Append(" ORDER BY r.F_PaymentTime DESC");
                return this.BaseRepository().FindList<ReceivableReportModel>(strSql.ToString(), new { startTime = startTime, endTime = endTime, orderCode = orderCode, customerCode = customerCode, customerName = customerName }, pagination);

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
