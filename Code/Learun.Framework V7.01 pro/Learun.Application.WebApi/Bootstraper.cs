using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace Learun.Application.WebApi
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.05.12
    /// 描 述：Nancy启动重载（用于解决跨域问题）
    /// </summary>
    public class Bootstraper : DefaultNancyBootstrapper
    {
        /// <summary>
        /// 自定义请求启动函数
        /// </summary>
        /// <param name="container"></param>
        /// <param name="pipelines"></param>
        /// <param name="context"></param>
        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            //CORS Enable
            pipelines.AfterRequest.AddItemToEndOfPipeline((ctx) =>
            {
                var originlist = ctx.Request.Headers["Origin"];
                foreach (var origin in originlist)
                {
                    ctx.Response.WithHeader("Access-Control-Allow-Origin", origin);
                }
                ctx.Response.WithHeader("Access-Control-Allow-Methods", "POST,GET")
                    .WithHeader("Access-Control-Allow-Credentials", "true")
                    .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type");

            });
        }
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            Nancy.Json.JsonSettings.MaxJsonLength = int.MaxValue;
        }

    }
}