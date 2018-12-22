/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：王小龙 wxl 
 * 日 期：2018-12-15
 * 描 述：根据配置动态加载桌面
 */
(function ($, learun) {
    "use strict";
    //因为要等异步加载完页面才能实例化滚动条
    var isScroll = 0;
    //图表对象数组，用于调整大小
    var chartArr = [];
    //排序并设置权限
    function sortAndPower(loginInfo, data) {
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
        return powerData;
    }
    //动态加载桌面数据
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
                    $("#lr_target").html("");
                    var powerData = sortAndPower(loginInfo, data);
                    $.each(powerData, function (index, itemobj) {
                        //往桌面添加统计小功能
                        var width = parseInt($("#lr_target").width() / powerData.length);
                        width = width < 220 ? 220 : width;
                        var htmlStr = "";
                        htmlStr += '<div class="lr-item-20" style="width: ' + width + 'px;">';
                        htmlStr += '<div class="task-stat" style="background-color: ' + itemobj.f_bgcolorcode0 + ';">';
                        htmlStr += '<div class="visual"><i class="' + itemobj.f_icon0 + '"></i></div>';
                        htmlStr += '<div class="details"><div id="' + itemobj.f_targetid0 + 'number" class="number"></div>';
                        htmlStr += '<div class="desc">' + itemobj.f_name0 + '</div></div>';
                        htmlStr += '<a id="' + itemobj.f_targetid0 + 'more" class="more" style="background-color: ' + itemobj.f_morecolorcode0 + ';" href="javascript:;">查看更多 <i class="fa fa-arrow-circle-right"></i></a>';
                        htmlStr += '</div></div>';
                        $("#lr_target").append(htmlStr);
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
                    //添加滚动条
                    $("#lr_target").lrscroll();
                    if ($("#lr_target").width() < 1100) {
                        $("#lr_target_box").css("width", '1100px');
                    }
                    isScroll++;
                }
            });
            //消息列表
            var par2 = {
                keyValue: '24c44373-4f1e-4089-926a-c616c2ba3dad',//设计的桌面消息列配置表单ID
                queryJson: '{ F_Enabled: "1" }'//获取有效的数据
            };
            learun.httpAsync('GET', top.$.rootUrl + '/LR_FormModule/FormRelation/GetPreviewList', par2, function (data) {
                if (data) {
                    var powerData = sortAndPower(loginInfo, data);
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
                                    htmlStr += '        <div class="lr-msg-line">';
                                    htmlStr += '            <a id="' + res.data[j].f_id + 'item" href="#" title="' + res.data[j].f_title + '" style="text-decoration: none;white-space: nowrap;">' + (res.data[j].f_title.length > 13 ? res.data[j].f_title.substr(0, 13) + "..." : res.data[j].f_title) + '</a>';
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
                                    htmlStr += '        <div class="lr-msg-line">';
                                    htmlStr += '            <a id="' + item.F_TaskId + 'item" href="#" title="' + item.F_ProcessName + '" style="text-decoration: none;white-space: nowrap;">[' + item.F_SchemeName + ']' + (item.F_ProcessName.length > 13 ? item.F_ProcessName.substr(0, 13) + "..." : item.F_ProcessName) + '</a>';
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
                    isScroll++;
                }
            });
            //报表
            var par3 = {
                keyValue: '7b334cd7-8d8c-442f-b88f-838241ca83bb',//设计的桌面图表配置表单ID
                queryJson: '{ F_Enabled: "1" }'//获取有效的数据
            };
            learun.httpAsync('GET', top.$.rootUrl + '/LR_FormModule/FormRelation/GetPreviewList', par3, function (data) {
                if (data) {
                    var powerData = sortAndPower(loginInfo, data);
                    $.each(powerData, function (index, itemobj) {
                        var htmlStr = "";
                        htmlStr += "<div class=\"col-xs-6\">";
                        htmlStr += "    <div class=\"portal-panel-title\">";
                        htmlStr += "        <i class=\"" + itemobj.f_icon0 + "\"></i>&nbsp;&nbsp;" + itemobj.f_name0 + "";
                        htmlStr += "    </div>";
                        htmlStr += "    <div class=\"portal-panel-content\">";
                        htmlStr += "        <div id=\"" + itemobj.f_chartid0 + "chart\" class=\"lr-chart-container\"></div>";
                        htmlStr += "    </div>";
                        htmlStr += "</div>";
                        $("#Charts").append(htmlStr);
                        //绑定数值
                        var res = learun.httpGet(top.$.rootUrl + '/LR_SystemModule/Desktop/GetSqlData', { databaseLinkId: itemobj.f_databaselinkid0, sql: itemobj.f_sql0 });
                        if (res) {
                            var nameArr = [], valueArr = [];
                            $.each(res.data, function (index1, itemobj1) {
                                nameArr.push(itemobj1.name);
                                valueArr.push(itemobj1.value);
                            });
                            // 基于准备好的dom，初始化echarts实例$("#" + itemobj.f_chartid0 + "chart")
                            var chart = echarts.init(document.getElementById(itemobj.f_chartid0 + "chart"));
                            // 指定图表的配置项和数据
                            var option = {
                                legend: {
                                    bottom: 'bottom',
                                    data: nameArr
                                },
                                series: [
                                    {
                                        name: itemobj.f_name0,
                                        type: itemobj.f_charttype0,
                                        data: res.data,
                                    }
                                ]
                            };
                            switch (itemobj.f_charttype0) {
                                case "pie"://饼图
                                    option.tooltip = {
                                        trigger: 'item',
                                        formatter: "{a} <br/>{b} : {c} ({d}%)"
                                    };
                                    $.each(option.series, function (key, item) {
                                        item.radius = '75%';
                                        item.center = ['50%', '50%'];
                                        item.label = {
                                            normal: {
                                                formatter: '{b}:{c}: ({d}%)',
                                                textStyle: {
                                                    fontWeight: 'normal',
                                                    fontSize: 12,
                                                    color: '#333'
                                                }
                                            }
                                        };
                                    });
                                    break;
                                case "line"://折线图
                                    option.tooltip = {
                                        trigger: 'axis'
                                    };
                                    option.xAxis = {
                                        type: 'category',
                                        boundaryGap: false,
                                        data: nameArr
                                    };
                                    option.yAxis = {
                                        type: 'value'
                                    };
                                    break;
                                case "bar"://折线图
                                    option.tooltip = {
                                        trigger: 'axis'
                                    };
                                    option.xAxis = {
                                        type: 'category',
                                        boundaryGap: false,
                                        data: nameArr
                                    };
                                    option.yAxis = {
                                        type: 'value'
                                    };
                                    break;
                                default:
                            }
                            // 使用刚指定的配置项和数据显示图表。
                            chart.setOption(option);
                            chartArr.push(chart);
                        }
                    });
                    isScroll++;
                }
            });
        }
        else {//如果当前没有用户信息，准备下次执行
            setTimeout(function () { loadDesktop(); }, 100);
        }
    }
    loadDesktop();
    //添加滚动条
    function islrscroll() {
        if (isScroll == 3) {//当isScroll==3时，实例化滚动条
            $(".lr-desktop-panel").lrscroll();
        } else {//如果当前没有异步加载完页面，准备下次执行
            setTimeout(function () { islrscroll(); }, 100);
        }
    }
    islrscroll();

    //页面大小调整后，元素自动调整大小
    window.onresize = function (e) {
        //动态调整指标块宽度
        $("#lr_target").find(".lr-item-20").each(function () {
            var width = parseInt($("#lr_target").width() / $("#lr_target").find(".lr-item-20").length);
            width = width < 220 ? 220 : width;
            $(this).css("width", width + 'px');
        });
        //根据页面宽度调整信息列表列是否展示
        if ($("#lr_target").width() < 700) {
            $(".lr-msg-line").find("label").each(function () {
                $(this).css("display", "none");
            });
        } else {
            $(".lr-msg-line").find("label").each(function () {
                $(this).css("display", "block");
            });
        }
        //动态调整指标块滚动条
        if ($("#lr_target").width() < 1100) {
            $("#lr_target_box").css("width", '1100px');
        }
        //避免火狐乱触发onresize事件导致echart爆错
        if ($("#lr_target").width() != 0) {
            //调整图标大小
            $.each(chartArr, function (key, chart) {
                chart.resize(e);
            });
        }
    };
})(window.jQuery, top.learun);