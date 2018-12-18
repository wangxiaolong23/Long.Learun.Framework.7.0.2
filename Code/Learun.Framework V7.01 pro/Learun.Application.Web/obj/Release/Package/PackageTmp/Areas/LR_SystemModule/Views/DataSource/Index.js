/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.17
 * 描 述：数据源	
 */
var refreshGirdData; // 更新数据
var selectedRow;
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.initGrid();
            page.bind();
        },
        bind: function () {
            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                page.search({ keyword: keyword });
            });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#lr_add').on('click', function () {
                selectedRow = null;
                learun.layerForm({
                    id: 'Form',
                    title: '添加数据源',
                    url: top.$.rootUrl + '/LR_SystemModule/DataSource/Form',
                    width: 700,
                    height: 430,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                selectedRow = $('#gridtable').jfGridGet('rowdata');
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'Form',
                        title: '编辑数据源',
                        url: top.$.rootUrl + '/LR_SystemModule/DataSource/Form',
                        width: 700,
                        height: 430,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 删除
            $('#lr_delete').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_SystemModule/DataSource/DeleteForm', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
            // 查看测试
            $('#lr_eye').on('click', function () {
                var code = $('#gridtable').jfGridValue('F_Code');
                if (learun.checkrow(code)) {
                    learun.layerForm({
                        id: 'TestForm',
                        title: '数据源测试查看',
                        url: top.$.rootUrl + '/LR_SystemModule/DataSource/TestForm?code=' + code,
                        width: 1000,
                        height: 800,
                        maxmin: true,
                        btn: null
                    });
                }
            });


        },
        initGrid: function () {
            $('#gridtable').lrAuthorizeJfGrid({
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
                reloadSelected: true,
                isPage: true
            });
            page.search();
        },
        search: function (param) {
            $('#gridtable').jfGridSet('reload', param);
        }
    };

    // 保存数据后回调刷新
    refreshGirdData = function () {
        page.search();
    }

    page.init();
}


