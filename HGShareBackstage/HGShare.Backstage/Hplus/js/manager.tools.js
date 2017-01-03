/*列表页*/
var ListTools = {
    Delete: {
        option: { url: null, type: "POST", btn: null, time: 1000 },
        Init: function (opt) {
            var _thistools = this;
            _thistools.option = $.extend(_thistools.option, opt);
            if (!_thistools.validata())
                return false;
            $(_thistools.option.btn).on("click", function () {
                var _this = this;
                var conf = layer.confirm('确定删除该信息？', {
                    btn: ['确定', '取消'] //按钮
                }, function () {
                    layer.close(conf);
                    tabTools.TabLoging();
                    $(_this).attr({ "disabled": "disabled" }).html("提交中...");
                    $.ajax({
                        url: _thistools.option.url,
                        type: _thistools.option.type,
                        data: { id: $(_this).attr("data-id") },
                        success: function (result) {
                            tabTools.TabLogingClose();
                            if (!result.ResultState) {
                                $(_this).removeAttr("disabled").html("删除");
                                layer.alert(result.Message ? result.Message : '删除失败！', { icon: 2 });
                            } else {
                                var a = layer.alert('删除成功！', { icon: 1 }, function () {
                                    layer.close(a);
                                    tabTools.TabLoging();
                                    JianRong.Reload();
                                });
                                setTimeout(function () { layer.close(a); tabTools.TabLoging(); JianRong.Reload(); }, _thistools.option.time);
                            }
                        },
                        error: function (xmlHttpRequest, textStatus, error) {
                            tabTools.TabLogingClose();
                            $(_this).removeAttr("disabled").html("删除");
                            layer.alert(error, { icon: 2 });
                        }
                    });
                });
            });
            return true;
        },
        validata: function () {
            var _this = this;
            if (!_this.option.url) {
                layer.alert("请配置删除请求路径！");
                return false;
            } else if (!_this.option.btn) {
                layer.alert("请配置触发删除事件按钮！");
                return false;
            } else
                return true;

        }
    },
    Deletes: {
        option: { url: null, type: "POST", btn: null, itemname: null, time: 1000 },
        Init: function (opt) {
            var _thistools = this;
            _thistools.option = $.extend(_thistools.option, opt);
            if (!_thistools.validata())
                return false;
            $(_thistools.option.btn).on("click", function () {
                var _this = this;
                var conf = layer.confirm('确定删除选中信息？', {
                    btn: ['确定', '取消'] //按钮
                }, function () {
                    layer.close(conf);
                    //取值
                    var ids = [];
                    $("input[name='" + _thistools.option.itemname + "']:checked").each(function (i, item) {
                        ids.push($(item).val());
                    });
                    if (ids.length == 0) {
                        layer.msg("请选中要删除的数据再执行！", { shift: 6 });
                        return false;
                    }
                    tabTools.TabLoging();
                    $(_this).attr({ "disabled": "disabled" });
                    $.ajax({
                        url: _thistools.option.url,
                        type: _thistools.option.type,
                        data: { ids: ids },
                        success: function (result) {
                            tabTools.TabLogingClose();
                            if (!result.ResultState) {
                                $(_this).removeAttr("disabled");
                                layer.alert(result.Message ? result.Message : '删除失败！', { icon: 2 });
                            } else {
                                var a = layer.alert('删除成功！', { icon: 1 }, function () {
                                    layer.close(a);
                                    tabTools.TabLoging();
                                    JianRong.Reload();
                                });
                                setTimeout(function () {
                                    layer.close(a);
                                    tabTools.TabLoging();
                                    JianRong.Reload();
                                }, _thistools.option.time);
                            }
                        },
                        error: function (xmlHttpRequest, textStatus, error) {
                            tabTools.TabLogingClose();
                            $(_this).removeAttr("disabled");
                            layer.alert(error, { icon: 2 });
                        }
                    });
                });
            });
            return true;
        },
        validata: function () {
            var _this = this;
            if (!_this.option.url) {
                layer.alert("请配置删除请求路径！");
                return false;
            } else if (!_this.option.btn) {
                layer.alert("请配置触发删除事件按钮！");
                return false;
            } else if (!_this.option.itemname) {
                layer.alert("请配置待选择删除项name属性！");
                return false;
            } else
                return true;

        }
    },
    /*待删除 2016年12月2日17:39:54 名字有歧义，请使用SelectBox*/
    DeleteSelectBox: {
        option: { btn: null, itemname: null },
        Init: function (opt) {
            var _thistools = this;
            _thistools.option = $.extend(_thistools.option, opt);
            if (!_thistools.validata())
                return false;
            $(_thistools.option.btn).on("click", function () {
                var items = $("input[name='" + _thistools.option.itemname + "']");
                if ($(this).is(":checked")) {
                    items.prop("checked", true);
                } else {
                    items.prop("checked", false);
                }
            });
            return true;
        },
        validata: function () {
            var _this = this;
            if (!_this.option.btn) {
                layer.alert("请配置删除选择触发元素！");
                return false;
            } else if (!_this.option.itemname) {
                layer.alert("请配置待选择删除项name属性！");
                return false;
            }
            return true;
        }
    },
    /*选中复选按钮*/
    SelectBox: {
        option: { btn: null, itemname: null },
        Init: function (opt) {
            var _thistools = this;
            _thistools.option = $.extend(_thistools.option, opt);
            if (!_thistools.validata())
                return false;
            $(_thistools.option.btn).on("click", function () {
                var items = $("input[name='" + _thistools.option.itemname + "']");
                if ($(this).is(":checked")) {
                    items.prop("checked", true);
                } else {
                    items.prop("checked", false);
                }
            });
            return true;
        },
        validata: function () {
            var _this = this;
            if (!_this.option.btn) {
                layer.alert("请配置删除选择触发元素！");
                return false;
            } else if (!_this.option.itemname) {
                layer.alert("请配置待选择删除项name属性！");
                return false;
            }
            return true;
        }
    },
    /*选中复选的值 数组*/
    SelectBoxValues: function(name) {
        var ids = [];
        $("input[name='" + name + "']:checked").each(function (i, item) {
            ids.push($(item).val());
        });
        return ids;
    },
    //刷新页面
    PageReload: {
        option: { btn: null },
        Init: function (opt) {
            var _thistools = this;
            _thistools.option = $.extend(_thistools.option, opt);
            $(_thistools.option.btn).on("click", function () {
                tabTools.TabPageReload();
            });
        }

    },
    /**批量操作**/
    BatchAction: {
        option: {
            btn: null, // 触发按钮
            itemname: 'item', // 选择项name
            confirm: true, // 是否确认
            isinputtext: false, // 是否输入文字
            inputtexttitle: '请输入原因', // 输入文字提示文字
            callback: null // 回调函数
        },
        Init: function (opt) {
            var _this = this;
            
            _this.option = $.extend(_this.option, opt);
            if (_this.option.btn == null)
                return;
            var callback = function () {
                if (_this.option.isinputtext) {
                    layer.prompt({ title: _this.option.inputtexttitle, formType: 2 }, function (text, index) {
                        layer.close(index);
                        _this.option.callback && _this.option.callback(ids);
                    });
                } else {
                    _this.option.callback && _this.option.callback(ids);
                }
            };
            var commfunc = function() {
                var ids = ListTools.SelectBoxValues(_this.option.itemname);
                if (ids.length == 0) {
                    layer.msg("请选择数据！", { shift: 6 });
                    return;
                }
                if (_this.option.confirm) {
                    var conf = layer.confirm(
                        '确定执行该操作？',
                        { btn: ['确定', '取消'] },
                        function() {
                            layer.close(conf);
                            callback();
                        }
                    );
                } else {
                    callback();
                }
            };
            $(_this.option.btn).on("click", commfunc);
        }
    }
};
/*表单验证*/
var ValidataTools = {
    option: {
        backurl: "",
        form: null,
        submitbtn: null,
        btntext: "提交",
        errorico: '<i class="fa fa-times-circle"></i> ',
        time: 1000,
        tipstype: "添加"
    },
    Init: function (opt) {
        var _thistools = this;
        _thistools.option = $.extend(_thistools.option, opt);
        if (!_thistools.Validata())
            return false;
        if ($.validator.unobtrusive)
            $.validator.unobtrusive.errorico = _thistools.option.errorico;
        /*客户端验证*/
        $.validator.setDefaults({
            highlight: function (a, b, c) {
                $(a).closest(".form-group").removeClass("has-success").addClass("has-error");
                $(a).nextAll(".glyphicon").removeClass("glyphicon-ok").addClass("glyphicon-remove");
            },
            unhighlight: function (a, b, c) {
                $(a).closest(".form-group").removeClass("has-error").addClass("has-success");
                $(a).nextAll(".glyphicon").removeClass("glyphicon-remove").addClass("glyphicon-ok");
            },
            errorElement: "span",
            errorClass: "help-block m-b-none",
            validClass: "valid",
            submitHandler: function (a, b, c) {
                tabTools.TabLoging();
                _thistools.option.btntext = $(_thistools.option.submitbtn).html();
                //按钮处理
                $(_thistools.option.submitbtn).attr({ "disabled": "disabled" }).html("提交中...");
                $(_thistools.option.form).ajaxSubmit({
                    success: function (result) {
                        tabTools.TabLogingClose();
                        //登陆结果判断
                        if (!result.ResultState) {
                            layer.alert(result.Message ? result.Message : _thistools.option.tipstype + '失败！', { icon: 2 });
                        } else {
                            var a = layer.alert(_thistools.option.tipstype + '成功！', { icon: 1 }, function () {
                                layer.close(a);
                                tabTools.TabLoging();
                                window.location.href = _thistools.option.backurl;
                            });
                            setTimeout(function () { layer.close(a); tabTools.TabLoging(); window.location.href = _thistools.option.backurl; }, _thistools.option.time);
                        }
                        //按钮处理
                        $(_thistools.option.submitbtn).removeAttr("disabled").html(_thistools.option.btntext);
                    },
                    error: function (xmlHttpRequest, textStatus, error) {
                        tabTools.TabLogingClose();
                        layer.alert(error, { icon: 2 });
                        $(_thistools.option.submitbtn).removeAttr("disabled").html(_thistools.option.btntext);
                    }
                });
            }
        });
        return true;
    },
    Validata: function () {
        var _this = this;
        if (!_this.option.backurl) {
            layer.alert("请配置返回路径！");
            return false;
        }
        if (!_this.option.form) {
            layer.alert("请配置表单对象！");
            return false;
        }
        if (!_this.option.submitbtn) {
            layer.alert("请配置提交按钮对象！");
            return false;
        }
        return true;
    }
};
/*异步请求*/
var hgajax = {
    post: function (url, opt, callback) {
        tabTools.TabLoging();
        $.ajax({
            url: url,
            type: "POST",
            data: opt,
            success: function (result) {
                tabTools.TabLogingClose();
                if (!result.ResultState) {
                    layer.alert(result.Message ? result.Message : '操作失败！', { icon: 2 });
                    callback && callback(result);
                } else {
                    var a = layer.alert('操作成功！', { icon: 1 }, function () {
                        layer.close(a);
                        callback && callback(result);
                    });
                }
            },
            error: function (xmlHttpRequest, textStatus, error) {
                tabTools.TabLogingClose();
                layer.alert(error, { icon: 2 });
            }
        });
    },
    postNoAlert: function (url, opt, callback) {
        tabTools.TabLoging();
        $.ajax({
            url: url,
            type: "POST",
            data: opt,
            success: function (result) {
                tabTools.TabLogingClose();
                callback && callback(result);
            },
            error: function (xmlHttpRequest, textStatus, error) {
                tabTools.TabLogingClose();
                layer.alert(error, { icon: 2 });
            }
        });
    }
}
/**
 * Switchery tools
 **/
var switcheryTools = {
    InitItems: function(elem,change) {
        var elems = Array.prototype.slice.call(document.querySelectorAll(elem));
        elems.forEach(function (checkbox) {
            var switchery = new Switchery(checkbox);
            checkbox.onchange = change;
        });
    }
};
/*兼容方法*/
var JianRong = {
    /* window.location.reload()  穿透缓存*/
    Reload: function () {
        var url = window.location.href;
        if (url.indexOf("?") > -1) {
            if (url.indexOf("&_r=") > -1) {
                var ran = url.match(/_r=[0-9]\d*\.\d+/)[0];
                url = url.replace(ran, "_r=" + Math.random());
            }
            else
                url += "&_r=" + Math.random();
        } else
            url += "?_r=" + Math.random();
        window.location.href = url;
    }
};
/*继承父窗体方法
 * 如果页面被单独访问，则自己加载
 */
var tabTools, layer;
if (parent.tabTools)
    tabTools = parent.tabTools;
else
    document.write('<script type="text/javascript" src="../hplus/js/contabs.min.js"></script>');
if (parent.layer)
    layer = parent.layer;
else
    document.write('<script type="text/javascript" src="../hplus/js/plugins/layer/layer.js"></script>');