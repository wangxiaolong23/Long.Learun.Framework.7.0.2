/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.11
 * 描 述：数据源管理	
 */
var dfopid = request('dfopid');
var selectValue = request('selectValue');
var selectText = decodeURI(request('selectText'));


var acceptClick;
var bootstrap = function ($, learun) {
    "use strict";
    var postitem = { value: '', text: '' };
    var valuelist = ['', '', ''];

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                $('#gridtable').jfGridSet('reload', { keyword: keyword });
            });
            $('#gridtable').jfGrid({
                url: top.$.rootUrl + '/LR_SystemModule/DataSource/GetPageList',
                headData: [
                    { label: "编码", name: "F_Code", width: 150, align: "left" },
                    { label: "名称", name: "F_Name", width: 150, align: "left" },
                    {
                        label: "数据库", name: "F_DbId", width: 200, align: "left",
                        formatterAsync: function (callback, value, row) {
                            learun.clientdata.getAsync('db', {
                                key: value,
                                callback: function (item) {
                                    callback(item.alias + '(' + item.name + ')');
                                }
                            });
                        }
                    },
                    { label: '创建用户', name: 'F_CreateUserName', width: 100, align: 'left' },
                    {
                        label: '创建时间', name: 'F_CreateDate', width: 130, align: 'left',
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd hh:mm');
                        }
                    },
                    { label: "备注", name: "F_Description", width: 300, align: "left" },
                ],
                mainId: 'F_Id',
                isPage: true,
                onSelectRow: function (row) {
                    if (valuelist[0] != row.F_Code)
                    {
                        valuelist[0] = row.F_Code;
                        valuelist[1] = '';
                        valuelist[2] = '';
                        selectText = row.F_Name;
                        page.setValue();
                        //获取字段数据
                        learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/DataSource/GetDataColName', { code: row.F_Code }, function (data) {
                            var fieldData = [];
                            for (var i = 0, l = data.length; i < l; i++) {
                                var id = data[i];
                                var selectpoint = { value: id, text: id };
                                fieldData.push(selectpoint);
                            }
                            $('#lr_show_field').lrselectRefresh({
                                data: fieldData
                            });
                            $('#lr_save_field').lrselectRefresh({
                                data: fieldData
                            });
                        });
                    }
                }
            });
            
            $('#lr_show_field').lrselect({
                title: 'text',
                text: 'text',
                value: 'value',
                maxHeight: 300,
                allowSearch: true,
                select: function (item) {
                    valuelist[1] = item.value;
                    page.setValue();
                }
            });
            $('#lr_save_field').lrselect({
                title: 'text',
                text: 'text',
                value: 'value',
                maxHeight: 300,
                allowSearch: true,
                select: function (item) {
                    valuelist[2] = item.value;
                    page.setValue();
                }
            });
            
            
        },
        initData: function () {
            if (!!selectValue) {
                valuelist = selectValue.split(',');
                learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/DataSource/GetDataColName', { code: valuelist[0] }, function (data) {
                    var fieldData = [];
                    for (var i = 0, l = data.length; i < l; i++) {
                        var id = data[i];
                        var selectpoint = { value: id, text: id };
                        fieldData.push(selectpoint);
                    }
                    $('#lr_show_field').lrselectRefresh({
                        data: fieldData
                    });
                    $('#lr_save_field').lrselectRefresh({
                        data: fieldData
                    });
                    $('#lr_show_field').lrselectSet(valuelist[1]);
                    $('#lr_save_field').lrselectSet(valuelist[2]);
                });
                page.setValue();
            }
            $('#gridtable').jfGridSet('reload', {});
        },
        setValue: function () {
            $('#v0').text(selectText);
            $('#v1').text(valuelist[1]);
            $('#v2').text(valuelist[2]);
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (valuelist[0] != '') {
            if (!$('.lr-form-layout-top-right').lrValidform()) {
                return false;
            }
            postitem.text = selectText;
            postitem.value = String(valuelist);
        }
        else {
            postitem.text = "";
            postitem.value = "";
        }
        callBack(postitem, dfopid);
        return true;
    };
    page.init();
}