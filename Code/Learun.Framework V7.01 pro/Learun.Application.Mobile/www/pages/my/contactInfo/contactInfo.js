(function () {
    var page = {
        isScroll: true,
        init: function ($page) {
            // 获取登录者信息
            var userinfo = learun.storage.get('userinfo');
            var baseinfo = userinfo.baseinfo;

            $page.find('.mobile').text(baseinfo.mobile || '');
            $page.find('.telephone').text(baseinfo.telephone || '');
            $page.find('.email').text(baseinfo.email || '');
            $page.find('.weChat').text(baseinfo.weChat || '');
            $page.find('.oICQ').text(baseinfo.oICQ || '');
        }
    };
    return page;
})();