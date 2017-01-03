/*
*
*文章页
*/
layui.define(['formsubmit', 'layedit', 'ajaxs', 'layer', 'form', 'config'], function (exports) {

    var $ = layui.jquery, ajaxs = layui.ajaxs, layer = layui.layer, layedit = layui.layedit,form=layui.form(),config=layui.config;

    

    var dom = {
        editor: $('.fly-editor'),
        commentCount: $("#commentCount"),
        comments: $('#comments'),
        noneComment: $(".none-comment")
    };
    var gather = {
        //评论
        InitComment: function (getcommenturl) {
            //评论框
            //layui.simpleeditor.layEditor({
            //    elem: dom.editor
            //});
            ////评论提交前转义
            //layui.formsubmit.submitpre = {
            //    addcomment: function(data) {
            //        data.field.content = layui.simpleeditor.content(data.field.content);
            //        return data;
            //    }
            //};
            //建立编辑器
            var editindex = layedit.build('comment_content', {
                hideTool: ['image'],
                height:150,
                tool: [
              'strong' //加粗
              , 'italic' //斜体
              , 'underline' //下划线
              , 'del' //删除线

              , '|' //分割线

              , 'left' //左对齐
              , 'center' //居中对齐
              , 'right' //右对齐
              , '|'
              , 'link' //超链接
              , 'unlink' //清除链接
              , '|'
              , 'face' //表情
              , 'image' //插入图片
              , 'code' //代码
              //,'|'
              //, 'html'//源码
                ]
            });
            //内容验证
            form.verify({
                content: function (value) {
                    layedit.sync(editindex);//同步内容
                    var html = layedit.getContent(editindex);
                    if (html.length <= 0)
                        return '请输入内容！';
                    if (html.length < config.contentOption.comment.min)
                        return '内容太过少了，怎么也得' + config.contentOption.comment.min + '个字吧！';
                }
            });
            //表单提交后执行内容
            layui.formsubmit.successend = {
                //添加评论时 追加 清空
                addcomment: function(field, rform, res) {
                    dom.commentCount.html(parseInt(dom.commentCount.html()) + 1);
                    var success = function (resc) {
                        dom.editor.val('');
                        $('.layui-layedit').find('iframe').contents().find('body').html('');
                        dom.noneComment.hide();
                        dom.comments.append(resc);
                        //代码渲染
                        layui.coderendering.show();
                    };
                    $.get(getcommenturl , { commentid: res.Body }, success);
                    //$.post(getcommenturl, { commentid: res.Body }, success);
                }
            };
        },
        //点赞
        InitDianZan: function (aipUrl) {
            var dianZan = function(elem) {
                if (!elem)
                    return;
                var commentId = elem.attr("data-id");
                //True 已被赞 False 未赞
                //已赞时 执行取消 未赞时执行点赞
                var zan = elem.attr("data-zan") == 'False';
                var num = elem.find('em').html();
                var options = {
                    success: function(res) {
                        //成功后 加一/减一 并置为已点赞/未点赞
                        var snum = parseInt(num) + (zan ? 1 : -1);
                        zan ? elem.addClass('zanok') : elem.removeClass('zanok');
                        elem.find('em').html(snum);
                        elem.attr("data-zan", zan ? 'True' : 'False');
                    }
                };
                ajaxs.request(aipUrl, { mid: layui.cache.AId, cid: commentId, zan: zan }, options);
            };
            //绑定点赞事件
            $(".jieda-zan").on("click", function () {
                dianZan($(this));
            });

        },
        //更新点击量
        InitUpdateDot: function(apiUrl,data) {
            $(function() {
                ajaxs.simplepost(apiUrl, data);
            });
        }
    }
    $(function() {
        //设置评论分页的hash
        $(".laypage-main").find('a').each(function (i, item) {
            var href = $(item).attr("href");
            href.indexOf('#comment') === -1 && $(item).attr("href", href + '#comment');
        });
        //图片弹出显示
        layer.ready(function () { 
            layer.photos({
                photos: '.photos'
              , anim: 5 
            });
        });
    });

    exports('aritcledetail', gather);
});
