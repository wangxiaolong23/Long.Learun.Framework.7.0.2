/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.18
 * 描 述：客户开票信息管理	
 */

var acceptClick;
var keyValue = '';
var bootstrap = function ($, learun) {
    "use strict";
    var customerName = '';

    var selectedRow = learun.frameTab.currentIframe().selectedRow;
    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            //客户名称
            $('#F_CustomerId').lrselect({
                url: '/LR_CRMModule/Customer/GetList',
                maxHeight: 230,
                value: "F_CustomerId",
                text: "F_FullName",
                select: function (item) {
                    customerName = item.F_FullName;
                }
            });
        },
        initData: function () {
            if (!!selectedRow) {
                keyValue = selectedRow.F_InvoiceId;
                $('#form').lrSetFormData(selectedRow);
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var postData = $('#form').lrGetFormData(keyValue);
        postData["F_CustomerName"] = customerName;
        $.lrSaveForm(top.$.rootUrl + '/LR_CRMModule/Invoice/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}