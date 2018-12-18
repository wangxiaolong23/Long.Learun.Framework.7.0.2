/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.11.11
 * 描 述：报表管理
 */
var keyValue = '';
var bootstrap = function ($, learun) {
    "use strict";
    var selectedRow = learun.frameTab.currentIframe().selectedRow;
    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            // 加载导向
            $('#wizard').wizard().on('change', function (e, data) {
                var $finish = $("#btn_finish");
                var $next = $("#btn_next");
                if (data.direction == "next") {
                    if (data.step == 1) {
                        if (!$('#step-1').lrValidform()) {
                            return false;
                        }
                        $finish.removeAttr('disabled');
                        $next.attr('disabled', 'disabled');
                    }
                } else {
                    $finish.attr('disabled', 'disabled');
                    $next.removeAttr('disabled');
                }
            });
            //报表分类
            $('#F_TempCategory').lrDataItemSelect({ code: 'ReportSort', maxHeight: 160 });
            //报表风格
            $("[name='F_TempStyle']").click(function () {
                if ($(this).val() == 1) {
                    $("div.F_TempType").hide();
                    $("div.F_TempType").find("#F_TempType").attr("isvalid", "no");
                    $("div.list-pane").show(); $("div.chart-pane").hide();
                } else if ($(this).val() == 2) {
                    $("div.F_TempType").show();
                    $("div.F_TempType").find("#F_TempType").attr("isvalid", "yes");
                    $("div.chart-pane").show(); $("div.list-pane").hide();
                } else {
                    $("div.F_TempType").show();
                    $("div.F_TempType").find("#F_TempType").attr("isvalid", "yes");
                    $("div.chart-pane,div.list-pane").show();
                }
            })
            //图表类型
            $('#F_TempType').lrselect();
            //数据库
            $('#F_DataSourceId').lrselect({
                url: top.$.rootUrl + '/LR_SystemModule/DatabaseLink/GetTreeList',
                type: 'tree',
                placeholder: '请选择数据库',
            });

            //完成事件
            $("#btn_finish").click(function () {
                if (!$('#wizard-steps').lrValidform()) {
                    return false;
                }
                var postData = $('#wizard-steps').lrGetFormData(keyValue);
                postData["F_TempStyle"] = $("[name='F_TempStyle']:checked").val();
                postData["F_ParamJson"] = function () {
                    var paramJson = {
                        F_DataSourceId: $("#F_DataSourceId").lrselectGet(),
                        F_ChartSqlString: $("#F_ChartSqlString").val(),
                        F_ListSqlString: $("#F_ListSqlString").val()
                    }
                    return JSON.stringify(paramJson);
                }();
                $.lrSaveForm(top.$.rootUrl + '/LR_ReportModule/ReportManage/SaveForm?keyValue=' + keyValue, postData, function (res) {
                    learun.frameTab.currentIframe().refreshGirdData();
                });
            })
        },
        initData: function () {
            if (!!selectedRow) {
                keyValue = selectedRow.F_TempId;
                $('#wizard-steps').lrSetFormData(selectedRow);
                $("[name='F_TempStyle'][value=" + selectedRow.F_TempStyle + "]").attr("checked", "checked").trigger("click");
                $('#wizard-steps').lrSetFormData(JSON.parse(selectedRow.F_ParamJson));
            }
        }
    };
    page.init();
}


