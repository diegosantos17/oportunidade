using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Minuto.Seguros.Infra.Data.Contracts
{
    public interface IBaseRepository<T> where T : class
    {
        T Add(T model);
        T Find(Expression<Func<T, bool>> filter);
        IEnumerable<T> All(Expression<Func<T, bool>> filter);
    }
}
