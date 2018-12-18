(function () {
    var beginTime = "1988-01-01 00:00:00";
    var endTime = "1988-01-01 00:00:00";
    var userId = '';
    var msgMap = {};
    var timeOutId;
    var $scroll;

    var getTime = function (time) {
        var d = new Date();
        var c = d.DateDiff('d', time);
        if (c < 1 && c > -1) {
            return learun.date.format(time, 'hh:mm:ss');
        }
        else {
            return learun.date.format(time, 'yyyy-MM-dd hh:mm');
        }
    };
    var getHeadImg = function (user) {
        var url = '';
        switch (user.img) {
            case '0':
                url += 'images/on-girl.jpg';
                break;
            case '1':
                url += 'images/on-boy.jpg';
                break;
            default:
                url += config.webapi + 'learun/adms/user/img?data=' + user.id;
                break;
        }
        return url;
    };


    var getMsgHtml = function (item) {
        var _html = '\
            <div class="chatTime"><span>'+ getTime(item.F_CreateDate) + '</span></div>\
            <div class="'+ (item.F_SendUserId === userId ? 'me' : 'other') + '">\
                <span class="arrow"></span>\
                <img src="'+ config.webapi + 'learun/adms/user/img?data=' + item.F_SendUserId + '" alt="">\
                <span class="content">'+ item.F_Content + '</span>\
            </div>';
        return _html;
    };
    // 获取最新的消息列表
    var getMsg = function (userId) {
        learun.httpget(config.webapi + "learun/adms/im/msg/list2", { otherUserId: userId, time: endTime }, (data) => {
            if (data) {
                var $list = $('#lr_chat_msgcontent .lr-chat-msgcontent-list');
                $.each(data, function (_index, _item) {
                    if (!msgMap[_item.F_MsgId]) {
                        msgMap[_item.F_MsgId] = "1";
                        var _html = getMsgHtml(_item);
                        $list.append(_html);
                        endTime = _item.F_CreateDate;
                        $scroll.refresh(true);
                        $scroll.scrollToBottom();
                    }
                });
                $list = null;
            }
            timeOutId = setTimeout(function () {
                getMsg(userId);
            }, 5000);
        });
    };

    var page = {
        init: function ($page, param) {
            $scroll = $page.find('#lr_chat_msgcontent').pullRefresh({
                down: {
                    height: 20,
                    contentinit: '',
                    contentdown: '',
                    contentover: '',
                    contentrefresh: '',
                    callback: function () {
                        var self = this;
                      
                        learun.httpget(config.webapi + "learun/adms/im/msg/list", { otherUserId: param.userId, time: beginTime }, (data) => {
                            if (data) {
                                var $list = $('#lr_chat_msgcontent .lr-chat-msgcontent-list');
                                $.each(data, function (_index, _item) {
                                    if (!msgMap[_item.F_MsgId]) {
                                        msgMap[_item.F_MsgId] = "1";
                                        var _html = getMsgHtml(_item);
                                        $list.prepend(_html);
                                        beginTime = _item.F_CreateDate;
                                    }
                                });
                                $list = null;
                            }
                            self.refresh(true);
                            self.endPulldownToRefresh();
                        });

                     
                    }
                }
            });

            userId = learun.storage.get('userinfo').baseinfo.userId;

            var $list = $('#lr_chat_msgcontent .lr-chat-msgcontent-list');

            if (param.hasHistory) {
                learun.layer.loading(true, '加载聊天信息中');
                // 先去获取最近的十条数据
                learun.httpget(config.webapi + "learun/adms/im/msg/lastlist", param.userId, (data) => {
                    learun.layer.loading(false);
                    if (data) {
                        $.each(data, function (_index, item) {
                            beginTime = item.F_CreateDate;
                            if (_index === 0) {
                                endTime = item.F_CreateDate;
                            }
                            msgMap[item.F_MsgId] = '1';
                            var _html = getMsgHtml(item);
                            $list.prepend(_html);
                        });
                    }
                    $scroll.refresh(true);
                    $scroll.scrollToBottom();
                    getMsg(param.userId);
                });
            }

            $page.find('#lr_chat_sendbtn').on('tap', function () {
                var text = $('#lr_chat_input').val();
                text = $.trim(text);
                $('#lr_chat_input').val("");
                if (text) {
                    clearTimeout(timeOutId);
                    var $list = $('#lr_chat_msgcontent .lr-chat-msgcontent-list');

                    var _html = '\
                    <div class="me">\
                        <span class="arrow"></span>\
                        <img src="'+ config.webapi + 'learun/adms/user/img?data=' + userId + '" alt="">\
                        <span class="content">'+ text + '</span>\
                    </div>';

                    $list.append(_html);
                    $scroll.refresh(true);
                    $scroll.scrollToBottom();

                    learun.httppost(config.webapi + "learun/adms/im/send", { userId: param.userId, content: text }, (data) => {
                        if (data) {
                            endTime = data.time;
                            msgMap[data.msgId] = "1";
                        }
                        getMsg(param.userId);
                    });
                }
            });
        },
        destroy: function (pageinfo) {
            clearTimeout(timeOutId);
        }
    };
    return page;
})();