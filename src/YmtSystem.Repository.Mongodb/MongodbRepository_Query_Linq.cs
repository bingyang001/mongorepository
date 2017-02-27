using System;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver.Linq;
using YmtSystem.Domain.MongodbRepository;

namespace YmtSystem.Repository.Mongodb
{
    public partial class MongodbRepository<TEntity> : IMongodbRepository<TEntity>
       where TEntity : class
    {
        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> exp)
        {
            return this.Context.GetCollection<TEntity>().AsQueryable().Where(exp);
        }      
        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> exp, string dbName, string collectionName)
        {
            return this.Context.GetCollection<TEntity>(dbName, collectionName).AsQueryable().Where(exp);
        }
    }
}
