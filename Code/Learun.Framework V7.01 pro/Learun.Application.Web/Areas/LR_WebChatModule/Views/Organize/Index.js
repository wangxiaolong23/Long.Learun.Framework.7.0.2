/*
 * 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2017 上海力软信息技术有限公司
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
            // 同步
            $('#lr_synchro').on('click', function () {
                learun.layerConfirm('是否确认同步数据！', function (res) {
                    if (res) {
                        learun.deleteForm(top.$.rootUrl + '/LR_WebChatModule/Organize/Sync', function (res) {
                            refreshGirdData();
                        });
                    }
                });
            });
        },
        initGrid: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_WebChatModule/Organize/GetTreeList',
                headData: [
                    { label: "名称", name: "F_FullName", width: 260, align: "left" },
                    { label: "编号", name: "F_EnCode", width: 150, align: "left" },
                    { label: "性质", name: "F_Nature", width: 80, align: "center" },
                    {
                        label: "时间", name: "F_FoundedTime", width: 80, align: "center",
                        formatter: function (value) {
                            return learun.formatDate(value, 'yyyy-MM-dd');
                        }
                    },
                    { label: "负责人", name: "F_Manager", width: 80, align: "center" },
                    {
                        label: "同步状态", name: "F_Fax", width: 100, align: "center",
                        formatter: function (cellvalue) {
                            if (cellvalue == "未同步") {
                                return '<span class=\"label label-default\" style=\"cursor: pointer;\">未同步</span>';
                            }
                            else return '<span class=\"label label-info\" style=\"cursor: pointer;\">已同步</span>';
                        }
                    },
                    { label: "同步信息", name: "F_Description", width: 200, align: "left" }
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


