/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.05
 * 描 述：分类管理	
 */
var parentId = request('parentId');
var selectedRow = top.layer_ClassifyIndex.selectedRow;

var keyValue = '';
var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            // 上级
            $('#F_ParentId').lrselect({
                url: top.$.rootUrl + '/LR_SystemModule/DataItem/GetClassifyTree',
                type: 'tree',
                allowSearch: true,
                maxHeight:225
            }).lrselectSet(parentId);
            /*检测重复项*/
            $('#F_ItemName').on('blur', function () {
                $.lrExistField(keyValue, 'F_ItemName', top.$.rootUrl + '/LR_SystemModule/DataItem/ExistItemName');
            });
            $('#F_ItemCode').on('blur', function () {
                $.lrExistField(keyValue, 'F_ItemCode', top.$.rootUrl + '/LR_SystemModule/DataItem/ExistItemCode');
            });
        },
        initData: function () {
            if (!!selectedRow) {
                keyValue = selectedRow.F_ItemId || '';
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
        if (postData["F_ParentId"] == '' || postData["F_ParentId"] == '&nbsp;') {
            postData["F_ParentId"] = '0';
        }
        else if (postData["F_ParentId"] == keyValue) {
            learun.alert.error('上级不能是自己本身');
            return false;
        }
        $.lrSaveForm(top.$.rootUrl + '/LR_SystemModule/DataItem/SaveClassifyForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };

    page.init();
}