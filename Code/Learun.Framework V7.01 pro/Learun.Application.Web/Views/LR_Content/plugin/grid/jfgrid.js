(function ($, learun) {
    "use strict";
    var $jfgridmove = null;
    var cb = ['checkbox_0.png', 'checkbox_1.png', 'checkbox_2.png'];
    var imageurl = top.$.rootUrl + '/Content/images/jfgrid/';

    // 初始化表头数据
    var headDataInit = function (data, parent, headData, cols, frozenCols, running) {
        var _left = running.left;
        var _frozenleft = running.frozenleft;

        var _top = 0;
        var _deep = 0;
        var _frozen = false;

        if (parent) {
            _left = parent.left;
            _top = parent.top + 28;
            _deep = parent.deep + 1;
            _frozen = parent.frozen;
        }
        var _width = 0;
        var len = data.length;
        $.each(data, function (_index, _item) {//_item:label 显示列名 /name 字段名 /width 宽度 /align 对齐方式 /frozen 冻结列 /statistics 统计 /isMerge 合并
            //设置列表头数据
            var col = {
                data: _item,
                height: 28,
                width: _item.width || 100,
                top: _top,
                left: _left,
                frozen: _frozen,
                deep: _deep,
                last: true,
                parent: parent
            };

            if (!parent && _item.frozen) {
                col.frozen = true;
                col.left = _frozenleft;
            }
            headData.push(col);



            // 如果有子节点
            if (_item.children && _item.children.length > 0) {
                col.last = false;
                col.width = headDataInit(_item.children, col, headData, cols, frozenCols, running);
            }
            else {
                running.MaxDeep = running.MaxDeep > col.deep ? running.MaxDeep : col.deep;

                if (col.frozen) {
                    frozenCols.push(col);
                }
                else {
                    cols.push(col);
                }

                // 判断是否需要和并行
                if (_item.isMerge) {
                    running.mergeCols.push(col);
                }
                // 如果有统计列
                if (_item.statistics) {
                    running.isStatistic = true;
                }
            }

            if (!parent) {
                if (col.frozen) {
                    _frozenleft += col.width;
                    running.frozenleft += col.width;
                }
                else {
                    _left += col.width;
                    running.left += col.width;
                }
            }
            else {
                _left += col.width;
            }
            _width += col.width;
        });


        return _width;
    };
    // 如果整体表格宽度大于所有列之和就调整之后一列的宽度
    var setColWidth = function (col, width, isUpdate) {
        col._width = col.width + width;
        if (isUpdate) {
            col.$cell.css({ 'width': col._width });
        }
        if (col.parent) {
            setColWidth(col.parent, width);
        }
    };
    var setLastColWidth = function ($self, op, isUpdate) {
        var width = $self.innerWidth();
        var colwidth = op.running.headWidth + op.running.leftWidth;
        if (op.running.cols.length > 0) {
            var col = op.running.cols[op.running.cols.length - 1];
            var _width = 0;
            var flag = false;
            if (width > colwidth) {
                _width = width - colwidth + 1;
                setColWidth(col, _width, isUpdate);
                flag = true;
            }
            else if (col._width != undefined) {
                _width = 1;
                setColWidth(col, _width, isUpdate);
                flag = true;
            }


            if (flag) {
                $self.find('#jfgrid_head_col_' + op.id).css({ 'width': (op.running.headWidth + _width) });
                if (isUpdate) {
                    $self.find('#jfgrid_body_' + op.id + '>.lr-scroll-box').css({ 'width': (op.running.headWidth + op.running.leftWidth + _width - 1) });
                    $self.find('#jfgrid_right_' + op.id).css({ 'width': (op.running.headWidth + _width - 1) });
                    $self.find('#jfgrid_right_' + op.id + '>[colname="' + col.data.name + '"]').css({ 'width': col._width });
                }
            }

        }
    };
    // 单元格数据格式化
    var formatterCell = function (value, item, row, rowItem, op) {
        if (value !== 0) {
            value = value || '';
        } 
        if (item.formatter) {
            var text = item.formatter(value, row, op, rowItem.$cell);
            rowItem.text = $('<div>' + (text || '') + '</div>').text();
            rowItem.$cell.attr('title', rowItem.text );

            var $expend = rowItem.$cell.find('.jfgrid-data-cell-expend');
            rowItem.$cell.html(text);
            rowItem.$cell.prepend($expend);

            if (item.statistics) {// 如果该单元格数据需要统计就进行计算
                rowItem.statisticsNum = rowItem.statisticsNum || 0;
                op.running.statisticData[item.name] = op.running.statisticData[item.name] || 0;
                op.running.statisticData[item.name] += (parseFloat(text || 0) - rowItem.statisticsNum);
                rowItem.statisticsNum = parseFloat(text || 0);
                $('#jfgrid_statistic_' + op.id + ' [name="' + item.name + '"]').text(op.running.statisticData[item.name]);
            }

            $expend = null;
        }
        else if (item.formatterAsync) {
            item.formatterAsync(function (text) {
                rowItem.text = $('<div>' + (text || '') + '</div>').text();
                rowItem.$cell.attr('title', rowItem.text);
                var $expend = rowItem.$cell.find('.jfgrid-data-cell-expend');
                rowItem.$cell.html(text);
                rowItem.$cell.prepend($expend);
                $expend = null;


                if (item.statistics) {// 如果该单元格数据需要统计就进行计算
                    rowItem.statisticsNum = rowItem.statisticsNum || 0;
                    op.running.statisticData[item.name] = op.running.statisticData[item.name] || 0;
                    op.running.statisticData[item.name] += (parseFloat(text || 0) - rowItem.statisticsNum);
                    rowItem.statisticsNum = parseFloat(text || 0);
                    $('#jfgrid_statistic_' + op.id + ' [name="' + item.name + '"]').text(op.running.statisticData[item.name]);
                }

            }, value, row, op, rowItem.$cell);
        }
        else {
            // 如果是编辑单元列
            if (item.edit) {
                switch (item.edit.type) {
                    case 'input':          // 输入框 文本,数字,密码
                        break;
                    case 'select':         // 下拉框选择
                        // 如果有data
                      
                        if (item.edit.op.data) {
                            $.each(item.edit.op.data, function (_index, _item) {
                                if (_item[item.edit.op.value] == value) {
                                    rowItem.text = _item[item.edit.op.text];
                                    rowItem.$cell.attr('title', rowItem.text);

                                    var $expend = rowItem.$cell.find('.jfgrid-data-cell-expend');
                                    rowItem.$cell.html(rowItem.text);
                                    rowItem.$cell.prepend($expend);
                                    $expend = null;
                                    return false;
                                }
                            });
                            return;
                        }
                        else {
                            if (item.edit.datatype === 'dataItem') {
                                learun.clientdata.getAsync('dataItem', {
                                    key: value,
                                    code: item.edit.code,
                                    rowItem: rowItem,
                                    callback: function (_data,_op) {
                                        _op.rowItem.text = _data.text;
                                        _op.rowItem.$cell.attr('title', _op.rowItem.text);
                                        var $expend = _op.rowItem.$cell.find('.jfgrid-data-cell-expend');
                                        _op.rowItem.$cell.html(_op.rowItem.text);
                                        _op.rowItem.$cell.prepend($expend);
                                        $expend = null;
                                    }
                                });
                                return;
                            }
                            else if (item.edit.datatype === 'dataSource'){
                                learun.clientdata.getAsync('sourceData', {
                                    key: value,
                                    keyId: item.edit.op.value,
                                    code: item.edit.code,
                                    rowItem: rowItem,
                                    callback: function (_data, _op) {
                                        _op.rowItem.text = _data[item.edit.op.text];
                                        _op.rowItem.$cell.attr('title', _op.rowItem.text);

                                        var $expend = _op.rowItem.$cell.find('.jfgrid-data-cell-expend');
                                        _op.rowItem.$cell.html(_op.rowItem.text);
                                        _op.rowItem.$cell.prepend($expend);
                                        $expend = null;
                                    }
                                });
                                return;
                            }
                        }
                        break;
                    case 'radio':          // 单选
                        if (item.edit.data) {
                            $.each(item.edit.data, function (_index, _item) {
                                if (_item.id == value) {
                                    rowItem.text = _item.text;
                                    rowItem.$cell.attr('title', rowItem.text);
                                    var $expend = rowItem.$cell.find('.jfgrid-data-cell-expend');
                                    rowItem.$cell.html(rowItem.text);
                                    rowItem.$cell.prepend($expend);
                                    $expend = null;
                                    return false;
                                }
                            });
                            return;
                        }
                        else {
                            if (item.edit.datatype === 'dataItem') {
                                learun.clientdata.getAsync('dataItem', {
                                    key: value,
                                    code: item.edit.code,
                                    rowItem: rowItem,
                                    callback: function (_data, _op) {
                                        _op.rowItem.text = _data.text;
                                        _op.rowItem.$cell.attr('title', _op.rowItem.text);
                                        var $expend = _op.rowItem.$cell.find('.jfgrid-data-cell-expend');
                                        _op.rowItem.$cell.html(_op.rowItem.text);
                                        _op.rowItem.$cell.prepend($expend);
                                        $expend = null;
                                    }
                                });
                                return;
                            }
                            else if (item.edit.datatype === 'dataSource') {
                                learun.clientdata.getAsync('sourceData', {
                                    key: value,
                                    keyText: item.edit.op.text,
                                    keyId: item.edit.op.value,
                                    code: item.edit.code,
                                    rowItem: rowItem,
                                    callback: function (_data, _op) {
                                        _op.rowItem.text = _data[_op.keyText];
                                        _op.rowItem.$cell.attr('title', _op.rowItem.text);

                                        var $expend = _op.rowItem.$cell.find('.jfgrid-data-cell-expend');
                                        _op.rowItem.$cell.html(_op.rowItem.text);
                                        _op.rowItem.$cell.prepend($expend);
                                        $expend = null;
                                    }
                                });
                                return;
                            }
                        }
                        break;
                    case 'checkbox':       // 多选
                        if (value != undefined && value != null && value != "") {
                            if (item.edit.data) {
                                var _vlist = value.split(',');
                                var _vmap = {};
                                $.each(_vlist, function (_index, _item) {
                                    _vmap[_item] = '1';
                                });
                                var _text = [];

                                $.each(item.edit.data, function (_index, _item) {
                                    if (_vmap[_item.id] == '1') {
                                        _text.push(_item.text);
                                    }
                                });

                                rowItem.text = String(_text);
                                rowItem.$cell.attr('title', rowItem.text);
                                var $expend = rowItem.$cell.find('.jfgrid-data-cell-expend');
                                rowItem.$cell.html(rowItem.text);
                                rowItem.$cell.prepend($expend);
                                $expend = null;
                                return;
                            }
                            else {
                                if (item.edit.datatype === 'dataItem') {
                                    learun.clientdata.getAllAsync('dataItem', {
                                        code: item.edit.code,
                                        rowItem: rowItem,
                                        value: value,
                                        callback: function (_dataes, _op) {
                                            var _vlist = _op.value.split(',');
                                            var _vmap = {};
                                            $.each(_vlist, function (_index, _item) {
                                                _vmap[_item] = '1';
                                            });
                                            var _text = [];

                                            $.each(_dataes, function (_index, _item) {
                                                if (_vmap[_item.value] == '1') {
                                                    _text.push(_item.text);
                                                }
                                            });

                                            _op.rowItem.text = String(_text);
                                            _op.rowItem.$cell.attr('title', _op.rowItem.text);
                                            var $expend = _op.rowItem.$cell.find('.jfgrid-data-cell-expend');
                                            _op.rowItem.$cell.html(_op.rowItem.text);
                                            _op.rowItem.$cell.prepend($expend);
                                            $expend = null;
                                        }
                                    });
                                    return;
                                }
                                else if (item.edit.datatype === 'dataSource') {
                                    learun.clientdata.getAllAsync('sourceData', {
                                        value: value,
                                        keyId: item.edit.op.value,
                                        keyText: item.edit.op.text,
                                        code: item.edit.code,
                                        rowItem: rowItem,
                                        callback: function (_dataes, _op) {
                                            var _vlist = _op.value.split(',');
                                            var _vmap = {};
                                            $.each(_vlist, function (_index, _item) {
                                                _vmap[_item] = '1';
                                            });
                                            var _text = [];

                                            $.each(_dataes, function (_index, _item) {
                                                if (_vmap[_item[_op.keyId]] == '1') {
                                                    _text.push(_item[_op.keyText]);
                                                }
                                            });

                                            _op.rowItem.text = String(_text);
                                            _op.rowItem.$cell.attr('title', _op.rowItem.text);
                                            var $expend = _op.rowItem.$cell.find('.jfgrid-data-cell-expend');
                                            _op.rowItem.$cell.html(_op.rowItem.text);
                                            _op.rowItem.$cell.prepend($expend);
                                            $expend = null;
                                        }
                                    });
                                    return;
                                }
                            }
                        }
                        break;
                    case 'datatime':       // 时间
                        if (item.edit.dateformat == "0") {
                            value = learun.formatDate(value, "yyyy-MM-dd");
                            row[item.name] = value;
                        }

                        break;
                    case 'layer':          // 弹层
                        // 获取下行号
                        var rownum = rowItem.$cell.attr('rowindex');
                        var $expend = rowItem.$cell.find('.jfgrid-data-cell-expend');
                        rowItem.text = value;
                        rowItem.$cell.attr('title', rowItem.text);
                        rowItem.$cell.html((value || '') + '<i class="fa fa-ellipsis-h" value="' + rownum + '" ></i>');
                        rowItem.$cell.prepend($expend);

                        rowItem.$cell.find('.fa-ellipsis-h')[0].op = item;
                        rowItem.$cell.find('.fa-ellipsis-h')[0].row = row;

                        rowItem.$cell.find('.fa-ellipsis-h').on('click', function () {
                            var $this = $(this);
                            var op = $this[0].op;
                            var _row = $this[0].row;
                            var rownum = $this.attr('value');
                            op.edit.init && op.edit.init(_row, $this.parent(), rownum);
                            top.lrGirdLayerEdit = op;
                            top.lrGirdLayerEditCallBack = function (data) {
                                op.edit.change && op.edit.change(_row, rownum, data);
                                top.lrGirdLayerEdit = null;
                                top.lrGirdLayerEditCallBack = null;
                            };
                            if (item.edit.op) {
                                learun.layerForm({
                                    id: 'lrgridlayerform',
                                    title: '选择' + item.label,
                                    url: top.$.rootUrl + '/Utility/JfGirdLayerForm',
                                    height: item.edit.op.height || 400,
                                    width: item.edit.op.width || 600,
                                    callBack: function (id) {
                                        var res = top[id].acceptClick(function (data) {
                                            op.edit.change && op.edit.change(_row, rownum, data);
                                        });
                                        top.lrGirdLayerEdit = null;
                                        return res;
                                    }
                                });
                            }
                            return false;
                        });
                        return;
                        break;
                }
            }

            var $expend = rowItem.$cell.find('.jfgrid-data-cell-expend');
            rowItem.text = value;
            rowItem.$cell.attr('title', rowItem.text);
            rowItem.$cell.html(rowItem.text);
            rowItem.$cell.prepend($expend);
            if (item.statistics) {// 如果该单元格数据需要统计就进行计算
                rowItem.statisticsNum = rowItem.statisticsNum || 0;
                op.running.statisticData[item.name] = op.running.statisticData[item.name] || 0;
                op.running.statisticData[item.name] += (parseFloat(rowItem.text || 0) - rowItem.statisticsNum);
                rowItem.statisticsNum = parseFloat(rowItem.text || 0);

                $('#jfgrid_statistic_' + op.id + ' [name="' + item.name + '"]').text(op.running.statisticData[item.name]);
            }

            $expend = null;
        }
    };

    // 调整列宽设置宽度
    var setColWidthByMove = function (col, width) {
        col.width = col.width + width;
        col.$cell.css({ 'width': col.width });
        if (col.parent) {
            setColWidthByMove(col.parent, width);
        }
    };

    // 初始化编辑单元格
    var initEditCell = function ($self, op, col) {
        switch (col.data.edit.type) {
            case 'input':          // 输入框 文本,数字,密码
                col.$edit = $('<div class="jfgrid-edit-cell"><input id="jfgrid_edit_' + op.id + '_' + col.data.name + '" /></div>');
                if (col.frozen) {
                    $self.find('#jfgrid_left_' + op.id).append(col.$edit);
                }
                else {
                    $self.find('#jfgrid_right_' + op.id).append(col.$edit);
                }
                col.$edit.on("keypress", function (e) {
                    if (event.keyCode == "13") {
                        hideEditCell();
                    }
                });
                break;
            case 'select':         // 下拉框选择
                col.$edit = $('<div class="jfgrid-edit-cell"><div id="jfgrid_edit_' + op.id + '_' + col.data.name + '" ></div></div>');
                if (col.frozen) {
                    $self.find('#jfgrid_left_' + op.id).append(col.$edit);
                }
                else {
                    $self.find('#jfgrid_right_' + op.id).append(col.$edit);
                }
                col.data.edit.op = col.data.edit.op || {};
                col.$edit.find('div').lrselect(col.data.edit.op);

                col.data.edit.op.value = col.$edit.find('div')[0]._lrselect.dfop.value;
                col.data.edit.op.text = col.$edit.find('div')[0]._lrselect.dfop.text;
                if (col.data.edit.datatype == 'dataItem') {
                    learun.clientdata.getAllAsync('dataItem', {
                        code: col.data.edit.code,
                        callback: function (dataes) {
                            var list = [];
                            $.each(dataes, function (_index, _item) {
                                list.push({ id: _item.value, text: _item.text, title: _item.text, k: _index });
                            });
                            col.$edit.find('div').lrselectRefresh({ data: list });
                        }
                    });
                }
                else if (col.data.edit.datatype == 'dataSource') {
                    learun.clientdata.getAllAsync('sourceData', {
                        code: col.data.edit.code,
                        callback: function (dataes) {
                            col.$edit.find('div').lrselectRefresh({
                                data: dataes
                            });
                        }
                    });
                }

                break;
            case 'radio':          // 单选
                col.$edit = $('<div class="jfgrid-edit-cell"><div id="jfgrid_edit_' + op.id + '_' + col.data.name + '" class="radio"  ></div></div>');
                if (col.frozen) {
                    $self.find('#jfgrid_left_' + op.id).append(col.$edit);
                }
                else {
                    $self.find('#jfgrid_right_' + op.id).append(col.$edit);
                }
                if (col.data.edit.datatype == 'dataItem') {
                    col.$edit.find('div').lrRadioCheckbox({
                        type: 'radio',
                        code: col.data.edit.code
                    });
                }
                else if (col.data.edit.datatype == 'dataSource'){
                    col.$edit.find('div').lrRadioCheckbox({
                        type: 'radio',
                        dataType: 'dataSource',
                        code: col.data.edit.code,
                        text: col.data.edit.text,
                        value: col.data.edit.value
                    });
                }
                else {
                    $.each(col.data.edit.data || [], function (id, item) {
                        var $point = $('<label><input name="jfgrid_edit_' + op.id + '_' + col.data.name + '" value="' + item.id + '"' + (col.data.edit.dfvalue == item.id ? "checked" : "") + ' type="radio">' + item.text + '</label>');
                        col.$edit.find('div').append($point);
                    });
                }
                break;
            case 'checkbox':       // 多选
                col.$edit = $('<div class="jfgrid-edit-cell"><div id="jfgrid_edit_' + op.id + '_' + col.data.name + '" class="checkbox"  ></div></div>');
                if (col.frozen) {
                    $self.find('#jfgrid_left_' + op.id).append(col.$edit);
                }
                else {
                    $self.find('#jfgrid_right_' + op.id).append(col.$edit);
                }
                if (col.data.edit.datatype == 'dataItem') {
                    col.$edit.find('div').lrRadioCheckbox({
                        type: 'checkbox',
                        code: col.data.edit.code
                    });
                }
                else if (col.data.edit.datatype == 'dataSource') {
                    col.$edit.find('div').lrRadioCheckbox({
                        type: 'checkbox',
                        dataType: 'dataSource',
                        code: col.data.edit.code,
                        text: col.data.edit.text,
                        value: col.data.edit.value
                    });
                }
                else {
                    $.each(col.data.edit.data || [], function (id, item) {
                        var $point = $('<label><input name="jfgrid_edit_' + op.id + '_' + col.data.name + '" value="' + item.id + '"' + (col.data.edit.dfvalue == item.id ? "checked" : "") + ' type="checkbox">' + item.text + '</label>');
                        col.$edit.find('div').append($point);
                    });
                }
                break;
            case 'datatime':       // 时间
                var dateformat = col.data.edit.dateformat == '0' ? 'yyyy-MM-dd' : 'yyyy-MM-dd HH:mm';
                col.$edit = $('<div class="jfgrid-edit-cell"><input id="jfgrid_edit_' + op.id + '_' + col.data.name + '" onClick="WdatePicker({dateFmt:\'' + dateformat + '\',qsEnabled:false,isShowClear:false,isShowOK:false,isShowToday:false,onpicked:function(){$(\'#jfgrid_edit_' + op.id + '_' + col.data.name + '\').trigger(\'change\');}})"  type="text" class="form-control" /></div>');
                if (col.frozen) {
                    $self.find('#jfgrid_left_' + op.id).append(col.$edit);
                }
                else {
                    $self.find('#jfgrid_right_' + op.id).append(col.$edit);
                }
                col.$edit.on("keypress", function (e) {
                    if (event.keyCode == "13") {
                        hideEditCell();
                    }
                });
                break;
            case 'layer':          // 弹层
                break;
        }
    };
    // 单元格编辑项隐藏
    var hideEditCell = function () {
        $('.jfgrid-layout .jfgrid-edit-cell ').hide();
        $('.jfgrid-layout .lr-select-option').slideUp(150);
        $('.jfgrid-layout .lr-select').removeClass('lr-select-focus');
    };


    var _jfgrid = {
        init: function ($self, op) {
            if (op.url == '' || op.url == null || op.url == undefined) {
                op.isPage = false;
            }

            $self.html('');
            $self.addClass('jfgrid-layout');
            // 添加一个移动板
            if ($jfgridmove === null) {
                $jfgridmove = $('<div style="position: fixed;top: 0;left: 0;width: 100%;height: 100%;z-index: 9999;cursor: col-resize;display: none;" ></div>');
                $('body').append($jfgridmove);
            }
            // 整体表格布局
            _jfgrid.layout($self, op);
            _jfgrid.bind($self, op);
            // 渲染头部
            _jfgrid.head($self, op);
            // 渲染数据
            _jfgrid.dataRender($self, op, op.rowdatas);
            op = null;
        },
        layout: function ($self, op) {
            if (op.height != undefined && op.height != null && op.height > 0) {
                $self.css({ height: op.height });
            }
            // 头部
            var $head = $('<div class="jfgrid-head" id="jfgrid_head_' + op.id + '" ></div>');
            $head.append('<div class="jfgrid-border" id="jfgrid_border_' + op.id + '" ></div>');
            $head.append('<div class="jfgrid-head-col" id="jfgrid_head_col_' + op.id + '" ></div>');
            $self.append($head);
            // 数据显示部分
            var $body = $('<div class="jfgrid-body" id="jfgrid_body_' + op.id + '" ></div>');
            $body.append('<div class="jfgrid-left" id="jfgrid_left_' + op.id + '" ></div>');
            $body.append('<div class="jfgrid-right" id="jfgrid_right_' + op.id + '" ></div>');
            $self.append($body);
            // 底部
            var $footer = $('<div class="jfgrid-footer" id="jfgrid_footer_' + op.id + '" ></div>');
            $self.append($footer);

            // 调整列宽移动条
            $self.append('<div class="jfgrid-move-line" id="jfgrid_move_line_' + op.id + '"  ></div>');

        
            // 初始化滚动条
            $body.lrscroll(function (x, y) {
                if (!$self.is(":hidden")) {
                    $self.find('#jfgrid_left_' + op.id).css('left', x);
                    $self.find('#jfgrid_head_col_' + op.id).css('left', op.running.leftWidth - x);
                    if (op.running.isStatistic) {// 添加统计条单元格
                        $self.find('#jfgrid_statistic_' + op.id + '>.jfgrid-statistic-right').css('left', - x);
                    }

                    hideEditCell();
                }
            });

            // 没有数据的时候显示的图片
            $self.find('#jfgrid_body_' + op.id).append('<div class="jfgrid-nodata-img" id="jfgrid_nodata_img_' + op.id + '"  ><img src="' + top.$.rootUrl + '/Content/images/jfgrid/nodata.jpg"></div>');

            // 分页条
            if (op.isPage) {// 支持
                $self.css({ 'padding-bottom': '35px' });
                var $pagebar = $('<div class="jfgrid-page-bar" id="jfgrid_page_bar_' + op.id + '"><div class="jfgrid-page-bar-info" >无显示数据</div>\
                <div class="paginations" id="jfgrid_page_bar_nums_'+ op.id + '" style="display:none;" >\
                <ul class="pagination pagination-sm"><li><a href="javascript:void(0);" class="pagebtn">首页</a></li></ul>\
                <ul class="pagination pagination-sm"><li><a href="javascript:void(0);" class="pagebtn">上一页</a></li></ul>\
                <ul class="pagination pagination-sm" id="jfgrid_page_bar_num_' + op.id + '" ></ul>\
                <ul class="pagination pagination-sm"><li><a href="javascript:void(0);" class="pagebtn">下一页</a></li></ul>\
                <ul class="pagination pagination-sm"><li><a href="javascript:void(0);" class="pagebtn">尾页</a></li></ul>\
                <ul class="pagination"><li><span></span></li></ul>\
                <ul class="pagination"><li><input type="text" class="form-control"></li></ul>\
                <ul class="pagination pagination-sm"><li><a href="javascript:void(0);" class="pagebtn">跳转</a></li></ul>\
                </div></div>');
                $footer.append($pagebar);
                $pagebar.find('#jfgrid_page_bar_num_' + op.id).on('click', _jfgrid.turnPage);
                $pagebar.find('#jfgrid_page_bar_nums_' + op.id + ' .pagebtn').on('click', { op: op }, _jfgrid.turnPage2);
                $pagebar = null;
            }
            else if (op.isEdit) {
                $self.css({ 'padding-bottom': '29px' });
                var $toolbar = $('<div class="jfgrid-toolbar" id="jfgrid_toolbar_' + op.id + '"></div>');
                var $add = $('<span><i class="fa fa-plus"></i></span>');
                var $minus = $('<span><i class="fa fa-minus"></i></span>');
                $add.on('click', function () {
                    var _item = {};
                    op.rowdatas.push(_item);
                    op.onAddRow && op.onAddRow(_item, op.rowdatas);
                    if (op.isTree) {
                        _jfgrid.rowRender($self, op, { data: _item, childRows: [] }, 0, 1);
                    }
                    else {
                        _jfgrid.rowRender($self, op, _item, 0);
                    }
                    $self.find('#jfgrid_nodata_img_' + op.id).hide();
                    
                });
                $minus.on('click', function () {
                    // 获取选中行
                    var flag = false;
                    var res = true;
                    if (op.isMultiselect) {
                        var checklist = [];
                        $.each(op.running.rowdata, function (_index, _item) {
                            if (_item['jfcheck'].value == 1) {
                                res = true;
                                if (op.beforeMinusRow) {
                                    res = op.beforeMinusRow(_item['jfgridRowData']);
                                }
                                if (res) {
                                    op.rowdatas.splice(op.rowdatas.indexOf(_item['jfgridRowData']), 1);
                                    checklist.push(_item['jfgridRowData']);
                                    flag = true;
                                }
                            }
                        });
                        if (flag) {
                            op.onMinusRow && op.onMinusRow(checklist, op.rowdatas);
                        }
                    }
                    else {
                        if (op.running.rowSelected != null) {
                            res = true;
                            if (op.beforeMinusRow) {
                                res = op.beforeMinusRow(op.running.rowSelected['jfgridRowData']);
                            }
                            if (res) {
                                op.rowdatas.splice((op.running.rowSelected['jfnum'].value - 1), 1);
                                flag = true;
                                op.onMinusRow && op.onMinusRow(op.running.rowSelected, op.rowdatas);
                                op.running.rowSelected = null;
                            }
                        }
                    }
                    if (flag) {
                        _jfgrid.dataRender($self, op, op.rowdatas);
                    }

                    if (op.running.rowdata.length == 0) {
                        $self.find('#jfgrid_nodata_img_' + op.id).show();
                    }
                });
                $toolbar.append($add);
                $toolbar.append($minus);
                $footer.append($toolbar);
                $toolbar = null;
            }
            $head = null;
            $body = null;
            $footer = null;
        },
        bind: function ($self, op) {
            // 点击事件
            $self.on('click', function (e) {
                var $this = $(this);
                var op = $this[0].dfop;
                var et = e.target || e.srcElement;
                var $et = $(et);

                if (!$et.hasClass('jfgrid-edit-cell') && $et.parents('.jfgrid-edit-cell').length == 0) {
                    hideEditCell();
                }

                if (op.running.isWidhChanging) {// 调整列表宽度
                    //$.jfGrid.moveHeadWidth(dfop, false);
                    dfop.isWidhChanging = false;
                }
                else if ($et.hasClass('jfgrid-head-cell') || $et.parents('.jfgrid-head-cell').length > 0) {// 排序
                    if (!$et.hasClass('jfgrid-head-cell')) {
                        $et = $et.parents('.jfgrid-head-cell');
                    }
                    _jfgrid.sortCol($this, $et, op);
                }
                else if ($et.attr('colname') == 'jfgrid_subGrid' || $et.parent().attr('colname') == 'jfgrid_subGrid') {// 展开或关闭子表单
                    _jfgrid.expandSub($this, $et, op);
                }
                else if ($et.parent().hasClass('jfgrid-data-cell-expend')) {// 树形结构展开和关闭
                    _jfgrid.expandTree($et, op);
                }
                else if ($et.hasClass('jfgrid-data-cell') || $et.parents('.jfgrid-data-cell').length > 0) {// 选中行
                    _jfgrid.clickRow($this, $et, op);
                    e.stopPropagation();
                }
                else if ($et.attr('id') == ('jfgrid_all_cb_' + op.id)) {// 全部勾选
                    _jfgrid.checkAllRows($this, $et, op);
                }
            });
            // 双击事件
            $self.on('dblclick', function (e) {
                var $this = $(this);
                var op = $this[0].dfop;
                var et = e.target || e.srcElement;
                var $et = $(et);
                if ($et.hasClass('jfgrid-data-cell') || $et.parents('.jfgrid-data-cell').length > 0) {// 选中行
                    op.dblclick && op.dblclick(op.running.rowSelected['jfgridRowData']);
                }
            });

            // 鼠标移过事件
            $self.on('mouseover', function (e) {
                var $this = $(this);
                $this.find('.jfgrid-data-cell-over').removeClass('jfgrid-data-cell-over');

                var et = e.target || e.srcElement;
                var $et = $(et);
                if ($et.hasClass('jfgrid-data-cell') || $et.parents('.jfgrid-data-cell').length > 0) {
                    var rowid = $et.attr('rownum');
                    if (!rowid) {
                        rowid = $et.parents('.jfgrid-data-cell').attr('rownum');
                    }
                    $this.find('[rownum="' + rowid + '"]').addClass('jfgrid-data-cell-over');
                }

            });
            // 监听表格大小变化
            $self.resize(function () {
                var $this = $(this);
                var op = $this[0].dfop;
                setLastColWidth($this, op, true);

                // 如果有子表格调整宽度
                var width = $this.find('#jfgrid_body_' + op.id).width();
                $this.find('.jfgrid-sub').css({ 'width': width });
            });
            // 调整列宽
            $self.delegate('.jfgrid-heed-move', 'mousedown', { op: op }, function (e) {
                $jfgridmove.show();
                var op = e.data.op;
                op.running.moveing = true;
                op.running.xMousedown = e.pageX;
                var $moveline = $('#jfgrid_move_line_' + op.id);
                var path = parseInt($(this).parent().attr('path'));
                var col = op.running.headData[path];

                op.running.moveCol = col;
                op.running.moveWidth = col._width || col.width;
                op.running.moveLineLeft = col.left + op.running.moveWidth + op.running.leftWidth;
                if (col.frozen) {
                    op.running.moveLineLeft = op.running.moveLineLeft - op.running.frozenleft;
                }
                $moveline.css({ 'left': op.running.moveLineLeft }).show();
            });
            top.$(document).on('mousemove', { $obj: $self }, function (e) {
                var op = e.data.$obj[0].dfop;
                var x = e.pageX;               
                if (op.running.moveing) {
                    var $moveline = e.data.$obj.find('#jfgrid_move_line_' + op.id);
                    var width = op.running.moveWidth + (x - op.running.xMousedown);
                    width = (width < 40 ? 40 : width);
                    var left = op.running.moveLineLeft + (width - op.running.moveWidth);

                    $moveline.css({ 'left': left });
                }
            }).on('mouseup', { $obj: $self }, function (e) {
                var op = e.data.$obj[0].dfop;
                if (op.running.moveing) {
                    op.running.moveing = false;
                    var x = e.pageX;

                    if (op.running.moveCol) {
                        var width = op.running.moveWidth + (x - op.running.xMousedown);
                        width = (width < 40 ? 40 : width);
                        var _width = width - op.running.moveWidth;
                        if (_width != 0){
                            setColWidthByMove(op.running.moveCol, _width);
                            if (op.running.moveCol.frozen) {
                                op.running.frozenleft += _width;
                                op.running.leftWidth += _width;

                                e.data.$obj.find('#jfgrid_head_' + op.id).css({ 'padding-left': op.running.leftWidth });
                                e.data.$obj.find('#jfgrid_border_' + op.id).css({ 'width': op.running.leftWidth });
                                e.data.$obj.find('#jfgrid_head_col_' + op.id).css({ 'left': op.running.leftWidth });
                                e.data.$obj.find('#jfgrid_body_' + op.id + '>.lr-scroll-box').css({ 'padding-left': op.running.leftWidth });
                                e.data.$obj.find('#jfgrid_left_' + op.id).css({ 'width': op.running.leftWidth });
                                e.data.$obj.find('#jfgrid_left_' + op.id + '>[colname="' + op.running.moveCol.data.name + '"]').css({ 'width': op.running.moveCol.width });

                                if (op.running.isStatistic) {
                                    e.data.$obj.find('#jfgrid_statistic_' + op.id).css({ 'padding-left': op.running.leftWidth });
                                    e.data.$obj.find('#jfgrid_statistic_' + op.id + '>.jfgrid-statistic-left').css({ 'width': op.running.leftWidth });
                                    e.data.$obj.find('#jfgrid_statistic_' + op.id + ' [name="' + op.running.moveCol.data.name + '"]').css({ 'width': op.running.moveCol.width });
                                }
                            }
                            else {
                                op.running.headWidth += _width;
                                op.running.left += _width;

                                e.data.$obj.find('#jfgrid_head_col_' + op.id).css({ 'width': op.running.headWidth });
                                e.data.$obj.find('#jfgrid_right_' + op.id).css({ 'width': op.running.headWidth });
                                e.data.$obj.find('#jfgrid_right_' + op.id + '>[colname="' + op.running.moveCol.data.name + '"]').css({ 'width': op.running.moveCol.width });


                                if (op.running.isStatistic) {
                                    e.data.$obj.find('#jfgrid_statistic_' + op.id + '>.jfgrid-statistic-right').css({ 'width': op.running.headWidth });
                                    e.data.$obj.find('#jfgrid_statistic_' + op.id + ' [name="' + op.running.moveCol.data.name + '"]').css({ 'width': op.running.moveCol.width });
                                }

                            }

                            var path = parseInt(op.running.moveCol.$cell.attr('path'));

                            for (var i = path + 1, l = op.running.headData.length; i < l; i++) {
                                var col = op.running.headData[i];
                                if (col.frozen && op.running.moveCol.frozen) {
                                    col.left += _width;
                                    col.$cell.css({ 'left': col.left + op.running.leftWidth - op.running.frozenleft });
                                    e.data.$obj.find('#jfgrid_left_' + op.id + '>[colname="' + col.data.name + '"]').css({ 'left': col.left + op.running.leftWidth - op.running.frozenleft});


                                    if (op.running.isStatistic) {
                                        e.data.$obj.find('#jfgrid_statistic_' + op.id + ' [name="' + col.data.name + '"]').css({ 'left': col.left + op.running.leftWidth - op.running.frozenleft });
                                    }
                                }
                                else if (!col.frozen && !op.running.moveCol.frozen) {
                                    col.left += _width;
                                    col.$cell.css({ 'left': col.left});
                                    e.data.$obj.find('#jfgrid_right_' + op.id + '>[colname="' + col.data.name + '"]').css({ 'left': col.left });
                                    if (op.running.isStatistic) {
                                        e.data.$obj.find('#jfgrid_statistic_' + op.id + ' [name="' + col.data.name + '"]').css({ 'left': col.left });
                                    }
                                }
                            }
                            setLastColWidth(e.data.$obj, op, true);
                        }
                        op.running.moveCol = null;
                    }
                   

                    $jfgridmove.hide();
                    var $moveline = e.data.$obj.find('#jfgrid_move_line_' + op.id);
                    $moveline.hide();


                }
            });
        },
        head: function ($self, op) {
            op.running.MaxDeep = 0;

            op.running.headWidth = 0;
            op.running.headHeight = 0;

            op.running.leftWidth = 0;

            op.running.left = 0;
            op.running.frozenleft = 0;

            op.running.cols = [];
            op.running.frozenCols = [];
            op.running.mergeCols || [];
            op.running.headData = [];

            // 列表表头数据初始化
            headDataInit(op.headData, false, op.running.headData, op.running.cols, op.running.frozenCols, op.running);

            // 判断是否有需要统计
            if (op.running.isStatistic) {

                op.running.statisticData = {};

                // 判断是否加载了底部条
                $self.find('#jfgrid_footer_' + op.id).append('<div class="jfgrid-statistic" id="jfgrid_statistic_' + op.id + '"><div class="jfgrid-statistic-left"></div><div class="jfgrid-statistic-right" ></div></div>');
                if (op.isPage) {
                    $self.find('#jfgrid_footer_' + op.id).css({ 'height': 64, 'padding-top': '29px' });
                    $self.css({ 'padding-bottom': '64px' });
                }
                else {
                    $self.find('#jfgrid_footer_' + op.id).css({ 'height': 29 });
                    $self.css({ 'padding-bottom': '29px' });
                }
                $self.find('#jfgrid_toolbar_' + op.id).css({'width':'50px'});
            } else if (op.isEdit) {
                $self.find('#jfgrid_toolbar_' + op.id).css({ 'width': '100%' });
            }
            
            op.running.headHeight = op.running.MaxDeep * 28 + 28;
            op.running.headWidth = op.running.left;

            var $border = $self.find('#jfgrid_border_' + op.id);
            var $headcol = $self.find('#jfgrid_head_col_' + op.id);

            // 判断是否有序号列
            if (op.isShowNum) {
                $border.append('<div class="jfgrid-border-cell jfgrid-border-num"></div>');
                op.running.leftWidth += 30;
            }
            // 判断是否允许多选
            if (op.isMultiselect) {
                var $cb = $('<div class="jfgrid-border-cell jfgrid-border-cb"><img  id="jfgrid_all_cb_' + op.id + '" src="' + imageurl + cb[0] + '" /></div>')
                    .css({ 'left': op.running.leftWidth, 'line-height': (op.running.headHeight - 1) + 'px' });
                $border.append($cb);
                op.running.leftWidth += 30;
            }
            // 是否有展开
            if (op.isSubGrid) {
                var $sub = $('<div class="jfgrid-border-cell jfgrid-border-sub"></div>').css('left', op.running.leftWidth);
                $border.append($sub);
                op.running.leftWidth += 30;
            }
            op.running.leftWidth += op.running.frozenleft;
            $self.css({ 'padding-top': op.running.headHeight });
            $self.find('#jfgrid_head_' + op.id).css({ 'padding-top': op.running.headHeight });
            $border.css({ 'width': op.running.leftWidth, 'height': op.running.headHeight });
            $headcol.css({ 'width': op.running.headWidth, 'height': op.running.headHeight, 'left': op.running.leftWidth });

            $self.find('#jfgrid_body_' + op.id +'>.lr-scroll-box').css({ 'padding-left': op.running.leftWidth });
            $self.find('#jfgrid_left_' + op.id).css({ 'width': op.running.leftWidth });

            // 设置最后一列的宽度
            setLastColWidth($self, op, false);

            if (op.running.isStatistic) {// 添加统计条单元格
                $self.find('#jfgrid_statistic_' + op.id).css({ 'padding-left': op.running.leftWidth });
                $self.find('#jfgrid_statistic_' + op.id + '>.jfgrid-statistic-left').css({ 'width': op.running.leftWidth });
            }

            var _left = 0;

            var $sLast = null;// 用来填写合计条的 
            var _sLastFlag = true;
            $.each(op.running.headData, function (_index, _item) {
                if (_item.last) {
                    _item.height = _item.height + (op.running.MaxDeep - _item.deep) * 28;
                }
                _item.data.height = _item.height;
                _item.$cell = $('<div class="jfgrid-head-cell" path="' + _index + '"  ><span>' + (_item.data.label || "") + '</span></div>')
                    .css({ 'top': _item.top, 'left': _item.left, 'width': (_item._width || _item.width), 'height': _item.height, 'line-height': (_item.height - 1)+'px', 'text-align': (_item.data.align || 'left') });


                // 翻译
                learun.language.get((_item.data.label || ""), function (text) {
                    _item.data.label = text;
                    _item.$cell.find('span').text(text);
                });
                if (_item.last) {
                    _item.$cell.append('<div class="jfgrid-heed-sort"><i class="fa fa-caret-up"></i><i class="fa fa-caret-down"></i></div>');
                    _item.$cell.append('<div class="jfgrid-heed-move"></div>');

                    // 如果是编辑单元格需要初始化
                    if (_item.data.edit) {
                        initEditCell($self, op, _item);
                    }

                    if (!_item.frozen) {
                        _left += (_item._width || _item.width);
                    }

                    if (op.running.isStatistic) {// 添加统计条单元格
                        if (_item.data.statistics && _sLastFlag && $sLast !=null ) {
                            $sLast.attr('isText', '1');
                            $sLast.css({ 'text-align': 'right' });
                            $sLast.text('合计：');
                            _sLastFlag = false;
                        }
                        $sLast = $('<div class="jfGird-statistic-cell"  name="' + _item.data.name + '" ></div>').css({ 'width': (_item._width || _item.width), 'text-align': (_item.data.align || 'left'), 'left': _item.left });

                        if (_item.frozen) {
                            $sLast.css({ 'left': (_item.left + op.running.leftWidth - op.running.frozenleft) });
                            $self.find('#jfgrid_statistic_' + op.id + '>.jfgrid-statistic-left').append($sLast);
                        }
                        else {
                            $self.find('#jfgrid_statistic_' + op.id + '>.jfgrid-statistic-right').append($sLast);
                        }

                    }

                }

                if (_item.frozen) {
                    _item.$cell.css({ 'left': (_item.left + op.running.leftWidth - op.running.frozenleft) });
                    $border.append(_item.$cell);
                }
                else {
                    $headcol.append(_item.$cell);
                }

            });

            $self.find('#jfgrid_right_' + op.id).parent().css({ 'width': op.running.leftWidth + _left - 1 });
            $self.find('#jfgrid_right_' + op.id).css({ 'width': _left - 1 });

            if (op.running.isStatistic) {// 添加统计条单元格\
                $self.find('#jfgrid_statistic_' + op.id + '>.jfgrid-statistic-right').css({ 'width': _left - 1 });
            }
        },

        // 渲染行数据
        dataRender: function ($self, op, data, bindex) {
            bindex = bindex || 0;
            op.running.rowdata = [];
            op.running.statisticData = {};
            $self.find('#jfgrid_left_' + op.id).find('.jfgrid-data-cell').remove();
            $self.find('#jfgrid_right_' + op.id).find('.jfgrid-data-cell').remove();
            hideEditCell();
            var isAllCheck = 3;
            if (op.isTree) {
                var treeList = _jfgrid.listTotree(data, op);
                if (treeList.length > 0) {
                    _treeRender(treeList, 1);
                }
                else {

                }
                function _treeRender(_data, deep) {
                    $.each(_data, function (_index, _item) {
                        _jfgrid.rowRender($self, op, _item, bindex, deep);
                        if (_item.childRows.length > 0) {
                            _treeRender(_item.childRows, deep + 1);
                        }
                    });
                };
            }
            else {
                $.each(data, function (_index, _item) {
                    _jfgrid.rowRender($self, op, _item, bindex);
                    if (op.multiselectfield && _item[op.multiselectfield] == 1) {
                        if (isAllCheck == 0) {
                            isAllCheck = 2;
                        }
                        else if (isAllCheck == 3) {
                            isAllCheck = 1;
                        }
                    }
                    else if (op.multiselectfield && _item[op.multiselectfield] == 0) {
                        if (isAllCheck == 1) {
                            isAllCheck = 2;
                        }
                        else if (isAllCheck == 3) {
                            isAllCheck = 0;
                        }
                    }
                });

                if (op.isPage) { // 如果是分页，刷新一下分页显示条
                    var $pagebar = $('#jfgrid_page_bar_' + op.id);
                    var $pagebarBtn = $pagebar.find('#jfgrid_page_bar_num_' + op.id);
                    var $pagebarBtns = $pagebar.find('#jfgrid_page_bar_nums_' + op.id);

                    var pagebarLabel = '';
                    var btnlist = "";
                    if (op.rowdatas.length == 0) {
                        pagebarLabel = '无显示数据';
                    }
                    else {
                        var pageparam = op.running.pageparam;
                        var beginnum = (pageparam.page - 1) * pageparam.rows + 1;
                        var endnum = beginnum + op.rowdatas.length - 1;
                        pagebarLabel = '显示第 ' + beginnum + ' - ' + endnum + ' 条记录  <span>|</span> 检索到 ' + pageparam.records + ' 条记录';

                        if (pageparam.total > 1) {
                            var bpage = pageparam.page - 6;
                            bpage = bpage < 0 ? 0 : bpage;
                            var epage = bpage + 10;
                            if (epage > pageparam.total) {
                                epage = pageparam.total;
                            }
                            if ((epage - bpage) < 10) {
                                bpage = epage - 10;
                            }
                            bpage = bpage < 0 ? 0 : bpage;

                            for (var i = bpage; i < epage; i++) {
                                btnlist += '<li><a href="javascript:void(0);" class=" pagebtn ' + ((i + 1) == pageparam.page ? 'active' : '') + '" >' + (i + 1) + '</a></li>';
                            }

                            $pagebarBtns.find('span').text('共' + pageparam.total + '页,到');

                            $pagebarBtns.show();
                        }
                        else {
                            $pagebarBtns.hide();
                        }
                    }
                    $pagebarBtn.html(btnlist);
                    $pagebar.find('.jfgrid-page-bar-info').html(pagebarLabel);

                }
            }
            if (op.isMultiselect) {
                if (isAllCheck == 2 || isAllCheck == 1) {
                    $self.find('#jfgrid_all_cb_' + op.id).attr('src', imageurl + cb[isAllCheck]);
                }
                else {
                    $self.find('#jfgrid_all_cb_' + op.id).attr('src', imageurl + cb[0]);
                }
            }

            if (op.running.rowdata.length > 0) {
                $self.find('#jfgrid_nodata_img_' + op.id).hide();
            }
            else {
                $self.find('#jfgrid_nodata_img_' + op.id).show();
            }
        },
        rowRender: function ($self, op, _item, bindex, deep) {


            var lastCell = null;

            var rowdata = op.running.rowdata;
            bindex = bindex || 0;

            var $left = $self.find('#jfgrid_left_' + op.id);
            var $right = $self.find('#jfgrid_right_' + op.id);

            var _index = rowdata.length;
            var _top = _index * op.rowHeight;
            var _left = 0;
            var row = {};

            var $expendcell = null;
            if (op.isTree) {
                row['jfgridRowData'] = _item.data;
                row.childRows = _item.childRows;
                row.jfdeep = deep;
                _item = _item.data;
                $expendcell = $('<div class="jfgrid-data-cell-expend" style="width:' + (deep * 16) + 'px;" ></div>');
                if (row.childRows.length > 0) {
                    $expendcell.append('<i class="fa fa-caret-down jfgrid-data-cell-expendi"></i>');
                }
                op.rowdatas[_index] = _item;

            } else {
                row['jfgridRowData'] = _item;
            }

            // 序号
            row['jfnum'] = {
                top: _top,
                left: _left,
                value: _index + 1,
                text: _index + bindex * (op.isPage ? op.running.pageparam.rows : 1 )+ 1
            };
            if (op.isShowNum) {
                row['jfnum'].$cell = $('<div class="jfgrid-data-cell" rowindex="' + _index + '" rownum="' + op.id + '_' + _index + '" colname="jfgrid_num"  >' + (_index + bindex * (op.isPage ? op.running.pageparam.rows : 1)+ 1) + '</div>');
                row['jfnum'].$cell.css({ 'top': row['jfnum'].top, 'left': row['jfnum'].left, 'text-align': 'center', 'height': op.rowHeight, 'line-height': (op.rowHeight - 1) + 'px' });
                $left.append(row['jfnum'].$cell);
                _left += 30;
            }
            // 是否多选
            if (op.isMultiselect) {
                var _colname = op.multiselectfield || 'jfcheck';
                row['jfcheck'] = {
                    top: _top,
                    left: _left,
                    value: _item[_colname] || 0,
                    $cell: $('<div class="jfgrid-data-cell" rowindex="' + _index + '" rownum="' + op.id + '_' + _index + '" colname="jfgrid_check"  ><img src="' + imageurl + cb[0] + '" /></div>')
                };
                if (row['jfcheck'].value + '' == '1') {
                    row['jfcheck'].$cell.find('img').attr('src', imageurl + cb[1]);
                }

                row['jfcheck'].$cell.css({ 'top': row['jfcheck'].top, 'left': row['jfcheck'].left, 'text-align': 'center', 'height': op.rowHeight, 'line-height': (op.rowHeight - 1) + 'px' });
                $left.append(row['jfcheck'].$cell);
                _left += 30;
            }
            // 是否有子表选项
            if (op.isSubGrid) {
                row['jfsubGrid'] = {
                    top: _top,
                    left: _left,
                    value: false,
                    $cell: $('<div class="jfgrid-data-cell"  rowindex="' + _index + '"  rownum="' + op.id + '_' + _index + '" colname="jfgrid_subGrid"  ><i class="fa fa-plus" ></i></div>')
                };
                row['jfsubGrid'].$cell.css({ 'top': row['jfsubGrid'].top, 'left': row['jfsubGrid'].left, 'text-align': 'center', 'height': op.rowHeight, 'line-height': (op.rowHeight - 1) + 'px' });
                $left.append(row['jfsubGrid'].$cell);
                _left += 30;
            }

            // 加载固定列数据
            $.each(op.running.frozenCols, function (_id, col) {
                //colData[col.name] = colData[col.name] || {};
                var value = _item[col.data.name];
                var _width = (col.data._width || col.data.width);
                row[col.data.name] = {
                    top: _top,
                    left: _left,
                    value: value,
                    $cell: $('<div class="jfgrid-data-cell" rowindex="' + _index + '" rownum="' + op.id + '_' + _index + '" colname="' + col.data.name + '"  ></div>')
                };
                formatterCell(value, col.data, _item, row[col.data.name], op);
                row[col.data.name].$cell.css({ 'top': row[col.data.name].top, 'left': row[col.data.name].left, 'text-align': col.data.align, 'width': _width, 'height': op.rowHeight, 'line-height': (op.rowHeight - 1) + 'px' });
                $left.append(row[col.data.name].$cell);
                _left += _width;

                if ($expendcell != null && _id == 0) {
                    row[col.data.name].$cell.prepend($expendcell);
                }

                lastCell = row[col.data.name];
            });
            _left = 0;
            // 加载滚动区域列数据
            $.each(op.running.cols, function (_id, col) {
                var value = _item[col.data.name];
                var _width = (col._width || col.width);
                row[col.data.name] = {
                    top: _top,
                    left: _left,
                    value: value,
                    $cell: $('<div class="jfgrid-data-cell" rowindex="' + _index + '" rownum="' + op.id + '_' + _index + '" colname="' + col.data.name + '"  ></div>')
                };
                formatterCell(value, col.data, _item, row[col.data.name], op);
                row[col.data.name].$cell.css({ 'top': row[col.data.name].top, 'left': row[col.data.name].left, 'text-align': col.data.align, 'width': _width, 'height': op.rowHeight, 'line-height': (op.rowHeight - 1) + 'px' });
                $right.append(row[col.data.name].$cell);
                _left += _width;

                if ($expendcell != null && _id == 0 && op.running.frozenCols.length == 0 ) {
                    row[col.data.name].$cell.prepend($expendcell);
                }

                lastCell = row[col.data.name];
            });

            rowdata.push(row);

            $right.css({ 'height': _top + op.rowHeight - 1 });

            if (op.isAutoHeight) {
                var _ptop = $self.css('padding-top');
                var _pbottom = $self.css('padding-bottom');
                $self.css({ 'height': _top + op.rowHeight + parseInt(_ptop) + parseInt(_pbottom)+1 });
            }

            if (op.running.rowSelected && lastCell) {
                if (op.running.rowSelected.jfgridRowData[op.mainId]) {
                    op.running.rowSelected.jfgridRowData[op.mainId] == row.jfgridRowData[op.mainId] && (lastCell.$cell.trigger('click'));
                }
                else {
                    op.running.rowSelected['jfnum'].value == row['jfnum'].value && (lastCell.$cell.trigger('click'));
                }
            }
        },
        updateRow: function (row, op) {
            var map = {};
            $.each(op.running.headData, function (i, col) {
                map[col.data.name] = col.data;
            });
            $.each(row, function (id, _item) {
                if (id != 'jfnum' && id != 'jfcheck' && id != 'jfsubGrid' && id != 'jfgridRowData') {
                    if (_item.value != row['jfgridRowData'][id]) {
                        _item.value = row['jfgridRowData'][id] || '';
                        formatterCell(_item.value, map[id], row['jfgridRowData'], _item, op);
                    }
                }
            });
        },
        // 后台数据加载 
        reload: function ($self, op) {
            op.rowdatas = [];
            if (op.isPage) {
                learun.loading(true, '正在获取数据');
                op.running.pageparam = op.running.pageparam || {
                    rows: 100,                // 每页行数      
                    page: 1,                  // 当前页
                    sidx: '',                 // 排序列
                    sord: '',                 // 排序类型
                    records: 0,               // 总记录数
                    total: 0                  // 总页数
                };

                op.running.pageparam.rows = op.rows;
                op.running.pageparam.sidx = op.sidx;
                op.running.pageparam.sord = op.sord;
                op.running.pageparam.page = op.running.pageparam.page || 1;
                op.running.pageparam.records = 0;
                op.running.pageparam.total = 0;
                op.param['pagination'] = JSON.stringify(op.running.pageparam);

                learun.httpAsync('GET', op.url, op.param, function (data) {
                    learun.loading(false);
                    if (data) {
                        op.rowdatas = data.rows;
                        op.running.pageparam.page = data.page;
                        op.running.pageparam.records = data.records;
                        op.running.pageparam.total = data.total;

                    }
                    else {
                        op.rowdatas = [];
                        op.running.pageparam.page = 1;
                        op.running.pageparam.records = 0;
                        op.running.pageparam.total = 0;
                    }
                    _jfgrid.dataRender($self, op, op.rowdatas, (op.running.pageparam.page - 1));
                    op.onRenderComplete && op.onRenderComplete(op.rowdatas);
                });
            } else {
                learun.loading(true, '正在获取数据');
                learun.httpAsync('GET', op.url, op.param, function (data) {
                    learun.loading(false);
                    op.rowdatas = data || [];
                    _jfgrid.dataRender($self, op, op.rowdatas, 0);
                    op.onRenderComplete && op.onRenderComplete(op.rowdatas);
                });
            }
        },
        // 数据转换
        listTotree: function (data, op) {
            // 只适合小数据计算
            var resdata = [];
            var mapdata = {};
            var mIds = {};
            var pIds = {};
            data = data || [];
            for (var i = 0, l = data.length; i < l; i++) {
                var item = data[i];
                mIds[item[op.mainId]] = 1;
                mapdata[item[op.parentId]] = mapdata[item[op.parentId]] || [];
                mapdata[item[op.parentId]].push(item);
                if (mIds[item[op.parentId]] == 1) {
                    delete pIds[item[op.parentId]];
                }
                else {
                    pIds[item[op.parentId]] = 1;
                }
                if (pIds[item[op.mainId]] == 1) {
                    delete pIds[item[op.mainId]];
                }
            }
            for (var id in pIds) {
                _fn(resdata, id);
            }
            function _fn(_data, vparentId) {
                var pdata = mapdata[vparentId] || [];
                for (var j = 0, l = pdata.length; j < l; j++) {
                    var _row = {
                        data: pdata[j],
                        childRows:[]
                    }
                    _fn(_row.childRows, pdata[j][op.mainId]);
                    _data.push(_row);
                }
            }
            return resdata;
        },
        // 树形展开或关闭
        expandTree: function ($et, op) {
            var _h = 0;
            var flag = false;
            // 获取当前数据所在行
            var rownum = $et.parents('.jfgrid-data-cell').attr('rownum');
            var rowIndex = parseInt(rownum.replace(op.id + '_', ''));
            var rowItem = op.running.rowdata[rowIndex];

            if ($et.hasClass('fa-caret-down')) {// 关闭s
                rowItem.lrClosedRows = {};
                for (var i = rowIndex + 1, l = op.running.rowdata.length; i < l; i++) {
                    var _row = op.running.rowdata[i];
                    if (_row.jfdeep > rowItem.jfdeep && !flag) {
                        if (!$('[rownum="' + op.id + '_' + i + '"]').is(':hidden')) {
                            _h += op.rowHeight;
                            $('[rownum="' + op.id + '_' + i + '"]').hide();


                            rowItem.lrClosedRows[i] = '1';
                        }
                    }
                    else {
                        flag = true;
                        var top = parseInt($('[rownum="' + op.id + '_' + i + '"]').eq(0).css('top').replace('px', '')) - _h;
                        $('[rownum="' + op.id + '_' + i + '"]').css({ top: top });
                    }
                }

                $et.removeClass('fa-caret-down');
                $et.addClass('fa-caret-right');
            }
            else {// 展开
                for (var i = rowIndex + 1, l = op.running.rowdata.length; i < l; i++) {
                    var _row = op.running.rowdata[i];
                    if (_row.jfdeep > rowItem.jfdeep && !flag) {
                        if (rowItem.lrClosedRows[i]) {
                            _h += op.rowHeight;
                            $('[rownum="' + op.id + '_' + i + '"]').show();
                        }
                    }
                    else {
                        flag = true;
                        var top = parseInt($('[rownum="' + op.id + '_' + i + '"]').eq(0).css('top').replace('px', '')) + _h;
                        $('[rownum="' + op.id + '_' + i + '"]').css({ top: top });
                    }
                }
                $et.removeClass('fa-caret-right');
                $et.addClass('fa-caret-down');
            }
            $('#jfgrid_left_' + op.id + ' .jfgrid-selected,#jfgrid_right_' + op.id + ' .jfgrid-selected').removeClass('jfgrid-selected');
            op.running.rowSelected = null;
        },
        // 排序
        sortCol: function ($self,$et, op) {
            var path = $et.attr('path');
            var head = op.running.headData[path];
            if (head.last && !head.data.sort && op.isPage) {
                var isup = true;

                if (op.running.sorthead) {
                    op.running.sorthead.$cell.find('.jfgrid-heed-sort').hide();
                    if (head == op.running.sorthead) {

                        var $i = head.$cell.find('.jfgrid-heed-sort .active');
                        $i.removeClass('active');
                        if ($i.hasClass('fa-caret-up')) {
                            head.$cell.find('.jfgrid-heed-sort .fa-caret-down').addClass('active');
                            isup = false;
                        }
                        else {
                            head.$cell.find('.jfgrid-heed-sort .fa-caret-up').addClass('active');
                            isup = true;
                        }
                        head.$cell.find('.jfgrid-heed-sort').show();
                    }
                    else {
                        head.$cell.find('.jfgrid-heed-sort .active').removeClass('active');
                        head.$cell.find('.jfgrid-heed-sort .fa-caret-up').addClass('active');
                        head.$cell.find('.jfgrid-heed-sort').show();
                        isup = true;
                    }
                }
                else {
                    head.$cell.find('.jfgrid-heed-sort .active').removeClass('active');
                    head.$cell.find('.jfgrid-heed-sort .fa-caret-up').addClass('active');
                    head.$cell.find('.jfgrid-heed-sort').show();

                    isup = true;
                }
                op.running.sorthead = head;
                if (op.isPage) {
                    // 调用后台接口
                    op.sidx = head.data.name;
                    op.sord = isup ? 'ASC' : 'DESC';

                    _jfgrid.reload($self, op);

                }
                else {
                    // js排序

                }
            }
            console.log(head);
        },
        // 单击行
        clickRow: function ($self, $et, op) {
            var $cell = $et;
            var rownum = $et.attr('rownum');
            if (!rownum) {
                $cell = $et.parents('.jfgrid-data-cell');
                rownum = $cell.attr('rownum');
            }
            var rowIndex = parseInt(rownum.replace(op.id + '_', ''));
            // 获取当前行数据
            var rowItem = op.running.rowdata[rowIndex];
            var rowData = rowItem['jfgridRowData'];

            // 判断当前单元格是否是可编辑
            var colname = $cell.attr('colname');
            var isEdit = false;
            $.each(op.running.headData, function (i, col) {
                if (col.data.name == colname) {
                    if (col.data.edit && col.data.edit.type != 'layer') {
                        isEdit = true;
                        var _top = $cell.css('top');
                        var _left = $cell.css('left');
                        var _width = $cell.css('width');
                        col.$edit.css({ 'top': _top, 'left': _left, 'width': _width, 'height': op.rowHeight }).show();

                        var $edit = col.$edit.find('#jfgrid_edit_' + op.id + '_' + col.data.name);
                        $edit.unbind('change');
                        
                        col.data.edit.init && col.data.edit.init(rowData, $edit);

                        switch (col.data.edit.type){
                            case 'input':          // 输入框 文本,数字,密码
                                $edit.unbind('input propertychange');
                                $edit.val(rowData[colname] || '');
                                $edit.select();
                                $edit.on("input propertychange", function (e) {
                                    var value = $(this).val();
                                    rowData[colname] = value;
                                    col.data.edit.change && col.data.edit.change(rowData, rowIndex);
                                    formatterCell(value, col.data, rowData, rowItem[col.data.name], op);
                                   
                                });
                                break;
                            case 'select':         // 下拉框选择
                                $edit.lrselectSet(rowData[colname] || '');
                                $edit.on('change', function () {
                                    var $this = $(this);
                                    var value = $this.lrselectGet();
                                    var item = $this.lrselectGetEx();

                                    rowData[colname] = value;
                                    col.data.edit.change && col.data.edit.change(rowData, rowIndex, item);
                                    formatterCell(value, col.data, rowData, rowItem[col.data.name], op);
                                });

                                break;
                            case 'radio': // 单选
                                $edit.find('input').unbind('click');
                                $edit.find('input[value="' + (rowData[colname] || '') + '"]').trigger('click');
                                $edit.find('input').on('click', function () {
                                    var value = $(this).val();
                                    if (rowData[colname] != value) {
                                        rowData[colname] = value;
                                        col.data.edit.change && col.data.edit.change(rowData, rowIndex);
                                        formatterCell(value, col.data, rowData, rowItem[col.data.name], op);
                                    }
                                });
                                if ((rowData[colname] == '' || rowData[colname] == null || rowData[colname] == undefined) && col.data.edit.dfvalue) {
                                    $edit.find('input[value="' + col.data.edit.dfvalue + '"]').trigger('click');
                                }
                                break;
                            case 'checkbox':       // 多选
                                $edit.find('input').unbind('click');
                                var _oldv = rowData[colname] || '';
                                var _cbmap = {};
                                var _cbValue = rowData[colname] || col.data.edit.dfvalue || '';
                                rowData[colname] = _cbValue;
                                var _cbValueList = _cbValue.split(',');
                                $.each(_cbValueList, function (_index, _item) {
                                    _cbmap[_item] = '1';
                                });

                                $edit.find('input').each(function () {
                                    var $this = $(this);
                                    var _v = $this.val();
                                    if ($this.is(':checked') && !_cbmap[_v]) {
                                        $this.trigger('click');
                                    }
                                    else if (!$this.is(':checked') && _cbmap[_v]) {
                                        $this.trigger('click');
                                    }
                                    $this = null;
                                });
                                if (_oldv != rowData[colname]) {
                                    col.data.edit.change && col.data.edit.change(rowData, rowIndex);
                                    formatterCell(rowData[colname], col.data, rowData, rowItem[col.data.name], op);
                                }
                               
                                $edit.find('input').on('click', function () {
                                    var $this = $(this);
                                    var value = $this.val();
                                    var _v = rowData[colname].split(',');


                                    var _vmap = {};
                                    var v = [];
                                    $.each(_v, function (_index, _item) {
                                        if (_item !== '' && _item !== undefined && _item !== null) {
                                            _vmap[_item] = _index;
                                            v.push(_item);
                                        }
                                    });

                                    if ($this.is(':checked')) {
                                        if (_vmap[value] == undefined) {
                                            v.push(value);
                                        }
                                    }
                                    else {
                                        if (_vmap[value] != undefined) {
                                            v.splice(_vmap[value], 1);
                                        }
                                    }
                                    rowData[colname] = String(v);
                                    col.data.edit.change && col.data.edit.change(rowData, rowIndex);
                                    formatterCell(rowData[colname], col.data, rowData, rowItem[col.data.name], op);
                                });
                                break;
                            case 'datatime':       // 时间
                                $edit.unbind('change');
                                $edit.val(rowData[colname] || '');
                                $edit.select();
                                $edit.on("change", function (e) {
                                    var value = $(this).val();
                                    rowData[colname] = value;
                                    col.data.edit.change && col.data.edit.change(rowData, rowIndex);
                                    formatterCell(value, col.data, rowData, rowItem[col.data.name], op);
                                });
                                break;
                            case 'layer':          // 弹层
                                break;
                        }
                        

                    }
                    return false;
                }
            });

            if (!isEdit) {
                if (op.isMultiselect) {
                    var _cbValue = rowItem['jfcheck'].value;
                    if ((_cbValue + '') == '1') {
                        rowItem['jfcheck'].$cell.find('img').attr('src', imageurl + cb[0]);
                        rowItem['jfcheck'].value = 0;
                        op.multiselectfield && (rowData[op.multiselectfield] = 0);
                        op.onSelectRow && op.onSelectRow(rowData, false);

                        if (!op.running.checkAlling) {
                            var _flag = false;
                            $.each(op.running.rowdata, function (_index, _item) {
                                if (_item['jfcheck'].value + '' == '1') {
                                    _flag = true;
                                    return false;
                                }
                            });
                            if (_flag) {
                                $self.find('#jfgrid_all_cb_' + op.id).attr('src', imageurl + cb[2]);
                            }
                            else {
                                $self.find('#jfgrid_all_cb_' + op.id).attr('src', imageurl + cb[0]);
                            }
                        }
                    }
                    else {
                        rowItem['jfcheck'].$cell.find('img').attr('src', imageurl + cb[1]);
                        rowItem['jfcheck'].value = 1;
                        op.multiselectfield && (rowData[op.multiselectfield] = 1);
                        op.onSelectRow && op.onSelectRow(rowData, true);

                        if (!op.running.checkAlling) {
                            var _flag = false;
                            $.each(op.running.rowdata, function (_index, _item) {
                                if (_item['jfcheck'].value + '' == '0') {
                                    _flag = true;
                                    return false;
                                }
                            });

                            if (_flag) {
                                $self.find('#jfgrid_all_cb_' + op.id).attr('src', imageurl + cb[2]);
                            }
                            else {
                                $self.find('#jfgrid_all_cb_' + op.id).attr('src', imageurl + cb[1]);
                            }
                        }
                    }
                }
                else {
                    $self.find('#jfgrid_left_' + op.id + ' .jfgrid-selected,#jfgrid_right_' + op.id + ' .jfgrid-selected').removeClass('jfgrid-selected');
                    $self.find('[rownum="' + rownum + '"]').addClass('jfgrid-selected');
                    op.onSelectRow && op.onSelectRow(rowData);
                    op.running.rowSelected = rowItem;
                }
            }
            else {
                $self.find('#jfgrid_left_' + op.id + ' .jfgrid-selected,#jfgrid_right_' + op.id + ' .jfgrid-selected').removeClass('jfgrid-selected');
                op.running.rowSelected = null;
            }
        },
        // 选中所有行
        checkAllRows: function ($self, $et, op) {
            // 判断当前是否是选中还是不选中
            var imgsrc = $et.attr('src').replace(imageurl, '');
            var ischecked = false;
            if (imgsrc == cb[1]) {
                ischecked = true;
                $et.attr('src', imageurl + cb[0]);
            }
            else {
                $et.attr('src', imageurl + cb[1]);
            }

            op.running.checkAlling = true;

            $.each(op.running.rowdata, function (_index, _item) {
                if (_item['jfcheck'].value + '' == '0' && !ischecked) {
                    _item['jfcheck'].$cell.trigger('click');
                }
                else if (_item['jfcheck'].value + '' == '1' &&ischecked) {
                    _item['jfcheck'].$cell.trigger('click');
                }
            });
            op.running.checkAlling = false;
        },
        // 展开子表区域
        expandSub: function ($self, $et, op) {
            var $cell = $et;
            var rownum = $et.attr('rownum');
            if (!rownum) {
                $cell = $et.parents('.jfgrid-data-cell');
                rownum = $cell.attr('rownum');
            }
            var rowIndex = parseInt(rownum.replace(op.id + '_', ''));
            // 获取当前行数据
            var rowItem = op.running.rowdata[rowIndex];
            var rowData = rowItem['jfgridRowData'];

            // 
            var $body = $self.find('#jfgrid_body_' + op.id);

            var $left = $self.find('#jfgrid_left_' + op.id);
            var $right = $self.find('#jfgrid_right_' + op.id);
            var subid = op.id + '_sub_' + rowIndex;
            var height = $left.height();

            if (rowItem['jfsubGrid'].value) {
                rowItem['jfsubGrid'].value = false;
                var $sub = $('#' + subid);
                $sub.remove();


                rowItem['jfsubGrid'].$cell.html('<i class="fa fa-plus" ></i>');
                // 调整单元格位置
                for (var i = rowIndex + 1, l = op.running.rowdata.length; i < l; i++) {
                    var item = op.running.rowdata[i];
                    $.each(item, function (_index, _item) {
                        if (_item.top != undefined) {
                            _item.top = _item.top  - op.subGridHeight;
                            _item.$cell.css({ 'top': _item.top});
                        }
                    });
                }
                $left.css({ 'height': height - op.subGridHeight });
                $right.css({ 'height': height -  op.subGridHeight });
            }
            else {
                rowItem['jfsubGrid'].value = true;
                var width = $body.width();
                rowItem['jfsubGrid'].$cell.html('<i class="fa fa-minus" ></i>');
                var subContentId = learun.newGuid();
                var $sub = $('<div class="jfgrid-sub" id="' + subid + '" ><div id="' + subContentId +'" ></div></div>').css({ 'left': 0, 'top': rowItem['jfsubGrid'].top + op.rowHeight, 'height': op.subGridHeight, 'width': width });
                // 调整单元格位置
                for (var i = rowIndex + 1, l = op.running.rowdata.length; i < l; i++) {
                    var item = op.running.rowdata[i];
                    $.each(item, function (_index, _item) {
                        if (_item.top != undefined) {
                            _item.top += op.subGridHeight;
                            _item.$cell.css({ 'top': _item.top });
                        }
                    });
                }
                $left.append($sub);

                $left.css({ 'height': height + op.subGridHeight });
                $right.css({ 'height': height + op.subGridHeight });
                
                op.subGridExpanded && op.subGridExpanded(subContentId, rowItem['jfgridRowData']);
            }
        },
        // 获取选中行
        getCheckRow: function ($self, op) {
            var res = [];
            $.each(op.running.rowdata, function (_index, _item) {
                if (_item['jfcheck'].value == 1) {
                    res.push(_item['jfgridRowData']);
                }
            });
            return res;
        },
        // 上移
        moveUp: function ($self, op, index) {
            if (index > 0) {
                var flag = true;
                var _item = op.rowdatas[index];
                if (op.isTree) {
                    flag = false;
                    if (op.running.rowdata[index].jfdeep <= op.running.rowdata[index - 1].jfdeep) {
                        for (var i = index - 1; i >= 0; i--) {
                            if (op.running.rowdata[index].jfdeep == op.running.rowdata[i].jfdeep) {
                                flag = true;
                                op.rowdatas[index] = op.rowdatas[i];
                                op.rowdatas[i] = _item;
                                break;
                            }
                        }
                    }
                }
                else {
                    op.rowdatas[index] = op.rowdatas[index - 1];
                    op.rowdatas[index - 1] = _item;
                }
                _jfgrid.dataRender($self, op, op.rowdatas);
                return true;
            }
            return false;
        },
        // 下移
        moveDown: function ($self, op, index) {
            if (index < op.rowdatas.length - 1) {
                var flag = true;
                var _item = op.rowdatas[index];
                if (op.isTree) {
                    flag = false;
                    for (var i = index + 1, l = op.rowdatas.length; i < l; i++) {
                        if (op.running.rowdata[index].jfdeep > op.running.rowdata[i].jfdeep) {
                            break;
                        }
                        if (op.running.rowdata[index].jfdeep == op.running.rowdata[i].jfdeep) {
                            flag = true;
                            op.rowdatas[index] = op.rowdatas[i];
                            op.rowdatas[i] = _item;
                            break;
                        }
                    }
                }
                else {
                    op.rowdatas[index] = op.rowdatas[index + 1];
                    op.rowdatas[index + 1] = _item;
                }

                _jfgrid.dataRender($self, op, op.rowdatas);
                return true;
            }
            return false;
        },
        turnPage: function (e) {
            var $this = $(this);
            var $self = $('#' + $this.attr('id').replace('jfgrid_page_bar_num_', ''));
            var op = $self[0].dfop;

            var et = e.target || e.srcElement;
            var $et = $(et);
            if ($et.hasClass('pagebtn')) {
                var page = parseInt($et.text());
                if (page != op.running.pageparam.page) {
                    $this.find('.active').removeClass('active');
                    $et.addClass('active');
                    op.running.pageparam.page = page;
                    _jfgrid.reload($self, op);
                }
            }

        },
        turnPage2: function (e) {
            var $this = $(this);
            var op = e.data.op;
            var name = $this.text();
            var $pagenum = $('#jfgrid_page_bar_num_' + op.id);
            var page = parseInt($pagenum.find('.active').text());
            var falg = false;
            switch (name) {
                case '首页':
                    if (page != 1) {
                        op.running.pageparam.page = 1;
                        falg = true;
                    }
                    break;
                case '上一页':
                    if (page > 1) {
                        op.running.pageparam.page = page - 1;
                        falg = true;
                    }
                    break;
                case '下一页':
                    if (page < op.running.pageparam.total) {
                        op.running.pageparam.page = page + 1;
                        falg = true;
                    }
                    break;
                case '尾页':
                    if (page != op.running.pageparam.total) {
                        op.running.pageparam.page = op.running.pageparam.total;
                        falg = true;
                    }
                    break;
                case '跳转':
                    var text = $this.parents('#jfgrid_page_bar_nums_' + op.id).find('input').val();
                    if (!!text) {
                        var p = parseInt(text);
                        if (String(p) != 'NaN') {
                            if (p < 1) {
                                p = 1;
                            }
                            if (p > op.running.pageparam.total) {
                                p = op.running.pageparam.total;
                            }
                            op.running.pageparam.page = p;
                            falg = true;
                        }
                    }
                    break;
            }
            if (falg) {
                _jfgrid.reload($('#' + op.id), op);
            }

        },

        // 打印处理
        print: function ($self) {
            var dfop = $self[0].dfop;
            var $table = $('<table border="1" style=""></table>');

            // 获取表头数据
            var thlist = [];
            function cTh(_data, _index) {
                var res = _data.length;
                $.each(_data, function (id, item) {
                    thlist[_index] = thlist[_index] || $('<tr></tr>');
                    var $th = $('<th>' + item.label + '</th>');
                    if (!!item.children && item.children.length > 0) {
                        var num = cTh(item.children, _index + 1);
                        res = res + num - 1;
                        $th.attr('colspan', num);
                    }
                    var _row = item.height / 28;
                    $th.attr('rowspan', _row);
                    $th.css('text-align', item.align);
                    thlist[_index].append($th);
                });
                return res;
            }

            cTh(dfop.headData, 0);
            $.each(thlist, function (id, item) {
                $table.append(item);
            });

            $.each(dfop.rowdatas, function (index, item) {
                var $tr = $('<tr></tr>');
                $self.find('[rownum="' + dfop.id + '_' + index + '"]').each(function () {
                    var $cell = $(this);
                    var colname = $cell.attr('colname');
                    if (!!colname && colname != 'jfgrid_num' && colname != 'jfgrid_multiselect' && colname != 'jfgrid_subGrid') {
                        var $td = $('<td>' + $cell.html() + '</td>');
                        $td.css('text-align', $cell.css('text-align'));
                        $tr.append($td);
                    }
                });

                $table.append($tr);
            });

            dfop = null;

            return $table;
        }
    };

    $.fn.jfGrid = function (op) {
        var $self = $(this);
        if (!$self[0] || $self[0].dfop) {
            return $self;
        }

        var id = $self.attr('id');
        if (id == null || id == undefined || id == '') {
            id = "jfgrid" + new Date().getTime();
            $self.attr('id', id);
        }

        var dfop = {
            url: '',                      // 数据服务地址
            param: {},                    // 请求参数
            rowdatas: [],                 // 列表数据

            headData: [],                 // 列数据

            isShowNum: true,              // 是否显示序号
            isMultiselect: false,         // 是否允许多选
            multiselectfield: '',         // 多选绑定字段

            isSubGrid: false,             // 是否有子表
            subGridExpanded: false,       // 子表展开后调用函数
            subGridHeight: 300,

            onSelectRow: false,           // 选中一行后回调函数
            dblclick: false,
            onRenderComplete: false,      // 表格加载完后调用

            onAddRow: false,              // 添加一行数据后执行
            onMinusRow: false,            // 删除一行数据后执行
            beforeMinusRow: false,        // 删除一行数据前执行
            
            isPage: false,                // 是否分页默认是不分页（目前只支持服务端分页）
            rows: 100,
            sidx: '',
            sord: 'ASC',

            isTree: false,                // 是否树形显示（没有分页的情况下才支持） (只有在数据不多情况下才建议使用)
            mainId: 'id',                 // 关联的主键
            parentId: 'parentId',         // 树形关联字段

           
            isEdit: false,                // 是否允许增删行

            isAutoHeight: false,          // 自动适应表格高度
            height: 0,
            rowHeight: 28                 // 行高


        };
        if (!!op) {
            $.extend(dfop, op);
        }

        dfop.id = id;
        $self[0].dfop = dfop;
        dfop.running = {};


        if (dfop.isMultiselect) {
            for (var i = 0; i < 3; i++) {
                var im = new Image();
                im.src = imageurl + cb[i];
            }
        }

        _jfgrid.init($self, dfop);

        dfop = null;
        return $self;
    };

    $.fn.jfGridSet = function (name, data) {
        var $self = $(this);
        var dfop = $self[0].dfop;
        if (!dfop) {
            return null;
        }
        switch (name) {
            case 'reload': // 重新加载一边数据
                data = data || dfop.param || {};
                dfop.param = data.param || data ;
                _jfgrid.reload($self, dfop);
                break;
            case 'refresh':
                break;
            case 'refreshdata':
                if (data) {
                    dfop.rowdatas = data.rowdatas || data;
                }
               
                _jfgrid.dataRender($self, dfop, dfop.rowdatas);
                break;
            case 'addRow':
                dfop.rowdatas.push(data || {});
                _jfgrid.rowRender($self, dfop, data || {}, 0);
                $self.find('#jfgrid_nodata_img_' + dfop.id).hide();
                break;
            case 'removeRow':
                $.each(dfop.rowdatas, function (_index, _item) {
                    if (dfop.mainId && data) {
                        if (_item[dfop.mainId] == data) {
                            dfop.rowdatas.splice(_index, 1);
                            return false;
                        }
                    }
                    else {
                        if (_item == dfop.running.rowSelected.jfgridRowData) {
                            dfop.rowdatas.splice(_index, 1);
                            return false;
                        }
                    }

                    
                });
                var _$self = $self;
                setTimeout(function () {
                    _jfgrid.dataRender(_$self, _$self[0].dfop, _$self[0].dfop.rowdatas, 0);
                    _$self = null;
                }, 100);
                break;
            case 'nocheck':
                if (dfop.isMultiselect) {
                    $.each(dfop.running.rowdata, function (_index, _item) {
                        if (_item['jfgridRowData'][dfop.mainId] == data) {
                            if (_item['jfcheck'].value == 1) {
                                _item['jfcheck'].$cell.trigger('click');
                            }
                            return false;
                        }
                    });
                }
                break;
            case 'updateRow':
                if (dfop.running.rowSelected) {
                    _jfgrid.updateRow(dfop.running.rowSelected, dfop);
                }
                else if (dfop.running.rowdata[data]){
                    _jfgrid.updateRow(dfop.running.rowdata[data], dfop);
                }
                break;
            case 'moveUp':
                return _jfgrid.moveUp($self, dfop, data);
                break;
            case 'moveDown':
                return _jfgrid.moveDown($self, dfop, data);
                break;
        }
        dfop = null;
        $self = null;
    };

    $.fn.jfGridGet = function (name) {
        var $self = $(this);
        var op = $self[0].dfop;
        if (!op) {
            return null;
        }
        switch (name) {
            case 'rowdata':
                if (op.isMultiselect) {
                    return _jfgrid.getCheckRow($self, op);
                }
                else {
                    if (op.running.rowSelected) {
                        return op.running.rowSelected['jfgridRowData'];
                    }
                    else {
                        return {};
                    }
                }
                break;
            case 'rowdatas':
                return op.rowdatas;
                break;
            case 'rowdatasByArray':
                break;
            case 'settingInfo':
                return op;
                break;
            case 'headData':
                break;
            case 'showData':
                var res = [];
                $.each(op.running.rowdata, function (_index, _item) {
                    var point = {};
                    $.each(_item, function (_jindex, _jitem) {
                        if (_jindex != 'jfgridRowData' && _jindex != 'jfnum' && _jindex != 'jfcheck' && _jindex != 'jfsubGrid') {
                            point[_jindex] = _jitem.text;
                        } 
                    });
                    res.push(point);
                });
                return res;
                break;
        }
        op = null;
    };

    $.fn.jfGridValue = function (name) {
        var $self = $(this);
        var op = $self[0].dfop;
        if (!op) {
            return null;
        }
        if (op.isMultiselect) {
            var res = [];
            var list = _jfgrid.getCheckRow($self, op);
            $.each(list, function (_index, _item) {
                res.push(_item[name]);
            }); 

            return String(res);
        }
        else {
            if (op.running.rowSelected) {
                return op.running.rowSelected['jfgridRowData'][name];
            }
            else {
                return '';
            }
        }
    };

    // 打印处理
    $.fn.jfGridPrint = function () {
        var $self = $(this);
        return _jfgrid.print($self);
    }
})(window.jQuery, top.learun);