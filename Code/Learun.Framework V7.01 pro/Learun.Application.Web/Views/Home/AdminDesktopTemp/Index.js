$(function () {
    "use strict";

    // 基于准备好的dom，初始化echarts实例
    var pieChart = echarts.init(document.getElementById('piecontainer'));
    // 指定图表的配置项和数据
    var pieoption = {
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {c} ({d}%)"
        },
        legend: {
            bottom: 'bottom',
            data: ['枢纽楼', 'IDC中心', '端局', '模块局', '营业厅','办公大楼','C网基站']
        },
        series: [
            {
                name: '用电占比',
                type: 'pie',
                radius: '75%',
                center: ['50%', '50%'],
                label : {
                    normal : {
                        formatter: '{b}:{c}: ({d}%)',
                        textStyle : {
                            fontWeight : 'normal',
                            fontSize : 12,
                            color:'#333'
                        }
                    }
                },
                data: [
                    { value: 10, name: '枢纽楼' },
                    { value: 10, name: 'IDC中心' },
                    { value: 10, name: '端局' },
                    { value: 10, name: '模块局' },
                    { value: 10, name: '营业厅' },
                    { value: 10, name: '办公大楼' },
                    { value: 40, name: 'C网基站' }
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
        ,
        color:['#df4d4b','#304552','#52bbc8','rgb(224,134,105)','#8dd5b4','#5eb57d','#d78d2f']
    };
    // 使用刚指定的配置项和数据显示图表。
    pieChart.setOption(pieoption);


    // 基于准备好的dom，初始化echarts实例
    var lineChart = echarts.init(document.getElementById('linecontainer'));
    // 指定图表的配置项和数据
    var lineoption = {
        tooltip: {
            trigger: 'axis'
        },
        legend: {
            bottom: 'bottom',
            data: ['预算', '实际']
        },
        grid: {
            bottom: '8%',
            containLabel: true
        },
        xAxis: {
            type: 'category',
            boundaryGap: false,
            data: ['1月', '2月', '3月', '4月', '5月', '6月', '7月', '8月', '9月', '10月', '11月', '12月']
        },
        yAxis: {
            type: 'value'
        },
        series: [
            {
                name: '预算',
                type: 'line',
                stack: '用电量',
                itemStyle: {
                    normal: {
                        color: "#fc0d1b",
                        lineStyle: {
                            color: "#fc0d1b"
                        }
                    }
                },
                data: [7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 23.3, 18.3, 13.9, 9.6, 1]
            },
            {
                name: '实际',
                type: 'line',
                stack: '用电量',
                itemStyle: {
                    normal:{
                        color:'#344858',
                        lineStyle:{
                            color:'#344858'
                        }
                    }

                },
                data: [3.9, 4.2, 5.7, 8.5, 11.9, 15.2, 17.0, 16.6, 14.2, 10.3, 6.6, 4.8]
            }
        ]
    };
    // 使用刚指定的配置项和数据显示图表。
    lineChart.setOption(lineoption);

    window.onresize = function (e) {
        pieChart.resize(e);
        lineChart.resize(e);
    }

    $(".lr-desktop-panel").mCustomScrollbar({ // 优化滚动条
        theme: "minimal-dark"
    });
});