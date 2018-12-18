(function () {
    var page = {
        isScroll: true,
        init: function ($page) {
            $('#select1').lrpicker({
                placeholder: '请选择(必填)',
                data: cityData
            });

            $('#select2').lrpicker({
                placeholder: '请选择(必填)',
                data: cityData,
                level: 2
            });
            $('#select3').lrpicker({
                placeholder: '请选择(必填)',
                data: cityData,
                level: 3
            });

            $('#switch1').lrswitch();

            $('#date1').lrdate();
            $('#date2').lrdate({
                type: 'date',
            });
            $('#date3').lrdate({
                type: 'time',
            });
            $('#date4').lrdate({
                type: 'month',
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




            $('#btnvalid1').on('tap', function () {
                $('#formvalidtest').lrformValid();
            });


            $('#btnvalid2').on('tap', function () {
                var data = $('#formvalidtest').lrformGet();
                learun.layer.toast(JSON.stringify(data));
            });


            $('#btnvalid3').on('tap', function () {
                var _data = {"checkbox1":"1,2,3,4,5,6", "input1": "123123","input3":"11111111111111111111hhhhhhhhhhhhh122222222222", "input2": "12323", "select1": "130000", "select2": "110000,110100", "select3": "110000,110100,110101", "switch1": "1", "date1": "2017-12-20 16:56", "date2": "2017-12-20", "date3": "16:56", "date4": "2017-12" };
                $('#formvalidtest').lrformSet(_data);
            });
        }
    };
    return page;
})();