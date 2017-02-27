using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YmtSystem.Repository.Mongodb;
using YmtSystem.Repository.Mongodb.Context;
using YmtSystem.Repository.MongodbTest.Context;
using YmtSystem.Repository.MongodbTest.Domain;

namespace YmtSystem.Repository.MongodbTest.Repository
{
    public class OrderRepository : MongodbRepository<Order>, IOrderRepository
    {
         public OrderRepository(MongodbContext context)
            : base(context)
        {
           
        }
         public OrderRepository()
             : this(new OrderContext())
        {

        }
    }
}
