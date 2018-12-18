/*
 * 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2017 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.03.22
 * 描 述：人员管理	
 */
var selectedRow;
var refreshGirdData;
var bootstrap = function ($, learun) {
    "use strict";
    var companyId = '';
    var departmentId = '';
    var page = {
        init: function () {
            page.inittree();
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
            // 同步
            $('#lr_synchro').on('click', function () {
                var keyValue = $('#girdtable').jfGridValue('F_UserId');
                learun.layerConfirm('是否确认同步该项！', function (res) {
                    if (res) {
                        learun.deleteForm(top.$.rootUrl + '/LR_WebChatModule/Organize/SyncMember', {}, function () {
                            refreshGirdData();
                        });
                    }
                });
            });
        },
        inittree: function () {
            $('#companyTree').lrtree({
                url: top.$.rootUrl + '/LR_WebChatModule/Organize/GetLeftTree',
                param: { parentId: '0' },
                nodeClick: page.treeNodeClick
            });
            $('#companyTree').lrtreeSet('setValue', '53298b7a-404c-4337-aa7f-80b2a4ca6681');
        },
        treeNodeClick: function (item) {
            console.log(item);
            $('#titleinfo').text(item.text);
            if (!!item.hasChildren) {//点击父级菜单
                companyId = item.id;
                departmentId = '';
                page.search();
            }
            else {//点击子菜单
                companyId = item.parentId;
                departmentId = item.id;
                page.search();
            }
        },
        initGird: function () {
            $('#girdtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_WebChatModule/Organize/GetUserPageList',
                headData: [
                        { label: '账户', name: 'F_Account', width: 80, align: 'left' },
                        { label: '姓名', name: 'F_RealName', width: 80, align: 'center' },
                        {
                            label: '性别', name: 'F_Gender', width: 45, align: 'center',
                            formatter: function (cellvalue) {
                                return cellvalue == 0 ? "女" : "男";
                            }
                        },
                        { label: '手机', name: 'F_Mobile', width: 100, align: 'center' },
                        {
                            label: "状态", name: "F_EnabledMark", index: "F_EnabledMark", width: 50, align: "center",
                            formatter: function (cellvalue) {
                                if (cellvalue == 1) {
                                    return '<span class=\"label label-success\" style=\"cursor: pointer;\">正常</span>';
                                }
                                else if (cellvalue == 0) {
                                    return '<span class=\"label label-default\" style=\"cursor: pointer;\">禁用</span>';
                                }
                            }
                        },
                        {
                            label: "同步状态", name: "F_AnswerQuestion", index: "F_AnswerQuestion", width: 80, align: "center",
                            formatter: function (cellvalue) {
                                if (cellvalue == "已同步") {
                                    return '<span class=\"label label-info\" style=\"cursor: pointer;\">已同步</span>';
                                }
                                else return '<span class=\"label label-default\" style=\"cursor: pointer;\">未同步</span>';
                            }
                        },
                        { label: "同步信息", name: "F_Description", index: "F_Description", width: 350, align: "left" },

                ],
                isPage: true,
                reloadSelected: true,
                mainId: 'F_UserId'
            });
        },
        search: function (param) {
            param = param || {};
            param.companyId = companyId;
            param.departmentId = departmentId;
            $('#girdtable').jfGridSet('reload', { companyId: companyId, departmentId: departmentId });
        }
    };
    refreshGirdData = function () {
        var keyword = $('#txt_Keyword').val();
        page.search({ keyword: keyword });
    };
    page.init();
}