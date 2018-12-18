/* * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2018-03-12 11:52
 * 描  述：添加常用字段
 */
var acceptClick;
var keyValue = request('keyValue');

var bootstrap = function ($, learun) {
    "use strict";

    var selectedRow = learun.frameTab.currentIframe().selectedRow;

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            $('#F_DataType').lrDataItemSelect({ code: 'DbFieldType', maxHeight: 100 });
        },
        /*初始化数据*/
        initData: function () {
            if (!!keyValue) {
                console.log(selectedRow);
                $('#form').lrSetFormData(selectedRow);
            }
        },

    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var postData = $('#form').lrGetFormData();
        $.lrSaveForm(top.$.rootUrl + '/LR_SystemModule/DbField/SaveForm?keyValue=' + keyValue, postData, function (res) {
            callBack();
        });
    };
    page.init();
}

