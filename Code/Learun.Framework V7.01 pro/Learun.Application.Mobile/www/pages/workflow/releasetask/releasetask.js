(function () {

    var page = {
        grid: null,
        init: function ($page) {
            // 初始化列表翻页
            page.grid = $page.find('#lr_releasetask_list').lrpagination({
                lclass: page.lclass,
                rows: 10,
                getData: function (param, callback) {
                    page.loadData(param, callback, $page);
                },
                renderData: function (_index, _item, _$item) {// 渲染数据模板
                    return page.rowRender(_index, _item, _$item, $page);
                },
                click: function (item, $item) {// 列表行点击事件
                    page.rowClick(item, $item, $page);
                }
            });
            // 点击搜索按钮
            $page.find('#lr_releasetask_search_btn').on('tap', function () {
                learun.nav.go({ path: 'search', title: '', isBack: true, isHead: true, param:'workflow/releasetask' });// 告诉搜索页本身所在的地址
            });
        },
        lclass: 'lr-list',
        loadData: function (param, callback, $page) {// 列表加载后台数据
            var _postParam = {
                pagination: {
                    rows: param.rows,
                    page: param.page,
                    sidx: 'F_Name',
                    sord: 'DESC'
                },
                queryJson: '{}'
            };
            if (param.keyword) {
                _postParam.queryJson = JSON.stringify({ keyword: param.keyword });
            }
            learun.httpget(config.webapi + "learun/adms/workflow/scheme", _postParam, (data) => {
                $page.find('.lr-badge').text('0');
                if (data) {
                    $page.find('.lr-badge').text(data.records);
                    callback(data.rows, parseInt(data.records));
                }
                else {
                    callback([], 0);
                }
            });
        },
        rowRender: function (_index, _item, _$item, $page) {// 渲染列表行数据
            var _html = '\
            <div class="lr-list-item lr-list-item-icon2">\
                <div class="lr-icon"><i class="iconfont icon-form"></i></div>\
                <div class="lr-nav-multi">\
                    <span>'+ _item.F_Name+'</span>\
                    <span>'+ _item.F_Code +'</span>\
                </div>\
            </div>';
            return _html;
        },
        rowClick: function (item, $item, $page) {// 列表行点击触发方法
            learun.nav.go({ path: 'workflow/bootstraper', title: '发起【' + item.F_Name+'】', type: 'right', param: { schemeCode: item.F_Code } });
        },
        rowBtns:[] // 列表行左滑按钮
    };
    return page;
})();