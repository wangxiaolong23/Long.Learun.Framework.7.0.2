(function () {
    var page = {
        isScroll: true,
        init: function ($page) {
            $('#cardswith').on('change', function () {
                var value = $(this).fswitchGet();
                var $list = $('#listdemo2');
                if (value == 1) {
                    $list.addClass('lr-card');
                }
                else {
                    $list.removeClass('lr-card');
                }
            });
            $('#cardswith').lrswitch().lrswitchSet(1);
        }
    };
    return page;
})();