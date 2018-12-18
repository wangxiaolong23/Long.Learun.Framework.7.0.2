/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.07.11
 * 描 述：公告通知	
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
                learun.layerForm({
                    id: 'form',
                    title: '添加公告',
                    url: top.$.rootUrl + '/LR_OAModule/Notice/Form',
                    width: 1000,
                    height: 650,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                selectedRow = $('#gridtable').jfGridGet('rowdata');
                var keyValue = $('#gridtable').jfGridValue('F_NewsId');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '编辑公告',
                        url: top.$.rootUrl + '/LR_OAModule/Notice/Form',
                        width: 1000,
                        height: 650,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 删除
            $('#lr_delete').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_NewsId');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_OAModule/Notice/DeleteForm', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
        },
        initGrid: function () {
            $('#gridtable').jfGrid({
                url: top.$.rootUrl + '/LR_OAModule/Notice/GetPageList',
                headData: [
                    { label: '公告标题', name: 'F_FullHead', index: 'F_FullHead', width: 600, align: 'left' },
                    { label: '公告类别', name: 'F_Category', index: 'F_Category', width: 100, align: 'center' },
                    {
                        label: "发布时间", name: "F_ReleaseTime", index: "F_ReleaseTime", width: 140, align: "center",
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd hh:mm');
                        }
                    },
                    { label: '信息来源', name: 'F_SourceName', index: 'F_SourceName', width: 100, align: 'center' },
                    { label: '阅读次数', name: 'PV', index: 'PV', width: 80, align: 'center' },
                    {
                        label: "发布状态", name: "F_EnabledMark", index: "F_EnabledMark", width: 80, align: "center", autowidth: false,
                        formatter: function (cellvalue) {
                            if (cellvalue == 1) {
                                return "<span class=\"label label-success\">已发布</span>";
                            } else {
                                return "<span class=\"label label-danger\">未发布</span>";
                            }
                        }
                    }
                ],
                mainId: 'F_NewsId',
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


