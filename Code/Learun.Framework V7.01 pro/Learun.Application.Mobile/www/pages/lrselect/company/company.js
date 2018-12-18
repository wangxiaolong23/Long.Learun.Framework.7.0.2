(function () {
    var companyMap = {};
    var companyData = {};

    var page = {
        isScroll: true,
        loadCompany: function (map, id, $list) {
            $.each(map[id], function (_index, _item) {
                companyData[_item.id] = _item;
                var _$html = $('\
                        <div class="lr-list-item" >\
                            <a class="lr-nav-left company" data-value="'+ _item.id + '" >' + _item.name + '</a>\
                        </div>');
                $list.append(_$html);
                if (map[_item.id] && map[_item.id].length > 0) {
                    _$html.find('a').addClass('bottom');
                    var _$list = $('<div class="lr-user-list" ></div>');
                    $list.css({ 'padding-left': '10px' });
                    page.loadCompany(map, _item.id, _$list);
                    _$html.append(_$list);
                }
            });
        },
        init: function ($page, param) {
            learun.clientdata.getAll('company', {
                callback: function (data) {
                    $.each(data, function (_id, _item) {
                        companyMap[_item.parentId] = companyMap[_item.parentId] || [];
                        _item.id = _id;
                        companyMap[_item.parentId].push(_item);
                    });
                    var $list = $page.find('#lr_select_company_list');
                    page.loadCompany(companyMap, param.op.companyId || '0', $list);
                }
            });
            // 注册点击事件
            $page.find('#lr_select_company_list').on('tap', function (e) {
                e = e || window.event;
                var et = e.target || e.srcElement;
                var $et = $(et);
                if (et.tagName === 'IMG' || et.tagName === 'SPAN') {
                    $et = $et.parent();
                }

                if ($et.hasClass('company')) {
                    var id = $et.attr('data-value');
                    param.callback(companyData[id], param.op, param.$this);
                    learun.nav.closeCurrent();
                    return false;
                }
            });
            $page.find('input').on('input propertychange', function () {
                var keyword = $(this).val();
                var $list = $('#lr_select_company_list');
                if (keyword) {
                    $list.html("");
                    $.each(companyData, function (_index, _item) {
                        if (_item.name.indexOf(keyword) !== -1) {
                            var _html = '\
                            <div class="lr-list-item" >\
                                <a class="lr-nav-left company" data-value="'+ _item.id + '" >' + _item.name + '</a>\
                            </div>';
                            $list.append(_html);
                        }
                    });
                }
                else {
                    $list.html("");
                    page.loadCompany(companyMap, param.op.companyId || '0', $list);
                }
            });
        },
        destroy: function (pageinfo) {
            companyMap = {};
            companyData = {};
        }
    };  
    return page;
})();