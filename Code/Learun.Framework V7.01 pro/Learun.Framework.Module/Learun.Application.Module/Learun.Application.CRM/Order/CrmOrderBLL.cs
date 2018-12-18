using Learun.Util;
using System;
using System.Collections.Generic;

namespace Learun.Application.CRM
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.09
    /// 描 述：订单管理
    /// </summary>
    public class CrmOrderBLL : CrmOrderIBLL
    {
        private CrmOrderService crmOrderService = new CrmOrderService();

        #region 获取数据
        /// <summary>
        /// 订单列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CrmOrderEntity> GetPageList(Pagination pagination, string queryJson, string custmerQuerySql)
        {
            try
            {
                return crmOrderService.GetPageList(pagination, queryJson, custmerQuerySql);
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
        /// 获取订单信息主键
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public CrmOrderEntity GetCrmOrderEntity(string keyValue)
        {
            try
            {
                return crmOrderService.GetCrmOrderEntity(keyValue);
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
        /// 获取订单产品明细数据
        /// </summary>
        /// <param name="orderId">订单主键</param>
        /// <returns></returns>
        public IEnumerable<CrmOrderProductEntity> GetCrmOrderProductEntity(string orderId)
        {
            try
            {
                return crmOrderService.GetCrmOrderProductEntity(orderId);
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
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                crmOrderService.DeleteEntity(keyValue);
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
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="crmOrderEntity">实体对象</param>
        /// <param name="CrmOrderProductEntity">明细实体对象</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, CrmOrderEntity crmOrderEntity, List<CrmOrderProductEntity> crmOrderProductList)
        {
            try
            {
                crmOrderService.SaveEntity(keyValue, crmOrderEntity, crmOrderProductList);
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
