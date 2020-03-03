using CreatEnJoy.Models;
using CreatEnJoy.Models.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatEnJoy.ViewModels
{
    public class PostCategoryViewModel
    {
        public Guid IDCategoryPost { get; set; }
        public Guid IDCategory { get; set; }
        public Guid IDPost { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public List<PostModel> Posts = new List<PostModel>();
    }
}