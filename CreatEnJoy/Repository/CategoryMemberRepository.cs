using CreatEnJoy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatEnJoy.Repository
{
    public class CategoryMemberRepository
    {
        //injectam container-ul ORM
        private Models.DBObjects.ForumMembershipModelsDataContext dbContext;
        public CategoryMemberRepository()
        {
            this.dbContext = new Models.DBObjects.ForumMembershipModelsDataContext();
        }
        //injectam un dbContext pentru a face repository noastra testabila
        public CategoryMemberRepository(Models.DBObjects.ForumMembershipModelsDataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<CategoryMemberModel> GetAllCategoryMembers(Guid IDMember)
        {
            List<CategoryMemberModel> categoryMemberList = new List<CategoryMemberModel>();
            foreach (Models.DBObjects.CategoryMember dbCategoryMember in dbContext.CategoryMembers.Where(x=>x.IDMember == IDMember))
            {
                categoryMemberList.Add(MapDbObjectToModel(dbCategoryMember));
            }
            return categoryMemberList;
        }
        public CategoryMemberModel GetCategoryMemberByID(Guid ID)
        {
            return MapDbObjectToModel(dbContext.CategoryMembers.FirstOrDefault(x => x.IDCategoryMember == ID));
        }
        public void InsertCategoryMember(CategoryMemberModel categoryMemberModel)
        {
            categoryMemberModel.IDCategoryMember = Guid.NewGuid(); //generate new ID for the new record
            dbContext.CategoryMembers.InsertOnSubmit(MapModelToDbObject(categoryMemberModel));//add to ORM layer
            dbContext.SubmitChanges(); //commit to db
        }
        public void UpdateCategoryMember(CategoryMemberModel categoryMemberModel)
        {
            //get existing record to update
            Models.DBObjects.CategoryMember existingCategoryMember = dbContext.CategoryMembers.FirstOrDefault(x => x.IDCategoryMember == categoryMemberModel.IDCategoryMember);
            if (existingCategoryMember != null)
            {
                //map updated values with keeping the ORM object reference
                existingCategoryMember.IDCategoryMember = categoryMemberModel.IDCategoryMember;
                existingCategoryMember.IDCategory = categoryMemberModel.IDCategory;
                existingCategoryMember.IDMember = categoryMemberModel.IDMember;
                dbContext.SubmitChanges();//commit to db
            }
        }
        public void DeleteCategoryMember(Guid ID)
        {
            //get existing record to delete
            Models.DBObjects.CategoryMember categoryMemberToDelete = dbContext.CategoryMembers.FirstOrDefault(x => x.IDCategoryMember == ID);
            if (categoryMemberToDelete != null)
            {
                dbContext.CategoryMembers.DeleteOnSubmit(categoryMemberToDelete); //mark for deletion
                dbContext.SubmitChanges(); //commit to db
            }
        }
        //map ORM model to Model object – mapper method
        private CategoryMemberModel MapDbObjectToModel(Models.DBObjects.CategoryMember dbCategoryMember)
        {
            CategoryMemberModel categoryMemberModel = new CategoryMemberModel();
            if (dbCategoryMember != null)
            {
                categoryMemberModel.IDCategoryMember = dbCategoryMember.IDCategoryMember;
                categoryMemberModel.IDCategory = dbCategoryMember.IDCategory;
                categoryMemberModel.IDMember = dbCategoryMember.IDMember;
                return categoryMemberModel;
            }
            return null;
        }
        //map Model object to ORM model – mapper method
        private Models.DBObjects.CategoryMember MapModelToDbObject(CategoryMemberModel categoryMemberModel)
        {
            Models.DBObjects.CategoryMember dbCategoryMemberModel = new Models.DBObjects.CategoryMember();
            if (categoryMemberModel != null)
            {
                dbCategoryMemberModel.IDCategoryMember = categoryMemberModel.IDCategoryMember;
                dbCategoryMemberModel.IDCategory = categoryMemberModel.IDCategory;
                dbCategoryMemberModel.IDMember = categoryMemberModel.IDMember;
                return dbCategoryMemberModel;
            }
            return null;

        }

    }
}