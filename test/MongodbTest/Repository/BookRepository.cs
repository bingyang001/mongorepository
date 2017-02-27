using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongodbTest.Context;
using MongodbTest.Domain;
using MongoDB.Driver;
using YmtSystem.Repository.Mongodb;
using YmtSystem.Repository.Mongodb.Context;
using YmtSystem.Repository.MongodbTest.Context;
using YmtSystem.Repository.MongodbTest.Repository;

namespace MongodbTest.Repository
{
    public class BookRepository : MongodbRepository<BookEntity>, IBookRepository
    {
        public BookRepository(MongodbContext context)
            : base(context)
        {

        }

        public BookRepository()
            : this(new BookContext())
        {

        }

        public BookEntity FindBookEntity(FilterDefinition<BookEntity> query, ProjectionDefinition<BookEntity, BookEntity> fields)
        {
            return this.ContextNewCore.GetCollection<BookEntity>().Find(query).Project(fields).ToList().FirstOrDefault();
        }        
    }
}
