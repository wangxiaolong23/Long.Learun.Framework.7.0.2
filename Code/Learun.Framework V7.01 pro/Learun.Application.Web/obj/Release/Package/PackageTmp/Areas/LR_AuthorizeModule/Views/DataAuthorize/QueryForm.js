/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.17
 * 描 述：数据权限
 */
var interfaceId = request('interfaceId');
var rowid = request('rowid');
var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";

    var fieldName = "";
    var fieldType = "";

    var symbolName = "";

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            // 选择字段
            learun.httpAsyncGet(top.$.rootUrl + '/LR_SystemModule/Interface/GetEntity?keyValue=' + interfaceId, function (res) {
                if (res.code == 200) {
                    var fieldsData = JSON.parse(res.data.F_FieldsJson);
                    $('#F_FieldId').lrselectRefresh({
                        data: fieldsData
                    });
                }
            });
            $('#F_FieldId').lrselect({
                value: 'fieldName',
                text: 'fieldDescribe',
                title: 'fieldDescribe',
                maxHeight: 140,
                allowSearch: true,
                select: function (item) {
                    fieldName = item.fieldDescribe;
                    fieldType = item.fieldType;
                }
            });

            // 字段值类型
            $('#F_FiledValueType').lrselect({
                data: [{ value: 1, text: '文本' }, { value: 2, text: '登录者ID' }, { value: 3, text: '登录者账号' }, { value: 4, text: '登录者公司' }, { value: 41, text: '登录者公司及下属公司' }, { value: 5, text: '登录者部门' }, { value: 51, text: '登录者部门及下属部门' }, { value: 6, text: '登录者岗位' }, { value: 7, text: '登录者角色' }],
                value: 'value',
                text: 'text',
                title: 'text',
                maxHeight: 99,
                placeholder: false,
                select: function (item) {
                    if (item.value == 1) {
                        $('#F_FiledValue').removeAttr('disabled');
                        $('#F_FiledValue').val('');
                    }
                    else {
                        $('#F_FiledValue').attr('disabled', 'disabled');
                        $('#F_FiledValue').val(item.text);
                    }
                }
            }).lrselectSet(1);
            // 类型
            $('#F_Symbol').lrselect({
                data: [{ value: 1, text: '等于' }, { value: 2, text: '大于' }, { value: 3, text: '大于等于' }, { value: 4, text: '小于' }, { value: 5, text: '小于等于' }, { value: 6, text: '包含' }, { value: 7, text: '包含于' }, { value: 8, text: '不等于' }, { value: 9, text: '不包含' }, { value: 10, text: '不包含于' }],
                value: 'value',
                text: 'text',
                title: 'text',
                placeholder:false,
                maxHeight: 130,
                select: function (item) {
                    symbolName = item.text;
                }
            }).lrselectSet(1);
            
        },
        initData: function () {
            if (rowid != "") {
                var _data = top.layer_form.queryDataList[rowid];
                $('#form').lrSetFormData(_data);
            }
        }
    };

    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var data = $('#form').lrGetFormData();
        data.F_FieldName = fieldName;
        data.F_FieldType = fieldType;
        data.F_SymbolName = symbolName;
        if (!!callBack) {
            callBack("【" + fieldName + "】 " + symbolName + " " + data.F_FiledValue, data, rowid);
        }

        return true;
    }

    page.init();



}


