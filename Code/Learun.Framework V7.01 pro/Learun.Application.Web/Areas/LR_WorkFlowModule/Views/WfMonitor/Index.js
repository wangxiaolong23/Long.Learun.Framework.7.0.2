/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.08.04
 * 描 述：流程监控
 */
var bootstrap = function ($, learun) {
    "use strict";
    var categoryId = '1';
    var logbegin = '';
    var logend = '';

    var page = {
        init: function () {
            $('#lr_verify').hide();
            page.initleft();
            page.initGrid();
            page.bind();
        },
        bind: function () {
            $('#datesearch').lrdate({
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

            // 查询
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                page.search({ keyword: keyword });
            });
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 查看流程进度
            $('#lr_eye').on('click', function () {
                var processId = $('#gridtable').jfGridValue('F_Id');
                var processName = $('#gridtable').jfGridValue('F_ProcessName');

                if (learun.checkrow(processId)) {
                    learun.frameTab.open({ F_ModuleId: "eye" + processId, F_Icon: 'fa magic', F_FullName: '查看流程进度【' + processName + '】', F_UrlAddress: '/LR_WorkFlowModule/WfMyTask/CustmerWorkFlowForm?tabIframeId=' + "eye" + processId + '&type=101' + "&processId=" + processId });
                }
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
                url: top.$.rootUrl + '/LR_WorkFlowModule/WfMonitor/GetPorcessList',
                headData: [
                    { label: "标题", name: "F_ProcessName", width: 150, align: "left" },
                    { label: "模板名称", name: "F_SchemeName", width: 150, align: "left" },
                    {
                        label: "等级", name: "F_ProcessLevel", width: 80, align: "center",
                        formatter: function (cellvalue) {
                            switch (cellvalue) {
                                case 0:
                                    return '普通';
                                    break;
                                case 1:
                                    return '重要';
                                    break;
                                case 2:
                                    return '紧急';
                                    break;
                            }
                        }
                    },
                    {
                        label: "状态", name: "F_EnabledMark", width: 70, align: "center",
                        formatter: function (cellvalue, row) {
                            if (row.F_IsFinished == 0) {
                                if (cellvalue == 1) {
                                    return "<span class=\"label label-success\">运行中</span>";
                                } else {
                                    return "<span class=\"label label-danger\">暂停</span>";
                                }
                            }
                            else {
                                return "<span class=\"label label-warning\">结束</span>";
                            }

                        }
                    },
                    { label: "发起者", name: "F_CreateUserName", width: 70, align: "center" },
                    {
                        label: "发起时间", name: "F_CreateDate", width: 150, align: "left",
                        formatter: function (cellvalue) {
                            return learun.formatDate(cellvalue, 'yyyy-MM-dd hh:mm:ss');
                        }
                    },
                    { label: "备注", name: "F_Description", width: 300, align: "left" }

                ],
                mainId: 'F_Id',
                isPage: true,
                sidx: 'F_CreateDate'
            });
        },
        search: function (param) {
            param = param || {};
            param.StartTime = logbegin;
            param.EndTime = logend;
            param.categoryId = categoryId;
            $('#gridtable').jfGridSet('reload', { queryJson: JSON.stringify(param) });
        }
    };

    page.init();
}


