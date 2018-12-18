/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.11.12
 * 描 述：商机管理	
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
                    id: 'form',
                    title: '新增商机',
                    url: top.$.rootUrl + '/LR_CRMModule/Chance/Form',
                    width: 1000,
                    height: 620,
                    maxmin: true,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                selectedRow = $('#gridtable').jfGridGet('rowdata');
                var keyValue = $('#gridtable').jfGridValue('F_ChanceId');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '编辑商机',
                        url: top.$.rootUrl + '/LR_CRMModule/Chance/Form',
                        width: 1000,
                        height: 620,
                        maxmin: true,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 删除
            $('#lr_delete').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_ChanceId');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_CRMModule/Chance/DeleteForm', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
        },
        initGrid: function () {
            $('#gridtable').jfGrid({
                url: top.$.rootUrl + '/LR_CRMModule/Chance/GetPageList',
                headData: [
                    { label: '商机编号', name: 'F_EnCode', width: 100, align: 'left' },
                    { label: '商机名称', name: 'F_FullName', width: 200, align: 'left' },
                    { label: '商机来源', name: 'F_SourceId', width: 100, align: 'left' },
                    { label: '商机阶段', name: 'F_StageId', width: 100, align: 'left' },
                    { label: '公司名称', name: 'F_CompanyName', width: 100, align: 'left', sort: true },
                    { label: '公司性质', name: 'F_CompanyNatureId', width: 100, align: 'left' },
                    {
                        label: "预计成交时间", name: "F_DealDate", width: 140, align: "left",
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd hh:mm');
                        }
                    },
                    {
                        label: "成功率", name: "F_SuccessRate", width: 80, align: "left",
                        formatter: function (cellvalue) {
                            return cellvalue == null ? '0' : (cellvalue+'%');
                        }
                    }
                ],
                mainId: 'F_ChanceId',
                reloadSelected: true,
                isPage: true,
                sidx: 'F_CreateDate'
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


