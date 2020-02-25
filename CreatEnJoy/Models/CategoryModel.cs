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
        public string Description { get; set; }

        public string ImageURL { get; set; }
    }
    public static class ProductRepository
    {
        static List<CategoryModel> objList;
        public static IEnumerable<CategoryModel> GetData(int pageIndex, int pageSize)
        {
            int startAt = (pageIndex - 1) * pageSize;
            objList = new List<CategoryModel>();
            CategoryModel obj;
            Random r = new Random();
            int n = r.Next(1, 7);
            for (int i = startAt; i < startAt + pageSize; i++)
            {
                n = r.Next(1, 7);
                obj = new CategoryModel();
                obj.ImageURL = String.Format("http://dummyimage.com/150x{1}/{0}{0}{0}/fff.png&text={2}", n, n * 50, i + 1);
                obj.Description = "Description of product " + (i + 1).ToString();
                objList.Add(obj);
            }
            return objList;
        }
    }
}
