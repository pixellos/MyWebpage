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
    public class Repository<T, TFakeKey> : IEnumerable<T>, IDisposable
        where T : IDatabaseModel
    {
        protected LiteDatabase Database { get; }
        protected Func< T, TFakeKey> FakeKeySelector { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="fakeKeySelector"> Key to use in edit and add comparing</param>
        public Repository(string databasePath, Func< T, TFakeKey> fakeKeySelector)
        {
            this.Database = new LiteDatabase(databasePath);
            this.FakeKeySelector = fakeKeySelector;
        }

        protected LiteCollection<T> Collection => this.Database.GetCollection<T>();

        public Result Add(T value)
        {
            if (value == null)
            {
                return new ArgumentError<T>(value, "NotNull");
            }
            if (this.Any(x => this.FakeKeySelector(x).Equals(this.FakeKeySelector(value))))
            {
                return new Exist<T>(value, "Exist");
            }
            using (var trans = this.Database.BeginTrans())
            {
                value.Id = Guid.NewGuid().ToString();
                value.Updated = new List<DateTime>();
                value.Updated.Add(DateTime.Now);
                this.Collection.Insert(value);
                trans.Commit();
            }
            return new Succedeed();
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
            return this.Collection.FindAll().OrderBy(x => x.Updated.Last()).GetEnumerator();
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