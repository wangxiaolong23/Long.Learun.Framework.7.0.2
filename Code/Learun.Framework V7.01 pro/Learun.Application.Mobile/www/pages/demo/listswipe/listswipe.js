(function () {
    var page = {
        isScroll: true,
        init: function ($page) {
            $page.find('.item1').lrlistswipe({
                btns: ['<a class="lr-btn-danger">删除</a>']
            });

            $page.find('.item2').lrlistswipe({
                btns: ['<a class="lr-btn-warning"><i class="iconfont icon-emoji"></i></a>']
            });

            $page.find('.item3').lrlistswipe({
                btns: ['<a class="lr-btn-danger">删除</a>', '<a class="lr-btn-warning"><i class="iconfont icon-emoji"></i></a>']
            });

            $page.find('.lr-list-item-media').lrlistswipe({
                btns: ['<a class="lr-btn-danger">删除</a>', '<a class="lr-btn-warning"><i class="iconfont icon-emoji"></i></a>']
            });
        }
    };
    return page;
})();