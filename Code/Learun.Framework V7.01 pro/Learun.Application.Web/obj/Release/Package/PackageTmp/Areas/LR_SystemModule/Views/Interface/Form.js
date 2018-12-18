/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.11
 * 描 述：接口管理	
 */
var keyValue = request('keyValue');

var acceptClick;
var currentColRow = null;
var bootstrap = function ($, learun) {
    "use strict";

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            $('#lr_add_field').on('click', function () {
                currentColRow = null;
                learun.layerForm({
                    id: 'FieldForm',
                    title: '添加',
                    url: top.$.rootUrl + '/LR_SystemModule/Interface/FieldForm',
                    width: 450,
                    height: 310,
                    callBack: function (id) {
                        return top[id].acceptClick(function (data) {
                            $('#gridtable').jfGridSet('addRow', data);
                        });
                    }
                });
            });
            $('#lr_edit_field').on('click', function () {
                currentColRow = $('#gridtable').jfGridGet('rowdata');
                var _id = currentColRow ? currentColRow.fieldName : '';
                if (learun.checkrow(_id)) {
                    learun.layerForm({
                        id: 'FieldForm',
                        title: '修改',
                        url: top.$.rootUrl + '/LR_SystemModule/Interface/FieldForm',
                        width: 450,
                        height: 310,
                        callBack: function (id) {
                            return top[id].acceptClick(function (data) {
                                $.extend(currentColRow, data);
                                $('#gridtable').jfGridSet('updateRow', data);
                            });
                        }
                    });
                }

            });
            $('#lr_delete_field').on('click', function () {
                currentColRow = null;
                var row = $('#gridtable').jfGridGet('rowdata');
                var _id = row ? row.fieldName : '';
                if (learun.checkrow(_id)) {
                    learun.layerConfirm('是否确认删除该项！', function (res, index) {
                        if (res) {
                            $('#gridtable').jfGridSet('removeRow');
                            top.layer.close(index); //再执行关闭  
                        }
                    });
                }
            });

            $('#gridtable').jfGrid({
                headData: [
                    { label: "字段名称", name: "fieldName", width: 160, align: "left" },
                    { label: "字段注释", name: "fieldDescribe", width: 160, align: "left" },
                    {
                        label: "字段类型", name: "fieldType", width: 100, align: "left",
                        formatterAsync: function (callback, value, row, op, $cell) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'FieldType',
                                callback: function (item) {
                                    callback(item.text);
                                }
                            });
                        }
                    }
                ]
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_SystemModule/Interface/GetEntity?keyValue=' + keyValue, function (data) {
                    $('#form1').lrSetFormData(data);
                    var formatdata = JSON.parse(data.F_FieldsJson);
                    $('#gridtable').jfGridSet('refreshdata', formatdata);
                });
            }

        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form1').lrValidform()) {
            return false;
        }
        var postData = $('#form1').lrGetFormData(keyValue);
        var formatdata = $('#gridtable').jfGridGet('rowdatas');
        if (formatdata.length == 0) {
            learun.alert.error('请设置字段！');
            return false;
        }
        postData.F_FieldsJson = JSON.stringify(formatdata);
        $.lrSaveForm(top.$.rootUrl + '/LR_SystemModule/Interface/SaveForm?keyValue=' + keyValue, postData, function (res) {
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}