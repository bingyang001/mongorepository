using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Xunit;
using Xunit.Extensions;
using YmtSystem.Repository.MongodbTest.Domain;
using YmtSystem.Repository.MongodbTest.Repository;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace YmtSystem.Repository.MongodbTest.Test
{
     [TestClass]
    public class UserTest
    {
        [Fact]
        [TestMethod]
        public void AddOrder()
        {
            IOrderRepository orderRepo = new OrderRepository();
            orderRepo.Add(new Domain.Order
            {
                BuyerName = "zhangsan",
                dMoney = 10.1M,
                OrderId = Guid.NewGuid().ToString("N"),
                sType = "m"
            }, new WriteConcern(1));
        }
        [Fact]
        [TestMethod]
        public void AddUserTest()
        {
            IUserRepository repo = new UserRepository();
            for (var i = 0; i < 2; i++)
            {
                var user = new User("u_" + i, "test00" + i, i % 2);
                user.AddUserAddress(new Address("中国", "上海", "上海市", "闸北,灵石路 xx", 123456, true));
                repo.Add(user);
            }
        }
        [Fact]
        [TestMethod]
        public void CreateCappedConnection()
        {
            IUserRepository repo = new UserRepository();
            repo.CreateCappedCollection("test_c", "test_c1", 1000, 1000);
        }
        [Fact]
        [TestMethod]
        public void UpdateUser()
        {
            var query = Query.EQ("_id", "u_1");
            var up = Update.Set("uName", "lisi");
            IUserRepository repo = new UserRepository();
            var result = repo.Update(query, up);
            Assert.AreEqual(false, result.HasLastErrorMessage);
            Assert.AreEqual(1, result.DocumentsAffected);
        }
        [Fact]
        [TestMethod]
        public void LinqToMonogo()
        {
            IUserRepository repo = new UserRepository();
            var result = repo.Find(u => u.UId == "u_0");

            Assert.AreEqual(1, result.Count());
        }
        [Fact]
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void LinqToMonogoGroupBy()
        {         
            IUserRepository repo = new UserRepository();
            //mongo linq 不支持统计
            var result = repo.Find(u => u.UType == 0).GroupBy(e => e.UName);           
        }
        [Fact]
        [TestMethod]
        public void Group()
        {
            IUserRepository repo = new UserRepository();
            var result = repo.Group(new GroupArgs
            {
                KeyFields = GroupBy.Keys("type"),//设置了 KeyFields 则无需设置 KeyFunction
                Query = Query.GTE("cTime", DateTime.Now.AddDays(-3)),
                Initial = new BsonDocument("count", 0),
                ReduceFunction = "function(doc,per){return per.count+=1;}",
            }).ToArray();

            Assert.AreEqual(2, result.Length);
        }
        [Fact]
        [TestMethod]
        public void MarpReduce()
        {
            //this 为当前文档，this.name 表示按name统计
            //相同name 计数 1
            var map = "function() { emit(this.uName,{count:1}) };";
            //迭代计算，相同name加1
            var reduce = @"function(key,emits){ 
                            var total=0; 
                            for(var i in emits){
                                total += emits[i].count;
                            }
                            return {count:total};
                        };";
            IUserRepository repo = new UserRepository();
            var result = repo.MapRedurce(new MapReduceArgs
             {
                 Query = Query.GTE("cTime", DateTime.Now.AddDays(-3)),
                 MapFunction = map,
                 ReduceFunction = reduce,
                 OutputCollectionName = "u_stats",
                 OutputMode = MapReduceOutputMode.Replace,//替换老的文档
             });
            Assert.AreEqual(true, result.Ok);
            //获取结果
            var resultDocument = result.GetResults();
        }
        [Fact]
        [TestMethod]
        public void Aggregate()
        {
            IUserRepository repo = new UserRepository();
            var results = repo.Aggregation(new AggregateArgs
            {
                OutputMode = AggregateOutputMode.Cursor,
                Pipeline = new BsonDocument[] 
                {
                    //$group 表示分组统计
                    new BsonDocument("$group",new BsonDocument
                    {
                        //指定输出文档_id
                        {"_id","$uName"},
                        //指定输出文档 count 属性；$sum 内置统计关键词类似SQL sum
                        {"count",new BsonDocument("$sum",1)}
                    })
                    //, new BsonDocument("$out", "temp")
                }
            });
            var dictionary = new Dictionary<string, int>();
            //遍历结果
            foreach (var result in results)
            {
                var x = result["_id"].AsString;
                var count = result["count"].AsInt32;
                Console.WriteLine(x + " -> " + count);
                dictionary[x] = count;
            }
        }

         [TestMethod]
        public void BatchAdd_New()
         {
             var list=new List<User>();
             IUserRepository repo = new UserRepository();
             var count = 50000;
             for (var i = 0; i < count; i++)
             {
                 var user = new User(Guid.NewGuid().ToString("N"), DateTime.Now.ToString("yyyyMMddHHmmss"), 0);
                 user.AddUserAddress( new Address("中国", "上海", "上海市", "闸北,灵石路 xx", 123456, true));
                 list.Add(user);
             }
             var watch = Stopwatch.StartNew();
             repo.BatchAdd_New(list,new WriteConcern(1));
             watch.Stop();
             Console.WriteLine("run {0:N0} ms,op/s:{1:N0},count:{2}", watch.ElapsedMilliseconds, count * 1000 / watch.ElapsedMilliseconds,count);
         }
         [TestMethod]
         public void BatchAdd_Old()
         {
             var list = new List<User>();
             var count = 50000;
             IUserRepository repo = new UserRepository();
             for (var i = 0; i < count; i++)
             {
                 var user = new User(Guid.NewGuid().ToString("N"), DateTime.Now.ToString("yyyyMMddHHmmss"), 0);
                 user.AddUserAddress(new Address("中国", "上海", "上海市", "闸北,灵石路 xx", 123456, true));
                 list.Add(user);
             }
             var watch = Stopwatch.StartNew();
             repo.BatchAdd(list, new WriteConcern(1));
             watch.Stop();
             Console.WriteLine("run {0:N0} ms,op/s:{1:N0},count:{2}", watch.ElapsedMilliseconds, count * 1000 / watch.ElapsedMilliseconds,count);
         }        
    }
}
