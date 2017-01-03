/*
* 头像上传
* 
*
*/
layui.define(['layer', 'cropper'], function (exports) {
    var $ = layui.jquery,
        layer = layui.layer,
        ajaxs = layui.ajaxs;
        layui.cropper($);
    var uploadavatar = {
        /*
        *upbtn 保存按钮 
        *imgbox 图片容器
        *upurl 提交地址
        *updata 提交数据
        */
        Init: function(selectbtn,upbtn,img,upurl,updata,imgmaxsize,cpoption) {
            var $upbtn = $("#" + upbtn);
            var $image = $("#" + img);
            if (!$image.cropper)
                return;
            var defaultcpoption = {
                aspectRatio: 1 / 1,
                preview: '.avatar-preview',
                crop: function (e) {},
                viewMode: 1
            };
            $image.cropper($.extend(defaultcpoption, cpoption));

            // 图片选择
            var $inputImage = $('#' + selectbtn);
            var url = window.URL || window.webkitURL;
            var blobUrl;

            if (url) {
                $inputImage.change(function () {
                    var files = this.files;
                    var file;
                    if (!$image.data('cropper')) {
                        return;
                    }
                    if (files && files.length) {
                        file = files[0];
                        if (/^image\/\w+$/.test(file.type)) {
                            blobUrl = url.createObjectURL(file);
                            $image.one('built.cropper', function () {
                                // Revoke when load complete
                                url.revokeObjectURL(blobUrl);
                            }).cropper('reset').cropper('replace', blobUrl);
                            $inputImage.val('');
                        } else {
                            layer.msg('不支持该类型图片!', { shift: 6 });
                        }
                    }
                });
            } else {
                $inputImage.prop('disabled', true).parent().addClass('disabled');
            }

            
            var completepre = function () {
                $upbtn.prop('disabled', false);
            }
            var success = function (res) {
                layer.alert(res.Message, {
                    icon: 1,
                    time: 10 * 1000,
                    end: function() {
                        window.location.reload();
                    }
                });
            };
            
            $upbtn.on("click", function () {
                $(this).prop('disabled', true);
                
              var result = $image.cropper('getCroppedCanvas');
              var imageBase64 = result.toDataURL('image/jpeg');
              if (imageBase64.length > imgmaxsize) {
                    layer.msg("图片太大了，换个小图试试！", { shift: 6 });
                    return;
                }
                var data = {imageBase64:imageBase64};
              ajaxs.request(upurl, $.extend(data, updata), { success: success, completepre: completepre });
          });
      }
    };
exports('uploadavatar', uploadavatar);
});