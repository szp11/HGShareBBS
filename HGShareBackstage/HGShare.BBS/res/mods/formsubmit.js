/**

 @Name: 表单异步提交

 */

layui.define(['layer', 'form', 'ajaxs'], function (exports) {

    var $ = layui.jquery,
        layer = layui.layer,
        form = layui.form(),
        ajaxs = layui.ajaxs;
    var gather = {
        /*预留不同的回调场景*/
        successend: {},
        submitpre: {}
    };
    
      //表单提交
    form.on('submit(*)', function (data) {
        
        var action = $(data.form).attr('action'), button = $(data.elem);
        var key = button.attr('key');
        if (gather.submitpre[key]) {
            data = gather.submitpre[key](data);
        } 
        if (gather.submitpre[action]) {
            data = gather.submitpre[action](data);
        }
          var success = function(res) {
              var end = function() {
                  if (res.Action) {
                      location.href = res.Action;
                  } else {
                        (gather.successend[action] && gather.successend[action](data.field, data.form, res))
                        ||
                        (gather.successend[key] && gather.successend[key](data.field, data.form, res));
                  }
              };
              if (res.ResultState) {
                  (button.attr('alert') && res.Message) ? layer.alert(res.Message, {
                      icon: 1,
                      time: 10 * 1000,
                      end: end
                  }) : end();
              };
          };
          
          ajaxs.request(action, data.field, { success: success});
        return false;
      });

      exports('formsubmit', gather);
});

