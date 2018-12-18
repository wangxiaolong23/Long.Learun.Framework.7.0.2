(function () {
    var page = {
        isScroll: false,
        init: function ($page, param) {
            $page.find('#cpageTitle').text(param);
            $page.find('#pageGoTab').on('tap', function () {
                learun.tab.go('demo');
            });
        }
    };
    return page;
})();