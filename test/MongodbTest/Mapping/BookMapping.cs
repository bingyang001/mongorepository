using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongodbTest.Domain;
using YmtSystem.Repository.Mongodb.Mapping;

namespace MongodbTest.Mapping
{
    public class BookMapping : ModelMappingBase<BookEntity>
    {
        public BookMapping()
        {
            BsonClassMap.RegisterClassMap<BookEntity>(m =>
            {
                m.MapIdProperty(_ => _.Author);
                m.MapProperty(_ => _.Price);
                m.MapProperty(_ => _.Title);
            });
        }

        public override EntityMappingConfigure MapToDbCollection()
        {
            return new EntityMappingConfigure()
            {
                MappType = typeof(BookEntity),
                ToDatabase = "Test_User",
                ToCollection = "Book"
            };
        }
    }
}
