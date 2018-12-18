/* * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：超级管理员
 * 日  期：2017-12-12 11:52
 * 描  述：消息策略
 */

var refreshGirdData;
var acceptClick;

var bootstrap = function ($, learun) {
    "use strict";
    var selectedRow;

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
        },
        initGrid: function () {
            $('#gridtable').jfGrid({
                url: top.$.rootUrl + '/LR_SystemModule/DbField/GetList',
                headData: [
                    { label: '列名', name: 'F_Name', width: 160, align: "left" },
                    { label: '描述', name: 'F_Remark', width: 160, align: "left" },
                    {
                        label: '数据类型', name: 'F_DataType', width: 80, align: "center",
                        formatterAsync: function (callback, value, row) {
                            learun.clientdata.getAsync('dataItem', {
                                key: value,
                                code: 'DbFieldType',
                                callback: function (_data) {
                                    callback(_data.text);
                                }
                            });
                        }
                    },
                    { label: '字段长度', name: 'F_Length', width: 80, align: "center" },
                ],
                mainId: 'F_Id',
                reloadSelected: true,
                isMultiselect: true
            });
            page.search();
        },
        search: function (param) {
            param = param || {};
            $('#gridtable').jfGridSet('reload', { queryJson: JSON.stringify(param) });
        }
    };
    refreshGirdData = function () {
        page.search();
    };
    page.init();


    acceptClick = function (callBack) {
        var selectedRow = $('#gridtable').jfGridGet('rowdata');
        callBack(selectedRow);
        return true;
    };
}
