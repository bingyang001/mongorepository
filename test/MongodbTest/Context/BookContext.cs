using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongodbTest.Mapping;
using YmtSystem.Repository.Mongodb.Context;
using YmtSystem.Repository.MongodbTest.Mapping;

namespace MongodbTest.Context
{
  public  class BookContext : MongodbContext
    {
      public BookContext()
            : base(ConfigurationManager.AppSettings["mongotest"])
        {

        }

        protected override void OnEntityMap(EntityClassMap map, string contextName)
        {
            map.AddMap(new BookMapping().MapToDbCollection(), contextName);
        }
    }
}
