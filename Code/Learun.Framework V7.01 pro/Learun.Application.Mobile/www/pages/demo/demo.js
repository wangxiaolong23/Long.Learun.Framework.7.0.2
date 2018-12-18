/*页面js模板,必须有init方法*/

(function () {
    var page = {
        isScroll: true,
        init: function ($page) {
            $('#demolist .lr-nav-right').on('tap', function () {
                var $this = $(this);
                var path = 'demo/' + $this.attr('data-value');
                var title = $this.text();
                learun.nav.go({ path: path, title: title, type: 'right' });
            });
        }
    };
    return page;
})();
