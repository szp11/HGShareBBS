/*
 * 已登陆访问，执行脚本
 */
layui.define(['config', 'ajaxs'], function (exports) {
    var ajaxs = layui.ajaxs,
        config = layui.config;

    var gather = {
        //定时刷新用户缓存信息，较耗资源
        timingRefreshUserInfo: function (url) {
            ajaxs.loopHttp({ url: url, type: "post", interval: config.refreshUserInfoInterval });
        }
    };
    exports('loggedin', gather);
});