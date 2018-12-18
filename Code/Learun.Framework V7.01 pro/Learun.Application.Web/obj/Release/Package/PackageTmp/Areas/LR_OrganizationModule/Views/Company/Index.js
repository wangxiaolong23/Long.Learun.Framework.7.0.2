/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.17
 * 描 述：公司管理	
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
                    title: '添加公司',
                    url: top.$.rootUrl + '/LR_OrganizationModule/Company/Form',
                    width: 750,
                    height: 500,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                selectedRow = $('#gridtable').jfGridGet('rowdata');
                var keyValue = $('#gridtable').jfGridValue('F_CompanyId');
                if (learun.checkrow(keyValue)) {
                    learun.layerForm({
                        id: 'Form',
                        title: '编辑公司',
                        url: top.$.rootUrl + '/LR_OrganizationModule/Company/Form',
                        width: 750,
                        height: 500,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });
            // 删除
            $('#lr_delete').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_CompanyId');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_OrganizationModule/Company/DeleteForm', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
        },
        initGrid: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_OrganizationModule/Company/GetList',
                headData: [
                    { label: "公司名称", name: "F_FullName", width: 260, align: "left" },
                    { label: "公司编码", name: "F_EnCode", width: 150, align: "left" },
                    { label: "公司简称", name: "F_ShortName", width: 150, align: "left" },
                    { label: "公司性质", name: "F_Nature", width: 80, align: "center" },
                    {
                        label: "成立时间", name: "F_FoundedTime", width: 80, align: "center",
                        formatter: function (value) {
                            return learun.formatDate(value, 'yyyy-MM-dd');
                        }
                    },
                    { label: "负责人", name: "F_Manager", width: 80, align: "center"},
                    { label: "经营范围", name: "F_Fax", width: 200, align: "left" },
                    { label: "备注", name: "F_Description", width: 200, align: "left" }
                ],
                isTree: true,
                mainId: 'F_CompanyId',
                parentId: 'F_ParentId'
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


