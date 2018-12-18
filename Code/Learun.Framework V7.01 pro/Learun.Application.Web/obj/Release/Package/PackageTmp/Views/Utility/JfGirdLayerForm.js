/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.11
 * 描 述：数据列表选择	
 */
var acceptClick;
var op = top.lrGirdLayerEdit;
var bootstrap = function ($, learun) {
    "use strict";

    console.log(op);


    var selectItem;
    var griddata = null;
    var page = {
        init: function () {
            page.bind();
        },
        bind: function () {
            $('#btn_Search').on('click', function () {
                if (griddata != null) {
                    var data = [];
                    var keyword = $('#txt_Keyword').val();
                    if (!!keyword) {
                        for (var i = 0, l = griddata.length; i < l; i++) {
                            var item = griddata[i];
                            for (var j = 0, jl = op.edit.op.colData.length; j < jl; j++) {
                                if (item[op.edit.op.colData[j].name] && item[op.edit.op.colData[j].name].indexOf(keyword) != -1) {
                                    data.push(item);
                                    break;
                                }
                            }
                        }
                        $('#gridtable').jfGridSet('refreshdata', data);
                    }
                    else {
                        $('#gridtable').jfGridSet('refreshdata', griddata);
                    }

                }
            });
            $('#gridtable').jfGrid({
                headData: op.edit.op.colData,
                url: op.edit.op.url,
                param: op.edit.op.param,
                onRenderComplete: function (data) {
                    griddata = data;
                },
                dblclick: function (row) {
                    top.lrGirdLayerEditCallBack(row);
                    learun.layerClose(window.name);
                },
                onSelectRow: function (row) {
                    selectItem = row;
                }
            });

            $('#gridtable').jfGridSet('reload');
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        callBack(selectItem);
        return true;
    };
    page.init();
}