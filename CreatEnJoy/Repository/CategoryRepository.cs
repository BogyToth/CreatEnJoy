using CreatEnJoy.Models;
using CreatEnJoy.Models.DBObjects;
using CreatEnJoy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatEnJoy.Repository
{
    public class CategoryRepository
    {
        //injectam container-ul ORM
        private Models.DBObjects.ForumMembershipModelsDataContext dbContext;
        public CategoryRepository()
        {
            this.dbContext = new Models.DBObjects.ForumMembershipModelsDataContext();
        }
        //injectam un dbContext pentru a face repository noastra testabila
        public CategoryRepository(Models.DBObjects.ForumMembershipModelsDataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<CategoryModel> GetAllCategory()
        {
            List<CategoryModel> categoryList = new List<CategoryModel>();
            foreach (Models.DBObjects.Category dbCategory in dbContext.Categories)
            {
                categoryList.Add(MapDbObjectToModel(dbCategory));
            }
            return categoryList;
        }
        public CategoryModel GetCategoryByID(Guid ID)
        {
            return MapDbObjectToModel(dbContext.Categories.FirstOrDefault(x => x.IDCategory == ID));
        }
        public void InsertCategory(CategoryModel categoryModel)
        {
            categoryModel.IDCategory = Guid.NewGuid(); //generate new ID for the new record
            dbContext.Categories.InsertOnSubmit(MapModelToDbObject(categoryModel));//add to ORM layer
            dbContext.SubmitChanges(); //commit to db
        }
        public void UpdateCategory(CategoryModel categoryModel)
        {
            //get existing record to update
            Models.DBObjects.Category existingCategory = dbContext.Categories.FirstOrDefault(x => x.IDCategory == categoryModel.IDCategory);
            if (existingCategory != null)
            {
                //map updated values with keeping the ORM object reference
                existingCategory.IDCategory = categoryModel.IDCategory;
                existingCategory.Name = categoryModel.Name;
                existingCategory.Description = categoryModel.Description;
                dbContext.SubmitChanges();//commit to db
            }
        }
        public void DeleteCategory(Guid ID)
        {
            //get existing record to delete
            Models.DBObjects.Category categoryToDelete = dbContext.Categories.FirstOrDefault(x => x.IDCategory == ID);
            if (categoryToDelete != null)
            {
                dbContext.Categories.DeleteOnSubmit(categoryToDelete); //mark for deletion
                dbContext.SubmitChanges(); //commit to db
            }
        }
        //map ORM model to Model object – mapper method
        private CategoryModel MapDbObjectToModel(Models.DBObjects.Category dbCategory)
        {
            CategoryModel categoryModel = new CategoryModel();
            if (dbCategory != null)
            {
                categoryModel.IDCategory = dbCategory.IDCategory;
                categoryModel.Name = dbCategory.Name;
                categoryModel.Description = dbCategory.Description;
                categoryModel.ImageURL = dbCategory.ImageURL;
                return categoryModel;
            }
            return null;
        }
        //map Model object to ORM model – mapper method
        private Models.DBObjects.Category MapModelToDbObject(CategoryModel categoryModel)
        {
            Models.DBObjects.Category dbCategoryModel = new Models.DBObjects.Category();
            if (categoryModel != null)
            {
                dbCategoryModel.IDCategory = categoryModel.IDCategory;
                dbCategoryModel.Name = categoryModel.Name;
                dbCategoryModel.Description = categoryModel.Description;
                dbCategoryModel.ImageURL = categoryModel.ImageURL;
                return dbCategoryModel;
            }
            return null;
        }
        public PostCategoryViewModel GetPostCategory(Guid categoryID)
        { PostCategoryViewModel postCategoryViewModel = new PostCategoryViewModel();
            Category category = forummembershipDataContext.Posts.FirstOrDefault(x => x.IDPost == categoryID);
            if(category !=null)
            {
                postCategoryViewModel.Subject = category.Name;
                postCategoryViewModel.Description = category.Description;
                IQueryable<Post> categoryPosts = forumMembershipDataContext.Posts.Where(x => x.IDCategory == categoryID);
                foreach (Post dbPost in categoryPosts)
                {
                    Models.PostModel postModel = new Models.PostModel();
                    postModel.Subject = dbPost.Subject;
                    postModel.Description = dbPost.Description;
                    postCategoryViewModel.Posts.Add(postModel);
                }
            }
            return postCategoryViewModel;
        }
    }
   
}
