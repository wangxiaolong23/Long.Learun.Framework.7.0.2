var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.initGrid();
            page.bind();
        },
        bind: function () {
            // 刷新
            $('#lr-replace').on('click', function () {
                location.reload();
            });
            //打印
            $('#lr-print').on('click', function () {
                $("#gridtable").jqprintTable({ title: '商品收发明细表' });
            });
            //导出
            $('#lr-export').on('click', function () {
                learun.download({
                    method: "POST",
                    url: '/Utility/ExportExcel',
                    param: {
                        fileName: "商品收发明细表",
                        columnJson: JSON.stringify($('#gridtable').jfGridGet('settingInfo').headData),
                        dataJson: JSON.stringify($('#gridtable').jfGridGet('settingInfo').rowdatas)
                    }
                });
            });
        },
        initGrid: function () {
            $("#gridtable").height($(window).height() - 170);
            $('#gridtable').jfGrid({
                url: top.$.rootUrl + '/LR_ReportModule/ReportTemplate/GetStockReportList',
                headData: [
                    { name: "invNo", label: "商品编号", width: 80, align: "left" },
                    { name: "invName", label: "商品名称", width: 150, align: "left" },
                    { name: "date", label: "单据日期", width: 80, align: "center" },
                    { name: "billNo", label: "单据单号", width: 100, align: "center" },
                    { name: "billType", label: "业务类别", width: 80, align: "center" },
                    { name: "buName", label: "往来单位", width: 100 },
                    { name: "location", label: "仓库", width: 80 },
                    {
                        label: "入库", name: "入库", width: 80, align: 'center',
                        children: [
                            {
                                name: "inqty", label: "入库数量", width: 80, align: "right",
                                formatter: function (cellvalue) {
                                    return learun.toDecimal(cellvalue);
                                }
                            },
                            {
                                name: "inunitCost", label: "单位成本", width: 80, align: "right",
                                formatter: function (cellvalue) {
                                    return learun.toDecimal(cellvalue);
                                }
                            },
                            {
                                name: "incost", label: "成本", width: 80, align: "right", formatter: function (cellvalue) {
                                    return learun.toDecimal(cellvalue);
                                }
                            },
                        ]
                    },
                    {
                        label: "出库", name: "出库", width: 80, align: 'center',
                        children: [
                            {
                                name: "outqty", label: "出库数量", width: 80, align: "right",
                                formatter: function (cellvalue) {
                                    return learun.toDecimal(cellvalue);
                                }
                            },
                            {
                                name: "outunitCost", label: "单位成本", width: 80, align: "right",
                                formatter: function (cellvalue) {
                                    return learun.toDecimal(cellvalue);
                                }
                            },
                            {
                                name: "outcost", label: "成本", width: 80, align: "right",
                                formatter: function (cellvalue) {
                                    return learun.toDecimal(cellvalue);
                                }
                            },
                        ]
                    },
                    {
                        label: "结存", name: "结存", width: 80, align: 'center',
                        children: [
                            {
                                name: "totalqty", label: "结存数量", width: 80, align: "right",
                                formatter: function (cellvalue) {
                                    return learun.toDecimal(cellvalue);
                                }
                            },
                            {
                                name: "totalunitCost", label: "单位成本", width: 80, align: "right",
                                formatter: function (cellvalue) {
                                    return learun.toDecimal(cellvalue);
                                }
                            },
                            {
                                name: "totalcost", label: "成本", width: 80, align: "left",
                                formatter: function (cellvalue) {
                                    return learun.toDecimal(cellvalue);
                                }
                            }
                        ]
                    }
                ],
                reloadSelected: true,
                mainId: 'billNo'
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            //var d = $('#gridtable').jfGridGet('headData');
            $('#gridtable').jfGridSet('reload', param);
        }
    };
    page.init();
}


