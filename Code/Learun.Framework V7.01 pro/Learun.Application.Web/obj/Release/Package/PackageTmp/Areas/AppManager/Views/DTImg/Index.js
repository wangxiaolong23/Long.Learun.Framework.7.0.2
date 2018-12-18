/* * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn) 
 * Copyright (c) 2013-2018 上海力软信息技术有限公司 
 * 创建人：超级管理员 
 * 日  期：2018-07-02 17:20 
 * 描  述：App首页图片管理
 */
var selectedRow;
var refreshGirdData;
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.initGird();
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
                    id: 'form',
                    title: '新增',
                    url: top.$.rootUrl + '/AppManager/DTImg/Form',
                    width: 505,
                    height: 340,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑 
            $('#lr_edit').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                selectedRow = $('#gridtable').jfGridGet('rowdata');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '编辑',
                        url: top.$.rootUrl + '/AppManager/DTImg/Form?keyValue=' + keyValue,
                        width: 505,
                        height: 340,
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
                            learun.deleteForm(top.$.rootUrl + '/AppManager/DTImg/DeleteForm', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });

            // 启用
            $('#lr_enabled').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                var enabledMark = $('#gridtable').jfGridValue('F_EnabledMark');
                if (learun.checkrow(keyValue)) {
                    if (enabledMark != 1) {
                        learun.layerConfirm('是否确认启用该项！', function (res) {
                            if (res) {
                                learun.postForm(top.$.rootUrl + '/AppManager/DTImg/UpDateSate', { keyValue: keyValue, state: 1 }, function () {
                                    refreshGirdData();
                                });
                            }
                        });
                    }
                    else {
                        learun.alert.warning('该项已启用!');
                    }
                }
            });
            // 禁用
            $('#lr_disabled').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_Id');
                var enabledMark = $('#gridtable').jfGridValue('F_EnabledMark');

                if (learun.checkrow(keyValue)) {
                    if (enabledMark == 1) {
                        learun.layerConfirm('是否确认禁用该项！', function (res) {
                            if (res) {
                                learun.postForm(top.$.rootUrl + '/AppManager/DTImg/UpDateSate', { keyValue: keyValue, state: 0 }, function () {
                                    refreshGirdData();
                                });
                            }
                        });
                    }
                    else {
                        learun.alert.warning('该项已禁用!');
                    }
                }
            });
        },
        initGird: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/AppManager/DTImg/GetPageList',
                headData: [
                    { label: '说明', name: 'F_Des', width: 200, align: "left" },
                    {
                        label: '图片', name: 'F_FileName', width: 197.5, align: "left",
                        formatter(value, row, op, $cell) {
                            return '<img src="' + top.$.rootUrl + '/AppManager/DTImg/GetImg?keyValue=' + row.F_Id + '"  style="position: absolute;height:60px;width:187.5px;top:5px;left:5px;" >';
                        }
                    },
                  
                    {
                        label: '状态', name: 'F_EnabledMark', width: 80, align: "center",
                        formatter: function (cellvalue, row) {
                            if (cellvalue == 1) {
                                return '<span class=\"label label-success\" style=\"cursor: pointer;\">启用</span>';
                            } else if (cellvalue == 0) {
                                return '<span class=\"label label-default\" style=\"cursor: pointer;\">禁用</span>';
                            }
                        }
                    },
                    { label: '排序码', name: 'F_SortCode', width: 100, align: "left" }
                ],
                mainId: 'F_Id',
                isPage: true,
                rowHeight: 70,
                sidx:'F_EnabledMark Desc,F_SortCode ASC'
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            $('#gridtable').jfGridSet('reload', { queryJson: JSON.stringify(param) });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();
} 