var ECharts = {
    ChartDataFormate: {
        FormateNOGroupData: function (data) {
            //data的格式如上的Result1，这种格式的数据，多用于饼图、单一的柱形图的数据源
            var categories = [];
            var datas = [];
            for (var i = 0; i < data.length; i++) {
                categories.push(data[i].name || "");
                datas.push({ name: data[i].name, value: data[i].value || 0 });
            }
            return { category: categories, data: datas };
        },
        FormateGroupData: function (data, type, is_stack) {
            //data[{name:"",value:"",group:""},...]
            //data的格式如上的Result2，type为要渲染的图表类型：可以为line，bar，is_stack表示为是否是堆积图，这种格式的数据多用于展示多条折线图、分组的柱图
            var chart_type = 'line';
            if (type)
                chart_type = type || 'line';
            var xAxis = [];
            var group = [];
            var series = [];
            for (var i = 0; i < data.length; i++) {
                for (var j = 0; j < xAxis.length && xAxis[j] != data[i].name; j++);
                if (j == xAxis.length)
                    xAxis.push(data[i].name);
                for (var k = 0; k < group.length && group[k] != data[i].group; k++);
                if (k == group.length)
                    group.push(data[i].group);
            }
            for (var i = 0; i < group.length; i++) {
                var temp = [];
                for (var j = 0; j < data.length; j++) {
                    if (group[i] == data[j].group) {
                        if (type == "map") {
                            temp.push({ name: data[j].name, value: data[i].value });
                        } else {
                            temp.push(data[j].value);
                        }
                    }
                }
                switch (type) {
                    case 'bar':
                        var series_temp = { name: group[i], data: temp, type: chart_type };
                        series_temp = $.extend({}, {
                            markPoint: {
                                data: [
                                    { type: 'max', name: '最大值' },
                                    { type: 'min', name: '最小值' }
                                ]
                            },
                            markLine: {
                                data: [
                                    { type: 'average', name: '平均值' }
                                ]
                            }
                        }, series_temp);
                        break;

                    case 'map':
                        var series_temp = {
                            name: group[i], type: chart_type, mapType: 'china', selectedMode: 'multiple',
                            itemStyle: {
                                normal: { label: { show: true } },
                                emphasis: { label: { show: true } }
                            },
                            data: temp
                        };
                        break;
                    case 'line':
                        var series_temp = { name: group[i], data: temp, type: chart_type };
                        if (is_stack)
                            series_temp = $.extend({}, { stack: 'stack' }, series_temp);
                        series_temp = $.extend({}, {
                            markPoint: {
                                data: [
                                    { type: 'max', name: '最大值' },
                                    { type: 'min', name: '最小值' }
                                ]
                            },
                            markLine: {
                                data: [
                                    { type: 'average', name: '平均值' }
                                ]
                            }
                        }, series_temp);
                        break;

                    default:
                        var series_temp = { name: group[i], data: temp, type: chart_type };
                }
                series.push(series_temp);
            }
            return { category: group, xAxis: xAxis, series: series };
        }
    }
    ,
    ChartOptionTemplates: {
        CommonOption: {
            title: {
                text: '力软智能图表',
                left: "40%"
            },
            //通用的图表基本配置 
            tooltip: {
                trigger: 'item'//tooltip触发方式:axis以X轴线触发,item以每一个数据项触发 
            },
            toolbox: {
                show: true,
                orient: 'vertical',
                left: 'left',
                top: 'top',
                feature: {
                    mark: { show: true },
                    dataView: { show: true, readOnly: false },
                    restore: { show: true },
                    saveAsImage: { show: true }
                }
            }
        },
        CommonLineOption: {//通用的折线图表的基本配置 
            title: {
                text: '力软智能图表',
                left:"40%"
            },
            tooltip: {
                trigger: 'axis'
            },
            calculable: true,
            toolbox: {
                show: true,
                //orient : 'vertical',
                left: 'right',
                top: 'top',
                feature: {
                    mark: { show: true },
                    dataView: { show: true, readOnly: false },
                    restore: { show: true },
                    magicType: ['line', 'bar'],//支持柱形图和折线图的切换 
                    saveAsImage: { show: true }
                }
            }
        },
        Pie: function (data, name) {
            //data:数据格式：{name：xxx,value:xxx}...
            var pie_datas = ECharts.ChartDataFormate.FormateNOGroupData(data);
            var option = {
                tooltip: {
                    trigger: 'item',
                    formatter: '{b} : {c} ({d}/%)',
                    show: true
                },
                legend: {
                    orient: 'vertical',
                    x: 'left',
                    data: pie_datas.category
                },
                calculable: true,
                toolbox: {
                    show: true,
                    feature: {
                        mark: { show: true },
                        dataView: { show: true, readOnly: true },
                        restore: { show: true },
                        saveAsImage: { show: true }
                    }
                },
                series: [
                    {
                        name: name || "",
                        type: 'pie',
                        radius: '65%',
                        center: ['50%', '50%'],
                        data: pie_datas.data
                    }
                ]
            }; 
            return $.extend({}, ECharts.ChartOptionTemplates.CommonOption, option);
        },
        Lines: function (data, name, is_stack) {
            //data:数据格式：{name：xxx,group:xxx,value:xxx}... 
            var stackline_datas = ECharts.ChartDataFormate.FormateGroupData(data, 'line', is_stack);
            var option = {
                legend: {
                    data: stackline_datas.xAxis
                },
                xAxis: [{
                    type: 'category', //X轴均为category，Y轴均为value 
                    data: stackline_datas.xAxis,
                    boundaryGap: false//数值轴两端的空白策略 
                }],
                yAxis: [{
                    type: 'value',
                    splitArea: { show: true }
                }],
                series: stackline_datas.series
            };
            return $.extend({}, ECharts.ChartOptionTemplates.CommonLineOption, option);
        },
        Bars: function (data, name, is_stack) {
            //data:数据格式：{name：xxx,group:xxx,value:xxx}...
            var bars_dates = ECharts.ChartDataFormate.FormateGroupData(data, 'bar', is_stack);
            var option = {
                legend: { data: bars_dates.category },
                xAxis: [{
                    type: 'category',
                    data: bars_dates.xAxis
                }],

                yAxis: [{
                    type: 'value'
                }],
                toolbox: bars_dates.toolbox,
                series: bars_dates.series
            };
            return $.extend({}, ECharts.ChartOptionTemplates.CommonLineOption, option);
        },
        Maps: function (data, name, is_stack) {
            //data:数据格式：{name：xxx,group:xxx,value:xxx}...
            var maps_dates = ECharts.ChartDataFormate.FormateGroupData(data, 'map', is_stack);
            var option = {
                tooltip: {
                    trigger: 'item'
                },
                legend: {
                    orient: 'vertical',
                    left: 'left',
                    data: ['iphone3', 'iphone4', 'iphone5']
                },
                visualMap: {
                    min: 0,
                    max: 2500,
                    left: 'left',
                    top: 'bottom',
                    text: ['高', '低'],           // 文本，默认为数值文本
                    calculable: true
                },
                series: maps_dates.series
            };
            return $.extend({}, ECharts.ChartOptionTemplates.CommonOption, option);
        }
    }


};
