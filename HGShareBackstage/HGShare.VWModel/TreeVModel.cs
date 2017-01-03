using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGShare.VWModel
{
    public class TreeVModel
    {
        /// <summary>
        /// 显示文本
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// ico样式
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// id对应数据库Modulid
        /// </summary>
        public int dataid { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// id(对应到html id)
        /// </summary>
        public string id {
            get { return "tree_node_" + this.dataid; }
        }
        /// <summary>
        /// pid(在html无对应)
        /// </summary>
        public int pid { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public TreeState state { get; set; }
        /// <summary>
        /// 子集
        /// </summary>
        public List<TreeVModel> children { get; set; }
        /// <summary>
        /// a标签自定义属性
        /// </summary>
        public TreeAttributes a_attr
        {
            get
            {
                return new TreeAttributes()
                {
                    data_id =this.dataid.ToString(CultureInfo.InvariantCulture),
                    data_domid=this.id,
                    data_pid =this.pid.ToString(CultureInfo.InvariantCulture),
                    data_name = this.text
                }; ;
            }
        }
         /// <summary>
        /// li标签自定义属性
        /// </summary>
        public TreeAttributes li_attr
        {
            get
            {
                return new TreeAttributes()
                {
                    data_id = this.dataid.ToString(CultureInfo.InvariantCulture),
                    data_domid = this.id,
                    data_pid =this.pid.ToString(CultureInfo.InvariantCulture),
                    data_name = this.text
                }; ;
            }
        }

        public int[] PIds;
    }
    /// <summary>
    /// 状态
    /// </summary>
    public class TreeState
    {
        /// <summary>
        /// true打开，false关闭
        /// </summary>
        public bool opened { get; set; }
        /// <summary>
        /// true选中，false未选中
        /// </summary>
        public bool selected { get; set; }
        /// <summary>
        /// true禁用，false启用
        /// </summary>
        public bool disabled { get; set; }
    }
    /// <summary>
    /// 自定义属性
    /// </summary>
    public class TreeAttributes
    {
        public string data_id { get; set; }
        public string data_domid { get; set; }
        public string data_pid { get; set; }

        public string data_name { get; set; }
    }

}
