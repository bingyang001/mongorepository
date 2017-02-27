using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using YmtSystem.MongodbRepository._Assert;
using YmtSystem.Repository.Mongodb.Mapping;

namespace YmtSystem.Repository.Mongodb.Context
{
    public class MongodbContext_NewCore 
    {
        private MongoClient client;
        private EntityClassMap cfg;
        private string contextName;

        public MongodbContext_NewCore(MongoClient client, EntityClassMap cfg, string contextName)
        {
            YmtSystemAssert.AssertArgumentNotNull(client,"mongo client instance cant'null");
            YmtSystemAssert.AssertArgumentNotNull(cfg, "cfg instance cant'null");
            YmtSystemAssert.AssertArgumentNotEmpty(contextName, "contextName cant'null");

            this.client = client;
            this.cfg = cfg;
            this.contextName = contextName;
        }
        public IMongoCollection<TEntity> GetCollection<TEntity>(string dbName = null, string collectionName=null)
        {
            if (string.IsNullOrEmpty(dbName) && string.IsNullOrEmpty(collectionName))
            {
                var cfg = GetMapCfg<TEntity>();
                return Database(cfg.ToDatabase).GetCollection<TEntity>(cfg.ToCollection);
            }
            return _GetCollection<TEntity>(dbName, collectionName);
        }
        private IMongoCollection<TEntity> _GetCollection<TEntity>(string dbName, string collectionName)
        {
            YmtSystemAssert.AssertArgumentNotEmpty(dbName, "数据库名不能为空");
            YmtSystemAssert.AssertArgumentNotEmpty(collectionName, "集合名不能为空");
            return this.Database(dbName).GetCollection<TEntity>(collectionName);
        }
        public IMongoCollection<BsonDocument> GetCollection(string dbName, string collectionName)
        {
            YmtSystemAssert.AssertArgumentNotEmpty(dbName, "数据库名不能为空");
            YmtSystemAssert.AssertArgumentNotEmpty(collectionName, "集合名不能为空");
            return this.Database(dbName).GetCollection<BsonDocument>(collectionName);
        }
        public IMongoDatabase Database<TEntity>()
        {          
            return Database(GetMapCfg<TEntity>().ToDatabase);
        }
        public IMongoDatabase Database(string dbName)
        {
            YmtSystemAssert.AssertArgumentNotEmpty(dbName, "数据库名不能为空");
            return this.client.GetDatabase(dbName);
        }
        private EntityMappingConfigure GetMapCfg<TEntity>()
        {
            var cfgInfo = this.cfg.GetMap(contextName).FirstOrDefault(e => e.MappType == typeof(TEntity));
            YmtSystemAssert.AssertArgumentNotNull(cfgInfo, string.Format("entity {0} no mapping.", typeof(TEntity).FullName));
            return cfgInfo;
        }        
    }
}