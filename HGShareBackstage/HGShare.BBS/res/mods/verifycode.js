
/*
* 验证码
*/
layui.define(['config'], function(exports) {
    var $ = layui.jquery,config=layui.config;

    var verifyCode = function() {
        return {
            options: {
                imgurl: '',
                boxelem: '.vercodeimg',
                triggerelem: '#Vercode',
                loadingimg: '',
                triggerevent: 'focus'
            },
            TriggerInit: function(opt) {
                var _this = this;
                $(_this.options.triggerelem).unbind(_this.options.triggerevent);
                var boxelem = $(_this.options.boxelem);
                if (!boxelem)
                    return;
                var codeimg = $('<img src="' + _this.options.imgurl + '" style="display:none;" class="verifycodeimg" alt="验证码，点击刷新！" />');
                var logingimg = $('<img src="' + _this.options.loadingimg + '" class="verifycodeloadingimg" alt="验证码，加载中！" />');
                var randomUrl=function(src) {
                    if (src.indexOf('?_=') > -1) {
                        src = src.replace(/\?_=\d+\.\d+/, '?_=' + Math.random());
                    } else {
                        src = src + '?_=' + Math.random();
                    }
                    return src;
                }
                codeimg.on('load', function() {
                    logingimg.hide();
                    codeimg.show();
                });
                codeimg.on('click', function() {
                    $(_this.options.triggerelem).val('');
                    $(this).hide();
                    logingimg.show();
                    codeimg.attr("src", randomUrl(_this.options.imgurl));
                   
                });
                boxelem.append(codeimg).append(logingimg);
                boxelem.show();
            },
            Init: function(opt) {
                var _this = this;
                //全局配置
                _this.options.imgurl = config.verifycodeOption.apiurl;
                _this.options.loadingimg = config.verifycodeOption.logingimg;
                //自定义
                _this.options = $.extend(_this.options, opt);

                $(_this.options.triggerelem) && $(_this.options.triggerelem).on(_this.options.triggerevent, function() { _this.TriggerInit(opt) });
            }
        };
    }
    //默认加载
    new verifyCode().Init();
    exports('verifycode', function(opt){
		var code= new verifyCode();
		code.Init(opt);
		return code;
	});
});