using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using YmtSystem.Repository.Mongodb.Mapping;
using YmtSystem.Repository.MongodbTest.Domain;

namespace YmtSystem.Repository.MongodbTest.Mapping
{
    public class UserMapping : ModelMappingBase<User>
    {
        public UserMapping()
        {
//            if (!BsonClassMap.IsClassMapRegistered(typeof (User)))
//            {
                BsonClassMap.RegisterClassMap<User>(map =>
                {

                    map.MapIdProperty(u => u.UId); //.SetIdGenerator(new StringObjectIdGenerator());
                    map.MapProperty(u => u.CreateTime).SetElementName("cTime");
                    map.MapProperty(u => u.UName).SetElementName("uName").SetIgnoreIfNull(true);
                    map.MapProperty(u => u.UserAddress).SetElementName("uAddress").SetIgnoreIfNull(true);
                    map.MapProperty(u => u.UType).SetElementName("type");
                });
//            }
//            if (!BsonClassMap.IsClassMapRegistered(typeof (Address)))
//            {
                BsonClassMap.RegisterClassMap<Address>(map =>
                {
                    map.MapProperty(a => a.Country).SetElementName("C").SetIgnoreIfNull(true);
                    map.MapProperty(a => a.City).SetElementName("C2").SetIgnoreIfNull(true);
                    map.MapProperty(a => a.State).SetElementName("S").SetIgnoreIfNull(true);
                    map.MapProperty(a => a.Street).SetElementName("S2").SetIgnoreIfNull(true);
                    map.MapProperty(a => a.IsDefault).SetElementName("DefVal");
                });
//            }
        }

        public override EntityMappingConfigure MapToDbCollection()
        {
            return new EntityMappingConfigure
            {
                MappType = typeof(User),
                ToCollection = "user",
                ToDatabase = "Test_User"
            };
        }
    }
}
