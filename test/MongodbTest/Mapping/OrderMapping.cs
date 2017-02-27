using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using YmtSystem.Repository.Mongodb.Mapping;
using YmtSystem.Repository.MongodbTest.Domain;

namespace YmtSystem.Repository.MongodbTest.Mapping
{
    public class OrderMapping : ModelMappingBase<Order>
    {
        public OrderMapping()
        {
            BsonClassMap.RegisterClassMap<Order>(map =>
            {
                map.MapIdProperty(o => o.OrderId);
                map.MapProperty(o => o.BuyerName).SetElementName("Name").SetIgnoreIfNull(true);
                map.MapProperty(o => o.dMoney).SetElementName("dM").SetIgnoreIfNull(true);
                map.MapProperty(o => o.sType).SetElementName("sType").SetIgnoreIfNull(true);
            });
        }
        public override EntityMappingConfigure MapToDbCollection()
        {
            return new EntityMappingConfigure
            {
                MappType = typeof(Order),
                ToCollection = "Order",
                ToDatabase = "Test_User"
            };
        }
    }
}
