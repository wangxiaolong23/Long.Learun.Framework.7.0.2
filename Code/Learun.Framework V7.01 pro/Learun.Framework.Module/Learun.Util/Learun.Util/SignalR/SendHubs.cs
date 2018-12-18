using Microsoft.AspNet.SignalR.Client;
using System.Threading;

namespace Learun.Util
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.06.15
    /// 描 述：发送消息给SignalR集结器
    /// </summary>
    public static class SendHubs
    {
        /// <summary>
        /// 调用hub方法
        /// </summary>
        /// <param name="methodName"></param>
        public static void callMethod(string methodName, params object[] args)
        {
            var hubConnection = new HubConnection(Config.GetValue("IMUrl"));
            IHubProxy ChatsHub = hubConnection.CreateHubProxy("ChatsHub");
            bool done = false;
            hubConnection.Start().ContinueWith(task =>
            {
                //连接成功调用服务端方法
                if (!task.IsFaulted)
                {
                    ChatsHub.Invoke(methodName, args);
                    done = true;
                }
                else {
                    done = true;
                }
            });
            while (!done)
            {
                Thread.Sleep(100);
            }
            //结束连接
            hubConnection.Stop();
        }
    }
}
