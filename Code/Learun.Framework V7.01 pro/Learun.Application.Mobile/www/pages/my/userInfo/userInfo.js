(function () {
    var page = {
        isScroll: true,
        init: function ($page) {
            // 获取登录者信息
            var userinfo = learun.storage.get('userinfo');
            var baseinfo = userinfo.baseinfo;

            $page.find('img').attr('src', config.webapi + 'learun/adms/user/img?data=' + baseinfo.userId);


            $page.find('.account').text(baseinfo.account);
            $page.find('.enCode').text(baseinfo.enCode);
            $page.find('.realName').text(baseinfo.realName);
            $page.find('.gender').text(baseinfo.gender == 1 ? '男' : '女');

            if (baseinfo.companyId) {
                learun.clientdata.get('company', {
                    key: baseinfo.companyId,
                    callback: function (data) {
                        $page.find('.company').text(data.name);
                    }
                });
            }

            if (baseinfo.departmentId) {
                learun.clientdata.get('department', {
                    key: baseinfo.departmentId,
                    callback: function (data) {
                        $page.find('.department').text(data.name);
                    }
                });
            }

            var post = [];
            var role = []; 
            $.each(userinfo.post, function (id, item) {
                post.push(item.F_Name);
            });
            $.each(userinfo.role, function (id, item) {
                role.push(item.F_FullName);
            });

            $page.find('.post').text(String(post));
            $page.find('.role').text(String(role));
        }
    };
    return page;
})();