using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoperasiTenteraDAL.Repositories
{
    public interface IRepository<TEntity, TEntityId> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(TEntityId entityId);
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntityId entityId);
    }
}
