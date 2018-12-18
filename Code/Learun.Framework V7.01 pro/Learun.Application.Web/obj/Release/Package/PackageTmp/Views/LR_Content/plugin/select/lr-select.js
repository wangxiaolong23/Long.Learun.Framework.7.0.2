/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力 软信息技术有限公司
 * 创建人：力 软-前端开发组
 * 日 期：2017.03.22
 * 描 述：learunSelect（普通，多选，树形数据，gird，搜索，输入框选择器）-渲染数据在点击的时候触发，考虑到在一个表单上有超级多的下拉框的绑定情况（这里需要考虑赋值的特殊性）
 */
(function ($, learun) {
    "use strict";

    $(function () {
        $(document).click(function (e) {
            e = e || Window.event;
            var et = e.target || e.srcElement;
            if (et.tagName != 'BODY') {
                $('.lr-select-option').slideUp(150);
                $('.lr-select').removeClass('lr-select-focus');
            }
          
        });
    });

    $.lrselect = {
        htmlToData: function ($self) {
            var dfop = $self[0]._lrselect.dfop;
            var $ul = $self.find('ul');
            dfop.data = [];
            $ul.find('li').each(function () {
                var $li = $(this);
                var point = { id: $li.attr('data-value'), text: $li.html() };
                dfop.data.push(point);
            });
            $ul.remove();
            $ul = null;
            dfop = null;
        },
        calc: function ($this, op) { // 计算高度和方向
            var bodyHeight = 0;
            var top = 0;

            bodyHeight = $('body').height();
            top = $this.offset().top;
            var topH = top - 1;
            var bottomH = bodyHeight - top - 30;
            var selctH = 2;
            // 计算选择框的高度
            if (op.allowSearch) {
                selctH += 30;
            }
            selctH += op.data.length * 26;
            if (op.placeholder) {
                selctH += 25;
            }

            if ((op.type === 'tree' || op.type === 'treemultiple') && op.data.length > 1) {
                selctH = 200;
            }

            selctH = op.height || selctH;

            var res = {
                type: 0, // 0 向下 1 向上
                height: 0
            };

            if (bottomH > 130 || bottomH > topH || bottomH > selctH) { // 如果能够显示四条数据和搜索框就采用下拉方式
                res.height = bottomH > selctH ? selctH : bottomH;
            } else {
                res.type = 1;
                res.height = topH > selctH ? selctH : topH;
            }

            return res;
        },
        initRender: function (dfop, $self) {
            $('#learun_select_option_' + dfop.id).remove();
            var $option = $('<div class="lr-select-option" id="learun_select_option_' + dfop.id + '"></div>');

            var $optionContent = $('<div class="lr-select-option-content"></div>');
            var $ul = $('<ul id="learun_select_option_content' + dfop.id + '"></ul>');
            //$optionContent.css('max-height', dfop.maxHeight + 'px');
            $option.hide();
            $optionContent.html($ul);
            $option.prepend($optionContent);
            if (dfop.allowSearch) {
                var $search = $('<div class="lr-select-option-search"><input type="text" placeholder="搜索关键字"><span class="input-query" title="查询"><i class="fa fa-search"></i></span></div>');
                $option.append($search);
                $option.css('padding-bottom', '25px');
                $search.on('click', function () { return false; });
                $search.find('input').on("keypress", function (e) {
                    e = e || window.event;
                    if (e.keyCode === 13) {
                        var $this = $(this);
                        var keyword = $this.val();
                        var $option = $this.parents('.lr-select-option');
                        var dfop = $option[0].dfop;
                        if (dfop.type === "tree" || dfop.type === "treemultiple") {
                            var $optionContent = $this.parent().prev();
                            $optionContent.lrtreeSet('search', { keyword: keyword });
                        }
                        else if (dfop.type === "default" || dfop.type === "multiple") {
                            for (var i = 0, l = dfop.data.length; i < l; i++) {
                                var _item = dfop.data[i];
                                if (!keyword || _item[dfop.text].indexOf(keyword) != -1) {
                                    _item._lrhide = false;
                                }
                                else {
                                    _item._lrhide = true;
                                }
                            }
                            $.lrselect.render(dfop);
                        }

                        $option = null;
                        
                    }
                });
            }
            $('body').append($option);
            $option.on('click', $.lrselect.itemClick);
            $option[0].dfop = dfop;
            $self.append('<div class="lr-select-placeholder" >==' + dfop.placeholder + '==</div>');
            $self.attr('type', 'lrselect').addClass('lr-select');

            if (dfop.type != 'tree') {
                $optionContent.lrscroll();
            }
        },
        render: function (dfop) {
            switch (dfop.type) {
                case 'default':
                    $.lrselect.defaultRender(dfop);
                    break;
                case 'tree':
                case 'treemultiple':
                    $.lrselect.treeRender(dfop);
                    break;
                case 'gird':
                    break;
                case 'multiple':
                    $.lrselect.multipleRender(dfop);
                    break;
                default:
                    break;
            }
            dfop.isrender = true;
            
        },
        defaultRender: function (dfop) {
            var $ul = $('#learun_select_option_content' + dfop.id);
            $ul.html("");
            if (dfop.placeholder) {
                $ul.append('<li data-value="-1" class="lr-selectitem-li" >==' + dfop.placeholder + '==</li>');
            }
            for (var i = 0, l = dfop.data.length; i < l; i++) {
                var item = dfop.data[i];
                if (!item._lrhide) {
                    var $li = $('<li data-value="' + i + '" class="lr-selectitem-li" >' + item[dfop.text] + '</li>');
                    $ul.append($li);
                }
              
            }
        },
        multipleRender: function (dfop) {
            var $ul = $('#learun_select_option_content' + dfop.id);
            $ul.html("");
            if (dfop.placeholder) {
                $ul.append('<li data-value="-1" class="lr-selectitem-li" >==' + dfop.placeholder + '==</li>');
            }
            for (var i = 0, l = dfop.data.length; i < l; i++) {
                var item = dfop.data[i];
                if (!item._lrhide) {
                    if (!!dfop.multipleMapValue && dfop.multipleMapValue[i] != undefined) {
                        var $li = $('<li data-value="' + i + '" class="lr-selectitem-li" ><img class="lr-select-node-cb" src="/Content/images/learuntree/checkbox_1.png">' + item[dfop.text] + '</li>');
                        $ul.append($li);
                    }
                    else {
                        var $li = $('<li data-value="' + i + '" class="lr-selectitem-li" ><img class="lr-select-node-cb" src="/Content/images/learuntree/checkbox_0.png">' + item[dfop.text] + '</li>');
                        $ul.append($li);
                    }
                }
            }
        },
        treeRender: function (dfop) {
            var $option = $('#learun_select_option_' + dfop.id);
            $option.find('.lr-select-option-content').remove();
            var $optionContent = $('<div class="lr-select-option-content"></div>');
            $option.prepend($optionContent);
            //$optionContent.css('max-height', dfop.maxHeight + 'px');
            dfop.data.unshift({
                "id": "-1",
                "text": '==' + dfop.placeholder + '==',
                "value": "-1",
                "icon": "-1",
                "parentnodes": "0",
                "showcheck": false,
                "isexpand": false,
                "complete": true,
                "hasChildren": false,
                "ChildNodes": []
            });
            var treeop = {
                data: dfop.data,
                nodeClick: $.lrselect.treeNodeClick
            };
            if (dfop.type === 'treemultiple') {
                treeop.nodeClick = $.lrselect.treeNodeClick2;
                treeop.nodeCheck = $.lrselect.treeNodeCheck;
            }
            $optionContent.lrtree(treeop);
        },
        bindEvent: function ($self) {
            $self.unbind('click');
            $self.on('click', $.lrselect.click);
        },
        click: function (e) {
            var $this = $(this);
            if ($this.attr('readonly') == 'readonly' || $this.attr('disabled') == 'disabled') {
                return false;
            }
            var dfop = $this[0]._lrselect.dfop;
            if (!dfop.isload) {
                return false;
            }
            if (!dfop.isrender) {
                $.lrselect.render(dfop);
            }

            // 选中下拉框的某一项
            e = e || Window.event;
            var et = e.target || e.srcElement;
            var $et = $(et);

            var $option = $('#learun_select_option_' + dfop.id);
            if ($option.is(":hidden")) {
                $('.lr-select').removeClass('lr-select-focus');
                $('.lr-select-option').slideUp(150);

                $this.addClass('lr-select-focus');
                var width = dfop.width || $this.parent().width();//+ (dfop.diffWidth || 0);
                var height = $this.innerHeight();
                var top = $this.offset().top;
                var left = $this.offset().left;
                var res = $.lrselect.calc($this, dfop);

                if (res.type == 0) {
                    $option.css({ 'width': width, 'top': top + height + 2, 'left': left, 'height': res.height }).show();
                }
                else {
                    $option.css({ 'width': width, 'top': top - res.height - 2, 'left': left, 'height': res.height }).show();
                }
                $option.find('.lr-select-option-search').find('input').select();

                if (dfop.type != 'multiple') {
                    $option.find('.selected').removeClass('selected');
                    if (dfop._index != -1) {
                        $option.find('.lr-selectitem-li[data-value="' + dfop._index + '"]').addClass('selected');
                    }
                }

            }
            else {
                $option.slideUp(150);
                $this.removeClass('lr-select-focus');
            }
          
            dfop = null;
            e.stopPropagation();
        },
        itemClick: function (e) {
            // 选中下拉框的某一项
            e = e || Window.event;
            var et = e.target || e.srcElement;
            var $et = $(et);
            var $option = $(this);
            var dfop = $option[0].dfop;
            var $this = $('#' + dfop.id);
            if (dfop.type != 'multiple') {
                if ($et.hasClass('lr-selectitem-li')) {
                    var _index = $et.attr('data-value');
                    $option.find('.selected').removeClass('selected');
                    $et.addClass('selected');
                    if (dfop._index != _index) {
                        var $inputText = $this.find('.lr-select-placeholder');

                        if (_index == -1) {
                            $inputText.css('color', '#999');
                            $inputText.html('==' + dfop.placeholder + '==');
                        }
                        else {
                            $inputText.css('color', '#000');
                            $inputText.html(dfop.data[_index][dfop.text]);
                        }
                        dfop._index = _index;

                        $this.trigger("change");
                        if (dfop.select) {
                            dfop.select(dfop.data[_index]);
                        }
                    }
                    $option.slideUp(150);
                    $this.removeClass('lr-select-focus');
                }
               
            }
            else {
                if ($et.hasClass('lr-selectitem-li') || $et.hasClass('lr-select-node-cb')) {
                    var $inputText = $this.find('.lr-select-placeholder');
                    var $cbobj = $et.find('.lr-select-node-cb');
                    var _index = $et.attr('data-value');
                    if ($et.hasClass('lr-select-node-cb')) {
                        $cbobj = $et;
                        _index = $et.parent().attr('data-value');
                    }

                    dfop.multipleMapValue = dfop.multipleMapValue || {};
                    dfop.multipleValue = dfop.multipleValue || [];
                    dfop.multipleText = dfop.multipleText || [];

                    if (_index == -1) {
                        $inputText.css('color', '#999');
                        $inputText.html('==' + dfop.placeholder + '==');
                        dfop.multipleMapValue = {};
                        dfop.multipleValue = [];
                        dfop.multipleText = [];

                        $option.find('.lr-select-node-cb[src$="checkbox_1.png"]').attr('src', '/Content/images/learuntree/checkbox_0.png');
                        $option.slideUp(150);
                        $this.removeClass('lr-select-focus');
                    }
                    else {
                        var selected = true;
                        if (dfop.multipleMapValue[_index] == undefined) {
                            $inputText.css('color', '#000');
                            dfop.multipleValue.push(dfop.data[_index][dfop.value]);
                            dfop.multipleText.push(dfop.data[_index][dfop.text]);

                            dfop.multipleMapValue[_index] = dfop.data[_index];
                            $inputText.html(String(dfop.multipleText));

                            $cbobj.attr('src', '/Content/images/learuntree/checkbox_1.png');
                        }
                        else {
                            dfop.multipleValue = [];
                            dfop.multipleText = [];
                            delete dfop.multipleMapValue[_index];
                            $.each(dfop.multipleMapValue, function (_id, _item) {
                                dfop.multipleValue.push(_item[dfop.value]);
                                dfop.multipleText.push(_item[dfop.text]);
                            });
                            if (dfop.multipleText.length == 0) {
                                $inputText.css('color', '#999');
                                $inputText.html('==' + dfop.placeholder + '==');
                            }
                            else {
                                $inputText.html(String(dfop.multipleText));
                            }
                            selected = false;
                            $cbobj.attr('src', '/Content/images/learuntree/checkbox_0.png');
                        }

                        $this.trigger("change");
                        if (dfop.select) {
                            dfop.select(dfop.data[_index], selected, $this);
                        }
                    }
                }
            }
            e.stopPropagation();
        },
        treeNodeClick: function (item, $item) {
            var $option = $item.parents('.lr-select-option');
            var dfop = $option[0].dfop;
            $option.slideUp(150);
            var $select = $('#' + dfop.id);
            $select.removeClass('lr-select-focus');
            dfop.currtentItem = item;
            var $inputText = $select.find('.lr-select-placeholder');
            $inputText.html(dfop.currtentItem.text);
            if (item.value == '-1') {
                $inputText.css('color', '#999');
            }
            else {
                $inputText.css('color', '#000');
            }
            $select.trigger("change");
            if (dfop.select) {
                dfop.select(dfop.currtentItem);
            }

            $option = null;
            $select = null;
        },
        treeNodeClick2: function (item, $item) {
            var $tree = $item.parents('.lr-select-option-content');
            var $option = $item.parents('.lr-select-option');
            var dfop = $option[0].dfop;
            var $select = $('#' + dfop.id);
          
            $select.removeClass('lr-select-focus');
            dfop.currtentItems = [];
            if (item.value == '-1') {
                $item.parents('.lr-select-option').slideUp(150);
                $tree.lrtreeSet('allNoCheck');
                var $inputText = $select.find('.lr-select-placeholder');
                $inputText.html(item.text);
                $inputText.css('color', '#999');
                $select.trigger("change");
                if (dfop.select) {
                    dfop.select([]);
                }
            }
            $tree = null;
            $option = null;
            $select = null;
        },
        treeNodeCheck: function (item, $item) {
            var $tree = $item.parents('.lr-select-option-content');
            var $option = $item.parents('.lr-select-option');
            var dfop = $option[0].dfop;
            var $select = $('#' + dfop.id);
            var $inputText = $select.find('.lr-select-placeholder');
            $select.removeClass('lr-select-focus');
            var data = $tree.lrtreeSet('getCheckNodesEx');
            dfop.currtentItems = data;
            var text = "";
            for (var i = 0, l = data.length; i < l; i++) {
                var one = data[i];
                if (text != "") {
                    text += ",";
                }
                text += one.text;
            }
            if (text == "") {
                $inputText.html("==" + dfop.placeholder + "==");
                $inputText.css('color', '#999');
            }
            else {
                $inputText.text(text);
                $inputText.css('color', '#000');
            }
            $select.trigger("change");
            if (dfop.select) {
                dfop.select(dfop.currtentItems);
            } 
            $tree = null;
            $option = null;
            $select = null;
            $inputText = null;

        },
        defaultValue: function ($self, type) {
            var dfop = $self[0]._lrselect.dfop;
            dfop.currtentItem = null;
            dfop._index = -1;
            var $inputText = $self.find('.lr-select-placeholder');
            $inputText.css('color', '#999');
            $inputText.html('==' + dfop.placeholder + '==');

            $('#' + dfop.id + ' .lr-select-option .selected').removeClass('selected');
            dfop.select && dfop.select(null, type);
            $self.trigger("change");
        }
    };


    $.fn.lrselect = function (op) {
        var dfop = {
            // 请选择
            placeholder: "请选择",
            // 类型
            type: 'default',// default,tree,treemultiple,gird,multiple
            // 字段
            value: "id",
            text: "text",
            title: "title",
            // 宽度
            width: false,
            // 是否允许搜索
            allowSearch: false,
            // 访问数据接口地址
            url: false,
            data: false,
            // 访问数据接口参数
            param: null,
            // 接口请求的方法
            method: "GET",

            //选择事件
            select: false,
            
            isload: false, // 数据是否加载完成
            isrender: false// 选项是否渲染完成
        };
        $.extend(dfop, op || {});
        var $self = $(this);
        if ($self.length == 0) {
            return $self;
        }

        dfop.id = $self.attr('id');

        if (!dfop.id) {
            return false;
        }
        if ($self[0]._lrselect) {
            return $self;
        }

        $self[0]._lrselect = { dfop: dfop };
        // 基础信息渲染
        $.lrselect.bindEvent($self);
        
        // 数据获取方式有三种：url,data,html
        // url优先级最高
        if (dfop.url) {
            learun.httpAsync(dfop.method, dfop.url, dfop.param, function (data) {
                $self[0]._lrselect.dfop.data = data || [];
                $self[0]._lrselect.dfop.backdata = data || [];
                dfop.isload = true;
            });
        }
        else if (dfop.data) {
            dfop.isload = true;
            dfop.backdata = dfop.data;
        }
        else {// 最后是html方式获取（只适合数据比较少的情况）
            $.lrselect.htmlToData($self);
            dfop.isload = true;
            dfop.backdata = dfop.data;
        }
        $.lrselect.initRender(dfop, $self);
        return $self;
        
    };

    $.fn.lrselectRefresh = function (op) {
        var $self = $(this);
        if ($self.length == 0) {
            return $self;
        }
        if (!$self[0]._lrselect) {
            return false;
        }
        var dfop = $self[0]._lrselect.dfop;
        if (!dfop) {
            return false;
        }
        $.extend(dfop, op || {});

        dfop.isload = false;
        dfop.isrender = false;
        if (dfop.url) {
            learun.httpAsync(dfop.method, dfop.url, dfop.param, function (data) {
                $self[0]._lrselect.dfop.data = data || [];
                $self[0]._lrselect.dfop.backdata = data || [];
                dfop.isload = true;
            });
        }
        else if (dfop.data) {
            dfop.isload = true;
            dfop.backdata = dfop.data;
        }

        if (dfop._setValue != null && dfop._setValue != undefined) {
            $self.lrselectSet(dfop._setValue);
        }
        else {
            $.lrselect.defaultValue($self, 'refresh');
        }
        return $self;
    };

    
    $.fn.lrselectGet = function () {
        var $this = $(this);
        if ($this.length == 0) {
            return $this;
        }
        var dfop = $this[0]._lrselect.dfop;
        var value = '';
        switch (dfop.type) {
            case 'default':
                if (dfop.data[dfop._index]) {
                    value = dfop.data[dfop._index][dfop.value];
                }
                break;
            case 'tree':
                if (dfop.currtentItem) {
                    value = dfop.currtentItem[dfop.value];
                }
                break;
            case 'treemultiple':
                if (dfop.currtentItems) {
                    for (var i = 0, l = dfop.currtentItems.length; i < l; i++) {
                        if (value != "") {
                            value += ",";
                        }
                        value += dfop.currtentItems[i][dfop.value];
                    }
                }
                break;
            case 'gird':
                break;
            case 'multiple':
                dfop.multipleValue = dfop.multipleValue || [];
                return String(dfop.multipleValue);
            default:
                break;
        }
        return value;
    };

    $.fn.lrselectSet = function (value) {
        // 设置数据的值
        var $this = $(this);
        if ($this.length == 0) {
            return $this;
        }
        if (!$this[0]._lrselect) {
            return $this;
        }
        value = value + '';
        if (value == '' || value == 'undefined' || value == 'null') {
            $.lrselect.defaultValue($this);
            return $this;
        }
        var dfop = $this[0]._lrselect.dfop;
        dfop._setValue = null;
        if (!dfop) {
            return $this;
        }
        $('#' + dfop.id + ' .lr-select-option .selected').removeClass('selected');

        function _fn(dfop) {
            if (dfop.isload == false) {
                setTimeout(function () {
                    _fn(dfop);
                }, 100);
            }
            else if (dfop.isload == true) {
                var _currtentItem;
                switch (dfop.type) {
                    case 'default':
                        for (var i = 0, l = dfop.data.length; i < l; i++) {
                            if (dfop.data[i][dfop.value] == value) {
                                dfop._index = i;
                                _currtentItem = dfop.data[i];
                                break;
                            }
                        }
                        break;
                    case 'tree':
                        _currtentItem = $.lrtree.findItem(dfop.data, dfop.value, value);
                        dfop.currtentItem = _currtentItem;
                        break;
                    case 'treemultiple':
                        $.lrselect.render(dfop);
                        $this.find('.lr-select-option-content').lrtreeSet('setCheck', value.split(','));
                        return false;
                    case 'gird':
                        break;
                    case 'multiple':
                        dfop.multipleMapValue = {};
                        dfop.multipleValue = [];
                        dfop.multipleText = [];
                        if (dfop.isrender) {
                            $this.find('.lr-select-node-cb[src$="checkbox_1.png"]').attr('src', '/Content/images/learuntree/checkbox_0.png');
                        }
                        var _valuellist = value.split(',');
                        for (var i = 0, l = dfop.data.length; i < l; i++) {
                            var _arrayIndex = $.inArray(dfop.data[i][dfop.value] + '', _valuellist);
                            
                            if (_arrayIndex != -1) {
                                dfop.multipleMapValue[i] = dfop.data[i];
                                dfop.multipleValue.push(dfop.data[i][dfop.value]);
                                dfop.multipleText.push(dfop.data[i][dfop.text]);

                                if (dfop.isrender) {
                                    $this.find('[data-value="' + i + '"] .lr-select-node-cb').attr('src', '/Content/images/learuntree/checkbox_1.png');
                                }
                                if (dfop.select) {
                                    dfop.select(dfop.data[i], true, $this);
                                }
                            }
                        }

                        if (dfop.multipleText.length > 0) {
                            _currtentItem = dfop.multipleText;
                        }
                        break;
                    default:
                        break;
                }
               

                if (_currtentItem) {
                    if (dfop.type == 'multiple') {
                        var $inputText = $this.find('.lr-select-placeholder');
                        if (dfop.multipleText.length > 0) {
                            $inputText.css('color', '#000');
                        }
                        else {
                            $inputText.css('color', '#999');
                        }
                        $inputText.html(String(dfop.multipleText));
                        $this.trigger("change");
                    } else {
                        var $inputText = $this.find('.lr-select-placeholder');
                        $inputText.html(_currtentItem[dfop.text]);
                        $inputText.css('color', '#000');
                        $this.trigger("change");
                        if (dfop.select) {
                            dfop.select(_currtentItem);
                        }
                    }
                }
                else {
                    dfop._setValue = value;
                }
            }
        }
        _fn(dfop);


        return $this;
    };

    $.fn.lrselectGetEx = function () {
        var $this = $(this);
        if ($this.length == 0) {
            return $this;
        }
        var dfop = $this[0]._lrselect.dfop;
        var item = null;
        switch (dfop.type) {
            case 'default':
                if (dfop.data[dfop._index]) {
                    item = dfop.data[dfop._index];
                }
                break;
            case 'tree':
                if (dfop.currtentItem) {
                    item = dfop.currtentItem;
                }
                break;
            case 'treemultiple':
                if (dfop.currtentItems) {
                    item = dfop.currtentItems;
                }
                break;
            case 'gird':
                break;
            case 'multiple':
                item = dfop.multipleValue || [];
                break;
            default:
                break;
        }
        return item;
    };

})(jQuery, top.learun);