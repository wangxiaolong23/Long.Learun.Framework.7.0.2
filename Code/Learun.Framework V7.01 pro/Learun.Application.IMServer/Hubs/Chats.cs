using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Threading.Tasks;

namespace Learun.Application.IMServer
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.01
    /// 描 述：即使通信服务(可供客户端调用的方法开头用小写)
    /// </summary>
    [HubName("ChatsHub")]
    public class Chats : Hub
    {
        #region 重载Hub方法
        /// <summary>
        /// 建立连接
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            AddOnline();
            return base.OnConnected();
        }
        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="stopCalled">是否是客户端主动断开：true是,false超时断开</param>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            RemoveOnline();
            return base.OnDisconnected(stopCalled);
        }
        /// <summary>
        /// 重新建立连接
        /// </summary>
        /// <returns></returns>
        public override Task OnReconnected()
        {
            AddOnline();
            return base.OnReconnected();
        }
        #endregion

        #region 客户端操作
        /// <summary>
        /// 添加在线用户
        /// </summary>
        public void AddOnline()
        {
            string clientId = Context.ConnectionId;
            string userId = GetUserId();
            Groups.Add(clientId, userId);
        }
        /// <summary>
        /// 移除在线用户
        /// </summary>
        public void RemoveOnline()
        {
            string clientId = Context.ConnectionId;
            string userId = GetUserId();

            Groups.Remove(clientId, userId);
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="toUserId">对方UserId</param>
        /// <param name="msg">消息</param>
        /// <param name="isSystem">是否系统消息0不是1是</param>
        public void SendMsg(string toUserId, string msg, int isSystem)
        {
            string userId = GetUserId();
            Clients.Group(toUserId).RevMsg(userId, msg, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), isSystem);
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="myUserId">我的UserId</param>
        /// <param name="toUserId">对方UserId</param>
        /// <param name="msg">消息</param>
        /// <param name="isSystem">是否系统消息0不是1是</param>
        public void SendMsg2(string myUserId, string toUserId, string msg, int isSystem)
        {
            Clients.Group(toUserId).RevMsg(myUserId, msg, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), isSystem);
        }

        #endregion

        #region 一般公用方法
        /// <summary>
        /// 获取登录用户Id
        /// </summary>
        /// <returns></returns>
        private string GetUserId()
        {
            string userId = "";
            if (Context.QueryString["userId"] != null)
            {
                userId = Context.QueryString["userId"];
            }
            return userId;
        }
        #endregion
    }
}
