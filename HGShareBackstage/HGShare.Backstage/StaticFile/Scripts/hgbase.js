(function ($) {
    /*循环请求*/
    $.LoopAjax = function (opt) {
        var options = {
            type: "get",
            interval: 10000,
            url: "",
            data: {},
            id: "",
            success: function() {},
            error: function() {}
        };
       var setings = $.extend(options,opt);
        function http() {
            $.ajax(setings);
        }
        var iv = setInterval(http, setings.interval);
        return {
            id: iv,
            close: function () {
                clearInterval(this.iv);
            }
        };
    };
})(jQuery);

