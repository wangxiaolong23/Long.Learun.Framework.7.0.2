(function () {
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

    var page = {
        isScroll: true,
        init: function ($page) {
            var _html = '';
            _html += '<div class="searchbar">';
            _html += '<i class="iconfont icon-search"></i>';
            _html += '<input type="text" placeholder="搜寻关键字">';
            _html += '</div><span id="lr_contacts_search_chanel" >取消</span>';
            $page.parent().find('.f-page-header').addClass('lr-contacts-search-header').html(_html);

            $page.parent().find('#lr_contacts_search_chanel').on('tap', function () {
                learun.nav.closeCurrent();
            });
            $page.parent().find('input').select();

            $page.parent().find('input').on('input propertychange', function () {
                var keyword = $(this).val();
                var $list = $('#lr_contact_searchlist');
                if (keyword) {
                    $list.show();
                    $page.find('.lr-contacts-search-bg').hide();
                    $list.html("");
                    learun.clientdata.getAll('user', {
                        callback: function (data) {
                            $.each(data, function (_index, _item) {
                                if (_item.name.indexOf(keyword) !== -1) {
                                    _item.id = _index;
                                    var _html = '\
                                    <div class="lr-list-item"  data-value="'+ _item.id + '"  >\
                                        <img src="'+ getHeadImg(_item) + '"  >\
                                        <span >' + _item.name + '</span>\
                                    </div>';
                                    $list.append(_html);
                                }
                            });
                        }
                    });
                }
                else {
                    $page.find('.lr-contacts-search-bg').show();
                    $list.hide();
                }
            });

            $page.find('#lr_contact_searchlist').on('tap', function (e) {
                e = e || window.event;
                var et = e.target || e.srcElement;
                var $et = $(et);
                if (et.tagName === 'IMG' || et.tagName === 'SPAN') {
                    $et = $et.parent();
                }
                if ($et.hasClass('lr-list-item')) {
                    var userId = $et.attr("data-value");
                    var userName = $et.find('span').text();
                    learun.nav.go({ path: 'chat', title: userName, isBack: true, isHead: true, param: { hasHistory: true, userId: userId }, type: 'right' });
                    return false;
                }
            });
        }
    };
    return page;
})();