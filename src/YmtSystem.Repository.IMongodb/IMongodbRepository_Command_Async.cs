using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace YmtSystem.Domain.MongodbRepository
{
    public partial interface IMongodbRepository<TEntity> where TEntity : class
    {        
        Task AddAsync(TEntity entity, string dbName=null, string collectionName=null, CancellationToken token = default(CancellationToken));        
        Task AddManyAsync(IEnumerable<TEntity> entities, string dbName=null, string collectionName=null, CancellationToken token = default(CancellationToken));
        Task<BulkWriteResult<TEntity>> BulkWriteAsync(IEnumerable<InsertOneModel<TEntity>> entities, string dbName = null, string collectionName = null,
           CancellationToken token = default(CancellationToken));
        Task<UpdateResult> UpdateOneAsync(FilterDefinition<TEntity> queryfilter, UpdateDefinition<TEntity> upFilter,
            string dbName = null,
            string collectionName = null, UpdateOptions op = null, CancellationToken toke = default(CancellationToken));
        Task<UpdateResult> UpdateManyAsync(FilterDefinition<TEntity> queryfilter, UpdateDefinition<TEntity> upFilter,
            string dbName = null,
            string collectionName = null, UpdateOptions op = null, CancellationToken toke = default(CancellationToken));
        Task<DeleteResult> DeleteOneAsync(FilterDefinition<TEntity> queryfilter, string dbName = null,
            string collectionName = null, UpdateOptions op = null, CancellationToken token = default(CancellationToken));
        Task<BulkWriteResult<TEntity>> BulkOperatingAsync(IEnumerable<WriteModel<TEntity>> upModel,
           string dbName = null, string collectionName = null,BulkWriteOptions bwOptions=null, CancellationToken token = default(CancellationToken));
        Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<TEntity> filter, TEntity newDocument, string dbName = null,
            string collectionName = null
            , UpdateOptions updateOptions = null, CancellationToken token = default(CancellationToken));
    }
}
