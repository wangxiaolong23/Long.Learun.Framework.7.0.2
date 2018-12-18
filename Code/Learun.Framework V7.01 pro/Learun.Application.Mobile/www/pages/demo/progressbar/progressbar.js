(function () {
    var page = {
        isScroll: true,
        init: function ($page) {
            $page.find('.setProgressbar').on('tap', function () {
                var name = $(this).text().replace(/%/g, "");
                $('#progressbar1').progressSet(parseInt(name));
            });
        }
    };
    return page;
})();