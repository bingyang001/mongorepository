using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace YmtSystem.Domain.MongodbRepository
{
    public partial interface IMongodbRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> exp);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> exp, string dbName, string collectionName);
    }
}
