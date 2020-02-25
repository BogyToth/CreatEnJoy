using CreatEnJoy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreatEnJoy.Repository
{
    public class postRepository
    {
        //injectam container-ul ORM
        private Models.DBObjects.ForumMembershipModelsDataContext dbContext;
        public postRepository()
        {
            this.dbContext = new Models.DBObjects.ForumMembershipModelsDataContext();
        }
        //injectam un dbContext pentru a face repository noastra testabila
        public postRepository(Models.DBObjects.ForumMembershipModelsDataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<PostModel> GetAllPosts()
        {
            List<PostModel> postList = new List<PostModel>();
            foreach (Models.DBObjects.Post dbPost in dbContext.Posts)
            {
                postList.Add(MapDbObjectToModel(dbPost));
            }
            return postList;
        }
        public PostModel GetPostByID(Guid ID)
        {
            return MapDbObjectToModel(dbContext.Posts.FirstOrDefault(x => x.IDPost == ID));
        }
        public void InsertPost(PostModel postModel)
        {
            postModel.IDPost = Guid.NewGuid(); //generate new ID for the new record
            dbContext.Posts.InsertOnSubmit(MapModelToDbObject(postModel));//add to ORM layer
            dbContext.SubmitChanges(); //commit to db
        }
        public void UpdatePost(PostModel postModel)
        {
            //get existing record to update
            Models.DBObjects.Post existingPost = dbContext.Posts.FirstOrDefault(x => x.IDPost == postModel.IDPost);
            if (existingPost != null)
            {
                //map updated values with keeping the ORM object reference
                existingPost.IDPost = postModel.IDPost;
                existingPost.Subject = postModel.Subject;
                existingPost.PostDate = postModel.PostDate;
                existingPost.Description = postModel.Description;
                dbContext.SubmitChanges();//commit to db
            }
        }
        public void DeletePost(Guid ID)
        {
            //get existing record to delete
            Models.DBObjects.Post postToDelete = dbContext.Posts.FirstOrDefault(x => x.IDPost == ID);
            if (postToDelete != null)
            {
                dbContext.Posts.DeleteOnSubmit(postToDelete); //mark for deletion
                dbContext.SubmitChanges(); //commit to db
            }
        }

        internal static void DeletePost(object iDPosts)
        {
            throw new NotImplementedException();
        }

        internal static List<PostModel> GetAllPostsByCategoryId(Guid id)
        {
            throw new NotImplementedException();
        }

        //map ORM model to Model object – mapper method
        private PostModel MapDbObjectToModel(Models.DBObjects.Post dbPost)
        {
            PostModel postModel = new PostModel();
            if (dbPost != null)
            {
                postModel.IDPost = dbPost.IDPost;
                postModel.Subject = dbPost.Subject;
                postModel.PostDate = dbPost.PostDate;
                postModel.Description = dbPost.Description;
                return postModel;
            }
            return null;
        }
        //map Model object to ORM model – mapper method
        private Models.DBObjects.Post MapModelToDbObject(PostModel postModel)
        {
            Models.DBObjects.Post dbPostModel = new Models.DBObjects.Post();
            if (postModel != null)
            {
                dbPostModel.IDPost = postModel.IDPost;
                dbPostModel.Subject = postModel.Subject;
                dbPostModel.PostDate = postModel.PostDate;
                dbPostModel.Description = postModel.Description;
                return dbPostModel;
            }
            return null;
        }
    }
}