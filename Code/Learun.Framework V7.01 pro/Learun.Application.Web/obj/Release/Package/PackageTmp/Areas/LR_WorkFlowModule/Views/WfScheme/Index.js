/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.17
 * 描 述：流程模板管理	
 */
var refreshGirdData; // 更新数据
var nowschemeId;
var bootstrap = function ($, learun) {
    "use strict";

    var categoryId = '';

    var page = {
        init: function () {
            page.initGrid();
            page.bind();
        },
        bind: function () {
            // 左侧数据加载
            $('#lr_left_tree').lrtree({
                url: top.$.rootUrl + '/LR_SystemModule/DataItem/GetDetailTree',
                param: { itemCode: 'FlowSort' },
                nodeClick: function (item) {
                    categoryId = item.value;
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
                    title: '新增流程模板',
                    url: top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/Form?categoryId=' + categoryId,
                    width: 1200,
                    height: 900,
                    maxmin: true,
                    btn: null
                });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                var name = $('#gridtable').jfGridValue('F_Name');
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                var code = $('#gridtable').jfGridValue('F_Code');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'Form',
                        title: '编辑流程模板-' + name,
                        url: top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/Form?keyValue=' + keyValue + '&categoryId=' + categoryId + '&shcemeCode=' + code,
                        width: 1200,
                        height: 900,
                        maxmin: true,
                        btn: null
                    });
                }
            });
            // 删除
            $('#lr_delete').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/DeleteForm', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });

            // 预览模板
            $('#lr_preview').on('click', function () {
                var schemeId = $('#gridtable').jfGridValue('F_SchemeId');
                if (learun.checkrow(schemeId)) {
                    learun.layerForm({
                        id: 'PreviewForm',
                        title: '预览当前模板',
                        url: top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/PreviewForm?schemeId=' + schemeId,
                        width: 1200,
                        height: 900,
                        maxmin: true,
                        btn: null
                    });
                }
            });

            // 启用
            $('#lr_enable').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                var enabledMark = $('#gridtable').jfGridValue('F_EnabledMark');

                var type = $('#gridtable').jfGridValue('F_Type');
                if (type == 2) {
                    learun.alert.warning('草稿模板不能启用!');
                    return false;
                }

                if (learun.checkrow(keyValue)) {
                    if (enabledMark != 1) {
                        learun.layerConfirm('是否确认启用该表单模板！', function (res) {
                            if (res) {
                                learun.postForm(top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/UpDateSate', { keyValue: keyValue, state: 1 }, function () {
                                    refreshGirdData();
                                });
                            }
                        });
                    }
                    else {
                        learun.alert.warning('该模板已启用!');
                    }
                }
            });
            // 禁用
            $('#lr_disabled').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                var enabledMark = $('#gridtable').jfGridValue('F_EnabledMark');

                var type = $('#gridtable').jfGridValue('F_Type');
                if (type == 2) {
                    learun.alert.warning('草稿模板不能禁用!');
                    return false;
                }

                if (learun.checkrow(keyValue)) {
                    if (enabledMark == 1) {
                        learun.layerConfirm('是否确认禁用该表单模板！', function (res) {
                            if (res) {
                                learun.postForm(top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/UpDateSate', { keyValue: keyValue, state: 0 }, function () {
                                    refreshGirdData();
                                });
                            }
                        });
                    }
                    else {
                        learun.alert.warning('该表单已禁用!');
                    }
                }
            });
            // 查看历史记录
            $('#lr_history').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                var Name = $('#gridtable').jfGridValue('F_Name');
                nowschemeId = $('#gridtable').jfGridValue('F_SchemeId');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'HistoryForm',
                        title: '流程模板历史记录-' + Name,
                        url: top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/HistoryForm?&keyValue=' + keyValue,
                        width: 600,
                        height: 400,
                        maxmin: true,
                        btn: null
                    });
                }
            });

            /*分类管理*/
            $('#lr_category').on('click', function () {
                learun.layerForm({
                    id: 'ClassifyIndex',
                    title: '分类管理',
                    url: top.$.rootUrl + '/LR_SystemModule/DataItem/DetailIndex?itemCode=FlowSort',
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
                url: top.$.rootUrl + '/LR_WorkFlowModule/WfScheme/GetSchemeInfoPageList',
                headData: [
                    { label: "编号", name: "F_Code", width: 150, align: "left" },
                    { label: "名称", name: "F_Name", width: 160, align: "left" },
                    {
                        label: "分类", name: "F_Category", width: 120, align: "center",
                        formatterAsync: function (callback, value, row) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'FlowSort',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    {
                        label: "类型", name: "F_Kind", width: 100, align: "center",
                        formatter: function (cellvalue, row) {
                            if (cellvalue == 0) {
                                return '<span class=\"label label-warning\" style=\"cursor: pointer;\">系统流程</span>';
                            } else {
                                return '<span class=\"label label-primary\" style=\"cursor: pointer;\">自定义流程</span>';
                            }
                        }
                    },
                   
                    {
                        label: "状态", name: "F_EnabledMark", width: 50, align: "center",
                        formatter: function (cellvalue, row) {
                            if (row.F_Type == 1) {
                                if (cellvalue == 1) {
                                    return '<span class=\"label label-success\" style=\"cursor: pointer;\">正常</span>';
                                } else if (cellvalue == 0) {
                                    return '<span class=\"label label-default\" style=\"cursor: pointer;\">禁用</span>';
                                }
                            }
                            else {
                                return '<span class=\"label label-info\" style=\"cursor: pointer;\">草稿</span>';
                            }
                        }
                    },
                    { label: "编辑人", name: "F_CreateUserName", width: 80, align: "center" },
                    {
                        label: "编辑时间", name: "F_CreateDate", width: 80, align: "center",
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd');
                        },
                        sort: true
                    },
                    { label: "备注", name: "F_Description", width: 200, align: "left" }
                ],
                mainId: 'F_Id',
                reloadSelected: true,
                isPage: true
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            param.category = categoryId;
            $('#gridtable').jfGridSet('reload', param);
        }
    };

    // 保存数据后回调刷新
    refreshGirdData = function () {
        page.search();
    }

    page.init();
}


