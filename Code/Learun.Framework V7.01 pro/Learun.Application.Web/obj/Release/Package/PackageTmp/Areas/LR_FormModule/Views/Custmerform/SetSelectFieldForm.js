/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.11
 * 描 述：表格选择项字段选择	
 */
var dbId = request('dbId');
var tableName = request('tableName');

var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";
    var selectFieldData = top.layer_SetFieldForm.selectFieldData;
    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            // 绑定字段
            $('#value').lrselect({
                value: 'f_column',
                text: 'f_column',
                title: 'f_remark',
                allowSearch: true,
                maxHeight:160
            });
            learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/DatabaseTable/GetFieldList', { databaseLinkId: dbId, tableName: tableName }, function (data) {
                $('#value').lrselectRefresh({
                    data: data
                });
            });
            // 对齐方式
            $('#align').lrselect({ placeholder: false }).lrselectSet('left');
            // 是否隐藏
            $('#hide').lrselect({ placeholder: false }).lrselectSet('0');
        },
        initData: function () {
            if (!!selectFieldData)
            {
                $('#form').lrSetFormData(selectFieldData);
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var postData = $('#form').lrGetFormData();
        callBack(postData);

        return true;
    };
    page.init();
}