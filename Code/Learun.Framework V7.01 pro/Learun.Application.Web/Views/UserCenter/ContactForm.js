/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.11
 * 描 述：个人中心-联系方式	
 */
var baseinfo;
var bootstrap = function ($, learun) {
    "use strict";

    var getBaseinfo = function (callback) {
        baseinfo = learun.frameTab.currentIframe().baseinfo;
        if (!baseinfo) {
            setTimeout(function () { getBaseinfo(callback) }, 100);
        }
        else {
            callback();
        }
    };

    var page = {
        init: function () {
            getBaseinfo(function () {
                page.bind();
                page.initData();
            });
        },
        bind: function () {
            $('#lr_save_btn').on('click', function () {
                var postData = $('#form').lrGetFormData();
                postData.F_CompanyId = baseinfo.companyId;
                postData.F_Account = baseinfo.account;
                $.lrSaveForm(top.$.rootUrl + '/LR_OrganizationModule/User/SaveForm?keyValue=' + baseinfo.userId, postData, function (res) { });
            });
        },
        initData: function () {
            $('#F_Mobile').val(baseinfo.mobile);
            $('#F_Telephone').val(baseinfo.telephone);
            $('#F_Email').val(baseinfo.email);
            $('#F_WeChat').val(baseinfo.weChat);
            $('#F_OICQ').val(baseinfo.oICQ);

        }
    };
    page.init();
}