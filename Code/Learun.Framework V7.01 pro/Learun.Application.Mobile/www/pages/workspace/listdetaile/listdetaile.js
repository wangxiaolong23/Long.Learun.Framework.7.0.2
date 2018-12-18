(function () {
    var page = {
        isScroll: true,
        init: function ($page, param) {
            $page.find('.lr-listdetaile-page-title').html(param.f_title);
            $page.find('.lr-listdetaile-page-content').html($('<div></div>').html(param.f_content).text());
        }
    };
    return page;
})();