/*
* 用户设置
*/
layui.define(['layer'], function (exports) {
    var $ = layui.jquery;

    //tab切换
    var gather = {},
        dom = { mine: $('#LAY-mine'), mineview: $('.mine-view') };
    dom.mineas=dom.mine.find('a');
    //显示当前tab
    gather.tabshow = function (index, hash) {
        //hash参数
        if (hash) {
            dom.mineas.each(function (i, item) {
                if ($(this).attr('hash') === hash) {
                    index = i;
                    return false;
                }
            });
        }
        //隐藏显示
        dom.mineas.eq(index).addClass('tab-this').siblings().removeClass('tab-this');
        dom.mineview.hide();
        dom.mineview.eq(index).show();
    };
    //按钮事件
    dom.mineas.on('click', function () {
        var othis = $(this), index = othis.index(), hash = othis.attr('hash');
        if (othis.attr('href') !== 'javascript:;') {
            return;
        }

        gather.tabshow(index);

        if (hash) {
            location.hash = hash;
        }
    });
    dom.mine[0] && gather.tabshow(0, location.hash.replace(/^#/, ''));
    
    exports('userset', gather);
});