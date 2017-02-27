using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YmtSystem.Repository.Mongodb.Context;
using YmtSystem.Repository.MongodbTest.Mapping;
using System.Configuration;

namespace YmtSystem.Repository.MongodbTest.Context
{
    public class UserContext : MongodbContext
    {
        public UserContext()
            : base(ConfigurationManager.AppSettings["mongotest"])
        {

        }

        protected override void OnEntityMap(EntityClassMap map, string contextName)
        {
            map.AddMap(new UserMapping().MapToDbCollection(), contextName);
        }
    }
}
