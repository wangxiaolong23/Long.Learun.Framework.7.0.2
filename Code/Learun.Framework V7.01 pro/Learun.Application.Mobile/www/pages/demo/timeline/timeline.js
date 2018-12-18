(function () {
    var page = {
        isScroll: false,
        init: function ($page) {
            $page.find('#timeline1').ftimeline([
                {
                    title: '标题一',
                    people: '审核人1',
                    content: '审核通过了该流程',
                    time: '2018-01-08 07:49:08'
                },
                {
                    title: '标题二',
                    people: '审核人2',
                    content: '审核通过了该流程',
                    time: '2018-01-09 07:49:08'
                },
                {
                    title: '标题三',
                    people: '审核人3',
                    content: '审核通过了该流程',
                    time: '2018-01-10 07:49:08'
                },
                {
                    title: '标题四',
                    people: '审核人4',
                    content: '审核通过了该流程',
                    time: '2018-01-11 07:49:08'
                }
            ]);
        }
    };
    return page;
})();