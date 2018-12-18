/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：王小龙 wxl 
 * 日 期：2018-12-15
 * 描 述：根据配置动态加载桌面
 */
(function ($, learun) {
    "use strict";

    function loadDesktop() {
        //获取用户信息
        var loginInfo = learun.clientdata.get(['userinfo']);
        if (loginInfo) {
            //统计指标
            var par = {
                keyValue: '3142dc6c-f694-49f6-9032-c9ecb724663e',//设计的桌面统计配置表单ID
                queryJson: '{ F_Enabled: "1" }'//获取有效的数据
            };
            learun.httpAsync('GET', top.$.rootUrl + '/LR_FormModule/FormRelation/GetPreviewList', par, function (data) {
                if (data) {
                    $("#Target").html("");
                    //数组排序
                    var sortData = data.sort(function (a, b) {
                        if (a.f_sortcode0 < b.f_sortcode0) return -1;
                        if (a.f_sortcode0 > b.f_sortcode0) return 1;
                        return 0;
                    });
                    //权限过滤
                    var powerData = [];
                    $.each(sortData, function (index, itemobj) {
                        if (loginInfo.userId == "System") {
                            powerData.push(itemobj);
                        }
                    });
                    $.each(powerData, function (index, itemobj) {
                        //往桌面添加统计小功能
                        var htmlStr = "";
                        htmlStr += '<div class="lr-item-20" style="width: ' + parseInt($("#Target").width() / sortData.length) + 'px;">';
                        htmlStr += '<div class="task-stat" style="background-color: ' + itemobj.f_bgcolorcode0 + ';">';
                        htmlStr += '<div class="visual"><i class="' + itemobj.f_icon0 + '"></i></div>';
                        htmlStr += '<div class="details"><div id="' + itemobj.f_targetid0 + 'number" class="number"></div>';
                        htmlStr += '<div class="desc">' + itemobj.f_name0 + '</div></div>';
                        htmlStr += '<a id="' + itemobj.f_targetid0 + 'more" class="more" style="background-color: ' + itemobj.f_morecolorcode0 + ';" href="javascript:;">查看更多 <i class="fa fa-arrow-circle-right"></i></a>';
                        htmlStr += '</div></div>';
                        $("#Target").append(htmlStr);
                        //绑定数值
                        var res = learun.httpGet(top.$.rootUrl + '/LR_SystemModule/Desktop/GetSqlData', { databaseLinkId: itemobj.f_databaselinkid0, sql: itemobj.f_sql0 });
                        if (res) {
                            $("#" + itemobj.f_targetid0 + "number").html(res.data[0].value);
                        }
                        //绑定跳转
                        $('#' + itemobj.f_targetid0 + "more").on('click', itemobj, function (event) {
                            learun.frameTab.open({ F_ModuleId: event.data.f_targetid0, F_Icon: event.data.f_icon0, F_FullName: event.data.f_name0, F_UrlAddress: event.data.f_url0 });
                        });
                    });
                }
            });
            //消息列表
            var par2 = {
                keyValue: '24c44373-4f1e-4089-926a-c616c2ba3dad',//设计的桌面消息列配置表单ID
                queryJson: '{ F_Enabled: "1" }'//获取有效的数据
            };
            learun.httpAsync('GET', top.$.rootUrl + '/LR_FormModule/FormRelation/GetPreviewList', par2, function (data) {
                if (data) {
                    //数组排序
                    var sortData = data.sort(function (a, b) {
                        if (a.f_sortcode0 < b.f_sortcode0) return -1;
                        if (a.f_sortcode0 > b.f_sortcode0) return 1;
                        return 0;
                    });
                    //权限过滤
                    var powerData = [];
                    $.each(sortData, function (index, itemobj) {
                        if (loginInfo.userId == "System") {
                            powerData.push(itemobj);
                        }
                    });
                    $.each(powerData, function (index, itemobj) {
                        var htmlStr = "";
                        htmlStr += '<div class="col-xs-6" style="height:281px;">';
                        htmlStr += '    <div class="portal-panel-title">';
                        htmlStr += '        <i class="' + itemobj.f_icon0 + '"></i>&nbsp;&nbsp;' + itemobj.f_name0 + '（Top 5）';
                        htmlStr += '        <span id="' + itemobj.f_mlistid0 + 'more" class="menu" title="更多"><span class="point"></span><span class="point"></span><span class="point"></span></span>';
                        htmlStr += '    </div>';
                        htmlStr += '    <div class="portal-panel-content" style="overflow: hidden; padding-top: 20px; padding-left: 30px; padding-right: 50px;">';
                        //获取行数据
                        var res;
                        if (itemobj.f_getdatatype0 == "0") {//Sql语句获取方式
                            res = learun.httpGet(top.$.rootUrl + itemobj.f_interface0, { databaseLinkId: itemobj.f_databaselinkid0, sql: itemobj.f_sql0 });
                            if (res) {
                                for (var j = 0; j < res.data.length; j++) {
                                    res.data[j].f_itemurl = itemobj.f_itemurl0;
                                    htmlStr += '        <div id="' + res.data[j].f_id + 'item" class="lr-msg-line">';
                                    htmlStr += '            <a href="#" style="text-decoration: none;">' + res.data[j].f_title + '</a>';
                                    htmlStr += '                <label>' + res.data[j].f_time + '</label>';
                                    htmlStr += '        </div>';
                                }
                            }
                        } else if (itemobj.f_getdatatype0 == "1") {//接口获取方式
                            var param = null;
                            if (itemobj.f_interfaceparam0) {
                                try {
                                    param = JSON.parse(itemobj.f_interfaceparam0);
                                    console.log('转换成功：' + param);
                                } catch (e) {
                                    console.log(itemobj.f_name0 + '转JSON失败，error：' + itemobj.f_interfaceparam0 + '!!!' + e);
                                }
                            }
                            res = learun.httpGet(top.$.rootUrl + itemobj.f_interface0, param);
                            if (res) {
                                for (var j = 0; j < res["data"]["rows"].length; j++) {
                                    res.data.rows[j].f_itemurl = itemobj.f_itemurl0;
                                    var item = res.data.rows[j];
                                    htmlStr += '        <div id="' + item.F_TaskId + 'item" class="lr-msg-line">';
                                    htmlStr += '            <a href="#" style="text-decoration: none;">[' + item.F_SchemeName + ']' + item.F_ProcessName + '</a>';
                                    htmlStr += '                <label>' + item.F_CreateDate + '</label>';
                                    htmlStr += '        </div>';
                                }
                            }
                        }
                        htmlStr += '    </div>';
                        htmlStr += '</div>';
                        $("#MList").append(htmlStr);
                        //绑定列表跳转
                        $('#' + itemobj.f_mlistid0 + "more").on('click', itemobj, function (event) {
                            learun.frameTab.open({ F_ModuleId: event.data.f_mlistid0, F_Icon: event.data.f_icon0, F_FullName: event.data.f_name0, F_UrlAddress: event.data.f_url0 });
                        });
                        //绑定行跳转
                        if (itemobj.f_getdatatype0 == "0") {//Sql语句获取方式
                            if (res) {
                                for (var j = 0; j < res.data.length; j++) {
                                    $('#' + res.data[j].f_id + "item").on('click', res.data[j], function (event) {
                                        //新闻类列表直接装到内存到展示页展示
                                        top['dtlist' + event.data.f_id] = event.data;
                                        //替换链接中的参数
                                        var openurl = event.data.f_itemurl;
                                        $.each(event.data, function (key, value) {
                                            openurl = openurl.replace(new RegExp("@" + key, "g"), value);
                                        });
                                        learun.frameTab.open({ F_ModuleId: event.data.f_id, F_Icon: '', F_FullName: event.data.f_title, F_UrlAddress: openurl });
                                    });
                                }
                            }
                        } else if (itemobj.f_getdatatype0 == "1") {//接口获取方式
                            if (res) {
                                for (var j = 0; j < res.data.rows.length; j++) {
                                    var item = res.data.rows[j];
                                    $('#' + item.F_TaskId + "item").on('click', item, function (event) {
                                        var openurl = event.data.f_itemurl;
                                        //替换链接中的参数
                                        $.each(event.data, function (key, value) {
                                            openurl = openurl.replace(new RegExp("@" + key, "g"), value);
                                        });
                                        learun.frameTab.open({
                                            F_ModuleId: event.data.F_TaskId,
                                            F_Icon: '',
                                            F_FullName: '审核流程【' + event.data.F_ProcessName + '/' + event.data.F_TaskName + '】',
                                            F_UrlAddress: openurl
                                        });
                                    });
                                }
                            }
                        }
                    });
                }
            });

        }
        else {//如果当前没有用户信息，准备下次执行
            setTimeout(function () { loadDesktop(); }, 100);
        }
    }
    loadDesktop();
    //动态调整指标块宽度
    function targetresize() {
        $("#Target").find(".lr-item-20").each(function () {
            var width = parseInt($("#Target").width() / $("#Target").find(".lr-item-20").length);
            width = width < 150 ? 150 : width;
            $(this).css("width", width + 'px');
        });
    }

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
            data: ['枢纽楼', 'IDC中心', '端局', '模块局', '营业厅', '办公大楼', 'C网基站']
        },
        series: [
            {
                name: '用电占比',
                type: 'pie',
                radius: '75%',
                center: ['50%', '50%'],
                label: {
                    normal: {
                        formatter: '{b}:{c}: ({d}%)',
                        textStyle: {
                            fontWeight: 'normal',
                            fontSize: 12,
                            color: '#333'
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
        color: ['#df4d4b', '#304552', '#52bbc8', 'rgb(224,134,105)', '#8dd5b4', '#5eb57d', '#d78d2f']
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
                    normal: {
                        color: '#344858',
                        lineStyle: {
                            color: '#344858'
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
        setTimeout(function () { targetresize(); }, 100);
    };

    $(".lr-desktop-panel").mCustomScrollbar({ // 优化滚动条
        theme: "minimal-dark"
    });


})(window.jQuery, top.learun);