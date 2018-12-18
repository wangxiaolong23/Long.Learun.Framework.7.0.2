/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.03.16
 * 描 述：表单处理方法
 */
(function ($, learun) {
    "use strict";

    /*获取和设置表单数据*/
    $.fn.lrGetFormData = function (keyValue) {// 获取表单数据
        var resdata = {};
        $(this).find('input,select,textarea,.lr-select,.lr-formselect,.lrUploader-wrap,.lr-radio,.lr-checkbox,.edui-default').each(function (r) {
            var id = $(this).attr('id');
            if (!!id) {
                var type = $(this).attr('type');
                switch (type) {
                    case "radio":
                        if ($("#" + id).is(":checked")) {
                            var _name = $("#" + id).attr('name');
                            resdata[_name] = $("#" + id).val();
                        }
                        break;
                    case "checkbox":
                        if ($("#" + id).is(":checked")) {
                            resdata[id] = 1;
                        } else {
                            resdata[id] = 0;
                        }
                        break;
                    case "lrselect":
                        resdata[id] = $(this).lrselectGet();
                        break;
                    case "formselect":
                        resdata[id] = $(this).lrformselectGet();
                        break;
                    case "lrGirdSelect":
                        resdata[id] = $(this).lrGirdSelectGet();
                        break;
                    case "lr-Uploader":
                        resdata[id] = $(this).lrUploaderGet();
                        break;
                    case "lr-radio":
                        resdata[id] = $(this).find('input:checked').val();
                        break;
                    case "lr-checkbox":
                        var _idlist = [];
                        $(this).find('input:checked').each(function () {
                            _idlist.push($(this).val());
                        });
                        resdata[id] = String(_idlist);
                        break;
                    default:
                        if ($("#" + id).hasClass('currentInfo')) {
                            var value = $("#" + id)[0].lrvalue;
                            resdata[id] = $.trim(value);
                        }
                        else if ($(this).hasClass('edui-default')) {
                            if ($(this)[0].ue) {
                                resdata[id] = $(this)[0].ue.getContent(null, null, true);
                            }
                        }
                        else {

                            var value = $("#" + id).val();
                            resdata[id] = $.trim(value);
                        }

                        break;
                }
                resdata[id] += '';
                if (resdata[id] == '') {
                    resdata[id] = '&nbsp;';
                }
                if (resdata[id] == '&nbsp;' && !keyValue) {
                    resdata[id] = '';
                }
            }
        });
        return resdata;
    };
    $.fn.lrSetFormData = function (data) {// 设置表单数据
        var $this = $(this);
        for (var id in data) {
            var value = data[id];
            var $obj = $this.find('#' + id);
            if ($obj.length == 0 && value != null) {
                $obj = $this.find('[name="' + id + '"][value="' + value + '"]');
                if ($obj.length > 0) {
                    if (!$obj.is(":checked")) {
                        $obj.trigger('click');
                    }
                }
            }
            else {
                var type = $obj.attr('type');
                if ($obj.hasClass("lr-input-wdatepicker")) {
                    type = "datepicker";
                }
                switch (type) {
                    case "checkbox":
                        var isck = 0;
                        if ($obj.is(":checked")) {
                            isck = 1;
                        } else {
                            isck = 0;
                        }
                        if (isck != parseInt(value)) {
                            $obj.trigger('click');
                        }
                        break;
                    case "lrselect":
                        $obj.lrselectSet(value);
                        break;
                    case "formselect":
                        $obj.lrformselectSet(value);
                        break;
                    case "lrGirdSelect":
                        $obj.lrGirdSelectSet(value);
                        break;
                    case "datepicker":
                        $obj.val(learun.formatDate(value, 'yyyy-MM-dd'));
                        break;
                    case "lr-Uploader":
                        $obj.lrUploaderSet(value);
                        break;
                    case "lr-radio":
                        if (!$obj.find('input[value="' + value + '"]').is(":checked")) {
                            $obj.find('input[value="' + value + '"]').trigger('click');
                        }
                        break;
                    default:
                        if ($obj.hasClass('currentInfo')) {
                            $obj[0].lrvalue = value;
                            if ($obj.hasClass('currentInfo-user')) {
                                $obj.val('');
                                learun.clientdata.getAsync('user', {
                                    key: value,
                                    callback: function (item, op) {
                                        op.obj.val(item.name);
                                    },
                                    obj: $obj
                                });
                            }
                            else if ($obj.hasClass('currentInfo-company')) {
                                $obj.val('');
                                learun.clientdata.getAsync('company', {
                                    key: value,
                                    callback: function (_data, op) {
                                        op.obj.val(_data.name);
                                    },
                                    obj: $obj
                                });
                            }
                            else if ($obj.hasClass('currentInfo-department')) {
                                $obj.val('');
                                learun.clientdata.getAsync('department', {
                                    key: value,
                                    callback: function (_data, op) {
                                        op.obj.val(_data.name);
                                    },
                                    obj: $obj
                                });
                            }
                            else {
                                $obj.val(value);
                            }

                        }
                        else if ($obj.hasClass('edui-default')) {
                            var ue = $obj[0].ue;
                            setUe(ue, value);
                        }
                        else {
                            $obj.val(value);
                        }

                        
                        break;
                }
            }
        }
    };

    function setUe(ue, value) {
        ue.ready(function () {
            ue.setContent(value);
        });
    }

    /*表单数据操作*/
    $.lrSetForm = function (url, callback) {
        learun.loading(true, '正在获取数据');
        learun.httpAsyncGet(url, function (res) {
            learun.loading(false);
            if (res.code == learun.httpCode.success) {
                callback(res.data);
            }
            else {
                learun.layerClose(window.name);
                learun.alert.error('表单数据获取失败,请重新获取！');
                learun.httpErrorLog(res.info);
            }
        });
    };
    $.lrSaveForm = function (url, param, callback, isNotClosed) {
        param['__RequestVerificationToken'] = $.lrToken;
        learun.loading(true, '正在保存数据');
        learun.httpAsyncPost(url, param, function (res) {
            learun.loading(false);
            if (res.code == learun.httpCode.success) {
                if (!!callback) {
                    callback(res);
                }
                learun.alert.success(res.info);
                if (!isNotClosed) {
                    learun.layerClose(window.name);
                }
            }
            else {
                learun.alert.error(res.info);
                learun.httpErrorLog(res.info);
            }
        });
    };
    $.lrPostForm = function (url, param) {
        param['__RequestVerificationToken'] = $.lrToken;
        learun.loading(true, '正在提交数据');
        learun.httpAsyncPost(url, param, function (res) {
            learun.loading(false);
            if (res.code == learun.httpCode.success) {
                learun.alert.success(res.info);
            }
            else {
                learun.alert.error(res.info);
                learun.httpErrorLog(res.info);
            }
        });
    };

    /*tab页切换*/
    $.fn.lrFormTab = function () {
        var $this = $(this);
        $this.parent().css({ 'padding-top': '44px' });
        $this.lrscroll();

        $this.on('DOMNodeInserted', function (e) {
            var $this = $(this);
            var w = 0;
            $this.find('li').each(function () {
                w += $(this).outerWidth();
            });
            $this.find('.lr-scroll-box').css({ 'width': w });
        });

     



        $this.delegate('li', 'click', { $ul: $this }, function (e) {
            var $li = $(this);
            if (!$li.hasClass('active')) {
                var $parent = $li.parent();
                var $content = e.data.$ul.next();

                var id = $li.find('a').attr('data-value');
                $parent.find('li.active').removeClass('active');
                $li.addClass('active');
                $content.find('.tab-pane.active').removeClass('active');
                $content.find('#' + id).addClass('active');
            }
        });
    }
    $.fn.lrFormTabEx = function (callback) {
        var $this = $(this);
        $this.delegate('li', 'click', { $ul: $this }, function (e) {
            var $li = $(this);
            if (!$li.hasClass('active')) {
                var $parent = $li.parent();
                var $content = e.data.$ul.next();

                var id = $li.find('a').attr('data-value');
                $parent.find('li.active').removeClass('active');
                $li.addClass('active');
                $content.find('.tab-pane.active').removeClass('active');
                $content.find('#' + id).addClass('active');

                if (!!callback) {
                    callback(id);
                }
            }
        });
    }
    
    /*检测字段是否重复*/
    $.lrExistField = function (keyValue, controlId, url, param) {
        var $control = $("#" + controlId);
        if (!$control.val()) {
            return false;
        }
        var data = {
            keyValue: keyValue
        };
        data[controlId] = $control.val();
        $.extend(data, param);
        learun.httpAsync('GET', url, data, function (data) {
            if (data == false) {
                $.lrValidformMessage($control, '已存在,请重新输入');
            }
        });
    };

    /*固定下拉框的一些封装：数据字典，组织机构，省市区级联*/
    // 数据字典下拉框
    $.fn.lrDataItemSelect = function (op) {
        // op:code 码,parentId 父级id,maxHeight 200,allowSearch， childId 级联下级框id
        var dfop = {
            // 是否允许搜索
            allowSearch: false,
            // 访问数据接口地址
            //url: top.$.rootUrl + '/LR_SystemModule/DataItem/GetDetailListByParentId',
            // 访问数据接口参数
            param: { itemCode: '', parentId: '0' },
            // 级联下级框
        }
        op = op || {};
        if (!op.code) {
            return $(this);
        }
        dfop.param.itemCode = op.code;
        dfop.param.parentId = op.parentId || '0';
        dfop.allowSearch = op.allowSearch;

        var list = [];

        if (!!op.childId) {
            var list2 = [];
            $('#' + op.childId).lrselect({
                // 是否允许搜索
                allowSearch: dfop.allowSearch
            });
            dfop.select = function (item) {
                if (!item) {
                    $('#' + op.childId).lrselectRefresh({
                        data: []
                    });
                }
                else {
                    list2 = [];
                    learun.clientdata.getAllAsync('dataItem', {
                        code: dfop.param.itemCode,
                        callback: function (dataes) {
                            $.each(dataes, function (_index, _item) {
                                if (_item.parentId == item.k) {
                                    list2.push({ id: _item.text, text: _item.value, title: _item.text, k: _index });
                                }
                            });
                            $('#' + op.childId).lrselectRefresh({
                                data: list2
                            });
                        }
                    });
                }
            };
        }
        var $select = $(this).lrselect(dfop);
        learun.clientdata.getAllAsync('dataItem', {
            code: dfop.param.itemCode,
            callback: function (dataes) {
                $.each(dataes, function (_index, _item) {
                    if (_item.parentId == dfop.param.parentId) {
                        list.push({ id: _item.value, text: _item.text, title: _item.text, k: _index });
                    }
                });
                $select.lrselectRefresh({
                    data: list
                });
            }
        });
        return $select;
    };
    // 数据源下拉框
    $.fn.lrDataSourceSelect = function (op) {
        op = op || {};
        var dfop = {
            // 是否允许搜索
            allowSearch: true,
            select: op.select,
        }
        if (!op.code) {
            return $(this);
        }
        var $select = $(this).lrselect(dfop);

        learun.clientdata.getAllAsync('sourceData', {
            code: op.code,
            callback: function (dataes) {
                $select.lrselectRefresh({
                    value: op.value,
                    text: op.text,
                    title: op.text,
                    data: dataes
                });
            }
        });
        return $select;
    }

    // 公司信息下拉框
    $.fn.lrCompanySelect = function (op) {
        // op:parentId 父级id,maxHeight 200,
        var dfop = {
            type: 'tree',
            // 是否允许搜索
            allowSearch: true,
            // 访问数据接口地址
            url: top.$.rootUrl + '/LR_OrganizationModule/Company/GetTree',
            // 访问数据接口参数
            param: { parentId: '0' },
        };
        op = op || {};
        dfop.param.parentId = op.parentId || '0';

        if (!!op.isLocal) {
            dfop.url = '';
        }
        var $select = $(this).lrselect(dfop);
        if (!!op.isLocal) {
            learun.clientdata.getAllAsync('company', {
                callback: function (dataes) {
                    var mapdata = {};
                    var resdata = [];
                    $.each(dataes, function (_index, _item) {
                        mapdata[_item.parentId] = mapdata[_item.parentId] || [];
                        _item.id = _index;
                        mapdata[_item.parentId].push(_item);
                    });
                    _fn(resdata, dfop.param.parentId);
                    function _fn(_data, vparentId) {
                        var pdata = mapdata[vparentId] || [];
                        for (var j = 0, l = pdata.length; j < l; j++) {
                            var _item = pdata[j];
                            var _point = {
                                id: _item.id,
                                text: _item.name,
                                value: _item.id,
                                showcheck: false,
                                checkstate: false,
                                hasChildren: false,
                                isexpand: false,
                                complete: true,
                                ChildNodes: []
                            };
                            if (_fn(_point.ChildNodes, _item.id)) {
                                _point.hasChildren = true;
                                _point.isexpand = true;
                            }
                            _data.push(_point);
                        }
                        return _data.length > 0;
                    }
                    $select.lrselectRefresh({
                        data: resdata
                    });
                }
            });
        }

        return $select;

    };
    // 部门信息下拉框
    $.fn.lrDepartmentSelect = function (op) {
        // op:parentId 父级id,maxHeight 200,
        var dfop = {
            type: 'tree',
            // 是否允许搜索
            allowSearch: true,
            // 访问数据接口地址
            url: top.$.rootUrl + '/LR_OrganizationModule/Department/GetTree',
            // 访问数据接口参数
            param: { companyId: '', parentId: '0' },
        }
        op = op || {};
        dfop.param.companyId = op.companyId;
        dfop.param.parentId = op.parentId;

        return $(this).lrselect(dfop);;
    };
    // 人员下拉框
    $.fn.lrUserSelect = function (type) {//0单选1多选
        if (type == 0) {
            $(this).lrformselect({
                layerUrl: top.$.rootUrl + '/LR_OrganizationModule/User/SelectOnlyForm',
                layerUrlW: 400,
                layerUrlH: 300,
                dataUrl: top.$.rootUrl + '/LR_OrganizationModule/User/GetListByUserIds'
            });
        }
        else {
            $(this).lrformselect({
                layerUrl: top.$.rootUrl + '/LR_OrganizationModule/User/SelectForm',
                layerUrlW: 800,
                layerUrlH: 520,
                dataUrl: top.$.rootUrl + '/LR_OrganizationModule/User/GetListByUserIds'
            });
        }
    }

    // 省市区级联
    $.fn.lrAreaSelect = function (op) {
        // op:parentId 父级id,maxHeight 200,
        var dfop = {
            // 字段
            value: "F_AreaCode",
            text: "F_AreaName",
            title: "F_AreaName",
            // 是否允许搜索
            allowSearch: true,
            // 访问数据接口地址
            url: top.$.rootUrl + '/LR_SystemModule/Area/Getlist',
            // 访问数据接口参数
            param: { parentId: ''},
        }
        op = op || {};
        if (!!op.parentId) {
            dfop.param.parentId = op.parentId;
        }
        var _obj = [], i = 0;
        var $this = $(this);
        $(this).find('div').each(function () {
            var $div = $('<div></div>');
            var $obj = $(this);
            dfop.placeholder = $obj.attr('placeholder');
            $div.addClass($obj.attr('class'));
            $obj.removeAttr('class');
            $obj.removeAttr('placeholder');
            $div.append($obj);
            $this.append($div);
            if (i == 0) {
                $obj.lrselect(dfop);
            }
            else {
                dfop.url = "";
                dfop.parentId = "";
                $obj.lrselect(dfop);
                _obj[i - 1].on('change', function () {
                    var _value = $(this).lrselectGet();
                    if (_value == "") {
                        $obj.lrselectRefresh({
                            url: '',
                            param: { parentId: _value },
                            data:[]
                        });
                    }
                    else {
                        $obj.lrselectRefresh({
                            url: top.$.rootUrl + '/LR_SystemModule/Area/Getlist',
                            param: { parentId: _value },
                        });
                    }
                  
                });
            }
            i++;
            _obj.push($obj);
        });
    };
    // 数据库选择
    $.fn.lrDbSelect = function (op) {
        // op:maxHeight 200,
        var dfop = {
            type: 'tree',
            // 是否允许搜索
            allowSearch: true,
            // 访问数据接口地址
            url: top.$.rootUrl + '/LR_SystemModule/DatabaseLink/GetTreeList'
        }
        op = op || {};

        return $(this).lrselect(dfop);
    };

    // 动态获取和设置radio，checkbox
    $.fn.lrRadioCheckbox = function (op) {
        var dfop = {
            type: 'radio',        // checkbox
            dataType: 'dataItem', // 默认是数据字典 dataSource（数据源）
            code: '',
            text: 'F_ItemName',
            value: 'F_ItemValue'
        };
        $.extend(dfop, op || {});
        var $this = $(this);
        $this.addClass(dfop.type);
        $this.addClass('lr-' + dfop.type);
        $this.attr('type', 'lr-' + dfop.type);
        var thisId = $this.attr('id');

        if (dfop.dataType == 'dataItem') {
            learun.clientdata.getAllAsync('dataItem', {
                code: dfop.code,
                callback: function (dataes) {
                    $.each(dataes, function (id, item) {
                        var $point = $('<label><input name="' + thisId + '" value="' + item.value + '"' + ' type="' + dfop.type + '">' + item.text + '</label>');
                        $this.append($point);
                    });
                    $this.find('input').eq(0).trigger('click');
                }
            });
        }
        else {
            learun.clientdata.getAllAsync('sourceData', {
                code: dfop.code,
                callback: function (dataes) {
                    $.each(dataes, function (id, item) {
                        var $point = $('<label><input name="' + thisId + '" value="' + item[dfop.value] + '"' + '" type="' + dfop.type + '">' + item[dfop.text] + '</label>');
                        $this.append($point);
                    });
                    $this.find('input').eq(0).trigger('click');
                }
            });
        }
    };
    // 多条件查询框
    $.fn.lrMultipleQuery = function (search, height, width) {
        var $this = $(this);
        var contentHtml = $this.html();
        $this.addClass('lr-query-wrap');


        var _html = '';
        _html += '<div class="lr-query-btn"><i class="fa fa-search"></i>&nbsp;多条件查询</div>';
        _html += '<div class="lr-query-content">';
        //_html += '<div class="lr-query-formcontent">';
        _html += contentHtml;
        //_html += '</div>';
        _html += '<div class="lr-query-arrow"><div class="lr-query-inside"></div></div>';
        _html += '<div class="lr-query-content-bottom">';
        _html += '<a id="lr_btn_queryReset" class="btn btn-default">&nbsp;重&nbsp;&nbsp;置</a>';
        _html += '<a id="lr_btn_querySearch" class="btn btn-primary">&nbsp;查&nbsp;&nbsp;询</a>';
        _html += '</div>';
        _html += '</div>';
        $this.html(_html);
        $this.find('.lr-query-formcontent').show();

        $this.find('.lr-query-content').css({ 'width': width || 400, 'height': height || 300 });

        $this.find('.lr-query-btn').on('click', function () {
            var $content = $this.find('.lr-query-content');
            if ($content.hasClass('active')) {
                $content.removeClass('active');
            }
            else {
                $content.addClass('active');
            }
        });

        $this.find('#lr_btn_querySearch').on('click', function () {
            var $content = $this.find('.lr-query-content');
            var query = $content.lrGetFormData();
            $content.removeClass('active');
            search(query);
        });

        $this.find('#lr_btn_queryReset').on('click', function () {
            var $content = $this.find('.lr-query-content');
            var query = $content.lrGetFormData();
            for (var id in query) {
                query[id] = "";
            }
            $content.lrSetFormData(query);
        });

        $(document).click(function (e) {
            var et = e.target || e.srcElement;
            var $et = $(et);
            if (!$et.hasClass('lr-query-wrap') && $et.parents('.lr-query-wrap').length <= 0) {

                $('.lr-query-content').removeClass('active');
            }
        });
    };

})(jQuery, top.learun);