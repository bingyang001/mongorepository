using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using YmtSystem.Domain.MongodbRepository;

namespace YmtSystem.Repository.Mongodb
{
    public partial class MongodbRepository<TEntity> : IMongodbRepository<TEntity>
         where TEntity : class
    {       
        public async Task AddAsync(TEntity entity, string dbName = null, string collectionName = null, CancellationToken token = default(CancellationToken))
        {
            await this.ContextNewCore.GetCollection<TEntity>(dbName, collectionName).InsertOneAsync(entity, cancellationToken: token).ConfigureAwait(false);
        }       
        public async Task AddManyAsync(IEnumerable<TEntity> entities, string dbName = null, string collectionName = null, CancellationToken token = default(CancellationToken))
        {
            await this.ContextNewCore.GetCollection<TEntity>(dbName,collectionName).InsertManyAsync(entities,cancellationToken:token).ConfigureAwait(false);
        }       
        public async Task<BulkWriteResult<TEntity>> BulkWriteAsync(IEnumerable<InsertOneModel<TEntity>> entities, string dbName=null,
            string collectionName=null,
            CancellationToken token = default(CancellationToken))
        {
            return await this.Context.ContextNewCore.GetCollection<TEntity>(dbName, collectionName).BulkWriteAsync(entities,cancellationToken: token).ConfigureAwait(false);
        }
        public async Task<UpdateResult> UpdateManyAsync(FilterDefinition<TEntity> queryfilter, UpdateDefinition<TEntity> upFilter, string dbName = null,
            string collectionName=null,UpdateOptions op=null,CancellationToken toke=default(CancellationToken))
        {
            return
                await
                    this.ContextNewCore.GetCollection<TEntity>(dbName, collectionName)
                        .UpdateManyAsync(queryfilter, upFilter, op, toke)
                        .ConfigureAwait(false);
        }
        public async Task<UpdateResult> UpdateOneAsync(FilterDefinition<TEntity> queryfilter, UpdateDefinition<TEntity> upFilter, string dbName = null,
           string collectionName = null, UpdateOptions op = null, CancellationToken toke = default(CancellationToken))
        {
            return
                await
                    this.ContextNewCore.GetCollection<TEntity>(dbName, collectionName)
                        .UpdateOneAsync(queryfilter, upFilter, op, toke)
                        .ConfigureAwait(false);
        }
        public async Task<DeleteResult> DeleteOneAsync(FilterDefinition<TEntity> queryfilter, string dbName = null,
           string collectionName = null, UpdateOptions op = null, CancellationToken token = default(CancellationToken))
        {
            return
                await
                    this.ContextNewCore.GetCollection<TEntity>(dbName, collectionName)
                        .DeleteOneAsync(queryfilter,cancellationToken:token)
                        .ConfigureAwait(false);
        } 
        public async  Task<BulkWriteResult<TEntity>> BulkOperatingAsync(IEnumerable<WriteModel<TEntity>> upModel,
            string dbName = null, string collectionName = null, BulkWriteOptions bwOptions = null,
            CancellationToken token = default(CancellationToken))
        {
            return await this.ContextNewCore.GetCollection<TEntity>(dbName, collectionName)
               .BulkWriteAsync(upModel, cancellationToken: token)
               .ConfigureAwait(false);
        }

        public async Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<TEntity> filter, TEntity newDocument, string dbName = null, string collectionName = null
            ,UpdateOptions updateOptions=null,CancellationToken token=default(CancellationToken))
        {
           return await this.ContextNewCore.GetCollection<TEntity>(dbName, collectionName)
                .ReplaceOneAsync(filter, newDocument, updateOptions, token).ConfigureAwait(false);
        }
    }
}
