using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YmtSystem.Repository.Mongodb.Context;
using YmtSystem.Repository.MongodbTest.Mapping;

namespace YmtSystem.Repository.MongodbTest.Context
{
    public class OrderContext : MongodbContext
    {
        public OrderContext()
            : base(ConfigurationManager.AppSettings["mongotest"])
        {

        }

        protected override void OnEntityMap(EntityClassMap map, string contextName)
        {
            map.AddMap(new OrderMapping().MapToDbCollection(), contextName);
        }
    }
}
