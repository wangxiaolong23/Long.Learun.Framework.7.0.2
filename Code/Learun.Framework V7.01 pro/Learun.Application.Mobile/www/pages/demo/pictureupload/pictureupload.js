(function () {
    var page = {
        isScroll: true,
        init: function ($page) {
            $page.find('#picture1').imagepicker();
        }
    };
    return page;
})();