(function () {
    var listData = [];
    var searchData = [];
    var isSearch = false;

    var page = {
        isScroll: true,
        init: function ($page, param) {
            // 加载数据（数据字典或者数据源）

            var $list = $page.find('#lr_select_layer_list');

            switch (param.op.type){
                case 'dataItem':
                    learun.clientdata.getAll('dataItem', {
                        code: param.op.code,
                        callback: function (data) {
                            listData = data;
                            $.each(data, function (_index, _item) {
                                var $item = $('<div class="lr-list-item lr-list-item-multi" data-value="' + _index + '" ></div>');
                                $.each(param.op.layerData, function (_jindex, _jitem) {
                                    if (_jitem.hide !== 1) {
                                        var _id = _jitem.name === 'F_ItemName' ? 'text' : 'value';
                                        $item.append('<p class="lr-ellipsis"><span>' + _jitem.label + ":</span>" + _item[_id] + '</p>');
                                    }
                                });
                                $list.append($item);
                                $item = null;
                            });
                        }
                    });

                    break;
                case 'sourceData':
                    learun.clientdata('sourceData', {
                        code: op.code,
                        callback: function (data) {
                            listData = data;
                            $.each(data, function (_index, _item) {
                                var $item = $('<div class="lr-list-item lr-list-item-multi" data-value="' + _index + '" ></div>');
                                $.each(param.op.layerData, function (_jindex, _jitem) {
                                    if (_jitem.hide !== 1) {
                                        $item.append('<p class="lr-ellipsis"><span>' + _jitem.label + ":</span>" + _item[_jitem.name] + '</p>');
                                    }
                                });
                                $list.append($item);
                                $item = null;
                            });
                        }
                    });
                    break;
            }
            // 点击事件
            $list.on('tap', function (e) {
                e = e || window.event;
                var et = e.target || e.srcElement;
                var $et = $(et);
                if ($et.hasClass('lr-ellipsis')) {
                    $et = $et.parents('.lr-list-item');
                }

                if ($et.hasClass('lr-list-item')) {
                    var _index = $et.attr('data-value');
                    var item = null;
                    if (!isSearch) {
                        item = listData[_index];
                    }
                    else {
                        item = searchData[_index];
                    }
                    if (param.op.type === 'dataItem') {// 如果是数据字典的话，为了与pc端的字段保持一致需要做一次转化
                        item['F_ItemName'] = item.text;
                        item['F_ItemValue'] = item.value;
                    }
                    param.callback(item, param.op, param.$this);
                    learun.nav.closeCurrent();
                    return false;
                }
            });
            $list = null;
            // 搜索框
            $page.find('input').on('input propertychange', function () {
                var keyword = $(this).val();
                var _$list = $('#lr_select_layer_list');
                if (keyword) {
                    _$list.html("");
                    searchData = [];
                    $.each(listData, function (_index, _item) {
                        var $item = $('<div class="lr-list-item lr-list-item-multi" data-value="' + _index + '" ></div>');
                        var isAdd = false;
                        $.each(param.op.layerData, function (_jindex, _jitem) {
                            if (_jitem.hide !== 1) {
                                var _id = '';
                                if (param.op.type === 'dataItem') {
                                    _id = _jitem.name === 'F_ItemName' ? 'text' : 'value';
                                }
                                $item.append('<p class="lr-ellipsis"><span>' + _jitem.label + ":</span>" + _item[_id || _jitem.name] + '</p>');
                                if (_item[_id || _jitem.name].indexOf(keyword) !== -1) {
                                    isAdd = true;
                                }
                            }
                        });
                        if (isAdd) {
                            searchData.push(_item);
                            _$list.append($item);
                        }
                        $item = null;
                    });
                }
                else {
                    _$list.html("");
                    $.each(listData, function (_index, _item) {
                        var $item = $('<div class="lr-list-item lr-list-item-multi" data-value="' + _index + '" ></div>');
                        $.each(param.op.layerData, function (_jindex, _jitem) {
                            if (_jitem.hide !== 1) {
                                if (param.op.type === 'dataItem') {
                                    var _id = _jitem.name === 'F_ItemName' ? 'text' : 'value';
                                    $item.append('<p class="lr-ellipsis"><span>' + _jitem.label + ":</span>" + _item[_id] + '</p>');
                                }
                                else {
                                    $item.append('<p class="lr-ellipsis"><span>' + _jitem.label + ":</span>" + _item[_jitem.name] + '</p>');
                                }
                            }
                        });
                        _$list.append($item);
                        $item = null;
                    });
                }
            });
        },
        destroy: function (pageinfo) {
            listData = [];
            searchData = [];
            isSearch = false;
        }
    };
    return page;
})();