/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.05
 * 描 述：单表开发模板	
 */

// 数据表数据
var dbAllTable = [];
var databaseLinkId = '';

var dbTable = [];
var mapField = {};
var mainTable = null;
var queryAllComponts = [];
var queryAllCompontMap = {};

var bootstrap = function ($, learun) {
    "use strict";
    
    var rootDirectory = $('#rootDirectory').val();
    var postData = {};

    // 设置所选数据表的字段
    var setTableFieldTree = function () {
        var flag = true;
        $.each(dbTable, function (_index, _item) {
            if (!mapField[databaseLinkId + _item.name]) {
                flag = false;
                return false;
            }
        });
        if (flag) {
            tableFieldTree.length = 0;
            $.each(dbTable, function (_index, _item) {
                var tableNode = {
                    id: _item.name,
                    text: _item.name,
                    value: _item.name,
                    hasChildren: true,
                    isexpand: true,
                    complete: true,
                    ChildNodes: []
                };
                for (var j = 0, jl = mapField[databaseLinkId + _item.name].length; j < jl; j++) {
                    var fieldItem = mapField[databaseLinkId + _item.name][j];
                    var point = {
                        id: tableNode.text + fieldItem.f_column,
                        text: fieldItem.f_column,
                        value: fieldItem.f_column,
                        title: fieldItem.f_remark,
                        hasChildren: false,
                        isexpand: false,
                        complete: true,
                        showcheck: true
                    };
                    tableNode.ChildNodes.push(point);
                }
                tableFieldTree.push(tableNode);
            });

        }
        else {
            setTimeout(function () {
                setTableFieldTree();
            }, 100);
        }


    }

    // 页面方法
    var page = {
        init: function () {
            page.bind();
        },
        /*绑定事件和初始化控件*/
        bind: function () {
            // 刷新
            $('#lr_refresh').on('click', function () {
                location.reload();
            });
            // 加载导向
            $('#wizard').wizard().on('change', function (e, data) {
                var $finish = $("#btn_finish");
                var $next = $("#btn_next");
                if (data.direction == "next") {
                    if (data.step == 1) {
                        dbTable = $('#dbtableGrid2').jfGridGet('rowdatas');
                        if (dbTable.length == 0) {
                            learun.alert.error('请选择数据表！');
                            return false;
                        }
                        mainTable = null;
                        var flag = true;
                        $.each(dbTable, function (_index, _item) {
                            if (_item.relationName == '') {
                                if (mainTable != null) {
                                    flag = false;
                                    learun.alert.error('只能设置一个主表！');
                                    return false;
                                }
                                mainTable = _item;
                            }
                            else {
                                if (_item.field == '') {
                                    flag = false;
                                    learun.alert.error('表【' + _item.name + '】请设置关联字段！');
                                    return false;
                                }
                                if (_item.relationField == '') {
                                    flag = false;
                                    learun.alert.error('表【' + _item.relationField + '】请设置关联表对应字段！');
                                    return false;
                                }
                            }
                        });

                        if (!flag) {
                            return false;
                        }

                        if (mainTable == null) {
                            learun.alert.error('请设置一个主表！');
                            return false;
                        }

                        $('#step-2').lrCustmerFormDesigner('updatedb', { dbId: databaseLinkId, dbTable: dbTable });
                        if (mapField[databaseLinkId + mainTable.name]) {
                            $('#queryDatetime').lrselectRefresh({ data: mapField[databaseLinkId + mainTable.name] });
                        }
                        else {
                            if (!mapField[databaseLinkId + mainTable.name]) {
                                learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/DatabaseTable/GetFieldList', { databaseLinkId: databaseLinkId, tableName: mainTable.name }, function (data) {
                                    mapField[databaseLinkId + mainTable.name] = data;
                                    $('#queryDatetime').lrselectRefresh({ data: data });

                                });
                            }
                        }

                    }
                    else if (data.step == 2) {
                        if (!$('#step-2').lrCustmerFormDesigner('valid')) {
                            return false;
                        }
                        var scheme = $('#step-2').lrCustmerFormDesigner('get');
                        queryAllComponts = [];
                        for (var i = 0, l = scheme.data.length; i < l; i++) {
                            var componts = scheme.data[i].componts;
                            for (var j = 0, jl = componts.length; j < jl; j++) {
                                var item = componts[j];
                                if (item.type != "girdtable" && item.table && item.field ) {
                                    queryAllComponts.push(item);
                                    queryAllCompontMap[item.table + item.field] = item;
                                }
                            }
                        }
                        if (queryAllComponts.length == 0) {
                            learun.alert.error('请设置表单字段！');
                            return false;
                        }

                        $('#treefieldRe').lrselectRefresh({ data: queryAllComponts });

                        // 初始化列表设置字段
                        var coldata = [];
                        var oldcoldata = $('#col_gridtable').jfGridGet('rowdatas');
                        if (oldcoldata.length > 0) {
                            $.each(oldcoldata, function (_index, _item) {
                                if (queryAllCompontMap[_item.id]) {
                                        coldata.push(_item)
                                }
                            });
                            $('#col_gridtable').jfGridSet('refreshdata', coldata);
                        }
                        if (coldata.length == 0) {
                            $.each(queryAllComponts, function (_index, _item) {
                                var point = { 'id': _item.table + _item.field, 'field': _item.field, 'align': 'left', 'width': 100 };
                                coldata.push(point);
                            });
                            $('#col_gridtable').jfGridSet('refreshdata', coldata);
                        }
                    }
                    else if (data.step == 3) {
                        
                    }
                    else if (data.step == 4) {
                        var isTree = $('[name="isViewTree"]:checked').val();
                        if (isTree == '1') {
                            var treeSource = $('#treeDataSource').lrselectGet();

                            if (treeSource == '1') {// 数据源
                                var treeSourceId = $('#treeDataSourceId').lrselectGet();
                                if (treeSourceId == '') {
                                    learun.alert.error('请选择数据源！');
                                    return false;
                                }
                            }
                            else {// sql语句
                                var treeSql = $('#treesql').val();
                                if (treeSql == '') {
                                    learun.alert.error('请填写sql语句！');
                                    return false;
                                }
                            }


                            var treefieldId = $('#treefieldId').lrselectGet();
                            if (treefieldId == '') {
                                learun.alert.error('请选择字段ID！');
                                return false;
                            }

                            var treeParentId = $('#treefieldParentId').lrselectGet();
                            if (treeParentId == '') {
                                learun.alert.error('请选择父级字段！');
                                return false;
                            }
                            var treefieldShow = $('#treefieldShow').lrselectGet();
                            if (treefieldShow == '') {
                                learun.alert.error('请选择显示字段！');
                                return false;
                            }
                            var treefieldRe = $('#treefieldRe').lrselectGet();
                            if (treefieldRe == '') {
                                learun.alert.error('请选择关联字段！');
                                return false;
                            }
                        }
                    }
                    else if (data.step == 5) {
                        if (!$('#step-5').lrValidform()) {
                            return false;
                        }
                        postData = {};
                        // 数据库连接ID
                        postData.databaseLinkId = databaseLinkId;

                        // 选择的数据表数据
                        postData.dbTable = JSON.stringify(dbTable);

                        // 表单设置数据
                        var scheme = $('#step-2').lrCustmerFormDesigner('get');
                        postData.formData = JSON.stringify(scheme.data);

                        // 条件配置数据
                        var _query = $('#query_girdtable').jfGridGet('rowdatas');
                        var _queryList = [];
                        $.each(_query, function (_index, _item) {
                            if (_item.id) {
                                _queryList.push(_item);
                            }
                        });

                        var _querySetting = {
                            width: $('#queryWidth').val(),
                            height: $('#queryHeight').val(),
                            isDate: $('[name="queryDatetime"]:checked').val(),
                            DateField: $('#queryDatetime').lrselectGet(),
                            fields: _queryList
                        };
                        postData.queryData = JSON.stringify(_querySetting);

                        // 获取列表数据
                        var colbtns = [];
                        $('#btnlist .lbtn.active').each(function () {
                            var v = $(this).attr('data-value');
                            colbtns.push(v);
                        });
                        var colbtnexs = [];
                        $('#btnlistex .lbtn.active').each(function () {
                            var v = $(this).text();
                            var id = $(this).attr('data-value');
                            colbtnexs.push({ id: id, name: v });
                        });

                        var _colData = {
                            isPage: $('[name="isPage"]:checked').val(),
                            fields: $('#col_gridtable').jfGridGet('rowdatas'),
                            btns: colbtns,
                            btnexs: colbtnexs,
                            isTree: $('[name="isViewTree"]:checked').val(),
                            treeSource: $('#treeDataSource').lrselectGet(),
                        };
                        if (_colData.isTree == '1') {
                            if (_colData.treeSource == '1') {// 数据源
                                _colData.treeSourceId = $('#treeDataSourceId').lrselectGet();
                               
                            }
                            else {// sql语句
                                _colData.treeSql = $('#treesql').val();
                            }
                            _colData.treefieldId = $('#treefieldId').lrselectGet();
                            _colData.treeParentId = $('#treefieldParentId').lrselectGet();
                            _colData.treefieldShow = $('#treefieldShow').lrselectGet();
                            _colData.treefieldRe = $('#treefieldRe').lrselectGet();
                        }
                        postData.colData = JSON.stringify(_colData);

                        // 基础配置信息
                        var baseInfo = $('#step-5').lrGetFormData();
                        postData.baseInfo = JSON.stringify(baseInfo);

                        learun.httpAsyncPost(top.$.rootUrl + '/LR_CodeGeneratorModule/TemplatePC/LookCustmerCode', postData, function (res) {
                            if (res.code == 200) {
                                $.each(res.data, function (id, item) {
                                    $('#' + id).html('<textarea name="SyntaxHighlighter" class="brush: c-sharp;"></textarea>');
                                    $('#' + id + ' [name="SyntaxHighlighter"]').text(item);
                                });
                                SyntaxHighlighter.highlight();
                            }
                        });
                    }
                    else if (data.step == 6) {
                        $finish.removeAttr('disabled');
                        $next.attr('disabled', 'disabled');
                    }
                    else {
                        $finish.attr('disabled', 'disabled');
                    }
                } else {
                    $finish.attr('disabled', 'disabled');
                    $next.removeAttr('disabled');
                }
            });

            // 数据表选择
            $('#dbTree').lrtree({
                url: top.$.rootUrl + '/LR_SystemModule/DatabaseLink/GetTreeList',
                nodeClick: function (node) {
                    if (node.hasChildren) {
                        $('#dbtableGrid1').jfGridSet('refreshdata', []);
                        $('#dbtableGrid2').jfGridSet('refreshdata', []);
                        databaseLinkId = '';
                    }
                    else if (databaseLinkId != node.id) {
                        $('#dbtableGrid1').jfGridSet('refreshdata', []);
                        $('#dbtableGrid2').jfGridSet('refreshdata', []);
                        databaseLinkId = node.id;
                        // 获取数据的表数据 
                        learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/DatabaseTable/GetList', { databaseLinkId: databaseLinkId }, function (data) {
                            dbAllTable = data;
                            var tableData = [];
                            $.each(data, function (_index, _item) {
                                tableData.push(_item);
                                _item.check = 0;
                            });
                            $('#dbtableGrid1').jfGridSet('refreshdata', tableData);
                        });
                    }
                }
            });
            $('#dbtableGrid1').jfGrid({
                headData: [
                    { label: "表名", name: "name", width: 200, align: "left" },
                    { label: "说明", name: "tdescription", width: 200, align: "left" }
                ],
                onSelectRow: function (row, isCheck) {
                    if (isCheck) {
                        var _row = { name: row.name, pk: row.pk, field: '', relationName: '', relationField: '' };
                        $('#dbtableGrid2').jfGridSet('addRow', _row);
                        if (!mapField[databaseLinkId + row.name]) {
                            learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/DatabaseTable/GetFieldList', { databaseLinkId: databaseLinkId, tableName: row.name }, function (data) {
                                mapField[databaseLinkId + row.name] = data;
                            });
                        }
                    } else {
                        $('#dbtableGrid2').jfGridSet('removeRow', row.name);
                    }
                },

                isMultiselect: true,
                multiselectfield:'check',
                mainId: 'name',
            });
            $('#dbtableGrid2').jfGrid({
                headData: [
                    {
                        label: "", name: "btn", width: 60, align: "center",
                        formatter: function (value, row, op, $cell) {
                            $cell.on('click', function () {
                                $('#dbtableGrid2').jfGridSet('removeRow', row.name);
                                $('#dbtableGrid1').jfGridSet('nocheck', row.name);
                                $.each(dbAllTable, function (_index, _item) {
                                    if (_item.name == row.name) {
                                        _item.check = 0;
                                        return false;
                                    }
                                });
                                return false;
                            });
                            return '<span class=\"label label-danger \" style=\"cursor: pointer;\">移除</span>';
                        }
                    },
                    {
                        label: "数据表名", name: "name", width: 200, align: "left"
                    },
                    {
                        label: "关联字段", name: "field", width: 200, align: "left",
                        edit: {
                            type: 'select',
                            init: function (row, $self) {// 选中单元格后执行
                                if (mapField[databaseLinkId + row.name]) {
                                    $self.lrselectRefresh({
                                        data: mapField[databaseLinkId + row.name]
                                    });
                                } else {
                                    learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/DatabaseTable/GetFieldList', { databaseLinkId: databaseLinkId, tableName: row.name }, function (data) {
                                        mapField[databaseLinkId + row.name] = data;
                                        $self.lrselectRefresh({
                                            data: data
                                        });
                                    });
                                }
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
                        label: "关联表", name: "relationName", width: 200, align: "left",
                        edit: {
                            type: 'select',
                            init: function (row, $self) {// 选中单元格后执行
                                // 获取当前表的数据
                                var _data = [];
                                var data = $('#dbtableGrid2').jfGridGet('rowdatas');
                                $.each(data, function (_index, _item) {
                                    if (_item.name != row.name) {
                                        _data.push(_item);
                                    }
                                });
                                $self.lrselectRefresh({
                                    data: _data
                                });
                            },
                            op: {
                                value: 'name',
                                text: 'name',
                                title: 'tdescription'
                            }
                        }
                    },
                    {
                        label: "关联表对应字段", name: "relationField", width: 200, align: "left",
                        edit: {
                            type: 'select',
                            init: function (row, $self) {// 选中单元格后执行
                                if (mapField[databaseLinkId + row.relationName]) {
                                    $self.lrselectRefresh({
                                        data: mapField[databaseLinkId + row.relationName]
                                    });
                                } else {
                                    learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/DatabaseTable/GetFieldList', { databaseLinkId: databaseLinkId, tableName: row.relationName }, function (data) {
                                        mapField[databaseLinkId + row.relationName] = data;
                                        $self.lrselectRefresh({
                                            data: data
                                        });
                                    });
                                }
                            },
                            op: {
                                value: 'f_column',
                                text: 'f_column',
                                title: 'f_remark',
                                allowSearch: true
                            }
                        }
                    }
                ],
                isShowNum: false,
                mainId: 'name'
            });
            $('#btn_Search').on('click', function () {
                var keyword = $('#txt_Keyword').val();
                searchTable(keyword);
            });
            $('#txt_Keyword').on("keypress", function (e) {
                if (event.keyCode == "13") {
                    var keyword = $('#txt_Keyword').val();
                    searchTable(keyword);
                }
            });
            function searchTable(keyword) {
                if (keyword != '' && keyword != null && keyword != undefined) {
                    var tableData = [];
                    $.each(dbAllTable, function (_index, _item) {
                        if (_item.name.indexOf(keyword) != -1) {
                            tableData.push(_item);
                        }
                    });
                    $('#dbtableGrid1').jfGridSet('refreshdata', tableData);
                } else {
                    $('#dbtableGrid1').jfGridSet('refreshdata', dbAllTable);
                }
            }
            // 表单设置
            $('#step-2').lrCustmerFormDesigner('init', { components: ['label', 'text', 'textarea', 'texteditor', 'radio', 'checkbox', 'select', 'datetime', 'datetimerange', 'encode', 'organize', 'currentInfo', 'upload','girdtable']});

            // 条件信息设置
            $('#queryDatetime').lrselect({
                value: 'f_column',
                text: 'f_column',
                title: 'f_remark',
                allowSearch: true
            });
            $('#query_girdtable').jfGrid({
                headData: [
                    {
                        label: "", name: "btn1", width: 50, align: "center",
                        formatter: function (value, row, op, $cell) {
                            $cell.on('click', function () {
                                var rowindex = parseInt($cell.attr('rowindex'));
                                var res = $('#query_girdtable').jfGridSet('moveUp', rowindex);
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
                                var res = $('#query_girdtable').jfGridSet('moveDown', rowindex);
                                return false;
                            });
                            return '<span class=\"label label-success\" style=\"cursor: pointer;\">下移</span>';
                        }
                    },
                    {
                        label: "字段项名称", name: "compontId", width: 300, align: "left",
                        formatter: function (value, row, op, $cell) {
                            if (queryAllCompontMap[row.id]) {
                                return queryAllCompontMap[row.id].title;
                            }
                            else {
                                return '';
                            }
                        },
                        edit: {
                            type: 'select',
                            init: function (row, $self) {// 选中单元格后执行
                                $self.lrselectRefresh({
                                    data: queryAllComponts
                                });
                            },
                            op: {
                                value: 'id',
                                text: 'title',
                                title: 'title',
                                allowSearch: true
                            },
                            change: function (rowData, rowIndex, item) {
                                if (item != null) {
                                    rowData.id = item.table + item.field;
                                }
                                else {
                                    rowData.id = '';
                                }
                            }
                        }
                    },
                    {
                        label: "所占行比例", name: "portion", width: 150, align: "left",
                        edit: {
                            type: 'select',
                            op: {
                                placeholder: false,
                                data: [
                                    {
                                        id: '1', text: '1/1'
                                    },
                                    {
                                        id: '2', text: '1/2'
                                    },
                                    {
                                        id: '3', text: '1/3'
                                    },
                                    {
                                        id: '4', text: '1/4'
                                    },
                                    {
                                        id: '6', text: '1/6'
                                    }
                                ]
                            }
                        },
                        formatter: function (value, row, op, $cell){
                            if (!!value) {
                                return '1/' + value;
                            }
                            else {
                                return '';
                            }
                        }
                    }
                ],
                onAddRow: function (row, rows) {
                    row.portion = '1';
                },
                mainId: 'id',
                isEdit: true,
                isMultiselect: true
            });

            // 列表页设置
            $('#treesetting').lrscroll();

            $('#btnlist>div').on('click', function () {
                var $this = $(this);
                if ($this.hasClass('active')) {
                    $this.removeClass('active');
                }
                else {
                    $this.addClass('active');
                }
                $this = null;
            });
            $('#btnlistex').delegate('div','click', function () {
                $(this).remove();
            });
            $('#lr_btnlistex_add').on('click', function () {            // 添加扩展按钮
                learun.layerForm({
                    id: 'AddBtnForm',
                    title: '添加按钮',
                    url: top.$.rootUrl + '/LR_CodeGeneratorModule/TemplatePC/AddBtnForm',
                    height: 230,
                    width: 400,
                    callBack: function (id) {
                        return top[id].acceptClick(function (_formData) {
                            $('#btnlistex').append('<div class="lbtn active" data-value="' + _formData.btnId + '" ><i class="fa fa-plus"></i>&nbsp;' + _formData.btnName + '</div>');
                        });
                    }
                });
            });

            $('#treefieldId').lrselect({
                title: 'text',
                text: 'text',
                value: 'value',
                allowSearch: true,
                select: function (item) {
                    if (item) {
                    }
                }
            });
            $('#treefieldParentId').lrselect({
                title: 'text',
                text: 'text',
                value: 'value',
                allowSearch: true,
                select: function (item) {
                    if (item) {
                    }
                }
            });
            $('#treefieldShow').lrselect({
                title: 'text',
                text: 'text',
                value: 'value',
                allowSearch: true,
                select: function (item) {
                    if (item) {
                    }
                }
            });
            $('#treefieldRe').lrselect({
                title: 'title',
                text: 'title',
                value: 'field',
                allowSearch: true
            });
            $('#treeDataSource').lrselect({
                data: [{ id: '1', text: '数据源' }, { id: '2', text: 'sql语句' }],
                placeholder: false,
                select: function (item) {
                    if (item) {
                        if (item.id == '1') {
                            $('.DataSourceType1').hide();
                            $('.DataSourceType2').show();
                        }
                        else {
                            $('.DataSourceType1').show();
                            $('.DataSourceType2').hide();
                        }
                        $('#treefieldId').lrselectRefresh({ data: [] });
                        $('#treefieldParentId').lrselectRefresh({ data: [] });
                        $('#treefieldShow').lrselectRefresh({ data: [] });
                    }
                }
            });
            $('#treeDataSourceId').lrselect({
                allowSearch: true,
                url: top.$.rootUrl + '/LR_SystemModule/DataSource/GetList',
                value: 'F_Code',
                text: 'F_Name',
                title: 'F_Name',
                select: function (item) {
                    if (!!item) {
                        learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/DataSource/GetDataColName', { code: item.F_Code }, function (data) {
                            var fieldData = [];
                            for (var i = 0, l = data.length; i < l; i++) {
                                var id = data[i];
                                var selectpoint = { value: id, text: id };
                                fieldData.push(selectpoint);
                            }
                            $('#treefieldId').lrselectRefresh({
                                data: fieldData
                            });
                            $('#treefieldParentId').lrselectRefresh({
                                data: fieldData
                            });
                            $('#treefieldShow').lrselectRefresh({
                                data: fieldData
                            });
                        });
                    }
                    else {

                    }

                }
            });

            $('[name="isViewTree"]').on('click', function () {
                var value = $(this).val();
                if (value == 1) {
                    $('.treesetting').show();
                    $('#treeDataSource').lrselectSet('2');

                }
                else {
                    $('.treesetting').hide();
                    $('#treeDataSource').lrselectSet('');
                }
            });

            $('#lr_treesql_set').on('click', function () {
                $('#treefieldId').lrselectRefresh({ data: [] });
                $('#treefieldParentId').lrselectRefresh({ data: [] });
                $('#treefieldShow').lrselectRefresh({ data: [] });
                var strSql = $('#treesql').val();
                learun.httpAsync('GET', top.$.rootUrl + '/LR_SystemModule/DatabaseTable/GetSqlColName', { databaseLinkId: databaseLinkId, strSql: strSql }, function (data) {
                    var fieldData = [];
                    for (var i = 0, l = data.length; i < l; i++) {
                        var id = data[i];
                        var selectpoint = { value: id, text: id };
                        fieldData.push(selectpoint);
                    }
                    $('#treefieldId').lrselectRefresh({
                        data: fieldData
                    });
                    $('#treefieldParentId').lrselectRefresh({
                        data: fieldData
                    });
                    $('#treefieldShow').lrselectRefresh({
                        data: fieldData
                    });
                });
            });
            // 列表显示配置
            $('#col_gridtable').jfGrid({
                headData: [
                    {
                        label: "", name: "btn1", width: 50, align: "center",
                        formatter: function (value, row, op, $cell) {
                            $cell.on('click', function () {
                                var rowindex = parseInt($cell.attr('rowindex'));
                                var res = $('#col_gridtable').jfGridSet('moveUp', rowindex);
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
                                var res = $('#col_gridtable').jfGridSet('moveDown', rowindex);
                                return false;
                            });
                            return '<span class=\"label label-success\" style=\"cursor: pointer;\">下移</span>';
                        }
                    },
                    {
                        label: "列名", name: "field", width: 300, align: "left",
                        formatter: function (value, row, op, $cell) {
                            if (queryAllCompontMap[row.id]) {
                                row.fieldName = queryAllCompontMap[row.id].title;
                                return queryAllCompontMap[row.id].title;
                            }
                            else {
                                return '';
                            }
                        },
                        edit: {
                            type: 'select',
                            init: function (row, $self) {// 选中单元格后执行
                                $self.lrselectRefresh({
                                    data: queryAllComponts
                                });
                            },
                            op: {
                                value: 'field',
                                text: 'title',
                                title: 'title',
                                allowSearch: true
                            },
                            change: function (rowData, rowIndex, item) {
                                if (item != null) {
                                    rowData.id = item.table + item.field;
                                    
                                }
                                else {
                                    rowData.id = '';
                                }
                            }
                        }
                    },
                    {
                        label: "对齐", name: "align", width: 80, align: "left",
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
                        label: "宽度", name: "width", width: 80, align: "left",
                        edit: {
                            type:'input'
                        }
                    }
                ],
                isEdit: true,
                isMultiselect: true,
                onAddRow: function (row, rows) {
                    row.align = 'left';
                    row.width = 100;
                },
            });


            // 基础信息配置
            var loginInfo = learun.clientdata.get(['userinfo']);
            $('#createUser').val(loginInfo.realName);
            $('#outputArea').lrDataItemSelect({ code: 'outputArea' });

            $('#mappingDirectory').val(rootDirectory + $('#_mappingDirectory').val());
            $('#serviceDirectory').val(rootDirectory + $('#_serviceDirectory').val());
            $('#webDirectory').val(rootDirectory + $('#_webDirectory').val());

            // 代码查看
            $('#nav_tabs').lrFormTabEx();
            // 发布功能
            // 上级
            $('#F_ParentId').lrselect({
                url: top.$.rootUrl + '/LR_SystemModule/Module/GetExpendModuleTree',
                type: 'tree',
                maxHeight: 280,
                allowSearch: true
            });
            // 选择图标
            $('#selectIcon').on('click', function () {
                learun.layerForm({
                    id: 'iconForm',
                    title: '选择图标',
                    url: top.$.rootUrl + '/Utility/Icon',
                    height: 700,
                    width: 1000,
                    btn: null,
                    maxmin: true,
                    end: function () {
                        if (top._learunSelectIcon != '') {
                            $('#F_Icon').val(top._learunSelectIcon);
                        }
                    }
                });
            });
            // 保存数据按钮
            $("#btn_finish").on('click', page.save);
        },
        dbTableSearch: function (param) {
            param = param || {};
            param.databaseLinkId = databaseLinkId;
            $('#dbtablegird').jfGridSet('reload', { param: param });
        },
        /*保存数据*/
        save: function () {
            if (!$('#step-7').lrValidform()) {
                return false;
            }
            var moduleData = $('#step-7').lrGetFormData();
            moduleData.F_EnabledMark = 1;
            postData.moduleEntityJson = JSON.stringify(moduleData);
            $.lrSaveForm(top.$.rootUrl + '/LR_CodeGeneratorModule/TemplatePC/CreateCustmerCode', postData, function (res) { }, true);
        }
    };

    page.init();
}