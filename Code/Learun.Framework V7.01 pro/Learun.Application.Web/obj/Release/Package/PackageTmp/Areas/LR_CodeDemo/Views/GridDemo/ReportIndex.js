/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.17
 * 描 述：单据编码	
 */
var refreshGirdData; // 更新数据
var selectedRow;
var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.initGrid();
        },
        initGrid: function () {
            $('#gridtable').jfGrid({
                headData: [
                    {
                        label: "商品信息", name: "a1", width: 80, align: "center", frozen: true,
                        children: [
                            {
                                label: '商品名称', name: 'F_ProductName', width: 260, align: 'left',
                                edit: {
                                    type: 'layer',
                                    init: function (data, $edit, rownum) {// 在点击单元格的时候触发，可以用来初始化输入控件，行数据和控件对象

                                    },
                                    change: function (row, rownum, selectdata) {// 行数据和行号,弹层选择行的数据，如果是自定义实现弹窗方式则该方法无效

                                        row.F_ProductName = selectdata.F_ItemName;
                                        row.F_ProductCode = selectdata.F_ItemValue;

                                        row.F_Qty = '1';
                                        row.F_Price = '0';
                                        row.F_Amount = '0';
                                        row.F_TaxRate = '0';
                                        row.F_Taxprice = '0';
                                        row.F_Tax = '0';
                                        row.F_TaxAmount = '0';



                                        $('#gridtable').jfGridSet('updateRow', rownum);
                                    },
                                    op: { // 如果未设置op属性可以在init中自定义实现弹窗方式
                                        width: 600,
                                        height: 400,
                                        colData: [
                                            { label: "商品编号", name: "F_ItemValue", width: 100, align: "left" },
                                            { label: "商品名称", name: "F_ItemName", width: 450, align: "left" }
                                        ],
                                        url: top.$.rootUrl + '/LR_SystemModule/DataItem/GetDetailList',
                                        param: { itemCode: 'Client_ProductInfo' }
                                    }
                                }
                            },
                            { label: '商品编号', name: 'F_ProductCode', width: 100, align: 'center', editType: 'label' },
                            {
                                label: '单位', name: 'F_UnitId', width: 100, align: 'center',
                                edit: {
                                    type: 'input'
                                }
                            }
                        ]
                    },
                    {
                        label: "价格信息", name: "a2", width: 80, align: "center",
                        children: [
                            {
                                label: '数量', name: 'F_Qty', width: 80, align: 'center', statistics: true,
                                edit: {
                                    type: 'input',
                                    change: function (row, rownum) {// 行数据和行号
                                        row.F_Amount = parseInt(parseFloat(row.F_Price || '0') * parseFloat(row.F_Qty || '0'));
                                        row.F_TaxAmount = parseInt((parseFloat(row.F_Price || '0') * (1 + parseFloat(row.F_TaxRate || '0') / 100)) * parseFloat(row.F_Qty || '0'));
                                        row.F_Tax = row.F_TaxAmount - row.F_Amount;
                                        $('#gridtable').jfGridSet('updateRow', rownum);
                                    },
                                }
                            },
                            {
                                label: '单价', name: 'F_Price', width: 80, align: 'center',
                                edit: {
                                    type: 'input',
                                    change: function (row, rownum) {// 行数据和行号
                                        row.F_Amount = parseInt(parseFloat(row.F_Price || '0') * parseFloat(row.F_Qty || '0'));
                                        row.F_TaxAmount = parseInt(parseFloat(row.F_Price || '0') * (1 + parseFloat(row.F_TaxRate || '0') / 100)) * parseFloat(row.F_Qty || '0');
                                        row.F_Tax = row.F_TaxAmount - row.F_Amount;

                                        row.F_Taxprice = parseInt(parseFloat(row.F_Price || '0') * (1 + parseFloat(row.F_TaxRate || '0') / 100));
                                        $('#gridtable').jfGridSet('updateRow', rownum);
                                    },
                                }
                            },
                            { label: '金额', name: 'F_Amount', width: 80, align: 'center', statistics: true },
                            {
                                label: '税率(%)', name: 'F_TaxRate', width: 80, align: 'center', editType: 'input',
                                edit: {
                                    type: 'input',
                                    change: function (row, rownum) {// 行数据和行号
                                        row.F_Amount = parseInt(parseFloat(row.F_Price || '0') * parseFloat(row.F_Qty || '0'));
                                        row.F_TaxAmount = parseInt((parseFloat(row.F_Price || '0') * (1 + parseFloat(row.F_TaxRate || '0') / 100)) * parseFloat(row.F_Qty || '0'));
                                        row.F_Tax = row.F_TaxAmount - row.F_Amount;

                                        row.F_Taxprice = parseInt(parseFloat(row.F_Price || '0') * (1 + parseFloat(row.F_TaxRate || '0') / 100));
                                        $('#gridtable').jfGridSet('updateRow', rownum);
                                    },
                                }
                            },
                            { label: '含税单价', name: 'F_Taxprice', width: 80, align: 'center', },
                            { label: '税额', name: 'F_Tax', width: 80, align: 'center', statistics: true },
                            { label: '含税金额', name: 'F_TaxAmount', width: 80, align: 'center', statistics: true },
                        ]
                    },
                    {
                        label: "说明信息", name: "F_Description", width: 200, align: "left",
                        edit: {
                            type: 'input'
                        }
                    }
                ],
                isEdit: true
            });
        }
    };
    page.init();
}


