/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.17
 * 描 述：自定义查询
 */
var code = request('code');


var bootstrap = function ($, learun) {
    "use strict";

    var fieldData = [];

    var page = {
        init: function () {
            page.bind();
        },
        bind: function () {
            //获取字段数据
            learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/DataSource/GetDataColName', { code: code }, function (data) {
                var headData = [];
                for (var i = 0, l = data.length; i < l; i++)
                {
                   
                    var item = data[i];
                    if (item != 'rownum') {
                        //var point = { id: item, text: item };
                        var point2 = { label: item, name: item, width: 150, align: "left" };
                        //fieldData.push(point);
                        headData.push(point2);
                    }
                }

                //$('#field').lrselectRefresh({
                //    data: fieldData
                //});
              
                $('#gridtable').jfGrid({
                    url: top.$.rootUrl + '/LR_SystemModule/DataSource/GetDataTablePage',
                    headData: headData,
                    isPage: true
                });
               // page.search();
            });

            // 功能选择
            //$('#field').lrselect({
            //    maxHeight: 300,
            //    allowSearch: true
            //});

            //$('#logic').lrselect({
            //    maxHeight: 300
            //});

            // 查询
            $('#btn_Search').on('click', function () {
                page.search();
            });
        },
        search: function () {
            var param = {};
            param.code = code;

            //param.field = $('#field').lrselectGet();
            //param.logic = $('#logic').lrselectGet();
            //param.keyword = $('#keyword').val();

            $('#gridtable').jfGridSet('reload', param);
        }
    };

    page.init();
}


