namespace YmtSystem.Repository.Mongodb
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;
    using YmtSystem.Domain.MongodbRepository;

    public partial class MongodbRepository<TEntity> : IMongodbRepository<TEntity>
        where TEntity : class
    {
        public virtual TEntity FindById<TKey>(TKey id)
        {
            return this.Context.GetCollection<TEntity>().FindOneById(BsonValue.Create(id));
        }
        public virtual TEntity FindOne(IMongoQuery query, bool single = true)
        {
            if (single)
                return this.Context.GetCollection<TEntity>().Find(query).SingleOrDefault();
            else
                return this.Context.GetCollection<TEntity>().FindOne(query); ;
        }
        public virtual TEntity FindOne(IMongoQuery query, string dbName, string collectionName, bool single = true)
        {
            if (single)
                return this.Context.GetCollection<TEntity>(dbName, collectionName).Find(query).SingleOrDefault();
            else
                return this.Context.GetCollection<TEntity>(dbName, collectionName).FindOne(query); ;
        }
        public virtual async Task<MongoCursor<TEntity>> FindAsync(IMongoQuery query, int millisecondsDelay = 3000, Action callback = null, Action<Exception> errorHandler = null)
        {
            return await ExecuteAsync(() => this.context.GetCollection<TEntity>().Find(query), default(MongoCursor<TEntity>), millisecondsDelay, callback, errorHandler);
        }
        public virtual bool Exists(IMongoQuery query)
        {
            return this.Context.GetCollection<TEntity>().FindOne(query) != null;
        }
        public virtual MongoCursor<TEntity> Find(IMongoQuery query, int index, int limit)
        {
            return this.Context.GetCollection<TEntity>().Find(query).SetSkip(index).SetLimit(limit);
        }
        public virtual MongoCursor<TEntity> Find(IMongoQuery query, IMongoSortBy order, int index, int limit)
        {
            return this.Context.GetCollection<TEntity>().Find(query).SetSortOrder(order).SetSkip(index).SetLimit(limit);
        }
        public virtual MongoCursor<TEntity> Find(IMongoQuery query, IMongoSortBy order, IMongoFields fields, int index, int limit)
        {
            return this.Context.GetCollection<TEntity>().Find(query).SetFields(fields).SetSortOrder(order).SetSkip(index).SetLimit(limit);
        }
        public virtual long Count(IMongoQuery query)
        {
            return this.Context.GetCollection<TEntity>().Count(query);
        }
        public virtual MongoCursor<TEntity> Find(IMongoQuery query)
        {
            return this.Context.GetCollection<TEntity>().Find(query);
        }
        public virtual TEntity FindById<TKey>(TKey id, string dbName, string collectionName)
        {
            return this.Context.GetCollection<TEntity>(dbName, collectionName).FindOneById(BsonValue.Create(id));
        }
        public virtual long Count(IMongoQuery query, string dbName, string collectionName)
        {
            return this.Context.GetCollection<TEntity>(dbName, collectionName).Count(query);
        }
        public virtual bool Exists(IMongoQuery query, string dbName, string collectionName)
        {
            return this.Context.GetCollection<TEntity>(dbName, collectionName).FindOne(query) != null;
        }
        public virtual async Task<MongoCursor<TEntity>> FindAsync(IMongoQuery query, string dbName, string collectionName)
        {
            return await Task.Run(() => this.Context.GetCollection<TEntity>(dbName, collectionName).Find(query));
        }
        public virtual async Task<TEntity> FindOneAsync(IMongoQuery query, string dbName, string collectionName, bool single = true, int millisecondsDelay = 3000, Action callback = null, Action<Exception> errorHandler = null)
        {
            if (single)
                return await ExecuteAsync(() => this.Context.GetCollection<TEntity>(dbName, collectionName).Find(query).SingleOrDefault(), default(TEntity), millisecondsDelay, callback, errorHandler).ConfigureAwait(false);
            return await ExecuteAsync(() => this.Context.GetCollection<TEntity>(dbName, collectionName).Find(query).FirstOrDefault(), default(TEntity), millisecondsDelay, callback, errorHandler).ConfigureAwait(false);
        }
        public virtual TEntity Find(IMongoQuery query, string dbName, string collectionName, bool single = true)
        {
            if (single)
                return this.Context.GetCollection<TEntity>(dbName, collectionName).Find(query).SingleOrDefault();
            return this.Context.GetCollection<TEntity>(dbName, collectionName).Find(query).FirstOrDefault();
        }
        public virtual MongoCursor<TEntity> Find(IMongoQuery query, string dbName, string collectionName)
        {
            return this.Context.GetCollection<TEntity>(dbName, collectionName).Find(query);
        }
        public virtual MongoCursor<TEntity> Find(IMongoQuery query, string dbName, string collectionName, int index, int limit)
        {
            return this.Context.GetCollection<TEntity>(dbName, collectionName).Find(query).SetSkip(index).SetLimit(limit);
        }
        public virtual MongoCursor<TEntity> Find(IMongoQuery query, string dbName, string collectionName, IMongoSortBy order, int index, int limit)
        {
            return this.Context.GetCollection<TEntity>(dbName, collectionName).Find(query).SetSortOrder(order).SetSkip(index).SetLimit(limit);
        }
        public virtual MongoCursor<TEntity> Find(IMongoQuery query, string dbName, string collectionName, IMongoSortBy order, IMongoFields fields, int index, int limit)
        {
            return this.Context.GetCollection<TEntity>(dbName, collectionName).Find(query).SetFields(fields).SetSortOrder(order).SetSkip(index).SetLimit(limit);
        }
    }
}
