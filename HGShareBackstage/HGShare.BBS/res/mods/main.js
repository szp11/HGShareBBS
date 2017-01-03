/**

 @Name: 社区基础公共设施

 */
layui.define(['layer', 'tools', 'coderendering','config'], function (exports) {
    var $ = layui.jquery,
        layer = layui.layer,
        device = layui.device(),
        tools = layui.tools;
    //代码渲染
    layui.coderendering.show();
	//阻止IE7以下访问
	if (device.ie && device.ie < 8) {
		layer.alert('如果您非得使用ie浏览Fly社区，那么请使用ie8+');
	}

	var gather = {
	    cookie: tools.cookie,
        //插入右下角悬浮bar
	    rbar : function () {
	        var style = $('head').find('.fly-style'),
			skin = {
			    stretch : 'charushuipingxian'
			};
	        var html = $('<ul class="fly-rbar">'
					 + '<li class="iconfont icon-' + (skin[gather.cookie('fly-style')] || 'huizongzuoyoutuodong') + '" method="' + (gather.cookie('fly-style') === 'stretch' ? 'reset' : 'stretch') + '"></li>'
					 + '<li id="F_topbar" class="iconfont icon-top" method="top"></li>'
					 + '</ul>');
	        var dict = {
	            stretch : function (othis) {
	                style.attr('href', style.data('href'));
	                othis.attr({
	                    method : 'reset',
	                    'class' : 'iconfont icon-charushuipingxian'
	                });
	                gather.cookie('fly-style', 'stretch', {
	                    path : '/',
	                    expires : 365
	                });
	            },
	            reset : function (othis) {
	                style.removeAttr('href');
	                othis.attr({
	                    method : 'stretch',
	                    'class' : 'iconfont icon-huizongzuoyoutuodong'
	                });
	                gather.cookie('fly-style', null, {
	                    path : '/'
	                });
	            },
	            top : function (othis) {
	                $('html,body').animate({
	                    scrollTop : 0
	                }, 100, function () {
	                    othis.hide();
	                });
	            }
	        };

	        $('body').append(html);

	        //事件
	        html.find('li').on('click', function () {
	            var othis = $(this),
				method = othis.attr('method');
	            dict[method].call(this, othis);
	        });

	        //滚动
	        var log = 0,
			topbar = $('#F_topbar'),
			scroll = function () {
			    var stop = $(window).scrollTop();
			    if (stop >= 200) {
			        if (!log) {
			            topbar.show();
			            log = 1;
			        }
			    } else {
			        if (log) {
			            topbar.hide();
			            log = 0;
			        }
			    }
			    return arguments.callee;
			}
			();
	        $(window).on('scroll', scroll);
	    }
    };

    ////搜索提交
	//$('.fly-search').submit(function () {
	//	var input = $(this).find('input'), val = input.val();
	//	if (val.replace(/\s/g, '') === '') {
	//		return false;
	//	}
	//	input.val('site:layui.com ' + input.val());
	//});
	////搜索点击
	//$('.icon-sousuo').on('click', function () {
	//	$('.fly-search').submit();
	//});


    //插入右下角bar
	gather.rbar();
  
	exports('main', {});
});