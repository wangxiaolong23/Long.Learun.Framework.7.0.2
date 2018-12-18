/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力 软信息技术有限公司
 * 创建人：力 软-前端开发组
 * 日 期：2018.04.19
 * 描 述：滚动条优化
 */
(function ($, learun, window) {
    "use strict";
    var $move = null;

    var methods = {
        init: function ($this, callback) {
            var id = $this.attr('id');
            if (!id) {
                id = 'lr_' + learun.newGuid();
                $this.attr('id', id);
            }

            $this.addClass('lr-scroll-wrap');
            // 加载内容
            var $content = $this.children();

            var $scroll = $('<div class="lr-scroll-box" id="' + id + '_box" ></div>');
            $this.append($scroll);
            $scroll.append($content);

            // 加载y滚动条
            var $vertical = $('<div class="lr-scroll-vertical"   ><div class="lr-scroll-vertical-block" id="' + id + '_vertical"></div></div>')
            $this.append($vertical);

            // 加载x滚动条
            var $horizontal = $('<div class="lr-scroll-horizontal" ><div class="lr-scroll-horizontal-block" id="' + id + '_horizontal"></div></div>')
            $this.append($horizontal);

            // 添加一个移动板
            if ($move === null) {
                $move = $('<div style="-moz-user-select: none;-webkit-user-select: none;-ms-user-select: none;-khtml-user-select: none;user-select: none;display: none;position: fixed;top: 0;left: 0;width: 100%;height: 100%;z-index: 9999;cursor: pointer;" ></div>');
                $('body').append($move);
            }
            // 初始化数据
            var sh = $scroll.innerHeight();
            var sw = $scroll.innerWidth();


            var h = $this.height();
            var w = $this.width();
            var data = {
                id: id,
                sy: 0,
                sx: 0,
                sh: sh,
                sw: sw,
                h: h,
                w: w,
                yh: 0,
                xw: 0,
                callback: callback
            };
            $this[0].op = data;
            methods.update($this);
            methods.bindEvent($this, $scroll, $vertical, $horizontal);

            $scroll = null;
            $content = null;
            $vertical = null;
            $horizontal = null;
            $this = null;
        },
        bindEvent: function ($this, $scroll, $vertical, $horizontal) { // 绑定监听事件
            // div大小变化
            $this.resize(function () {
                var $this = $(this);
                var op = $this[0].op;
                var h = $this.height();
                var w = $this.width();
                if (h != op.h) {
                    op.h = h;
                    methods.updateY($this);
                }
                if (w != op.w) {
                    op.w = w;
                    methods.updateX($this);
                }
                $this = null;
            });
            $scroll.resize(function () {
                var $this = $(this);
                var $scrollWrap = $this.parent();
                var op = $scrollWrap[0].op;
                var sh = $this.innerHeight();
                var sw = $this.innerWidth();

                if (sh != op.sh) {
                    op.sh = sh;
                    methods.updateY($scrollWrap);
                }
                if (sw != op.sw) {
                    op.sw = sw;
                    methods.updateX($scrollWrap);
                }
                $this = null;
                $scrollWrap = null;
            });

            // 监听鼠标滚动
            $this.mousewheel(function (event, delta, deltaX, deltaY) {

                var $this = $(this);
                var op = $this[0].op;
                var d = delta * 4;
                if (op.sh > op.h) {
                    op.oldsy = op.sy;
                    op.sy = op.sy - d;
                    methods.moveY($this, true);
                    $this = null;
                    return false;
                } else if (op.sw > op.w) {
                    op.oldsx = op.sx;
                    op.sx = op.sx - d;
                    methods.moveX($this, true);
                    $this = null;
                    return false;
                }
            });

            // 监听鼠标移动
            $vertical.find('.lr-scroll-vertical-block').on('mousedown', function (e) {
                $move.show();
                var $this = $(this).parent().parent();
                var op = $this[0].op;
                op.isYMousedown = true;
                op.yMousedown = e.pageY;
                $this.addClass('lr-scroll-active');
                $this = null;
            });
            $horizontal.find('.lr-scroll-horizontal-block').on('mousedown', function (e) {
                $move.show();
                var $this = $(this).parent().parent();
                var op = $this[0].op;
                op.isXMousedown = true;
                op.xMousedown = e.pageX;
                $this.addClass('lr-scroll-active');
                $this = null;
            });


            top.$(document).on('mousemove', { $obj: $this }, function (e) {
                var op = e.data.$obj[0].op;
                if (op.isYMousedown) {
                    var y = e.pageY;
                    var _yd = y - op.yMousedown;
                    op.yMousedown = y;
                    op.oldsy = op.sy;
                    op.blockY = op.blockY + _yd;

                    if ((op.blockY + op.yh) > op.h) {
                        op.blockY = op.h - op.yh;
                    }
                    if (op.blockY < 0) {
                        op.blockY = 0;
                    }
                    methods.getY(op);
                    methods.moveY(e.data.$obj);
                }
                else if (op.isXMousedown) {
                    var op = e.data.$obj[0].op;
                    var x = e.pageX;
                    var _xd = x - op.xMousedown;
                    op.xMousedown = x;
                    op.oldsx = op.sx;
                    op.blockX = op.blockX + _xd;
                    if ((op.blockX + op.xw) > op.w) {
                        op.blockX = op.w - op.xw;
                    }
                    if (op.blockX < 0) {
                        op.blockX = 0;
                    }
                    methods.getX(op);
                    methods.moveX(e.data.$obj);
                }
            }).on('mouseup', { $obj: $this }, function (e) {
                e.data.$obj[0].op.isYMousedown = false;
                e.data.$obj[0].op.isXMousedown = false;
                $move.hide();
                e.data.$obj.removeClass('lr-scroll-active');
            });
        },
        update: function ($this) { // 更新滚动条
            methods.updateY($this);
            methods.updateX($this);
        },
        updateY: function ($this) {
            var op = $this[0].op;
            var $scroll = $this.find('#' + op.id + '_box');
            var $vertical = $this.find('#' + op.id + '_vertical');
            if (op.sh > op.h) { // 出现纵向滚动条
                // 更新显示区域位置
                if ((op.sh - op.sy) < op.h) {
                    var _sy = 0;
                    op.sy = op.sh - op.h;
                    if (op.sy < 0) {
                        op.sy = 0;
                    } else {
                        _sy = 0 - op.sy;
                    }
                    $scroll.css('top', _sy + 'px');
                }
                // 更新滚动条高度
                var scrollH = parseInt(op.h * op.h / op.sh);
                scrollH = (scrollH < 30 ? 30 : scrollH);
                op.yh = scrollH;

                // 更新滚动条位置
                var _y = parseInt(op.sy * (op.h - scrollH) / (op.sh - op.h));
                if ((_y + scrollH) > op.h) {
                    _y = op.h - scrollH;
                }
                if (_y < 0) {
                    _y = 0;
                }

                op.blockY = _y;

                // 设置滚动块大小和位置
                $vertical.css({
                    'top': _y + 'px',
                    'height': scrollH + 'px'
                });
            } else {
                op.blockY = 0;
                op.sy = 0;
                $scroll.css('top', '0px');
                $vertical.css({
                    'top': '0px',
                    'height': '0px'
                });
            }

            op.callback && op.callback(op.sx, op.sy);
            $scroll = null;
            $vertical = null;
        },
        updateX: function ($this) {
            var op = $this[0].op;
            var $scroll = $this.find('#' + op.id + '_box');
            var $horizontal = $this.find('#' + op.id + '_horizontal');
            if (op.sw > op.w) {
                // 更新显示区域位置
                if ((op.sw - op.sx) < op.w) {
                    var _sx = 0;
                    op.sx = op.sw - op.w;
                    if (op.sx < 0) {
                        op.sx = 0;
                    } else {
                        _sx = 0 - op.sx;
                    }
                    $scroll.css('left', _sx + 'px');
                }
                // 更新滚动条高度
                var scrollW = parseInt(op.w * op.w / op.sw);
                scrollW = (scrollW < 30 ? 30 : scrollW);
                op.xw = scrollW;

                // 更新滚动条位置
                var _x = parseInt(op.sx * (op.w - scrollW) / (op.sw - op.w));
                if ((_x + scrollW) > op.w) {
                    _x = op.w - scrollW;
                }
                if (_x < 0) {
                    _x = 0;
                }
                op.blockX = _x;
                // 设置滚动块大小和位置
                $horizontal.css({
                    'left': _x + 'px',
                    'width': scrollW + 'px'
                });

            } else {
                op.sx = 0;
                op.blockX = 0;
                $scroll.css('left', '0px');
                $horizontal.css({
                    'left': '0px',
                    'width': '0px'
                });
            }
            op.callback && op.callback(op.sx, op.sy);
            $scroll = null;
            $horizontal = null;
        },
        moveY: function ($this, isMousewheel) {
            var op = $this[0].op;
            var $scroll = $this.find('#' + op.id + '_box');
            var $vertical = $this.find('#' + op.id + '_vertical');

            // 更新显示区域位置
            var _sy = 0;
            if (op.sy < 0) {
                op.sy = 0;
            } else if (op.sy + op.h > op.sh) {
                op.sy = op.sh - op.h;
                _sy = 0 - op.sy;
            } else {
                _sy = 0 - op.sy;
            }
            if (isMousewheel) {
                var _y = methods.getBlockY(op);
                if (_y == 0 && op.sy != 0) {
                    op.sy = 0;
                    _sy = 0;
                }
                op.blockY = _y;
                // 设置滚动块位置
                //var d = Math.abs(op.sy - op.oldsy) * 100 / 4;
                $scroll.css({
                    'top': _sy + 'px'
                });
                $vertical.css({
                    'top': _y + 'px'
                });
            } else {
                $scroll.css({
                    'top': _sy + 'px'
                });
                $vertical.css({
                    'top': op.blockY + 'px'
                });
            }
            op.callback && op.callback(op.sx, op.sy);
            $scroll = null;
            $vertical = null;
        },
        moveX: function ($this, isMousewheel) {
            var op = $this[0].op;
            var $scroll = $this.find('#' + op.id + '_box');
            var $horizontal = $this.find('#' + op.id + '_horizontal');

            // 更新显示区域位置
            var _sx = 0;
            if (op.sx < 0) {
                op.sx = 0;
            } else if (op.sx + op.w > op.sw) {
                op.sx = op.sw - op.w;
                _sx = 0 - op.sx;
            } else {
                _sx = 0 - op.sx;
            }

            if (isMousewheel) {
                // 更新滑块的位置
                var _x = methods.getBlockX(op);
                if (_x == 0 && op.sx != 0) {
                    op.sx = 0;
                    _sx = 0;
                }
                op.blockX = _x;
                // 设置滚动块位置
                //var d = Math.abs(op.sx - op.oldsx) * 100 / 4;
                $scroll.css({
                    'left': _sx + 'px'
                });
                $horizontal.css({
                    'left': _x + 'px'
                });
            } else {
                $scroll.css({
                    'left': _sx + 'px'
                });
                $horizontal.css({
                    'left': op.blockX + 'px'
                });
            }
            op.callback && op.callback(op.sx, op.sy);
            $scroll = null;
            $horizontal = null;

        },
        getBlockY: function (op) {
            var _y = parseFloat(op.sy * (op.h - op.yh) / (op.sh - op.h));
            if ((_y + op.yh) > op.h) {
                _y = op.h - op.yh;
            }
            if (_y < 0) {
                _y = 0;
            }
            return _y;
        },
        getY: function (op) {
            op.sy = parseInt(op.blockY * (op.sh - op.h) / (op.h - op.yh));
            if ((op.sy + op.h) > op.sh) {
                op.sy = op.sh - op.h;
            }
            if (op.sy < 0) {
                op.sy = 0;
            }
        },
        getBlockX: function (op) {
            var _x = parseFloat(op.sx * (op.w - op.xw) / (op.sw - op.w));
            if ((_x + op.xw) > op.w) {
                _x = op.w - op.xw;
            }
            if (_x < 0) {
                _x = 0;
            }
            return _x;
        },
        getX: function (op) {
            op.sx = parseInt(op.blockX * (op.sw - op.w) / (op.w - op.xw));
            if ((op.sx + op.w) > op.sw) {
                op.sx = op.sw - op.w;
            }
            if (op.sx < 0) {
                op.sx = 0;
            }
        },
    };
    $.fn.lrscroll = function (callback) {
        $(this).each(function () {
            var $this = $(this);
            methods.init($this, callback);
        });
    }

    $.fn.lrscrollSet = function (name, data) {
        switch (name) {
            case 'moveRight':
                var $this = $(this);
                setTimeout(function () {
                    var op = $this[0].op;
                    op.oldsx = op.sx;
                    op.sx = op.sw - op.w;
                    methods.moveX($this, true);
                    $this = null;
                }, 250);
                break;
            case 'moveBottom':
                var $this = $(this);
                setTimeout(function () {
                    var op = $this[0].op;
                    op.oldsy = op.sx;
                    op.sy = op.sh - op.h;
                    methods.moveY($this, true);
                    $this = null;
                }, 250);
                break;
        }
    }

})(window.jQuery, top.learun, window);