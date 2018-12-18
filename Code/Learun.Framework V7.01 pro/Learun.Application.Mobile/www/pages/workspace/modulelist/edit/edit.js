(function () {

    var page = {
        isScroll: true,
        init: function ($page) {
            var _html = '<div id="lr_modulelist_finish" >完成</div>';
            $page.parent().find('.f-page-backbtn').html("取消");
            $page.parent().find('.f-page-header').addClass('lr-modulelistedit-header').append(_html);

            learun.clientdata.get('module', {
                callback: function (data) {
                    var map = {};
                    var TypeMap = {};
                    $.each(data, function (_index, _item) {
                        map[_item.F_Id] = _item;
                        TypeMap[_item.F_Type] = TypeMap[_item.F_Type] || [];
                        TypeMap[_item.F_Type].push(_item);
                    });
                    // 加载我的应用
                    learun.myModule.get(data, function (myModules) {
                        var $appbox = $page.find('#lr_modulelist_edit_mymodule');
                        var myMap = {};
                        $.each(myModules, function (_index, _id) {
                            var item = map[_id];
                            if (item) {
                                var _html = '\
                                    <div class="appitem myappitem" data-value="'+ item.F_Id + '">\
                                        <div><i class="'+ item.F_Icon + '"></i></div>\
                                        <span>'+ item.F_Name + '</span>\
                                        <div class="operation minus">-</div>\
                                    </div>';
                                var _$html = $(_html);
                                _$html[0].item = item;
                                $appbox.append(_$html);
                                myMap[_id] = "1";
                            }
                        });

                        // 加载全部应用
                        var $app = $page.find('.lr-modulelist-page');
                        $.each(TypeMap, function (_type, _modules) {
                            var _html = '\
                        <div class="lr-app-panel" data-app="'+ _type + '">\
                            <div class="title" ></div>\
                            <div class="content"></div>\
                        </div>';
                            $app.append(_html);
                            var $content = $app.find('[data-app="' + _type + '"] .content');
                            learun.clientdata.get('dataItem', {
                                code: 'function',
                                key: _type,
                                callback: function (data, op) {
                                    $app.find('[data-app="' + op.key + '"] .title').text(data.text);
                                }
                            });
                            $.each(_modules, function (_index, _item) {
                                var __html = '\
                                <div class="appitem" data-value="'+ _item.F_Id + '">\
                                    <div><i class="'+ _item.F_Icon + '"></i></div>\
                                    <span>'+ _item.F_Name + '</span>' + (myMap[_item.F_Id] ? ' <div class="operation minus">-</div>' :' <div class="operation plus">+</div>') +'\
                                </div>';
                                var __$html = $(__html);
                                __$html[0].item = _item;
                                $content.append(__$html);
                            });
                        });

                    });


                   


                }
            });

            $page.delegate('.appitem', 'tap', function () {
                var $this = $(this);
                var Id = $this.attr('data-value');
                if ($this.hasClass('myappitem')) {
                    $this.remove();
                    $page.find('[data-value="' + Id + '"] .operation').removeClass('minus').addClass('plus').text('+');
                }
                else {
                    if ($this.find('.operation').hasClass('minus')) {// 去掉我的应用
                        $this.find('.operation').removeClass('minus').addClass('plus').text('+');
                        $page.find('#lr_modulelist_edit_mymodule .appitem[data-value="' + Id + '"]').remove();
                    }
                    else {// 添加我的应用
                        var len = $page.find('#lr_modulelist_edit_mymodule .appitem').length;
                        if (len >= 11) {
                            learun.layer.toast('最多添加11个应用');
                        }
                        else {
                            $this.find('.operation').removeClass('plus').addClass('minus').text('-');
                            var _html = '<div class="appitem myappitem" data-value="' + Id + '">' + $this.html() + '</div>';
                            $page.find('#lr_modulelist_edit_mymodule').append(_html);
                        }
                    }
                }
            });

            // 注册完成按钮
            $page.parent().find('#lr_modulelist_finish').on('tap', function () {
                var list = [];
                $page.find('#lr_modulelist_edit_mymodule .appitem').each(function () {
                    var id = $(this).attr('data-value');
                    list.push(id);
                });
                learun.storage.set("mymoduleData", list);
                learun.httppost(config.webapi + "learun/adms/function/mylist/update", String(list), (res) => {
                });
                learun.nav.closeCurrent();
            });
        }
    };
    return page;
})();