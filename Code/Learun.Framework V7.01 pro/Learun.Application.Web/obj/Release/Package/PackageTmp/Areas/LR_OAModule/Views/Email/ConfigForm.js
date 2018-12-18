/*
 * 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2017 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.06.05
 * 描 述：邮件配置	
 */

var acceptClick;
var keyValue = '';
var Id = '';
var bootstrap = function ($, learun) {
    "use strict";

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {

        },
        initData: function () {
            var loginInfo = learun.clientdata.get(['userinfo']); //登陆者信息
            $.lrSetForm(top.$.rootUrl + '/LR_OAModule/Email/GetConfigEntity?keyValue=' + loginInfo.userId, function (data) {//
                console.log(data);
                Id = data.F_Id;
                $('#form').lrSetFormData(data);
            });
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var postData = $('#form').lrGetFormData(keyValue);
        $.lrSaveForm(top.$.rootUrl + '/LR_OAModule/Email/SaveConfigEntity?keyValue=' + Id, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}