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
        Task<TEntity> FindOneAsync(FilterDefinition<TEntity> filter, string dbName = null, string collectionName = null, CancellationToken token = default(CancellationToken));
        Task<List<TProjection>> FindAsync<TProjection>(FilterDefinition<TEntity> filter,
            string dbName = null, string collectionName = null,FindOptions<TEntity, TProjection> options = null, CancellationToken token = default(CancellationToken));
        Task<long> CountAsync(FilterDefinition<TEntity> filter,
            string dbName = null, string collectionName = null, CountOptions options = null,
            CancellationToken token = default(CancellationToken));
    }
}
