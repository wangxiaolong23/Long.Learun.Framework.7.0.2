(function () {
    var page = {
        init: function ($page) {
            $page.find('#db2').on('tap', function () {
                learun.storage.set('learuntest', { login: '我是一条登录信息' });
                learun.layer.toast('保存成功');
            });
            $page.find('#db3').on('tap', function () {
                var obj = learun.storage.get('learuntest');
                learun.layer.toast(JSON.stringify(obj));
            });
        }
    };
    return page;
})();