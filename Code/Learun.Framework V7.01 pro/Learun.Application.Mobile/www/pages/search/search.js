(function () {
    var page = {
        init: function ($page, param) {
            var _html = '';
            _html += '<div class="searchbar">';
            _html += '<i class="iconfont icon-search"></i>';
            _html += '<input type="text" placeholder="搜寻关键字">';
            _html += '</div><span id="lr_pagedata_search_ok" >搜索</span><span id="lr_pagedata_search_chanel" >取消</span>';
            $page.parent().find('.f-page-header').addClass('lr-pagedata-search-header').addClass('nodata').html(_html);

            $page.parent().find('#lr_pagedata_search_chanel').on('tap', function () {
                learun.nav.closeCurrent();
            });

            $page.parent().find('input').select();

            // 获取上一页面对象
            var prepage = learun.nav.getpage(param);

            // 初始化列表
            page.grid = $page.find('#lr_pagedata_searchlist').lrpagination({
                lclass: prepage.lclass,
                rows: 20,
                getData: function (param, callback) {
                    param.keyword = $page.parent().find('.searchbar input').val();
                    if ($.trim(param.keyword)) {
                        prepage.loadData(param, function (rows, records) {
                            if (records > 0) {
                                $page.parent().find('.f-page-header').removeClass('nodata');
                                $page.find('#lr_pagedata_searchlist').show();
                                $page.find('.lr-pagedata-search-bg').hide();
                            }
                            else {
                                $page.parent().find('.f-page-header').addClass('nodata');
                                $page.find('#lr_pagedata_searchlist').hide();
                                $page.find('.lr-pagedata-search-bg').show();
                            }
                            callback(rows, records);
                        }, $page);
                    } else {
                        callback([], 0);
                        if (!$page.parent().find('.f-page-header').hasClass('nodata')) {
                            $page.parent().find('.f-page-header').addClass('nodata');
                        }
                        $page.find('#lr_pagedata_searchlist').hide();
                        $page.find('.lr-pagedata-search-bg').show();
                    }
                },
                renderData: function (_index, _item, _$item) {// 渲染数据模板
                    return prepage.rowRender(_index, _item, _$item, $page);
                },
                click: function (item, $item) {// 列表行点击事件
                    if (prepage.click) {
                        prepage.click(item, $item, $page);
                    } else if (prepage.rowClick) {
                        prepage.rowClick(item, $item, $page);
                    }
                }
            });


            // 点击搜索按钮
            $page.parent().find('#lr_pagedata_search_ok').on('tap', function () {
                page.grid.reload();
            });

        },
        reload: function ($page, pageinfo) {
            page.grid.reload();
        }
    };
    return page;
})();