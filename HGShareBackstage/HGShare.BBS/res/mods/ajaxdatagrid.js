/*
*
*数据列表
*/
layui.define(['config', 'laypage', 'ajaxs', 'laytpl'], function (exports) {

    var $ = layui.jquery,
        ajaxs = layui.ajaxs,
        laypage = layui.laypage,
        config = layui.config;
    var gather = {
        laypage_options: {
            cont: 'lay-page',
            pages: 0,
            groups: config.userCenterOption.page_groups,
            curr:1
        },
        ajax_options: {
            datalist_api: '',
            datalist_opt: {},
            datacount_api: '',
            datacount_opt: {}
        },
        other_options: {
            datatemplate:'',
            datagridbox:''
        },
        callback:null
        ,
        Init: function (laypageoptions, ajaxoptions,otheroptions,callback) {
            $.extend(this.laypage_options, {curr:1});
            $.extend(this.laypage_options, laypageoptions);
            $.extend(this.ajax_options, ajaxoptions);
            $.extend(this.other_options, otheroptions);
            this.ShowPage();
            this.ShowData(1);
            this.callback = callback;
        },
        ShowPage: function () {
            var _this = this;
            var success = function (res) {
                $.extend(_this.laypage_options, { pages: res.Body, jump: function (obj, first) { !first && _this.ShowData(obj.curr); } });
                laypage(_this.laypage_options);
            };
            ajaxs.request(_this.ajax_options.datacount_api, _this.ajax_options.datacount_opt, { success: success });
        },
        ShowData: function(page) {
            var _this = this;
            var success = function (res) {
                layui.laytpl($(_this.other_options.datatemplate).html()).render(res.Body, function (html) {
                    $(_this.other_options.datagridbox).html(html);
                    _this.callback && _this.callback();
                });
                laypage(_this.laypage_options);
            };
            $.extend(_this.ajax_options.datalist_opt, { pageIndex: page });
            ajaxs.request(_this.ajax_options.datalist_api, _this.ajax_options.datalist_opt, { success: success });
        }
    };

    exports('ajaxdatagrid', gather);
});
