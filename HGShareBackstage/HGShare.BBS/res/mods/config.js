
//全局配
layui.define(function (exports) {
    var config = {
        //ajaxs加载中load配置
        lodingOption: {
            shade: [0.1, '#999']
        },
        //用户中心分页配置
        userCenterOption: {
            page_groups: 5
        },
        refreshUserInfoInterval: 20000,
        //内容限制
        contentOption: {
            article: {
                min: 20
            },
            comment: {
                min: 10
            }
        },
        //验证码配置
        verifycodeOption: {
            apiurl: '',
            logingimg:''
        }
    };
    exports('config', config);

});