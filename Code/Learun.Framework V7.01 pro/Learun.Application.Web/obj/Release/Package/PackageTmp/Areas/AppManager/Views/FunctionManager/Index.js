/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.17
 * 描 述：流程模板管理	
 */
var refreshGirdData; // 更新数据
var bootstrap = function ($, learun) {
    "use strict";
    var type = '';

    var page = {
        init: function () {
            page.initGrid();
            page.bind();
        },
        bind: function () {
            // 左侧数据加载
            $('#lr_left_tree').lrtree({
                url: top.$.rootUrl + '/LR_SystemModule/DataItem/GetDetailTree',
                param: { itemCode: 'function' },
                nodeClick: function (item) {
                    type = item.value;
                    $('#titleinfo').text(item.text);
                    page.search();
                }
            });
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
                learun.layerForm({
                    id: 'Form',
                    title: '新增移动功能',
                    url: top.$.rootUrl + '/AppManager/FunctionManager/Form?type=' + type,
                    width: 600,
                    height: 450,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');                                                 
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'Form',
                        title: '编辑移动功能',
                        url: top.$.rootUrl + '/AppManager/FunctionManager/Form?keyValue=' + keyValue,
                        width: 600,
                        height: 450,
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
                    learun.layerConfirm('是否确认删除该功能！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/AppManager/FunctionManager/DeleteForm', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });

            // 启用
            $('#lr_enable').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                var enabledMark = $('#gridtable').jfGridValue('F_EnabledMark');
                if (learun.checkrow(keyValue)) {
                    if (enabledMark != 1) {
                        learun.layerConfirm('是否启用该功能！', function (res) {
                            if (res) {
                                learun.postForm(top.$.rootUrl + '/AppManager/FunctionManager/UpDateSate', { keyValue: keyValue, state: 1 }, function () {
                                    refreshGirdData();
                                });
                            }
                        });
                    }
                    else {
                        learun.alert.warning('该功能已启用!');
                    }
                }
            });
            // 禁用
            $('#lr_disabled').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                var enabledMark = $('#gridtable').jfGridValue('F_EnabledMark');

                if (learun.checkrow(keyValue)) {
                    if (enabledMark == 1) {
                        learun.layerConfirm('是否禁用该功能！', function (res) {
                            if (res) {
                                learun.postForm(top.$.rootUrl + '/AppManager/FunctionManager/UpDateSate', { keyValue: keyValue, state: 0 }, function () {
                                    refreshGirdData();
                                });
                            }
                        });
                    }
                    else {
                        learun.alert.warning('该功能已禁用!');
                    }
                }
            });
           
            /*分类管理*/
            $('#lr_category').on('click', function () {
                learun.layerForm({
                    id: 'ClassifyIndex',
                    title: '分类管理',
                    url: top.$.rootUrl + '/LR_SystemModule/DataItem/DetailIndex?itemCode=function',
                    width: 800,
                    height: 500,
                    maxmin: true,
                    btn: null,
                    end: function () {
                        learun.clientdata.update('dataItem');
                        location.reload();
                    }
                });
            });
        },
        initGrid: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/AppManager/FunctionManager/GetPageList',
                headData: [
                    { label: "功能名称", name: "F_Name", width: 150, align: "left" },
                    {
                        label: "分类", name: "F_Type", width: 120, align: "left",
                        formatterAsync: function (callback, value, row) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'function',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    {
                        label: "类型", name: "F_IsSystem", width: 100, align: "center",
                        formatter: function (cellvalue, row) {
                            if (cellvalue == 1) {
                                return '<span class=\"label label-info\" style=\"cursor: pointer;\">代码开发</span>';
                            } else {
                                return '<span class=\"label label-warning\" style=\"cursor: pointer;\">自定义表单</span>';
                            }
                        }
                    },
                    {
                        label: "状态", name: "F_EnabledMark", width: 60, align: "center",
                        formatter: function (cellvalue, row) {
                            if (cellvalue == 1) {
                                return '<span class=\"label label-success\" style=\"cursor: pointer;\">正常</span>';
                            } else if (cellvalue == 0) {
                                return '<span class=\"label label-default\" style=\"cursor: pointer;\">禁用</span>';
                            }
                        }
                    },
                    { label: "编辑人", name: "F_CreateUserName", width: 120, align: "left" },
                    {
                        label: "编辑时间", name: "F_CreateDate", width: 150, align: "left",
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd');
                        },
                        sort: true
                    }
                ],
                mainId: 'F_Id',
                sidx:'F_SortCode',
                reloadSelected: true,
                isPage: true
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.type = type;
            $('#gridtable').jfGridSet('reload', param);
        }
    };

    // 保存数据后回调刷新
    refreshGirdData = function () {
        page.search();
    }

    page.init();
}


