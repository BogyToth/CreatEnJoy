using CreatEnJoy.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreatEnJoy.Controllers
{
    public class CategoryPostController : Controller
    {
        CategoryPostRepository categoryPostRepository = new CategoryPostRepository();
        // GET: CategoryPost
        public ActionResult Index()
        {
            List<Models.CategoryPostModel> categoryPosts = categoryPostRepository.GetAllCategoryPosts();
            return View("Index",categoryPosts);
        }

        // GET: CategoryPost/Details/5
        public ActionResult Details(Guid id)
        {
            Models.CategoryPostModel categoryPostModel = categoryPostRepository.GetCategoryPostByID(id);
            return View("CategoryPostDetails",categoryPostModel);
        }

        // GET: CategoryPost/Create
        public ActionResult Create()
        {
            return View("CreateCategoryPost");
            
        }

        // POST: CategoryPost/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Models.CategoryPostModel categoryPostModel = new Models.CategoryPostModel();
                UpdateModel(categoryPostModel);
                categoryPostRepository.InsertCategoryPost(categoryPostModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateCategoryPost");
            }
        }

        // GET: CategoryPost/Edit/5
        public ActionResult Edit(Guid id)
        {
            Models.CategoryPostModel categoryPostModel = categoryPostRepository.GetCategoryPostByID(id);
            return View("EditCategoryPost",categoryPostModel);
        }

        // POST: CategoryPost/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                Models.CategoryPostModel categoryPostModel = new Models.CategoryPostModel();
                UpdateModel(categoryPostModel);
                categoryPostRepository.UpdateCategoryPost(categoryPostModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("EditCategoryPost");
            }
        }

        // GET: CategoryPost/Delete/5
        public ActionResult Delete(Guid id)
        {
            Models.CategoryPostModel categoryPostModel = categoryPostRepository.GetCategoryPostByID(id);
            return View("DeleteCategoryPost", categoryPostModel);
        }

        // POST: CategoryPost/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                categoryPostRepository.DeleteCategoryPost(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteCategoryPost");
            }
        }
    }
}
