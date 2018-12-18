$.LoadReport = function (options) {
    var defaults = {
        url: "",
        element: "",
    };
    var options = $.extend(defaults, options);
    $.ajax({
        url: options.url,
        cache: false,
        async: false,
        dataType: 'json',
        success: function (data) {
            var $echart, $list;
            if (data.tempStyle == 1) {
                if (data.listData.length > 0) {
                    $list = $('<div id="gridtable" class="lr-layout-body"></div>');
                    options.element.append($list);
                    DrawList(data.listData, $list);
                }
            } else if (data.tempStyle == 2) {
                if (data.chartData.length > 0) {
                    $echart = $('<div id="echart" style="width: 100%; height: 400px;"></div>');
                    options.element.append($echart);
                    switch (data.chartType) {
                        case 'pie':
                            DrawPie(data.chartData, $echart[0]);
                            break;
                        case 'bar':
                            DrawBar(data.chartData, $echart[0]);
                            break;
                        case 'line':
                            DrawLine(data.chartData, $echart[0]);
                            break;
                        default:
                    }

                }
            } else {
                if (data.chartData.length > 0) {
                    $echart = $('<div id="echart" style="width: 100%; height: 400px;"></div>');
                    options.element.append($echart);
                    switch (data.chartType) {
                        case 'pie':
                            DrawPie(data.chartData, $echart[0]);
                            break;
                        case 'bar':
                            DrawBar(data.chartData, $echart[0]);
                            break;
                        case 'line':
                            DrawLine(data.chartData, $echart[0]);
                            break;
                        default:
                    }

                }
                if (data.listData.length > 0) {
                    $list = $('<div id="gridtable" class="lr-layout-body"></div>');
                    options.element.append($list);
                    DrawList(data.listData, $list);
                }
            }
        }
    });
    function DrawPie(data, echartElement) {
        var myChart = echarts.init(echartElement);
        var option = ECharts.ChartOptionTemplates.Pie(data);
        myChart.setOption(option);
    }
    function DrawBar(data, echartElement) {
        var myChart = echarts.init(echartElement);
        var option = ECharts.ChartOptionTemplates.Bars(data, 'bar', true);
        myChart.setOption(option);
    }
    function DrawLine(data, echartElement) {
        var myChart = echarts.init(echartElement);
        var option = ECharts.ChartOptionTemplates.Lines(data, 'line', true);
        myChart.setOption(option);
    }
    function DrawList(data, listElement) {
        listElement.jfGrid({
            headData: function () {
                var colModelData = [];
                for (key in data[0]) {
                    colModelData.push({ name: key, label: key, width: 120, align: "left" });
                }
                return colModelData;
            }(),
            rowdatas: data,
            isAutoHeight: true
        });
    }
}
