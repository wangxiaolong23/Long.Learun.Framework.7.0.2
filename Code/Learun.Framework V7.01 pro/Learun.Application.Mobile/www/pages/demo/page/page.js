(function () {
    var page = {
        isScroll: false,
        init: function ($page) {
            $page.find('#pageNewBtn1').on('tap', function () {
                learun.nav.go({ path: 'demo/page/cpage', title: '我是标题', isBack: true, isHead: true, param: '我是一个参数', type: 'right' });
            });

            $page.find('#pageNewBtn2').on('tap', function () {
                learun.nav.go({ path: 'demo/page/cpage', title: '我是标题', isBack: true, isHead: true, param: '我是一个参数' });
            });

            $page.find('#pageNewBtn3').on('tap', function () {
                learun.nav.go({ path: 'demo/page/cpage', title: '我是标题', isBack: true, isHead: true, param: '我是一个参数', type: 'bottom' });
            });

            $page.find('#pageCloseBtn').on('tap', function () {
                learun.nav.closeCurrent();
                //learun.nav.close('demo/page');

            });
        },
        beforedestroy: function (pageinfo) {
            return true;// false 就不关闭
        },
        destroy: function (pageinfo) {
        },
        reload: function ($page, pageinfo) {
        }
    };
    return page;
})();