/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.18
 * 描 述：成员添加
 */
var objectId = request('objectId');
var category = request('category');

var companyId = request('companyId');
var departmentId = request('departmentId');

var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";


    var userlist = {};
    var userlistselected = [];
    var userlistselectedobj = {};

    // 渲染用户列表
    function renderUserlist(list) {
        var $warp = $('<div></div>');
        for (var i = 0, l = list.length; i < l; i++) {
            var item = list[i];
            var active = "";
            var imgName = "UserCard02.png";
            if (item.F_Gender == 0) {
                imgName = "UserCard01.png";
            }
            if (userlistselected.indexOf(item.F_UserId) != -1) {
                active = "active";
            }
            var _cardbox = "";
            _cardbox += '<div class="card-box ' + active + '" data-value="' + item.F_UserId + '" >';
            _cardbox += '    <div class="card-box-img">';
            _cardbox += '        <img src="' + top.$.rootUrl + '/Content/images/' + imgName + '" />';
            _cardbox += '    </div>';
            _cardbox += '    <div class="card-box-content">';
            _cardbox += '        <p>账户：' + item.F_Account + '</p>';
            _cardbox += '        <p>姓名：' + item.F_RealName + '</p>';
            _cardbox += '        <p>部门：<span data-id="' + item.F_DepartmentId + '"></span></p>';
            _cardbox += '    </div>';
            _cardbox += '</div>';
            var $cardbox = $(_cardbox);
            $cardbox[0].userinfo = item;
            $warp.append($cardbox);
            learun.clientdata.getAsync('department', {
                key: item.F_DepartmentId,
                callback: function (_data,op) {
                    $warp.find('[data-id="' + op.key + '"]').text(_data.name);
                }
            });
        }
        $warp.find('.card-box').on('click', function () {
            var $this = $(this);
            var userid = $this.attr('data-value');
            if ($this.hasClass('active')) {
                $this.removeClass('active');
                removeUser(userid);
                userlistselected.splice(userlistselected.indexOf(userid), 1);

            }
            else {
                $this.addClass('active');
                userlistselectedobj[userid] = $this[0].userinfo;
                userlistselected.push(userid);
                addUser($this[0].userinfo);
            }
        });

        $('#user_list').html($warp);
    };

    function addUser(useritem) {
        var $warp = $('#selected_user_list');
        var _html = '<div class="user-selected-box" data-value="' + useritem.F_UserId + '" >';
        _html += '<p><span data-id="' + useritem.F_CompanyId + '"></span></p>';
        _html += '<p><span data-id="' + useritem.F_DepartmentId + '"></span>【' + useritem.F_RealName + '】</p>';
        _html += '<span class="user-reomve" title="移除选中人员"></span>';
        _html += '</div>';
        $warp.append(_html);
        learun.clientdata.getAsync('department', {
            key: useritem.F_DepartmentId,
            callback: function (_data,op) {
                $warp.find('[data-id="' + op.key + '"]').text(_data.name);
            }
        });
        learun.clientdata.getAsync('company', {
            key: useritem.F_CompanyId,
            callback: function (_data,op) {
                $warp.find('[data-id="' + op.key + '"]').text(_data.name);
            }
        });
    };
    function removeUser(userid) {
        var $warp = $('#selected_user_list');
        $warp.find('[data-value="' + userid + '"]').remove();
    };

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            // 部门
            $('#department_tree').lrtree({
                nodeClick: function (item) {
                    departmentId = item.id;
                    if (!!userlist[item.id]) {
                        renderUserlist(userlist[item.id]);
                    }
                    else {
                        learun.httpAsync('GET', top.$.rootUrl + '/LR_OrganizationModule/User/GetList', { companyId: companyId, departmentId: departmentId }, function (data) {
                            userlist[item.id] = data || [];
                            renderUserlist(userlist[item.id]);
                        });
                    }
                }
            });
            // 公司
            $('#company_select').lrCompanySelect({ isLocal: true }).bind('change', function () {
                companyId = $(this).lrselectGet();
                $('#department_tree').lrtreeSet('refresh', {
                    url: top.$.rootUrl + '/LR_OrganizationModule/Department/GetTree',
                    // 访问数据接口参数
                    param: { companyId: companyId},
                });
            });
            // 已选人员按钮
            $('#user_selected_btn').on('click', function () {
                $('#form_warp_right').animate({ right: '0px' }, 300);
            });
            $('#user_selected_btn_close').on('click', function () {
                $('#form_warp_right').animate({ right: '-180px' }, 300);
            });
            // 搜索
            $("#txt_keyword").keydown(function (event) {
                if (event.keyCode == 13) {
                    var keyword = $(this).val();
                    if (keyword != "") {
                        learun.httpAsync('GET', top.$.rootUrl + '/LR_OrganizationModule/User/GetList', { companyId: companyId, keyword: keyword }, function (data) {
                            renderUserlist(data || []);
                        });
                    }
                    else {
                        var data = userlist[departmentId] || [];
                        renderUserlist(data);
                    }
                }
            });

            // 选中人员按钮点击事件
            $('#selected_user_list').on('click', function (e) {
                var et = e.target || e.srcElement;
                var $et = $(et);
                if ($et.hasClass('user-reomve')) {
                    var userid = $et.parent().attr('data-value');
                    removeUser(userid);
                    userlistselected.splice(userlistselected.indexOf(userid), 1);
                    $('#user_list').find('[data-value="' + userid + '"]').removeClass('active');
                }
            });

            // 滚动条
            $('#user_list_warp').lrscroll();
            $('#selected_user_list_warp').lrscroll();
        },
        initData: function () {
            if (!!companyId) {
                $('#company_select').lrselectSet(companyId);
            }
            if (!!departmentId) {
                $('#department_tree').lrtreeSet('setValue', departmentId);
            }

            $.lrSetForm(top.$.rootUrl + '/LR_AuthorizeModule/UserRelation/GetUserIdList?objectId=' + objectId, function (data) {
                if (data.userIds == "") {
                    return false;
                }
                var $warp = $('#selected_user_list');
                $.each(data.userInfoList, function (id, item) {
                    if (item) {
                        userlistselectedobj[item.F_UserId] = item;
                    }
                });
                var userList = data.userIds.split(',');
                for (var i = 0, l = userList.length; i < l; i++) {
                    var userId = userList[i];
                    var item = userlistselectedobj[userId];
                    if (!!item) {
                        if (userlistselected.indexOf(userId) == -1) {
                            userlistselected.push(userId);
                        }

                        var _html = '<div class="user-selected-box" data-value="' + item.F_UserId + '" >';
                        _html += '<p><span data-id="' + item.F_CompanyId + '"></span></p>';
                        _html += '<p><span data-id="' + item.F_DepartmentId + '"></span>【' + item.F_RealName + '】</p>';
                        _html += '<span class="user-reomve" title="移除选中人员"></span>';
                        _html += '</div>';
                        $warp.append($(_html));
                        learun.clientdata.getAsync('department', {
                            key: item.F_DepartmentId,
                            callback: function (_data,op) {
                                $warp.find('[data-id="' + op.key + '"]').text(_data.name);
                            }
                        });
                        learun.clientdata.getAsync('company', {
                            key: item.F_CompanyId,
                            callback: function (_data,op) {
                                $warp.find('[data-id="' + op.key + '"]').text(_data.name);
                            }
                        });
                        $('#user_list').find('[data-value="' + item.F_UserId + '"]').addClass('active');
                    }
                }
            });
        }
    };
    // 保存数据
    acceptClick = function () {
        $.lrSaveForm(top.$.rootUrl + '/LR_AuthorizeModule/UserRelation/SaveForm', { objectId: objectId, category: category, userIds: String(userlistselected) }, function (res) { });
        return true;
    };
    page.init();
}