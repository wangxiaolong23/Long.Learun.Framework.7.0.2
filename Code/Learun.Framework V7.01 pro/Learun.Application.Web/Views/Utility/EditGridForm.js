/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.04.28
 * 描 述：表格字段编辑	
 */
var keyValue = request('keyValue');
var gridSetting = top[keyValue];

var bootstrap = function ($, learun) {
    "use strict";

    var currentRow = null;
    var setFlag = false;
    function initData(data) {
        var colDatas = [];
        var map = {};
        $.each(gridSetting.fields, function (_index, _item) {
            map[_item.field] = _item;
        });
        $.each(data, function (_index, _item) {
            var point = {
                isHide: 0,
                field: _item.f_column,
                name: _item.f_remark,
                width: 80,
                align: 'left',
                type: 'input'
            };
            if (map[_item.field]) {
                point = map[_item.field];
            }
            colDatas.push(point);
        });
        gridSetting.fields = colDatas;

    }

    function setRowOp() {
        console.log(currentRow);

        setFlag = true;
        var res = [];
        var parentId = currentRow.parentId;
        $('.field').show();
        $.each(gridSetting.fields, function (_index, _item) {
            if (_item.field === '' && _item.name !== '' && _item.id !== currentRow.id) {
                res.push({ 'id': _item.id, 'text': _item.name });
            }
            if (currentRow.id === _item.parentId) {
                $('.field').hide();
            }
        });
        $('#parentId').lrselectRefresh({
            data: res
        });

        //$('#colop').lrSetFormData(currentRow);
        $('#name').val(currentRow.name);
        $('#parentId').lrselectSet(parentId);
        $('#field').lrselectSet(currentRow.field);
        $('#align').lrselectSet(currentRow.align);
        $('#width').val(currentRow.width);
        $('#type').lrselectSet(currentRow.type);

        switch (currentRow.type) {
            case 'label':
            case 'input':
            case 'guid':
                break;
            case 'radio':
            case 'checkbox':
            case 'select':
                $('#dataSource').lrselectSet(currentRow.dataSource);
                $('#dfvalue').lrselectSet(currentRow.dfvalue);
                if (currentRow.dataSource === '0') {
                    $('#dataItemId').lrselectSet(currentRow.itemCode);
                }
                else {
                    $('#dataSourceId').lrselectSet(currentRow.dataSourceId);
                    $('#showField').lrselectSet(currentRow.showField);
                    $('#saveField').lrselectSet(currentRow.saveField);
                }
                break;
            case 'layer':
                $('#layerW').val(currentRow.layerW || '');
                $('#layerH').val(currentRow.layerH || '');
                break;
            case 'datetime':
                $('#datetime').lrselectSet(currentRow.datetime);
                break;
        }

        setFlag = false;
    }

    // 设置弹层显示数据
    function setLayerGridData(data) {
        currentRow.layerData = data || [
            { label: '项目名', name: 'F_ItemName', value: '', width: '100', align: 'left', hide: 0 },
            { label: '项目值', name: 'F_ItemValue', value: '', width: '100', align: 'left', hide: 0 }
        ];
        $('#layerGrid').jfGridSet('refreshdata', currentRow.layerData);
    }


    var page = {
        init: function () {
            page.bind();
        },
        bind: function () {
            $('#colopWrap').lrscroll();
            // 显示名称
            $('#name').on("input propertychange", function (e) {
                var value = $(this).val();
                currentRow['name'] = value;
                $('#edit_grid').jfGridSet('updateRow');
            });

            // 字段绑定
            $('#field').lrselect({
                value: 'f_column',
                text: 'f_column',
                title: 'f_remark',
                allowSearch: true,
                select: function (item) {
                    if (item) {
                        currentRow.field = item.f_column;
                        if (currentRow.name === '') {
                            currentRow.name = item.f_remark;
                            $('#name').val(currentRow.name);
                        }
                        $('.lastcol').show();
                    }
                    else {
                        if (currentRow) {
                            currentRow.field = '';
                        }
                        $('.lastcol').hide();
                    }

                    $('#edit_grid').jfGridSet('updateRow');
                }
            });

            // 对齐方式
            $('#align').lrselect({
                placeholder: false,
                data: [
                    { 'id': 'left', 'text': '靠左' },
                    { 'id': 'center', 'text': '居中' },
                    { 'id': 'right', 'text': '靠右' }
                ],
                select: function (item) {
                    currentRow.align = item.id;
                    $('#edit_grid').jfGridSet('updateRow');
                }
            });

            // 字段宽度
            $('#width').on("input propertychange", function (e) {
                var value = $(this).val();
                currentRow['width'] = value;
            });

            // 字段类型
            $('#type').lrselect({
                placeholder: false,
                data: [
                    { 'id': 'label', 'text': '文本' },
                    { 'id': 'input', 'text': '输入框' },
                    { 'id': 'select', 'text': '下拉框' },
                    { 'id': 'radio', 'text': '单选框' },
                    { 'id': 'checkbox', 'text': '多选框' },
                    { 'id': 'layer', 'text': '弹层选择框' },
                    { 'id': 'datetime', 'text': '日期' },
                    { 'id': 'guid', 'text': 'GUID' }
                ],
                select: function (item) {
                    currentRow.type = item.id;
                    $('#layerop').hide();
                    $('#layerop').parent().removeClass('layerop');
                    $('.layer').hide();

                    switch (item.id) {
                        case 'label':
                        case 'input':
                        case 'guid':
                            $('.datasource').hide();
                            $('.datasource1').hide();
                            $('.datetime').hide();
                            break;
                        case 'radio':
                        case 'checkbox':
                        case 'select':
                            $('.datetime').hide();
                            $('.datasource').show();
                            $('#dataSource').lrselectSet(currentRow.dataSource);
                            if (currentRow.dataSource === '0') {
                                $('#dataItemId').lrselectSet(currentRow.itemCode || currentRow.dataItemCode);
                            }
                            else {
                                $('#dataSourceId').lrselectSet(currentRow.dataSourceId);
                                $('#showField').lrselectSet(currentRow.showField);
                                $('#saveField').lrselectSet(currentRow.saveField);
                            }
                            break;
                        case 'layer':
                            $('.datetime').hide();
                            $('.datasource1').hide();
                            $('.datasource').show();

                            $('.dfvalue').hide();
                            $('#dataSource').lrselectSet(currentRow.dataSource);
                            if (currentRow.dataSource === '0') {
                                $('#dataItemId').lrselectSet(currentRow.itemCode || currentRow.dataItemCode);
                            }
                            else {
                                $('#dataSourceId').lrselectSet(currentRow.dataSourceId);
                            }
                            $('.layer').show();
                            $('#layerop').show();
                            $('#layerop').parent().addClass('layerop');


                            break;
                        case 'datetime':
                            $('.datetime').show();
                            $('.datasource').hide();
                            $('.datasource1').hide();
                            break;
                    }
                    $('#edit_grid').jfGridSet('updateRow');
                }
            });

            // 上级列头
            $('#parentId').lrselect({
                select: function (item) {
                    if (!item) {
                        currentRow.parentId = '';
                    }
                    else {
                        if (currentRow.parentId !== item.id) {
                            currentRow.parentId = item.id;
                        }
                    }

                    if (!setFlag) {
                        $('#edit_grid').jfGridSet('refreshdata', gridSetting.fields);
                    }
                }
            });

            // 数据来源
            $('#dfvalue').lrselect({
                allowSearch: true,
                select: function (item) {
                    if (item) {
                        if (currentRow.dataSource === '0') {
                            currentRow.dfvalue = item.id;
                        }
                        else {
                            currentRow.dfvalue = item[currentRow.saveField];
                        }
                    }
                    else {
                        currentRow.dfvalue = '';
                    }
                }
            });

            // 数据字典选项
            $('#dataItemId').lrselect({
                allowSearch: true,
                url: top.$.rootUrl + '/LR_SystemModule/DataItem/GetClassifyTree',
                type: 'tree',
                value: 'value',
                select: function (item) {
                    if (item) {
                        if (currentRow.dataSourceId !== item.id) {
                            currentRow.dfvalue = '';
                            currentRow.itemCode = item.value;
                        }

                        if (currentRow.type === 'radio' || currentRow.type === 'checkbox' || currentRow.type === 'select') {

                            learun.clientdata.getAllAsync('dataItem', {
                                code: item.value,
                                callback: function (dataes) {
                                    var list = [];
                                    $.each(dataes, function (_index, _item) {
                                        if (_item.parentId === "0") {
                                            list.push({ id: _item.value, text: _item.text, title: _item.text, k: _index });
                                        }
                                    });
                                    var _value = currentRow.dfvalue;
                                    $('#dfvalue').lrselectRefresh({
                                        value: "id",
                                        text: "text",
                                        title: "title",
                                        data: list,
                                        url: ''
                                    });
                                    if (_value !== '' && _value !== undefined && _value !== null) {
                                        $('#dfvalue').lrselectSet(_value);
                                    }

                                }
                            });
                        }
                    }
                    else {
                        currentRow.itemCode = '';
                        currentRow.dataItemCode && (currentRow.dataItemCode = '');
                        if (currentRow.type === 'radio' || currentRow.type === 'checkbox' || currentRow.type === 'select') {
                            currentRow.dfvalue = '';
                            $('#dfvalue').lrselectRefresh({ url: '', data: [] });
                        }
                    }
                }
            });

            // 数据源
            $('#showField').lrselect({
                title: 'text',
                text: 'text',
                value: 'value',
                allowSearch: true,
                select: function (item) {
                    if (item) {
                        currentRow.showField = item.value;
                        if (currentRow.saveField !== '') {
                            learun.clientdata.getAllAsync('sourceData', {
                                code: currentRow.dataSourceId,
                                callback: function (dataes) {
                                    var _value = currentRow.dfvalue;
                                    $('#dfvalue').lrselectRefresh({
                                        value: currentRow.saveField,
                                        text: currentRow.showField,
                                        title: currentRow.showField,
                                        url: '',
                                        data: dataes
                                    });

                                    if (_value !== '' && _value !== undefined && _value !== null) {
                                        $('#dfvalue').lrselectSet(_value);
                                    }
                                }
                            });
                        }
                    }
                }
            });
            $('#saveField').lrselect({
                title: 'text',
                text: 'text',
                value: 'value',
                allowSearch: true,
                select: function (item) {
                    if (item) {
                        currentRow.saveField = item.value;
                        if (currentRow.showField !== '') {
                            learun.clientdata.getAllAsync('sourceData', {
                                code: currentRow.dataSourceId,
                                callback: function (dataes) {
                                    var _value = currentRow.dfvalue;
                                    $('#dfvalue').lrselectRefresh({
                                        value: currentRow.saveField,
                                        text: currentRow.showField,
                                        title: currentRow.showField,
                                        url: '',
                                        data: dataes
                                    });

                                    if (_value !== '' && _value !== undefined && _value !== null) {
                                        $('#dfvalue').lrselectSet(_value);
                                    }
                                }
                            });
                        }
                    }
                }
            });
            $('#dataSourceId').lrselect({
                allowSearch: true,
                url: top.$.rootUrl + '/LR_SystemModule/DataSource/GetList',
                value: 'F_Code',
                text: 'F_Name',
                title: 'F_Name',
                select: function (item) {
                    var flag = true;
                    if (item) {
                        if (currentRow.dataSourceId !== item.F_Code) {
                            currentRow.showField = '';
                            currentRow.saveField = '';
                            currentRow.dfvalue = '';
                            flag = false;
                        }

                        currentRow.dataSourceId = item.F_Code;
                        if (currentRow.type === 'radio' || currentRow.type === 'checkbox' || currentRow.type === 'select') {
                            var _value = currentRow.dfvalue;
                            $('#dfvalue').lrselectRefresh({ url: '', data: [] });
                            currentRow.dfvalue = _value;
                            //获取字段数据
                            learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/DataSource/GetDataColName', { code: item.F_Code }, function (data) {
                                var fieldData = [];
                                for (var i = 0, l = data.length; i < l; i++) {
                                    var id = data[i];
                                    var selectpoint = { value: id, text: id };
                                    fieldData.push(selectpoint);
                                }
                                $('#showField').lrselectRefresh({
                                    data: fieldData,
                                    placeholder: false
                                });
                                $('#showField').lrselectSet(currentRow.showField || fieldData[0].value);
                                $('#saveField').lrselectRefresh({
                                    data: fieldData,
                                    placeholder: false
                                });
                                $('#saveField').lrselectSet(currentRow.saveField || fieldData[0].value);
                            });
                        }
                        else if (currentRow.type === 'layer') {
                            if (!flag || currentRow.layerData.length === 0) {
                                learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/DataSource/GetDataColName', { code: item.F_Code }, function (data) {
                                    var fieldData = [];
                                    $.each(data, function (_index, _item) {
                                        var selectpoint = { label: '', name: _item, value: '', width: '100', align: 'left', hide: 0 };
                                        fieldData.push(selectpoint);
                                    });
                                    setLayerGridData(fieldData);
                                });
                            }
                            else {
                                setLayerGridData(currentRow.layerData);
                            }
                        }
                    }
                    else {
                        currentRow.dataSourceId = '';
                        if (currentRow.type === 'radio' || currentRow.type === 'checkbox' || currentRow.type === 'select') {
                            currentRow.showField = '';
                            currentRow.saveField = '';
                            currentRow.dfvalue = '';

                            $('#showField').lrselectRefresh({
                                data: [],
                                placeholder: '请选择'
                            });
                            $('#saveField').lrselectRefresh({
                                data: [],
                                placeholder: '请选择'
                            });
                            $('#dfvalue').lrselectRefresh({ url: '', data: [] });
                        } else if (currentRow.type === 'layer') {
                            setLayerGridData([]);
                        }
                    }
                }
            });
            $('#dataSource').lrselect({
                data: [{ id: '0', text: '数据字典' }, { id: '1', text: '数据源' }],
                value: 'id',
                text: 'text',
                placeholder: false,
                select: function (item) {
                    if (currentRow) {
                        if (currentRow.dataSource === '0' && item.id === '1') {
                            currentRow.layerData = [];
                        }
                        else if (currentRow.dataSource === '1' && item.id === '0') {
                            currentRow.layerData = [
                                { label: '项目名', name: 'F_ItemName', value: '', width: '100', align: 'left', hide: 0 },
                                { label: '项目值', name: 'F_ItemValue', value: '', width: '100', align: 'left', hide: 0 }
                            ];
                        }

                        currentRow.dataSource = item.id;
                    }
                    if (item.id === '0') {
                        $('#dataSourceId').hide();
                        $('#dataItemId').show();
                        $('.datasource1').hide();

                        if (currentRow.type === 'layer') {
                            setLayerGridData(currentRow.layerData);
                        }
                    }
                    else {
                        $('#dataItemId').hide();
                        $('#dataSourceId').show();

                        if (currentRow.type === 'radio' || currentRow.type === 'checkbox' || currentRow.type === 'select') {
                            $('.datasource1').show();
                        }
                        else {
                            $('.datasource1').hide();
                        }

                        if (currentRow.type === 'layer') {
                            setLayerGridData(currentRow.layerData || []);
                        }
                    }
                }
            });

            // 弹层数据设置
            $('#layerGrid').jfGrid({
                headData: [
                    {
                        label: "字段名", name: "label", width: 150, align: "left",
                        edit: {
                            type: 'input'
                        }
                    },
                    { label: "字段ID", name: "name", width: 150, align: "left" },
                    {
                        label: "绑定字段", name: "value", width: 150, align: "left",
                        edit: {
                            type: 'select',
                            init: function (row, $self) {// 选中单元格后执行
                                $self.lrselectRefresh({
                                    data: gridSetting.fieldMap[gridSetting.dbId + gridSetting.tableName]
                                });
                            },
                            op: {
                                value: 'f_column',
                                text: 'f_column',
                                title: 'f_remark',
                                allowSearch: true
                            }
                        }
                    },
                    {
                        label: "宽度", name: "width", width: 70, align: "center",
                        edit: {
                            type: 'input'
                        }
                    },
                    {
                        label: "对齐", name: "align", width: 70, align: "center",
                        edit: {
                            type: 'select',
                            op: {
                                placeholder: false,
                                data: [
                                    { 'id': 'left', 'text': '靠左' },
                                    { 'id': 'center', 'text': '居中' },
                                    { 'id': 'right', 'text': '靠右' }
                                ]
                            }
                        }
                    },
                    {
                        label: "隐藏", name: "hide", width: 50, align: "center",
                        formatter: function (value, row, op, $cell) {
                            $cell.on('click', function () {
                                var v = $cell.find('span').text();
                                if (v === '是') {
                                    row.hide = 0;
                                    $cell.html('<span class=\"label label-success \" style=\"cursor: pointer;\">否</span>');
                                }
                                else {
                                    row.hide = 1;
                                    $cell.html('<span class=\"label label-default \" style=\"cursor: pointer;\">是</span>');
                                }

                                return false;
                            });


                            if (value === 1) {
                                return '<span class=\"label label-default \" style=\"cursor: pointer;\">是</span>';
                            } else if (value === 0) {
                                return '<span class=\"label label-success \" style=\"cursor: pointer;\">否</span>';
                            }
                        }
                    },// 1 隐藏 0 显示
                    {
                        label: "", name: "btn1", width: 50, align: "center",
                        formatter: function (value, row, op, $cell) {
                            $cell.on('click', function () {
                                var rowindex = parseInt($cell.attr('rowindex'));
                                var res = $('#layerGrid').jfGridSet('moveUp', rowindex);
                                return false;
                            });
                            return '<span class=\"label label-info\" style=\"cursor: pointer;\">上移</span>';
                        }
                    },
                    {
                        label: "", name: "btn2", width: 50, align: "center",
                        formatter: function (value, row, op, $cell) {
                            $cell.on('click', function () {
                                var rowindex = parseInt($cell.attr('rowindex'));
                                var res = $('#layerGrid').jfGridSet('moveDown', rowindex);
                                return false;
                            });
                            return '<span class=\"label label-warning\" style=\"cursor: pointer;\">下移</span>';
                        }
                    }

                ],
                isShowNum: false
            });
            // 弹层宽
            $('#layerW').on("input propertychange", function (e) {
                var value = $(this).val();
                currentRow['layerW'] = value;
            });
            // 弹层高
            $('#layerH').on("input propertychange", function (e) {
                var value = $(this).val();
                currentRow['layerH'] = value;
            });

            // 日期格式
            $('#datetime').lrselect({
                placeholder: false,
                data: [
                    { 'id': 'date', 'text': '仅日期' },
                    { 'id': 'datetime', 'text': '日期和时间' }
                ],
                select: function (item) {
                    if (currentRow) {
                        if (item) {
                            currentRow.datetime = item.id;
                        }
                        else {
                            currentRow.datetime = 'date';
                        }
                    }
                }
            }).lrselectSet('date');


            $('#edit_grid').jfGrid({
                headData: [
                    {
                        label: "显示名称", name: "name", width: 200, align: "left"
                    },
                    {
                        label: "绑定字段", name: "field", width: 200, align: "left"
                    },
                    {
                        label: "类型", name: "type", width: 100, align: "center",
                        formatter: function (value, row, op, $cell) {
                            switch (value) {
                                case 'label':
                                    return '文本';
                                case 'input':          // 输入框 文本,数字,密码
                                    return '输入框';
                                case 'select':         // 下拉框选择
                                    return '下拉框';
                                case 'radio':          // 单选
                                    return '单选框';
                                case 'checkbox':       // 多选
                                    return '多选框';
                                case 'datetime':       // 时间
                                    return '日期';
                                case 'layer':          // 弹层
                                    return '弹层选择框';
                                case 'guid':
                                    return 'GUID';
                            }
                        }
                    },
                    {
                        label: "", name: "btn1", width: 50, align: "center",
                        formatter: function (value, row, op, $cell) {
                            $cell.on('click', function () {
                                var rowindex = parseInt($cell.attr('rowindex'));
                                var res = $('#edit_grid').jfGridSet('moveUp', rowindex);
                                return false;
                            });
                            return '<span class=\"label label-info\" style=\"cursor: pointer;\">上移</span>';
                        }
                    },
                    {
                        label: "", name: "btn2", width: 50, align: "center",
                        formatter: function (value, row, op, $cell) {
                            $cell.on('click', function () {
                                var rowindex = parseInt($cell.attr('rowindex'));
                                var res = $('#edit_grid').jfGridSet('moveDown', rowindex);
                                return false;
                            });
                            return '<span class=\"label label-warning\" style=\"cursor: pointer;\">下移</span>';
                        }
                    },
                    {
                        label: "", name: "", width: 5, align: "center"
                    }
                ],
                isEdit: true,
                isShowNum: false,
                mainId: 'id',
                isTree: true,
                onSelectRow: function (rowdata) {
                    $('#colopWrap').show();
                    $('#colopWrap').parent().addClass('editopen');
                    currentRow = rowdata;
                    setRowOp();
                },
                onAddRow: function (row, rows) {
                    row.id = learun.newGuid();
                    row.name = '';
                    row.type = 'label';
                    row.width = 100;
                    row.align = 'left';
                    row.parentId = '';
                    row.field = '';
                    row.dataSource = '0';
                },
                onMinusRow: function (row, rows) {

                }
            });
            $('#edit_grid').jfGridSet('refreshdata', gridSetting.fields);
            if (!gridSetting.fieldMap[gridSetting.dbId + gridSetting.tableName]) {
                learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/DatabaseTable/GetFieldList', { databaseLinkId: gridSetting.dbId, tableName: gridSetting.tableName }, function (data) {
                    gridSetting.fieldMap[gridSetting.dbId + gridSetting.tableName] = data;
                    $('#field').lrselectRefresh({
                        data: data
                    });
                });
            }
            else {
                $('#field').lrselectRefresh({
                    data: gridSetting.fieldMap[gridSetting.dbId + gridSetting.tableName]
                });
            }
        }
    };
    page.init();
};