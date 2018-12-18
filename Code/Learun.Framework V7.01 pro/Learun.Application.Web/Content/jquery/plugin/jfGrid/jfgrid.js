/*
*名称：jfGrid
*版本号: V1.0.0 
*日期:2017.03.04
*作者: JFine开发团队
*描述：世界上最好用的表格,您的使用是对我们最大的支持!
*
*coldata:label,name,width,align,sortable,formatter
*/
(function ($) {
    "use strict";

    $.jfGrid = $.jfGrid || {};
    $.extend($.jfGrid, {
        /*绘制表格*/
        render: function ($self) {
            var dfop = $self[0].dfop;
            $self.html('');
            $self.addClass('jfgrid-layout');

            $.jfGrid.renderHead($self);
            $.jfGrid.renderBorder($self);
            $.jfGrid.renderLeft($self);
            $.jfGrid.renderScorllArea($self);
            $.jfGrid.renderPageBarinit($self);

            $.jfGrid.renderFooterrow($self);
            $.jfGrid.renderToolBar($self);

            $self.append('<div class="jfgrid-move-line" id="jfgrid_move_line_' + $self[0].dfop.id + '"  ></div>');// 调整宽度线条
            $self.append('<div class="jfgrid-nodata-img" id="jfgrid_nodata_img_' + $self[0].dfop.id + '"  ></div>');// 调整宽度线条
            $self.append('<div class="jfgrid-loading" id="jfgrid_loading_' + $self[0].dfop.id + '"  ><div class="bg"></div><div class="img">正在拼命加载列表数据…</div></div>');// 调整宽度线条

            // 注册整体事件
            $self.on('mousedown', $.jfGrid.bindmdown);
            $self.on('click', $.jfGrid.bindClick);
            $self.mousemove($.jfGrid.bindmmove);
            $self.on('mouseover', $.jfGrid.bindmover);

            // 加载数据
            if (!!dfop.rowdatas && !dfop.url) {
                if (dfop.isEidt && dfop.rowdatas.length == 0) {
                    dfop.rowdatas.push({});// 
                }
                $.jfGrid.renderData($self);
            }

            $self.css({ 'min-height': dfop.minheight });
            if (dfop.height > 0) {
                $self.css({ 'height': dfop.height });
            }

            dfop = null;
            return;
        },
        renderHead: function ($self) {// 渲染表格头部
            var dfop = $self[0].dfop;
            dfop._headWidth = 0;
            dfop._leftWidth = 0;
            dfop._headHeight = 0;
            dfop._colModel = [];
            dfop._colFrozenModel = [];

            var $head = $('<div class="jfgrid-head" id="jfgrid_head_' + dfop.id + '" ></div>');
            var $border = $('<div class="jfgrid-border" id="jfgrid_border_' + dfop.id + '" ></div>');
            var $borderCol = $('<div class="jfgrid-border-col" id="jfgrid_border_col_' + dfop.id + '" ></div>');

            var len = dfop.headData.length;
            for (var i = 0; i < len; i++) {
                var cell = dfop.headData[i];
                var left = cell.frozen ? dfop._leftWidth : dfop._headWidth;
                cell.height = 28;
                var $cell = $('<div class="jfgrid-head-cell jfgrid-heed-rownum_0" path="' + i + '" >' + (cell.label || "") + '</div>');
                cell.obj = $cell;
                cell.path = i;
                if (!!cell.children) {// 如果有子列
                    if (cell.frozen) {
                        $.jfGrid.renderSubHead($borderCol, cell, dfop, dfop._leftWidth, 1, true, i);
                    }
                    else {
                        $.jfGrid.renderSubHead($head, cell, dfop, dfop._headWidth, 1, false, i);
                    }
                }
                else {
                    $cell.attr('jfgrid-heed-cellrender', 'ok');
                    $cell.append('<div class="jfgrid-heed-sort"><i class="fa fa-caret-up"></i><i class="fa fa-caret-down"></i></div>');
                    $cell.append('<div class="jfgrid-heed-move"></div>');
                    if (cell.frozen) {
                        dfop._colFrozenModel[dfop._colFrozenModel.length] = cell;
                    } else {
                        dfop._colModel[dfop._colModel.length] = cell;
                    }
                    cell.type = 'datacol';
                }
                cell.left = left;
                $cell.css({ 'width': ((cell.width || 100) + 'px'), 'text-align': (cell.align || 'left'), 'left': (left + 'px') });
                dfop._headHeight = dfop._headHeight > cell.height ? dfop._headHeight : cell.height;
                if (cell.frozen) {
                    dfop._leftWidth += cell.width;
                    $borderCol.append($cell);
                } else {
                    dfop._headWidth += cell.width;
                    $head.append($cell);
                }
            }
            if (dfop._headHeight > 28) {
                $head.find('.jfgrid-heed-rownum_0[jfgrid-heed-cellrender="ok"]').css({
                    'height': (dfop._headHeight + 'px'),
                    'line-height': ((dfop._headHeight - 1) + 'px')
                });
                $borderCol.find('.jfgrid-heed-rownum_0[jfgrid-heed-cellrender="ok"]').css({
                    'height': (dfop._headHeight + 'px'),
                    'line-height': ((dfop._headHeight - 1) + 'px')
                });
                for (var j = 0; j < len; j++) {
                    var cell = dfop.headData[j];
                    if (!!cell.children && cell.height < dfop._headHeight) {
                        if (!!cell.children) {
                            $.jfGrid.renderSubHeadToo(cell.children, dfop._headHeight - cell.height);
                        }
                        else {
                            cell.obj.css({
                                'height': (dfop._headHeight - cell.height + 28 + 'px'),
                                'line-height': ((dfop._headHeight - cell.height + 27) + 'px')
                            });
                        }
                    }
                }
            }
            $border.append($borderCol);
            $self.append($border);
            $self.append($head);

            dfop = null;
        },
        renderSubHead: function ($head, parentCell, dfop, left, level, frozen, path) {// 渲染子表头
            parentCell.width = 0;
            var chs = parentCell.children;
            var len = chs.length;
            var _height = 28;
            for (var i = 0; i < len; i++) {
                var cell = chs[i];
                cell.height = 28;
                cell.parent = parentCell;
                var $cell = $('<div class="jfgrid-head-cell jfgrid-heed-rownum_' + level + '" path="' + path + '.' + i + '" >' + (cell.label || "") + '</div>');
                cell.obj = $cell;
                cell.path = i;
                if (!!cell.children) {// 如果有子列
                    $.jfGrid.renderSubHead($head, cell, dfop, left + parentCell.width, level + 1, frozen, path + '.' + i);
                }
                else {
                    $cell.attr('jfgrid-heed-cellrender', 'ok');
                    $cell.append('<div class="jfgrid-heed-sort"><i class="fa fa-caret-up"></i><i class="fa fa-caret-down"></i></div>');
                    $cell.append('<div class="jfgrid-heed-move"></div>');
                    if (frozen) {
                        cell.frozen = true;
                        dfop._colFrozenModel[dfop._colFrozenModel.length] = cell;
                    } else {
                        dfop._colModel[dfop._colModel.length] = cell;
                    }
                    cell.type = 'datacol';
                }
                cell.left = left + parentCell.width;
                $cell.css({
                    'width': ((cell.width || 100) + 'px'),
                    'text-align': (cell.align || left),
                    'left': (left + parentCell.width + 'px'),
                    'top': (level * 28 + 'px')
                });
                parentCell.width += cell.width;
                _height = cell.height > _height ? cell.height : _height;
                $head.append($cell);
            }
            if (_height > 28) {
                $head.find('.jfgrid-heed-rownum_' + level + '[jfgrid-heed-cellrender="ok"]').css({
                    'height': (_height + 'px'),
                    'line-height': ((_height - 1) + 'px')
                });
            }
            parentCell.height += _height;

        },
        renderSubHeadToo: function (children, height) {
            var l = children.length;
            for (var i = 0; i < l; i++) {
                var $obj = children[i].obj;
                if (!!children[i].children) {
                    $.jfGrid.renderSubHeadToo(children[i].children, height);
                }
                else {
                    $obj.css({
                        'height': (height + children[i].height + 'px'),
                        'line-height': ((children[i].height + height - 1) + 'px')
                    });
                }
            }
        },
        renderBorder: function ($self) {// 渲染表格交界处
            var dfop = $self[0].dfop;
            // 头部和左侧交界部分
            var $border = $self.find('#jfgrid_border_' + dfop.id);
            var $head = $self.find('#jfgrid_head_' + dfop.id);
            var _width = 0;
            if (dfop.isShowNum) {
                var $num = $('<div class="jfgrid-border-cell jfgrid-border-num"></div>');
                $border.prepend($num);
                _width += 30;
            }
            if (dfop.isMultiselect) {
                var $cb = $('<div class="jfgrid-border-cell jfgrid-border-cb"><input role="checkbox" id="jfgrid_all_cb_' + dfop.id + '" type="checkbox"></div>')
                    .css('left', _width + 'px').css('line-height', (dfop._headHeight - 1) + 'px');
                $border.prepend($cb);
                _width += 30;
            }
            if (dfop.isSubGrid) {
                var $sub = $('<div class="jfgrid-border-cell jfgrid-border-sub"></div>').css('left', _width + 'px');
                $border.prepend($sub);
                _width += 30;
            }

            dfop._borderLeftPadding = _width;
            dfop._leftWidth += _width;
            //dfop._headWidth -= _width;

            $border.css({
                'width': (dfop._leftWidth + 'px'),
                'height': (dfop._headHeight + 'px'),
                'padding-left': (_width + 'px')
            });

            $head.css({
                'width': (dfop._headWidth + 'px'),
                'height': (dfop._headHeight + 'px'),
                'left': (dfop._leftWidth + 'px')
            });
            $self.css({
                'padding-left': (dfop._leftWidth + 'px'),
                'padding-top': (dfop._headHeight + 'px')
            });
            dfop = null;
        },
        renderLeft: function ($self) {
            var dfop = $self[0].dfop;
            var $left = $('<div class="jfgrid-left" id="jfgrid_left_' + dfop.id + '" ></div>');
            $left.css({ 'width': (dfop._leftWidth + 'px'), 'top': (dfop._headHeight + 'px') });
            $self.append($left);
            dfop = null;
        },
        renderScorllArea: function ($self) {
            var dfop = $self[0].dfop;
            // 滚动显示区域
            var $scrollArea = $('<div class="jfgrid-scrollarea" id="jfgrid_scrollarea_' + dfop.id + '" ><div class="jfgrid-scrollarea-content" id="jfgrid_scrollarea_content_' + dfop.id + '"  style="width:' + dfop._headWidth + 'px;" ></div></div>');
            $self.append($scrollArea);
            $scrollArea.mCustomScrollbar({ // 优化滚动条
                axis: "xy",
                setLeft: 0,
                callbacks: {
                    whileScrolling: function () {
                        var dfop = $(this.mcs.content)[0].jfGridDfop;
                        if (!!dfop) {
                            $('#jfgrid_left_' + dfop.id).css('top', (dfop._headHeight + this.mcs.top) + 'px');
                            $('#jfgrid_head_' + dfop.id).css('left', (dfop._leftWidth + this.mcs.left) + 'px');
                            $('#jfgrid_statistics_' + dfop.id).css('left', (dfop._leftWidth + this.mcs.left) + 'px');
                        }
                    },
                    onOverflowYNone: function (e) {
                        if (!!this.mcs) {

                            var dfop = $(this.mcs.content)[0].jfGridDfop;
                            $('#jfgrid_left_' + dfop.id).css('top', dfop._headHeight + 'px');
                        }
                    },
                    onOverflowXNone: function (e) {
                        if (!!this.mcs) {
                            var dfop = $(this.mcs.content)[0].jfGridDfop;
                            $('#jfgrid_head_' + dfop.id).css('left', dfop._leftWidth + 'px');
                            $('#jfgrid_statistics_' + dfop.id).css('left', dfop._leftWidth + 'px');
                        }
                    },
                    onUpdate: function () {
                        if (!!this.mcs) {
                            var dfop = $(this.mcs.content)[0].jfGridDfop;
                            $('#jfgrid_scrollarea_' + dfop.id).mCustomScrollbar("scrollTo", "left", {
                                scrollInertia: 0,
                                timeout: 0
                            });
                        }
                    }
                }
            });
            $scrollArea.find('#jfgrid_scrollarea_content_' + dfop.id).parent()[0].jfGridDfop = dfop;
            dfop = null;
        },
        renderPageBarinit: function ($self) {
            var dfop = $self[0].dfop;
            if (dfop.isPage && dfop.url) {
                $self.css({ 'padding-bottom': '35px' });
                var $pagebar = $('<div class="jfgrid-page-bar" id="jfgrid_page_bar_' + dfop.id + '"><div class="jfgrid-page-bar-info" >无显示数据</div>\
                <div class="paginations" id="jfgrid_page_bar_nums_'+ dfop.id + '" style="display:none;" >\
                <ul class="pagination pagination-sm"><li><a href="javascript:void(0);" class="pagebtn">首页</a></li></ul>\
                <ul class="pagination pagination-sm"><li><a href="javascript:void(0);" class="pagebtn">上一页</a></li></ul>\
                <ul class="pagination pagination-sm" id="jfgrid_page_bar_num_' + dfop.id + '" ></ul>\
                <ul class="pagination pagination-sm"><li><a href="javascript:void(0);" class="pagebtn">下一页</a></li></ul>\
                <ul class="pagination pagination-sm"><li><a href="javascript:void(0);" class="pagebtn">尾页</a></li></ul>\
                <ul class="pagination"><li><span></span></li></ul>\
                <ul class="pagination"><li><input type="text" class="form-control"></li></ul>\
                <ul class="pagination pagination-sm"><li><a href="javascript:void(0);" class="pagebtn">跳转</a></li></ul>\
                </div></div>');
                $self.append($pagebar);
                var $pagebarBtn = $pagebar.find('#jfgrid_page_bar_num_' + dfop.id);
                $pagebarBtn.on('click', $.jfGrid.reloadPage);

                $('#jfgrid_page_bar_nums_' + dfop.id + ' .pagebtn').on('click', { op: dfop }, $.jfGrid.reloadPage2);
            }
            dfop = null;
        },
        renderPageBar: function ($self) {
            var dfop = $self[0].dfop;
            var $pagebar = $('#jfgrid_page_bar_' + dfop.id);
            var $pagebarBtn = $pagebar.find('#jfgrid_page_bar_num_' + dfop.id);
            var $pagebarBtns = $pagebar.find('#jfgrid_page_bar_nums_' + dfop.id);

            var pagebarLabel = '';
            var btnlist = "";
            if (dfop.rowdatas.length == 0) {
                pagebarLabel = '无显示数据';
            }
            else {
                var pageparam = dfop.pageparam;
                var beginnum = (pageparam.page - 1) * pageparam.rows + 1;
                var endnum = beginnum + dfop.rowdatas.length - 1;
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

            dfop = null;
        },
        renderFooterrow: function ($self) {
            var dfop = $self[0].dfop;
            if (dfop.footerrow && !dfop.isPage) {
                $self.css({ 'padding-bottom': '29px' });
                var $footerrow = $('<div class="jfgrid-footerrow" id="jfgrid_footerrow_' + dfop.id + '"></div>');
                $self.append($footerrow);
            }
            dfop = null;
        },
        renderToolBar: function ($self) {
            var dfop = $self[0].dfop;
            if (dfop.isEidt) {
                var $toolbar = $('<div class="jfgrid-toolbar" id="jfgrid_toolbar_' + dfop.id + '"></div>');

                var $add = $('<span><i class="fa fa-plus"></i></span>');
                var $minus = $('<span><i class="fa fa-minus"></i></span>');


                $add.on('click', function () {
                    dfop.rowdatas.push({});
                    $.jfGrid.renderData($self);
                });

                $minus.on('click', function () {
                    var iMap = {};
                    var flag = false;
                    $self.find('.jfgrid_selected_' + dfop.id + '[datapath]').each(function () {
                        var _index = $(this).attr('datapath');
                        iMap[_index] = '1';
                        flag = true;
                    });
                    if (flag) {
                        var _rows = [];
                        for (var i = 0, l = dfop.rowdatas.length; i < l; i++) {
                            if (!iMap[i]) {
                                _rows.push(dfop.rowdatas[i]);
                            }
                        }
                        if (_rows.length == 0) {
                            _rows.push({});
                        }
                        dfop.rowdatas = _rows;

                        $.jfGrid.renderData($self);
                    }
                    else {
                        top.learun.alert.warning('请选择需要删除的行!');
                    }
                });


                $toolbar.append($add);
                $toolbar.append($minus);

                $self.append($toolbar);
            }
        },
        // 合计条
        renderStatistics: function ($self) {
            var dfop = $self[0].dfop;
            if (dfop.isStatistics) {
                var $footerrow = $self.find('#jfgrid_footerrow_' + dfop.id);
                var $statistics = $footerrow.find('#jfgrid_statistics_' + dfop.id);
                if ($statistics.length == 0) {
                    $statistics = $('<div class="jfgrid-statistics" id="jfgrid_statistics_' + dfop.id + '" ></div>');
                    $footerrow.append($statistics);
                    $statistics.css({
                        'width': (dfop._headWidth + 'px'),
                        'left': (dfop._leftWidth + 'px')
                    });
                }
                $statistics.html("");

                var sindex = -1;
                var $dcell;
                for (var n = 0, nl = dfop._colModel.length; n < nl; n++) {
                    var col = dfop._colModel[n];
                    var vaule = 0;
                    if (col.statistics) {
                        if (sindex == -1) {
                            sindex = 0;
                            if (!!$dcell) {
                                $dcell.html('合计:');
                            }
                        }

                        for (var i = 0, l = dfop.rowdatas.length; i < l; i++) {
                            var item = dfop.rowdatas[i];
                            if (item[col.name] != undefined && item[col.name] != null && item[col.name] != "") {
                                vaule += parseFloat(item[col.name]);
                            }
                        }
                    }
                    $dcell = $('<div class="jfgrid-data-cell"  colname="' + col.name + '" style="width:' + col.width + 'px;text-align:' + col.align + ';" ></div>');
                    if (col.statistics) {
                        $dcell.html(vaule);
                        $dcell.attr('title', col.label);
                    }
                    $statistics.append($dcell);
                }
            }
            dfop = null;
        },
        renderStatisticsOne: function ($self, name) {
            var dfop = $self[0].dfop;
            if (dfop.isStatistics) {
                var $statistics = $self.find('#jfgrid_statistics_' + dfop.id);
                var vaule = 0;
                for (var i = 0, l = dfop.rowdatas.length; i < l; i++) {
                    var item = dfop.rowdatas[i];
                    if (item[name] != undefined && item[name] != null && item[name] != "") {
                        vaule += parseFloat(item[name]);
                    }
                }
                $statistics.find('[colname="' + name + '"]').html(vaule);

            }
            dfop = null;
        },

        /*刷新方法*/
        reload: function ($self) {
            var dfop = $self[0].dfop;
            dfop.rowdatas = [];
            if (dfop.isPage) {
                dfop.pageparam.page = dfop.pageparam.page || 1;
                dfop.pageparam.records = 0;
                dfop.pageparam.total = 0;
                dfop.param['pagination'] = JSON.stringify(dfop.pageparam);
                $.jfGrid.ajaxLoad($self, dfop.url, dfop.param, function (res) {
                    if (res.code == 200) {
                        dfop.rowdatas = res.data.rows;
                        dfop.pageparam.page = res.data.page;
                        dfop.pageparam.records = res.data.records;
                        dfop.pageparam.total = res.data.total;
                    }
                    else {
                        dfop.rowdatas = [];
                        dfop.pageparam.page = 1;
                        dfop.pageparam.records = 0;
                        dfop.pageparam.total = 0;
                    }
                    $.jfGrid.renderData($self);
                    if (!!dfop.onRenderComplete) {
                        dfop.onRenderComplete(res.data.rows);
                    }
                });
            } else {
                $.jfGrid.ajaxLoad($self, dfop.url, dfop.param, function (res) {
                    if (res.code == 200) {
                        dfop.rowdatas = res.data;
                    }
                    else {
                        dfop.rowdatas = [];
                    }
                    $.jfGrid.renderData($self);
                    if (!!dfop.onRenderComplete) {
                        dfop.onRenderComplete(dfop.rowdatas);
                    }
                });
            }
        },
        reloadPage: function (e) {
            var $this = $(this);
            var $self = $('#' + $this.attr('id').replace('jfgrid_page_bar_num_', ''));
            var dfop = $self[0].dfop;

            var et = e.target || e.srcElement;
            var $et = $(et);
            if ($et.hasClass('pagebtn')) {
                var page = parseInt($et.text());
                if (page != dfop.pageparam.page) {
                    $this.find('.active').removeClass('active');
                    $et.addClass('active');
                    dfop.pageparam.page = page;
                    dfop.param['pagination'] = JSON.stringify(dfop.pageparam);
                    $.jfGrid.ajaxLoad($self, dfop.url, dfop.param, function (res) {
                        if (res.code == 200) {
                            dfop.rowdatas = res.data.rows;
                            dfop.pageparam.page = res.data.page;
                            dfop.pageparam.records = res.data.records;
                            dfop.pageparam.total = res.data.total;
                        }
                        $.jfGrid.renderData($self);
                    });
                }
            }
        },
        reloadPage2: function (e) {
            var $this = $(this);
            var dfop = e.data.op;
            var name = $this.text();
            var $pagenum = $('#jfgrid_page_bar_num_' + dfop.id);
            var page = parseInt($pagenum.find('.active').text());
            var falg = false;
            switch (name) {
                case '首页':
                    if (page != 1) {
                        dfop.pageparam.page = 1;
                        falg = true;
                    }
                    break;
                case '上一页':
                    if (page > 1) {
                        dfop.pageparam.page = page - 1;
                        falg = true;
                    }
                    break;
                case '下一页':
                    if (page < dfop.pageparam.total) {
                        dfop.pageparam.page = page + 1;
                        falg = true;
                    }
                    break;
                case '尾页':
                    if (page != dfop.pageparam.total) {
                        dfop.pageparam.page = dfop.pageparam.total;
                        falg = true;
                    }
                    break;
                case '跳转':
                    var text = $this.parents('#jfgrid_page_bar_nums_' + dfop.id).find('input').val();
                    if (!!text) {
                        var p = parseInt(text);
                        if (String(p) != 'NaN') {
                            if (p < 1) {
                                p = 1;
                            }
                            if (p > dfop.pageparam.total) {
                                p = dfop.pageparam.total;
                            }
                            dfop.pageparam.page = p;
                            falg = true;
                        }
                    }
                    break;
            }
            if (falg) {
                dfop.param['pagination'] = JSON.stringify(dfop.pageparam);
                $.jfGrid.ajaxLoad($('#' + dfop.id), dfop.url, dfop.param, function (res) {
                    if (res.code == 200) {
                        dfop.rowdatas = res.data.rows;
                        dfop.pageparam.page = res.data.page;
                        dfop.pageparam.records = res.data.records;
                        dfop.pageparam.total = res.data.total;
                    }
                    $.jfGrid.renderData($('#' + dfop.id));
                });
            }
        },

        renderData: function ($self) {
            var dfop = $self[0].dfop;
            if (dfop.isPage) {
                /*做一下分页数据的处理*/
                $.jfGrid.renderRowData($self, dfop.rowdatas);
                $.jfGrid.renderPageBar($self);
            }
            else if (dfop.isTree) {
                /*做一下树形数据的处理*/
                if (dfop.datatype != 'tree') {
                    dfop.rowdatas = $.jfGrid.listTotree(dfop.rowdatas, dfop.parentId, dfop.mainId, dfop);
                }
                $.jfGrid.renderTreeRowData($self, dfop.rowdatas);
            }
            else {
                $.jfGrid.renderRowData($self, dfop.rowdatas);
            }
            $.jfGrid.renderStatistics($self);
            dfop = null;
        },
        renderRowData: function ($self, data) {
            var dfop = $self[0].dfop;
            var $letf = $self.find('#jfgrid_left_' + dfop.id);
            var $scrollareaContent = $self.find('#jfgrid_scrollarea_content_' + dfop.id);
            var $bgimg = $self.find('#jfgrid_nodata_img_' + dfop.id);
            $bgimg.hide();

            /*当前选中项*/
            var selectedRow;
            if (dfop.reloadSelected && !dfop.isMultiselect) {
                selectedRow = $.jfGrid.getSelectedRow($self);
            }

            $letf.html("");
            $scrollareaContent.html("");
            data = data || [];
            var len = data.length;
            for (var i = 0; i < len; i++) {//行循环
                var _row = data[i];
                var $lastCell = null;

                var rownum = 'rownum_' + dfop.id + '_' + i;
                $letf.append('<div class="jfgrid-data-cell jfgrid-hide-cell" rownum="' + rownum + '"  datapath="' + i + '" ></div>');

                if (dfop.isShowNum) {
                    var $dcell = $('<div class="jfgrid-data-cell jfgrid-tool-cell" rownum="' + rownum + '" colname="jfgrid_rownum"  >' + (i + 1) + '</div>');
                    $letf.append($dcell);
                    $lastCell = $dcell;
                }
                if (dfop.isMultiselect) {
                    // 如果是多选绑定字段
                    var ck = '';
                    //if (!!dfop.multiselectfield && parseInt(_row[dfop.multiselectfield]) == 1) {
                    //    ck = 'checked';
                    //}
                    var $dcell = $('<div class="jfgrid-data-cell jfgrid-tool-cell" rownum="' + rownum + '" colname="jfgrid_multiselect" ><input role="checkbox" type="checkbox" ></div>');
                    $letf.append($dcell);
                    $lastCell = $dcell;


                }
                if (dfop.isSubGrid) {
                    var $dcell = $('<div class="jfgrid-data-cell jfgrid-tool-cell" rownum="' + rownum + '" colname="jfgrid_subGrid"><i class="fa fa-plus jfgrid_expend" rownum="' + rownum + '" ></i></div>');
                    $letf.append($dcell);
                    $lastCell = $dcell;
                }

                for (var m = 0, ml = dfop._colFrozenModel.length; m < ml; m++) {// 左侧固定列
                    var $dcell = $('<div class="jfgrid-data-cell" rownum="' + rownum + '"  colname="' + dfop._colFrozenModel[m].name + '" style="width:' + dfop._colFrozenModel[m].width + 'px;text-align:' + dfop._colFrozenModel[m].align + ';"  title="' + (_row[dfop._colFrozenModel[m].name] || "") + '" ></div>');
                    if (m == ml - 1) {
                        $lastCell = $dcell;
                    }
                    $.jfGrid.getCellHtml(dfop._colFrozenModel[m], _row, dfop, $dcell, i)
                    $letf.append($dcell);
                }
                if (!!$lastCell) {
                    $lastCell.addClass('jfgrid-data-cell-last');
                }
                for (var n = 0, nl = dfop._colModel.length; n < nl; n++) {// 右侧滚动显示区域
                    var $dcell = $('<div class="jfgrid-data-cell" rownum="' + rownum + '" colname="' + dfop._colModel[n].name + '" style="width:' + dfop._colModel[n].width + 'px;text-align:' + dfop._colModel[n].align + ';" title="' + (_row[dfop._colModel[n].name] || "") + '"  ></div>');
                    if (n == nl - 1) {
                        $dcell.addClass('jfgrid-data-cell-last');
                    }
                    $.jfGrid.getCellHtml(dfop._colModel[n], _row, dfop, $dcell, i)
                    $scrollareaContent.append($dcell);
                }
                // 是否刷新被选中
                if (dfop.reloadSelected && !dfop.isMultiselect && !!selectedRow && _row[dfop.mainId] == selectedRow[dfop.mainId]) {
                    $self.find('.jfgrid-data-cell[rownum="' + rownum + '"]').addClass('jfgrid-head-cell-selected').addClass('jfgrid_selected_' + dfop.id);
                }

                if (!!dfop.multiselectfield && parseInt(_row[dfop.multiselectfield]) == 1) {
                    $self.find('.jfgrid-data-cell[rownum="' + rownum + '"][colname="jfgrid_multiselect"] input').trigger('click');
                }

            }
            if (len == 0) {// 没有数据显示背景图片
                $bgimg.show();
            }
            else {
                if (dfop.isAutoHeight) {
                    var _height = 28 * len;
                    var _top = $self.css('padding-top');
                    if (dfop.footerrow && !dfop.isPage) {
                        _height = parseInt(_height) + 29;
                    }
                    $self.css({ 'height': (parseInt(_height) + parseInt(_top) + 2) });
                }
            }



            dfop = null;
        },//刷新数据
        renderTreeRowData: function ($self, data) {
            var dfop = $self[0].dfop;
            var $letf = $self.find('#jfgrid_left_' + dfop.id);
            var $scrollareaContent = $self.find('#jfgrid_scrollarea_content_' + dfop.id);
            var $bgimg = $self.find('#jfgrid_nodata_img_' + dfop.id);

            /*当前选中项*/
            var selectedRow;
            if (dfop.reloadSelected && !dfop.isMultiselect) {
                selectedRow = $.jfGrid.getSelectedRow($self);
            }


            var rownum = 0;
            $bgimg.hide();
            $letf.html("");
            $scrollareaContent.html("");
            data = data || [];

            if (data.length > 0) {
                _fn(data, 1, '');
            }
            else {
                $bgimg.show();
            }

            function _fn(_data, deep, path) {
                for (var j = 0, l = _data.length; j < l; j++) {
                    var _row = _data[j];
                    var $expendcell = $('<div class="jfgrid-data-cell-expend" style="width:' + (deep * 16) + 'px;" ></div>');
                    _row["jfgrid_rownum"] = rownum;

                    var rownumindex = 'rownum_' + dfop.id + '_' + rownum;

                    var $lastCell = null;

                    $letf.append('<div class="jfgrid-data-cell jfgrid-hide-cell"  rownum="' + rownumindex + '"  datapath="' + path + j + '" ></div>');
                    if (dfop.isShowNum) {
                        var $dcell = $('<div class="jfgrid-data-cell jfgrid-tool-cell" rownum="' + rownumindex + '" colname="jfgrid_rownum"  >' + (rownum + 1) + '</div>');
                        $letf.append($dcell);
                        $lastCell = $dcell;
                    }
                    if (dfop.isMultiselect) {
                        // 如果是多选绑定字段
                        var ck = '';
                        //if (!!dfop.multiselectfield && parseInt(_row[dfop.multiselectfield]) == 1) {
                        //    ck = 'checked';
                        //}
                        var $dcell = $('<div class="jfgrid-data-cell jfgrid-tool-cell" rownum="' + rownumindex + '" colname="jfgrid_multiselect" ><input role="checkbox" type="checkbox" ></div>');
                        $letf.append($dcell);
                        $lastCell = $dcell;


                    }
                    if (dfop.isSubGrid) {
                        var $dcell = $('<div class="jfgrid-data-cell jfgrid-tool-cell" rownum="' + rownumindex + '" colname="jfgrid_subGrid"><i class="fa fa-plus jfgrid_expend" rownum="' + rownumindex + '" ></i></div>');
                        $letf.append($dcell);
                        $lastCell = $dcell;
                    }
                    for (var m = 0, ml = dfop._colFrozenModel.length; m < ml; m++) {// 左侧固定列
                        var $dcell = $('<div class="jfgrid-data-cell" rownum="' + rownumindex + '"  colname="' + dfop._colFrozenModel[m].name + '" style="width:' + dfop._colFrozenModel[m].width + 'px;text-align:' + dfop._colFrozenModel[m].align + ';"  title="' + (_row[dfop._colFrozenModel[m].name] || "") + '" >' + (_row[dfop._colFrozenModel[m].name] || "") + '</div>');
                        $.jfGrid.getCellHtml(dfop._colFrozenModel[m], _row, dfop, $dcell, j)
                        if (m == 0) {
                            $dcell.prepend($expendcell);
                        }
                        if (m == ml - 1) {
                            $lastCell = $dcell;
                        }
                        $letf.append($dcell);
                    }
                    if (!!$lastCell) {
                        $lastCell.addClass('jfgrid-data-cell-last');
                    }
                    for (var n = 0, nl = dfop._colModel.length; n < nl; n++) {// 右侧滚动显示区域
                        var $dcell = $('<div class="jfgrid-data-cell" rownum="' + rownumindex + '" colname="' + dfop._colModel[n].name + '" style="width:' + dfop._colModel[n].width + 'px;text-align:' + dfop._colModel[n].align + ';" title="' + (_row[dfop._colModel[n].name] || "") + '"  >' + (_row[dfop._colModel[n].name] || "") + '</div>');
                        $.jfGrid.getCellHtml(dfop._colModel[n], _row, dfop, $dcell, j);
                        if (n == 0 && dfop._colFrozenModel.length == 0) {
                            $dcell.prepend($expendcell);
                        }
                        if (n == nl - 1) {
                            $dcell.addClass('jfgrid-data-cell-last');
                        }

                        $scrollareaContent.append($dcell);
                    }

                    // 是否刷新被选中
                    if (dfop.reloadSelected && !dfop.isMultiselect && !!selectedRow && _row[dfop.mainId] == selectedRow[dfop.mainId]) {
                        $self.find('.jfgrid-data-cell[rownum="' + rownumindex + '"]').addClass('jfgrid-head-cell-selected').addClass('jfgrid_selected_' + dfop.id);
                    }
                    if (!!dfop.multiselectfield && parseInt(_row[dfop.multiselectfield]) == 1) {
                        $self.find('.jfgrid-data-cell[rownum="' + rownum + '"][colname="jfgrid_multiselect"] input').trigger('click');
                    }
                    rownum++;
                    if (_row.jfGrid_ChildRows.length > 0) {
                        $expendcell.append('<i class="fa fa-caret-down jfgrid-data-cell-expendi"></i>');
                        _fn(_row.jfGrid_ChildRows, deep + 1, path + j + '.');
                    }
                }
            };
            dfop = null;
        },
        renderPage: function (dfop) {// 刷新分页
            var $pagebar = $('#jfgrid_page_bar_' + dfop.id);
            if (!!dfop._pagination && dfop._pagination.total > 1) {
                var $previous = $('<li title="上一页" ><a href="#" aria-label="上一页"><span aria-hidden="true">&laquo;</span></a></li>');
                var $next = $('<li title="下一页"><a href="#" aria-label="下一页"><span aria-hidden="true">&raquo;</span></a></li>');
            }



        },
        getCellHtml: function (node, row, dfop, $dcell, rownum) {
            var res = '';
            var value = row[node.name];

            if (!!node.editType) {
                switch (node.editType) {
                    case 'input':
                        var $input = $('<input  type="text" class="form-control" />').val(value || '');
                        $input.css({ 'text-align': node.align });
                        $input.on('input propertychange', function () {
                            var _$input = $(this);
                            row[node.name] = _$input.val();
                            if (node.statistics) {
                                $.jfGrid.renderStatisticsOne($('#' + dfop.id), node.name);
                            }
                            if (!!node.editOp && !!node.editOp.callback) {
                                node.editOp.callback(rownum, row);
                                $.jfGrid.renderData($('#' + dfop.id));
                                $('[rownum="rownum_' + dfop.id + '_' + rownum + '"][colname="' + node.name + '"]>input').focus();
                            }
                        });
                        $dcell.html($input);
                        if (!!node.formatter) {
                            node.formatter(value, row, dfop, $dcell);
                        }
                        break;
                    case 'label':
                        var $label = $('<span class="form-label" >' + (value || '') + '</span>');
                        $label.css({ 'text-align': node.align });
                        $dcell.html($label);
                        break;
                    case 'select':
                        var $label = $('<div class="form-select" >' + (value || '') + '<i class="fa fa-ellipsis-h"></i></div>');
                        $label.css({ 'text-align': node.align });
                        $label.find('.fa-ellipsis-h')[0]._node = node;
                        $label.find('.fa-ellipsis-h').on('click', function () {
                            var _node = $(this)[0]._node;
                            $.jfGrid.layer({
                                html: '<div class="jfgird-select"><div class="jfgird-select-tool"><div class="jfgird-select-tool-item"><input id="jfgird_select_keyword" style="width:200px;" type="text" class="form-control" placeholder="请输入要查询关键字"></div><div class="jfgird-select-tool-item"><a id="jfgird_select_search" class="btn btn-primary btn-sm"><i class="fa fa-search"></i>&nbsp;查询</a></div></div><div id="jfgird_select"></div></div>',
                                width: _node.editOp.width || 400,
                                height: _node.editOp.height || 400
                            },
                                function ($html) {
                                    $html.find('#jfgird_select').jfGrid({
                                        headData: _node.editOp.colData,
                                        url: _node.editOp.url,
                                        onRenderComplete: function (rowdatas) {
                                            _node.editOp.rowdatas = rowdatas;
                                        },
                                        onSelectRow: function (rowdata) {
                                            if (!!_node.editOp.callback) {
                                                _node.editOp.callback(rowdata, rownum, row, _node.editOp.selectData);
                                            }
                                            $html.remove();
                                            $.jfGrid.renderData($('#' + dfop.id));
                                        }
                                    });
                                    if (!!_node.editOp.rowdatas) {
                                        $html.find('#jfgird_select').jfGridSet('refreshdata', { rowdatas: _node.editOp.rowdatas });
                                    }
                                    else {
                                        $html.find('#jfgird_select').jfGridSet('reload', { param: _node.editOp.param });
                                    }
                                    $('#jfgird_select_search').on('click', function () {
                                        var data = [];
                                        var keyword = $('#jfgird_select_keyword').val();
                                        if (!!keyword) {
                                            for (var i = 0, l = _node.editOp.rowdatas.length; i < l; i++) {
                                                var item = _node.editOp.rowdatas[i];
                                                for (var j = 0, jl = _node.editOp.colData.length; j < jl; j++) {
                                                    if (item[_node.editOp.colData[j].name].indexOf(keyword) != -1) {
                                                        data.push(item);
                                                        break;
                                                    }
                                                }
                                            }
                                            $('#jfgird_select').jfGridSet('refreshdata', { rowdatas: data });
                                        }
                                        else {
                                            $('#jfgird_select').jfGridSet('refreshdata', { rowdatas: _node.editOp.rowdatas });
                                        }

                                    });
                                });

                        });
                        $dcell.html($label);
                        break;
                    case 'checkbox':
                        var $input = $('<div style="position: absolute;top: 0;left: 0;width: 100%;height: 100%;background: #fff;" ><input  type="checkbox" style="margin:0;margin-top:7px;" /></div>');
                        if (value == 1 || value == '1') {
                            $input.find('input').trigger('click');
                        }
                        $input.find('input').on('click', function () {
                            if ($(this).is(':checked')) {
                                row[node.name] = 1;
                            }
                            else {
                                row[node.name] = 0;
                            }
                        });
                        $dcell.html($input);
                        break;
                    case 'layerselect':
                        var $label = $('<div class="form-select" >' + (value || '') + '<i class="fa fa-ellipsis-h"></i></div>');
                        $label.css({ 'text-align': node.align });
                        $label.find('.fa-ellipsis-h').on('click', function () {
                            node.formatter(value, row, dfop, $dcell);
                        });
                        $dcell.html($label);
                        break;
                }
            }
            else {
                if (!!node.formatter) {
                    res = node.formatter(value, row, dfop, $dcell);
                    $dcell.html(res);
                }
                else if (node.formatterAsync) {
                    node.formatterAsync(function (text) {
                        text = text || '';
                        $dcell.html(text);
                    }, value, row, dfop, $dcell);
                }
                else {
                    res = value;
                    if (value == null || value == undefined || value == 'null' || value == 'undefined') {
                        res = '';
                    }
                    $dcell.html(res);
                }
            }
        },


        /*绑定事件*/
        bindClick: function (e) {
            var $this = $(this);
            var dfop = $this[0].dfop;
            var et = e.target || e.srcElement;
            var $et = $(et);
            if (dfop.isHeadWidhChange) {// 调整表头宽
                $.jfGrid.moveHeadWidth(dfop, false);
                dfop.isHeadWidhChange = false;
            }
            else if ($et.hasClass('jfgrid-head-cell')) {// 排序
                $.jfGrid.sortCol($et, dfop);
            }
            else if (($et.hasClass('jfgrid_expend') && $et.parent().hasClass('jfgrid-data-cell')) || ($et.hasClass('jfgrid-data-cell') && $et.find('.jfgrid_expend').length > 0)) {// 展开或关闭子表单
                $.jfGrid.expandSubGrid($et, dfop);
            }
            else if ($et.hasClass('jfgrid-data-cell-expendi')) {// 树形结构展开和关闭
                $.jfGrid.expandTree($et, dfop);
            }
            else if ($et.hasClass('jfgrid-data-cell') || $et.parents('.jfgrid-data-cell').length > 0) {// 选中行
                $.jfGrid.selectRow($this, $et, dfop);
                e.stopPropagation();
                //return false;
            }
            else if ($et.attr('id') == ('jfgrid_all_cb_' + dfop.id)) {// 全部勾选
                $.jfGrid.selectAllRow($this, $et, dfop);
            }

        },
        bindmdown: function (e) {
            var $this = $(this);
            var dfop = $this[0].dfop;
            var et = e.target || e.srcElement;
            var $et = $(et);
            if ($et.hasClass('jfgrid-heed-move')) {

                var path = $et.parent().attr('path');
                var $moveline = $('#jfgrid_move_line_' + dfop.id);
                dfop._currentMoveCell = $.jfGrid.getHeadCell(path, dfop.headData);
                dfop._currentMoveCell.pageX = e.pageX;
                dfop._currentMoveLeft = 0;
                if (dfop._currentMoveCell.frozen) {
                    dfop._currentMoveLeft = dfop._borderLeftPadding + dfop._currentMoveCell.left + dfop._currentMoveCell.width;
                }
                else {
                    var _leftWidth = parseInt($('#jfgrid_head_' + dfop.id).css('left').replace(/px/g, ""));
                    dfop._currentMoveLeft = _leftWidth + dfop._currentMoveCell.left + dfop._currentMoveCell.width;
                }
                dfop._currentMoveWidth = dfop._currentMoveCell.width;
                $moveline.css('left', dfop._currentMoveLeft + 'px');
                $moveline.show();
                dfop.isHeadWidhChange = true;
            }
        },
        bindmmove: function (e) {
            var $this = $(this);
            var dfop = $this[0].dfop;
            if (dfop.isHeadWidhChange) {
                $.jfGrid.moveHeadWidth(dfop, true, e.pageX);
            }
            dfop = null;
        },
        bindmover: function (e) {
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
        },

        /*处理方法*/
        moveHeadWidth: function (dfop, ismove, pageX) {// 处理表头宽度调整方法
            var $moveline = $('#jfgrid_move_line_' + dfop.id);
            if (ismove) {
                var width = dfop._currentMoveCell.width + (pageX - dfop._currentMoveCell.pageX);
                width = (width < 40 ? 40 : width);
                var left = dfop._currentMoveLeft + (width - dfop._currentMoveCell.width);
                dfop._currentMoveWidth = width;
                $moveline.css('left', left + 'px');
            }
            else {
                if (dfop._currentMoveWidth != dfop._currentMoveCell.width) {
                    var _width = dfop._currentMoveWidth - dfop._currentMoveCell.width;
                    dfop._currentMoveCell.width = dfop._currentMoveWidth;
                    dfop._currentMoveCell.obj.css('width', dfop._currentMoveCell.width + 'px');
                    var p = dfop._currentMoveCell.parent;
                    var c = dfop._currentMoveCell;
                    $.jfGrid.refreshHead(c.frozen, dfop, p, c, _width);
                }
                $moveline.hide();
            }
        },
        getHeadCell: function (path, data) {
            var paths = path.split('.');
            var r = data;
            for (var i = 0, l = paths.length; i < l; i++) {
                if (i == l - 1) {
                    return r[paths[i]];
                }
                r = r[paths[i]].children;
            }
        },
        refreshHead: function (frozen, dfop, p, c, width) {
            // 调整自己孩子的位置
            function setChildLeft(chs) {
                for (var i = 0, l = chs.length; i < l; i++) {
                    var item = chs[i];
                    item.left = item.left + width;
                    item.obj.css('left', item.left + 'px');
                    if (!!item.children) {
                        setChildLeft(item.children);
                    }
                }
            }
            // 调整单元格所在集合的位置和大小
            while (p) {
                // 移动自己的兄弟
                for (var i = c.path + 1, l = p.children.length; i < l; i++) {
                    var item = p.children[i];
                    item.left = item.left + width;
                    item.obj.css('left', item.left + 'px');
                    if (!!item.children) {
                        setChildLeft(item.children);
                    }
                }
                p.width = p.width + width;
                p.obj.css('width', p.width + 'px');
                c = p;
                p = p.parent;
            }
            // 刷新单元格右侧的位置
            for (var i = c.path + 1, l = dfop.headData.length; i < l; i++) {
                var item = dfop.headData[i];
                if (item.frozen == frozen) {
                    item.left = item.left + width;
                    item.obj.css('left', item.left + 'px');

                    if (!!item.children) {
                        setChildLeft(item.children);
                    }
                }
            }

            // 刷新单元承载容器
            var $jfGrid = $('#' + dfop.id);
            var $head = $jfGrid.find('#jfgrid_head_' + dfop.id);
            if (!!frozen) {
                var $border = $jfGrid.find('#jfgrid_border_' + dfop.id);
                var $left = $jfGrid.find('#jfgrid_left_' + dfop.id);
                dfop._leftWidth = dfop._leftWidth + width;
                $jfGrid.css('padding-left', dfop._leftWidth + 'px');
                $border.css('width', dfop._leftWidth + 'px');
                $left.css('width', dfop._leftWidth + 'px');
                $head.css('left', dfop._leftWidth + 'px');
            }
            else {

                dfop._headWidth = dfop._headWidth + width;
                $head.css('width', dfop._headWidth + 'px');
                var $scrollareaContent = $jfGrid.find('#jfgrid_scrollarea_content_' + dfop.id);
                $scrollareaContent.css('width', dfop._headWidth - 10 + 'px');
                if (width < 0) {
                    $jfGrid.css('width', '10000px');
                    $('#jfgrid_scrollarea_' + dfop.id).mCustomScrollbar("update");
                    $jfGrid.css('width', '100%');
                }
                $scrollareaContent.css('width', dfop._headWidth + 'px');
                $('#jfgrid_scrollarea_' + dfop.id).mCustomScrollbar("update");
            }

            //移动表格对应数据的单元格列宽度
            $jfGrid.find('.jfgrid-data-cell[colname="' + dfop._currentMoveCell.name + '"]').css('width', dfop._currentMoveCell.width + 'px');
        },
        selectRow: function ($this, $et, dfop) {
            var $selfcell = $et;
            var rowid = $et.attr('rownum');
            if (!rowid) {
                $selfcell = $et.parents('.jfgrid-data-cell');
                rowid = $selfcell.attr('rownum');
            }
            var classid = '.jfgrid_selected_' + dfop.id;
            if (dfop.isMultiselect) {
                if ($et.attr('role') == 'checkbox') {
                    var datapath = $this.find('[rownum="' + rowid + '"][datapath]').attr('datapath');
                    var rowItem = $.jfGrid.getRowDataByPath(dfop, datapath);

                    console.log(rowItem[dfop.multiselectfield]);

                    if ($selfcell.hasClass('jfgrid-head-cell-selected')) {
                        $this.find(classid + '[rownum="' + rowid + '"]').removeClass('jfgrid-head-cell-selected').removeClass('jfgrid_selected_' + dfop.id);
                        if ($('#jfgrid_all_cb_' + dfop.id).is(':checked')) {
                            dfop._cancelallcb = true;

                            $('#jfgrid_all_cb_' + dfop.id).trigger('click');
                        }
                        dfop.multiselectfield && (rowItem[dfop.multiselectfield] = 0);
                        dfop.checkRow && dfop.checkRow(rowItem, false);
                    }
                    else {
                        $this.find('[rownum="' + rowid + '"]').addClass('jfgrid-head-cell-selected').addClass('jfgrid_selected_' + dfop.id);
                        dfop.multiselectfield && (rowItem[dfop.multiselectfield] = 1);
                        dfop.checkRow && dfop.checkRow(rowItem, true);
                    }
                }
                //else {
                //    $this.find('.jfgrid-tool-cell[rownum="' + rowid + '"] input[role="checkbox"]').trigger('click');
                //}
            }
            else {

                $this.find(classid).removeClass('jfgrid-head-cell-selected').removeClass('jfgrid_selected_' + dfop.id);
                $this.find('[rownum="' + rowid + '"]').addClass('jfgrid-head-cell-selected').addClass('jfgrid_selected_' + dfop.id);
            }
            if (dfop.onSelectRow) {
                dfop.onSelectRow($.jfGrid.getSelectedRow($this));
            }
        },
        selectAllRow: function ($this, $et, dfop) {
            if ($et.is(':checked')) {
                $this.find('.jfgrid-tool-cell [role="checkbox"]').not(':checked').trigger('click');

            }
            else {
                if (!dfop._cancelallcb) {
                    $this.find('.jfgrid-tool-cell [role="checkbox"]:checked').trigger('click');
                }
                dfop._cancelallcb = false;
            }
        },
        sortCol: function ($et, dfop) {
            var path = $et.attr('path');
            var cell = $.jfGrid.getHeadCell(path, dfop.headData);
            if (cell.type == 'datacol' && cell.sort && dfop.isPage) {
                var isup = true;
                if (!!dfop.sortcell) {
                    dfop.sortcell.obj.find('.jfgrid-heed-sort').hide();
                    if (cell == dfop.sortcell) {
                        var $i = dfop.sortcell.obj.find('.jfgrid-heed-sort .active');
                        $i.removeClass('active');
                        if ($i.hasClass('fa-caret-up')) {
                            dfop.sortcell.obj.find('.jfgrid-heed-sort .fa-caret-down').addClass('active');
                            isup = false;
                        }
                        else {
                            dfop.sortcell.obj.find('.jfgrid-heed-sort .fa-caret-up').addClass('active');
                            isup = true;
                        }
                        dfop.sortcell.obj.find('.jfgrid-heed-sort').show();
                    }
                    else {
                        cell.obj.find('.jfgrid-heed-sort .active').removeClass('active');
                        cell.obj.find('.jfgrid-heed-sort .fa-caret-up').addClass('active');
                        cell.obj.find('.jfgrid-heed-sort').show();
                        isup = true;
                    }
                }
                else {
                    cell.obj.find('.jfgrid-heed-sort .active').removeClass('active');
                    cell.obj.find('.jfgrid-heed-sort .fa-caret-up').addClass('active');
                    cell.obj.find('.jfgrid-heed-sort').show();
                    isup = true;
                }
                dfop.sortcell = cell;
                // 排序调用后台数据
                var $self = $('#' + dfop.id);
                dfop.pageparam.sidx = cell.name;
                dfop.pageparam.sord = isup ? 'ASC' : 'DESC';
                $.jfGrid.reload($self);
            }
        },
        expandSubGrid: function ($et, dfop) {
            var $this = $et;
            if (!$et.hasClass('jfgrid_expend')) {
                $this = $et.find('.jfgrid_expend');
            }
            /*获取当前的行号*/
            var rowId = $et.attr('rownum');
            var $scrollareaContent = $('#jfgrid_scrollarea_content_' + dfop.id);
            var $left = $('#jfgrid_left_' + dfop.id);
            var $lastcolr = $scrollareaContent.find('.jfgrid-data-cell-last[rownum="' + rowId + '"]');
            var $lastcoll = $left.find('.jfgrid-data-cell-last[rownum="' + rowId + '"]');
            if ($lastcoll.next().find('.jfgrid_chlidgird').length > 0) {
                $lastcoll.next().remove();
                $lastcolr.next().remove();
                $this.removeClass('fa-minus');
                $this.addClass('fa-plus');
            }
            else {
                $this.removeClass('fa-plus');
                $this.addClass('fa-minus');
                var subid = 'jfgrid_chlidgird_content_' + dfop.id + '_' + rowId;

                var _html = '<div class="jfgrid-subgird-cell" for-rownum="rowId" style="width:' + dfop._leftWidth + 'px;height:' + dfop.subGridHeight + 'px;" >';
                _html += '<div class="jfgrid_chlidgird" style="width:' + ($('#jfgrid_scrollarea_' + dfop.id).width() + dfop._leftWidth) + 'px;padding-left:' + dfop._borderLeftPadding + 'px;" ><div class="jfgrid_subgird_left" style="width:' + dfop._borderLeftPadding + 'px;" ></div>';
                _html += '<div class="jfgrid-chlidgird-content" id="' + subid + '"></div>'
                _html += '</div></div>';
                $lastcoll.after(_html);
                $lastcolr.after('<div class="jfgrid-data-cell" for-rownum="rowId" style="width:' + dfop._headWidth + 'px;height:' + dfop.subGridHeight + 'px;" ></div>');

                if (!!dfop.subGridRowExpanded) {
                    var datapath = $('#' + dfop.id).find('.jfgrid-hide-cell[rownum="' + rowId + '"]').attr('datapath');
                    var rowdata = $.jfGrid.getRowDataByPath(dfop, datapath);
                    dfop.subGridRowExpanded(subid, rowdata);
                }

            }
        },
        expandTree: function ($et, dfop) {
            var $jfgrid = $('#' + dfop.id);
            var rownum = $et.parent().parent().attr('rownum');
            var datapath = $jfgrid.find('.jfgrid-hide-cell[rownum="' + rownum + '"]').attr('datapath');
            var rowdata = $.jfGrid.getRowDataByPath(dfop, datapath);
            if ($et.hasClass('fa-caret-down')) {// 关闭
                rowdata._isclosed = true;
                hideRow($jfgrid, rowdata.jfGrid_ChildRows);
                $et.removeClass('fa-caret-down');
                $et.addClass('fa-caret-right');
            }
            else {// 展开
                rowdata._isclosed = false;
                showRow($jfgrid, rowdata.jfGrid_ChildRows);
                $et.removeClass('fa-caret-right');
                $et.addClass('fa-caret-down');
            }
            function showRow($jfgrid, data) {
                var dfop = $jfgrid[0].dfop;
                for (var i = 0, l = data.length; i < l; i++) {
                    var rownumindex = 'rownum_' + dfop.id + '_' + data[i].jfgrid_rownum;
                    $jfgrid.find('[rownum="' + rownumindex + '"]').not('.jfgrid-hide-cell').show();
                    if (data[i].jfGrid_ChildRows.length > 0 && !data[i]._isclosed) {
                        var _data = data[i].jfGrid_ChildRows;
                        showRow($jfgrid, _data);
                    }
                }
            }
            function hideRow($jfgrid, data) {
                for (var i = 0, l = data.length; i < l; i++) {
                    var rownumindex = 'rownum_' + dfop.id + '_' + data[i].jfgrid_rownum;
                    $jfgrid.find('[rownum="' + rownumindex + '"]').hide();
                    if (data[i].jfGrid_ChildRows.length > 0 && !data[i]._isclosed) {
                        var _data = data[i].jfGrid_ChildRows;
                        hideRow($jfgrid, _data);
                    }
                }
            }
        },
        /*数据转换*/
        listTotree: function (data, parentId, mainId, dfop) {
            // 只适合小数据计算
            var resdata = [];
            var mapdata = {};
            var mIds = {};
            var pIds = {};
            dfop._maprowdatas = {};
            data = data || [];
            for (var i = 0, l = data.length; i < l; i++) {
                var item = data[i];
                mIds[item[mainId]] = 1;
                mapdata[item[parentId]] = mapdata[item[parentId]] || [];
                mapdata[item[parentId]].push(item);
                if (mIds[item[parentId]] == 1) {
                    delete pIds[item[parentId]];
                }
                else {
                    pIds[item[parentId]] = 1;
                }
                if (pIds[item[mainId]] == 1) {
                    delete pIds[item[mainId]];
                }
            }
            for (var id in pIds) {
                _fn(resdata, id);
            }
            function _fn(_data, vparentId) {
                var pdata = mapdata[vparentId] || [];
                for (var j = 0, l = pdata.length; j < l; j++) {
                    var _item = pdata[j];
                    _item.jfGrid_ChildRows = [];
                    _fn(_item.jfGrid_ChildRows, _item[mainId]);
                    _data.push(_item);
                    dfop._maprowdatas[_item[mainId]] = _item;
                }
            }
            return resdata;
        },
        treeTolist: function (data) {
            var res = [];
            _fn(data);
            function _fn(_data) {
                for (var i = 0, l = _data.length; i < l; i++) {
                    var point = {};
                    for (var _id in _data[i]) {
                        if (_id != 'jfGrid_ChildRows') {
                            point[_id] = _data[i][_id];
                        }
                    }
                    res.push(point);
                    if (_data[i].jfGrid_ChildRows.length > 0) {
                        _fn(_data[i].jfGrid_ChildRows);
                    }
                }
            }
            return res;
        },

        /*获取数据*/
        getSelectedRow: function ($self) {
            var dfop = $self[0].dfop;
            var res;
            var resArray = [];
            $self.find('.jfgrid_selected_' + dfop.id + '[datapath]').each(function () {
                var datapath = $(this).attr('datapath');
                if (!!datapath) {
                    res = $.jfGrid.getRowDataByPath(dfop, datapath);
                    resArray.push(res);
                }
            });
            if (resArray.length > 1) {
                return resArray;
            }
            else {
                return res;
            }
        },
        getRowDataByPath: function (dfop, datapath) {
            var datapaths = datapath.split('.');
            var p = dfop.rowdatas;
            var rowdataone = null;
            for (var i = 0, l = datapaths.length; i < l; i++) {
                rowdataone = p[datapaths[i]];
                if (!rowdataone) {
                    return null;
                }
                p = rowdataone.jfGrid_ChildRows;
            }
            return rowdataone;
        },

        /*设置数据*/
        addTreeRow: function ($self, dfop) {
            var row = dfop.row;
            row.jfGrid_ChildRows = [];
            dfop.rowdatas = dfop.rowdatas || [];
            dfop._maprowdatas = dfop._maprowdatas || {};
            if (dfop._maprowdatas[row[dfop.parentId]]) {
                dfop._maprowdatas[row[dfop.parentId]].jfGrid_ChildRows.push(row);
            }
            else {
                dfop.rowdatas.push(row);
            }
            dfop._maprowdatas[row[dfop.mainId]] = row;
            $.jfGrid.renderTreeRowData($self, dfop.rowdatas);
        },
        addRow: function ($self, dfop) {
            var dfop = $self[0].dfop;
            var row = dfop.row;
            dfop.rowdatas.push(row);
            $.jfGrid.renderRowData($self, dfop.rowdatas);
            dfop = null;
        },
        // 更新行数据
        updateRow: function ($self, dfop) {
            var data = $.jfGrid.getSelectedRow($self);
            if (!!data) {
                $.extend(data, dfop.row);
                if (!dfop.isPage && dfop.isTree) {
                    $.jfGrid.renderTreeRowData($self, dfop.rowdatas);
                }
                else {
                    $.jfGrid.renderRowData($self, dfop.rowdatas);
                }
            }
        },
        // 删除行数据
        removeRow: function ($self, dfop) {
            var datapath = $($self.find('.jfgrid-head-cell-selected[datapath]')[0]).attr('datapath');
            if (!!datapath) {
                var datapaths = datapath.split('.');
                var p = dfop.rowdatas;
                for (var i = 0, l = datapaths.length; i < l; i++) {
                    if (i == (l - 1)) {
                        p.splice(datapaths[i], 1);
                        break;
                    }
                    else {
                        p = p[datapaths[i]].jfGrid_ChildRows;
                    }
                }
                if (!dfop.isPage && dfop.isTree) {
                    $.jfGrid.renderTreeRowData($self, dfop.rowdatas);
                }
                else {
                    $.jfGrid.renderRowData($self, dfop.rowdatas);
                }
            }
        },
        // 弹层
        layer: function (op, callback) {
            var $layerwarp = $('<div class="jfgrid-layer"><div class="jfgrid-layer-bg"></div><div class="jfgrid-layer-content" style="width:' + op.width + 'px;height:' + op.height + 'px;margin:-' + (op.height / 2) + 'px 0 0 -' + (op.width / 2) + 'px;">' + op.html + '<span id="jfgridlayerremove" class="jfgrid-layer-remove"  title="关闭"><i class="fa fa-times"></i></span></div></div>');
            $('body').append($layerwarp);
            $layerwarp.find('#jfgridlayerremove').on('click', function () {
                $layerwarp.remove();
            });
            if (!!callback) {
                callback($layerwarp);
            }
        },

        loading: function ($self, isShow) {
            var dfop = $self[0].dfop;
            var $loading = $('#jfgrid_loading_' + dfop.id);
            if (isShow) {
                $loading.show();
            }
            else {
                $loading.hide();
            }
            dfop = null;
        },
        ajaxLoad: function ($self, url, param, callback) {
            $.jfGrid.loading($self, true);
            // 从服务端加载数据
            $.ajax({
                url: url,
                data: param,
                type: "GET",
                dataType: "json",
                async: true,
                cache: false,
                success: function (data) {
                    callback(data);
                    $.jfGrid.loading($self, false);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    callback({ code: 500 });
                },
                beforeSend: function () {

                },
                complete: function () {
                    $.jfGrid.loading($self, false);
                }
            });
        },

        // 打印处理
        print: function ($self) {
            var dfop = $self[0].dfop;
            console.log(dfop);
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
                    var _row = parseInt(item.obj.css('height')) / 28;
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
                $self.find('[rownum="rownum_' + dfop.id + '_' + index + '"]').each(function () {
                    var $cell = $(this);
                    var colname = $cell.attr('colname');
                    if (!!colname && colname != 'jfgrid_rownum' && colname != 'jfgrid_multiselect' && colname != 'jfgrid_subGrid') {
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

    });
    // 构造方法
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
            datatype: 'array',            // 数据类型
            headData: [],                 // 列数据

            isShowNum: true,              // 是否显示序号
            isMultiselect: false,         // 是否允许多选
            multiselectfield: '',         // 多选绑定字段

            isSubGrid: false,             // 是否有子表
            subGridRowExpanded: false,     // 子表展开后调用函数
            subGridHeight: 300,

            onSelectRow: false,            // 选中一行后回调函数
            onRenderComplete: false,       // 表格加载完后调用

            isPage: false,                 // 是否分页默认是不分页（目前只支持服务端分页）
            pageparam: {
                rows: 50,                 // 每页行数      
                page: 1,                  // 当前页
                sidx: '',                 // 排序列
                sord: '',                 // 排序类型
                records: 0,               // 总记录数
                total: 0                  // 总页数
            },
            sidx: '',
            sord: 'ASC',


            isTree: false,                // 是否树形显示（没有分页的情况下才支持） (只有在数据不多情况下才建议使用)
            mainId: 'id',                 // 关联的主键
            parentId: 'parentId',         // 树形关联字段

            reloadSelected: false,        // 刷新后是否还选择之前选中的,只支持单选

            isAutoHeight: false,          // 自动适应表格高度
            footerrow: false,             // 底部合计条

            isEidt: false,
            minheight: 0,
            height: 0,
            isStatistics: false            // 统计条

        };
        if (!!op) {
            $.extend(dfop, op);
        }
        dfop.id = id;
        $self[0].dfop = dfop;
        dfop.pageparam.sidx = dfop.sidx;
        dfop.pageparam.sord = dfop.sord;


        $.jfGrid.render($self);
        dfop = null;
        return $self;
    };
    // 设置方法
    $.fn.jfGridSet = function (name, op) {
        var $self = $(this);
        var id = $self.attr('id');
        if (id == null || id == undefined || id == '') {
            return null;
        }
        var dfop = $self[0].dfop;


        if (!dfop) {
            return null;
        }
        if (!!op) {
            $.extend(dfop, op);
        }
        switch (name) {
            case 'reload':
                $.jfGrid.reload($self);
                break;
            case 'refreshdata':
                $.jfGrid.renderData($self);
                break;
            case 'addRow':
                if (!dfop.isPage && dfop.isTree) {
                    $.jfGrid.addTreeRow($self, dfop);
                }
                else {
                    $.jfGrid.addRow($self, dfop);
                }
                break;
            case 'updateRow':
                $.jfGrid.updateRow($self, dfop);
                break;
            case 'removeRow':
                $.jfGrid.removeRow($self, dfop);
                break;
        }
        dfop = null;
    };

    function getHeadData(data, res) {
        $.each(data, function (_index, _item) {
            var point = {
                label: _item.label,
                name: _item.name
            };
            res.push(point);
            if (_item.children) {
                point.children = [];
                getHeadData(_item.children, point.children);
            }
        });
    }

    $.fn.jfGridGet = function (name) {
        var $self = $(this);
        var id = $self.attr('id')
        if (id == null || id == undefined || id == '') {
            return null;
        }
        var dfop = $self[0].dfop;
        if (!dfop) {
            return null;
        }
        switch (name) {
            case 'rowdata':
                return $.jfGrid.getSelectedRow($self);
                break;
            case 'rowdatas':
                return dfop.rowdatas;
                break;
            case 'rowdatasByArray':
                return $.jfGrid.treeTolist(dfop.rowdatas);
                break;
            case 'settingInfo':
                return dfop;
                break;
            case 'headData':
                var headData = dfop.headData;
                var res = [];
                getHeadData(headData, res);
                return res;
                break;
            case 'showData':
                var resData = [];
                $.each(dfop.rowdatas, function (index, item) {
                    resData[index] = {};
                    $self.find('[rownum="rownum_' + id + '_' + index + '"]').each(function () {
                        var $cell = $(this);
                        var colname = $cell.attr('colname');
                        if (!!colname && colname != 'jfgrid_rownum') {
                            resData[index][colname] = $cell.text();
                        }
                    });
                });
                return resData;
                break;
        }
        dfop = null;
    };

    $.fn.jfGridValue = function (rowId) {
        var $self = $(this);
        var id = $self.attr('id')
        if (id == null || id == undefined || id == '') {
            return null;
        }
        var dfop = $self[0].dfop;
        if (!dfop) {
            return null;
        }
        var _rowdata = $.jfGrid.getSelectedRow($self);
        if (!!_rowdata) {
            var res = "";
            if (_rowdata.length > 0) {
                $.each(_rowdata, function (id, item) {
                    if (res != "") {
                        res += ',';
                    }
                    res += item[rowId] || '';
                });
                return res;
            }
            else {
                return _rowdata[rowId] || '';
            }
        }
        else {
            return '';
        }
    };

    // 打印处理
    $.fn.jfGridPrint = function () {
        var $self = $(this);
        return $.jfGrid.print($self);
    }
})(window.jQuery)