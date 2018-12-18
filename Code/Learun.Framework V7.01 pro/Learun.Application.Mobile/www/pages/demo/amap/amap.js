(function () {
    var page = {
        init: function ($page) {
            GaoDe.getCurrentPosition(function (success) {
            }, function (error) {
            });
            var map = new AMap.Map('amapcontainer', {
                center: [117.000923, 36.675807],
                zoom: 15
            });
        }
    };
    return page;
})();