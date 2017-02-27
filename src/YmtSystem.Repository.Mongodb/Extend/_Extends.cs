using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace YmtSystem.Repository.Mongodb.Extend
{
    public static class _DictionaryExtends
    {
        public static bool IsEmpty(this IDictionary dic) 
        {
            if (dic == null || dic.Count <= 0) return true;
            return false;
        }

        public static IEnumerable<InsertOneModel<TEntity>> ToInsertOneModel<TEntity>(this IEnumerable<TEntity> entities)
        {
            var list = new List<InsertOneModel<TEntity>>(entities.Count());
            foreach (var entity in entities)
            {
               list.Add(new InsertOneModel<TEntity>(entity)); 
            }
            return list;
        }
        public static IEnumerable<InsertOneModel<TEntity>> ToInsertOneModelParallel<TEntity>(this IEnumerable<TEntity> entities)
        {
            var list = new ConcurrentBag<InsertOneModel<TEntity>>();
            Parallel.ForEach(entities, e => list.Add(new InsertOneModel<TEntity>(e)));
            return list;
        }
    }
}
