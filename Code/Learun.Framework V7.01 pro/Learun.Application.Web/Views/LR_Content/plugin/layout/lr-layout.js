/*
 * 版 本 Learun-ADMS V7.0.0 力软敏捷开发框架(http://www.learun.cn)
 * Copyright (c) 2013-2018 上海力软信息技术有限公司
 * 创建人：力软-前端开发组
 * 日 期：2017.04.12
 * 描 述：lrLayout 页面布局插件（目前支持左右布局，新的布局请联系力软售后人员）
 */
(function ($, learun) {
    "use strict";

    $.fn.lrLayout = function (op) {
        var dfop = {
            blocks: [
                {
                    target: '.lr-layout-left',
                    type: 'right',
                    size: 203
                }
            ]
        };
        $.extend(dfop, op || {});
        var $this = $(this);
        if ($this.length <= 0) {
            return false;
        }
        $this[0]._lrLayout = { "dfop": dfop };
        dfop.id = "lrlayout" + new Date().getTime();

        for (var i = 0, l = dfop.blocks.length; i < l; i++) {
            var _block = dfop.blocks[i];
            $this.children(_block.target).append('<div class="lr-layout-move lr-layout-move-' + _block.type + ' " path="' + i + '"  ></div>');
        }

        $this.on('mousedown', function (e) {
            var et = e.target || e.srcElement;
            var $et = $(et);
            var $this = $(this);
            var dfop = $this[0]._lrLayout.dfop;
            if ($et.hasClass('lr-layout-move')) {
                var _index = $et.attr('path');
                dfop._currentBlock = dfop.blocks[_index];
                dfop._ismove = true;
                dfop._pageX = e.pageX;
            }
        });

        $this.mousemove(function (e) {
            var $this = $(this);
            var dfop = $this[0]._lrLayout.dfop;
            if (!!dfop._ismove) {
                var $block = $this.children(dfop._currentBlock.target);
                $block.css('width', dfop._currentBlock.size + (e.pageX - dfop._pageX));
                $this.css('padding-left', dfop._currentBlock.size + (e.pageX - dfop._pageX));
            }
        });

        $this.on('click', function (e) {
            var $this = $(this);
            var dfop = $this[0]._lrLayout.dfop;
            if (!!dfop._ismove) {
                dfop._currentBlock.size += (e.pageX - dfop._pageX);
                dfop._ismove = false;
            }
        });
    }
})(jQuery, top.learun);