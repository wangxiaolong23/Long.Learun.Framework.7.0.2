var bootstrap = function ($, learun) {
    "use strict";
    var page = {
        init: function () {
            page.initGrid();
            page.initChart();
            page.bind();
        },
        bind: function () {
            // 刷新
            $('#lr-replace').on('click', function () {
                location.reload();
            });
        },
        initGrid: function () {
            $(".lr-layout-grid").height($(window).height() - 110);
            $('#gridtable').jfGrid({
                url: top.$.rootUrl + '/LR_ReportModule/ReportTemplate/GetFinanceReportList',
                headData: [
                    { name: "month", label: "月份", width: 120, align: "center" },
                    { name: "business", label: "业务摘要", width: 200, align: "left" },
                    { name: "tradedate", label: "交易日期", width: 120, align: "center" },
                    { name: "incomeMoney", label: "收入金额", width: 120, align: "left" },
                    { name: "expenditureMoney", label: "支出金额", width: 120, align: "left" },
                    { name: "balance", label: "余额", width: 120, align: "left" },
                    { name: "description", label: "备注", width: 180, align: "left" }
                ],
                reloadSelected: true,
                mainId: 'billNo',
                isAutoHeight: true
            });
            page.search();
        },
        initChart: function () {
            loadmain();
            loadmain1();
            function loadmain() {
                var myChart = echarts.init(document.getElementById('main'));
                var option = {
                    title: {
                        text: '各项支出分析图',
                        x: 'center'
                    },
                    tooltip: {
                        trigger: 'item',
                        formatter: "{a} <br/>{b} : {c} ({d}%)"
                    },
                    legend: {
                        orient: 'vertical',
                        left: 'left',
                        data: ['代发工资', '代扣水电费', '贷款利息', '客户贷款']
                    },
                    series: [
                        {
                            name: '访问来源',
                            type: 'pie',
                            radius: '55%',
                            center: ['50%', '60%'],
                            data: [
                                { value: 335, name: '代发工资' },
                                { value: 310, name: '代扣水电费' },
                                { value: 234, name: '贷款利息' },
                                { value: 135, name: '客户贷款' }
                            ],
                            itemStyle: {
                                emphasis: {
                                    shadowBlur: 10,
                                    shadowOffsetX: 0,
                                    shadowColor: 'rgba(0, 0, 0, 0.5)'
                                }
                            }
                        }
                    ]
                };
                myChart.setOption(option);
            }
            function loadmain1() {
                var myChart = echarts.init(document.getElementById('main1'));
                var option = {
                    title: {
                        text: '公司收支趋势图',
                        x: 'center'
                    },
                    tooltip: {
                        trigger: 'axis'
                    },
                    grid: {
                        left: '3%',
                        right: '4%',
                        bottom: '3%',
                        containLabel: true
                    },
                    xAxis: {
                        type: 'category',
                        boundaryGap: false,
                        data: ['周一', '周二', '周三', '周四', '周五', '周六', '周日']
                    },
                    yAxis: {
                        type: 'value'
                    },
                    series: [
                        {
                            name: '收入',
                            type: 'line',
                            stack: '总量',
                            data: [120, 132, 101, 134, 90, 230, 210]
                        },
                        {
                            name: '支付',
                            type: 'line',
                            stack: '总量',
                            data: [220, 182, 191, 234, 290, 330, 310]
                        }
                    ]
                };

                myChart.setOption(option);
            }
        },
        search: function (param) {
            param = param || {};
            $('#gridtable').jfGridSet('reload', param);
        }
    };
    page.init();
}


