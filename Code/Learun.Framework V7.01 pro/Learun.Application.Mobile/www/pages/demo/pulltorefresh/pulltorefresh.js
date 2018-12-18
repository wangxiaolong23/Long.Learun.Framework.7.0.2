(function () {
    var page = {
        init: function ($page, param) {
            var pageobj = $('#pulltorefreshlist').lrpagination({
                lclass: "lr-list",
                rows: 10,                            // 每页行数
                getData: function (param, callback) {// 获取数据 param 分页参数,callback 异步回调
                    // param:   page: 当前页,rows:行数
                    setTimeout(function () {
                        var data = [];
                        for (var i = 0; i < 10; i++) {
                            data.push({ text: '第' + ((param.page - 1) * param.rows + i + 1) + '行数据' });
                        }
                        callback(data, 20);
                    }, 1000);
                },
                renderData: function (_index, _item) {// 渲染数据模板
                    var _html = '<div class="lr-list-item"><a class="lr-nav-right" >' + _item.text + '</a ></div >';
                    return _html;
                },
                down: {
                    contentinit: '下拉可以刷新',
                    contentdown: '下拉可以刷新',
                    contentover: '释放立即刷新',
                    contentrefresh: '正在刷新...'
                },
                up: {
                    contentinit: '上拉显示更多',
                    contentdown: '上拉显示更多',
                    contentrefresh: '正在加载...',
                    contentnomore: '没有更多数据了'
                },
                click: function (item, $item) {// 列表行点击事件
                    
                }
            });
           // 重新刷新列表
           /*setTimeout(function () {
               pageobj.reload();
           }, 2000);*/
        }
    };
    return page;
})();
