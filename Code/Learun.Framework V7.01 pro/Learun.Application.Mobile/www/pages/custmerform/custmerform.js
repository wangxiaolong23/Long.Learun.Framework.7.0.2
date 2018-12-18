/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2017 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.7.12
 * 描 述：力软移动端框架(ADMS) 自定义表单发布功能-列表页
 */
(function () {
    var page = {
        formSchemeId: '',
        girdScheme: null,
        formScheme: null,

        mainTable: '',
        mainPk: '',
        mainCompontId: '',

        compontMap: {},
        tableMap: {},

        gird: null,
        init: function ($page, param) {
            // 获取参数初始化
            page.formSchemeId = param.formSchemeId;
            page.girdScheme = JSON.parse(param.girdScheme);
            page.formScheme = null;

            page.mainTable = '';
            page.mainPk = '';
            page.mainCompontId = '';

            page.compontMap = {};
            page.tableMap = {};

            // 获取自定义表单模板
            learun.custmerform.loadScheme(page.formSchemeId, function (scheme) {
                if (!scheme[page.formSchemeId]) {
                    learun.layer.toast('表单模板加载失败！');
                    learun.nav.closeCurrent();
                    return;
                }
                page.formScheme = scheme[page.formSchemeId];
                page.initScheme($page);
                page.initView($page);
            });
        },
        initScheme: function ($page) {// 初始化模板数据
            // 获取主表和主表主键
            $.each(page.formScheme.dbTable, function (_index, _item) {
                if (_item.relationName === "") {
                    page.mainTable = _item.name;
                    page.mainPk = _item.field;
                }
            });
            var tableIndex = 0;
            $.each(page.formScheme.data, function (_index, _item) {
                var componts = _item.componts;
                $.each(componts, function (_i, _compont) {
                    // 设置表对应标号
                    if (!!_compont.table && page.tableMap[_compont.table] === undefined) {
                        page.tableMap[_compont.table] = tableIndex;
                        tableIndex++;
                    }
                    if (page.mainTable === _compont.table && page.mainPk === _compont.field) {
                        page.mainCompontId = _compont.field + page.tableMap[_compont.table];
                    }
                    page.compontMap[_compont.id] = _compont;
                });
            });
        },
        initView: function ($page) {// 初始化视图列表页
            page.gird = $page.find('#custmerformfn').lrpagination({
                lclass: page.lclass,
                rows: 10,                            // 每页行数
                getData: function (param, callback) {// 获取数据 param 分页参数,callback 异步回调
                    page.loadData(param, callback, $page);
                },
                renderData: function (_index, _item, $item) {// 渲染数据模板
                    return page.rowRender(_index, _item, $item, $page);
                },
                click: function (item, $item, $et) {// 列表行点击事件
                    if ($et.hasClass('lr-btn-danger')) {
                        page.btnClick(item, $item, $page);
                    }
                    else {
                        page.rowClick(item, $item, $page);
                    }
                },
                btns: page.rowBtns
            });

            // 注册新增按钮点击事件
            $page.find('#custmerformfn_addbtn').on('tap', function () {
                learun.nav.go({ path: 'custmerform/form', title: '新增', type: 'right', param: { formScheme: page.formScheme, formSchemeId: page.formSchemeId } });
                return false;
            });
        },
        lclass: 'lr-custmer-list',
        loadData: function (param, callback, $page) {// 列表加载后台数据
            var _postParam = {
                pagination: {
                    rows: param.rows,
                    page: param.page,
                    sidx: page.mainCompontId.toLowerCase(),
                    sord: 'ASC'
                },
                queryJson: '{}',
                formId: page.formSchemeId
            };
            if (param.queryJson) {
                _postParam.queryJson = JSON.stringify(param.queryJson);
            }

            learun.httpget(config.webapi + "learun/adms/custmer/pagelist", _postParam, (data) => {
                $page.find('.lr-badge').text('0');
                if (data) {
                    $page.find('.lr-badge').text(data.records);
                    callback(data.rows, parseInt(data.records));
                }
                else {
                    callback([], 0);
                }
            });
        },
        rowRender: function (_index, _item, _$item, $page) {// 渲染列表行数据
            var title = page.girdScheme.title.split(',');

            var content0 = page.compontMap[page.girdScheme.content[0]];
            var content1 = page.compontMap[page.girdScheme.content[1]];
            var content2 = page.compontMap[page.girdScheme.content[2]];

            var _html = '<div class="lr-list-item">\
                                <div class="title" >\
                                </div >\
                                <div class="content">\
                                    <div class="one"><div><span class="lr-tag"></span>'+ content0.title + '</div><div class="text"></div></div>\
                                    <div class="two"><div><span class="lr-tag"></span>'+ content1.title + '</div><div class="text"></div></div>\
                                    <div class="three"><div><span class="lr-tag"></span>'+ content2.title + '</div><div class="text"></div></div>\
                                </div>\
                            </div>';
            _$item.append(_html);

            var _$title = _$item.find('.title');
            var _$one = _$item.find('.one>.text');
            var _$two = _$item.find('.two>.text');
            var _$three = _$item.find('.three>.text');

            page.getText(content0, _item[(content0.field + page.tableMap[content0.table]).toLowerCase()] || '', _$one);
            page.getText(content1, _item[(content1.field + page.tableMap[content1.table]).toLowerCase()] || '', _$two);
            page.getText(content2, _item[(content2.field + page.tableMap[content2.table]).toLowerCase()] || '', _$three);

            var titleText = '';
            $.each(title, function (_index, _jitem) {
                var _citem = page.compontMap[_jitem];
                var $span = $('<span></span>');
                page.getText(_citem, _item[(_citem.field + page.tableMap[_citem.table]).toLowerCase()] || '', $span);
                _$title.append($span);
            });
            return '';
        },
        rowClick: function (item, $item, $page) {// 列表行点击触发方法
            learun.nav.go({ path: 'custmerform/form', title: '详情', type: 'right', param: { formScheme: page.formScheme, formSchemeId: page.formSchemeId, keyValue: item[page.mainCompontId.toLowerCase()] } });
        },
        btnClick: function (item, $item, $page) {// 左滑按钮点击事件
            learun.layer.confirm('确定要删除该笔数据吗？', function (_index) {
                if (_index === '1') {
                    learun.layer.loading(true, "正在删除该笔数据");
                    learun.httppost(config.webapi + "learun/adms/form/delete", { schemeInfoId: page.formSchemeId, keyValue: item[page.mainCompontId.toLowerCase()] }, (data) => {
                        if (data) {// 删除数据成功
                            page.gird.reload();
                        }
                        learun.layer.loading(false);
                    });
                }
            }, '力软提示', ['取消', '确定']);
        },
        rowBtns: ['<a class="lr-btn-danger">删除</a>'], // 列表行左滑按钮
        getText: function (conpontItem, value, $div) {
            if (!conpontItem)
            { return; }
            switch (conpontItem.type) {
                case 'checkbox':
                    var v = value.split(',');
                    $.each(v, function (_index, _item) {
                        if (conpontItem.dataSource === "0") {
                            learun.clientdata.get('dataItem', {
                                key: _item,
                                code: conpontItem.itemCode,
                                callback: function (_data) {
                                    $div.append(_data.text);
                                }
                            });
                        }
                        else {
                            var vlist = conpontItem.dataSourceId.split(',');
                            learun.clientdata.get('sourceData', {
                                key: _item,
                                code: vlist[2],
                                callback: function (_data) {
                                    $div.append(_data[vlist[1]]);
                                }
                            });
                        }
                    });
                    break;
                case 'radio':
                case 'select':
                    if (conpontItem.dataSource === "0") {
                        learun.clientdata.get('dataItem', {
                            key: value,
                            code: conpontItem.itemCode,
                            callback: function (_data) {
                                $div.append(_data.text);
                            }
                        });
                    }
                    else {
                        var vlist = conpontItem.dataSourceId.split(',');
                        learun.clientdata.get('sourceData', {
                            key: value,
                            code: vlist[2],
                            callback: function (_data) {
                                $div.append(_data[vlist[1]]);
                            }
                        });
                    }
                    break;
                case 'organize':
                case 'currentInfo':
                    learun.clientdata.get(conpontItem.dataType, {
                        key: value,
                        callback: function (_data) {
                            $div.append(_data.name);
                        }
                    });
                    break;
                case 'datetime':
                    if (conpontItem.dateformat === '0') {
                        $div.append(learun.date.format(value, 'yyyy-MM-dd'));
                    }
                    else {
                        $div.append(learun.date.format(value, 'yyyy-MM-dd hh:mm'));
                    }
                    break;
                default:
                    $div.append(value);
                    break;
            }
        }
    };

    return page;
})();