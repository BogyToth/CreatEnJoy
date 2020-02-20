using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatEnJoy.Models
{
    public class CategoryPostModel
    {
        public Guid IDCategoryPost { get; set; }
        public Guid IDCategory { get; set; }
        public Guid IDPost { get; set; }
    }
}