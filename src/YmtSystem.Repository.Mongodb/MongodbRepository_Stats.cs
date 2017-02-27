using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using YmtSystem.Domain.MongodbRepository;

namespace YmtSystem.Repository.Mongodb
{

    public partial class MongodbRepository<TEntity> : IMongodbRepository<TEntity>
       where TEntity : class
    {
        public virtual MapReduceResult MapRedurce(MapReduceArgs args,string dbName,string collectionName) 
        {
            return this.Context.GetCollection<TEntity>(dbName,collectionName).MapReduce(args);
        }
        public virtual MapReduceResult MapRedurce(MapReduceArgs args)
        {
            return this.Context.GetCollection<TEntity>().MapReduce(args);
        }
        public virtual IEnumerable<TResult> MapRedurce<TResult>(MapReduceArgs args, string dbName, string collectionName)
        {
            return this.Context.GetCollection<TEntity>(dbName, collectionName).MapReduce(args).GetResultsAs<TResult>();
        }
        public virtual IEnumerable<TResult> MapRedurce<TResult>(MapReduceArgs args)
        {
            return this.Context.GetCollection<TEntity>().MapReduce(args).GetResultsAs<TResult>();
        }
        public virtual IEnumerable<BsonDocument> Group(GroupArgs args, string dbName, string collectionName)
        {
            return this.Context.GetCollection<TEntity>(dbName, collectionName).Group(args);
        }
        public virtual IEnumerable<BsonDocument> Group(GroupArgs args)
        {
            return this.Context.GetCollection<TEntity>().Group(args);
        }
        public virtual IEnumerable<BsonDocument> Aggregation(AggregateArgs args, string dbName, string collectionName)
        {
            return this.Context.GetCollection<TEntity>(dbName, collectionName).Aggregate(args);
        }
        public virtual IEnumerable<BsonDocument> Aggregation(AggregateArgs args)
        {
            return this.Context.GetCollection<TEntity>().Aggregate(args);
        }
    }
}
