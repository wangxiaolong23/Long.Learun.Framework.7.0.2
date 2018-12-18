/*页面js模板,必须有init方法*/
(function () {
    var dateTime = "";
    var timeOutId = '';

    var getTime = function (time) {
        var d = new Date();
        var c = d.DateDiff('d', time);
        if (c < 1 && c > -1) {
            return learun.date.format(time, 'hh:mm:ss');
        }
        else {
            return learun.date.format(time, 'yyyy/MM/dd');
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

    var getContacts = function () {
        learun.httpget(config.webapi + "learun/adms/im/contacts", dateTime, (data) => {
            var $list = $('#lr_im_message_list');
            if (data) {
                dateTime = data.time;
                $.each(data.data, function (_index, _item) {
                    var $item = $list.find('[data-value="' + _item.F_Id + '"]').remove();

                    var _html = '\
                        <div class="lr-list-item lr-list-item-media" data-value="'+ _item.F_Id + '" data-userId="' + _item.F_OtherUserId + '" >\
                            <img class="lr-media-head" src="images/on-boy.jpg">\
                            <div class="lr-media-body"><span>xxx</span><p class="lr-ellipsis">'+ (_item.F_Content || '') + '</p></div>\
                            <div class="date" >'+ getTime(_item.F_Time) + '</div>\
                        </div>';
                    $list.prepend(_html);

                    learun.clientdata.get('user', {
                        key: _item.F_OtherUserId,
                        callback: function (data, op) {
                            var _$item = $('#lr_im_message_list [data-userId="' + op.key + '"]');
                            data.id = op.key;
                            _$item.find('img').attr('src', getHeadImg(data));
                            _$item.find('.lr-media-body>span').text(data.name);
                        }
                    });
                });
            }
            if (!$list.is(':hidden')) {
                timeOutId = setTimeout(function () {
                    getContacts();
                }, 8000);// 8s钟遍历一次在当前界面的情况下（后续可以考虑更长的时间）
            }
            $list = null;
        });
    };

    var page = {
        isScroll: true,
        init: function ($page) {
            timeOutId = '';
            dateTime = "1888-10-10 10:10:10";
            getContacts();
            $('#lr_im_message_list').on('tap', function (e) {
                e = e || window.event;
                var et = e.target || e.srcElement;
                var $et = $(et);
                if (!$et.hasClass('lr-list-item-media')) {
                    $et = $et.parents('.lr-list-item-media');
                }
                var userId = $et.attr('data-userId');
                var userName = $et.find('.lr-media-body>span').text();
                learun.nav.go({ path: 'chat', title: userName, isBack: true, isHead: true, param: { hasHistory: true, userId: userId }, type: 'right' });

            });
        },
        reload: function ($page, pageinfo) {
            clearTimeout(timeOutId);
            getContacts();
        },
        destroy: function (pageinfo) {
            clearTimeout(timeOutId);
        }
    };
    return page;
})();