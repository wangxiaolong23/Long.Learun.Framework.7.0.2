/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.04.09
 * 描 述：数据列表选择	
 */
var dfopid = request('dfopid');
var acceptClick;

var bootstrap = function ($, learun) {
    "use strict";
    var selectItem;

    var gridData = null;
    var op = null;
    var treeValue = '';


    var selectData = {};


    var page = {
        init: function () {
            page.bind();
        },
        bind: function () {
            op = top.lrlayerSelect[dfopid];

            var _value = op._value.split(',');
            var _text = op._text.split(',');
            $.each(_value, function (_index, _item) {
                if (_item != "") {
                    selectData[_item] = _text[_index] || '';
                }
            });

            // 初始化表格
            $('#gridtable').jfGrid({
                headData: op.grid,
                isMultiselect: op.isMultiple,
                mainId: op.dataValueId,
                multiselectfield:'lrcheck',
                onSelectRow: function (data, isCheck) {
                    page.setSelect(data, isCheck);
                }
            });

            // 设置树形数据
            learun.clientdata.getAllAsync('sourceData', {
                code: op.treeCode,
                callback: function (_data) {
                    var treeData = $.lrtree.listTotree(_data, op.treeParentId, op.treeValueId, op.treeTextId, op.treeValueId, false);
                    $('#tree').lrtree({
                        data: treeData,
                        nodeClick: function (item) {
                            treeValue = item.value;
                            page.search("");
                        }
                    });
                }
            });
            // 获取表格数据
            learun.clientdata.getAllAsync('sourceData', {
                code: op.dataCode,
                callback: function (_data) {
                    //gridData = _data;
                    gridData = [];
                    var $list = $('#selected_item_list');

                    $.each(_data, function (_index, _item) {
                        if (selectData[_item[op.dataValueId]]) {
                            selectData[_item[op.dataValueId]] = _item[op.dataTextId];
                            _item.lrcheck = 1;

                            var _html = '<div class="item-selected-box" data-value="' + _item[op.dataValueId] + '" >';
                            _html += '<p><span>' + _item[op.dataTextId] + '</span></p>';
                            _html += '<span class="item-reomve" title="移除选中项"></span>';
                            _html += '</div>';
                            $list.append($(_html));
                        } else {
                            _item.lrcheck = 0;
                        }
                        gridData.push(_item);
                    });

                    $('#gridtable').jfGridSet('refreshdata', _data);
                }
            });

            // 搜索框初始化
            $('#txt_keyword').on("keypress", function (e) {
                if (event.keyCode == "13") {
                    var $this = $(this);
                    var keyword = $this.val();
                    page.search(keyword);
                }
            });

            $('.input-query').on('click', function () {
                var keyword = $('#txt_keyword').val();
                page.search(keyword);
            });

            // 已选项
            if (op.isMultiple) {
                $('#item_selected_btn').on('click', function () {
                    $('#form_warp_right').animate({ right: '0px' }, 300);
                });
                $('#item_selected_btn_close').on('click', function () {
                    $('#form_warp_right').animate({ right: '-180px' }, 300);
                });
            }
            else {
                $('#item_selected_btn').remove();
                $('.input-query').css('right','10px');
            }

            $('.selected-item-list-warp').lrscroll();

            var $warp = $('#selected_item_list');
            $warp.on('click', function (e) {
                var et = e.target || e.srcElement;
                var $et = $(et);
                if ($et.hasClass('item-reomve')) {
                    var id = $et.parent().attr('data-value');
                    $et.parent().remove();
                    delete selectData[id];
                    var keyword = $('#txt_keyword').val();
                    page.search(keyword);
                }
            });

        },
        search: function (text) {
            if (gridData == null) {
                setTimeout(function () {
                    page.search(text);
                }, 100);
            }
            else {
                var _data = [];
                $.each(gridData, function (_index, _item) {
                    if (!!selectData[_item[op.dataValueId]]) {
                        _item.lrcheck = 1;
                    }
                    else {
                        _item.lrcheck = 0;
                    }
                    if (_item[op.dataTreeId] == treeValue || treeValue == "") {
                        if (text == "" || _item[op.dataTextId].indexOf(text) != -1) {
                            _data.push(_item);
                        }
                    }
                });
                $('#gridtable').jfGridSet('refreshdata', _data);

            }
        },
        setSelect: function (data, ischeck) {
            var $list = $('#selected_item_list');
            if (ischeck) {
                if ($list.find('[data-value="' + data[op.dataValueId] + '"]').length == 0) {
                    selectData[data[op.dataValueId]] = data[op.dataTextId];
                    var _html = '<div class="item-selected-box" data-value="' + data[op.dataValueId] + '" >';
                    _html += '<p><span>' + data[op.dataTextId] + '</span></p>';
                    _html += '<span class="item-reomve" title="移除选中项"></span>';
                    _html += '</div>';
                    $list.append($(_html));
                }
            }
            else {
                $list.find('[data-value="' + data[op.dataValueId] + '"]').remove();
                delete selectData[data[op.dataValueId]];
            }

        }

    };
    // 保存数据
    acceptClick = function (callBack) {
        if (op.isMultiple) {
            callBack(selectData, dfopid);
        } else {
            var item = $('#gridtable').jfGridGet('rowdata');
            selectData = {}; 
            if (item) {
                selectData[item[op.dataValueId]] = item[op.dataTextId];
            }
            callBack(selectData, dfopid);
        }
        return true;
    };
    page.init();
}