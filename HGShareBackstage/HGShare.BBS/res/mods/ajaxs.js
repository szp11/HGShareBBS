/*
*
*异步请求集合
*/
layui.define(['layer', 'config'], function (exports) {

    var $ = layui.jquery,
        layer = layui.layer, config = layui.config;
    var closeloding = function() { layer.closeAll('loading'); };
    var gather = {
        //Ajax
        request: function (url, data, options) {
            layer.load(1, config.lodingOption);
            options = options || {};
            data = data || {};
            return $.ajax({
                type: options.type || 'post',
                dataType: options.dataType || 'json',
                data: data,
                url: url,
                success: function (res) {
                    closeloding();
                    options.completepre && options.completepre(res);
                    if (res.ResultState) {
                        options.success && options.success(res);
                    } else {
                        layer.msg(res.Message || res.Code, { shift: 6 });
                        options.fail && options.fail();
                    }
                    options.complete && options.complete(res);
                },
                error: function (e) {
                    closeloding();
                    options.completepre && options.completepre(e);
                    options.error || layer.msg('请求异常，请重试', { shift: 6 });
                    options.complete && options.complete(e);
                }
            });
        },
        get: function(url, data, options) {
            layer.load(1, config.lodingOption);
            options = options || {};
            data = data || {};
            $.get(url, data, function (res, status) {
                closeloding();
                if (status == 'success') {
                    if (res.ResultState) {
                        options.success && options.success(res);
                    } else {
                        layer.msg(res.Message|| '操作失败，请重试！', { shift: 6 });
                        options.error && options.error(res);
                    }
                } else {
                    var msg = '请求异常，请重试(' + status + ')';
                    layer.msg(msg, { shift: 6 });
                    options.error && options.error(res);
                }

            }, options.dataType || 'json');

        },
        simplepost: function (url, data, options) {
            options = options || {};
            data = data || {};
            $.post(url, data, function (res, status) {
                if (status == 'success') {
                    if (res.ResultState) {
                        options.success && options.success(res);
                    } else {
                        options.error && options.error(res);
                    }
                } else {
                    options.error && options.error(res);
                }

            });

        },
        loopHttp :function (opt) {
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
        }
    };


    exports('ajaxs', gather);
});
