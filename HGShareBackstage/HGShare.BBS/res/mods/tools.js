/*
 * 工具箱 操作
 */
layui.define(function (exports) {
    var $ = layui.jquery;
    Date.prototype.Format = function (fmt) { //author: meizz   
        var o = {
            "M+": this.getMonth() + 1,                 //月份   
            "d+": this.getDate(),                    //日   
            "h+": this.getHours(),                   //小时   
            "m+": this.getMinutes(),                 //分   
            "s+": this.getSeconds(),                 //秒   
            "q+": Math.floor((this.getMonth() + 3) / 3), //季度   
            "S": this.getMilliseconds()             //毫秒   
        };
        if (/(y+)/.test(fmt))
            fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        for (var k in o)
            if (new RegExp("(" + k + ")").test(fmt))
                fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        return fmt;
    }
    var gather = {
        //cookie 操作
        cookie: function (e, o, t) {
            e = e || ""; var n, i, r, a, c, p, s, d, u; if ("undefined" == typeof o) { if (p = null, document.cookie && "" != document.cookie) for (s = document.cookie.split(";"), d = 0; d < s.length; d++) if (u = $.trim(s[d]), u.substring(0, e.length + 1) == e + "=") { p = decodeURIComponent(u.substring(e.length + 1)); break } return p } t = t || {}, null === o && (o = "", t.expires = -1), n = "", t.expires && ("number" == typeof t.expires || t.expires.toUTCString) && ("number" == typeof t.expires ? (i = new Date, i.setTime(i.getTime() + 864e5 * t.expires)) : i = t.expires, n = "; expires=" + i.toUTCString()), r = t.path ? "; path=" + t.path : "", a = t.domain ? "; domain=" + t.domain : "", c = t.secure ? "; secure" : "", document.cookie = [e, "=", encodeURIComponent(o), n, r, a, c].join("");
        },
        //json Date(142000000) 格式化
        jsonvalue2date: function (value) {
            var reg = /\(\d+\)/;
            var date = eval(value.match(reg)[0]);
            return new Date(date).Format('yyyy-MM-dd hh:mm:ss');
        }
    };
    exports('tools', gather);
});