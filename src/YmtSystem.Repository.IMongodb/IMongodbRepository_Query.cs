using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace YmtSystem.Domain.MongodbRepository
{
    public partial interface IMongodbRepository<TEntity> where TEntity : class
    {
        TEntity FindById<TKey>(TKey id);
        MongoCursor<TEntity> Find(IMongoQuery query);
        TEntity FindOne(IMongoQuery query, bool single = true);
        TEntity FindOne(IMongoQuery query,string dbName,string collectionName, bool single = true);
        Task<MongoCursor<TEntity>> FindAsync(IMongoQuery query, int millisecondsDelay = 3000, Action callback = null, Action<Exception> errorHandler = null);
        bool Exists(IMongoQuery query);
        MongoCursor<TEntity> Find(IMongoQuery query, int index, int limit);
        MongoCursor<TEntity> Find(IMongoQuery query, IMongoSortBy order, int index, int limit);
        MongoCursor<TEntity> Find(IMongoQuery query, IMongoSortBy order, IMongoFields fields, int index, int limit);
        long Count(IMongoQuery query);
        long Count(IMongoQuery query, string dbName, string collectionName);
        
        TEntity FindById<TKey>(TKey id, string dbName, string collectionName);
        bool Exists(IMongoQuery query, string dbName, string collectionName);        
        Task<MongoCursor<TEntity>> FindAsync(IMongoQuery query, string dbName, string collectionName);
        Task<TEntity> FindOneAsync(IMongoQuery query, string dbName, string collectionName, bool single = true, int millisecondsDelay = 3000, Action callback = null, Action<Exception> errorHandler = null);
        TEntity Find(IMongoQuery query, string dbName, string collectionName, bool single = true);
        MongoCursor<TEntity> Find(IMongoQuery query, string dbName, string collectionName);
        MongoCursor<TEntity> Find(IMongoQuery query, string dbName, string collectionName, int index, int limit);
        MongoCursor<TEntity> Find(IMongoQuery query, string dbName, string collectionName, IMongoSortBy order, int index, int limit);
        MongoCursor<TEntity> Find(IMongoQuery query, string dbName, string collectionName, IMongoSortBy order, IMongoFields fields, int index, int limit);
    }
}
