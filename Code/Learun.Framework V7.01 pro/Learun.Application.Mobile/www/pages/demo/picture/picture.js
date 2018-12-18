(function () {
    var page = {
        isScroll: true,
        init: function ($page) {
            $page.find('#picture1').slider({ data: ['images/1.jpg', 'images/2.jpg', 'images/3.jpg', 'images/4.jpg'], indicator: true });//
        }
    };
    return page;
})();