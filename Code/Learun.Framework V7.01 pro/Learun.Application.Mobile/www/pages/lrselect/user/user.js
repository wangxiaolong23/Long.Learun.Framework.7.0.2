/*页面js模板,必须有init方法*/
(function () {
    var companyMap;
    var departmentMap;
    var userMap;
    var userData = {};


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
        init: function ($page, param) {
            companyMap = {};
            departmentMap = {};
            userMap = {};

            // 人员列表数据初始化
            learun.clientdata.getAll('user', {
                callback: function (data) {
                    userData = data;
                    $.each(data, function (_id, _item) {
                        _item.id = _id;
                        if (_item.departmentId) {
                            userMap[_item.departmentId] = userMap[_item.departmentId] || [];
                            userMap[_item.departmentId].push(_item);
                        }
                        else if (_item.companyId) {
                            userMap[_item.companyId] = userMap[_item.companyId] || [];
                            userMap[_item.companyId].push(_item);
                        }
                    });
                    var $list = $page.find('#lr_select_user_list');
                    if (param.op.departmentId) {
                        userData = {};
                        // 加载人员
                        $.each(userMap[param.op.departmentId] || [], function (_index, _item) {
                            userData[_item.id] = _item;
                            var _html = '\
                            <div class="lr-list-item user lr-list-item-onlyuser"  data-value="'+ _item.id + '"  >\
                                <img src="'+ getHeadImg(_item) + '"  >\
                                <span >' + _item.name + '</span>\
                            </div>';
                            $list.append(_html);
                        });
                    }
                    else if (param.op.companyId) {
                        userData = {};
                        // 加载人员
                        $.each(userMap[param.op.companyId] || [], function (_index, _item) {
                            userData[_item.id] = _item;
                            var _html = '\
                            <div class="lr-list-item user lr-list-item-onlyuser"  data-value="'+ _item.id + '"  >\
                                <img src="'+ getHeadImg(_item) + '"  >\
                                <span >' + _item.name + '</span>\
                            </div>';
                            $list.append(_html);
                        });
                    }
                    else {
                        // 公司列表数据初始化
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

                                // 部门列表数据初始化
                                learun.clientdata.getAll('department', {
                                    callback: function (data) {
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

                                    }
                                });
                            }
                        });

                    }


                }
            });

           
            // 注册点击事件
            $('#lr_select_user_list').on('tap', function (e) {
                e = e || window.event;
                var et = e.target || e.srcElement;
                var $et = $(et);
                if (et.tagName === 'IMG' || et.tagName === 'SPAN') {
                    $et = $et.parent();
                }

                var $list = $('<div class="lr-user-list" ></div>');
                var flag = false;
                var id = $et.attr('data-value');

                if ($et.hasClass('company')) {
                    if ($et.hasClass('bottom')) {
                        $et.removeClass('bottom');
                        $et.parent().find('.lr-user-list').remove();
                    }
                    else {
                        $list.css({ 'padding-left': '10px' });
                        // 加载人员
                        $.each(userMap[id] || [], function (_index, _item) {
                            var _html = '\
                            <div class="lr-list-item user"  data-value="'+ _item.id + '"  >\
                                <img src="'+ getHeadImg(_item) + '"  >\
                                <span >' + _item.name + '</span>\
                            </div>';

                            $list.append(_html);
                            flag = true;
                        });
                        // 加载部门
                        $.each(departmentMap[id] || [], function (_index, _item) {
                            var _html = '\
                            <div class="lr-list-item" >\
                                <a class="lr-nav-left department" data-value="'+ _item.id + '" >' + _item.name + '</a>\
                            </div>';

                            $list.append(_html);
                            flag = true;
                        });
                        // 加载公司
                        $.each(companyMap[id] || [], function (_index, _item) {
                            var _html = '\
                            <div class="lr-list-item" >\
                                <a class="lr-nav-left company" data-value="'+ _item.id + '" >' + _item.name + '</a>\
                            </div>';

                            $list.append(_html);
                            flag = true;
                        });

                        if (flag) {
                            $et.parent().append($list);
                        }
                        $et.addClass('bottom');
                    }
                    $list = null;
                    return false;
                }
                else if ($et.hasClass('department')) {
                    if ($et.hasClass('bottom')) {
                        $et.removeClass('bottom');
                        $et.parent().find('.lr-user-list').remove();
                    }
                    else {
                        $list.css({ 'padding-left': '10px' });
                        // 加载人员
                        $.each(userMap[id] || [], function (_index, _item) {
                            var _html = '\
                            <div class="lr-list-item user"  data-value="'+ _item.id + '"  >\
                                <img src="'+ getHeadImg(_item) + '"  >\
                                <span >' + _item.name + '</span>\
                            </div>';

                            $list.append(_html);
                            flag = true;
                        });
                        // 加载部门
                        $.each(departmentMap[id] || [], function (_index, _item) {
                            var _html = '\
                            <div class="lr-list-item" >\
                                <a class="lr-nav-left department" data-value="'+ _item.id + '" >' + _item.name + '</a>\
                            </div>';

                            $list.append(_html);
                            flag = true;
                        });

                        if (flag) {
                            $et.parent().append($list);
                        }
                        $et.addClass('bottom');
                    }
                    $list = null;
                    return false;
                }
                else if ($et.hasClass('user')) {
                    param.callback(userData[id], param.op, param.$this);
                    learun.nav.closeCurrent();
                    $list = null;
                    return false;
                }

            });

            // 搜索事件
            $page.find('input').on('input propertychange', function () {
                var keyword = $(this).val();
                var $list = $('#lr_select_user_list');
                if (keyword) {
                    $list.html("");
                    $.each(userData, function (_index, _item) {
                        if (_item.name.indexOf(keyword) !== -1) {
                            var _html = '\
                            <div class="lr-list-item user lr-list-item-onlyuser"  data-value="'+ _item.id + '"  >\
                                <img src="'+ getHeadImg(_item) + '"  >\
                                <span >' + _item.name + '</span>\
                            </div>';
                            $list.append(_html);
                        }
                    });
                }
                else {
                    $list.html("");
                    if (param.op.departmentId) {
                        // 加载人员
                        $.each(userMap[param.op.departmentId] || [], function (_index, _item) {
                            var _html = '\
                            <div class="lr-list-item user lr-list-item-onlyuser"  data-value="'+ _item.id + '"  >\
                                <img src="'+ getHeadImg(_item) + '"  >\
                                <span >' + _item.name + '</span>\
                            </div>';
                            $list.append(_html);
                        });
                    }
                    else if (param.op.companyId) {
                        // 加载人员
                        $.each(userMap[param.op.companyId] || [], function (_index, _item) {
                            var _html = '\
                            <div class="lr-list-item user lr-list-item-onlyuser"  data-value="'+ _item.id + '"  >\
                                <img src="'+ getHeadImg(_item) + '"  >\
                                <span >' + _item.name + '</span>\
                            </div>';
                            $list.append(_html);
                        });
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
            companyMap = null;
            departmentMap = null;
            userMap = null;
        }
    };
    return page;
})();
