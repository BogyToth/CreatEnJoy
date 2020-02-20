using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CreatEnJoy.Controllers
{
    public class CategoryMemberController : Controller
    {
        private Repository.CategoryMemberRepository categoryMemberRepository = new Repository.CategoryMemberRepository();
        // GET: CategoryMember
        public ActionResult Index() 
        {
            List<Models.CategoryMemberModel> categoryMembers = categoryMemberRepository.GetAllCategoryMembers();
            return View("Index", categoryMembers);
        }

        // GET: CategoryMember/Details/5
        public ActionResult Details(Guid id)
        {
            Models.CategoryMemberModel categoryMemberModel = categoryMemberRepository.GetCategoryMemberByID(id);
            return View("CategoryMemberDetail",categoryMemberModel);
        }

        // GET: CategoryMember/Create
        public ActionResult Create()
        {
            return View("CreateCategoryMember");
        }

        // POST: CategoryMember/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Models.CategoryMemberModel categoryMemberModel = new Models.CategoryMemberModel();
                UpdateModel(categoryMemberModel);
                categoryMemberRepository.InsertCategoryMember(categoryMemberModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateCategoryMember");
            }
        }

        // GET: CategoryMember/Edit/5
        public ActionResult Edit(Guid id)
        {
            Models.CategoryMemberModel categoryMemberModel = categoryMemberRepository.GetCategoryMemberByID(id);
            return View("EditCategoryMember",categoryMemberModel);
        }

        // POST: CategoryMember/Edit/5
        [HttpPost]
        public ActionResult Edit(int Guid, FormCollection collection)
        {
            try
            {
                Models.CategoryMemberModel categoryMemberModel = new Models.CategoryMemberModel();
                UpdateModel(categoryMemberModel);
                categoryMemberRepository.UpdateCategoryMember(categoryMemberModel);
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View("EditCategoryMember");
            }
        }

        // GET: CategoryMember/Delete/5
        public ActionResult Delete(Guid id)
        {
            Models.CategoryMemberModel categoryMemberModel = categoryMemberRepository.GetCategoryMemberByID(id);
            return View("DeleteCategoryMember", categoryMemberModel);
        }

        // POST: CategoryMember/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                categoryMemberRepository.DeleteCategoryMember(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteCategoryMember");
            }
        }
    }
}
