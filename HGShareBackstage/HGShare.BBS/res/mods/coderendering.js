/*
*
*pre code 块渲染
*/
layui.define(function (exports) {
    var $ = layui.jquery;
    var gather = {
        //Ajax
        show: function() {
            $('pre').each(function (i, item) {
                var pre = $(item);
                var preClass = pre.attr('class');
                var lang = pre.attr('lay-lang');
                //没有设置语言
                if (!lang)
                    return;
                //已经渲染过了
                if (preClass && preClass.indexOf('language-') > -1)
                    return;
                var html = $(item).html();
                $(item).html('');

                $(item).html('<code class="language-' + lang + '">' + html + '</code>');
            });

            Prism && Prism.highlightAll(true, function () { console.log("code 渲染完毕"); });
        }
    };
    exports('coderendering', gather);
});
