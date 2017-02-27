using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YmtSystem.Repository.MongodbTest.Domain
{
    public class Order
    {
       
        public string OrderId { get; set; }
        public string sType { get; set; }
        public decimal dMoney { get; set; }
        public string BuyerName { get; set; }

    }
}
