
//文章实时处理
layui.define(['ajaxs'], function (exports) {
    var $ = layui.jquery, ajaxs = layui.ajaxs;

    var gather = {
        InitDynamicDot: function (apiUrl) {
            $(function() {
                var ids = [];
                $('.article_dot').each(function (i, item) {
                    ids.push($(item).attr('data-id'));
                });
                ajaxs.simplepost(apiUrl, { ids: ids }, {
                    success: function (res) {
                        if (res.Body) {
                            for (var i = 0; i < res.Body.length; i++) {
                                $('.article_dot[data-id=' + res.Body[i].AId + ']').html(res.Body[i].Dot);
                            }
                        }
                    }
                });
            });
        }
    };
    exports('articlerealtimehandle', gather);
});