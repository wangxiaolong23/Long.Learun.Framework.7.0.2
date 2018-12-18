/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2017 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2018.7.12
 * 描 述：力软移动端框架(ADMS) 自定义表单
 */

(function ($, learun, window) {
    // 加载自定义表单模板
    learun.custmerform = {
        loadScheme: function (schemeIds, callback) {// formIds表单主键集合,callback回调函数
            if (learun.type(schemeIds) === 'string') {
                schemeIds = [schemeIds];
            }

            var req = [];
            var scheme = {};
            $.each(schemeIds, function (_index, _item) {
                var formId = 'lrform' + _item;
                var formScheme = learun.storage.get(formId);// 从缓存中获取表单模板数据
                if (!formScheme) {
                    req.push({ id: _item, ver: "" });
                }
                else {
                    scheme[_item] = JSON.parse(formScheme.content);
                    req.push({ id: _item, ver: formScheme.ver });
                }
            });
            // 加载自定义表单模板
            learun.httpget(config.webapi + "learun/adms/form/scheme", req, (data) => {
                if (data) {
                    $.each(data, function (_index, _item) {
                        scheme[_index] = JSON.parse(_item.F_Scheme);
                        var formScheme = { ver: _item.F_Id, content: _item.F_Scheme };
                        learun.storage.set('lrform' + _index,formScheme);
                    });
                }
                callback(scheme);
            });
        }
    };
    // 自定义表单初始化
    $.fn.custmerform = function (formScheme) {
        var $this = $(this);
        $this.scroll();
        var $container = $this.find('.f-scroll');

        $.each(formScheme, function (_id, _scheme) {
            custmerformRender($container, _scheme.data, _id);
        });
        $this = null;
        $container = null;
    };
    // 获取自定义表单数据
    $.fn.custmerformGet = function () {
        var res = {};
        var validateflag = true;
        $(this).find('.lrcomponts').each(function () {
            var $this = $(this);
            var schemeInfoId = $this.attr('data-id');
            var _componts = $this[0].componts;
            res[schemeInfoId] = res[schemeInfoId] || {};
            // 遍历自定义表单控件
            $.each(_componts, function (_index, _item) {
                var _fn = componts[_item.type].get;
                if (_fn) {
                    var compontData = _fn(_item, $this);
                    if (compontData.isHad) {
                        if (_item.verify) {
                            var checkfn = window.fui.validator['is' + _item.verify];
                            var r = checkfn(compontData.value);
                            if (!r.code) {
                                validateflag = false;
                                window.fui.dialog({ msg: r.msg });
                                return false;
                            }
                        }
                        res[schemeInfoId][_item.id] = compontData.value;
                    }
                }
            });
            $this = null;
            if (!validateflag) {
                return false;
            }
        });
        if (!validateflag) {
            return null;
        }
        return res;
    };
    // 设置自定义表单数据
    $.fn.custmerformSet = function (data) {
        var $this = $(this);
        function set($this, data) {
            if ($this.find('.lrcomponts').length > 0) {
                $this.find('.lrcomponts').each(function () {
                    var $this = $(this);
                    var schemeInfoId = $this.attr('data-id');
                    var _componts = $this[0].componts;
                    var _data = {};
                    $.each(data[schemeInfoId] || [], function (_index, _item) {
                        $.each(_item[0] || [], function (_id, _jitem) {
                            _data[_index.toLowerCase() + _id] = _jitem;
                        });
                    });
                    // 遍历自定义表单控件
                    $.each(_componts, function (_index, _item) {
                        var _fn = componts[_item.type].set;
                        if (_fn) {
                            if (_item.table && _item.field) {
                                _fn(_item, _data[(_item.table + _item.field).toLowerCase()], $this);
                            }
                            else if (_item.table){// 表格
                                _fn(_item, data[schemeInfoId][_item.table], $this);
                            }
                        }
                    });
                    $this = null;
                });
            }
            else {
                setTimeout(function () {
                    set($this, data);
                }, 100);
            }
        }
        set($this, data);
    };

    function getFontHtml(verify) {
        var res = "";
        switch (verify) {
            case "NotNull":
            case "Num":
            case "Email":
            case "EnglishStr":
            case "Phone":
            case "Fax":
            case "Mobile":
            case "MobileOrPhone":
            case "Uri":
                res = '<font face="宋体">*</font>';
                break;
        }
        return res;
    }
    function loadCheck(data, text, value, compontId) {
        var $row = $('#' + compontId);
        if ($row.length === 0) {
            setTimeout(function () {
                loadCheck(data, text, value, compontId);
            }, 100);
        }
        else {
            var $Last = $row;
            $.each(data, function (_index, _item) {
                $div = $('<div class="lr-form-row" data-name="' + compontId + '" data-value="' + _item[value] + '" ><label>' + _item[text] + '</label><div class="checkbox" ></div></div>');
                $Last.after($div);
                $div.find('.checkbox').lrswitch();
                $Last = $div;
                $div = null;
            });
            if ($Last) {
                $Last.after('<div class="lr-form-row lr-form-row-title" style="min-height:6px;" ></div>');
                $Last = null;
            }
        }
        $row = null;
    }
    function organizeRegister1(_compont) {
        if ($('#' + _compont.relation).length > 0) {
            $('#' + _compont.relation).on('change', { myId: _compont.id }, function (e) {
                e = e || window.event;
                var myId = e.data.myId;
                var value = $(this).lrselectGet();
                $('#' + myId).lrselectUpdate({
                    companyId: value,
                    needPre: value === '' ? true : false
                });
            });
        }
        else {
            setTimeout(function () { organizeRegister1(_compont); }, 100);
        }
    }
    function organizeRegister2(_compont) {
        if ($('#' + _compont.relation).length > 0) {
            $('#' + _compont.relation).on('change', { myId: _compont.id }, function (e) {
                e = e || window.event;
                var myId = e.data.myId;
                var value = $(this).lrselectGet();
                $('#' + myId).lrselectUpdate({
                    departmentId: value,
                    needPre: value === '' ? true : false
                });
            });
        }
        else {
            setTimeout(function () { organizeRegister2(_compont); }, 100);
        }
    }

    function loadGridComponts(compont) {
    }
    function loadGridButton(compont) {// 编辑表格增加按钮
        var $row = $('#' + compont.id);
        if ($row.length === 0) {
            setTimeout(function () {
                loadGridButton(compont);
            }, 100);
        }
        else {
            var $btn = $('<div class="lr-form-row lr-edit-grid-btn" ><i class="iconfont icon-add1" ></i>增加' + (compont.title || '') + '</div>');
            $btn[0].compont = compont;

            $btn.on('tap', { compont: compont }, function (e) {
                e = e || window.event;
            });

            $row.after($btn);
        }
        $row = null;
    }

    // 渲染自定义表单
    function custmerformRender($container, scheme, schemeInfoId) {
        var loaddataComponts = [];
        $.each(scheme, function (_index, _item) {
            var $list = $('<div class="lr-form-container lrcomponts" data-id="' + schemeInfoId + '" ></div>');
            $list[0].componts = _item.componts;
            $.each(_item.componts, function (_jindex, _jitem) {
                var $row = $('<div class="lr-form-row"><label>' + _jitem.title + '</label></div>');
                if (componts[_jitem.type].render($row, _jitem)) {
                    $list.append($row);
                    $row.prepend(getFontHtml(_jitem.verify));
                }
            });
            $container.append($list);
        });

        $container = null;
    }

    var componts = {
        label: {
            render: function ($row, compont) {
                $row.addClass('lr-form-row-title');
                return true;
            }
        },
        html: {
            render: function ($row, compont) {
                return false;
            }
        },
        text: {
            render: function ($row, compont) {
                var $compont = $('<input id="' + compont.id + '" type="text" />');
                $row.append($compont);
                $compont.val(compont.dfvalue || '');

                $compont = null;
                $row = null;
                if (compont.isHide === '1') {
                    return false;
                }

                return true;
            },
            get: function (compont, $container) {
                var res = {};
                var $compont = $container.find('#' + compont.id);
                res.isHad = $compont.length > 0 ? true : false;
                if (res.isHad) {
                    res.value = $compont.val();
                }
                $compont = null;
                return res;
            },
            set: function (compont, value, $container) {
                $container.find('#' + compont.id).val(value || '');
            }
        },
        textarea: {
            render: function ($row, compont) {
                $row.addClass('lr-form-row-multi');
                var $compont = $('<textarea id="' + compont.id + '" ' + 'style="height: ' + compont.height + 'px;" ></textarea>');
                $compont.text(compont.dfvalue || '');
                $row.append($compont);
                $compont = null;
                $row = null;
                return true;
            },
            get: function (compont, $container) {
                var res = {};
                var $compont = $container.find('#' + compont.id);
                res.isHad = $compont.length > 0 ? true : false;
                if (res.isHad) {
                    res.value = $compont.val();
                }
                $compont = null;
                return res;
            },
            set: function (compont, value, $container) {
                $container.find('#' + compont.id).val(value || '');
            }
        },
        texteditor: {
            render: function ($row, compont) {
                $row.addClass('lr-form-row-multi');
                var $compont = $('<textarea id="' + compont.id + '" ' + 'style="height: ' + compont.height + 'px;" ></textarea>');
                $compont.text(compont.dfvalue || '');
                $row.append($compont);
                $compont = null;
                $row = null;
                return true;
            },
            get: function (compont, $container) {
                var res = {};
                var $compont = $container.find('#' + compont.id);
                res.isHad = $compont.length > 0 ? true : false;
                if (res.isHad) {
                    res.value = $compont.val();
                }
                $compont = null;
                return res;
            },
            set: function (compont, value, $container) {
                $container.find('#' + compont.id).val(value || '');
            }
        },
        radio: {
            render: function ($row, compont) {// 单选改用和下拉一致
                var $compont = $('<div id="' + compont.id + '" ></div>');
                $row.append($compont);
                if (compont.dataSource === '0') {
                    $compont.lrpickerex({
                        code: compont.itemCode,
                        type: 'dataItem'
                    });
                } else {
                    var vlist = compont.dataSourceId.split(',');
                    $compont.lrpickerex({
                        code: vlist[0],
                        type: 'sourceData',
                        ivalue: vlist[2],
                        itext: vlist[1]
                    });
                }

                $compont = null;
                $row = null;
                return true;
            },
            get: function (compont, $container) {
                var res = {};
                var $compont = $container.find('#' + compont.id);
                res.isHad = $compont.length > 0 ? true : false;
                if (res.isHad) {
                    res.value = $compont.lrpickerGet();
                }
                $compont = null;
                return res;
            },
            set: function (compont, value, $container) {
                $container.find('#' + compont.id).lrpickerSet(value);
            }
        },
        checkbox: {
            render: function ($row, compont) {
                var $compont = $('<div id="' + compont.id + '" ></div>');
                $row.append($compont);
                if (compont.dataSource === '0') {
                    $compont.lrcheckboxex({
                        code: compont.itemCode,
                        type: 'dataItem'
                    });
                } else {
                    var vlist = compont.dataSourceId.split(',');
                    $compont.lrcheckboxex({
                        code: vlist[0],
                        type: 'sourceData',
                        ivalue: vlist[2],
                        itext: vlist[1]
                    });
                }

                $compont = null;
                $row = null;
                return true;
            },
            get: function (compont, $container) {
                var res = {};
                var $compont = $container.find('#' + compont.id);
                res.isHad = $compont.length > 0 ? true : false;
                if (res.isHad) {
                    res.value = $compont.lrcheckboxGet();
                }
                $compont = null;
                return res;
            },
            set: function (compont, value, $container) {
                $container.find('#' + compont.id).lrcheckboxSet(value);
            }
        },
        select: {
            render: function ($row, compont) {//
                var $compont = $('<div id="' + compont.id + '" ></div>');
                $row.append($compont);
                if (compont.dataSource === '0') {
                    $compont.lrpickerex({
                        code: compont.itemCode,
                        type: 'dataItem'
                    });
                } else {
                    var vlist = compont.dataSourceId.split(',');
                    $compont.lrpickerex({
                        code: vlist[0],
                        type: 'sourceData',
                        ivalue: vlist[2],
                        itext: vlist[1]
                    });
                }

                $compont = null;
                $row = null;
                return true;
            },
            get: function (compont, $container) {
                var res = {};
                var $compont = $container.find('#' + compont.id);
                res.isHad = $compont.length > 0 ? true : false;
                if (res.isHad) {
                    res.value = $compont.lrpickerGet();
                }
                $compont = null;
                return res;
            },
            set: function (compont, value, $container) {
                $container.find('#' + compont.id).lrpickerSet(value);
            }
        },
        datetime: {
            render: function ($row, compont) {//
                var $compont = $('<div id="' + compont.id + '" ></div>');
                $row.append($compont);
                if (compont.dateformat === '0') {
                    $compont.lrdate({
                        type: 'date'
                    });
                }
                else {
                    $compont.lrdate();
                }
                $compont = null;
                $row = null;
                return true;
            },
            get: function (compont, $container) {
                var res = {};
                var $compont = $container.find('#' + compont.id);
                res.isHad = $compont.length > 0 ? true : false;
                if (res.isHad) {
                    res.value = $compont.lrdateGet();
                }
                $compont = null;
                return res;
            },
            set: function (compont, value, $container) {
                if (compont.dateformat === '0') {
                    value = learun.date.format(value, 'yyyy-MM-dd');
                }
                else {
                    value = learun.date.format(value, 'yyyy-MM-dd hh:mm');
                }

                $container.find('#' + compont.id).lrdateSet(value);
            }
        },
        datetimerange: {
            render: function ($row, compont) {//
                var $compont = $('<input id="' + compont.id + '" type="text" />');
                function register(_compont) {
                    if ($('#' + _compont.startTime).length > 0 && $('#' + _compont.endTime).length > 0) {
                        $('#' + _compont.startTime).on('change', { myId: _compont.id, end: _compont.endTime }, function (e) {
                            e = e || window.event;
                            var end = e.data.end;
                            var myId = e.data.myId;
                            var st = $(this).lrdateGet();
                            var et = $('#' + end).lrdateGet();
                            if (!!st && !!et) {
                                var diff = learun.date.parse(st).DateDiff('d', et) + 1;
                                $('#' + myId).val(diff);
                            }
                        });
                        $('#' + _compont.endTime).on('change', { myId: _compont.id, begin: _compont.startTime }, function (e) {
                            e = e || window.event;
                            var begin = e.data.begin;
                            var myId = e.data.myId;

                            var st = $('#' + begin).lrdateGet();
                            var et = $(this).lrdateGet();
                            if (!!st && !!et) {
                                var diff = learun.date.parse(st).DateDiff('d', et) + 1;
                                $('#' + myId).val(diff);
                            }
                        });
                    }
                    else {
                        setTimeout(function () {
                            register(_compont);
                        }, 100);
                    }
                }
                if (!!compont.startTime && compont.endTime) {
                    register(compont);
                }
                $row.append($compont);
                $compont = null;
                $row = null;
                return true;
            },
            get: function (compont, $container) {
                var res = {};
                var $compont = $container.find('#' + compont.id);
                res.isHad = $compont.length > 0 ? true : false;
                if (res.isHad) {
                    res.value = $compont.val();
                }
                $compont = null;
                return res;
            },
            set: function (compont, value, $container) {
                $container.find('#' + compont.id).val(value);
            }
        },
        encode: {
            render: function ($row, compont) {
                var $compont = $('<input id="' + compont.id + '" type="text" readonly  />');
                compont.isInit = false;
                learun.getRuleCode(compont.rulecode, function (data) {
                    if (!compont.isInit) {
                        compont.isInit = true;
                        $('#' + compont.id).val(data);
                    }
                });
                $row.append($compont);
                $compont = null;
                $row = null;
                return true;
            },
            get: function (compont, $container) {
                var res = {};
                var $compont = $container.find('#' + compont.id);
                res.isHad = $compont.length > 0 ? true : false;
                if (res.isHad) {
                    res.value = $compont.val();
                }
                $compont = null;
                return res;
            },
            set: function (compont, value, $container) {
                compont.isInit = true;
                $container.find('#' + compont.id).val(value);
            }
        },
        organize: {
            render: function ($row, compont) {
                var $compont = $('<div id="' + compont.id + '" ></div>');
                $row.append($compont);
                switch (compont.dataType) {
                    case "company"://公司
                        $compont.lrselect();
                        break;
                    case "department"://部门
                        $compont.lrselect({
                            type: 'department',
                            needPre: compont.relation === '' ? false : true
                        });
                        
                        organizeRegister1(compont);
                        break;
                    case "user"://用户
                        $compont.lrselect({
                            type: 'user',
                            needPre: compont.relation === '' ? false : true
                        });
                        organizeRegister2(compont);
                        break;
                }
                $compont = null;
                $row = null;
                return true;
            },
            get: function (compont, $container) {
                var res = {};
                var $compont = $container.find('#' + compont.id);
                res.isHad = $compont.length > 0 ? true : false;
                if (res.isHad) {
                    res.value = $compont.lrselectGet();
                }
                $compont = null;
                return res;
            },
            set: function (compont, value, $container) {
                $container.find('#' + compont.id).lrselectSet(value);
            }
        },
        currentInfo: {
            render: function ($row, compont) {
                var $compont = $('<input id="' + compont.id + '" readonly type="text"  />');
                var userinfo = learun.storage.get('userinfo');
                switch (compont.dataType) {
                    case 'company':
                        compont.value = userinfo.baseinfo.companyId;
                        if (compont.isHide !== '1') {
                            learun.clientdata.get('company', {
                                key: compont.value,
                                compont: compont,
                                callback: function (item, _op) {
                                    if (!_op.compont.isEdit) {
                                        $(_op.compont.id).val(item.name);
                                    }
                                }
                            });
                        }
                        break;
                    case 'department':
                        compont.value = userinfo.baseinfo.departmentId;
                        if (compont.isHide !== '1') {
                            learun.clientdata.get('department', {
                                key: compont.value,
                                compont: compont,
                                callback: function (item, _op) {
                                    if (!_op.compont.isEdit) {
                                        $(_op.compont.id).val(item.name);
                                    }
                                }
                            });
                        }
                        break;
                    case 'user':
                        $compont.val(userinfo.baseinfo.realName);
                        compont.value = userinfo.baseinfo.userId;
                        break;
                    case 'time':
                        compont.value = learun.date.format(new Date(), 'yyyy-MM-dd hh:mm:ss');
                        $compont.val(compont.value);
                        break;
                    case 'guid':
                        compont.value = learun.guid();
                        $compont.val(compont.value);
                        break;
                }
                if (compont.isHide === '1') {
                    $compont = null;
                    $row = null;
                    return false;
                }
                else {
                    $row.append($compont);
                    $compont = null;
                    $row = null;
                }
                return true;
            },
            get: function (compont, $container) {
                var res = {};
                var $compont = $container.find('#' + compont.id);
                res.isHad = $compont.length > 0 ? true : false;
                if (res.isHad) {
                    res.value = compont.value;
                }
                $compont = null;
                return res;
            },
            set: function (compont, value, $container) {
                if (value) {
                    var organization = learun.storage.get('organization');
                    compont.isEdit = true;
                    switch (compont.dataType) {
                        case 'company':
                            compont.value = value;
                            if (compont.isHide !== '1') {
                                learun.clientdata.get('company', {
                                    key: compont.value,
                                    compont: compont,
                                    callback: function (item, _op) {
                                        $container.find('#' + _op.compont.id).val(item.name || '');
                                    }
                                });
                            }
                            break;
                        case 'department':
                            compont.value = value;
                            if (compont.isHide !== '1') {
                                learun.clientdata.get('department', {
                                    key: compont.value,
                                    compont: compont,
                                    callback: function (item, _op) {
                                        $container.find('#' + _op.compont.id).val(item.name || '');
                                    }
                                });
                            }
                            break;
                        case 'user':
                            compont.value = value;
                            if (compont.isHide !== '1') {
                                learun.clientdata.get('user', {
                                    key: compont.value,
                                    compont: compont,
                                    callback: function (item, _op) {
                                        $container.find('#' + _op.compont.id).val(item.name || '');
                                    }
                                });
                            }
                            break;
                        case 'time':
                            compont.value = value;
                            if (compont.isHide !== '1') {
                                $container.find('#' + compont.id).val(value);
                            }
                            break;
                        case 'guid':
                            compont.value = value;
                            if (compont.isHide !== '1') {
                                $container.find('#' + compont.id).val(value);
                            }
                            break;
                    }
                }
            }
        },
        guid: {
            render: function ($row, compont) {
                compont.value = learun.guid();
                $row.remove();
                return false;
            },
            get: function (compont) {
                var res = {};
                res.isHad = true;
                res.value = compont.value;
                return res;
            },
            set: function (compont, value) {
                compont.value = value;
            }
        },
        upload: {
            render: function ($row, compont) {
                $row.addClass('lr-form-row-multi');
                var $compont = $('<div id="' + compont.id + '" ></div>');
                $row.append($compont);
                $compont.lrImagepicker();
                $compont = null;
                $row = null;
                return true;
            },
            get: function (compont, $container) {
                var res = {};
                var $compont = $container.find('#' + compont.id);
                res.isHad = $compont.length > 0 ? true : false;
                if (res.isHad) {
                    res.value = $compont.imagepickerGet();
                }
                $compont = null;
                return res;
            },
            set: function (compont, value, $container) {
                $container.find('#' + compont.id).imagepickerSet(value);
            }
        },
        girdtable: {
            render: function ($row, compont) {
                var gridCompont = [];
                $.each(compont.fieldsData, function (_index, _item) {
                    if (_item.field) {
                        switch (_item.type) {
                            case 'radio':
                            case 'select':
                            case 'checkbox':
                                if (_item.dataSource === '0') {
                                    _item.code = _item.itemCode;
                                    _item.datatype = 'dataItem';
                                }
                                else {
                                    _item.code = _item.dataSourceId;
                                    _item.ivalue = _item.saveField;
                                    _item.itext = _item.showField;
                                    _item.datatype = 'sourceData';
                                }
                                break;
                            case 'layer':
                                if (_item.dataSource === '0') {
                                    _item.code = _item.itemCode;
                                    _item.datatype = 'dataItem';
                                }
                                else {
                                    _item.code = _item.dataSourceId;
                                    _item.datatype = 'sourceData';
                                }
                                break;
                        }

                      
                        gridCompont.push(_item);
                    }
                });
                $row.attr('id', compont.id);
                $row.lrgrid({
                    title: compont.title,
                    componts: gridCompont,
                    isToLowerCase: true
                });
                $row = null;
                return true;
            },
            get: function (compont, $container) {
                var res = {};
                var $compont = $container.find('#' + compont.id);
                res.isHad = $compont.length > 0 ? true : false;
                if (res.isHad) {
                    res.value = $compont.lrgridGet();
                }
                $compont = null;
                return res;
            },
            set: function (compont, value, $container) {
                $container.find('#' + compont.id).lrgridSet(value);
            }
        }
    };

})(window.jQuery, window.lrmui, window);

