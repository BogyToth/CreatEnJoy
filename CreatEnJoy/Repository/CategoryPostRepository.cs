using CreatEnJoy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatEnJoy.Repository
{
    public class CategoryPostRepository
    {
        //injectam container-ul ORM
        private Models.DBObjects.ForumMembershipModelsDataContext dbContext;
        public CategoryPostRepository()
        {
            this.dbContext = new Models.DBObjects.ForumMembershipModelsDataContext();
        }
        //injectam un dbContext pentru a face repository noastra testabila
        public CategoryPostRepository(Models.DBObjects.ForumMembershipModelsDataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<CategoryPostModel> GetAllCategoryPosts()
        {
            List<CategoryPostModel> categoryPostList = new List<CategoryPostModel>();
            foreach (Models.DBObjects.CategoryPost dbCategoryPost in dbContext.CategoryPosts)
            {
                categoryPostList.Add(MapDbObjectToModel(dbCategoryPost));
            }
            return categoryPostList;
        }
        public CategoryPostModel GetCategoryPostByID(Guid ID)
        {
            return MapDbObjectToModel(dbContext.CategoryPosts.FirstOrDefault(x => x.IDCategoryPost == ID));
        }
        public void InsertCategoryPost(CategoryPostModel categoryPostModel)
        {
            categoryPostModel.IDCategoryPost = Guid.NewGuid(); //generate new ID for the new record
            dbContext.CategoryPosts.InsertOnSubmit(MapModelToDbObject(categoryPostModel));//add to ORM layer
            dbContext.SubmitChanges(); //commit to db
        }
        public void UpdateCategoryPost(CategoryPostModel categoryPostModel)
        {
            //get existing record to update
            Models.DBObjects.CategoryPost existingCategoryPost = dbContext.CategoryPosts.FirstOrDefault(x => x.IDCategoryPost == categoryPostModel.IDCategoryPost);
            if (existingCategoryPost != null)
            {
                //map updated values with keeping the ORM object reference
                existingCategoryPost.IDCategoryPost = categoryPostModel.IDCategoryPost;
                existingCategoryPost.IDCategory = categoryPostModel.IDCategory;
                existingCategoryPost.IDPost = categoryPostModel.IDPost;
                dbContext.SubmitChanges();//commit to db
            }
        }
        public void DeleteCategoryPost(Guid ID)
        {
            //get existing record to delete
            Models.DBObjects.CategoryPost categoryPostToDelete = dbContext.CategoryPosts.FirstOrDefault(x => x.IDCategoryPost == ID);
            if (categoryPostToDelete != null)
            {
                dbContext.CategoryPosts.DeleteOnSubmit(categoryPostToDelete); //mark for deletion
                dbContext.SubmitChanges(); //commit to db
            }
        }
        //map ORM model to Model object – mapper method
        private CategoryPostModel MapDbObjectToModel(Models.DBObjects.CategoryPost dbCategoryPost)
        {
            CategoryPostModel categoryPostModel = new CategoryPostModel();
            if (dbCategoryPost != null)
            {
                categoryPostModel.IDCategoryPost = dbCategoryPost.IDCategoryPost;
                categoryPostModel.IDPost = dbCategoryPost.IDPost;
                categoryPostModel.IDCategory = dbCategoryPost.IDCategory;
                return categoryPostModel;
            }
            return null;
        }
        //map Model object to ORM model – mapper method
        private Models.DBObjects.CategoryPost MapModelToDbObject(CategoryPostModel categoryPostModel)
        {
            Models.DBObjects.CategoryPost dbCategoryPostModel = new Models.DBObjects.CategoryPost();
            if (categoryPostModel != null)
            {
                dbCategoryPostModel.IDCategoryPost = categoryPostModel.IDCategoryPost;
                dbCategoryPostModel.IDCategory = categoryPostModel.IDCategory;
                dbCategoryPostModel.IDPost = categoryPostModel.IDPost;
                return dbCategoryPostModel;
            }
            return null;

        }

    }
}