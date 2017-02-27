namespace YmtSystem.Domain.MongodbRepository
{
    using System;
    using MongoDB.Bson;
    using MongoDB.Driver;
    
    /// <summary>
    /// mongodb 上下文。
    /// </summary>
    /// <remarks>
    /// 注：mongodb 本身不支持事务，所以不存在 UnitOfWork 模式
    /// </remarks>
    public interface IMongodbContext
    {
        /// <summary>
        ///  获取集合
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dbName">dbName</param>
        /// <param name="collectionName">collectionName</param>
        /// <returns></returns>
        MongoCollection<TEntity> GetCollection<TEntity>(string dbName, string collectionName);
        /// <summary>
        /// 获取集合
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        MongoCollection<TEntity> GetCollection<TEntity>();
        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="dbName">dbName</param>
        /// <param name="collectionName">collectionName</param>
        /// <returns></returns>
        MongoCollection<BsonDocument> GetCollection(string dbName, string collectionName);     
    }
}
