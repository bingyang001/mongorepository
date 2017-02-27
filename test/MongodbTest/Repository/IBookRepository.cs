using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongodbTest.Domain;
using MongoDB.Driver;
using YmtSystem.Domain.MongodbRepository;

namespace MongodbTest.Repository
{
    public interface IBookRepository : IMongodbRepository<BookEntity>
    {
        BookEntity FindBookEntity(FilterDefinition<BookEntity> query, ProjectionDefinition<BookEntity, BookEntity> fields);
    }
}
