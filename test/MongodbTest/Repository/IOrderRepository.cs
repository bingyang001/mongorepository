using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YmtSystem.Domain.MongodbRepository;
using YmtSystem.Repository.MongodbTest.Domain;

namespace YmtSystem.Repository.MongodbTest.Repository
{
    public interface IOrderRepository : IMongodbRepository<Order>
    {
    }
}
