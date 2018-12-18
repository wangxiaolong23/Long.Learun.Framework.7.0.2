/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.03.22
 * 描 述：数据表管理	
 */
var selectedRow;
var refreshGirdData;
var bootstrap = function ($, learun) {
    "use strict";
    var databaseLinkId = '';
    var page = {
        init: function () {
            page.inittree();
            page.initGrid();
            page.bind();
        },
        bind: function () {
            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                page.search({ tableName: keyword });
            });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 新增
            $('#lr_add').on('click', function () {
                if (databaseLinkId == '') {
                    learun.alert.warning('请选择数据库!');
                } else {
                    learun.layerForm({
                        id: 'form',
                        title: '新建表',
                        url: top.$.rootUrl + '/LR_SystemModule/DatabaseTable/EditTableForm?databaseLinkId=' + databaseLinkId,
                        width: 800,
                        height: 600,
                        btn: null,
                        callBack: function (id) {
                            return top[id].acceptClick(refreshGirdData);
                        }
                    });
                }
            });

            /*打开表数据*/
            $('#lr_tabledata').on('click', function () {
                var tableName = $('#gridtable').jfGridValue('name');
                if (learun.checkrow(tableName)) {
                    learun.layerForm({
                        id: 'TableIndex',
                        title: '打开表数据【' + tableName + "】",
                        url: top.$.rootUrl + '/LR_SystemModule/DatabaseTable/TableIndex?tableName=' + tableName + '&databaseLinkId=' + databaseLinkId,
                        width: 1000,
                        height: 800,
                        maxmin: true,
                        btn: null
                    });
                }
            });
        },
        inittree: function () {
            $('#db_tree').lrtree({
                url: top.$.rootUrl + '/LR_SystemModule/DatabaseLink/GetTreeList',
                nodeClick: page.treeNodeClick
            });
        },
        treeNodeClick: function (item) {
            if (!item.hasChildren) {
                databaseLinkId = item.id;
                $('#titleinfo').html('[' + item.text + ']&nbsp;[' + item.parent.text + ']&nbsp;[' + item.value + ']');
                
                page.search();
            }
        },
        initGrid: function () {
            $('#gridtable').jfGrid({
                url: top.$.rootUrl + '/LR_SystemModule/DatabaseTable/GetList',
                headData: [
                    { label: "表名", name: "name", width: 300, align: "left"},
                    {
                        label: "记录数", name: "sumrows", width: 100, align: "center",
                        formatter: function (cellvalue) {
                            return cellvalue + "条";
                        }
                    },
                    { label: "使用大小", name: "reserved", width: 100, align: "center" },
                    { label: "索引使用大小", name: "index_size", width: 120, align: "center"},
                    { label: "说明", name: "tdescription", width: 200, align: "left" }
                ],

                reloadSelected: true,
                mainId: 'name',
                isSubGrid: true,             // 是否有子表
                subGridExpanded: function (subid, rowdata) {
                    $('#' + subid).jfGrid({
                        url: top.$.rootUrl + '/LR_SystemModule/DatabaseTable/GetFieldList',
                        headData: [
                            { label: "列名", name: "f_column", width: 300, align: "left" },
                            { label: "数据类型", name: "f_datatype",  width: 120, align: "center"},
                            { label: "长度", name: "f_length",  width: 57, align: "center" },
                            {
                                label: "允许空", name: "f_isnullable", width: 50, align: "center",
                                formatter: function (cellvalue) {
                                    return cellvalue == 1 ? "<i class=\"fa fa-toggle-on\"></i>" : "<i class=\"fa fa-toggle-off\"></i>";
                                }
                            },
                            { label: "标识", name: "f_identity", width: 58, align: "center" },
                            {
                                label: "主键", name: "f_key", width: 50, align: "center",
                                formatter: function (cellvalue) {
                                    return cellvalue == 1 ? "<i class=\"fa fa-toggle-on\"></i>" : "<i class=\"fa fa-toggle-off\"></i>";
                                }
                            },
                            { label: "默认值", name: "f_default", width: 120, align: "center"},
                            { label: "说明", name: "f_remark", width: 200, align: "left" }
                        ]
                    });
                    $('#' + subid).jfGridSet('reload', { databaseLinkId: databaseLinkId, tableName: rowdata.name });
                }// 子表展开后调用函数
            });
        },
        search: function (param) {
            param = param || {};
            param.databaseLinkId = databaseLinkId;
            $('#gridtable').jfGridSet('reload', param);
        }
    };

    refreshGirdData = function () {
        page.search();
    };

    page.init();
}


