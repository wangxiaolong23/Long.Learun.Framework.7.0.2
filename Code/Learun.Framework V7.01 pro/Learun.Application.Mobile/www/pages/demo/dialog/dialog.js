(function () {
    var page = {
        init: function ($page) {
            $page.find('#dialog1').on('tap', function () {
                learun.layer.warning('欢迎使用力软框架', function () { }, '力软提示', '好的');
            });
            $page.find('#dialog2').on('tap', function () {
                learun.layer.confirm('力软框架很不错', function (_index) { }, '力软提示', ['否', '是']);
            });
            $page.find('#dialog3').on('tap', function () {
                learun.layer.popinput('请评价一下力软框架', function (_index, _input) { }, '力软提示', ['取消', '确定'],'性能好');
            });
            $page.find('#dialog4').on('tap', function () {
                learun.layer.toast('欢迎使用力软框架');
            });
            $page.find('#dialog5').on('tap', function () {
                learun.layer.loading(true, '数据加载中');
                setTimeout(function () {
                    learun.layer.loading(false);
                }, 3000);
            });
        }
    };
    return page;
})();