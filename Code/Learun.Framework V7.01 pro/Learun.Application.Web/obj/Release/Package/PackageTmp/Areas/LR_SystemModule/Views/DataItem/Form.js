/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.05
 * 描 述：添加明细
 */
var parentId = request('parentId');
var itemCode = request('itemCode');

var keyValue = '';
var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";
    var selectedRow = top.selectedDataItemRow;

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            /*检测重复项*/
            $('#F_ItemName').on('blur', function () {
                $.lrExistField(keyValue, 'F_ItemName', top.$.rootUrl + '/LR_SystemModule/DataItem/ExistDetailItemName', { itemCode: itemCode });
            });
            $('#F_ItemValue').on('blur', function () {
                $.lrExistField(keyValue, 'F_ItemValue', top.$.rootUrl + '/LR_SystemModule/DataItem/ExistDetailItemValue', { itemCode: itemCode });
            });
        },
        initData: function () {
            if (!!selectedRow) {
                keyValue = selectedRow.F_ItemDetailId || '';
                $('#form').lrSetFormData(selectedRow);
            }
            else {
                $('#F_ParentId').val(parentId);
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var postData = $('#form').lrGetFormData(keyValue);
        $.lrSaveForm(top.$.rootUrl + '/LR_SystemModule/DataItem/SaveDetailForm?keyValue=' + keyValue + "&itemCode="+itemCode, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };

    page.init();
}