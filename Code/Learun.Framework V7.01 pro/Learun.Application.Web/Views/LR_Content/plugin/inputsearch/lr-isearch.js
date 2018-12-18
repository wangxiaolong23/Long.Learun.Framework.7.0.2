/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.03.22
 * 描 述：learunISearch 输入搜索框，支持input输入框，数据异步加载，本地查询
 */
(function ($, learun) {
    "use strict";
    $.lrisearch = {
        init: function ($self) {
            var dfop = $self[0]._lrisearch.dfop;
            $self.parent().append('<div class="lr-isearch-panel"  style="max-height:' + dfop.maxHeight + 'px;" ><ul id="lrisearch_' + dfop.id + '" ></ul></div>');
        },
        bind: function ($self) {
            $self.on('input propertychange', function () {
                var $this = $(this);
                $.lrisearch.triggerSearch($self);
            });
        },
        triggerSearch: function ($self) {
            var dfop = $self[0]._lrisearch.dfop;
            var $showPanel = $('#lrisearch_' + dfop.id);
            $showPanel.parent().hide();
            var _value = $self.val();
            if (_value) {
                if (!dfop._isload) {
                    dfop._isSearchneed = true;
                }
                else {
                    dfop._first = true;
                    dfop._value = _value;
                    dfop._begin = 0;
                    dfop._end = 100 > dfop.data.length ? dfop.data.length : 100;
                    if (dfop._isSearched) {
                        dfop._isSearched = false;
                        setTimeout(function () {
                            $.lrisearch.search($self);
                        });
                    }
                }
            }
            else {
                dfop._isSearchneed = false;
                $showPanel.html("");
            }
        },

        search: function ($self) {// 每次搜索100条
            var dfop = $self[0]._lrisearch.dfop;
            var value = dfop._value;
            var begin = dfop._begin;
            var end = dfop._end;
            var data = dfop.data;

            for (var i = begin; i < end; i++) {
                var _item = data[i];
                if (item[dfop.text].indexOf(value) != -1) {
                    $.lrisearch.renderNone($self, item[dfop.text]);
                }
            }

            if (end < data.length) {
                dfop._begin = end;
                dfop._end = end + 100;
                if (dfop._end > data.length) {
                    dfop._end = data.length;
                }
                setTimeout(function () {
                    $.lrisearch.search($self);
                });
            }
            else {
                dfop._isSearched = true;
            }
        },
        renderNone: function ($self, text) {// 刷新一条数据
            var dfop = $self[0]._lrisearch.dfop;
            var $showPanel = $('#lrisearch_' + dfop.id);
            if (dfop._first) {
                dfop._first = false;
                $showPanel.html("");
                $showPanel.parent().show();
            }
            $showPanel.append('<li>' + text + '</li>');
        }
    };


    $.fn.lrisearch = function (op) {
        var dfop = {
            // 展开最大高度
            maxHeight: 200,
            // 字段
            text: "text",

            method: "GET",
            url: '',
            data: [],
            // 访问数据接口参数
            param: null,

            _isload: false,
            _isSearched: false,
            _first: false,
            _isSearchneed: false
        };
        $.extend(dfop, op || {});
        var $self = $(this);
        dfop.id = $self.attr('id');
        if (!dfop.id) {
            return false;
        }
        $self[0]._lrisearch = {dfop:dfop};

        $.lrisearch.init($self);
        //加载数据
        if (!!dfop.url) {
            learun.httpAsync(dfop.method, dfop.url, dfop.param, function (data) {
                $self[0]._lrisearch.dfop.data = data || [];
                dfop.isload = true;
                if (dfop._isSearchneed) {
                    $.lrisearch.triggerSearch($self);// 触发查询函数
                }
                
            });
        }
        else {
            dfop.isload = true;
        }
        return $self;
    }


})(jQuery, top.learun);