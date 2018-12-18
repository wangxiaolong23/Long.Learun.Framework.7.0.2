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
                url: top.$.rootUrl + '/LR_SystemModule/CodeRule/GetPageList',
                headData: [
                    {
                        label: '输入框', name: 'input', width: 120, align: 'left',
                        edit: {
                            type: 'input',
                            init: function (data, $edit) {// 在点击单元格的时候触发，可以用来初始化输入控件，行数据和控件对象

                            },
                            change: function (data, num) {// 行数据和行号

                            }
                        }
                    },
                    {
                        label: '下拉框', name: 'select', width: 120, align: 'left',
                        edit: {
                            type: 'select',
                            init: function (data, $edit) {// 在点击单元格的时候触发，可以用来初始化输入控件，行数据和控件对象

                            },
                            change: function (row, num, item) {// 行数据和行号,下拉框选中数据

                            },
                            op: {// 下拉框设置参数 和 lrselect一致
                                data: [
                                    { 'id': '1', 'text': '选项一' },
                                    { 'id': '2', 'text': '选项二' },
                                    { 'id': '3', 'text': '选项三' },
                                    { 'id': '4', 'text': '选项四' }
                                ]
                            }
                        }
                    },
                    {
                        label: '单选框', name: 'radio', width: 120, align: 'left',
                        edit: {
                            type: 'radio',
                            datatype: 'dataItem',
                            code: 'DbVersion',
                            init: function (data, $edit) {// 在点击单元格的时候触发，可以用来初始化输入控件，行数据和控件对象
                            },
                            change: function (data, num) {// 行数据和行号

                            },
                            //data: [
                            //    { 'id': '1', 'text': '选项一' },
                            //    { 'id': '2', 'text': '选项二' }
                            //],
                            dfvalue:'Oracle'// 默认选中项
                        }
                    },
                    {
                        label: '多选框', name: 'checkbox', width: 260, align: 'left',
                        edit: {
                            type: 'checkbox',
                            init: function (data, $edit) {// 在点击单元格的时候触发，可以用来初始化输入控件，行数据和控件对象

                            },
                            change: function (data, num) {// 行数据和行号

                            },
                            data: [
                                { 'id': '1', 'text': '选项一' },
                                { 'id': '2', 'text': '选项二' },
                                { 'id': '3', 'text': '选项三' },
                                { 'id': '4', 'text': '选项四' }
                            ],
                            dfvalue: '1,2'// 默认选中项
                        }
                    },
                    {
                        label: '时间', name: 'date', width: 120, align: 'left',
                        edit: {
                            type: 'datatime',
                            dateformat: '0',       // 0:yyyy-MM-dd;1:yyyy-MM-dd HH:mm,格式
                            init: function (data, $edit) {// 在点击单元格的时候触发，可以用来初始化输入控件，行数据和控件对象

                            },
                            change: function (data, num) {// 行数据和行号

                            }
                        }
                    },
                    {
                        label: '弹层', name: 'layer', width: 120, align: 'left',
                        edit: {
                            type: 'layer',
                            init: function (data, $edit, rownum) {// 在点击单元格的时候触发，可以用来初始化输入控件，行数据和控件对象

                            },
                            change: function (data, rownum, selectData) {// 行数据和行号,弹层选择行的数据，如果是自定义实现弹窗方式则该方法无效
                                data.layer = selectData.F_ItemValue;
                                data.layer2 = selectData.F_ItemName;
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
                    {
                        label: '弹层2', name: 'layer2', width: 120, align: 'left'
                    }

                ],
                isEdit: true,
                isMultiselect: true,
                onAddRow: function (row, rows) {//行数据和所有行数据

                },
                onMinusRow: function (row, rows) {//行数据和所有行数据

                },
                beforeMinusRow: function (row) {// 行数据 返回false 则不许被删除
                    return true;
                }
            });
        }
    };
    page.init();
}


