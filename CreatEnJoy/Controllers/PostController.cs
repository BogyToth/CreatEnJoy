using CreatEnJoy.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreatEnJoy.Controllers
{
    public class PostController : Controller
    {
        postRepository postRepository = new postRepository();
        // GET: Post
        public ActionResult Index()
        {
            List<Models.PostModel> posts = postRepository.GetAllPosts();
            return View("Index",posts);
        }

        // GET: Post/Details/5
        public ActionResult Details(Guid id)
        {
            Models.PostModel postModel = postRepository.GetPostByID(id);
            return View("PostDetails",postModel);
        }

        // GET: Post/Create
        private CategoryRepository categoryRepository = new CategoryRepository();
        public ActionResult Create()
        {
            var categories = categoryRepository.GetAllCategory();
            SelectList lst = new SelectList(categories, "IDCategory", "Name");
            ViewData["category"] = lst;
            return View("CreatePost");
            //ViewBag.IDCategory = new SelectList(categories, "IDCategory", "Name");
            //return View("CreatePost");
        }

        // POST: Post/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Models.PostModel postModel = new Models.PostModel();
                UpdateModel(postModel);
                //adaugare tag utilizator
                if (User.Identity.IsAuthenticated) //daca avem utilizator logat
                {
                    postModel.Subject= User.Identity.Name + ": " + postModel.Subject;
                    postModel.Description = postModel.Description + "," + User.Identity.Name;
                }

                postRepository.InsertPost(postModel);


                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreatePost");
            }
        }

        // GET: Post/Edit/5
        public ActionResult Edit(Guid id)
        {
            Models.PostModel postModel = postRepository.GetPostByID(id);

            return View("EditPost",postModel);
        }

        // POST: Post/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                Models.PostModel postModel = new Models.PostModel();
                UpdateModel(postModel);
                postRepository.UpdatePost(postModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("EditPost");
            }
        }

        // GET: Post/Delete/5
        public ActionResult Delete(Guid id)
        {
            Models.PostModel postModel = postRepository.GetPostByID(id);
            return View("DeletePost",postModel);
        }

        // POST: Post/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                postRepository.DeletePost(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeletePost");
            }
        }
    }
}
