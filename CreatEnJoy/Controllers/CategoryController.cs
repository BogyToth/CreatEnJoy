using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreatEnJoy.Controllers
{
    public class CategoryController : Controller
    {
        private Repository.CategoryRepository categoryRepository = new Repository.CategoryRepository();
        // GET: Category
        public ActionResult Index()
        {
            List<Models.CategoryModel> categories = categoryRepository.GetAllCategory();
            return View("Index", categories);
        }

        // GET: Category/Details/5
        public ActionResult Details(Guid id)
        {
            Models.CategoryModel categoryModel = categoryRepository.GetCategoryByID(id);
            return View("CategoryDetails",categoryModel);

        }

        // GET: Category/Create
        public ActionResult Create()
        {
            return View("CreateCategory");
        }

        // POST: Category/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Models.CategoryModel categoryModel = new Models.CategoryModel();
                UpdateModel(categoryModel);
                categoryRepository.InsertCategory(categoryModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateCategory");
            }
        }

        // GET: Category/Edit/5
        public ActionResult Edit(Guid id)
        {
            Models.CategoryModel categoryModel = categoryRepository.GetCategoryByID(id);
            return View("EditCategory",categoryModel);
        }

        // POST: Category/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                Models.CategoryModel categoryModel = new Models.CategoryModel();
                UpdateModel(categoryModel);
                categoryRepository.UpdateCategory(categoryModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("EditCategory");
            }
        }

        // GET: Category/Delete/5
        public ActionResult Delete(Guid id)
        {
            Models.CategoryModel categoryModel = categoryRepository.GetCategoryByID(id);
            return View("DeleteCategory",categoryModel);
        }

        // POST: Category/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                categoryRepository.DeleteCategory(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteCategory");
            }
        }
    }
}
