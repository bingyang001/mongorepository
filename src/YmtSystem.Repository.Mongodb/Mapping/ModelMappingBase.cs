namespace YmtSystem.Repository.Mongodb.Mapping
{
    using System;
    using MongoDB.Bson.Serialization;
    /// <summary>
    ///  模型属性映射，DB和集合关系映射基类。
    ///  <see cref="http://docs.mongodb.org/ecosystem/tutorial/serialize-documents-with-the-csharp-driver/"/>
    /// </summary>  
    /// <typeparam name="TEntity"></typeparam>
    public abstract class ModelMappingBase<TEntity> : BsonClassMap<TEntity>
    {
        /// <summary>
        /// 实体与数据库，集合关系映射
        /// </summary>
        public abstract EntityMappingConfigure MapToDbCollection();
    }
}
