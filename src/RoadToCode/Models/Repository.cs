using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LiteDB;
using RoadToCode.Models;
using RoadToCode.Models.Results;
using RoadToCode.Services.Blog;

namespace RoadToCode.Models
{
    public class Repository<T> : IEnumerable<T>, IDisposable
        where T : IDatabaseModel
    {
        protected LiteDatabase Database { get; }

        public Repository(string databasePath)
        {
            this.Database = new LiteDatabase(databasePath);
        }

        protected LiteCollection<T> Collection => this.Database.GetCollection<T>();

        protected Result Add(T value, DateTime addTime)
        {
            if (value == null)
            {
                return new ArgumentError<T>(value, "NotNull");
            }
            if (this.Any(x => x.Id == value.Id))
            {
                return new Exist<T>(value, "You're trying to add existing entry.");
            }
            using (var trans = this.Database.BeginTrans())
            {
                value.Id = Guid.NewGuid().ToString();
                value.Added = addTime;
                this.Collection.Insert(value);
                trans.Commit();
            }
            return new Succedeed();
        }

        public Result Add(T value)
        {
            return this.Add(value, DateTime.Now);
        }

        public void Delete(Expression<Func<T, bool>> selector)
        {
            using (var trans = this.Database.BeginTrans())
            {
                var items = this.Collection.Find(selector).Where(x => x.State == ModelState.Ok);
                foreach (var item in items)
                {
                    item.State = ModelState.Deleted;
                }
                trans.Commit();
            }
        }

        public void CopyTo(Array array, int index)
        {
            var collection = this.Collection.FindAll();
            var remaining = collection.Count() - index;
            Array.Copy(collection.ToArray(), index, array, 0, remaining);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Collection.Find(x => x.State == ModelState.Ok).OrderBy(x => x.Added).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerator<T>)this);
        }

        public void Dispose()
        {
            this.Database.Dispose();
        }
    }
}