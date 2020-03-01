using CreatEnJoy.Models;
using CreatEnJoy.Repository;
using CreatEnJoy.ViewModels;
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
            //Models.CategoryModel categoryModel = categoryRepository.GetCategoryByID(id);
            //return View("CategoryDetails",categoryModel);
            PostCategoryViewModel viewModel = categoryRepository.GetPostCategory(id);
            return View(viewModel);
        }

        // GET: Category/Create
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            return View("CreateCategory");
        }

        // POST: Category/Create
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Guid id)
        {
            Models.CategoryModel categoryModel = categoryRepository.GetCategoryByID(id);
            return View("EditCategory",categoryModel);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id)
        {
            Models.CategoryModel categoryModel = categoryRepository.GetCategoryByID(id);
            return View("DeleteCategory",categoryModel);
        }

        // POST: Category/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                List<PostModel> posts =postRepository.GetAllPostsByCategoryId(id);
                foreach (PostModel post in posts)
                {
                    postRepository.DeletePost(post.IDPost);
                }

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
