using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongodbTest.Domain;
using MongodbTest.Repository;
using MongoDB.Driver;

namespace MongodbTest.Test
{
    [TestClass]
    public class MongoNewApiTest
    {
        [TestMethod]
        public async Task AddOneAsync()
        {
            IBookRepository bookRepository = new BookRepository();
            await bookRepository.AddAsync(new BookEntity() {Author = Guid.NewGuid().ToString("N"), Price = 2.9, Title = "test"}).ConfigureAwait(false);
        }

        [TestMethod]
        public async Task AddManyAsync()
        {
            IBookRepository bookRepository = new BookRepository();
            var count = 50000;
            var list=new List<BookEntity>();
            for (var i = 0; i < count; i++)
            {
                list.Add(new BookEntity() {Author = Guid.NewGuid().ToString("N"), Price = 12.7, Title = "test" + i});
            }
            var stopWatch = Stopwatch.StartNew();
            await
                bookRepository.AddManyAsync(list).ConfigureAwait(false);
             stopWatch.Stop();
             Console.WriteLine("run {0:N0} ms,op/s:{1:N0},count:{2}", stopWatch.ElapsedMilliseconds, count * 1000 / stopWatch.ElapsedMilliseconds, count);
        }
        [TestMethod]
        public async Task BulkWriteAsync()
        {
            IBookRepository bookRepository = new BookRepository();
            var count = 50000;
            var list = new List<InsertOneModel<BookEntity>>();
            for (var i = 0; i < count; i++)
            {
                list.Add(new InsertOneModel<BookEntity>(new BookEntity() { Author = Guid.NewGuid().ToString("N"), Price = 12.7, Title = "test" + i }));
            }
            var stopWatch = Stopwatch.StartNew();
            await
                bookRepository.BulkWriteAsync(list).ConfigureAwait(false);
            stopWatch.Stop();
            Console.WriteLine("run {0:N0} ms,op/s:{1:N0},count:{2}", stopWatch.ElapsedMilliseconds, count * 1000 / stopWatch.ElapsedMilliseconds, count);
        }

        [TestMethod]
        public async Task FindOneAsync()
        {
            IBookRepository bookRepository = new BookRepository();
            var bookInfo =
                await
                    bookRepository.FindOneAsync(Builders<BookEntity>.Filter.Eq(b => b.Author,
                        "5ae7f5f258644d0fbb323cbdbf1da0f3")).ConfigureAwait(false);
            Assert.AreEqual(12.7, bookInfo.Price);
        }

        [TestMethod]
        public async Task UpdateAsync()
        {
            IBookRepository bookRepository=new BookRepository();
            var up =await bookRepository.UpdateManyAsync(
                Builders<BookEntity>.Filter.Eq(b => b.Author, "d6ac9cefbfd04cddaac93925fdadac56"),
                Builders<BookEntity>.Update.Set(b => b.Price, 100.2)).ConfigureAwait(false);
            Assert.AreEqual(1,up.ModifiedCount);
            var bookInfo =
               await
                   bookRepository.FindOneAsync(Builders<BookEntity>.Filter.Eq(b => b.Author,
                       "d6ac9cefbfd04cddaac93925fdadac56")).ConfigureAwait(false);
            Assert.AreEqual(100.2, bookInfo.Price);
        }
        [TestMethod]
        public void BulkWrite()
        {
            var list = new List<InsertOneModel<BookEntity>>();
            var count = 50000;
            IBookRepository repo = new BookRepository();
            for (var i = 0; i < count; i++)
            {
                var book = new BookEntity{Author = Guid.NewGuid().ToString("N"),Price = i,Title = "test."+i};

                list.Add(new InsertOneModel<BookEntity>(book));
            }
            var watch = Stopwatch.StartNew();
            var result= repo.BulkOperating(list);
            watch.Stop();
            Console.WriteLine("run {0:N0} ms,op/s:{1:N0},count:{2}", watch.ElapsedMilliseconds, count * 1000 / watch.ElapsedMilliseconds, count);
            Assert.AreEqual(count, result.InsertedCount);
        }

        [TestMethod]
        public void BulkUpdate()
        {
            var list = new List<UpdateManyModel<BookEntity>>();            
            IBookRepository repo = new BookRepository();      
            list.Add(new UpdateManyModel<BookEntity>(Builders<BookEntity>.Filter.Empty,
                Builders<BookEntity>.Update.Set(b => b.Price, 500)));            
            var watch = Stopwatch.StartNew();
            var result = repo.BulkOperating(list);
            watch.Stop();
            Console.WriteLine("run {0:N0} ms,op/s:{1:N0},count:{2}", watch.ElapsedMilliseconds, result.ModifiedCount * 1000 / watch.ElapsedMilliseconds, result.ModifiedCount);
            var count = repo.CountAsync(Builders<BookEntity>.Filter.Empty).Result;
            Assert.AreEqual(count, result.ModifiedCount);
        }
        [TestMethod]
        public async  Task BulkUpdateAsync()
        {
            var list = new List<UpdateManyModel<BookEntity>>();
            IBookRepository repo = new BookRepository();
            list.Add(new UpdateManyModel<BookEntity>(Builders<BookEntity>.Filter.Empty,
                Builders<BookEntity>.Update.Set(b => b.Price, 1210)));
            var watch = Stopwatch.StartNew();
            var result =await repo.BulkOperatingAsync(list).ConfigureAwait(false);
            watch.Stop();
            Console.WriteLine("run {0:N0} ms,op/s:{1:N0},count:{2}", watch.ElapsedMilliseconds, result.ModifiedCount * 1000 / watch.ElapsedMilliseconds, result.ModifiedCount);
            var count =await repo.CountAsync(Builders<BookEntity>.Filter.Empty).ConfigureAwait(false);
            Assert.AreEqual(count, result.ModifiedCount);
        }

        [TestMethod]
        public void FindOne()
        {
            IBookRepository repo = new BookRepository();
            var bookInfo = repo.FindBookEntity(Builders<BookEntity>.Filter.Eq("_id", "13cbe695c4f444aab81c8dfe6aaf3397"),
                Builders<BookEntity>.Projection.Include("Author").Include("Price"));

            Assert.AreEqual("13cbe695c4f444aab81c8dfe6aaf3397",bookInfo.Author);
            Assert.AreEqual(1210, bookInfo.Price);
        }
    }
}
