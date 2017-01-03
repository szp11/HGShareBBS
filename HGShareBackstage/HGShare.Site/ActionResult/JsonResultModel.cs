namespace HGShare.Site.ActionResult
{
    /// <summary>
    /// 返回Json数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
     public class JsonResultModel<T> : BaseOperationResult
    {
         public T Body { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
     public class JsonResultModel : BaseOperationResult
     {
        public string Action { get; set; }
     }
}
