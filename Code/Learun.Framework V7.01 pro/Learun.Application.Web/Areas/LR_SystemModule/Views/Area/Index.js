/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.03.22
 * 描 述：行政区域	
 */
var refreshGird; // 更新表格
var selectedRow;
var bootstrap = function ($, learun) {
    "use strict";
    var parentId = "0";
    var page = {
        init: function () {
            page.initTree();
            page.initGrid();
            page.bind();
        },
        bind: function () {
            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                page.search({keyword: keyword });
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
                    title: '添加区域',
                    url: top.$.rootUrl + '/LR_SystemModule/Area/Form?parentId=' + parentId,
                    height: 310,
                    width: 500,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGird);
                    }
                });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_AreaId');
                selectedRow = $('#gridtable').jfGridGet('rowdata');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'form',
                        title: '编辑区域',
                        url: top.$.rootUrl + '/LR_SystemModule/Area/Form?parentId=' + parentId,
                        height: 310,
                        width: 500,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGird);
                        }
                    });
                }
            });
            // 删除
            $('#lr_delete').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_AreaId');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_SystemModule/Area/DeleteForm', { keyValue: keyValue }, function () {
                                refreshGird();
                            });
                        }
                    });
                }
            });
        },
        initTree: function () {
            $('#lr_left_tree').lrtree({
                url: top.$.rootUrl + '/LR_SystemModule/Area/GetTree',
                nodeClick: function (item) {
                    parentId = item.id;
                    page.search();
                    $('#titleinfo').text(item.text);
                }
            });
        },
        initGrid: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_SystemModule/Area/GetList',
                headData: [
                    { label: "编号", name: "F_AreaCode", width: 100, align: "left" },
                    { label: "名称", name: "F_AreaName", width: 300, align: "left" },
                    { label: "简拼", name: "F_SimpleSpelling", width: 100, align: "left" },
                    { label: "级别", name: "F_Layer", width: 50, align: "center" },
                    {
                        label: "有效", name: "F_EnabledMark", width: 50, align: "center",
                        formatter: function (cellvalue, options, rowObject) {
                            return cellvalue == 1 ? "<i class=\"fa fa-toggle-on\"></i>" : "<i class=\"fa fa-toggle-off\"></i>";
                        }
                    },
                    { label: "备注", name: "F_Description", width: 200, align: "left" }
                ],
                mainId: 'F_AreaId',
                reloadSelected: true
            });

            page.search();
        },
        search: function (param) {
            param = param || {};
            param.parentId = parentId;
            $('#gridtable').jfGridSet('reload', param);
        }
    };
    // 保存数据后回调刷新
    refreshGird = function () {
        page.search();
    }

    page.init();
}