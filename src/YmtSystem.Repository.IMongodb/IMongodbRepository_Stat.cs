using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace YmtSystem.Domain.MongodbRepository
{
    partial interface IMongodbRepository<TEntity> where TEntity : class
    {
        MapReduceResult MapRedurce(MapReduceArgs args, string dbName, string collectionName);
        IEnumerable<TResult> MapRedurce<TResult>(MapReduceArgs args, string dbName, string collectionName);
        IEnumerable<BsonDocument> Group(GroupArgs args, string dbName, string collectionName);
        IEnumerable<BsonDocument> Aggregation(AggregateArgs args, string dbName, string collectionName);
        MapReduceResult MapRedurce(MapReduceArgs args);
        IEnumerable<TResult> MapRedurce<TResult>(MapReduceArgs args);
        IEnumerable<BsonDocument> Group(GroupArgs args);
        IEnumerable<BsonDocument> Aggregation(AggregateArgs args);
    }
}
