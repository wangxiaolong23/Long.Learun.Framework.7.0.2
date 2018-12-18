(function () {
    var page = {
        init: function ($page) {
            $page.find('#getpage1').on('tap', function () {
                var prepage = learun.nav.getpage('demo');
                if (prepage) {
                    learun.layer.toast('获取成功');
                } else {
                    learun.layer.toast('获取失败');
                }
            });
        }
    };
    return page;
})();