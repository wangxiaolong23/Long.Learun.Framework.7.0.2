using Nancy;

namespace Learun.Application.WebApi
{
    /// <summary>  
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.05.12
    /// 描 述：默认页面
    /// </summary>
    public class Index : BaseApi
    {
        public Index()
            : base()
        {
            Get["/"] = MainIndex;
            Get["/bgimg"] = BgImg;
        }
        /// <summary>
        /// 默认开始页面
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Response MainIndex(dynamic _)
        {
            return Response.AsFile("index.html");
        }
        /// <summary>
        /// 默认开始页面图片
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        private Response BgImg(dynamic _)
        {
            return Response.AsImage("port.png");
        }
    }
}