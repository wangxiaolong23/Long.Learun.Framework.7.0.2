using Learun.Application.Base.SystemModule;
using Learun.Application.CRM;
using Learun.Util;
using Nancy;
using System.Collections.Generic;

namespace Learun.Application.WebApi.Modules.LR_Crm
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.07.10
    /// 描 述：订单管理
    /// </summary>
    public class CrmOrder: BaseApi
    {
        private CrmOrderIBLL crmOrderIBLL = new CrmOrderBLL();
        private CodeRuleIBLL codeRuleIBLL = new CodeRuleBLL();

        /// <summary>
        /// 注册接口
        /// </summary>
        public CrmOrder()
            : base("/learun/adms/crm/order")
        {
            Get["/pagelist"] = GetPageList;
            Get["/form"] = GetForm;

            Post["delete"] = DeleteForm;
            Post["save"] = SaveForm;
        }

        #region 获取数据
        /// <summary>
        ///  分页查询
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        /// <returns></returns>
        public Response GetPageList(dynamic _)
        {
            ReqPageParam parameter = this.GetReqData<ReqPageParam>();

            var data = crmOrderIBLL.GetPageList(parameter.pagination, parameter.queryJson, "");
            var jsonData = new
            {
                rows = data,
                total = parameter.pagination.total,
                page = parameter.pagination.page,
                records = parameter.pagination.records,
            };
            return Success(jsonData);
        }
        /// <summary>
        ///  获取数据
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public Response GetForm(dynamic _)
        {
            string keyValue = this.GetReqData();

            var orderData = crmOrderIBLL.GetCrmOrderEntity(keyValue);
            var orderProductData = crmOrderIBLL.GetCrmOrderProductEntity(keyValue);
            var jsonData = new
            {
                orderData = orderData,
                orderProductData = orderProductData
            };
            return Success(jsonData);
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 删除订单数据
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public Response DeleteForm(dynamic _)
        {
            string keyValue = this.GetReqData();
            crmOrderIBLL.DeleteEntity(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存订单表单（新增、修改）
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public Response SaveForm(dynamic _)
        {

            ReqFormEntity parameter = this.GetReqData<ReqFormEntity>();


            var crmOrderEntity = parameter.crmOrderJson.ToObject<CrmOrderEntity>();
            var crmOrderProductEntity = parameter.crmOrderProductJson.ToObject<List<CrmOrderProductEntity>>();
            crmOrderIBLL.SaveEntity(parameter.keyValue, crmOrderEntity, crmOrderProductEntity);
            if (string.IsNullOrEmpty(parameter.keyValue))
            {
                codeRuleIBLL.UseRuleSeed(((int)CodeRuleEnum.CrmOrderCode).ToString());
            }
            return Success("保存成功。");
        }
        #endregion


        /// <summary>
        /// 表单实体类
        /// </summary>
        private class ReqFormEntity
        {
            public string keyValue { get; set; }
            public string crmOrderJson { get; set; }
            public string crmOrderProductJson { get; set; }
        }


    }
}