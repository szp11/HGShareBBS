/**

 @Name: 文章类型树

 */

layui.define(['jstree'], function (exports) {
    var $ = layui.jquery;
    //jstree
    layui.jstree($);
    var gather = {
        Init: function(elem,options) {
            var  tree = $(elem).jstree({
                "core": {
                    "multiple": false,
                    "data": options.data.trees
                },
                "checkbox": {
                    "keep_selected_style": true, //是否显示节点的选中样
                    "three_state": false//选择时是否保持其它选择
                },
                "plugins": ["checkbox"]
            }).on('click.jstree', function (event) {
                //点击
            }).on('changed.jstree', function (e, data) {
                //选中
                var selected = data.instance.get_node(data.selected[0]);
                if (selected) {
                    var id = selected.li_attr.data_id;
                    var name = selected.li_attr.data_name;
                    options.changed && options.changed(id, name);
                } else {
                    options.cancle && options.cancle();
                }
            }).on('ready.jstree', function () {
                /*修改时，默认选中已选节点*/
                options.data.pid != undefined && options.data.pid !== '' && tree.jstree().select_node(["tree_node_" + options.data.pid]);
                /*禁用不可选节点*/
                options.data.did != undefined && options.data.did !== '' && tree.jstree().disable_node(["tree_node_" + options.data.did]);
                options.ready && options.ready(tree);
            });
        }
    };
    exports('atypetree', gather);
});

