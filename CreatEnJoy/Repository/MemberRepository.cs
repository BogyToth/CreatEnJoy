using CreatEnJoy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatEnJoy.Repository
{
    public class MemberRepository
    {
        //injectam container-ul ORM
        private Models.DBObjects.ForumMembershipModelsDataContext dbContext;
        public MemberRepository()
        {
            this.dbContext = new Models.DBObjects.ForumMembershipModelsDataContext();
        }
        //injectam un dbContext pentru a face repository noastra testabila
        public MemberRepository(Models.DBObjects.ForumMembershipModelsDataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<MemberModel> GetAllMembers()
        {
            List<MemberModel> memberList = new List<MemberModel>();
            foreach (Models.DBObjects.Member dbMember in dbContext.Members)
            {
                memberList.Add(MapDbObjectToModel(dbMember));
            }
            return memberList;
        }
        public MemberModel GetMemberByID(Guid ID)
        {
            return MapDbObjectToModel(dbContext.Members.FirstOrDefault(x => x.IDMember == ID));
        }
        public void InsertMember(MemberModel memberModel)
        {
            memberModel.IDMember = Guid.NewGuid(); //generate new ID for the new record
            dbContext.Members.InsertOnSubmit(MapModelToDbObject(memberModel));//add to ORM layer
            dbContext.SubmitChanges(); //commit to db
        }
        public void UpdateMember(MemberModel memberModel)
        {
            //get existing record to update
            Models.DBObjects.Member existingMember = dbContext.Members.FirstOrDefault(x => x.IDMember == memberModel.IDMember);
            if (existingMember != null)
            {
                //map updated values with keeping the ORM object reference
                existingMember.IDMember = memberModel.IDMember;
                existingMember.Title = memberModel.Title;
                existingMember.Username = memberModel.Username;
                existingMember.Description = memberModel.Description;
                dbContext.SubmitChanges();//commit to db
            }
        }
        public void DeleteMember(Guid ID)
        {
            //get existing record to delete
            Models.DBObjects.Member memberToDelete = dbContext.Members.FirstOrDefault(x => x.IDMember == ID);
            if (memberToDelete != null)
            {
                dbContext.Members.DeleteOnSubmit(memberToDelete); //mark for deletion
                dbContext.SubmitChanges(); //commit to db
            }
        }
        //map ORM model to Model object – mapper method
        private MemberModel MapDbObjectToModel(Models.DBObjects.Member dbMember)
        {
            MemberModel memberModel = new MemberModel();
            if (dbMember != null)
            {
                memberModel.IDMember = dbMember.IDMember;
                memberModel.Username = dbMember.Username;
                memberModel.Title = dbMember.Title;
                memberModel.Description = dbMember.Description;
                return memberModel;
            }
            return null;
        }
        //map Model object to ORM model – mapper method
        private Models.DBObjects.Member MapModelToDbObject(MemberModel
       memberModel)
        {
            Models.DBObjects.Member dbMemberModel = new Models.DBObjects.Member();
            if (memberModel != null)
            {
                dbMemberModel.IDMember = memberModel.IDMember;
                dbMemberModel.Username = memberModel.Username;
                dbMemberModel.Title = memberModel.Title;
                dbMemberModel.Description = memberModel.Description;
                return dbMemberModel;
            }
            return null;

        }

    }
}