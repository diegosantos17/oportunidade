using Minuto.Seguros.Domain.Entities;
using Minuto.Seguros.Infra.Data.Contracts;
using Minuto.Seguros.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Minuto.Seguros.Service.Services
{
    internal abstract class BaseService<T, TE> : IBaseService<T, TE> where T : IBaseRepository<TE> where TE : BaseEntity
    {
        public readonly IBaseRepository<TE> _repository;

        public BaseService(IBaseRepository<TE> repository)
        {
            _repository = repository;
        }

        public virtual TE Add(TE entity)
        {
            return _repository.Add(entity);
        }        

        public IEnumerable<TE> All(Expression<Func<TE, bool>> filter)
        {
            return _repository.All(filter);
        }        

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }        

        public TE Find(Expression<Func<TE, bool>> filter)
        {
            return _repository.Find(filter);
        }
                
    }
}
