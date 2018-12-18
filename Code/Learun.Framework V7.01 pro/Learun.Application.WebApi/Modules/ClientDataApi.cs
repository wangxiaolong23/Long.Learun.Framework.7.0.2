using Learun.Application.AppMagager;
using Learun.Application.WorkFlow;
using Nancy;

namespace Learun.Application.WebApi.Modules
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.03.22
    /// 描 述：客户端数据
    /// </summary>
    public class ClientDataApi: BaseApi
    {
        /// <summary>
        /// 注册接口
        /// </summary>
        public ClientDataApi()
            : base("/learun/adms/clinet")
        {
            Get["/module"] = GetModule;// 获取数据字典详细列表
        }

        private FunctionIBLL functionIBLL = new FunctionBLL();
        private WfSchemeIBLL wfSchemeIBLL = new WfSchemeBLL();
        
        /// <summary>
        /// 获取客户端数据
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Response GetModule(dynamic _)
        {
            //var custmerform = functionIBLL.GetList();
            var wfSchemeList = wfSchemeIBLL.GetAppCustmerSchemeInfoList(userInfo);

            var jsonData = new
            {
                //custmerform = custmerform,
                wflist = wfSchemeList
            };
            return Success(jsonData);
        }


    }
}