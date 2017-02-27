using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YmtSystem.Repository.MongodbTest.Domain
{
    public class User
    {
        public string UId { get; private set; }
        public string UName { get; private set; }
        public DateTime CreateTime { get; private set; }
        public List<Address> UserAddress { get; private set; }
        public int UType { get; private set; }
  
        public User(string uId, string uName,int uType)
        {
            this.UId = uId;
            this.UName = uName;
            this.CreateTime = DateTime.Now;
            this.UType = uType;
        }

        public void AddUserAddress(Address address)
        {
            if (UserAddress == null)
                UserAddress = new List<Address>();

            UserAddress.Add(address);
        }

        protected User()
        {

        }
    }

    public class Address
    {
        public string Country { get; private set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public bool IsDefault { get; private set; }

        public Address(string country, string state, string city, string street, int zip, bool isDefault)
        {
            this.Country = country;
            this.State = state;
            this.City = city;
            this.Street = street;
            this.Zip = zip;
            this.IsDefault = IsDefault;
        }

        public void SetDefault(bool @default)
        {
            this.IsDefault = @default;
        }

        protected Address()
        {
        }
    }

}
