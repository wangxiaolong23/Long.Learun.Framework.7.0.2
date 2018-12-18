using Learun.Application.CRM;
using Nancy;

namespace Learun.Application.WebApi
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.03.22
    /// 描 述：开票信息
    /// </summary>
    public class Invoice: BaseApi
    {
        /// <summary>
        /// 注册接口
        /// </summary>
        public Invoice()
            : base("/learun/adms/crm/invoice")
        {
            Get["/list"] = GetList;// 获取开票信息列表

            Post["save"] = Save;
        }
        private CrmInvoiceIBLL crmInvoiceIBLL = new CrmInvoiceBLL();

        /// <summary>
        /// 获取客户端数据
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Response GetList(dynamic _)
        {
            ReqPageParam parameter = this.GetReqData<ReqPageParam>();

            var list = crmInvoiceIBLL.GetPageList(parameter.pagination, parameter.queryJson);
            var jsonData = new
            {
                rows = list,
                total = parameter.pagination.total,
                page = parameter.pagination.page,
                records = parameter.pagination.records,
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 获取客户端数据
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Response Save(dynamic _)
        {
            ReqEntity<CrmInvoiceEntity> parameter = this.GetReqData<ReqEntity<CrmInvoiceEntity>>();
            crmInvoiceIBLL.SaveEntity(parameter.keyValue, parameter.entity);
            return Success("保存成功");
        }
    }
}