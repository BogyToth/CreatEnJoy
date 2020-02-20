using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreatEnJoy.Models
{
    public class CategoryModel
    {
        public Guid IDCategory { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        [StringLength(100, ErrorMessage = "String too long (max. 100 chars)")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        [StringLength(250, ErrorMessage = "String too long (max. 250 chars)")]
        public int NumberPosts { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        [StringLength(1000, ErrorMessage = "String too long (max. 1000 chars)")]
        public string Description { get; set; }

        public string ImageURL { get; set; }
    }
}