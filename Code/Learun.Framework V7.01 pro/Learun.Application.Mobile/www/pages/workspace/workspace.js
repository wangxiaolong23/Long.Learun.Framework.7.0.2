(function () {

    var custmerform = {};
    var $scroll = '';


    var page = {
        init: function ($page) {
            var _html = '';
            _html += '<div class="scanner">';
            _html += '<i class="iconfont icon-scan"></i>';
            _html += '</div>';

            _html += '\
                <div class="searchBox">\
                    <i class="iconfont icon-search"></i>\
                    <div class="search" >搜索应用</div>\
                </div>';


            _html += '<div class="message">';
            _html += '<i class="iconfont icon-mail"></i>';
            _html += '<span class="red"></span>';
            _html += '</div>';
            $page.parent().find('.f-page-header').addClass('lr-workspace-header').html(_html);
            // 点击搜索框
            $page.parent().find('.searchBox').on('tap', function () {
                learun.nav.go({ path: 'workspace/search', title: '', isBack: true, isHead: true });
            });
            // 点击消息图标
            $page.parent().find('.message').on('tap', function () {
                learun.nav.go({ path: 'message', title: '消息', isBack: true, isHead: true,type:'right' });
            });
            // 注册扫描
            $page.parent().find('.scanner').on('tap', function () {
                learun.code.scan(function (res) {
                    if (res.status === 'success') {
                        learun.layer.toast(res.msg);
                    }
                    else {
                        learun.layer.toast('扫描失败:' + res.msg);
                    }
                });
            });
            // 图片加载
            $page.find('.banner').slider({ data: ['images/banner.png'], indicator: true, interval: 10000 });


            // 基础数据初始化
            learun.clientdata.init();

            // 加载功能列表
            learun.clientdata.get('module', {
                callback: function (data) {
                    learun.myModule.get(data, function (myModules) {
                        var mylen = parseInt((myModules.length + 1) / 4) + ((myModules.length + 1) % 4 > 0 ? 1 : 0);
                        switch (mylen) {
                            case 1:
                                $page.find('.lr-workspace-page').css('padding-top', '210px');
                                break;
                            case 2:
                                $page.find('.lr-workspace-page').css('padding-top', '290px');
                                break;
                            case 3:
                                $page.find('.lr-workspace-page').css('padding-top', '370px');
                                break;
                        }

                        var map = {};
                        $.each(data, function (_index, _item) {
                            map[_item.F_Id] = _item;
                        });
                        var $appbox = $page.find('.appbox');
                        var $last = null;
                        $.each(myModules, function (_index, _id) {
                            var item = map[_id];
                            if (item) {
                                var _html = '\
                                        <div class="appitem appitem2" data-value="'+ item.F_Id + '">\
                                            <div><i class="'+ item.F_Icon + '"></i></div>\
                                            <span>'+ item.F_Name + '</span>\
                                        </div>';
                                var _$html = $(_html);
                                _$html[0].item = item;
                                if ($last === null) {
                                    $appbox.prepend(_$html);
                                }
                                else {
                                    $last.after(_$html);
                                }
                                $last = _$html;

                            }
                        });
                        $last = null;
                    });
                }
            });
            // 注册更多功能按钮
            $page.find('#lr_more_app').on('tap', function () {
                learun.nav.go({ path: 'workspace/modulelist', title: "", type: 'right' });
            });
            // 点击功能按钮
            $page.delegate('.appitem2', 'tap', function () {
                var $this = $(this);
                var item = $this[0].item;
                if (item.F_IsSystem === 1) {// 代码开发功能
                    learun.nav.go({ path: item.F_Url, title: item.F_Name, isBack: true, isHead: true, type: 'right' });
                }
                else {// 自定义表单开发功能
                    learun.nav.go({ path: 'custmerform', title: item.F_Name, param: { formSchemeId: item.F_FormId, girdScheme: item.F_Scheme }, isBack: true, isHead: true, type: 'right' });
                }
                return false;
            });
        },
        reload: function ($page, pageinfo) {
            if (learun.isOutLogin) {// 如果是重新登录的情况刷新下桌面数据
                learun.isOutLogin = false;
                learun.clientdata.clear('module');
                learun.myModule.states = -1;
                // 图片加载
                learun.httpget(config.webapi + "learun/adms/desktop/imgid", null, function (data) {
                    if (data) {
                        var _list = [];
                        $.each(data, function (_index, _item) {
                            _list.push(config.webapi + "learun/adms/desktop/img?data=" + _item);
                        });
                        $page.find('.banner').after('<div class="banner"></div>').remove();
                        $page.find('.banner').slider({ data: _list, indicator: true, interval: 10000 });
                    }
                });
            }
            // 加载功能列表
            learun.clientdata.get('module', {
                callback: function (data) {
                    learun.myModule.get(data, function (myModules) {
                        var mylen = parseInt((myModules.length + 1) / 4) + ((myModules.length + 1) % 4 > 0 ? 1 : 0);
                        switch (mylen) {
                            case 1:
                                $page.find('.lr-workspace-page').css('padding-top', '210px');
                                break;
                            case 2:
                                $page.find('.lr-workspace-page').css('padding-top', '290px');
                                break;
                            case 3:
                                $page.find('.lr-workspace-page').css('padding-top', '370px');
                                break;
                        }

                        var map = {};
                        $.each(data, function (_index, _item) {
                            map[_item.F_Id] = _item;
                        });
                        var $appbox = $page.find('.appbox');
                        var $last = null;
                        $appbox.find(".appitem2").remove();
                        $.each(myModules, function (_index, _id) {
                            var item = map[_id];
                            if (item) {
                                var _html = '\
                                        <div class="appitem appitem2" data-value="'+ item.F_Id + '">\
                                            <div><i class="'+ item.F_Icon + '"></i></div>\
                                            <span>'+ item.F_Name + '</span>\
                                        </div>';
                                var _$html = $(_html);
                                _$html[0].item = item;
                                if ($last === null) {
                                    $appbox.prepend(_$html);
                                }
                                else {
                                    $last.after(_$html);
                                }
                                $last = _$html;

                            }
                        });
                        $last = null;
                    });
                }
            });
        }
    };
    return page;
})();