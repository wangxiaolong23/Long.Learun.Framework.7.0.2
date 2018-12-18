(function () {
    var page = {
        init: function ($page) {
            // 获取登录者信息
            var userinfo = learun.storage.get('userinfo');
            $page.find('.name').text(userinfo.baseinfo.realName);

            $page.find('img').attr('src', config.webapi + 'learun/adms/user/img?data=' + userinfo.baseinfo.userId);

            // 人员列表数据初始化
            learun.clientdata.get('user', {
                key: userinfo.baseinfo.departmentId,
                callback: function (data) {
                    $page.find('.subname').text(data.name || '总集团公司');
                }
            });
            learun.code.encode({ id: 'lr_mycode_qrcode', text: 'http://www.learun.cn/' });
        }
    };
    return page;
})();