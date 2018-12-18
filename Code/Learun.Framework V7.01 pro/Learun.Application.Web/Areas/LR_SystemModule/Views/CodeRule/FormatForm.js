/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.18
 * 描 述：单据编号规则	
 */
var acceptClick;
var currentColRow = top.layer_Form.currentColRow;
var bootstrap = function ($, learun) {
    "use strict";

    var itemTypeName;

    var page = {
        init: function () {
            page.bind();
            page.initData();
        },
        bind: function () {
            // 前缀
            $('#itemType').lrselect({
                select: function (item) {
                    if (!!item && item.id == "2") {
                        $('#stepValue').removeAttr('readonly');
                        $('#stepValue').attr('isvalid', 'yes');

                        $('#initValue').removeAttr('readonly');
                        $('#initValue').attr('isvalid', 'yes');
                    }
                    else {
                        $('#stepValue').attr('readonly', 'readonly');
                        $('#stepValue').attr('isvalid', 'no');
                        $('#stepValue').val('');
                        $('#stepValue').lrRemoveValidMessage();

                        $('#initValue').attr('readonly', 'readonly');
                        $('#initValue').attr('isvalid', 'no');
                        $('#initValue').val('');
                        $('#initValue').lrRemoveValidMessage();
                    }

                    var $formatStr = $('#formatStr');
                    $formatStr.lrRemoveValidMessage();
                    var $parent = $formatStr.parent();
                    if (!!item) {
                        $formatStr.remove();
                        itemTypeName = item.text;
                        switch (item.id) {
                            case '0':
                                $parent.append('<input id="formatStr" type="text" class="form-control" isvalid="yes" checkexpession="NotNull" />');
                                break;
                            case '1':
                                $parent.append('<div id="formatStr" isvalid="yes" checkexpession="NotNull"><ul>'
                                + '<li data-value="mmdd">mmdd</li>'
                                + '<li data-value="ddmm">ddmm</li>'
                                + '<li data-value="mmyy">mmyy</li>'
                                + '<li data-value="yymm">yymm</li>'
                                + '<li data-value="yyyymm">yyyymm</li>'
                                + '<li data-value="yymmdd">yymmdd</li>'
                                + '<li data-value="yyyymmdd">yyyymmdd</li>'
                                + '</ul></div>');
                                $('#formatStr').lrselect({ maxHeight: 145 });
                                break;
                            case '2':
                                $parent.append('<div id="formatStr" isvalid="yes" checkexpession="NotNull"><ul>'
                                + '<li data-value="000">000</li>'
                                + '<li data-value="0000">0000</li>'
                                + '<li data-value="00000">00000</li>'
                                + '<li data-value="000000">000000</li>'
                                + '</ul></div>');
                                $('#formatStr').lrselect();
                                break;
                            case '3':
                                $parent.append('<div id="formatStr" isvalid="yes" checkexpession="NotNull"><ul>'
                                + '<li data-value="code">公司编号</li>'
                                + '<li data-value="name">公司名称</li>'
                                + '</ul></div>');
                                $('#formatStr').lrselect();
                                break;
                            case '4':
                                $parent.append('<div id="formatStr" isvalid="yes" checkexpession="NotNull"><ul>'
                                + '<li data-value="code">部门编号</li>'
                                + '<li data-value="name">部门名称</li>'
                                + '</ul></div>');
                                $('#formatStr').lrselect();
                                break;
                            case '5':
                                $parent.append('<div id="formatStr" isvalid="yes" checkexpession="NotNull"><ul>'
                                + '<li data-value="code">用户编号</li>'
                                + '<li data-value="name">用户名称</li>'
                                + '</ul></div>');
                                $('#formatStr').lrselect();
                                break;
                        }
                    }
                }
            });
        },
        initData: function () {
            if (!!currentColRow) {
                $('#form').lrSetFormData(currentColRow);
            }
        }
    };
    // 保存数据
    acceptClick = function (callBack) {
        if (!$('#form').lrValidform()) {
            return false;
        }
        var data = $('#form').lrGetFormData();
        data.itemTypeName = itemTypeName;
        if (!!callBack) { callBack(data); }
        return true;
    };
    page.init();
}