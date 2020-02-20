using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatEnJoy.Models
{
    public class CategoryMemberModel
    {
        public Guid IDCategoryMember { get; set; }
        public Guid IDCategory { get; set; }
        public Guid IDMember { get; set; }
    }
}