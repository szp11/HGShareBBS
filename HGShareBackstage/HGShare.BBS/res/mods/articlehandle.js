/*
*
*发表/编辑 文章
*/
layui.define(['layer', 'layedit', 'formsubmit', 'ajaxs', 'atypetree', 'form', 'config', 'verifycode'], function (exports) {
    var $ = layui.jquery,
    ajaxs = layui.ajaxs,
    layer = layui.layer,
    form = layui.form(),
    layedit = layui.layedit,
    config = layui.config;
    layui.jstree($);

    var editindex = layedit.build('content', { hideTool: ['image'] ,tool: [
  'strong' //加粗
  ,'italic' //斜体
  ,'underline' //下划线
  ,'del' //删除线
  
  ,'|' //分割线
  
  ,'left' //左对齐
  ,'center' //居中对齐
  , 'right' //右对齐
  ,'|'
  ,'link' //超链接
  , 'unlink' //清除链接
  ,'|'
  ,'face' //表情
  ,'image' //插入图片
  , 'code' //代码
  //,'|'
  //, 'html'//源码
    ]
    }); //建立编辑器

    form.verify({
        content: function (value) {
            layedit.sync(editindex);//同步内容
            var html = layedit.getContent(editindex);
            if (html.length <= 0)
                return '请输入内容！';
            if (html.length < config.contentOption.article.min)
                return '内容太过少了，怎么也得' + config.contentOption.article.min + '个字吧！';
        }
    });

    //提交前同步内容
    //layui.formsubmit.submitpre["addarticle"]= function() {
    //    layedit.sync(editindex);
    //}
    
    var options = {
        treeDataApi: '',
        displayNameElem: null,
        displayIdElem: null,
        chengElem: null
    };
    var init = function (option) {
        options = $.extend(options, option);
        //类型选中
        var typetreechanged = function (id, name) {
            $(options.displayIdElem).val(id);
            $(options.displayNameElem).val(name);
        }
        var typetreecancle= function() {
            $(options.displayIdElem).val('');
            $(options.displayNameElem).val('');
        }
        //ajax 加载treedata成功
        var gettreedatasuc = function (res) {
            //加载层
            layer.open({
                title: "选择类型",
                area: ['300px', '500px'],
                type: 1,
                shift: 2,
                shadeClose: true,
                content: '<div id="using_tree"></div>',
                btn: ['关闭']
            });
            var selectnode = $(options.displayIdElem).val();
            
            //初始化树
            layui.atypetree.Init("#using_tree", { changed: typetreechanged, cancle: typetreecancle, data: { trees: res.Body, did: 0, pid: selectnode } });
        };
        //加载类型
        var showtypes = function () {
            ajaxs.get(options.treeDataApi,
                    {},
                    {
                        dataType: 'json',
                        success: gettreedatasuc
                    }
                );
        };
        //选择
        $(options.chengElem).on('click', showtypes);
    }
    exports('articlehandle', init);
});
