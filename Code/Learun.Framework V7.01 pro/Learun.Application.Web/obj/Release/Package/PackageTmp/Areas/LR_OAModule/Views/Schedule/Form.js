/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.11.11
 * 描 述：日程管理
 */
var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            //开始时间
            $('#F_StartTime').lrselect({
                maxHeight: 150
            });
            //结束时间
            $('#F_EndTime').lrselect({
                maxHeight: 150
            });
        },
        initData: function () {
            $("#F_StartDate").val(request('startDate'));
            $("#F_StartTime").lrselectSet(request('startTime'));
        }
    };
    acceptClick = function () {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var postData = $('#form').lrGetFormData("");
        $.lrSaveForm(top.$.rootUrl + '/LR_OAModule/Schedule/SaveForm?keyValue=', postData, function (res) {
            learun.frameTab.currentIframe().location.reload();
        });
    }
    page.init();
}


