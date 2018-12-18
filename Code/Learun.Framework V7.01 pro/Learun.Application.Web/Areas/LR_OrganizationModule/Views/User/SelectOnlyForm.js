/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.18
 * 描 述：人员选择	
 */
var acceptClick;
var dfopid = request('dfopid');
var userName = '';
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.bind();
        },
        bind: function () {
            $('#userId').lrselect({
                value: 'F_UserId',
                text: 'F_RealName',
                title: 'F_RealName',
                // 展开最大高度
                maxHeight: 110,
                // 是否允许搜索
                allowSearch: true,
                select: function (item) {
                    if (!!item) {
                        userName = item.F_RealName;
                    }
                    else {
                        userName = '';
                    }
                }
            });
            $('#department').lrDepartmentSelect({
                maxHeight: 150
            }).on('change', function () {
                var value = $(this).lrselectGet();
                $('#userId').lrselectRefresh({
                    url: top.$.rootUrl + '/LR_OrganizationModule/User/GetList',
                    param: { departmentId: value }
                });
            });
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var formData = $('#form').lrGetFormData();
        var postitem = { value: formData.userId, text: userName };
        callBack(postitem, dfopid);
        return true;
    };
    page.init();
}