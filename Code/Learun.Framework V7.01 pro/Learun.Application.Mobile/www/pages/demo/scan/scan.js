(function () {
    var page = {
        init: function ($page) {
            $page.find('#scan1').on('tap', function () {
                learun.code.scan(function (res) {
                    if (res.status === 'success') {
                        learun.layer.toast(res.msg);
                    }
                    else {
                        learun.layer.toast('扫描失败:' + res.msg);
                    }
                });
            });
            $page.find('#scan2').on('tap', function () {
                learun.code.encode({ id: 'qrcode1', text:'http://www.learun.cn/'});
            });
        }
    };
    return page;
})();