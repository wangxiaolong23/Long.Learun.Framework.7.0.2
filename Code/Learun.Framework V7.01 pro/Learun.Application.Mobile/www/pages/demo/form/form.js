(function () {
    var page = {
        isScroll: true,
        init: function ($page) {
            $page.find('#select1').lrpicker({
                placeholder: '请选择(必填)',
                data: cityData
            });

            $page.find('#select2').lrpicker({
                placeholder: '请选择(必填)',
                data: cityData,
                level: 2
            });
            $page.find('#select3').lrpicker({
                placeholder: '请选择(必填)',
                data: cityData,
                level: 3
            });

            $page.find('#switch1').lrswitch();

            $page.find('#date1').lrdate();
            $page.find('#date2').lrdate({
                type: 'date'
            });
            $page.find('#date3').lrdate({
                type: 'time'
            });
            $page.find('#date4').lrdate({
                type: 'month'
            });

            $page.find('#checkbox1').lrcheckbox({
                data: [{
                    text: '第一项',
                    value: '1'
                }, {
                    text: '第二项',
                    value: '2'
                }, {
                    text: '第三项',
                    value: '3'
                }, {
                    text: '第四项',
                    value: '4'
                }, {
                    text: '第五项',
                    value: '5'
                }, {
                    text: '第六项',
                    value: '6'
                }, {
                    text: '第五项',
                    value: '7'
                }, {
                    text: '第五项',
                    value: '8'
                }, {
                    text: '第五项',
                    value: '9'
                }, {
                    text: '第五项',
                    value: '10'
                }, {
                    text: '第五项',
                    value: '11'
                }, {
                    text: '第五项',
                    value: '12'
                }, {
                    text: '第五项',
                    value: '13'
                }, {
                    text: '第五项',
                    value: '14'
                }, {
                    text: '第五项',
                    value: '15'
                }, {
                    text: '第五项',
                    value: '16'
                }, {
                    text: '第五项',
                    value: '17'
                }]
            });


        }
    };
    return page;
})();