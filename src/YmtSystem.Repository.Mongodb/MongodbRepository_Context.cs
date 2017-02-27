using System;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using YmtSystem.MongodbRepository._Assert;
using YmtSystem.Repository.Mongodb.Context;

namespace YmtSystem.Repository.Mongodb
{
    public partial class MongodbRepository<TEntity> where TEntity : class
    {
        private MongodbContext context;
       
        public MongodbRepository(MongodbContext context)
        {
            this.context = context;
        }
            
        protected MongodbContext Context {
        get
        {
            YmtSystemAssert.AssertArgumentNotNull(this.context, "context not init.");
            return this.context;
        }}
        protected MongodbContext_NewCore ContextNewCore
        {
            get
            {
                YmtSystemAssert.AssertArgumentNotNull(this.context.ContextNewCore, "ContextNewCore not init.");
                return this.context.ContextNewCore;
            }
        }
        private async Task<TResult> ExecuteAsync<TResult>(Func<TResult> fn, TResult defReturn = default(TResult), int millisecondsDelay = 3000, Action callback = null, Action<Exception> errorHandler = null)
        {
            var cancel = new CancellationTokenSource(millisecondsDelay);
            var ct = cancel.Token;
            if (callback != null)
                ct.Register(callback);
            try
            {
                return await Task.Run(() =>
                {
                    if (!ct.IsCancellationRequested)
                        return fn();
                    ct.ThrowIfCancellationRequested();
                    return defReturn;
                }, ct);
            }
            catch (AggregateException ex)
            {
                if (errorHandler != null)
                {
                    foreach (var e in ex.InnerExceptions)
                        errorHandler(e);
                }
                return defReturn;
            }
        }
    }
}
