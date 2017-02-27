namespace YmtSystem.Repository.Mongodb.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MongoDB.Bson.Serialization;

    public static class BsonClassMapExtend
    {
        /// <summary>
        /// 实体映射到数据库和集合
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="map"></param>
        /// <param name="dbName"></param>
        /// <param name="collectionName"></param>
        public static void ToDatatbaseAndCollection<TEntity>(this BsonClassMap<TEntity> map, string dbName, string collectionName)
        {
            new EntityMappingConfigure
            {
                MappType = typeof(TEntity),
                ToDatabase = dbName,
                ToCollection=collectionName
            };
        }
        /// <summary>
        /// 实体映射到数据库和集合
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="map"></param>
        /// <param name="mapCfg"></param>
        public static void ToDatatbaseAndCollection<TEntity>(this BsonClassMap<TEntity> map, EntityMappingConfigure mapCfg)
        {
           
        }
    }
}
