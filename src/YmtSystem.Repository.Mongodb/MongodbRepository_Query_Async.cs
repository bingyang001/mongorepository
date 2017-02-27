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
        public async Task<TEntity> FindOneAsync(FilterDefinition<TEntity> filter, string dbName = null, string collectionName = null, CancellationToken token = default(CancellationToken))
        {
           var result= await this.ContextNewCore.GetCollection<TEntity>(dbName,collectionName).FindAsync(filter, cancellationToken: token).ConfigureAwait(false);
            var info = await result.ToListAsync(token).ConfigureAwait(false);
            return info.FirstOrDefault();
        }

        public async Task<List<TProjection>> FindAsync<TProjection>(FilterDefinition<TEntity> filter,
            string dbName = null, string collectionName = null, FindOptions<TEntity, TProjection> options = null, CancellationToken token = default(CancellationToken))
        {
            var result = await this.ContextNewCore.GetCollection<TEntity>(dbName,collectionName).FindAsync(filter, options, cancellationToken: token).ConfigureAwait(false);
            return await result.ToListAsync(token).ConfigureAwait(false);           
        }

        public async Task<long> CountAsync(FilterDefinition<TEntity> filter,
            string dbName = null, string collectionName = null, CountOptions options = null, CancellationToken token = default(CancellationToken))
        {
           return await this.ContextNewCore.GetCollection<TEntity>(dbName, collectionName).CountAsync(filter, options, cancellationToken: token).ConfigureAwait(false);           
        }
    }
}
