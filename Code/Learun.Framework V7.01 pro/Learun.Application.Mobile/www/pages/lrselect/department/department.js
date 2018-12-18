(function () {
    var companyMap = {};
    var departmentMap = {};
    var departmentData = {};

    var page = {
        isScroll: true,
        loadDepartment: function (map, id, $list) {
            $.each(map[id] || [], function (_index, _item) {
                departmentData[_item.id] = _item;
                var _$html = $('\
                        <div class="lr-list-item" >\
                            <a class="lr-nav-left department" data-value="'+ _item.id + '" >【部门】 ' + _item.name + '</a>\
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
            // 部门列表数据初始化
            learun.clientdata.getAll('department', {
                callback: function (data) {
                    departmentData = data;
                    var $list = $page.find('#lr_select_department_list');
                    $.each(data, function (_id, _item) {
                        _item.id = _id;
                        if (_item.parentId === "0") {
                            departmentMap[_item.companyId] = departmentMap[_item.companyId] || [];
                            departmentMap[_item.companyId].push(_item);
                        }
                        else {
                            departmentMap[_item.parentId] = departmentMap[_item.parentId] || [];
                            departmentMap[_item.parentId].push(_item);
                        }
                    });

                    if (param.op.departmentId) {
                        departmentData = {};
                        page.loadDepartment(departmentMap, param.op.departmentId, $list);
                        $list = null;
                    }
                    else if (param.op.companyId) {
                        departmentData = {};
                        page.loadDepartment(departmentMap, param.op.companyId, $list);
                        $list = null;
                    }
                    else {
                        learun.clientdata.getAll('company', {
                            callback: function (data) {
                                $.each(data, function (_id, _item) {
                                    companyMap[_item.parentId] = companyMap[_item.parentId] || [];
                                    _item.id = _id;
                                    companyMap[_item.parentId].push(_item);
                                });
                                $.each(companyMap["0"], function (_index, _item) {
                                    var _html = '\
                                    <div class="lr-list-item" >\
                                        <a class="lr-nav-left company" data-value="'+ _item.id + '" >' + _item.name + '</a>\
                                    </div>';
                                    $list.append(_html);
                                });
                                $list = null;
                            }
                        });
                    }
                }
            });



            // 注册点击事件
            $page.find('#lr_select_department_list').on('tap', function (e) {
                e = e || window.event;
                var et = e.target || e.srcElement;
                var $et = $(et);
                if (et.tagName === 'IMG' || et.tagName === 'SPAN') {
                    $et = $et.parent();
                }

                if ($et.hasClass('company')) {
                    if ($et.hasClass('bottom')) {
                        $et.removeClass('bottom');
                        $et.parent().find('.lr-user-list').remove();
                    }
                    else {
                        var id = $et.attr('data-value');
                        var $list = $('<div class="lr-user-list" ></div>');
                        $list.css({ 'padding-left': '10px' });
                        // 加载部门
                        page.loadDepartment(departmentMap, id, $list);
                        // 加载公司
                        $.each(companyMap[id] || [], function (_index, _item) {
                            var _html = '\
                            <div class="lr-list-item" >\
                                <a class="lr-nav-left company" data-value="'+ _item.id + '" >' + _item.name + '</a>\
                            </div>';

                            $list.append(_html);
                        });

                        if (companyMap[id] || departmentMap[id]) {
                            $et.parent().append($list);
                        }
                        $et.addClass('bottom');
                    }
                    return false;
                }
                else if ($et.hasClass('department')) {
                    param.callback(departmentData[$et.attr('data-value')], param.op, param.$this);
                    learun.nav.closeCurrent();
                    return false;
                }
            });
            // 搜索事件
            $page.find('input').on('input propertychange', function () {
                var keyword = $(this).val();
                var $list = $('#lr_select_department_list');
                if (keyword) {
                    $list.html("");
                    $.each(departmentData, function (_index, _item) {
                        if (_item.name.indexOf(keyword) !== -1) {
                            var _html = '\
                            <div class="lr-list-item" >\
                                <a class="lr-nav-left department" data-value="'+ _item.id + '" >【部门】' + _item.name + '</a>\
                            </div>';
                            $list.append(_html);
                        }
                    });
                }
                else {
                    $list.html("");

                    if (param.op.departmentId) {
                        page.loadDepartment(departmentMap, param.op.departmentId, $list);
                    }
                    else if (param.op.companyId) {
                        page.loadDepartment(departmentMap, param.op.companyId, $list);
                    }
                    else {
                        $.each(companyMap["0"], function (_index, _item) {
                            var _html = '\
                            <div class="lr-list-item" >\
                                <a class="lr-nav-left company" data-value="'+ _item.id + '" >' + _item.name + '</a>\
                            </div>';
                            $list.append(_html);
                        });
                    }
                }
            });
        },
        destroy: function (pageinfo) {
            companyMap = {};
            departmentMap = {};
            departmentData = null;
        }
    };
    return page;
})();