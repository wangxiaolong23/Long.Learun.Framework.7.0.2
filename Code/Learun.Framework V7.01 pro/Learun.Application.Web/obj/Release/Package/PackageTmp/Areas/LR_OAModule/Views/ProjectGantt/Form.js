/*
 * 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2017 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.06.20
 * 描 述：添加项目
 */
var keyValue = request('keyValue');

var acceptClick;
var ue;
var bootstrap = function ($, learun) {
    "use strict";

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            $('#status').lrselect();
        },
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_OAModule/ProjectGantt/GetEntity?keyValue=' + keyValue, function (data) {//
                    //$('#form').lrSetFormData(data);
                });
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var postData = $('#form').lrGetFormData(keyValue);
        postData.progressByWorklog = false;
        postData.progressByWorklog = false;
        postData.relevance = 0;
        postData.type = "";
        postData.typeId = "";
        postData.level = 0;
        postData.depends = "";
        postData.canWrite = true;
        postData.collapsed = false;
        postData.hasChild = true;
        postData.assigs = "[]";
        postData.start = new Date(postData.start).getTime();
        postData.end = new Date(postData.end).getTime();
        postData.startIsMilestone = postData.startIsMilestone == 1 ? true : false;
        postData.endIsMilestone = postData.endIsMilestone == 1 ? true : false;
        console.log(postData);
        $.lrSaveForm(top.$.rootUrl + '/LR_OAModule/ProjectGantt/SaveEntity?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}