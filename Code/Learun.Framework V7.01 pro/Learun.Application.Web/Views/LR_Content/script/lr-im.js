(function ($, learun) {
    "use strict";

    var msgList = {};
    var imChat;
    var isLoaded = -1;
    var loadingMsg = {};
    var loadingMsg2 = {};

    var _im = {
        init: function () {
            _im.registerServer();
            _im.connect();
        }
        // 连接服务端
        , connect: function () {
            var loginInfo = learun.clientdata.get(['userinfo']);
            $.ajax({
                url: loginInfo.imUrl + "/hubs",
                type: "get",
                dataType: "text",
                success: function (data) {
                    eval(data);
                    //Set the hubs URL for the connection
                    $.connection.hub.url = loginInfo.imUrl;
                    $.connection.hub.qs = { "userId": loginInfo.userId };
                    // Declare a proxy to reference the hub.
                    imChat = $.connection.ChatsHub;
                    _im.registerClient();
                    // 连接成功后注册服务器方法
                    $.connection.hub.start().done(function () {
                        _im.afterSuccess();
                    });
                    //断开连接后
                    $.connection.hub.disconnected(function () {
                        _im.disconnected();
                    });
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    isLoaded = 0;
                },
            });
        }
        // 连接成功后执行方法
        , afterSuccess: function () {
            isLoaded = 1;
            $('.lr-im-bell').show();
        }
        // 断开连接后执行
        , disconnected: function () {
            isLoaded = 0;
        }
        // 注册客户端方法
        , registerClient: function () {
            if (imChat) {
                //接收消息
                imChat.client.revMsg = function (userId, msg, dateTime, isSystem) {
                    if (!loadingMsg2[userId]) {
                        var point = { userId: userId, content: msg, time: dateTime, isSystem: isSystem || 0 };
                        addMsgList(userId, point);
                        learun.im.revMsg && learun.im.revMsg(userId, msg, dateTime, isSystem || 0);
                    }
                }
            }
        }
        // 注册服务端方法
        ,registerServer: function () {
            // 发送信息
            _im.sendMsg = function (userId, msg) {
                if (isLoaded == 1) {
                    imChat.server.sendMsg(userId, msg, 0);
                }
                else if (isLoaded == -1) {
                    setTimeout(function () {
                        _im.sendMsg(userId, msg);
                    }, 100);
                }
            };
        }
    };


    function addMsgList(userId, item) {
        msgList[userId] = msgList[userId] || [];
        if (loadingMsg[userId]) {
            setTimeout(function () {
                addMsgList(userId, item);
            }, 100);
        }
        else {
            msgList[userId].push(item);
        }
    }

    learun.im = {
        addContacts: function (userId) {// 添加联系人
            learun.httpAsync('Post', top.$.rootUrl + '/LR_IM/IMMsg/AddContact', { otherUserId: userId }, function (data) {});
        },
        getContacts: function (callback) {// 获取最近的联系人列表
            setTimeout(function () {
                learun.httpAsync('GET', top.$.rootUrl + '/LR_IM/IMMsg/GetContactsList', {}, function (data) {
                    data = data || [];
                    _im.init();
                    callback(data);
                });
            }, 1000);
        },
        updateContacts: function (userId) {
            learun.httpAsync('Post', top.$.rootUrl + '/LR_IM/IMMsg/UpdateContactState', { otherUserId: userId}, function (data) {
            });
        },
        sendMsg: function (userId, msg) {// 发送消息
            var time = "";
            var loginInfo = learun.clientdata.get(['userinfo']);
            var point = { userId: loginInfo.userId, content: msg, time: learun.getDate('yyyy-MM-dd hh:mm:ss'), isSystem: 0 };
            addMsgList(userId, point);
            learun.httpAsync('Post', top.$.rootUrl + '/LR_IM/IMMsg/SendMsg', { userId: userId, content: msg }, function (data) {
                _im.sendMsg(userId, msg);// 发送给即时通讯服务
            });
            if (msgList[userId].length > 1) {
                if (learun.parseDate(point.time).DateDiff('s', msgList[userId][msgList[userId].length - 2].time) > 60) {
                    time = point.time;
                }
            }
            else {
                time = point.time;
            }
            return time;
        },
        getMsgList: function (userId, callback,isGetMsgList) {
            msgList[userId] = msgList[userId] || [];
            loadingMsg[userId] = true;
            if (msgList[userId].length == 0 && isGetMsgList) {// 如果没有信息，获取最近10条的聊天记录
                loadingMsg2[userId] = true;
                learun.httpAsync('GET', top.$.rootUrl + '/LR_IM/IMMsg/GetMsgList', { userId: userId }, function (data) {
                    msgList[userId] = msgList[userId] || [];
                    data = data || [];
                    var len = data;
                    if (len > 0) {
                        for (var i = len - 1; i >= 0; i--) {
                            var item = data[i];
                            var point = { userId: _item.F_SendUserId, content: _item.F_Content, time: _item.F_CreateDate, isSystem: _item.F_IsSystem || 0 };
                            msgList[userId].push(point);
                        }
                    }
                    callback(msgList[userId]);
                    loadingMsg[userId] = false;
                    loadingMsg2[userId] = false;
                });
            }
            else {
                callback(msgList[userId]);
                loadingMsg[userId] = false;
            }
        },
        registerRevMsg: function (callback) {// 获取消息记录
            learun.im.revMsg = callback;
        }
    };

})(jQuery, top.learun);