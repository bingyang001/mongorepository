using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

using YmtSystem.MongodbRepository._Assert;
using YmtSystem.Domain.MongodbRepository;
using YmtSystem.Repository.MongodbException;
using MongoDB.Driver.Core.Operations;

namespace YmtSystem.Repository.Mongodb
{  
    public partial class MongodbRepository<TEntity> : IMongodbRepository<TEntity>
        where TEntity : class
    {
        public virtual void BatchAdd(IEnumerable<TEntity> documents, WriteConcern writeConcern)
        {
            this.Context.GetCollection<TEntity>().InsertBatch(documents, writeConcern);
        }
        public virtual void BatchAdd(IEnumerable<TEntity> documents, WriteConcern writeConcern, string dbName, string collectionName)
        {
            YmtSystemAssert.AssertArgumentNotEmpty(dbName, "数据库名不能为空");
            YmtSystemAssert.AssertArgumentNotEmpty(collectionName, "集合名称不能为空");

            this.Context.GetCollection<TEntity>(dbName, collectionName).InsertBatch(documents, writeConcern);
        }
        public virtual void BatchAdd_New(IEnumerable<TEntity> documents, WriteConcern writeConcern)
        {
            var bulk = this.Context.GetCollection<TEntity>().InitializeOrderedBulkOperation();
            foreach (var entity in documents)
            {
                bulk.Insert(entity);
            }
            bulk.Execute(writeConcern);
        }
        public virtual void BatchAdd_New(IEnumerable<TEntity> documents, WriteConcern writeConcern, string dbName, string collectionName)
        {
            var bulk = this.Context.GetCollection<TEntity>(dbName,collectionName).InitializeOrderedBulkOperation();
            foreach (var entity in documents)
            {
                bulk.Insert(entity);
            }
            bulk.Execute(writeConcern);
        }

        public virtual WriteConcernResult Add(TEntity document, WriteConcern writeConcern = null)
        {
            if (writeConcern == null)
                return this.Context.GetCollection<TEntity>().Insert(document);
            else return this.Context.GetCollection<TEntity>().Insert(document, writeConcern);
        }
        
        public virtual void Add(TEntity document)
        {
            this.Context.GetCollection<TEntity>().Insert(document);
        }

        public virtual WriteConcernResult Add(TEntity document, MongoInsertOptions iOptions = null)
        {
            if (iOptions == null)
                return this.Context.GetCollection<TEntity>().Insert(document);
            return this.Context.GetCollection<TEntity>().Insert(document, iOptions);
        }

        public virtual void Save(TEntity document, WriteConcern writeConcern = null)
        {
            if (writeConcern != null)
                this.Context.GetCollection<TEntity>().Save(document, writeConcern);
            else
                this.Context.GetCollection<TEntity>().Save(document);
        }
        public virtual long? Remove(IMongoQuery query, WriteConcern writeConcern = null)
        {
            if (writeConcern == null)
            {
                this.Context.GetCollection<TEntity>().Remove(query);
                return 0L;
            }
            else
            {
                var result = this.Context.GetCollection<TEntity>().Remove(query, writeConcern);
                return result.DocumentsAffected;
            }
        }
        public virtual void Remove(IMongoQuery query, string dbName, string collectionName)
        {
            YmtSystemAssert.AssertArgumentNotEmpty(dbName, "数据库名不能为空");
            YmtSystemAssert.AssertArgumentNotEmpty(collectionName, "集合名称不能为空");

            this.Context.GetCollection<TEntity>(dbName, collectionName).Remove(query);
        }
        public virtual void Remove(IMongoQuery query)
        {
            this.Context.GetCollection<TEntity>().Remove(query);
        }
        public virtual Tuple<TResult, int> FindAndRemove<TResult>(IMongoQuery query, IMongoSortBy order)
        {
            var result = this.Context.GetCollection<TEntity>().FindAndRemove(new FindAndRemoveArgs { Query = query, SortBy = order });
            return Tuple.Create(result.GetModifiedDocumentAs<TResult>(), result.ModifiedDocument.ElementCount);
        }
        public virtual Tuple<TResult, int> FindAndRemove<TResult>(FindAndRemoveArgs args)
        {
            var result = this.Context.GetCollection<TEntity>().FindAndRemove(args);
            return Tuple.Create(result.GetModifiedDocumentAs<TResult>(), result.ModifiedDocument.ElementCount);
        }

        public virtual Tuple<TResult, int> FindAndModify<TResult>(IMongoQuery query, IMongoUpdate update, bool upSet = true)
        {
            var result = this.Context.GetCollection<TEntity>().FindAndModify(new FindAndModifyArgs { Query = query, Update = update, Upsert = upSet });
            return Tuple.Create(result.GetModifiedDocumentAs<TResult>(), result.ModifiedDocument.ElementCount);
        }
        public virtual Tuple<TResult, int> FindAndModify<TResult>(FindAndModifyArgs args)
        {
            var result = this.Context.GetCollection<TEntity>().FindAndModify(args);
            return Tuple.Create(result.GetModifiedDocumentAs<TResult>(), result.ModifiedDocument.ElementCount);
        }

        public virtual WriteConcernResult Update(IMongoQuery query, IMongoUpdate update)
        {
            return this.Context.GetCollection<TEntity>().Update(query, update);
        }
        public virtual WriteConcernResult Update(IMongoQuery query, IMongoUpdate update, MongoUpdateOptions options)
        {
            if (options != null)
                return this.Context.GetCollection<TEntity>().Update(query, update, options);
            return this.Context.GetCollection<TEntity>().Update(query, update);
        }
        public virtual WriteConcernResult Update(IMongoQuery query, IMongoUpdate update, string dbName, string collectionName)
        {
            YmtSystemAssert.AssertArgumentNotEmpty(dbName, "数据库名不能为空");
            YmtSystemAssert.AssertArgumentNotEmpty(collectionName, "集合名称不能为空");

            return this.Context.GetCollection(dbName, collectionName).Update(query, update);
        }
        public virtual WriteConcernResult Update(IMongoQuery query, IMongoUpdate update, MongoUpdateOptions options, string dbName, string collectionName)
        {
            YmtSystemAssert.AssertArgumentNotEmpty(dbName, "数据库名不能为空");
            YmtSystemAssert.AssertArgumentNotEmpty(collectionName, "集合名称不能为空");

            if (options != null)
                return this.Context.GetCollection<TEntity>(dbName, collectionName).Update(query, update, options);
            return this.Context.GetCollection<TEntity>(dbName, collectionName).Update(query, update);
        }

        public virtual BulkWriteResult<TEntity> BulkOperating(IEnumerable<WriteModel<TEntity>> entities, string dbName = null, string collectionName = null, BulkWriteOptions bwOptions = null)
        {
            return this.Context.ContextNewCore.GetCollection<TEntity>(dbName, collectionName).BulkWrite(entities, bwOptions);
        }

        public virtual void BatchUpdate(IEnumerable<Action<BulkWriteOperation<TEntity>>> batchUpdate)
        {
            BatchUpdate(batchUpdate, WriteConcern.W1,null,null);
        }

        public virtual void BatchUpdate(IEnumerable<Action<BulkWriteOperation<TEntity>>> batchUpdate, WriteConcern wc,
            string dbName, string collectionName)
        {
            YmtSystemAssert.AssertArgumentNotNull(batchUpdate, "批量更新命令不能为空");
            BulkWriteOperation<TEntity> bulk;
            if (!string.IsNullOrEmpty(dbName) && !string.IsNullOrEmpty(collectionName))
                bulk = this.Context.GetCollection<TEntity>(dbName, collectionName)
                    .InitializeOrderedBulkOperation();
            else
                bulk = this.Context.GetCollection<TEntity>()
                    .InitializeOrderedBulkOperation();
            foreach (var action in batchUpdate)
            {
                action(bulk);
            }
            bulk.Execute(wc);
        }

        public virtual void CreateCappedCollection(string dbName, string collectionName, long maxSize, long maxDocNum, bool dropExistsCollection = true)
        {
            YmtSystemAssert.AssertArgumentRange(maxSize, 0L, long.MaxValue, "maxSize 超过范围");
            YmtSystemAssert.AssertArgumentRange(maxDocNum, 0L, long.MaxValue, "maxDocNum 超过范围");

            var exist = this.Context.Database(dbName).CollectionExists(collectionName);
            if (dropExistsCollection && exist)
                this.Context.Database(dbName).DropCollection(collectionName);
            else if (exist)
                throw new Exception<MongodbRepositoryException>("collectionName exists");

            var options = CollectionOptions.SetCapped(true).SetMaxSize(maxSize).SetMaxDocuments(maxDocNum);
            this.Context.Database(dbName).CreateCollection(collectionName, options);
        }
        public virtual void CreateCappedCollection(long maxSize, long maxDocNum, bool dropExistsCollection = true)
        {
            YmtSystemAssert.AssertArgumentRange(maxSize, 0L, long.MaxValue, "maxSize 超过范围");
            YmtSystemAssert.AssertArgumentRange(maxDocNum, 0L, long.MaxValue, "maxDocNum 超过范围");

            var exist = this.Context.GetCollection<TEntity>().Exists();
            if (dropExistsCollection && this.Context.GetCollection<TEntity>().Exists())
                this.Context.GetCollection<TEntity>().Drop();
            else if (exist)
                throw new Exception<MongodbRepositoryException>("collectionName exists");

            var options = CollectionOptions.SetCapped(true).SetMaxSize(maxSize).SetMaxDocuments(maxDocNum);
            this.Context.GetCollection<TEntity>().Database.CreateCollection(Context.GetMapCfg<TEntity>().ToCollection, options);
        }
        public virtual ReplaceOneResult ReplaceOne(FilterDefinition<TEntity> filter, TEntity newDocument, string dbName = null, string collectionName = null
           , UpdateOptions updateOptions = null, CancellationToken token = default(CancellationToken))
        {
            return  this.Context.ContextNewCore.GetCollection<TEntity>(dbName, collectionName)
                 .ReplaceOne(filter, newDocument, updateOptions, token);
        }
        public virtual void Save(TEntity document, string dbName, string collectionName, WriteConcern writeConcern = null)
        {
            YmtSystemAssert.AssertArgumentNotEmpty(dbName, "数据库名不能为空");
            YmtSystemAssert.AssertArgumentNotEmpty(collectionName, "集合名称不能为空");

            if (writeConcern != null)
                this.Context.GetCollection<TEntity>(dbName, collectionName).Save(document, writeConcern);
            else
                this.Context.GetCollection<TEntity>(dbName, collectionName).Save(document);
        }
        public virtual WriteConcernResult Add(TEntity document, string dbName, string collectionName, MongoInsertOptions iOptions = null)
        {
            YmtSystemAssert.AssertArgumentNotEmpty(dbName, "数据库名不能为空");
            YmtSystemAssert.AssertArgumentNotEmpty(collectionName, "集合名称不能为空");

            if (iOptions == null)
                return this.Context.GetCollection<TEntity>(dbName, collectionName).Insert(document);
            return this.Context.GetCollection<TEntity>(dbName, collectionName).Insert(document, iOptions);
        }
        public virtual void Add(TEntity document, string dbName, string collectionName)
        {
            this.Context.GetCollection<TEntity>(dbName, collectionName).Insert(document);
        }
        public virtual WriteConcernResult Add(TEntity document, string dbName, string collectionName, WriteConcern writeConcern = null)
        {
            YmtSystemAssert.AssertArgumentNotEmpty(dbName, "数据库名不能为空");
            YmtSystemAssert.AssertArgumentNotEmpty(collectionName, "集合名称不能为空");

            if (writeConcern == null)
                return this.Context.GetCollection<TEntity>(dbName, collectionName).Insert(document);
            else return this.Context.GetCollection<TEntity>(dbName, collectionName).Insert(document, writeConcern);
        }
        public virtual long? Remove(IMongoQuery query, string dbName, string collectionName, WriteConcern writeConcern = null)
        {
            YmtSystemAssert.AssertArgumentNotEmpty(dbName, "数据库名不能为空");
            YmtSystemAssert.AssertArgumentNotEmpty(collectionName, "集合名称不能为空");

            if (writeConcern == null)
            {
                this.Context.GetCollection<TEntity>(dbName, collectionName).Remove(query);
                return 0L;
            }
            else
            {
                var result = this.Context.GetCollection<TEntity>(dbName, collectionName).Remove(query, writeConcern);
                return result.DocumentsAffected;
            }
        }

        public virtual Tuple<TResult, int> FindAndRemove<TResult>(IMongoQuery query, IMongoSortBy order, string dbName, string collectionName)
        {
            YmtSystemAssert.AssertArgumentNotEmpty(dbName, "数据库名不能为空");
            YmtSystemAssert.AssertArgumentNotEmpty(collectionName, "集合名称不能为空");

            var result = this.Context.GetCollection<TEntity>(dbName, collectionName).FindAndRemove(query, order);
            return Tuple.Create(result.GetModifiedDocumentAs<TResult>(), result.ModifiedDocument.ElementCount);
        }
        public virtual Tuple<TResult, int> FindAndRemove<TResult>(FindAndRemoveArgs args, string dbName, string collectionName)
        {
            YmtSystemAssert.AssertArgumentNotEmpty(dbName, "数据库名不能为空");
            YmtSystemAssert.AssertArgumentNotEmpty(collectionName, "集合名称不能为空");

            var result = this.Context.GetCollection<TEntity>(dbName, collectionName).FindAndRemove(args);
            return Tuple.Create(result.GetModifiedDocumentAs<TResult>(), result.ModifiedDocument.ElementCount);
        }
        public virtual Tuple<TResult, int> FindAndModify<TResult>(IMongoQuery query, IMongoUpdate update, string dbName, string collectionName, bool upSet = true)
        {
            YmtSystemAssert.AssertArgumentNotEmpty(dbName, "数据库名不能为空");
            YmtSystemAssert.AssertArgumentNotEmpty(collectionName, "集合名称不能为空");

            var result = this.Context.GetCollection<TEntity>(dbName, collectionName).FindAndModify(new FindAndModifyArgs { Query = query, Update = update, Upsert = upSet });
            return Tuple.Create(result.GetModifiedDocumentAs<TResult>(), result.ModifiedDocument.ElementCount);
        }
        public virtual Tuple<TResult, int> FindAndModify<TResult>(FindAndModifyArgs args, string dbName, string collectionName)
        {
            YmtSystemAssert.AssertArgumentNotEmpty(dbName, "数据库名不能为空");
            YmtSystemAssert.AssertArgumentNotEmpty(collectionName, "集合名称不能为空");

            var result = this.Context.GetCollection<TEntity>(dbName, collectionName).FindAndModify(args);
            return Tuple.Create(result.GetModifiedDocumentAs<TResult>(), result.ModifiedDocument.ElementCount);
        }
    }
}
