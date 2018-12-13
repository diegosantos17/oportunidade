using Minuto.Seguros.Domain.Entities;
using Minuto.Seguros.Infra.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Minuto.Seguros.Service.Contracts
{
    public interface IBaseService<T, TE> : IDisposable where T : IBaseRepository<TE> where TE : BaseEntity
    {
        TE Add(TE model);
        TE Find(Expression<Func<TE, bool>> filter);
        IEnumerable<TE> All(Expression<Func<TE, bool>> filter);
    }
}
