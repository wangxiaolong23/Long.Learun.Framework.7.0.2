/* * 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2017 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2017-09-04 16:04
 * 描  述：会员信息
 */
var acceptClick;
var keyValue = request('keyValue');
var bootstrap = function ($, learun) {
    "use strict";
    var selectedRow = learun.frameTab.currentIframe().selectedRow;
    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            $('#Sex').lrDataItemSelect({ code: 'usersex' });
            $('#LR_Demo_OrderList').jfGrid({
                headData: [
                    {
                        label: '料号', name: 'MeterialId', width: 100, align: 'left',editType: 'select',
                        editOp: {
                            width: 600,
                            height: 400,
                            colData: [
                               { label: '品名', name: 'F_ItemName', width: 100, align: 'left' },
                               { label: '料号', name: 'F_ItemValue', width: 100, align: 'left' },
                            ],
                            url: top.$.rootUrl + '/LR_SystemModule/DataItem/GetDetailList',
                            param: { itemCode: 'Client_ProductInfo' },
                            callback: function (selectdata, rownum, row) {
                                row.MeterialName = selectdata.F_ItemName;
                                row.MeterialId = selectdata.F_ItemValue;
                            }
                        }
                    },
                    {
                        label: '品名', name: 'MeterialName', width: 150, align: 'left',editType: 'input'
                    },
                    {
                        label: '数量', name: 'Qty', width: 100, align: 'left',editType: 'input'
                    },
                    {
                        label: '单价', name: 'Price', width: 100, align: 'left',editType: 'input'
                    },
                    {
                        label: '合计', name: 'Amount', width: 160, align: 'left',editType: 'input'
                    },
                ],
                isAutoHeight: true,
                isEidt: true,
                footerrow: true,
                minheight: 400
            });
        },
        initData: function () {
            if (!!keyValue) {
                $.lrSetForm(top.$.rootUrl + '/LR_CodeDemo/CustomerInfo/GetFormData?keyValue=' + keyValue, function (data) {
                    for (var id in data) {
                        if (!!data[id].length && data[id].length > 0) {
                            $('#LR_Demo_OrderList').jfGridSet('refreshdata', { rowdatas: data[id] });
                        }
                        else {
                            $('[data-table="' + id + '"]').lrSetFormData(data[id]);
                        }
                    }
                });
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('body').lrValidform()) {
            return false;
        }
        var postData = {};
        postData.strEntity = JSON.stringify($('[data-table="LR_Demo_User"]').lrGetFormData());
        postData.strlR_Demo_OrderListList = JSON.stringify($('#LR_Demo_OrderList').jfGridGet('rowdatas'));
        $.lrSaveForm(top.$.rootUrl + '/LR_CodeDemo/CustomerInfo/SaveForm?keyValue=' + keyValue, postData, function (res) {
            // 保存成功后才回调
            if (!!callBack) {
                callBack();
            }
        });
    };
    page.init();
}
