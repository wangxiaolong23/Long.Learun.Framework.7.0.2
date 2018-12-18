(function () {
    var page = {
        isScroll: false,
        init: function ($page) {
            $page.find('#toptab').toptab(['待办公文', '已办公文', '全部公文']).each(function (index) {
                var $this = $(this);
                switch (index) {
                    case 0:
                        $this.html('待办公文');
                        break;
                    case 1:
                        $this.html('已办公文');
                        break;
                    case 2:
                        $this.html('全部公文');
                        break;
                }
                $this = null;
            });
        }
    };
    return page;
})();