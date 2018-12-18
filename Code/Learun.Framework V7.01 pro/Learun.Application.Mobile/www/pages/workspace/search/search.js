(function () {

    var page = {
        isScroll: true,
        init: function ($page) {
            var _html = '';
            _html += '<div class="searchbar">';
            _html += '<i class="iconfont icon-search"></i>';
            _html += '<input type="text" placeholder="搜寻关键字">';
            _html += '</div><span id="lr_app_search_chanel" >取消</span>';
            $page.parent().find('.f-page-header').addClass('lr-app-search-header').html(_html);

            $page.parent().find('#lr_app_search_chanel').on('tap', function () {
                learun.nav.closeCurrent();
            });
            $page.parent().find('input').select();

            $page.parent().find('input').on('input propertychange', function () {
                var keyword = $(this).val();
                var $list = $('#lr_app_searchlist');
                if (keyword) {
                    $list.show();
                    $page.find('.lr-app-search-bg').hide();
                    $list.html("");
                    learun.clientdata.get('module', {
                        callback: function (data) {
                            $.each(data, function (_index, _item) {
                                if (_item.F_Name.indexOf(keyword) != -1) {
                                    var $html = $('\
                                    <div class="lr-list-item lr-list-item-icon">\
                                        <i class="'+ _item.F_Icon + '"></i>\
                                        <a class="lr-nav-right">'+ _item.F_Name + '</a>\
                                    </div>');
                                    $html[0].item = _item;
                                    $list.append($html);
                                }
                            });
                        }
                    });

                }
                else {
                    $page.find('.lr-app-search-bg').show();
                    $list.hide();
                }
            });

            $page.find('#lr_app_searchlist').on('tap', function (e) {
                e = e || window.event;

                var et = e.target || e.srcElement;
                var $et = $(et);
                if (et.tagName === 'I' || et.tagName === 'A') {
                    $et = $et.parent();
                }

                if ($et.hasClass('lr-list-item')) {
                    var item = $et[0].item;
                    if (item.F_IsSystem === 1) {// 代码开发功能
                        learun.nav.go({ path: item.F_Url, title: item.F_Name, isBack: true, isHead: true, type: 'right' });
                    }
                    else {// 自定义表单开发功能
                        learun.nav.go({ path: 'custmerform', title: item.F_Name, param: { formSchemeId: item.F_FormId, girdScheme: item.F_Scheme }, isBack: true, isHead: true, type: 'right' });
                    }
                } 

                return false;
            });
        }
    };
    return page;
})();