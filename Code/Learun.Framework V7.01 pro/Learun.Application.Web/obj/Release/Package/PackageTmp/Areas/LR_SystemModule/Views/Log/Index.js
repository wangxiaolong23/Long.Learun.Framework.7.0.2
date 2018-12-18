/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.03.22
 * 描 述：日志管理	
 */
var bootstrap = function ($, learun) {
    "use strict";
    var categoryId = '1';
    var logbegin = '';
    var logend = '';

    var refreshGirdData = function () {
        page.search();
    }

    var page = {
        init: function () {
            page.initleft();
            page.initGrid();
            page.bind();
        },
        bind: function () {
            $('.datetime').each(function () {
                $(this).lrdate({
                    dfdata: [
                        { name: '今天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00') }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                        { name: '近7天', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'd', -6) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                        { name: '近1个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -1) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
                        { name: '近3个月', begin: function () { return learun.getDate('yyyy-MM-dd 00:00:00', 'm', -3) }, end: function () { return learun.getDate('yyyy-MM-dd 23:59:59') } },
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
                        logbegin = begin;
                        logend = end;
                        page.search();
                    }
                });
            });
            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                page.search({ keyword: keyword });
            });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 清空
            $('#lr_removelog').on('click', function () {
                learun.layerForm({
                    id: 'form',
                    title: '清空',
                    url: top.$.rootUrl + '/LR_SystemModule/Log/Form?categoryId=' + categoryId,
                    height: 200,
                    width: 400,
                    callBack: function (id) {
                        return top[id].acceptClick(refreshGirdData);
                    }
                });
            });
            // 导出
            $('#lr_export').on('click', function () {
                learun.layerForm({
                    id: "ExcelExportForm",
                    title: '导出Excel数据',
                    url: top.$.rootUrl + '/Utility/ExcelExportForm?gridId=gridtable&filename=log',
                    width: 500,
                    height: 380,
                    callBack: function (id) {
                        return top[id].acceptClick();
                    },
                    btn: ['导出Excel', '关闭']
                });
            });
        },
        initleft: function () {
            $('#lr_left_list li').on('click', function () {
                var $this = $(this);
                var $parent = $this.parent();
                $parent.find('.active').removeClass('active');
                $this.addClass('active');

                categoryId = $this.attr('data-value');
                page.search();
            });
        },
        initGrid: function () {
            $('#gridtable').jfGrid({
                url: top.$.rootUrl + '/LR_SystemModule/Log/GetPageList',
                headData: [
                     {
                        label: "操作时间", name: "F_OperateTime",width: 135, align: "left",
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd hh:mm:ss');
                        }
                     },
                    { label: "操作用户", name: "F_OperateAccount",width: 140, align: "left" },
                    { label: "IP地址", name: "F_IPAddress", width: 100, align: "left" },
                    { label: "系统功能", name: "F_Module", width: 150, align: "left" },
                    { label: "操作类型", name: "F_OperateType", width: 70, align: "center" },
                    {
                        label: "执行结果", name: "F_ExecuteResult", width: 70, align: "center",
                        formatter: function (cellvalue) {
                            if (cellvalue == '1') {
                                return "<span class=\"label label-success\">成功</span>";
                            } else {
                                return "<span class=\"label label-danger\">失败</span>";
                            }
                        }
                    },
                    { label: "执行结果描述", name: "F_ExecuteResultJson", width: 300, align: "left" }

                ],

                mainId: 'F_ItemDetailId',
                isPage: true,
                sidx: 'F_OperateTime'
            });
        },
        search: function (param) {
            param = param || {};
            param.CategoryId = categoryId;
            param.StartTime = logbegin;
            param.EndTime = logend;

            $('#gridtable').jfGridSet('reload', { queryJson: JSON.stringify(param) });
        }
    };



    page.init();
}


