using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HGShare.BBS.Models.Search
{
    public class CommentSearch
    {
        public int PageIndex { get; set; }
        public long AId { get; set; }
        public int AuthorId { get; set; }
        public int Order { get; set; }
    }
}