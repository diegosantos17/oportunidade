using Minuto.Seguros.Domain.Entities;
using Minuto.Seguros.Infra.Data.Context;
using Minuto.Seguros.Infra.Data.Contracts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Minuto.Seguros.Infra.Data.Repository
{
    public class FeedRepository : IFeedRepository
    {
        private readonly MongoContext _mongoContext;

        public FeedRepository(MongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public Feed Add(Feed model)
        {
            _mongoContext.Feeds.InsertOneAsync(model);
            return model;
        }        

        public IEnumerable<Feed> All(Expression<Func<Feed, bool>> filter)
        {
            return _mongoContext.Feeds.Find(filter).ToList();
        }              

        public Feed Find(Expression<Func<Feed, bool>> filter)
        {
            return _mongoContext.Feeds.Find(filter).FirstOrDefault();
        }                
    }
}
