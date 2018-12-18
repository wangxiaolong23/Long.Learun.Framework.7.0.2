using Learun.Application.IM;
using Learun.Util;
using Nancy;
using System;

namespace Learun.Application.WebApi.Modules
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.06.14
    /// 描 述：即时通讯接口
    /// </summary>
    public class IMApi : BaseApi
    {
        public IMApi()
            : base("/learun/adms/im")
        {
            Get["/contacts"] = GetContactsList;

            Post["/send"] = SendMsg;
            Post["/addcontact"] = AddContact;
            Post["/updete"] = UpdateContactState;

            Get["/msg/lastlist"] = GetMsgList;
            Get["/msg/list"] = GetMsgList1;
            Get["/msg/list2"] = GetMsgList2;

        }


        private IMMsgIBLL iMMsgIBLL = new IMMsgBLL();
        private IMContactsIBLL iMContactsIBLL = new IMContactsBLL();


        /// <summary>
        /// 获取最近联系人列表
        /// <summary>
        /// <returns></returns>
        public Response GetContactsList(dynamic _)
        {
            string time = this.GetReqData();// 获取模板请求数据
            DateTime beginTime = DateTime.Now;
            var data = iMContactsIBLL.GetList(userInfo.userId,DateTime.Parse(time));
            var jsondata = new
            {
                data = data,
                time = beginTime
            };
            return Success(jsondata);
        }


        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public Response SendMsg(dynamic _)
        {
            MsgModel msyModel = this.GetReqData<MsgModel>();// 获取模板请求数据
            IMMsgEntity entity = new IMMsgEntity();
            entity.F_SendUserId = userInfo.userId;
            entity.F_RecvUserId = msyModel.userId;
            entity.F_Content = msyModel.content;
            iMMsgIBLL.SaveEntity(entity);
            // 向即时消息服务器发送一条信息
            SendHubs.callMethod("sendMsg2", userInfo.userId, msyModel.userId, msyModel.content, 0);

            var jsonData = new
            {
                time = entity.F_CreateDate,
                msgId = entity.F_MsgId
            };

            return Success(jsonData);
        }
        /// <summary>
        /// 添加一条最近的联系人
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public Response AddContact(dynamic _)
        {
            string otherUserId = this.GetReqData();// 获取模板请求数据
            IMContactsEntity entity = new IMContactsEntity();
            entity.F_MyUserId = userInfo.userId;
            entity.F_OtherUserId = otherUserId;
            iMContactsIBLL.SaveEntity(entity);
            return Success("添加成功！");
        }
        /// <summary>
        /// 更新消息读取状态
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public Response UpdateContactState(dynamic _)
        {
            string otherUserId = this.GetReqData();// 获取模板请求数据
            iMContactsIBLL.UpdateState(userInfo.userId, otherUserId);
            return Success("更新成功！");
        }

        /// <summary>
        /// 获取最近10条聊天记录
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public Response GetMsgList(dynamic _) {
            string otherUserId = this.GetReqData();// 获取模板请求数据
            var data = iMMsgIBLL.GetList(userInfo.userId, otherUserId);
            return Success(data);
        }

        /// <summary>
        /// 获取小于某时间点的5条记录
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public Response GetMsgList1(dynamic _)
        {
            MsgReqModel msgReqModel = this.GetReqData<MsgReqModel>();// 获取模板请求数据
            var data = iMMsgIBLL.GetListByTime(userInfo.userId, msgReqModel.otherUserId, msgReqModel.time);
            return Success(data);
        }
        /// <summary>
        /// 获取大于某时间点的所有数据
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public Response GetMsgList2(dynamic _)
        {
            MsgReqModel msgReqModel = this.GetReqData<MsgReqModel>();// 获取模板请求数据
            var data = iMMsgIBLL.GetListByTime2(userInfo.userId, msgReqModel.otherUserId, msgReqModel.time);
            return Success(data);
        }
    }

    public class MsgModel {
        /// <summary>
        /// 发送给人员Id 
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string content { get; set; }
    }


    public class MsgReqModel
    {
        /// <summary>
        /// 发送给人员Id 
        /// </summary>
        public string otherUserId { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public DateTime time { get; set; }
    }


}