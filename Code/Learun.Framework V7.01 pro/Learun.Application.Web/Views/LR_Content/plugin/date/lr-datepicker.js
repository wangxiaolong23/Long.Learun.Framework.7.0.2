/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.03.22
 * 描 述：lrdate 时间段选择器 @YY@-当年 @MM@-当月 @DD@-当天 @JJ@-当季度
 */
(function ($, learun) {
    "use strict";
    $.lrdate = {
        init: function ($self) {
            var dfop = $self[0]._lrdate.dfop;
            $self.html('');
            $self.addClass('lr-search-date');
            var $text = $('<div class="lr-search-date-text" id="lr_search_date_' + dfop.id + '" ></div>');
            var $container = $('<div class="lr-search-date-container" id="lr_search_date_container_' + dfop.id + '"><div class="lr-search-date-arrow"><div class="lr-search-date-inside"></div></div></div>');

            var $btnlist = $('<div class="lr-search-date-content-btns" id="lr_search_date_content_btns_' + dfop.id + '"></div>');
            var $customDate = $('<div class="lr-search-date-btn-block"><a href="javascript:;" data-value="customDate">自定义</a></div>');

            var $clearDate = $('<div class="lr-search-date-btn-block"><a href="javascript:;" data-value="clearDate">清空</a></div>');
            $btnlist.append($customDate);
            $btnlist.append($clearDate);
            $container.append($btnlist);

            var $datepickerContent = $('<div class="lr-search-date-datepicker-content"></div>');

            var $datepicker1 = $('<div class="lr-search-date-datepicker-container first" id="lr_search_date_datepicker1_' + dfop.id + '"  ></div>');
            var $datepicker2 = $('<div class="lr-search-date-datepicker-container" id="lr_search_date_datepicker2_' + dfop.id + '"  ></div>');
            var $datepickerBtn = $('<div class="lr-search-date-datepicker-btn"><a class="btn btn-primary">确定</a></div>');
            $datepickerContent.append($datepicker1);
            $datepickerContent.append($datepicker2);
            $datepickerContent.append($datepickerBtn);

            $container.append($datepickerContent);

            $self.append($text);
            $self.append($container);

          

            WdatePicker({ eCont: 'lr_search_date_datepicker1_' + dfop.id, onpicked: function (dp) { dfop._begindate = dp.cal.getDateStr() + " 00:00:00"; }, minDate: dfop.minDate, maxDate: dfop.maxDate });// 开始时间
            WdatePicker({ eCont: 'lr_search_date_datepicker2_' + dfop.id, onpicked: function (dp) { dfop._enddate = dp.cal.getDateStr() + " 23:59:59"; }, minDate: dfop.minDate, maxDate: dfop.maxDate });// 结束时间


            /*事件的绑定*/
            $text.on('click', function (e) {
                var $this = $(this);
                var $self = $this.parents('.lr-search-date');
                var dfop = $self[0]._lrdate.dfop;
                var $container =$self.find('#lr_search_date_container_' + dfop.id);
                if ($container.is(':hidden')) {
                    $container.show();
                }
                else {
                    $container.hide();
                }
            });
            $(document).click(function (e) {
                var et = e.target || e.srcElement;
                var $et = $(et);
                if (!$et.hasClass('lr-search-date') && $et.parents('.lr-search-date').length <= 0) {
                    $('.lr-search-date-container').hide();
                }
            });

            $customDate.find('a').on('click', function (e) {
                var $this = $(this);
                var $self = $this.parents('.lr-search-date');
                var dfop = $self[0]._lrdate.dfop;

                $self.find('.lr-search-date-content-btns a.active').removeClass('active');
                $('#lr_search_date_container_' + dfop.id).addClass('width');
                $this.addClass('active');
                $self.find('.lr-search-date-datepicker-content').show();

            });
            $clearDate.find('a').on('click', function (e) {
                var $this = $(this);
                var $self = $this.parents('.lr-search-date');
                var dfop = $self[0]._lrdate.dfop;
                var $container = $self.find('#lr_search_date_container_' + dfop.id);
                var $text = $self.find('#lr_search_date_' + dfop.id);
                $container.hide();
                $self.find('.lr-search-date-content-btns a.active').removeClass('active');
                $text.html("");

                if (!!dfop.selectfn) {
                    dfop.selectfn("0001-01-01", "3000-01-01");
                }

            });

            // 时间确定按钮
            $datepickerBtn.find('a').on('click', function () {
                var $self = $(this).parents('.lr-search-date');
                var dfop = $self[0]._lrdate.dfop;
                var $container = $self.find('#lr_search_date_container_' + dfop.id);
                var $text = $self.find('#lr_search_date_' + dfop.id);
                $container.hide();
                var label = learun.formatDate(dfop._begindate, 'yyyy-MM-dd') + '~' + learun.formatDate(dfop._enddate, 'yyyy-MM-dd');
                $text.html(label);

                if (!!dfop.selectfn) {
                    dfop.selectfn(dfop._begindate, dfop._enddate);
                }
            });
        },
        monthinit: function ($self) {// 月：上月，本月
            var dfop = $self[0]._lrdate.dfop;
            var $btnlist = $('#lr_search_date_content_btns_' + dfop.id);
            var $block = $('<div class="lr-search-date-btn-block"></div>');
            if (dfop.premShow) {
                $block.append('<a href="javascript:;" class="datebtn" data-value="preM">上月</a>');
            }
            if (dfop.mShow) {
                $block.append('<a href="javascript:;" class="datebtn" data-value="currentM">本月</a>');
            }
            $btnlist.prepend($block);
            dfop = null;
        },
        jinit: function ($self) {// 季度
            var dfop = $self[0]._lrdate.dfop;
            var $btnlist = $('#lr_search_date_content_btns_' + dfop.id);
            var $block = $('<div class="lr-search-date-btn-block"></div>');
            if (dfop.prejShow) {
                $block.append('<a href="javascript:;" class="datebtn" data-value="preJ">上季度</a>');
            }
            if (dfop.jShow) {
                $block.append('<a href="javascript:;" class="datebtn" data-value="currentJ">本季度</a>');
            }
            $btnlist.prepend($block);
            dfop = null;
        },
        yinit: function ($self) {
            var dfop = $self[0]._lrdate.dfop;
            var $btnlist = $('#lr_search_date_content_btns_' + dfop.id);
            var $block = $('<div class="lr-search-date-btn-block"></div>');
            if (dfop.ysShow) {
                $block.append('<a href="javascript:;" class="datebtn" data-value="yS">上半年</a>');
            }
            if (dfop.yxShow) {
                $block.append('<a href="javascript:;" class="datebtn" data-value="yX">下半年</a>');
            }
            if (dfop.preyShow) {
                $block.append('<a href="javascript:;" class="datebtn" data-value="preY">去年</a>');
            }
            if (dfop.yShow) {
                $block.append('<a href="javascript:;" class="datebtn" data-value="currentY">今年</a>');
            }
            $btnlist.prepend($block);
            dfop = null;
        },
        custmerinit: function ($self) {
            var dfop = $self[0]._lrdate.dfop;
            var $btnlist = $('#lr_search_date_content_btns_' + dfop.id);
            var $block = $('<div class="lr-search-date-btn-block"></div>');
            
            for (var i = 0, l = dfop.dfdata.length; i < l; i++) {
                var item = dfop.dfdata[i];
                $block.append('<a href="javascript:;" class="datebtn" data-value="' + i + '">' + item.name + '</a>');
            }

            $btnlist.prepend($block);
            dfop = null;
        },
        bindEvent: function ($self) {
            $self.find('.datebtn').on('click', function () {
                var $this = $(this);
                var $self = $this.parents('.lr-search-date');
                var value = $this.attr('data-value');
                $.lrdate.select($self, value);
            });
        },
        select: function ($self, value) {
            var dfop = $self[0]._lrdate.dfop;
            var $container = $self.find('#lr_search_date_container_' + dfop.id);
            var $text = $self.find('#lr_search_date_' + dfop.id);
            var $btnlist = $('#lr_search_date_content_btns_' + dfop.id);
            $btnlist.find('.active').removeClass('active');
            var $this = $btnlist.find('.datebtn[data-value="' + value + '"]').addClass('active');
            switch (value) {
                case 'preM':
                    var d = learun.getPreMonth();
                    dfop._begindate = d.begin;
                    dfop._enddate = d.end;
                    break;
                case 'currentM':
                    var d = learun.getMonth();
                    dfop._begindate = d.begin;
                    dfop._enddate = d.end;
                    break;
                case 'preJ':
                    var d = learun.getPreQuarter();
                    dfop._begindate = d.begin;
                    dfop._enddate = d.end;
                    break;
                case 'currentJ':
                    var d = learun.getCurrentQuarter();
                    dfop._begindate = d.begin;
                    dfop._enddate = d.end;
                    break;
                case 'yS':
                    var d = learun.getFirstHalfYear();
                    dfop._begindate = d.begin;
                    dfop._enddate = d.end;
                    break;
                case 'yX':
                    var d = learun.getSecondHalfYear();
                    dfop._begindate = d.begin;
                    dfop._enddate = d.end;
                    break;
                case 'preY':
                    var d = learun.getPreYear();
                    dfop._begindate = d.begin;
                    dfop._enddate = d.end;
                    break;
                case 'currentY':
                    var d = learun.getYear();
                    dfop._begindate = d.begin;
                    dfop._enddate = d.end;
                    break;
                default:
                    var rowid = parseInt(value);
                    var data = dfop.dfdata[rowid];

                    dfop._begindate = data.begin();
                    dfop._enddate = data.end();
                    break;
            }
            $container.hide();
            var label = learun.formatDate(dfop._begindate, 'yyyy-MM-dd') + '~' + learun.formatDate(dfop._enddate, 'yyyy-MM-dd');
            $text.html(label);
            $('#lr_search_date_container_' + dfop.id).removeClass('width');
            $self.find('.lr-search-date-datepicker-content').hide();
            if (!!dfop.selectfn) {
                dfop.selectfn(dfop._begindate, dfop._enddate);
            }
        }
    };

    $.fn.lrdate = function (op) {
        var dfop = {
            // 自定义数据
            dfdata: [],
            // 月
            mShow: true,
            premShow: true,
            // 季度
            jShow: true,
            prejShow: true,
            // 年
            ysShow: true,
            yxShow: true,
            preyShow: true,
            yShow: true,

            dfvalue: false,//preM,currentM,preJ,currentJ,yS,yX,preY,currentY,
            selectfn: false,

            minDate: '',
            maxDate: '',

            
        };
        $.extend(dfop, op || {});
        var $self = $(this);
        dfop.id = $self.attr('id');
        if (!dfop.id) {
            return false;
        }
        $self[0]._lrdate = { "dfop": dfop };
        $.lrdate.init($self);

        $.lrdate.yinit($self);
        $.lrdate.jinit($self);
        $.lrdate.monthinit($self);

        $.lrdate.custmerinit($self);

        $.lrdate.bindEvent($self);

        if (dfop.dfvalue != false) {
            $.lrdate.select($self, dfop.dfvalue);
        }
        return $self;
    };
})(jQuery, top.learun);