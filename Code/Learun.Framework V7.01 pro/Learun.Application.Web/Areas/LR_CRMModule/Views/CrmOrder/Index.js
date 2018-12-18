/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.17
 * 描 述：订单添加	
 */
var refreshGirdData; // 更新数据
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.initGrid();
            page.bind();
        },
        bind: function () {

            // 时间搜索框
            $('#datesearch').lrdate({
                dfdata: [
                    { name: '今天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00') }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近7天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'd', -6) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近1个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -1) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                    { name: '近3个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -3) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } }
                ],
                // 月
                mShow: false,
                premShow: false,
                // 季度
                jShow: false,
                prejShow: false,
                // 年
                ysShow: false,
                yxShow: false,
                preyShow: false,
                yShow: false,
                // 默认
                dfvalue: '1',
                selectfn: function (begin, end) {
                }
            });
            $('#multiple_condition_query').lrMultipleQuery(function (queryJson) {
                // 调用后台查询
                // queryJson 查询条件
                console.log(queryJson);
                page.search({ queryJson: JSON.stringify(queryJson)});

            },220);

            // 客户选择
            $('#customerId').lrselect({
                url: top.$.rootUrl + '/LR_CRMModule/Customer/GetList',
                text: 'F_FullName',
                value: 'F_CustomerId',
                allowSearch: true,
                maxHeight: 400
            });
            // 销售人员
            $('#sellerId').lrselect({
                url: top.$.rootUrl + '/LR_OrganizationModule/User/GetList?departmentId=2f077ff9-5a6b-46b3-ae60-c5acdc9a48f1',
                text: 'F_RealName',
                value: 'F_UserId',
                allowSearch: true,
                maxHeight: 400
            });
            // 收款方式
            $('#paymentState').lrDataItemSelect({ code: 'Client_PaymentMode' });


            


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
                learun.frameTab.open({ F_ModuleId: 'order_add', F_Icon: 'fa fa-file-text-o', F_FullName: '新增订单', F_UrlAddress: '/LR_CRMModule/CrmOrder/Form' });
            });
            // 编辑
            $('#lr_edit').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_OrderId');
                if (learun.checkrow(keyValue)) {
                    learun.frameTab.open({ F_ModuleId: 'order_add', F_Icon: 'fa fa-file-text-o', F_FullName: '新增订单', F_UrlAddress: '/LR_CRMModule/CrmOrder/Form?keyValue=' + keyValue });
                }
            });
            // 删除
            $('#lr_delete').on('click', function () {
                var keyValue = $('#gridtable').jfGridValue('F_OrderId');
                if (learun.checkrow(keyValue)) {
                    learun.layerConfirm('是否确认删除该项！', function (res) {
                        if (res) {
                            learun.deleteForm(top.$.rootUrl + '/LR_CRMModule/CrmOrder/DeleteForm', { keyValue: keyValue }, function () {
                                refreshGirdData();
                            });
                        }
                    });
                }
            });
        },
        initGrid: function () {
            $('#gridtable').lrAuthorizeJfGrid({
                url: top.$.rootUrl + '/LR_CRMModule/CrmOrder/GetPageList',
                headData: [
                    {
                        label: "单据日期", name: "F_OrderDate", width: 100, align: "left",
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd');
                        }
                    },

                    { label: "单据编号", name: "F_OrderCode", width: 130, align: "left" },
                    {
                        label: "客户名称", name: "F_CustomerId", width: 250, align: "left",
                        formatterAsync: function (callback, value, row) {
                            learun.clientdata.getAsync('custmerData', {
                                url: '/LR_CRMModule/Customer/GetList',
                                key: value,
                                keyId: 'F_CustomerId',
                                callback: function (item) {
                                    callback(item.F_FullName);
                                }
                            });
                        }
                    },
                    {
                        label: "销售人员", name: "F_SellerId", width: 80, align: "left",
                        formatterAsync: function (callback, value, row) {
                            learun.clientdata.getAsync('user', {
                                key: value,
                                callback: function (item) {
                                    callback(item.name);
                                }
                            });
                        }
                    },

                    { label: "优惠金额", name: "F_DiscountSum", width: 80, align: "left" },
                    { label: "收款金额", name: "F_Accounts", width: 80, align: "left" },
                    {
                        label: "收款方式", name: "F_PaymentMode", width: 80, align: "center",
                        formatterAsync: function (callback, value, row) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'Client_PaymentMode',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    {
                    label: "收款状态", name: "F_PaymentState", width: 80, align: "center",
                    formatter: function (cellvalue) {
                        if (cellvalue == 2) {
                                return "<span style='color:green'>部分收款</span>";
                            } else if (cellvalue == 3) {
                                return "<span style='color:blue'>全部收款</span>";
                            } else {
                                return "<span style='color:red'>未收款</span>";
                            }
                        }
                    },
                    { label: "制单人员", name: "F_CreateUserName", width: 80, align: "left" },
                    { label: "备注", name: "F_Description", width: 200, align: "left" }
                ],
                mainId: 'F_OrderId',
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


