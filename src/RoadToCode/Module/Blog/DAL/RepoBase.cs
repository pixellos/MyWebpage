using System;
using System.Web.Hosting;
using LiteDB;

namespace RoadToCode.Module.Blog.DAL
{
    public abstract class RepoBase<T> where T : IDatabaseModel, new()
    {
        protected readonly Func<LiteDatabase> createConnection = () => new LiteDatabase(HostingEnvironment.MapPath(@"~/App_Data/Database.db"));

        protected LiteCollection<T> GetCollection(LiteDatabase database) => database.GetCollection<T>(TableName);

        public string TableName;


        protected RepoBase(string tableName)
        {
            TableName = tableName;
        }
        public void SavePost(T post)
        {
            using (var db = createConnection())
            {
                
                if (post == null) return;
                post.DateTime = DateTime.Now;
                var collection = db.GetCollection<T>(TableName);

                if (collection.Exists(x => x.Id == post.Id))
                {
                    collection.Update(post);
                    db.Commit();
                    return;
                }

                collection.Insert(post);
                collection.EnsureIndex(x => x.Id);

                db.Commit();
            }
        }

       
    }
}