using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YmtSystem.Repository.Mongodb;
using YmtSystem.Repository.Mongodb.Context;
using YmtSystem.Repository.MongodbTest.Context;
using YmtSystem.Repository.MongodbTest.Domain;
using YmtSystem.Repository.MongodbTest.Repository;

namespace YmtSystem.Repository.MongodbTest
{
    public class UserRepository : MongodbRepository<User>, IUserRepository
    {       
        public UserRepository(MongodbContext context)
            : base(context)
        {
           
        }
        public UserRepository()
            : this(new UserContext())
        {

        }
    }
}
