using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;

namespace YmtSystem.Domain.MongodbRepository
{
    public partial interface IMongodbRepository<TEntity> where TEntity : class
    {
        [Obsolete("use BatchAdd")]
        void BatchAdd_New(IEnumerable<TEntity> documents, WriteConcern writeConcern);
        [Obsolete("use BatchAdd")]
        void BatchAdd_New(IEnumerable<TEntity> documents, WriteConcern writeConcern, string dbName,
            string collectionName);
        BulkWriteResult<TEntity> BulkOperating(IEnumerable<WriteModel<TEntity>> entities, string dbName = null,
            string collectionName=null,BulkWriteOptions bwOptions=null);       
        void BatchAdd(IEnumerable<TEntity> documents, WriteConcern writeConcern);
        void BatchAdd(IEnumerable<TEntity> documents, WriteConcern writeConcern, string dbName, string collectionName);
        void Save(TEntity document, WriteConcern writeConcern = null);
        void Save(TEntity document, string dbName, string collectionName, WriteConcern writeConcern = null);
        WriteConcernResult Add(TEntity document, MongoInsertOptions iOptions = null);
        WriteConcernResult Add(TEntity document, string dbName, string collectionName, MongoInsertOptions iOptions = null);
        void Add(TEntity document);
        void Add(TEntity document, string dbName, string collectionName);
        WriteConcernResult Add(TEntity document, WriteConcern writeConcern = null);
        WriteConcernResult Add(TEntity document, string dbName, string collectionName, WriteConcern writeConcern = null);
        long? Remove(IMongoQuery query, WriteConcern writeConcern = null);
        long? Remove(IMongoQuery query, string dbName, string collectionName, WriteConcern writeConcern = null);
        void Remove(IMongoQuery query);
        void Remove(IMongoQuery query, string dbName, string collectionName);

        Tuple<TResult, int> FindAndRemove<TResult>(IMongoQuery query, IMongoSortBy order);
        Tuple<TResult, int> FindAndRemove<TResult>(FindAndRemoveArgs args);
        Tuple<TResult, int> FindAndModify<TResult>(IMongoQuery query, IMongoUpdate update, bool upSet = true);
        Tuple<TResult, int> FindAndModify<TResult>(FindAndModifyArgs args);

        Tuple<TResult, int> FindAndRemove<TResult>(IMongoQuery query, IMongoSortBy order, string dbName, string collectionName);
        Tuple<TResult, int> FindAndRemove<TResult>(FindAndRemoveArgs args, string dbName, string collectionName);
        Tuple<TResult, int> FindAndModify<TResult>(IMongoQuery query, IMongoUpdate update, string dbName, string collectionName, bool upSet = true);
        Tuple<TResult, int> FindAndModify<TResult>(FindAndModifyArgs args, string dbName, string collectionName);

        WriteConcernResult Update(IMongoQuery query, IMongoUpdate update);
        WriteConcernResult Update(IMongoQuery query, IMongoUpdate update, MongoUpdateOptions options);
        WriteConcernResult Update(IMongoQuery query, IMongoUpdate update, string dbName, string collectionName);
        WriteConcernResult Update(IMongoQuery query, IMongoUpdate update, MongoUpdateOptions options, string dbName, string collectionName);

        void BatchUpdate(IEnumerable<Action<BulkWriteOperation<TEntity>>> batchUpdate);
        void BatchUpdate(IEnumerable<Action<BulkWriteOperation<TEntity>>> batchUpdate, WriteConcern wc,
            string dbName, string collectionName);
       
        void CreateCappedCollection(string dbName, string collectionName, long maxSize, long maxDocNum, bool dropExistsCollection = true);
        void CreateCappedCollection(long maxSize, long maxDocNum, bool dropExistsCollection = true);
        ReplaceOneResult ReplaceOne(FilterDefinition<TEntity> filter, TEntity newDocument, string dbName = null,
            string collectionName = null
            , UpdateOptions updateOptions = null, CancellationToken token = default(CancellationToken));
    }
}
