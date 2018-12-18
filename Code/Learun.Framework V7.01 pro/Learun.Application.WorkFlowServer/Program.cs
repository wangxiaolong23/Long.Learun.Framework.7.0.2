using Nancy.Hosting.Self;
using System;
using System.Configuration;

namespace Learun.Application.WorkFlowServer
{
    /// <summary>
    /// 程序入口
    /// </summary>
    class Program
    {
        /// <summary>
        /// 入口函数
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string serverUrl = ConfigurationManager.AppSettings["workflowService"].ToString();
            var nancyHost = new NancyHost(new Uri(serverUrl));
            nancyHost.Start();
            Console.WriteLine("工作流引擎服务监听在 " + serverUrl + ". Press enter to stop");
            Console.ReadKey();
            nancyHost.Stop();
            Console.WriteLine("服务停止.谢谢!");
        }
    }
}
