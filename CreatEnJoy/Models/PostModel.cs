using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreatEnJoy.Models
{
    public class PostModel
    {
        public Guid IDPost { get; set; }
        [Required(ErrorMessage="Mandatory field")]
        public DateTime PostDate { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        [StringLength(250,ErrorMessage ="String too long (max. 250 chars)")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Mandatory field")]
        [StringLength(1000, ErrorMessage = "String too long (max. 1000 chars)")]

        public string Description { get; set; }
        public Guid IDCategory { get; set; }

        public Guid IDMember { get; set; }

    }

}